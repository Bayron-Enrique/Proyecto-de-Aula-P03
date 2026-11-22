using DATOS;
using ENTIDADES;
using LOGICA;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PRESENTACION
{
    public partial class FrmMantenimiento : Form
    {
        private readonly NotificacionMantenimientoService servicio;
        private readonly VehiculoService vehiculoService;
        private readonly ArticuloService articuloService;
        private readonly Validacion validacion = new Validacion();
        private List<NotificacionMantenimiento> notificaciones = new List<NotificacionMantenimiento>();
        private List<Vehiculo> vehiculos = new List<Vehiculo>();
        private List<Articulo> articulos = new List<Articulo>();
        private bool editando;
        private int idSeleccionado;

        public FrmMantenimiento()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            var vehiculoRepo = new VehiculoRepository();
            var articuloRepo = new ArticuloRepository();
            vehiculoService = new VehiculoService(vehiculoRepo, new ClienteRepository());
            articuloService = new ArticuloService(articuloRepo, new ProveedorRepository());
            servicio = new NotificacionMantenimientoService(new NotificacionMantenimientoRepository(), vehiculoRepo, articuloRepo);
            ConfigurarInteraccion();
            Load += (s, e) => CargarDatos();
        }

        private void ConfigurarInteraccion()
        {
            cmbEstado.SelectedIndex = 0;
            dtpFecha.Value = DateTime.Now;

            btnGuardar.Click += Guardar_Click;
            btnNuevo.Click += (s, e) => LimpiarFormulario();
            btnEliminar.Click += Eliminar_Click;
            btnEstado.Click += ActualizarEstado_Click;
            btnEnviar.Click += EnviarRecordatorio_Click;
            txtFiltroPlaca.TextChanged += (s, e) => AplicarFiltro();
            grid.CellClick += (s, e) => Grid_SelectionChanged(s, EventArgs.Empty);
            EstiloUI.ConectarTabla(grid);
        }

        private void CargarDatos()
        {
            try
            {
                vehiculos = vehiculoService.Consultar();
                articulos = articuloService.Consultar();
                cmbVehiculo.DataSource = vehiculos.Select(v => new VehiculoOpcion(v)).ToList(); cmbVehiculo.DisplayMember = "Texto"; cmbVehiculo.ValueMember = "Placa";
                cmbArticulo.DataSource = articulos.Select(a => new ArticuloOpcion(a)).ToList(); cmbArticulo.DisplayMember = "Texto"; cmbArticulo.ValueMember = "Id";
                notificaciones = servicio.Consultar(); AplicarFiltro();
            }
            catch (Exception ex) { MessageBox.Show(this, "No se pudieron cargar los datos de mantenimiento. " + ex.Message, "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void AplicarFiltro()
        {
            if (grid == null) return;
            string placa = txtFiltroPlaca == null ? string.Empty : txtFiltroPlaca.Text.Trim();
            IEnumerable<NotificacionMantenimiento> filas = notificaciones;
            if (placa.Length > 0) filas = filas.Where(n => n.Vehiculo?.Placa?.IndexOf(placa, StringComparison.OrdinalIgnoreCase) >= 0);
            grid.DataSource = filas.Select(NotificacionFila.Crear).ToList();
            LimpiarFormulario();
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            bool valido = !validacion.ValidarCampoVacioEspecificoCombo(cmbVehiculo, errores) &
                !validacion.ValidarCampoVacioEspecificoCombo(cmbArticulo, errores) & !validacion.ValidarCampoVacioEspecificoCombo(cmbEstado, errores) &
                !validacion.ValidarCampoVacioEspecificoDataPiker(dtpFecha, errores);
            if (!valido) { MessageBox.Show(this, "Selecciona vehículo, artículo, fecha y estado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            string mensaje = editando
                ? servicio.Modificar(idSeleccionado, dtpFecha.Value, txtComentario.Text, cmbEstado.Text, cmbVehiculo.SelectedValue.ToString(), Convert.ToInt32(cmbArticulo.SelectedValue))
                : servicio.Guardar(dtpFecha.Value, txtComentario.Text, cmbEstado.Text, cmbVehiculo.SelectedValue.ToString(), Convert.ToInt32(cmbArticulo.SelectedValue));
            bool correcto = mensaje.StartsWith("Se registró la notificación", StringComparison.OrdinalIgnoreCase) || mensaje.StartsWith("La notificación se modificó", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, mensaje, correcto ? "Mantenimiento" : "No se pudo guardar", MessageBoxButtons.OK, correcto ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (correcto) { CargarDatos(); LimpiarFormulario(); }
        }

        private void ActualizarEstado_Click(object sender, EventArgs e)
        {
            if (idSeleccionado <= 0) { MessageBox.Show(this, "Selecciona una notificación.", "Mantenimiento", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            string mensaje = servicio.CambiarEstado(idSeleccionado, cmbEstado.Text);
            bool correcto = mensaje.StartsWith("El estado", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, mensaje, "Mantenimiento", MessageBoxButtons.OK, correcto ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (correcto) { CargarDatos(); LimpiarFormulario(); }
        }

        private void EnviarRecordatorio_Click(object sender, EventArgs e)
        {
            if (idSeleccionado <= 0) { MessageBox.Show(this, "Selecciona una notificación.", "Mantenimiento", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            string mensaje = servicio.EnviarRecordatorioCorreo(idSeleccionado);
            bool correcto = mensaje.StartsWith("Recordatorio enviado", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, mensaje, "Mantenimiento", MessageBoxButtons.OK, correcto ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (correcto) { CargarDatos(); LimpiarFormulario(); }
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado <= 0) return;
            if (MessageBox.Show(this, "¿Eliminar la notificación seleccionada?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            string mensaje = servicio.Eliminar(idSeleccionado);
            bool correcto = mensaje.StartsWith("La notificación se eliminó", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, mensaje, "Mantenimiento", MessageBoxButtons.OK, correcto ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (correcto) { CargarDatos(); LimpiarFormulario(); }
        }

        private void Grid_SelectionChanged(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0) return;
            NotificacionMantenimiento n = (grid.SelectedRows[0].DataBoundItem as NotificacionFila)?.Original;
            if (n == null) return;
            editando = true; idSeleccionado = n.NotificacionID; dtpFecha.Value = n.Fecha < dtpFecha.MinDate ? dtpFecha.MinDate : n.Fecha > dtpFecha.MaxDate ? dtpFecha.MaxDate : n.Fecha; txtComentario.Text = n.Comentario;
            cmbEstado.SelectedItem = n.Estado;
            if (n.Vehiculo != null) cmbVehiculo.SelectedValue = n.Vehiculo.Placa;
            else cmbVehiculo.SelectedIndex = -1;
            if (n.Articulo != null) cmbArticulo.SelectedValue = n.Articulo.ArticuloId;
            else cmbArticulo.SelectedIndex = -1;
            btnGuardar.Text = "Actualizar";
        }

        private void LimpiarFormulario()
        {
            editando = false; idSeleccionado = 0; dtpFecha.Value = DateTime.Now; txtComentario.Clear(); cmbEstado.SelectedItem = "PENDIENTE"; btnGuardar.Text = "Guardar"; grid.ClearSelection();
            foreach (Control c in new Control[] { cmbVehiculo, cmbArticulo, cmbEstado }) errores.SetError(c, string.Empty);
        }

        private sealed class VehiculoOpcion
        {
            public VehiculoOpcion(Vehiculo v) { Placa = v.Placa; Texto = v.Placa + " — " + (v.Cliente == null ? "" : (v.Cliente.Nombre + " " + v.Cliente.Apellido).Trim()); }
            public string Placa { get; private set; } public string Texto { get; private set; }
        }
        private sealed class ArticuloOpcion
        {
            public ArticuloOpcion(Articulo a) { Id = a.ArticuloId; Texto = a.Nombre + (a is Servicio ? " (Servicio)" : " (Repuesto)"); }
            public int Id { get; private set; } public string Texto { get; private set; }
        }
        private sealed class NotificacionFila
        {
            public int Id { get; set; } public string Fecha { get; set; } public string Placa { get; set; } public string Articulo { get; set; }
            public string Estado { get; set; } public string Comentario { get; set; } public NotificacionMantenimiento Original { get; set; }
            public static NotificacionFila Crear(NotificacionMantenimiento n) { return new NotificacionFila { Id = n.NotificacionID, Fecha = n.Fecha.ToString("g"), Placa = n.Vehiculo.Placa, Articulo = n.Articulo.Nombre, Estado = n.Estado, Comentario = n.Comentario, Original = n }; }
        }
    }
}
