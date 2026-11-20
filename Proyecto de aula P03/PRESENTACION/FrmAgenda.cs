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
    public partial class FrmAgenda : Form
    {
        private readonly AgendaService agendaService;
        private readonly NotificacionMantenimientoService mantenimientoService;
        private readonly ClienteService clienteService;
        private readonly EmpleadoService empleadoService;
        private readonly ArticuloService articuloService;
        private readonly Validacion validacion = new Validacion();
        private List<Agenda> agendas = new List<Agenda>();
        private List<Vehiculo> vehiculos = new List<Vehiculo>();
        private List<Empleado> empleados = new List<Empleado>();
        private List<Servicio> servicios = new List<Servicio>();
        private bool editando;
        private int agendaSeleccionada;

        public FrmAgenda()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            var clienteRepo = new ClienteRepository();
            var vehiculoRepo = new VehiculoRepository();
            var empleadoRepo = new EmpleadoRepository();
            var articuloRepo = new ArticuloRepository();
            var agendaRepo = new AgendaRepository();
            var notificacionRepo = new NotificacionMantenimientoRepository();
            var proveedorRepo = new ProveedorRepository();
            clienteService = new ClienteService(clienteRepo);
            empleadoService = new EmpleadoService(empleadoRepo, new CargoRepository());
            articuloService = new ArticuloService(articuloRepo, proveedorRepo);
            agendaService = new AgendaService(agendaRepo, clienteRepo, vehiculoRepo, empleadoRepo, articuloRepo);
            mantenimientoService = new NotificacionMantenimientoService(notificacionRepo, vehiculoRepo, articuloRepo);
            ConfigurarInteraccion();
            Load += (s, e) => CargarTodo();
        }

        private void ConfigurarInteraccion()
        {
            dtpFecha.Value = DateTime.Now.AddHours(1);
            cmbEstado.SelectedIndex = 0;
            cmbFiltroEstado.SelectedIndex = 0;

            btnGuardar.Click += Guardar_Click;
            btnNuevo.Click += (s, e) => LimpiarFormulario();
            btnEstado.Click += CambiarEstado_Click;
            btnCancelar.Click += (s, e) => CambiarEstado("CANCELADA");
            btnMantenimiento.Click += (s, e) =>
            {
                using (var formulario = new FrmMantenimiento())
                {
                    formulario.ShowDialog(this);
                }
            };
            dtpFiltroFecha.ValueChanged += (s, e) => AplicarFiltros();
            cmbFiltroEstado.SelectedIndexChanged += (s, e) => AplicarFiltros();
            grid.CellClick += (s, e) => Grid_SelectionChanged(s, EventArgs.Empty);
            EstiloUI.ConectarTabla(grid);
        }

        private void CargarTodo()
        {
            try
            {
                vehiculos = new VehiculoRepository().Consultar();
                empleados = empleadoService.Consultar().Where(e => e.Activo).ToList();
                servicios = articuloService.Consultar().OfType<Servicio>().ToList();
                cmbVehiculo.DataSource = vehiculos.Select(v => new OpcionVehiculo(v)).ToList();
                cmbVehiculo.DisplayMember = "Texto"; cmbVehiculo.ValueMember = "Placa";
                cmbEmpleado.DisplayMember = "Texto"; cmbEmpleado.ValueMember = "Cedula";
                cmbEmpleado.DataSource = empleados.Select(e => new OpcionEmpleado(e)).ToList();
                cmbEmpleado.SelectedIndex = empleados.Count > 0 ? 0 : -1;
                cmbServicio.DataSource = servicios.Select(s => new OpcionServicio(s)).ToList();
                cmbServicio.DisplayMember = "Texto"; cmbServicio.ValueMember = "Id";
                agendas = agendaService.Consultar();
                AplicarFiltros();
            }
            catch (Exception ex) { MessageBox.Show(this, "No se pudo cargar la agenda. " + ex.Message, "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void AplicarFiltros()
        {
            if (grid == null) return;
            IEnumerable<Agenda> lista = agendas;
            if (dtpFiltroFecha != null && dtpFiltroFecha.Checked) lista = lista.Where(a => a.Fecha.Date == dtpFiltroFecha.Value.Date);
            if (cmbFiltroEstado != null && cmbFiltroEstado.SelectedIndex > 0) lista = lista.Where(a => string.Equals(a.Estado, cmbFiltroEstado.Text, StringComparison.OrdinalIgnoreCase));
            grid.DataSource = lista.Select(AgendaFila.Crear).ToList();
            LimpiarFormulario();
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            bool valido = !validacion.ValidarCampoVacioEspecificoCombo(cmbVehiculo, errores) &
                !validacion.ValidarCampoVacioEspecificoCombo(cmbEmpleado, errores) &
                !validacion.ValidarCampoVacioEspecificoCombo(cmbServicio, errores) &
                !validacion.ValidarCampoVacioEspecificoCombo(cmbEstado, errores) &
                !validacion.ValidarCampoVacioEspecificoDataPiker(dtpFecha, errores);
            if (!valido) { MessageBox.Show(this, "Completa los datos requeridos para la cita.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            int servicioId = cmbServicio.SelectedValue == null ? 0 : Convert.ToInt32(cmbServicio.SelectedValue);
            string mensaje = editando
                ? agendaService.Modificar(agendaSeleccionada, dtpFecha.Value, txtComentario.Text, cmbEstado.Text, cmbVehiculo.SelectedValue.ToString(), cmbEmpleado.SelectedValue.ToString(), servicioId)
                : agendaService.Guardar(dtpFecha.Value, txtComentario.Text, cmbEstado.Text, cmbVehiculo.SelectedValue.ToString(), cmbEmpleado.SelectedValue.ToString(), servicioId);
            bool correcto = mensaje.StartsWith("Se registró la cita", StringComparison.OrdinalIgnoreCase) || mensaje.StartsWith("La cita se modificó", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, mensaje, correcto ? "Agenda" : "No se pudo guardar", MessageBoxButtons.OK, correcto ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (correcto) { CargarTodo(); LimpiarFormulario(); }
        }

        private void CambiarEstado_Click(object sender, EventArgs e) { CambiarEstado(cmbEstado.Text); }

        private void CambiarEstado(string estado)
        {
            if (agendaSeleccionada <= 0) { MessageBox.Show(this, "Selecciona una cita primero.", "Agenda", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            string mensaje = agendaService.CambiarEstado(agendaSeleccionada, estado);
            bool correcto = mensaje.StartsWith("El estado", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, mensaje, "Agenda", MessageBoxButtons.OK, correcto ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (correcto) { CargarTodo(); LimpiarFormulario(); }
        }

        private void Grid_SelectionChanged(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0) return;
            Agenda agenda = (grid.SelectedRows[0].DataBoundItem as AgendaFila)?.Original;
            if (agenda == null) return;
            editando = true; agendaSeleccionada = agenda.AgendaID;
            dtpFecha.Value = agenda.Fecha < dtpFecha.MinDate ? dtpFecha.MinDate : agenda.Fecha > dtpFecha.MaxDate ? dtpFecha.MaxDate : agenda.Fecha;
            txtComentario.Text = agenda.Comentario;
            cmbEstado.SelectedItem = agenda.Estado;
            cmbVehiculo.SelectedValue = agenda.Vehiculo.Placa;
            cmbEmpleado.SelectedValue = agenda.Empleado.Cedula;
            if (agenda.ArticuloServicio != null) cmbServicio.SelectedValue = agenda.ArticuloServicio.ArticuloId;
            btnGuardar.Text = "Actualizar cita";
        }

        private void LimpiarFormulario()
        {
            editando = false; agendaSeleccionada = 0;
            dtpFecha.Value = DateTime.Now.AddHours(1); txtComentario.Clear();
            cmbEstado.SelectedItem = "PENDIENTE";
            btnGuardar.Text = "Guardar cita";
            grid.ClearSelection();
            foreach (Control control in new Control[] { cmbVehiculo, cmbEmpleado, cmbServicio, cmbEstado }) errores.SetError(control, string.Empty);
        }

        private sealed class OpcionVehiculo
        {
            public OpcionVehiculo(Vehiculo v) { Placa = v.Placa; Texto = v.Placa + " — " + (v.Cliente == null ? "Sin propietario" : (v.Cliente.Nombre + " " + v.Cliente.Apellido).Trim()); }
            public string Placa { get; private set; } public string Texto { get; private set; }
        }
        private sealed class OpcionEmpleado
        {
            public OpcionEmpleado(Empleado e) { Cedula = e.Cedula; Texto = (e.Nombre + " " + e.Apellido).Trim() + " — " + (e.Cargo == null ? "Sin cargo" : e.Cargo.NombreCargo); }
            public string Cedula { get; private set; } public string Texto { get; private set; }
        }
        private sealed class OpcionServicio
        {
            public OpcionServicio(Servicio s) { Id = s.ArticuloId; Texto = s.Nombre; }
            public int Id { get; private set; } public string Texto { get; private set; }
        }
        private sealed class AgendaFila
        {
            public int Id { get; set; } public string Fecha { get; set; } public string Placa { get; set; } public string Cliente { get; set; }
            public string Empleado { get; set; } public string Cargo { get; set; } public string Servicio { get; set; } public string Estado { get; set; }
            public string Comentario { get; set; } public Agenda Original { get; set; }
            public static AgendaFila Crear(Agenda a) { return new AgendaFila { Id = a.AgendaID, Fecha = a.Fecha.ToString("g", CultureInfo.CurrentCulture), Placa = a.Vehiculo.Placa, Cliente = a.Vehiculo.Cliente == null ? "" : (a.Vehiculo.Cliente.Nombre + " " + a.Vehiculo.Cliente.Apellido).Trim(), Empleado = (a.Empleado.Nombre + " " + a.Empleado.Apellido).Trim(), Cargo = a.Empleado.Cargo == null ? "" : a.Empleado.Cargo.NombreCargo, Servicio = a.ArticuloServicio == null ? "" : a.ArticuloServicio.Nombre, Estado = a.Estado, Comentario = a.Comentario, Original = a }; }
        }
    }
}
