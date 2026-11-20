using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DATOS;
using ENTIDADES;
using LOGICA;
using System.Globalization;

namespace PRESENTACION
{
    public partial class FrmFacturacion : Form
    {
        private readonly ClienteService clienteService;
        private readonly EmpleadoService empleadoService;
        private readonly VehiculoService vehiculoService;
        private readonly AgendaService agendaService;
        private readonly ArticuloService articuloService;
        private readonly FacturaService facturaService;
        private readonly Validacion validacion = new Validacion();
        private List<Cliente> clientes = new List<Cliente>();
        private List<Empleado> empleados = new List<Empleado>();
        private List<Vehiculo> vehiculos = new List<Vehiculo>();
        private List<Agenda> agendas = new List<Agenda>();
        private List<Articulo> articulos = new List<Articulo>();
        private List<Factura> facturas = new List<Factura>();
        private readonly List<DetalleFactura> carrito = new List<DetalleFactura>();

        public FrmFacturacion()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            var clienteRepo = new ClienteRepository();
            var empleadoRepo = new EmpleadoRepository();
            var vehiculoRepo = new VehiculoRepository();
            var articuloRepo = new ArticuloRepository();
            var proveedorRepo = new ProveedorRepository();
            var agendaRepo = new AgendaRepository();
            clienteService = new ClienteService(clienteRepo);
            empleadoService = new EmpleadoService(empleadoRepo, new CargoRepository());
            vehiculoService = new VehiculoService(vehiculoRepo, clienteRepo);
            agendaService = new AgendaService(agendaRepo, clienteRepo, vehiculoRepo, empleadoRepo, articuloRepo);
            articuloService = new ArticuloService(articuloRepo, proveedorRepo);
            facturaService = new FacturaService(new FacturaRepository(), clienteRepo, empleadoRepo, articuloRepo);
            ConfigurarInteraccion();
            Load += (s, e) => CargarDatos();
        }

        private void ConfigurarInteraccion()
        {
            cmbTipo.SelectedIndex = 0;
            cmbEstadoPago.SelectedItem = "Pendiente";
            dtpFecha.Value = DateTime.Now;

            cmbTipo.SelectedIndexChanged += (s, e) => FiltrarAgendasVehiculo();
            cmbCliente.SelectedIndexChanged += (s, e) => FiltrarVehiculosCliente();
            cmbVehiculo.SelectedIndexChanged += (s, e) => FiltrarAgendasVehiculo();
            cmbAgenda.SelectionChangeCommitted += SeleccionarVehiculoDeCita;
            chkDomicilio.CheckedChanged += (s, e) =>
            {
                txtDireccion.Enabled = chkDomicilio.Checked;
                if (!chkDomicilio.Checked) txtDireccion.Clear();
            };
            btnAgregar.Click += AgregarItem_Click;
            btnQuitar.Click += QuitarItem_Click;
            btnGuardar.Click += GuardarDocumento_Click;
            btnNuevo.Click += (s, e) => NuevoDocumento();
            btnActualizar.Click += (s, e) => CargarHistorial();
            btnConvertir.Click += Convertir_Click;
            btnAnular.Click += Anular_Click;
            gridHistorial.CellClick += (s, e) => Historial_SelectionChanged(s, EventArgs.Empty);
            EstiloUI.ConectarTabla(gridCarrito);
            EstiloUI.ConectarTabla(gridHistorial);
            EstiloUI.ConectarTabla(gridDetalles);
        }

        private void CargarDatos()
        {
            string etapa = "inicializar listas";
            try
            {
                etapa = "consultar clientes";
                clientes = clienteService.Consultar();
                etapa = "consultar empleados";
                empleados = empleadoService.Consultar().Where(e => e.Activo).ToList();
                etapa = "consultar vehículos";
                vehiculos = vehiculoService.Consultar();
                etapa = "enlazar clientes";
                EnlazarCombo(cmbCliente, clientes.Select(c => new Opcion(c.Cedula, (c.Nombre + " " + c.Apellido).Trim() + " — " + c.Cedula)).ToList(), "Texto", "Valor");
                etapa = "enlazar empleados";
                EnlazarCombo(cmbEmpleado, empleados.Select(e => new Opcion(e.Cedula, (e.Nombre + " " + e.Apellido).Trim() + " — " + (e.Cargo == null ? "" : e.Cargo.NombreCargo))).ToList(), "Texto", "Valor");
                PreseleccionarEmpleado();
                if (empleados.Count == 0) errores.SetError(cmbEmpleado, "Registra un empleado en Empleados y cargos para emitir documentos.");
                etapa = "cargar artículos y citas";
                CargarArticulosYCitas();
                etapa = "enlazar vehículos";
                FiltrarVehiculosCliente();
                etapa = "cargar historial de documentos";
                CargarHistorial();
                Form1 principal = MdiParent as Form1;
                // El acceso de respaldo (cuando EMPLEADO está vacío) no tiene cédula.
                // SelectedValue no acepta una clave nula en un ComboBox enlazado.
                if (principal != null && principal.UsuarioActual != null &&
                    !string.IsNullOrWhiteSpace(principal.UsuarioActual.Cedula) &&
                    empleados.Any(e => string.Equals(e.Cedula, principal.UsuarioActual.Cedula, StringComparison.Ordinal)))
                {
                    cmbEmpleado.SelectedValue = principal.UsuarioActual.Cedula;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    "No se pudo cargar Facturación al " + etapa + ". " + ex.Message,
                    "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PreseleccionarEmpleado()
        {
            Empleado actual = (MdiParent as Form1)?.UsuarioActual;
            if (actual != null && !string.IsNullOrWhiteSpace(actual.Cedula) && empleados.Any(e => e.Cedula == actual.Cedula))
                cmbEmpleado.SelectedValue = actual.Cedula;
            else cmbEmpleado.SelectedIndex = empleados.Count > 0 ? 0 : -1;
        }

        private void CargarArticulosYCitas()
        {
            object articuloAnterior = cmbArticulo.SelectedValue;
            object citaAnterior = cmbAgenda.SelectedValue;
            articulos = articuloService.Consultar();
            agendas = agendaService.Consultar();
            EnlazarCombo(cmbArticulo, articulos.Select(a => new OpcionArticulo(a)).ToList(), "Texto", "Id");
            if (articuloAnterior is int id && articulos.Any(a => a.ArticuloId == id))
                cmbArticulo.SelectedValue = id;
            FiltrarAgendasVehiculo();
            if (citaAnterior is int cita && cmbAgenda.Items.Cast<Opcion>().Any(o => Equals(o.Valor, cita)))
                cmbAgenda.SelectedValue = cita;
        }

        private void NuevoDocumento()
        {
            carrito.Clear(); ActualizarCarrito(); errores.Clear();
            txtObservaciones.Clear(); txtDireccion.Clear(); chkDomicilio.Checked = false;
            cmbTipo.SelectedIndex = 0; cmbEstadoPago.SelectedItem = "Pendiente"; dtpFecha.Value = DateTime.Now;
            nudCantidad.Value = 1; nudDescuento.Value = 0;
            cmbCliente.SelectedIndex = clientes.Count > 0 ? 0 : -1;
            FiltrarVehiculosCliente(); PreseleccionarEmpleado();
        }

        private void FiltrarVehiculosCliente()
        {
            if (cmbVehiculo == null || cmbCliente == null) return;
            string cedula = cmbCliente.SelectedValue == null ? null : cmbCliente.SelectedValue.ToString();
            var opciones = new List<OpcionVehiculo> { new OpcionVehiculo(null) };
            opciones.AddRange(vehiculos.Where(v => v.Cliente != null && v.Cliente.Cedula == cedula).Select(v => new OpcionVehiculo(v)));
            EnlazarCombo(cmbVehiculo, opciones, "Texto", "Placa");
            cmbVehiculo.SelectedIndex = 0;
            FiltrarAgendasVehiculo();
        }

        private void FiltrarAgendasVehiculo()
        {
            if (cmbAgenda == null) return;
            string placa = cmbVehiculo.SelectedValue as string;
            string cedula = cmbCliente.SelectedValue as string;
            var opciones = new List<Opcion> { new Opcion(0, "Sin cita") };
            if (cmbTipo.Text == "VENTA" && !string.IsNullOrWhiteSpace(cedula))
            {
                var lista = agendas.Where(a => a.Vehiculo?.Cliente?.Cedula == cedula);
                if (string.IsNullOrEmpty(placa))
                    lista = lista.Where(a => string.Equals(a.Estado, "PENDIENTE", StringComparison.OrdinalIgnoreCase));
                else
                    lista = lista.Where(a => a.Vehiculo.Placa == placa &&
                        !string.Equals(a.Estado, "CANCELADA", StringComparison.OrdinalIgnoreCase) &&
                        !string.Equals(a.Estado, "COMPLETADA", StringComparison.OrdinalIgnoreCase));
                opciones.AddRange(lista.Select(a => new Opcion(a.AgendaID, a.Fecha.ToString("g") + " — " + a.Vehiculo.Placa + " — " + a.Estado)));
            }
            EnlazarCombo(cmbAgenda, opciones, "Texto", "Valor"); cmbAgenda.SelectedIndex = 0;
        }

        private void SeleccionarVehiculoDeCita(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbVehiculo.SelectedValue as string) ||
                !(cmbAgenda.SelectedValue is int id) || id == 0) return;
            Agenda cita = agendas.FirstOrDefault(a => a.AgendaID == id);
            if (cita?.Vehiculo == null) return;
            cmbVehiculo.SelectedValue = cita.Vehiculo.Placa;
            // Cambiar la placa vuelve a enlazar la lista de citas.
            cmbAgenda.SelectedValue = id;
        }

        private static void EnlazarCombo<T>(ComboBox combo, List<T> opciones, string miembroTexto, string miembroValor)
        {
            combo.BeginUpdate();
            try
            {
                combo.DataSource = null;
                combo.DisplayMember = miembroTexto;
                combo.ValueMember = miembroValor;
                combo.DataSource = opciones ?? new List<T>();
            }
            finally { combo.EndUpdate(); }
        }

        private void AgregarItem_Click(object sender, EventArgs e)
        {
            if (validacion.ValidarCampoVacioEspecificoCombo(cmbArticulo, errores))
            {
                MessageBox.Show(this, "Selecciona un artículo o servicio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            OpcionArticulo opcion = cmbArticulo.SelectedItem as OpcionArticulo;
            if (opcion == null) return;
            DetalleFactura linea = carrito.FirstOrDefault(d => d.Articulo.ArticuloId == opcion.Id);
            int cantidad = (int)nudCantidad.Value;
            long acumulada = (long)(linea?.Cantidad ?? 0) + cantidad;
            if (acumulada > int.MaxValue)
            {
                MessageBox.Show(this, "La cantidad acumulada supera el máximo permitido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            if (cmbTipo.Text == "VENTA" && opcion.Articulo is Repuesto repuesto && acumulada > repuesto.Cantidad)
            {
                MessageBox.Show(this, $"Stock insuficiente para {repuesto.Nombre}. Cantidad acumulada: {acumulada}. Disponibles: {repuesto.Cantidad}.",
                    "Inventario", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            decimal descuento = nudDescuento.Value;
            decimal subtotal = opcion.Articulo.PrecioVenta * (int)nudCantidad.Value;
            if (descuento > subtotal)
            {
                MessageBox.Show(this, "El descuento no puede superar el subtotal de la línea.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            if (linea == null)
                carrito.Add(new DetalleFactura(cantidad, opcion.Articulo) { Descuento = descuento });
            else
            {
                linea.Cantidad = (int)acumulada;
                linea.Descuento += descuento;
            }
            ActualizarCarrito(); nudDescuento.Value = 0; nudCantidad.Value = 1;
        }

        private void QuitarItem_Click(object sender, EventArgs e)
        {
            if (gridCarrito.SelectedRows.Count == 0) return;
            int index = gridCarrito.SelectedRows[0].Index;
            if (index >= 0 && index < carrito.Count) carrito.RemoveAt(index);
            ActualizarCarrito();
        }

        private void ActualizarCarrito()
        {
            gridCarrito.DataSource = null;
            gridCarrito.DataSource = carrito.Select(LineaCarrito.Crear).ToList();
            var factura = new Factura { Detalles = new List<DetalleFactura>(carrito) }; factura.CalcularTotales();
            lblSubtotal.Text = "Subtotal: " + factura.SubTotal.ToString("C2"); lblIva.Text = "IVA: " + factura.Iva.ToString("C2");
            lblDescuento.Text = "Descuento: " + factura.Descuento.ToString("C2"); lblTotal.Text = "Total: " + factura.Total.ToString("C2");
        }

        private void GuardarDocumento_Click(object sender, EventArgs e)
        {
            bool valido = !validacion.ValidarCampoVacioEspecificoCombo(cmbTipo, errores) & !validacion.ValidarCampoVacioEspecificoCombo(cmbCliente, errores) &
                !validacion.ValidarCampoVacioEspecificoCombo(cmbEmpleado, errores) & !validacion.ValidarCampoVacioEspecificoDataPiker(dtpFecha, errores);
            if (chkDomicilio.Checked) valido &= !validacion.ValidarCampoVacioEspecifico(txtDireccion, errores);
            if (!valido) { MessageBox.Show(this, "Completa los datos obligatorios del documento.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            var factura = new Factura
            {
                Fecha = dtpFecha.Value, TipoDocumento = cmbTipo.Text, EstadoPago = cmbEstadoPago.Text,
                Cliente = new Cliente { Cedula = cmbCliente.SelectedValue.ToString() },
                Empleado = new Empleado { Cedula = cmbEmpleado.SelectedValue.ToString() },
                Vehiculo = cmbVehiculo.SelectedValue == null || string.IsNullOrEmpty(cmbVehiculo.SelectedValue.ToString()) ? null : vehiculos.FirstOrDefault(v => v.Placa == cmbVehiculo.SelectedValue.ToString()),
                AgendaID = cmbAgenda.SelectedValue == null || Convert.ToInt32(cmbAgenda.SelectedValue) == 0 ? (int?)null : Convert.ToInt32(cmbAgenda.SelectedValue),
                Domicilio = chkDomicilio.Checked, DireccionDomicilio = chkDomicilio.Checked ? txtDireccion.Text.Trim() : null,
                Observaciones = string.IsNullOrWhiteSpace(txtObservaciones.Text) ? null : txtObservaciones.Text.Trim(),
                Detalles = new List<DetalleFactura>(carrito)
            };
            string resultado = facturaService.GuardarDocumento(factura);
            bool correcto = resultado.StartsWith("Se registró el documento", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, resultado, correcto ? "Documento" : "No se pudo guardar", MessageBoxButtons.OK, correcto ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (correcto) { carrito.Clear(); ActualizarCarrito(); txtObservaciones.Clear(); txtDireccion.Clear(); chkDomicilio.Checked = false; CargarHistorial(true); }
        }

        private void CargarHistorial(bool actualizarOpciones = false)
        {
            gridDetalles.DataSource = null;
            lblDetalleDocumento.Text = "Selecciona un documento para consultar sus líneas.";
            try
            {
                facturas = facturaService.Consultar();
                gridHistorial.DataSource = facturas.OrderByDescending(f => f.Fecha).Select(FacturaFila.Crear).ToList();
                if (actualizarOpciones) CargarArticulosYCitas();
            }
            catch (Exception ex) { MessageBox.Show(this, "No se pudo consultar el historial. " + ex.Message, "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Historial_SelectionChanged(object sender, EventArgs e)
        {
            if (gridHistorial.SelectedRows.Count == 0) return;
            Factura factura = (gridHistorial.SelectedRows[0].DataBoundItem as FacturaFila)?.Original;
            if (factura == null) return;
            gridDetalles.DataSource = factura.Detalles.Select(LineaHistorial.Crear).ToList();
            lblDetalleDocumento.Text = "Documento " + factura.FacturaID + " · " + factura.TipoDocumento + " · Domicilio: " + (factura.Domicilio ? factura.DireccionDomicilio : "No") + (factura.AgendaID.HasValue ? " · Cita: " + factura.AgendaID : "");
        }

        private void Convertir_Click(object sender, EventArgs e)
        {
            if (gridHistorial.SelectedRows.Count == 0) return;
            Factura factura = (gridHistorial.SelectedRows[0].DataBoundItem as FacturaFila)?.Original;
            if (factura == null || !string.Equals(factura.TipoDocumento, "COTIZACION", StringComparison.OrdinalIgnoreCase))
            { MessageBox.Show(this, "Selecciona una cotización para convertir.", "Historial", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (MessageBox.Show(this, "¿Convertir la cotización " + factura.FacturaID + " en venta? Se descontará el stock de repuestos.", "Confirmar conversión", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            string mensaje = facturaService.ConvertirCotizacionEnVenta(factura.FacturaID);
            bool correcto = mensaje.StartsWith("La cotización se convirtió", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, mensaje, "Conversión", MessageBoxButtons.OK, correcto ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (correcto) CargarHistorial(true);
        }

        private void Anular_Click(object sender, EventArgs e)
        {
            if (gridHistorial.SelectedRows.Count == 0) return;
            Factura factura = (gridHistorial.SelectedRows[0].DataBoundItem as FacturaFila)?.Original;
            if (factura == null) return;
            if (MessageBox.Show(this, "¿Anular el documento " + factura.FacturaID + "? Si es una venta se devolverán sus repuestos al inventario.",
                "Confirmar anulación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            string mensaje = facturaService.AnularDocumento(factura.FacturaID);
            bool correcto = mensaje.StartsWith("Documento anulado", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, mensaje, "Facturación", MessageBoxButtons.OK, correcto ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (correcto) CargarHistorial(true);
        }

        private sealed class Opcion
        {
            public Opcion(object valor, string texto) { Valor = valor; Texto = texto; }
            public object Valor { get; private set; } public string Texto { get; private set; }
        }
        private sealed class OpcionVehiculo
        {
            public OpcionVehiculo(Vehiculo v) { Placa = v == null ? "" : v.Placa; Texto = v == null ? "Sin vehículo" : v.Placa + " — " + (v.Cliente == null ? "" : (v.Cliente.Nombre + " " + v.Cliente.Apellido).Trim()); }
            public string Placa { get; private set; } public string Texto { get; private set; }
        }
        private sealed class OpcionArticulo
        {
            public OpcionArticulo(Articulo a) { Articulo = a; Id = a.ArticuloId; Texto = a.Nombre + (a is Repuesto r ? " (Repuesto, stock " + r.Cantidad + ")" : " (Servicio)"); }
            public Articulo Articulo { get; private set; } public int Id { get; private set; } public string Texto { get; private set; }
        }
        private sealed class LineaCarrito
        {
            public string Articulo { get; set; } public string Tipo { get; set; } public int Cantidad { get; set; } public decimal Precio { get; set; }
            public decimal PorcentajeIVA { get; set; } public decimal Descuento { get; set; } public decimal Subtotal { get; set; } public decimal Iva { get; set; } public decimal Total { get; set; }
            public static LineaCarrito Crear(DetalleFactura d) { return new LineaCarrito { Articulo = d.Descripcion, Tipo = d.Articulo is Repuesto ? "Repuesto" : "Servicio", Cantidad = d.Cantidad, Precio = d.ValorUnitario, PorcentajeIVA = d.PorcentajeIVA, Descuento = d.Descuento, Subtotal = d.Subtotal, Iva = d.ValorIVA, Total = d.Total }; }
        }
        private sealed class LineaHistorial
        {
            public string Articulo { get; set; } public int Cantidad { get; set; } public decimal Precio { get; set; } public decimal Descuento { get; set; } public decimal Iva { get; set; } public decimal Total { get; set; }
            public static LineaHistorial Crear(DetalleFactura d) { return new LineaHistorial { Articulo = d.Descripcion, Cantidad = d.Cantidad, Precio = d.ValorUnitario, Descuento = d.Descuento, Iva = d.ValorIVA, Total = d.Total }; }
        }
        private sealed class FacturaFila
        {
            public int Id { get; set; } public string Tipo { get; set; } public DateTime Fecha { get; set; } public string Cliente { get; set; } public string Empleado { get; set; }
            public string Vehiculo { get; set; } public decimal Subtotal { get; set; } public decimal Iva { get; set; } public decimal Descuento { get; set; } public decimal Total { get; set; } public string EstadoPago { get; set; } public Factura Original { get; set; }
            public static FacturaFila Crear(Factura f) { return new FacturaFila { Id = f.FacturaID, Tipo = f.TipoDocumento, Fecha = f.Fecha, Cliente = f.Cliente == null ? "" : (f.Cliente.Nombre + " " + f.Cliente.Apellido).Trim(), Empleado = f.Empleado == null ? "" : (f.Empleado.Nombre + " " + f.Empleado.Apellido).Trim(), Vehiculo = f.Vehiculo == null ? "" : f.Vehiculo.Placa, Subtotal = f.SubTotal, Iva = f.Iva, Descuento = f.Descuento, Total = f.Total, EstadoPago = f.EstadoPago, Original = f }; }
        }

        private void lblNudCantidad_Click(object sender, EventArgs e)
        {

        }

        private void gridCarrito_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
