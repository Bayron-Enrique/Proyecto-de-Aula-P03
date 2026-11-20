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
    public partial class FrmArticulos : Form
    {
        private readonly ArticuloService articuloService;
        private readonly ProveedorService proveedorService;
        private readonly Validacion validacion = new Validacion();
        private readonly ErrorProvider errores;
        private List<Articulo> articulos = new List<Articulo>();
        private List<Proveedor> proveedores = new List<Proveedor>();

        private TextBox txtNombre, txtPrecio, txtStock, txtDuracion, txtDescripcion, txtBuscar;
        private ComboBox cmbTipo, cmbIva, cmbProveedor, cmbFiltroTipo;
        private Label lblStock, lblDuracion, lblProveedor;
        private DataGridView grid;
        private Button btnGuardar;
        private bool editando;
        private int articuloIdSeleccionado;

        public FrmArticulos()
        {
            InitializeComponent();
            var proveedorRepository = new ProveedorRepository();
            proveedorService = new ProveedorService(proveedorRepository);
            articuloService = new ArticuloService(new ArticuloRepository(), proveedorRepository);
            errores = new ErrorProvider(components) { ContainerControl = this };
            ConstruirInterfaz();
            EstiloUI.Aplicar(this);
            Load += (s, e) => CargarTodo();
        }

        private void ConstruirInterfaz()
        {
            Text = "Inventario de artículos";
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 9F);
            var titulo = new Label
            {
                Text = "INVENTARIO / ARTÍCULOS", Dock = DockStyle.Top, Height = 42, Padding = new Padding(18, 8, 0, 0),
                Font = new Font("Segoe UI", 16F, FontStyle.Bold), ForeColor = Color.FromArgb(35, 45, 60)
            };
            Controls.Add(titulo);

            var campos = new TableLayoutPanel
            {
                Dock = DockStyle.Top, Height = 204, Padding = new Padding(18, 3, 18, 3),
                ColumnCount = 4, RowCount = 5, BackColor = Color.White
            };
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 108));
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 108));
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            for (int i = 0; i < 5; i++) campos.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));

            txtNombre = CrearTexto(100);
            cmbTipo = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbTipo.Items.AddRange(new object[] { "Repuesto", "Servicio" });
            cmbTipo.SelectedIndexChanged += (s, e) => ConfigurarCamposTipo();
            txtPrecio = CrearTexto(20);
            cmbIva = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbIva.Items.AddRange(new object[] { "0", "5", "19" });
            cmbIva.SelectedItem = "19";
            txtStock = CrearTexto(7);
            txtStock.KeyPress += SoloDigitos_KeyPress;
            txtDuracion = CrearTexto(5);
            txtDuracion.KeyPress += SoloDigitos_KeyPress;
            cmbProveedor = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            txtDescripcion = CrearTexto(200);
            txtDescripcion.Multiline = true;
            txtDescripcion.ScrollBars = ScrollBars.Vertical;
            lblStock = new Label { Text = "Stock", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
            lblDuracion = new Label { Text = "Duración (min)", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
            lblProveedor = new Label { Text = "Proveedor", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };

            AgregarCampo(campos, 0, "Nombre", txtNombre, 0);
            AgregarCampo(campos, 0, "Tipo", cmbTipo, 2);
            AgregarCampo(campos, 1, "Precio", txtPrecio, 0);
            AgregarCampo(campos, 1, "IVA (%)", cmbIva, 2);
            campos.Controls.Add(lblStock, 0, 2);
            campos.Controls.Add(txtStock, 1, 2);
            campos.Controls.Add(lblDuracion, 2, 2);
            campos.Controls.Add(txtDuracion, 3, 2);
            campos.Controls.Add(lblProveedor, 0, 3);
            campos.Controls.Add(cmbProveedor, 1, 3);
            campos.SetColumnSpan(cmbProveedor, 3);
            campos.Controls.Add(new Label { Text = "Descripción", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 0, 4);
            campos.Controls.Add(txtDescripcion, 1, 4);
            campos.SetColumnSpan(txtDescripcion, 3);
            Controls.Add(campos);

            var acciones = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 42, Padding = new Padding(12, 3, 0, 3), BackColor = Color.White };
            btnGuardar = CrearBoton("Guardar", Color.FromArgb(29, 111, 180));
            var btnNuevo = CrearBoton("Nuevo", Color.FromArgb(85, 95, 110));
            var btnEliminar = CrearBoton("Eliminar", Color.FromArgb(173, 58, 58));
            var btnProveedores = CrearBoton("Gestionar proveedores", Color.FromArgb(67, 100, 78));
            btnProveedores.Width = 170;
            btnGuardar.Click += Guardar_Click;
            btnNuevo.Click += (s, e) => LimpiarFormulario();
            btnEliminar.Click += Eliminar_Click;
            btnProveedores.Click += GestionarProveedores_Click;
            acciones.Controls.AddRange(new Control[] { btnGuardar, btnNuevo, btnEliminar, btnProveedores });
            Controls.Add(acciones);

            var filtros = new Panel { Dock = DockStyle.Top, Height = 42, Padding = new Padding(18, 4, 18, 4) };
            filtros.Controls.Add(new Label { Text = "Buscar nombre:", AutoSize = true, Location = new Point(18, 11) });
            txtBuscar = new TextBox { Location = new Point(120, 5), Width = 250, Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right };
            txtBuscar.TextChanged += (s, e) => AplicarFiltro();
            filtros.Controls.Add(txtBuscar);
            filtros.Controls.Add(new Label { Text = "Tipo:", AutoSize = true, Location = new Point(390, 11) });
            cmbFiltroTipo = new ComboBox { Location = new Point(435, 5), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbFiltroTipo.Items.AddRange(new object[] { "Todos", "Repuesto", "Servicio" });
            cmbFiltroTipo.SelectedIndex = 0;
            cmbFiltroTipo.SelectedIndexChanged += (s, e) => AplicarFiltro();
            filtros.Controls.Add(cmbFiltroTipo);
            cmbFiltroTipo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            filtros.SizeChanged += (s, e) =>
            {
                cmbFiltroTipo.Left = Math.Max(200, filtros.ClientSize.Width - 168);
                filtros.Controls.OfType<Label>().First(l => l.Text == "Tipo:").Left = cmbFiltroTipo.Left - 42;
                txtBuscar.Width = Math.Max(50, cmbFiltroTipo.Left - txtBuscar.Left - 58);
            };
            Controls.Add(filtros);

            grid = new DataGridView
            {
                Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                AutoGenerateColumns = false, MultiSelect = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White, BorderStyle = BorderStyle.None, RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            grid.Columns.Add(Columna("ID", "ArticuloId"));
            grid.Columns.Add(Columna("Nombre", "Nombre"));
            grid.Columns.Add(Columna("Tipo", "Tipo"));
            grid.Columns.Add(Columna("Precio", "Precio"));
            grid.Columns.Add(Columna("IVA %", "PorcentajeIVA"));
            grid.Columns.Add(Columna("Stock", "Stock"));
            grid.Columns.Add(Columna("Duración", "Duracion"));
            grid.Columns.Add(Columna("Proveedor", "Proveedor"));
            grid.Columns.Add(Columna("Descripción", "Descripcion"));
            grid.CellFormatting += Grid_CellFormatting;
            grid.CellClick += (s, e) => Grid_SelectionChanged(s, EventArgs.Empty);
            Controls.Add(grid);

            Controls.SetChildIndex(titulo, 0);
            Controls.SetChildIndex(campos, 1);
            Controls.SetChildIndex(acciones, 2);
            Controls.SetChildIndex(filtros, 3);
            Controls.SetChildIndex(grid, 4);
            cmbTipo.SelectedIndex = 0;
            ConfigurarCamposTipo();
        }

        private void CargarTodo()
        {
            try
            {
                articulos = articuloService.Consultar();
                CargarProveedores();
                AplicarFiltro();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "No se pudieron cargar los artículos o proveedores. " + ex.Message,
                    "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarProveedores()
        {
            try
            {
                string nitActual = cmbProveedor.SelectedValue == null ? null : cmbProveedor.SelectedValue.ToString();
                proveedores = proveedorService.Consultar();
                cmbProveedor.DataSource = null;
                cmbProveedor.DataSource = proveedores.Select(p => new ProveedorOpcion(p)).ToList();
                cmbProveedor.DisplayMember = "Texto";
                cmbProveedor.ValueMember = "Nit";
                if (nitActual != null && proveedores.Any(p => p.Nit == nitActual)) cmbProveedor.SelectedValue = nitActual;
                else cmbProveedor.SelectedIndex = proveedores.Count > 0 ? 0 : -1;
            }
            catch (Exception ex)
            {
                proveedores = new List<Proveedor>();
                cmbProveedor.DataSource = null;
                MessageBox.Show(this, "No se pudieron cargar los proveedores. " + ex.Message,
                    "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AplicarFiltro()
        {
            if (grid == null) return;
            string nombre = txtBuscar == null ? string.Empty : txtBuscar.Text.Trim();
            string tipo = cmbFiltroTipo == null || cmbFiltroTipo.SelectedItem == null ? "Todos" : cmbFiltroTipo.Text;
            IEnumerable<Articulo> lista = articulos;
            if (nombre.Length > 0) lista = lista.Where(a => a.Nombre.IndexOf(nombre, StringComparison.OrdinalIgnoreCase) >= 0);
            if (tipo != "Todos") lista = lista.Where(a => TipoDe(a) == tipo);
            grid.DataSource = lista.Select(ArticuloFila.Crear).ToList();
        }

        private void ConfigurarCamposTipo()
        {
            if (cmbTipo == null) return;
            bool esRepuesto = cmbTipo.Text == "Repuesto";
            lblStock.Enabled = esRepuesto;
            txtStock.Enabled = esRepuesto;
            lblProveedor.Enabled = esRepuesto;
            cmbProveedor.Enabled = esRepuesto;
            lblDuracion.Enabled = !esRepuesto;
            txtDuracion.Enabled = !esRepuesto;
            txtDescripcion.Enabled = !esRepuesto;
            if (esRepuesto)
            {
                txtDuracion.Clear();
                txtDescripcion.Clear();
            }
            else
            {
                txtStock.Clear();
                cmbProveedor.SelectedIndex = -1;
            }
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;
            decimal precio;
            if (!decimal.TryParse(txtPrecio.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out precio))
            {
                MessageBox.Show(this, "Escribe un precio válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int cantidad = 0, duracion = 0;
            bool entero = cmbTipo.Text == "Repuesto" ? int.TryParse(txtStock.Text, out cantidad) : int.TryParse(txtDuracion.Text, out duracion);
            if (!entero)
            {
                MessageBox.Show(this, "La cantidad y la duración deben ser números enteros dentro del rango permitido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            double iva = Convert.ToDouble(cmbIva.Text, CultureInfo.InvariantCulture);
            string nit = cmbTipo.Text == "Repuesto" && cmbProveedor.SelectedValue != null ? cmbProveedor.SelectedValue.ToString() : null;
            string descripcion = cmbTipo.Text == "Servicio" ? TextoOpcional(txtDescripcion) : null;

            string resultado = editando
                ? articuloService.Modificar(articuloIdSeleccionado, txtNombre.Text.Trim(), (double)precio, iva,
                    cmbTipo.Text, cantidad, descripcion, duracion, nit)
                : articuloService.Guardar(txtNombre.Text.Trim(), (double)precio, iva,
                    cmbTipo.Text, cantidad, descripcion, duracion, nit);
            bool correcto = resultado.StartsWith("Se registró el artículo", StringComparison.OrdinalIgnoreCase) ||
                            resultado.StartsWith("El artículo se modificó", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, resultado, correcto ? "Inventario" : "No se pudo guardar", MessageBoxButtons.OK,
                correcto ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (correcto) { CargarTodo(); LimpiarFormulario(); }
        }

        private bool ValidarFormulario()
        {
            bool valido = true;
            valido &= !validacion.ValidarCampoVacioEspecifico(txtNombre, errores);
            valido &= !validacion.ValidarCampoVacioEspecifico(txtPrecio, errores);
            valido &= !validacion.ValidarCampoVacioEspecificoCombo(cmbTipo, errores);
            valido &= !validacion.ValidarCampoVacioEspecificoCombo(cmbIva, errores);
            if (!validacion.ValidarCampoNumero(txtPrecio, errores)) valido = false;
            if (cmbTipo.Text == "Repuesto")
            {
                valido &= !validacion.ValidarCampoVacioEspecifico(txtStock, errores);
                valido &= !validacion.ValidarCampoVacioEspecificoCombo(cmbProveedor, errores);
                if (!validacion.ValidarCampoNumero(txtStock, errores)) valido = false;
            }
            else if (cmbTipo.Text == "Servicio")
            {
                valido &= !validacion.ValidarCampoVacioEspecifico(txtDuracion, errores);
                if (!validacion.ValidarCampoNumero(txtDuracion, errores)) valido = false;
            }
            if (!valido) MessageBox.Show(this, "Completa los campos obligatorios y verifica sus valores.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return valido;
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            Articulo articulo = grid.SelectedRows.Count == 0 ? null : (grid.SelectedRows[0].DataBoundItem as ArticuloFila)?.Original;
            if (articulo == null) return;
            if (MessageBox.Show(this, "¿Eliminar el artículo seleccionado?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            string resultado = articuloService.Eliminar(articulo.ArticuloId);
            bool correcto = resultado.StartsWith("El artículo se eliminó", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, resultado, correcto ? "Inventario" : "No se pudo eliminar", MessageBoxButtons.OK,
                correcto ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (correcto) { CargarTodo(); LimpiarFormulario(); }
        }

        private void GestionarProveedores_Click(object sender, EventArgs e)
        {
            using (var formulario = new FrmProveedores()) formulario.ShowDialog(this);
            CargarProveedores();
        }

        private void Grid_SelectionChanged(object sender, EventArgs e)
        {
            Articulo articulo = grid.SelectedRows.Count == 0 ? null : (grid.SelectedRows[0].DataBoundItem as ArticuloFila)?.Original;
            if (articulo == null) return;
            editando = true;
            articuloIdSeleccionado = articulo.ArticuloId;
            cmbTipo.Enabled = false;
            txtNombre.Text = articulo.Nombre;
            txtPrecio.Text = articulo.PrecioVenta.ToString("0.00", CultureInfo.CurrentCulture);
            cmbIva.SelectedItem = articulo.PorcentajeIVA.ToString("0.##", CultureInfo.InvariantCulture);
            cmbTipo.SelectedItem = TipoDe(articulo);
            ConfigurarCamposTipo();
            if (articulo is Repuesto repuesto)
            {
                txtStock.Text = repuesto.Cantidad.ToString(CultureInfo.InvariantCulture);
                if (repuesto.Proveedor != null) cmbProveedor.SelectedValue = repuesto.Proveedor.Nit;
            }
            else if (articulo is Servicio servicio)
            {
                txtDuracion.Text = servicio.Duracion.ToString(CultureInfo.InvariantCulture);
                txtDescripcion.Text = servicio.Descripcion;
            }
            btnGuardar.Text = "Actualizar";
        }

        private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            ArticuloFila fila = grid.Rows[e.RowIndex].DataBoundItem as ArticuloFila;
            if (fila == null) return;
            if (fila.Tipo == "Repuesto" && fila.StockValue <= 5)
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 242, 204);
                if (grid.Columns[e.ColumnIndex].Name == "Stock")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(150, 45, 25);
                    e.CellStyle.Font = new Font(grid.Font, FontStyle.Bold);
                }
            }
        }

        private void LimpiarFormulario()
        {
            editando = false;
            articuloIdSeleccionado = 0;
            cmbTipo.Enabled = true;
            txtNombre.Clear(); txtPrecio.Clear(); txtStock.Clear(); txtDuracion.Clear(); txtDescripcion.Clear();
            cmbTipo.SelectedItem = "Repuesto";
            cmbIva.SelectedItem = "19";
            cmbProveedor.SelectedIndex = proveedores.Count > 0 ? 0 : -1;
            btnGuardar.Text = "Guardar";
            foreach (Control control in new Control[] { txtNombre, txtPrecio, txtStock, txtDuracion, cmbTipo, cmbIva, cmbProveedor }) errores.SetError(control, string.Empty);
            grid.ClearSelection();
            txtNombre.Focus();
        }

        private static string TipoDe(Articulo articulo)
        {
            return articulo is Repuesto ? "Repuesto" : "Servicio";
        }

        private static TextBox CrearTexto(int maximo)
        {
            return new TextBox { Dock = DockStyle.Fill, MaxLength = maximo, Margin = new Padding(4, 6, 10, 4) };
        }

        private static void AgregarCampo(TableLayoutPanel panel, int fila, string titulo, Control control, int columna)
        {
            panel.Controls.Add(new Label { Text = titulo, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, columna, fila);
            panel.Controls.Add(control, columna + 1, fila);
        }

        private static Button CrearBoton(string texto, Color color)
        {
            return new Button { Text = texto, Width = 104, Height = 32, FlatStyle = FlatStyle.Flat, ForeColor = Color.White, BackColor = color, Margin = new Padding(4), Cursor = Cursors.Hand };
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

        private sealed class ProveedorOpcion
        {
            public ProveedorOpcion(Proveedor proveedor)
            {
                Nit = proveedor.Nit;
                Texto = proveedor.RazonSocial + " — " + proveedor.Nit;
            }
            public string Nit { get; private set; }
            public string Texto { get; private set; }
        }

        private sealed class ArticuloFila
        {
            public int ArticuloId { get; set; }
            public string Nombre { get; set; }
            public string Tipo { get; set; }
            public decimal Precio { get; set; }
            public decimal PorcentajeIVA { get; set; }
            public object Stock { get; set; }
            public int StockValue { get; set; }
            public object Duracion { get; set; }
            public string Proveedor { get; set; }
            public string Descripcion { get; set; }
            public Articulo Original { get; set; }

            public static ArticuloFila Crear(Articulo articulo)
            {
                var fila = new ArticuloFila
                {
                    ArticuloId = articulo.ArticuloId, Nombre = articulo.Nombre, Tipo = TipoDe(articulo),
                    Precio = articulo.PrecioVenta, PorcentajeIVA = articulo.PorcentajeIVA,
                    Original = articulo
                };
                if (articulo is Repuesto repuesto)
                {
                    fila.Stock = repuesto.Cantidad;
                    fila.StockValue = repuesto.Cantidad;
                    fila.Proveedor = repuesto.Proveedor == null ? string.Empty : repuesto.Proveedor.RazonSocial ?? repuesto.Proveedor.Nit;
                }
                else if (articulo is Servicio servicio)
                {
                    fila.Duracion = servicio.Duracion;
                    fila.Descripcion = servicio.Descripcion;
                }
                return fila;
            }
        }
    }
}
