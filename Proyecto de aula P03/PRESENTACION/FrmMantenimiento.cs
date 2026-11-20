using DATOS;
using ENTIDADES;
using LOGICA;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PRESENTACION
{
    public class FrmMantenimiento : Form
    {
        private readonly NotificacionMantenimientoService servicio;
        private readonly Validacion validacion = new Validacion();
        private readonly ErrorProvider errores;
        private List<NotificacionMantenimiento> notificaciones = new List<NotificacionMantenimiento>();
        private List<Vehiculo> vehiculos = new List<Vehiculo>();
        private List<Articulo> articulos = new List<Articulo>();
        private ComboBox cmbVehiculo, cmbArticulo, cmbEstado;
        private DateTimePicker dtpFecha;
        private TextBox txtComentario, txtFiltroPlaca;
        private DataGridView grid;
        private Button btnGuardar;
        private bool editando;
        private int idSeleccionado;

        public FrmMantenimiento()
        {
            var vehiculoRepo = new VehiculoRepository();
            var articuloRepo = new ArticuloRepository();
            servicio = new NotificacionMantenimientoService(new NotificacionMantenimientoRepository(), vehiculoRepo, articuloRepo);
            errores = new ErrorProvider { ContainerControl = this };
            ConstruirInterfaz();
            EstiloUI.Aplicar(this);
            Load += (s, e) => CargarDatos();
        }

        private void ConstruirInterfaz()
        {
            Text = "Notificaciones de mantenimiento";
            ClientSize = new Size(1050, 680);
            MinimumSize = new Size(850, 560);
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 9F);
            var titulo = new Label { Text = "NOTIFICACIONES DE MANTENIMIENTO", Dock = DockStyle.Top, Height = 44, Padding = new Padding(18, 9, 0, 0), Font = new Font("Segoe UI", 15F, FontStyle.Bold) };
            Controls.Add(titulo);

            var campos = new TableLayoutPanel { Dock = DockStyle.Top, Height = 142, Padding = new Padding(16, 4, 16, 4), ColumnCount = 4, RowCount = 3, BackColor = Color.White };
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 108)); campos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 108)); campos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            for (int i = 0; i < 3; i++) campos.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            cmbVehiculo = CrearCombo(); cmbArticulo = CrearCombo(); cmbEstado = CrearCombo();
            cmbEstado.Items.AddRange(new object[] { "PENDIENTE", "ENVIADA", "COMPLETADA", "CANCELADA" }); cmbEstado.SelectedIndex = 0;
            dtpFecha = new DateTimePicker { Dock = DockStyle.Fill, Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy HH:mm", ShowUpDown = true, Value = DateTime.Now };
            txtComentario = new TextBox { Dock = DockStyle.Fill, MaxLength = 200 };
            AgregarCampo(campos, 0, "Vehículo", cmbVehiculo, 0); AgregarCampo(campos, 0, "Artículo / servicio", cmbArticulo, 2);
            AgregarCampo(campos, 1, "Programada", dtpFecha, 0); AgregarCampo(campos, 1, "Estado", cmbEstado, 2);
            AgregarCampo(campos, 2, "Observación", txtComentario, 0); campos.SetColumnSpan(txtComentario, 3);
            Controls.Add(campos);

            var acciones = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 42, Padding = new Padding(12, 3, 0, 3), BackColor = Color.White };
            btnGuardar = CrearBoton("Guardar");
            var btnNuevo = CrearBoton("Nuevo"); var btnEliminar = CrearBoton("Eliminar"); var btnEstado = CrearBoton("Actualizar estado");
            var btnEnviar = CrearBoton("Enviar recordatorio");
            btnGuardar.Click += Guardar_Click; btnNuevo.Click += (s, e) => LimpiarFormulario(); btnEliminar.Click += Eliminar_Click; btnEstado.Click += ActualizarEstado_Click;
            btnEnviar.Click += EnviarRecordatorio_Click;
            acciones.Controls.AddRange(new Control[] { btnGuardar, btnNuevo, btnEliminar, btnEstado, btnEnviar });
            Controls.Add(acciones);

            var filtro = new Panel { Dock = DockStyle.Top, Height = 42, Padding = new Padding(16, 5, 16, 3) };
            filtro.Controls.Add(new Label { Text = "Filtrar por placa:", AutoSize = true, Location = new Point(16, 10) });
            txtFiltroPlaca = new TextBox { Location = new Point(130, 5), Width = 260, CharacterCasing = CharacterCasing.Upper };
            txtFiltroPlaca.TextChanged += (s, e) => AplicarFiltro(); filtro.Controls.Add(txtFiltroPlaca);
            Controls.Add(filtro);

            grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AutoGenerateColumns = false, MultiSelect = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, RowHeadersVisible = false, BackgroundColor = Color.White, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            grid.Columns.Add(Columna("ID", "Id")); grid.Columns.Add(Columna("Fecha programada", "Fecha")); grid.Columns.Add(Columna("Placa", "Placa"));
            grid.Columns.Add(Columna("Artículo / servicio", "Articulo")); grid.Columns.Add(Columna("Estado", "Estado")); grid.Columns.Add(Columna("Observación", "Comentario"));
            grid.CellClick += (s, e) => Grid_SelectionChanged(s, EventArgs.Empty); Controls.Add(grid);
            Controls.SetChildIndex(titulo, 0); Controls.SetChildIndex(campos, 1); Controls.SetChildIndex(acciones, 2); Controls.SetChildIndex(filtro, 3); Controls.SetChildIndex(grid, 4);
        }

        private void CargarDatos()
        {
            try
            {
                vehiculos = new VehiculoRepository().Consultar();
                articulos = new ArticuloRepository().Consultar();
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
            if (placa.Length > 0) filas = filas.Where(n => n.Vehiculo.Placa.IndexOf(placa, StringComparison.OrdinalIgnoreCase) >= 0);
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
            cmbEstado.SelectedItem = n.Estado; cmbVehiculo.SelectedValue = n.Vehiculo.Placa; cmbArticulo.SelectedValue = n.Articulo.ArticuloId; btnGuardar.Text = "Actualizar";
        }

        private void LimpiarFormulario()
        {
            editando = false; idSeleccionado = 0; dtpFecha.Value = DateTime.Now; txtComentario.Clear(); cmbEstado.SelectedItem = "PENDIENTE"; btnGuardar.Text = "Guardar"; grid.ClearSelection();
            foreach (Control c in new Control[] { cmbVehiculo, cmbArticulo, cmbEstado }) errores.SetError(c, string.Empty);
        }

        private static ComboBox CrearCombo() { return new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList }; }
        private static void AgregarCampo(TableLayoutPanel p, int r, string t, Control c, int col)
        {
            p.Controls.Add(new Label { Text = t, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, col, r); p.Controls.Add(c, col + 1, r);
        }
        private static Button CrearBoton(string t) { return new Button { Text = t, Width = 142, Height = 32, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(45, 95, 145), ForeColor = Color.White, Margin = new Padding(4) }; }
        private static DataGridViewTextBoxColumn Columna(string h, string p) { return new DataGridViewTextBoxColumn { HeaderText = h, DataPropertyName = p, Name = p, SortMode = DataGridViewColumnSortMode.NotSortable }; }

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
