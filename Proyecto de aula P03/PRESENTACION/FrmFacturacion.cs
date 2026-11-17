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
        private readonly ErrorProvider errores;
        private List<Cliente> clientes = new List<Cliente>();
        private List<Empleado> empleados = new List<Empleado>();
        private List<Vehiculo> vehiculos = new List<Vehiculo>();
        private List<Agenda> agendas = new List<Agenda>();
        private List<Articulo> articulos = new List<Articulo>();
        private List<Factura> facturas = new List<Factura>();
        private readonly List<DetalleFactura> carrito = new List<DetalleFactura>();
        private ComboBox cmbTipo, cmbCliente, cmbEmpleado, cmbVehiculo, cmbAgenda, cmbArticulo;
        private ComboBox cmbEstadoPago;
        private DateTimePicker dtpFecha;
        private CheckBox chkDomicilio;
        private TextBox txtDireccion, txtObservaciones;
        private NumericUpDown nudCantidad, nudDescuento;
        private DataGridView gridCarrito, gridHistorial, gridDetalles;
        private Label lblSubtotal, lblIva, lblDescuento, lblTotal, lblDetalleDocumento;

        public FrmFacturacion()
        {
            InitializeComponent();
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
            errores = new ErrorProvider(components) { ContainerControl = this };
            ConstruirInterfaz();
            Load += (s, e) => CargarDatos();
        }

        private void ConstruirInterfaz()
        {
            Text = "Facturación y cotizaciones"; ClientSize = new Size(1160, 760); MinimumSize = new Size(960, 620);
            BackColor = Color.FromArgb(245, 247, 250); Font = new Font("Segoe UI", 9F);
            var tabs = new TabControl { Dock = DockStyle.Fill };
            var tabNuevo = new TabPage("Nuevo documento") { BackColor = BackColor };
            var tabHistorial = new TabPage("Historial") { BackColor = BackColor };
            tabs.TabPages.Add(tabNuevo); tabs.TabPages.Add(tabHistorial); Controls.Add(tabs);
            ConstruirTabNuevo(tabNuevo); ConstruirTabHistorial(tabHistorial);
        }

        private void ConstruirTabNuevo(TabPage tab)
        {
            var encabezado = new Label { Text = "FACTURACIÓN / COTIZACIONES", Dock = DockStyle.Top, Height = 40, Padding = new Padding(16, 8, 0, 0), Font = new Font("Segoe UI", 15F, FontStyle.Bold) };
            tab.Controls.Add(encabezado);
            var cabecera = new TableLayoutPanel { Dock = DockStyle.Top, Height = 184, Padding = new Padding(14, 2, 14, 2), ColumnCount = 4, RowCount = 4, BackColor = Color.White };
            cabecera.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110)); cabecera.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            cabecera.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110)); cabecera.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            for (int i = 0; i < 4; i++) cabecera.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            cmbTipo = CrearCombo(); cmbTipo.Items.AddRange(new object[] { "VENTA", "COTIZACION" }); cmbTipo.SelectedIndex = 0;
            cmbCliente = CrearCombo(); cmbCliente.SelectedIndexChanged += (s, e) => FiltrarVehiculosCliente();
            cmbEmpleado = CrearCombo(); cmbVehiculo = CrearCombo(); cmbVehiculo.SelectedIndexChanged += (s, e) => FiltrarAgendasVehiculo();
            cmbAgenda = CrearCombo(); cmbEstadoPago = CrearCombo(); cmbEstadoPago.Items.AddRange(new object[] { "Pendiente", "Pagada", "Anulada" }); cmbEstadoPago.SelectedItem = "Pendiente";
            dtpFecha = new DateTimePicker { Dock = DockStyle.Fill, Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy HH:mm", ShowUpDown = true, Value = DateTime.Now };
            chkDomicilio = new CheckBox { Text = "Domicilio", AutoSize = true, Anchor = AnchorStyles.Left, Checked = false };
            chkDomicilio.CheckedChanged += (s, e) => { txtDireccion.Enabled = chkDomicilio.Checked; if (!chkDomicilio.Checked) txtDireccion.Clear(); };
            txtDireccion = new TextBox { Dock = DockStyle.Fill, MaxLength = 150, Enabled = false };
            txtObservaciones = new TextBox { Dock = DockStyle.Fill, MaxLength = 200 };
            AgregarCampo(cabecera, 0, "Documento", cmbTipo, 0); AgregarCampo(cabecera, 0, "Cliente", cmbCliente, 2);
            AgregarCampo(cabecera, 1, "Atiende", cmbEmpleado, 0); AgregarCampo(cabecera, 1, "Vehículo (opcional)", cmbVehiculo, 2);
            AgregarCampo(cabecera, 2, "Cita (opcional)", cmbAgenda, 0); AgregarCampo(cabecera, 2, "Fecha", dtpFecha, 2);
            cabecera.Controls.Add(chkDomicilio, 0, 3); cabecera.Controls.Add(txtDireccion, 1, 3);
            cabecera.Controls.Add(new Label { Text = "Estado de pago", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 2, 3); cabecera.Controls.Add(cmbEstadoPago, 3, 3);
            tab.Controls.Add(cabecera);

            var obsPanel = new Panel { Dock = DockStyle.Top, Height = 42, Padding = new Padding(14, 4, 14, 2) };
            obsPanel.Controls.Add(new Label { Text = "Observaciones:", AutoSize = true, Location = new Point(14, 10) });
            txtObservaciones.Location = new Point(120, 5); txtObservaciones.Width = 600; txtObservaciones.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            obsPanel.Controls.Add(txtObservaciones); tab.Controls.Add(obsPanel);

            var accionesDetalle = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 44, Padding = new Padding(10, 4, 0, 3), BackColor = Color.White };
            cmbArticulo = CrearCombo(); cmbArticulo.Width = 330;
            nudCantidad = new NumericUpDown { Minimum = 1, Maximum = 1000000, Value = 1, Width = 90, DecimalPlaces = 0, ThousandsSeparator = true };
            nudDescuento = new NumericUpDown { Minimum = 0, Maximum = 99999999.99M, Value = 0, Width = 130, DecimalPlaces = 2, ThousandsSeparator = true };
            var btnAgregar = CrearBoton("Agregar ítem", 118); var btnQuitar = CrearBoton("Quitar ítem", 108);
            btnAgregar.Click += AgregarItem_Click; btnQuitar.Click += QuitarItem_Click;
            accionesDetalle.Controls.Add(new Label { Text = "Artículo:", AutoSize = true, Padding = new Padding(0, 7, 0, 0) });
            accionesDetalle.Controls.Add(cmbArticulo); accionesDetalle.Controls.Add(new Label { Text = "Cantidad:", AutoSize = true, Padding = new Padding(0, 7, 0, 0) });
            accionesDetalle.Controls.Add(nudCantidad); accionesDetalle.Controls.Add(new Label { Text = "Desc. línea:", AutoSize = true, Padding = new Padding(0, 7, 0, 0) });
            accionesDetalle.Controls.Add(nudDescuento); accionesDetalle.Controls.Add(btnAgregar); accionesDetalle.Controls.Add(btnQuitar);
            tab.Controls.Add(accionesDetalle);

            gridCarrito = CrearGrid();
            gridCarrito.Columns.Add(Columna("Artículo", "Articulo")); gridCarrito.Columns.Add(Columna("Tipo", "Tipo"));
            gridCarrito.Columns.Add(Columna("Cantidad", "Cantidad")); gridCarrito.Columns.Add(Columna("Precio unitario", "Precio"));
            gridCarrito.Columns.Add(Columna("IVA %", "PorcentajeIVA")); gridCarrito.Columns.Add(Columna("Descuento", "Descuento"));
            gridCarrito.Columns.Add(Columna("Subtotal", "Subtotal")); gridCarrito.Columns.Add(Columna("IVA", "Iva")); gridCarrito.Columns.Add(Columna("Total", "Total"));
            tab.Controls.Add(gridCarrito);

            var pie = new Panel { Dock = DockStyle.Bottom, Height = 76, BackColor = Color.White, Padding = new Padding(14, 5, 14, 5) };
            lblSubtotal = CrearEtiquetaTotal("Subtotal: —", 0); lblIva = CrearEtiquetaTotal("IVA: —", 190);
            lblDescuento = CrearEtiquetaTotal("Descuento: —", 340); lblTotal = CrearEtiquetaTotal("Total: —", 520);
            var btnGuardar = CrearBoton("Guardar documento", 180); btnGuardar.Anchor = AnchorStyles.Top | AnchorStyles.Right; btnGuardar.Location = new Point(Width - 230, 19);
            btnGuardar.Click += GuardarDocumento_Click; pie.Controls.AddRange(new Control[] { lblSubtotal, lblIva, lblDescuento, lblTotal, btnGuardar });
            tab.Controls.Add(pie);
            tab.Controls.SetChildIndex(encabezado, 0); tab.Controls.SetChildIndex(cabecera, 1); tab.Controls.SetChildIndex(obsPanel, 2);
            tab.Controls.SetChildIndex(accionesDetalle, 3); tab.Controls.SetChildIndex(pie, 4); tab.Controls.SetChildIndex(gridCarrito, 5);
        }

        private void ConstruirTabHistorial(TabPage tab)
        {
            var barra = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 44, Padding = new Padding(12, 4, 0, 3), BackColor = Color.White };
            var btnActualizar = CrearBoton("Actualizar", 110); var btnConvertir = CrearBoton("Convertir cotización en venta", 240);
            btnActualizar.Click += (s, e) => CargarHistorial(); btnConvertir.Click += Convertir_Click;
            barra.Controls.Add(btnActualizar); barra.Controls.Add(btnConvertir); tab.Controls.Add(barra);
            gridHistorial = CrearGrid();
            gridHistorial.Columns.Add(Columna("ID", "Id")); gridHistorial.Columns.Add(Columna("Tipo", "Tipo"));
            gridHistorial.Columns.Add(Columna("Fecha", "Fecha")); gridHistorial.Columns.Add(Columna("Cliente", "Cliente"));
            gridHistorial.Columns.Add(Columna("Empleado", "Empleado")); gridHistorial.Columns.Add(Columna("Vehículo", "Vehiculo"));
            gridHistorial.Columns.Add(Columna("Subtotal", "Subtotal")); gridHistorial.Columns.Add(Columna("IVA", "Iva"));
            gridHistorial.Columns.Add(Columna("Descuento", "Descuento")); gridHistorial.Columns.Add(Columna("Total", "Total"));
            gridHistorial.Columns.Add(Columna("Pago", "EstadoPago")); gridHistorial.SelectionChanged += Historial_SelectionChanged;
            tab.Controls.Add(gridHistorial);
            gridDetalles = CrearGrid();
            gridDetalles.Columns.Add(Columna("Artículo", "Articulo")); gridDetalles.Columns.Add(Columna("Cantidad", "Cantidad"));
            gridDetalles.Columns.Add(Columna("Precio unitario", "Precio")); gridDetalles.Columns.Add(Columna("Descuento", "Descuento"));
            gridDetalles.Columns.Add(Columna("IVA", "Iva")); gridDetalles.Columns.Add(Columna("Total", "Total"));
            tab.Controls.Add(gridDetalles);
            lblDetalleDocumento = new Label { Text = "Selecciona un documento para consultar sus líneas.", Dock = DockStyle.Bottom, Height = 28, Padding = new Padding(12, 4, 0, 0) };
            tab.Controls.Add(lblDetalleDocumento);
            tab.Controls.SetChildIndex(barra, 0); tab.Controls.SetChildIndex(gridHistorial, 1); tab.Controls.SetChildIndex(lblDetalleDocumento, 2); tab.Controls.SetChildIndex(gridDetalles, 3);
            gridHistorial.Dock = DockStyle.Top; gridHistorial.Height = 270; gridDetalles.Dock = DockStyle.Fill;
        }

        private void CargarDatos()
        {
            string etapa = "inicializar listas";
            try
            {
                etapa = "consultar clientes";
                clientes = clienteService.Consultar();
                etapa = "consultar empleados";
                empleados = empleadoService.Consultar();
                etapa = "consultar vehículos";
                vehiculos = vehiculoService.Consultar();
                etapa = "consultar artículos y servicios";
                articulos = articuloService.Consultar();
                etapa = "consultar agenda";
                agendas = agendaService.Consultar();
                etapa = "enlazar clientes";
                EnlazarCombo(cmbCliente, clientes.Select(c => new Opcion(c.Cedula, (c.Nombre + " " + c.Apellido).Trim() + " — " + c.Cedula)).ToList(), "Texto", "Valor");
                etapa = "enlazar empleados";
                EnlazarCombo(cmbEmpleado, empleados.Select(e => new Opcion(e.Cedula, (e.Nombre + " " + e.Apellido).Trim() + " — " + (e.Cargo == null ? "" : e.Cargo.NombreCargo))).ToList(), "Texto", "Valor");
                etapa = "enlazar artículos y servicios";
                EnlazarCombo(cmbArticulo, articulos.Select(a => new OpcionArticulo(a)).ToList(), "Texto", "Id");
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
            string placa = cmbVehiculo == null || cmbVehiculo.SelectedValue == null ? "" : cmbVehiculo.SelectedValue.ToString();
            IEnumerable<Agenda> lista = agendas;
            if (!string.IsNullOrEmpty(placa)) lista = lista.Where(a => a.Vehiculo.Placa == placa);
            var opciones = new List<Opcion> { new Opcion(0, "Sin cita") };
            opciones.AddRange(lista.Where(a => !string.Equals(a.Estado, "CANCELADA", StringComparison.OrdinalIgnoreCase))
                .Select(a => new Opcion(a.AgendaID, a.Fecha.ToString("g") + " — " + a.Vehiculo.Placa + " — " + a.Estado)));
            EnlazarCombo(cmbAgenda, opciones, "Texto", "Valor"); cmbAgenda.SelectedIndex = 0;
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
            if (!validacion.ValidarCampoVacioEspecificoCombo(cmbArticulo, errores))
            {
                MessageBox.Show(this, "Selecciona un artículo o servicio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            OpcionArticulo opcion = cmbArticulo.SelectedItem as OpcionArticulo;
            if (opcion == null) return;
            decimal descuento = nudDescuento.Value;
            decimal subtotal = opcion.Articulo.PrecioVenta * (int)nudCantidad.Value;
            if (descuento > subtotal)
            {
                MessageBox.Show(this, "El descuento no puede superar el subtotal de la línea.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            carrito.Add(new DetalleFactura((int)nudCantidad.Value, opcion.Articulo) { Descuento = descuento });
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
            bool valido = validacion.ValidarCampoVacioEspecificoCombo(cmbTipo, errores) & validacion.ValidarCampoVacioEspecificoCombo(cmbCliente, errores) &
                validacion.ValidarCampoVacioEspecificoCombo(cmbEmpleado, errores) & validacion.ValidarCampoVacioEspecificoDataPiker(dtpFecha, errores);
            if (chkDomicilio.Checked) valido &= validacion.ValidarCampoVacioEspecifico(txtDireccion, errores);
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
            if (correcto) { carrito.Clear(); ActualizarCarrito(); txtObservaciones.Clear(); txtDireccion.Clear(); chkDomicilio.Checked = false; CargarHistorial(); }
        }

        private void CargarHistorial()
        {
            try
            {
                facturas = facturaService.Consultar();
                gridHistorial.DataSource = facturas.OrderByDescending(f => f.Fecha).Select(FacturaFila.Crear).ToList();
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
            if (correcto) CargarHistorial();
        }

        private static ComboBox CrearCombo() { return new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList }; }
        private static DataGridView CrearGrid() { return new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false, AutoGenerateColumns = false, MultiSelect = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, RowHeadersVisible = false, BackgroundColor = Color.White, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill }; }
        private static DataGridViewTextBoxColumn Columna(string h, string p) { return new DataGridViewTextBoxColumn { HeaderText = h, DataPropertyName = p, Name = p, SortMode = DataGridViewColumnSortMode.NotSortable }; }
        private static Button CrearBoton(string t, int w) { return new Button { Text = t, Width = w, Height = 32, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(45, 95, 145), ForeColor = Color.White, Margin = new Padding(4) }; }
        private static Label CrearEtiquetaTotal(string t, int x) { return new Label { Text = t, AutoSize = true, Location = new Point(14 + x, 28), Font = new Font("Segoe UI", 10F, FontStyle.Bold), Anchor = AnchorStyles.Left | AnchorStyles.Top }; }
        private static void AgregarCampo(TableLayoutPanel p, int r, string t, Control c, int col) { p.Controls.Add(new Label { Text = t, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, col, r); p.Controls.Add(c, col + 1, r); }

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
    }
}
