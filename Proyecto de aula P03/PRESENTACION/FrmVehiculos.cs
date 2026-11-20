using DATOS;
using ENTIDADES;
using LOGICA;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace PRESENTACION
{
    public partial class FrmVehiculos : Form
    {
        private readonly ClienteService clienteService;
        private readonly VehiculoService vehiculoService;
        private readonly SoatService soatService;
        private readonly Validacion validacion = new Validacion();
        private List<Cliente> clientes = new List<Cliente>();
        private List<Vehiculo> vehiculos = new List<Vehiculo>();
        private List<Soat> soats = new List<Soat>();

        private bool editandoVehiculo;

        private bool editandoSoat;
        private int soatIdSeleccionado;

        public FrmVehiculos()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            var clienteRepository = new ClienteRepository();
            clienteService = new ClienteService(clienteRepository);
            vehiculoService = new VehiculoService(new VehiculoRepository(), clienteRepository);
            soatService = new SoatService(new SoatRepository(), new VehiculoRepository());
            ConfigurarInteraccion();
            Load += (s, e) => CargarTodo();
        }

        private void ConfigurarInteraccion()
        {
            DateTime hoy = DateTime.Today;
            dtpFechaInicio.MaxDate = hoy.AddYears(10);
            dtpFechaFin.MaxDate = hoy.AddYears(10);
            dtpFechaInicio.Value = hoy;
            dtpFechaFin.Value = hoy;

            btnGuardarVehiculo.Click += GuardarVehiculo_Click;
            btnNuevoVehiculo.Click += (s, e) => LimpiarVehiculo();
            btnEliminarVehiculo.Click += EliminarVehiculo_Click;
            txtModelo.KeyPress += SoloDigitos_KeyPress;
            txtBuscarVehiculo.TextChanged += (s, e) => MostrarVehiculos();
            gridVehiculos.CellClick += (s, e) => GridVehiculos_SelectionChanged(s, EventArgs.Empty);

            btnGuardarSoat.Click += GuardarSoat_Click;
            btnNuevoSoat.Click += (s, e) => LimpiarSoat();
            btnEliminarSoat.Click += EliminarSoat_Click;
            txtNumeroSoat.KeyPress += SoloDigitos_KeyPress;
            cmbPlacaSoat.SelectedValueChanged += (s, e) => MostrarSoats();
            gridSoat.CellClick += (s, e) => GridSoat_SelectionChanged(s, EventArgs.Empty);
            EstiloUI.ConectarTabla(gridVehiculos);
            EstiloUI.ConectarTabla(gridSoat);
        }

        private void CargarTodo()
        {
            try
            {
                clientes = clienteService.Consultar();
                vehiculos = vehiculoService.Consultar();
                soats = soatService.Consultar();
                CargarPropietarios();
                CargarPlacasSoat();
                MostrarVehiculos();
                MostrarSoats();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "No se pudieron cargar vehículos o pólizas SOAT. " + ex.Message,
                    "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarPropietarios()
        {
            var seleccion = cmbPropietario.SelectedValue == null ? null : cmbPropietario.SelectedValue.ToString();
            cmbPropietario.DataSource = null;
            cmbPropietario.DataSource = clientes.Select(c => new ClienteOpcion(c)).ToList();
            cmbPropietario.DisplayMember = "Texto";
            cmbPropietario.ValueMember = "Cedula";
            if (seleccion != null && clientes.Any(c => c.Cedula == seleccion)) cmbPropietario.SelectedValue = seleccion;
            else cmbPropietario.SelectedIndex = clientes.Count > 0 ? 0 : -1;
        }

        private void CargarPlacasSoat()
        {
            string seleccion = cmbPlacaSoat.SelectedValue == null ? null : cmbPlacaSoat.SelectedValue.ToString();
            cmbPlacaSoat.DataSource = null;
            cmbPlacaSoat.DataSource = vehiculos.Select(v => new VehiculoOpcion(v)).ToList();
            cmbPlacaSoat.DisplayMember = "Texto";
            cmbPlacaSoat.ValueMember = "Placa";
            if (seleccion != null && vehiculos.Any(v => v.Placa == seleccion)) cmbPlacaSoat.SelectedValue = seleccion;
            else cmbPlacaSoat.SelectedIndex = vehiculos.Count > 0 ? 0 : -1;
        }

        private void MostrarVehiculos()
        {
            if (gridVehiculos == null) return;
            string filtro = txtBuscarVehiculo == null ? string.Empty : txtBuscarVehiculo.Text.Trim();
            IEnumerable<Vehiculo> lista = vehiculos;
            if (filtro.Length > 0)
                lista = vehiculos.Where(v => v.Placa.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (v.Cliente != null && v.Cliente.Cedula.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0));
            gridVehiculos.DataSource = lista.Select(v => new VehiculoFila
            {
                Placa = v.Placa, Marca = v.Marca, Linea = v.Linea, Modelo = v.Modelo,
                TipoVehiculo = v.TipoVehiculo, Color = v.Color,
                Propietario = v.Cliente == null ? string.Empty : (v.Cliente.Nombre + " " + v.Cliente.Apellido).Trim(),
                CedulaPropietario = v.Cliente == null ? string.Empty : v.Cliente.Cedula,
                Original = v
            }).ToList();
        }

        private void MostrarSoats()
        {
            if (gridSoat == null) return;
            string placa = cmbPlacaSoat == null || cmbPlacaSoat.SelectedValue == null ? null : cmbPlacaSoat.SelectedValue.ToString();
            IEnumerable<Soat> lista = soats;
            if (!string.IsNullOrWhiteSpace(placa)) lista = soats.Where(s => s.Vehiculo != null && s.Vehiculo.Placa == placa);
            gridSoat.DataSource = lista.Select(s => new SoatFila
            {
                SoatId = s.SoatId, NumeroSoat = s.NumeroSoat, Placa = s.Vehiculo == null ? string.Empty : s.Vehiculo.Placa,
                Vehiculo = s.Vehiculo == null ? string.Empty : (s.Vehiculo.Marca + " " + s.Vehiculo.Linea).Trim(),
                Propietario = s.Vehiculo == null || s.Vehiculo.Cliente == null ? string.Empty : s.Vehiculo.Cliente.NombreCompleto,
                FechaInicio = s.FechaInicio.ToString("dd/MM/yyyy"), FechaFin = s.FechaFin.ToString("dd/MM/yyyy"),
                Estado = s.FechaInicio.Date > DateTime.Today ? "PENDIENTE VIGENCIA" : s.FechaFin.Date < DateTime.Today ? "VENCIDO" : s.FechaFin.Date <= DateTime.Today.AddDays(30) ? "POR VENCER" : "VIGENTE", Original = s
            }).ToList();
        }

        private void GuardarVehiculo_Click(object sender, EventArgs e)
        {
            if (!ValidarVehiculo()) return;
            int modelo;
            if (!int.TryParse(txtModelo.Text, NumberStyles.None, CultureInfo.InvariantCulture, out modelo))
            {
                MessageBox.Show(this, "Escribe un año válido para el modelo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string cedula = cmbPropietario.SelectedValue == null ? null : cmbPropietario.SelectedValue.ToString();
            string resultado = editandoVehiculo
                ? vehiculoService.Modificar(txtPlaca.Text.Trim(), modelo, TextoOpcional(txtMarca), cmbTipoVehiculo.Text,
                    TextoOpcional(txtColor), TextoOpcional(txtLinea), cedula)
                : vehiculoService.Guardar(txtPlaca.Text.Trim(), modelo, TextoOpcional(txtMarca), cmbTipoVehiculo.Text,
                    TextoOpcional(txtColor), TextoOpcional(txtLinea), cedula);
            bool correcto = resultado.StartsWith("se registro el vehiculo", StringComparison.OrdinalIgnoreCase) ||
                            resultado.StartsWith("El vehiculo se modifico", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, resultado, correcto ? "Vehículos" : "No se pudo guardar", MessageBoxButtons.OK,
                correcto ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (correcto) { CargarTodo(); LimpiarVehiculo(); }
        }

        private bool ValidarVehiculo()
        {
            bool valido = true;
            valido &= !validacion.ValidarCampoVacioEspecifico(txtPlaca, errores);
            valido &= !validacion.ValidarCampoVacioEspecifico(txtModelo, errores);
            valido &= !validacion.ValidarCampoVacioEspecificoCombo(cmbTipoVehiculo, errores);
            valido &= !validacion.ValidarCampoVacioEspecificoCombo(cmbPropietario, errores);
            if (!validacion.ValidarCampoNumero(txtModelo, errores)) valido = false;
            if (!valido) MessageBox.Show(this, "Completa los campos obligatorios y revisa el modelo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return valido;
        }

        private void EliminarVehiculo_Click(object sender, EventArgs e)
        {
            Vehiculo vehiculo = gridVehiculos.SelectedRows.Count == 0 ? null : (gridVehiculos.SelectedRows[0].DataBoundItem as VehiculoFila)?.Original;
            if (vehiculo == null) return;
            if (MessageBox.Show(this, "¿Eliminar el vehículo seleccionado?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            string resultado = vehiculoService.Eliminar(vehiculo.Placa);
            bool correcto = resultado.StartsWith("El vehiculo se elimino", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, resultado, correcto ? "Vehículos" : "No se pudo eliminar", MessageBoxButtons.OK,
                correcto ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (correcto) { CargarTodo(); LimpiarVehiculo(); }
        }

        private void GridVehiculos_SelectionChanged(object sender, EventArgs e)
        {
            Vehiculo vehiculo = gridVehiculos.SelectedRows.Count == 0 ? null : (gridVehiculos.SelectedRows[0].DataBoundItem as VehiculoFila)?.Original;
            if (vehiculo == null) return;
            editandoVehiculo = true;
            txtPlaca.Text = vehiculo.Placa;
            txtPlaca.Enabled = false;
            txtMarca.Text = vehiculo.Marca;
            txtModelo.Text = vehiculo.Modelo.ToString(CultureInfo.InvariantCulture);
            cmbTipoVehiculo.SelectedItem = vehiculo.TipoVehiculo;
            txtColor.Text = vehiculo.Color;
            txtLinea.Text = vehiculo.Linea;
            if (vehiculo.Cliente != null) cmbPropietario.SelectedValue = vehiculo.Cliente.Cedula;
            if (vehiculos.Any(v => v.Placa == vehiculo.Placa)) cmbPlacaSoat.SelectedValue = vehiculo.Placa;
            btnGuardarVehiculo.Text = "Actualizar";
        }

        private void LimpiarVehiculo()
        {
            editandoVehiculo = false;
            txtPlaca.Enabled = true;
            txtPlaca.Clear(); txtMarca.Clear(); txtModelo.Clear(); txtColor.Clear(); txtLinea.Clear();
            cmbTipoVehiculo.SelectedIndex = -1;
            cmbPropietario.SelectedIndex = clientes.Count > 0 ? 0 : -1;
            btnGuardarVehiculo.Text = "Guardar";
            foreach (Control control in new Control[] { txtPlaca, txtModelo, cmbTipoVehiculo, cmbPropietario }) errores.SetError(control, string.Empty);
            gridVehiculos.ClearSelection();
            txtPlaca.Focus();
        }

        private void GuardarSoat_Click(object sender, EventArgs e)
        {
            bool valido = !validacion.ValidarCampoVacioEspecifico(txtNumeroSoat, errores) &
                          !validacion.ValidarCampoVacioEspecificoCombo(cmbPlacaSoat, errores) &
                          !validacion.ValidarCampoVacioEspecificoDataPiker(dtpFechaInicio, errores) &
                          !validacion.ValidarCampoVacioEspecificoDataPiker(dtpFechaFin, errores);
            if (!valido)
            {
                MessageBox.Show(this, "Completa los datos de la póliza SOAT.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string placa = cmbPlacaSoat.SelectedValue == null ? null : cmbPlacaSoat.SelectedValue.ToString();
            string resultado = editandoSoat
                ? soatService.Modificar(soatIdSeleccionado, txtNumeroSoat.Text.Trim(), dtpFechaInicio.Value.Date, dtpFechaFin.Value.Date, placa)
                : soatService.Guardar(txtNumeroSoat.Text.Trim(), dtpFechaInicio.Value.Date, dtpFechaFin.Value.Date, placa);
            bool correcto = resultado.StartsWith("se registro el SOAT", StringComparison.OrdinalIgnoreCase) ||
                            resultado.StartsWith("El SOAT se modifico", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, resultado, correcto ? "SOAT" : "No se pudo guardar", MessageBoxButtons.OK,
                correcto ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (correcto) { CargarTodo(); LimpiarSoat(); }
        }

        private void EliminarSoat_Click(object sender, EventArgs e)
        {
            Soat soat = gridSoat.SelectedRows.Count == 0 ? null : (gridSoat.SelectedRows[0].DataBoundItem as SoatFila)?.Original;
            if (soat == null) return;
            if (MessageBox.Show(this, "¿Eliminar la póliza SOAT seleccionada?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            string resultado = soatService.Eliminar(soat.SoatId);
            bool correcto = resultado.StartsWith("El SOAT se elimino", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, resultado, correcto ? "SOAT" : "No se pudo eliminar", MessageBoxButtons.OK,
                correcto ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (correcto) { CargarTodo(); LimpiarSoat(); }
        }

        private void GridSoat_SelectionChanged(object sender, EventArgs e)
        {
            Soat soat = gridSoat.SelectedRows.Count == 0 ? null : (gridSoat.SelectedRows[0].DataBoundItem as SoatFila)?.Original;
            if (soat == null) return;
            editandoSoat = true;
            soatIdSeleccionado = soat.SoatId;
            txtNumeroSoat.Text = soat.NumeroSoat;
            dtpFechaInicio.Value = soat.FechaInicio < dtpFechaInicio.MinDate ? dtpFechaInicio.MinDate : soat.FechaInicio;
            dtpFechaFin.Value = soat.FechaFin < dtpFechaFin.MinDate ? dtpFechaFin.MinDate : soat.FechaFin;
            if (soat.Vehiculo != null) cmbPlacaSoat.SelectedValue = soat.Vehiculo.Placa;
            btnGuardarSoat.Text = "Actualizar";
        }

        private void LimpiarSoat()
        {
            editandoSoat = false;
            soatIdSeleccionado = 0;
            txtNumeroSoat.Clear();
            dtpFechaInicio.Value = DateTime.Today;
            dtpFechaFin.Value = DateTime.Today.AddYears(1);
            btnGuardarSoat.Text = "Guardar";
            errores.SetError(txtNumeroSoat, string.Empty);
            errores.SetError(cmbPlacaSoat, string.Empty);
            gridSoat.ClearSelection();
            txtNumeroSoat.Focus();
        }

        private static void SoloDigitos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private static string TextoOpcional(TextBox control)
        {
            return string.IsNullOrWhiteSpace(control.Text) ? null : control.Text.Trim();
        }

        private sealed class ClienteOpcion
        {
            public ClienteOpcion(Cliente cliente)
            {
                Cedula = cliente.Cedula;
                Texto = string.Format("{0} {1} — {2}", cliente.Nombre, cliente.Apellido, cliente.Cedula).Trim();
            }
            public string Cedula { get; private set; }
            public string Texto { get; private set; }
        }

        private sealed class VehiculoOpcion
        {
            public VehiculoOpcion(Vehiculo vehiculo)
            {
                Placa = vehiculo.Placa;
                Texto = vehiculo.Placa + (vehiculo.Cliente == null ? string.Empty : " — " + vehiculo.Cliente.Nombre + " " + vehiculo.Cliente.Apellido);
            }
            public string Placa { get; private set; }
            public string Texto { get; private set; }
        }

        private sealed class VehiculoFila
        {
            public string Placa { get; set; }
            public string Marca { get; set; }
            public string Linea { get; set; }
            public int Modelo { get; set; }
            public string TipoVehiculo { get; set; }
            public string Color { get; set; }
            public string Propietario { get; set; }
            public string CedulaPropietario { get; set; }
            public Vehiculo Original { get; set; }
        }

        private sealed class SoatFila
        {
            public int SoatId { get; set; }
            public string NumeroSoat { get; set; }
            public string Placa { get; set; }
            public string Vehiculo { get; set; }
            public string Propietario { get; set; }
            public string FechaInicio { get; set; }
            public string FechaFin { get; set; }
            public string Estado { get; set; }
            public Soat Original { get; set; }
        }
    }
}
