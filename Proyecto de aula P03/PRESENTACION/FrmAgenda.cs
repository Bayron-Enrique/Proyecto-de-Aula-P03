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
        private readonly VehiculoService vehiculoService;
        private readonly EmpleadoService empleadoService;
        private readonly ArticuloService articuloService;
        private readonly Validacion validacion = new Validacion();
        private List<Agenda> agendas = new List<Agenda>();
        private List<Vehiculo> vehiculos = new List<Vehiculo>();
        private List<Empleado> empleados = new List<Empleado>();
        private List<Servicio> servicios = new List<Servicio>();
        private bool editando;
        private int agendaSeleccionada;
        private Timer temporizadorSugerencias;
        private ComboBox campoSugerencias;
        private List<object> elementosSugeridos = new List<object>();
        private Func<IEnumerable<object>> obtenerElementosSugeridos;
        private Func<object, string> obtenerTituloSugerido, obtenerSubtituloSugerido, obtenerBusquedaSugerida, obtenerTextoSeleccionado;
        private Action<object> seleccionarElementoSugerido;
        private string textoEscritoSugerido;
        private bool seleccionandoSugerencia;

        public FrmAgenda() : this(null)
        {
        }

        internal FrmAgenda(int? citaInicial)
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            pnlSugerencias.Visible = false;

            var clienteRepo = new ClienteRepository();
            var vehiculoRepo = new VehiculoRepository();
            var empleadoRepo = new EmpleadoRepository();
            var articuloRepo = new ArticuloRepository();
            var agendaRepo = new AgendaRepository();
            var proveedorRepo = new ProveedorRepository();
            vehiculoService = new VehiculoService(vehiculoRepo, clienteRepo);
            empleadoService = new EmpleadoService(empleadoRepo, new CargoRepository());
            articuloService = new ArticuloService(articuloRepo, proveedorRepo);
            agendaService = new AgendaService(agendaRepo, clienteRepo, vehiculoRepo, empleadoRepo, articuloRepo);
            ConfigurarInteraccion();
            Load += (s, e) =>
            {
                CargarTodo();
                if (citaInicial.HasValue) SeleccionarCita(citaInicial.Value);
            };
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

            cmbVehiculo.DropDownStyle = ComboBoxStyle.DropDown;
            cmbServicio.DropDownStyle = ComboBoxStyle.DropDown;
            temporizadorSugerencias = new Timer { Interval = 150 };
            temporizadorSugerencias.Tick += (s, e) =>
            {
                temporizadorSugerencias.Stop();
                MostrarSugerencias();
            };
            ConfigurarSugerenciasCombo(cmbVehiculo,
                () => vehiculos.Cast<object>(),
                elemento => ((Vehiculo)elemento).Placa,
                elemento =>
                {
                    Vehiculo v = (Vehiculo)elemento;
                    return (v.Marca + " " + v.Linea + " • " + v.Color).Trim();
                },
                elemento =>
                {
                    Vehiculo v = (Vehiculo)elemento;
                    return v.Placa + " " + v.Marca + " " + v.Linea + " " + v.Color + " " +
                           (v.Cliente == null ? string.Empty : v.Cliente.Nombre + " " + v.Cliente.Apellido + " " + v.Cliente.Cedula);
                },
                elemento => new OpcionVehiculo((Vehiculo)elemento).Texto,
                elemento => cmbVehiculo.SelectedValue = ((Vehiculo)elemento).Placa);
            ConfigurarSugerenciasCombo(cmbServicio,
                () => servicios.Cast<object>(),
                elemento => ((Servicio)elemento).Nombre,
                elemento => "Servicio • " + ((Servicio)elemento).PrecioVenta.ToString("C0", CultureInfo.CurrentCulture),
                elemento => ((Servicio)elemento).Nombre + " " + ((Servicio)elemento).Descripcion + " " + ((Servicio)elemento).PrecioVenta,
                elemento => ((Servicio)elemento).Nombre,
                elemento => cmbServicio.SelectedValue = ((Servicio)elemento).ArticuloId);
            lstSugerencias.DrawItem += lstSugerencias_DrawItem;
            lstSugerencias.MouseClick += lstSugerencias_MouseClick;
            Deactivate += (s, e) => pnlSugerencias.Visible = false;
            Resize += (s, e) => { if (pnlSugerencias.Visible) PosicionarSugerencias(); };
            FormClosed += (s, e) => { temporizadorSugerencias.Stop(); temporizadorSugerencias.Dispose(); };
            lstSugerencias.Items.Clear();

        }

        private void ConfigurarSugerenciasCombo(ComboBox combo, Func<IEnumerable<object>> proveedor,
            Func<object, string> titulo, Func<object, string> subtitulo, Func<object, string> busqueda,
            Func<object, string> textoSeleccionado, Action<object> seleccionar)
        {
            combo.TextChanged += (s, e) =>
            {
                if (seleccionandoSugerencia || !combo.Focused) return;
                campoSugerencias = combo;
                obtenerElementosSugeridos = proveedor;
                obtenerTituloSugerido = titulo;
                obtenerSubtituloSugerido = subtitulo;
                obtenerBusquedaSugerida = busqueda;
                obtenerTextoSeleccionado = textoSeleccionado;
                seleccionarElementoSugerido = seleccionar;
                textoEscritoSugerido = combo.Text;
                temporizadorSugerencias.Stop();
                temporizadorSugerencias.Start();
            };
            combo.KeyDown += ComboSugerencias_KeyDown;
            combo.LostFocus += (s, e) =>
            {
                if (!pnlSugerencias.ClientRectangle.Contains(pnlSugerencias.PointToClient(Cursor.Position)))
                    pnlSugerencias.Visible = false;
            };
        }

        private void MostrarSugerencias()
        {
            if (campoSugerencias == null || !campoSugerencias.Focused || string.IsNullOrWhiteSpace(campoSugerencias.Text))
            {
                pnlSugerencias.Visible = false;
                return;
            }

            List<string> terminos = Normalizar(campoSugerencias.Text).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            elementosSugeridos = (obtenerElementosSugeridos == null ? Enumerable.Empty<object>() : obtenerElementosSugeridos())
                .Where(elemento => terminos.All(termino => Normalizar(obtenerTituloSugerido(elemento) + " " +
                    obtenerSubtituloSugerido(elemento) + " " + obtenerBusquedaSugerida(elemento)).Contains(termino)))
                .Take(100).ToList();

            lstSugerencias.BeginUpdate();
            lstSugerencias.Items.Clear();
            for (int i = 0; i < elementosSugeridos.Count; i++) lstSugerencias.Items.Add(obtenerTituloSugerido(elementosSugeridos[i]));
            lstSugerencias.EndUpdate();
            bool sinResultados = elementosSugeridos.Count == 0;
            lstSugerencias.Visible = !sinResultados;
            lblSinSugerencias.Visible = sinResultados;
            PosicionarSugerencias();
            pnlSugerencias.Visible = true;
            pnlSugerencias.BringToFront();
        }

        private void PosicionarSugerencias()
        {
            if (campoSugerencias == null) return;
            Rectangle campo = campoSugerencias.RectangleToScreen(campoSugerencias.ClientRectangle);
            Rectangle area = Screen.FromRectangle(campo).WorkingArea;
            int ancho = Math.Min(Math.Max(campo.Width, 260), area.Width);
            int filasVisibles = Math.Min(7, Math.Max(1, elementosSugeridos.Count));
            int alto = elementosSugeridos.Count == 0 ? 58 : filasVisibles * 56 + 2;
            bool arriba = area.Bottom - campo.Bottom < alto && campo.Top - area.Top > area.Bottom - campo.Bottom;
            int x = Math.Max(area.Left, Math.Min(campo.Left, area.Right - ancho));
            int y = arriba ? campo.Top - alto : campo.Bottom;
            if (y + alto > area.Bottom) y = area.Bottom - alto;
            if (y < area.Top) y = area.Top;
            pnlSugerencias.Bounds = new Rectangle(PointToClient(new Point(x, y)), new Size(ancho, alto));
            lstSugerencias.Dock = DockStyle.Fill;
            lblSinSugerencias.Dock = DockStyle.Fill;
        }

        private void ComboSugerencias_KeyDown(object sender, KeyEventArgs e)
        {
            if (!pnlSugerencias.Visible) return;
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                if (lstSugerencias.Items.Count > 0)
                {
                    int indice = lstSugerencias.SelectedIndex + (e.KeyCode == Keys.Down ? 1 : -1);
                    if (indice < 0) indice = lstSugerencias.Items.Count - 1;
                    if (indice >= lstSugerencias.Items.Count) indice = 0;
                    lstSugerencias.SelectedIndex = indice;
                }
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                pnlSugerencias.Visible = false;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Enter && lstSugerencias.SelectedIndex >= 0)
            {
                SeleccionarSugerencia(lstSugerencias.SelectedIndex);
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Tab && lstSugerencias.SelectedIndex >= 0)
            {
                SeleccionarSugerencia(lstSugerencias.SelectedIndex);
                e.SuppressKeyPress = true;
                SelectNextControl((Control)sender, !e.Shift, true, true, true);
            }
        }

        private void SeleccionarSugerencia(int indice)
        {
            if (indice < 0 || indice >= elementosSugeridos.Count) return;
            object seleccionado = elementosSugeridos[indice];
            seleccionandoSugerencia = true;
            campoSugerencias.Text = obtenerTextoSeleccionado(seleccionado);
            campoSugerencias.SelectionStart = campoSugerencias.Text.Length;
            seleccionarElementoSugerido(seleccionado);
            pnlSugerencias.Visible = false;
            campoSugerencias.Focus();
            seleccionandoSugerencia = false;
        }

        private void lstSugerencias_MouseClick(object sender, MouseEventArgs e)
        {
            int indice = lstSugerencias.IndexFromPoint(e.Location);
            if (indice >= 0) SeleccionarSugerencia(indice);
        }

        private void lstSugerencias_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= elementosSugeridos.Count) return;
            object elemento = elementosSugeridos[e.Index];
            string titulo = obtenerTituloSugerido(elemento) ?? string.Empty;
            string subtitulo = obtenerSubtituloSugerido(elemento) ?? string.Empty;
            bool seleccionado = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            using (Brush fondo = new SolidBrush(seleccionado ? Color.FromArgb(239, 246, 255) : Color.White)) e.Graphics.FillRectangle(fondo, e.Bounds);
            if (seleccionado) using (Brush acento = new SolidBrush(Color.FromArgb(37, 99, 235))) e.Graphics.FillRectangle(acento, e.Bounds.Left, e.Bounds.Top, 3, e.Bounds.Height);
            bool coincide = Normalizar(titulo).Contains(Normalizar(textoEscritoSugerido));
            using (Font fuente = new Font("Segoe UI", 9.5f, coincide ? FontStyle.Bold : FontStyle.Regular))
            using (Brush tinta = new SolidBrush(coincide ? Color.FromArgb(37, 99, 235) : Color.FromArgb(15, 23, 42)))
            using (Font fuenteSecundaria = new Font("Segoe UI", 8.5f))
            using (Brush gris = new SolidBrush(Color.FromArgb(100, 116, 139)))
            {
                e.Graphics.DrawString(titulo, fuente, tinta, e.Bounds.Left + 12, e.Bounds.Top + 7);
                e.Graphics.DrawString(subtitulo, fuenteSecundaria, gris, e.Bounds.Left + 12, e.Bounds.Top + 31);
            }
            e.DrawFocusRectangle();
        }

        private static string Normalizar(string texto)
        {
            string descompuesto = (texto ?? string.Empty).Normalize(NormalizationForm.FormD);
            StringBuilder resultado = new StringBuilder(descompuesto.Length);
            foreach (char caracter in descompuesto)
                if (CharUnicodeInfo.GetUnicodeCategory(caracter) != UnicodeCategory.NonSpacingMark)
                    resultado.Append(char.ToLowerInvariant(caracter));
            return resultado.ToString().Normalize(NormalizationForm.FormC);
        }

        private void CargarTodo()
        {
            try
            {
                vehiculos = vehiculoService.Consultar();
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
            MostrarCita(agenda);
        }

        private void SeleccionarCita(int agendaId)
        {
            foreach (DataGridViewRow fila in grid.Rows)
            {
                if (!(fila.DataBoundItem is AgendaFila cita) || cita.Id != agendaId) continue;
                grid.CurrentCell = fila.Cells.Cast<DataGridViewCell>().First(celda => celda.Visible);
                fila.Selected = true;
                MostrarCita(cita.Original);
                return;
            }
            MessageBox.Show(this, "La cita ya no está disponible.", "Agenda", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MostrarCita(Agenda agenda)
        {
            editando = true; agendaSeleccionada = agenda.AgendaID;
            dtpFecha.Value = agenda.Fecha < dtpFecha.MinDate ? dtpFecha.MinDate : agenda.Fecha > dtpFecha.MaxDate ? dtpFecha.MaxDate : agenda.Fecha;
            txtComentario.Text = agenda.Comentario;
            cmbEstado.SelectedItem = agenda.Estado;
            if (agenda.Vehiculo != null) cmbVehiculo.SelectedValue = agenda.Vehiculo.Placa;
            else cmbVehiculo.SelectedIndex = -1;
            if (agenda.Empleado != null) cmbEmpleado.SelectedValue = agenda.Empleado.Cedula;
            else cmbEmpleado.SelectedIndex = -1;
            if (agenda.ArticuloServicio != null) cmbServicio.SelectedValue = agenda.ArticuloServicio.ArticuloId;
            else cmbServicio.SelectedIndex = -1;
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
