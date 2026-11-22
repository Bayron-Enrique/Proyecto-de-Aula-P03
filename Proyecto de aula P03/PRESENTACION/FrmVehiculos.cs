using DATOS;
using ENTIDADES;
using LOGICA;
using System;
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
        private readonly ErrorProvider errores;
        private readonly TabControl tabs = new TabControl();
        private readonly TabPage tabVehiculos = new TabPage("Vehículos");
        private readonly TabPage tabSoat = new TabPage("SOAT");
        private List<Cliente> clientes = new List<Cliente>();
        private List<Vehiculo> vehiculos = new List<Vehiculo>();
        private List<Soat> soats = new List<Soat>();

        private TextBox txtPlaca, txtMarca, txtModelo, txtColor, txtLinea, txtBuscarVehiculo;
        private ComboBox cmbTipoVehiculo, cmbPropietario;
        private DataGridView gridVehiculos;
        private Button btnGuardarVehiculo;
        private bool editandoVehiculo;

        private ComboBox cmbPlacaSoat;
        private TextBox txtNumeroSoat;
        private DateTimePicker dtpFechaInicio, dtpFechaFin;
        private DataGridView gridSoat;
        private Button btnGuardarSoat;
        private bool editandoSoat;
        private int soatIdSeleccionado;

        public FrmVehiculos()
        {
            InitializeComponent();
            var clienteRepository = new ClienteRepository();
            clienteService = new ClienteService(clienteRepository);
            vehiculoService = new VehiculoService(new VehiculoRepository(), clienteRepository);
            soatService = new SoatService(new SoatRepository(), new VehiculoRepository());
            errores = new ErrorProvider(components) { ContainerControl = this };
            ConstruirInterfaz();
            EstiloUI.Aplicar(this);
            Load += (s, e) => CargarTodo();
        }

        private void ConstruirInterfaz()
        {
            Text = "Gestión de vehículos y SOAT";
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 9F);
            tabs.Dock = DockStyle.Fill;
            tabVehiculos.BackColor = BackColor;
            tabSoat.BackColor = BackColor;
            tabs.TabPages.Add(tabVehiculos);
            tabs.TabPages.Add(tabSoat);
            Controls.Add(tabs);
            ConstruirTabVehiculos();
            ConstruirTabSoat();
        }

        private void ConstruirTabVehiculos()
        {
            var encabezado = CrearEncabezado("VEHÍCULOS");
            tabVehiculos.Controls.Add(encabezado);

            var campos = new TableLayoutPanel
            {
                Dock = DockStyle.Top, Height = 128, Padding = new Padding(16, 3, 16, 3),
                BackColor = Color.White, ColumnCount = 4, RowCount = 4
            };
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 108));
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 108));
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            for (int i = 0; i < 4; i++) campos.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));

            txtPlaca = CrearTexto(6);
            txtPlaca.CharacterCasing = CharacterCasing.Upper;
            txtPlaca.TextChanged += (s, e) => { int pos = txtPlaca.SelectionStart; txtPlaca.Text = txtPlaca.Text.ToUpperInvariant(); txtPlaca.SelectionStart = Math.Min(pos, txtPlaca.Text.Length); };
            txtMarca = CrearTexto(50);
            txtModelo = CrearTexto(4);
            txtModelo.KeyPress += SoloDigitos_KeyPress;
            cmbTipoVehiculo = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbTipoVehiculo.Items.AddRange(new object[] { "Automovil", "Motocicleta" });
            txtColor = CrearTexto(50);
            txtLinea = CrearTexto(50);
            cmbPropietario = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };

            AgregarCampo(campos, 0, "Placa", txtPlaca, 0);
            AgregarCampo(campos, 0, "Marca", txtMarca, 2);
            AgregarCampo(campos, 1, "Modelo / año", txtModelo, 0);
            AgregarCampo(campos, 1, "Tipo", cmbTipoVehiculo, 2);
            AgregarCampo(campos, 2, "Color", txtColor, 0);
            AgregarCampo(campos, 2, "Línea", txtLinea, 2);
            campos.Controls.Add(new Label { Text = "Propietario", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 3);
            campos.Controls.Add(cmbPropietario, 1, 3);
            campos.SetColumnSpan(cmbPropietario, 3);
            tabVehiculos.Controls.Add(campos);

            var acciones = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 42, Padding = new Padding(12, 3, 0, 3), BackColor = Color.White };
            btnGuardarVehiculo = CrearBoton("Guardar", Color.FromArgb(29, 111, 180));
            var btnNuevo = CrearBoton("Nuevo", Color.FromArgb(85, 95, 110));
            var btnEliminar = CrearBoton("Eliminar", Color.FromArgb(173, 58, 58));
            btnGuardarVehiculo.Click += GuardarVehiculo_Click;
            btnNuevo.Click += (s, e) => LimpiarVehiculo();
            btnEliminar.Click += EliminarVehiculo_Click;
            acciones.Controls.AddRange(new Control[] { btnGuardarVehiculo, btnNuevo, btnEliminar });
            tabVehiculos.Controls.Add(acciones);

            var busqueda = new Panel { Dock = DockStyle.Top, Height = 40, Padding = new Padding(16, 3, 16, 3) };
            busqueda.Controls.Add(new Label { Text = "Filtrar por placa o cédula:", AutoSize = true, Location = new Point(16, 10) });
            txtBuscarVehiculo = new TextBox { Location = new Point(190, 5), Width = 300, Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right };
            txtBuscarVehiculo.TextChanged += (s, e) => MostrarVehiculos();
            busqueda.Controls.Add(txtBuscarVehiculo);
            EstiloUI.AjustarBusqueda(txtBuscarVehiculo);
            tabVehiculos.Controls.Add(busqueda);

            gridVehiculos = CrearGrid();
            gridVehiculos.Columns.Add(Columna("Placa", "Placa"));
            gridVehiculos.Columns.Add(Columna("Marca", "Marca"));
            gridVehiculos.Columns.Add(Columna("Línea", "Linea"));
            gridVehiculos.Columns.Add(Columna("Modelo / año", "Modelo"));
            gridVehiculos.Columns.Add(Columna("Tipo", "TipoVehiculo"));
            gridVehiculos.Columns.Add(Columna("Color", "Color"));
            gridVehiculos.Columns.Add(Columna("Propietario", "Propietario"));
            gridVehiculos.Columns.Add(Columna("Cédula", "CedulaPropietario"));
            gridVehiculos.CellClick += (s, e) => GridVehiculos_SelectionChanged(s, EventArgs.Empty);
            tabVehiculos.Controls.Add(gridVehiculos);

            tabVehiculos.Controls.SetChildIndex(encabezado, 0);
            tabVehiculos.Controls.SetChildIndex(campos, 1);
            tabVehiculos.Controls.SetChildIndex(acciones, 2);
            tabVehiculos.Controls.SetChildIndex(busqueda, 3);
            tabVehiculos.Controls.SetChildIndex(gridVehiculos, 4);
        }

        private void ConstruirTabSoat()
        {
            var encabezado = CrearEncabezado("PÓLIZAS SOAT");
            tabSoat.Controls.Add(encabezado);
            var campos = new TableLayoutPanel
            {
                Dock = DockStyle.Top, Height = 94, Padding = new Padding(16, 3, 16, 3),
                BackColor = Color.White, ColumnCount = 4, RowCount = 2
            };
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            campos.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            campos.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            cmbPlacaSoat = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            txtNumeroSoat = CrearTexto(20);
            txtNumeroSoat.KeyPress += SoloDigitos_KeyPress;
            dtpFechaInicio = CrearFecha();
            dtpFechaFin = CrearFecha();
            AgregarCampo(campos, 0, "Vehículo", cmbPlacaSoat, 0);
            AgregarCampo(campos, 0, "Número SOAT", txtNumeroSoat, 2);
            AgregarCampo(campos, 1, "Fecha inicio", dtpFechaInicio, 0);
            AgregarCampo(campos, 1, "Fecha fin", dtpFechaFin, 2);
            cmbPlacaSoat.SelectedValueChanged += (s, e) => MostrarSoats();
            tabSoat.Controls.Add(campos);

            var acciones = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 42, Padding = new Padding(12, 3, 0, 3), BackColor = Color.White };
            btnGuardarSoat = CrearBoton("Guardar", Color.FromArgb(29, 111, 180));
            var btnNuevo = CrearBoton("Nuevo", Color.FromArgb(85, 95, 110));
            var btnEliminar = CrearBoton("Eliminar", Color.FromArgb(173, 58, 58));
            btnGuardarSoat.Click += GuardarSoat_Click;
            btnNuevo.Click += (s, e) => LimpiarSoat();
            btnEliminar.Click += EliminarSoat_Click;
            acciones.Controls.AddRange(new Control[] { btnGuardarSoat, btnNuevo, btnEliminar });
            tabSoat.Controls.Add(acciones);

            gridSoat = CrearGrid();
            gridSoat.Columns.Add(Columna("ID", "SoatId"));
            gridSoat.Columns.Add(Columna("Número SOAT", "NumeroSoat"));
            gridSoat.Columns.Add(Columna("Placa", "Placa"));
            gridSoat.Columns.Add(Columna("Marca / línea", "Vehiculo"));
            gridSoat.Columns.Add(Columna("Propietario", "Propietario"));
            gridSoat.Columns.Add(Columna("Fecha inicio", "FechaInicio"));
            gridSoat.Columns.Add(Columna("Fecha fin", "FechaFin"));
            gridSoat.Columns.Add(Columna("Estado", "Estado"));
            gridSoat.CellClick += (s, e) => GridSoat_SelectionChanged(s, EventArgs.Empty);
            tabSoat.Controls.Add(gridSoat);

            tabSoat.Controls.SetChildIndex(encabezado, 0);
            tabSoat.Controls.SetChildIndex(campos, 1);
            tabSoat.Controls.SetChildIndex(acciones, 2);
            tabSoat.Controls.SetChildIndex(gridSoat, 3);
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

        private static Label CrearEncabezado(string texto)
        {
            return new Label
            {
                Text = texto, Dock = DockStyle.Top, Height = 42, Padding = new Padding(18, 8, 0, 0),
                Font = new Font("Segoe UI", 16F, FontStyle.Bold), ForeColor = Color.FromArgb(35, 45, 60)
            };
        }

        private static TextBox CrearTexto(int maximo)
        {
            return new TextBox { Dock = DockStyle.Fill, MaxLength = maximo, Margin = new Padding(4, 6, 10, 4) };
        }

        private static DateTimePicker CrearFecha()
        {
            return new DateTimePicker { Dock = DockStyle.Fill, Format = DateTimePickerFormat.Short, MinDate = new DateTime(2000, 1, 1), MaxDate = DateTime.Today.AddYears(10), Value = DateTime.Today };
        }

        private static void AgregarCampo(TableLayoutPanel panel, int fila, string texto, Control control, int columna)
        {
            panel.Controls.Add(new Label { Text = texto, TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, columna, fila);
            panel.Controls.Add(control, columna + 1, fila);
        }

        private static Button CrearBoton(string texto, Color color)
        {
            return new Button { Text = texto, Width = 104, Height = 32, FlatStyle = FlatStyle.Flat, ForeColor = Color.White, BackColor = color, Margin = new Padding(4), Cursor = Cursors.Hand };
        }

        private static DataGridView CrearGrid()
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                AutoGenerateColumns = false, MultiSelect = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White, BorderStyle = BorderStyle.None, RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
        }

        private static DataGridViewTextBoxColumn Columna(string encabezado, string propiedad)
        {
            return new DataGridViewTextBoxColumn { HeaderText = encabezado, DataPropertyName = propiedad, Name = propiedad, SortMode = DataGridViewColumnSortMode.NotSortable };
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
