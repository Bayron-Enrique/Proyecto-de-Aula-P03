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
    public partial class FrmProveedores : Form
    {
        private readonly ProveedorService servicio;
        private readonly Validacion validacion = new Validacion();
        private readonly ErrorProvider errores;
        private List<Proveedor> proveedores = new List<Proveedor>();
        private TextBox txtNit, txtRazonSocial, txtTelefono, txtBuscar;
        private DataGridView grid;
        private Button btnGuardar;
        private bool editando;

        public FrmProveedores()
        {
            InitializeComponent();
            servicio = new ProveedorService(new ProveedorRepository());
            errores = new ErrorProvider(components) { ContainerControl = this };
            ConstruirInterfaz();
            EstiloUI.Aplicar(this);
            Load += (s, e) => CargarProveedores();
        }

        private void ConstruirInterfaz()
        {
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 9F);
            var titulo = new Label
            {
                Text = "PROVEEDORES", Dock = DockStyle.Top, Height = 42, Padding = new Padding(18, 8, 0, 0),
                Font = new Font("Segoe UI", 16F, FontStyle.Bold), ForeColor = Color.FromArgb(35, 45, 60)
            };
            Controls.Add(titulo);

            var campos = new TableLayoutPanel
            {
                Dock = DockStyle.Top, Height = 104, Padding = new Padding(18, 4, 18, 4),
                ColumnCount = 4, RowCount = 2, BackColor = Color.White
            };
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112));
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112));
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            campos.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            campos.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            txtNit = CrearTexto(15);
            txtRazonSocial = CrearTexto(100);
            txtTelefono = CrearTexto(20);
            AgregarCampo(campos, 0, "NIT", txtNit, 0);
            AgregarCampo(campos, 0, "Razón social", txtRazonSocial, 2);
            AgregarCampo(campos, 1, "Teléfono", txtTelefono, 0);
            Controls.Add(campos);

            var acciones = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 42, Padding = new Padding(12, 3, 0, 3), BackColor = Color.White };
            btnGuardar = CrearBoton("Guardar", Color.FromArgb(29, 111, 180));
            var btnNuevo = CrearBoton("Nuevo", Color.FromArgb(85, 95, 110));
            var btnEliminar = CrearBoton("Eliminar", Color.FromArgb(173, 58, 58));
            btnGuardar.Click += Guardar_Click;
            btnNuevo.Click += (s, e) => LimpiarFormulario();
            btnEliminar.Click += Eliminar_Click;
            acciones.Controls.AddRange(new Control[] { btnGuardar, btnNuevo, btnEliminar });
            Controls.Add(acciones);

            var panelBuscar = new Panel { Dock = DockStyle.Top, Height = 40, Padding = new Padding(18, 3, 18, 3) };
            panelBuscar.Controls.Add(new Label { Text = "Buscar por NIT o razón social:", AutoSize = true, Location = new Point(18, 10) });
            txtBuscar = new TextBox { Location = new Point(220, 5), Width = 330, Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right };
            txtBuscar.TextChanged += (s, e) => AplicarFiltro();
            panelBuscar.Controls.Add(txtBuscar);
            EstiloUI.AjustarBusqueda(txtBuscar);
            Controls.Add(panelBuscar);

            grid = new DataGridView
            {
                Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                AutoGenerateColumns = false, MultiSelect = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White, BorderStyle = BorderStyle.None, RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            grid.Columns.Add(Columna("NIT", "Nit"));
            grid.Columns.Add(Columna("Razón social", "RazonSocial"));
            grid.Columns.Add(Columna("Teléfono", "Telefono"));
            grid.CellClick += (s, e) => Grid_SelectionChanged(s, EventArgs.Empty);
            Controls.Add(grid);

            Controls.SetChildIndex(titulo, 0);
            Controls.SetChildIndex(campos, 1);
            Controls.SetChildIndex(acciones, 2);
            Controls.SetChildIndex(panelBuscar, 3);
            Controls.SetChildIndex(grid, 4);
        }

        private void CargarProveedores()
        {
            try
            {
                proveedores = servicio.Consultar();
                AplicarFiltro();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "No se pudieron consultar los proveedores. " + ex.Message,
                    "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AplicarFiltro()
        {
            if (grid == null) return;
            string filtro = txtBuscar == null ? string.Empty : txtBuscar.Text.Trim();
            IEnumerable<Proveedor> lista = proveedores;
            if (filtro.Length > 0)
                lista = proveedores.Where(p => p.Nit.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    p.RazonSocial.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0);
            grid.DataSource = lista.ToList();
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            bool valido = !validacion.ValidarCampoVacioEspecifico(txtNit, errores) &
                          !validacion.ValidarCampoVacioEspecifico(txtRazonSocial, errores);
            if (!valido)
            {
                MessageBox.Show(this, "Completa el NIT y la razón social.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string resultado = editando
                ? servicio.Modificar(txtNit.Text.Trim(), txtRazonSocial.Text.Trim(), TextoOpcional(txtTelefono))
                : servicio.Guardar(txtNit.Text.Trim(), txtRazonSocial.Text.Trim(), TextoOpcional(txtTelefono));
            bool correcto = resultado.StartsWith("Se registró el proveedor", StringComparison.OrdinalIgnoreCase) ||
                            resultado.StartsWith("El proveedor se modificó", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, resultado, correcto ? "Proveedores" : "No se pudo guardar", MessageBoxButtons.OK,
                correcto ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (correcto) { CargarProveedores(); LimpiarFormulario(); }
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0) return;
            Proveedor proveedor = grid.SelectedRows[0].DataBoundItem as Proveedor;
            if (proveedor == null) return;
            if (MessageBox.Show(this, "¿Eliminar el proveedor seleccionado? Si está asociado a repuestos, Oracle impedirá eliminarlo.",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            string resultado = servicio.Eliminar(proveedor.Nit);
            bool correcto = resultado.StartsWith("El proveedor se eliminó", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, resultado, correcto ? "Proveedores" : "No se pudo eliminar", MessageBoxButtons.OK,
                correcto ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (correcto) { CargarProveedores(); LimpiarFormulario(); }
        }

        private void Grid_SelectionChanged(object sender, EventArgs e)
        {
            Proveedor proveedor = grid.SelectedRows.Count == 0 ? null : grid.SelectedRows[0].DataBoundItem as Proveedor;
            if (proveedor == null) return;
            editando = true;
            txtNit.Text = proveedor.Nit;
            txtNit.Enabled = false;
            txtRazonSocial.Text = proveedor.RazonSocial;
            txtTelefono.Text = proveedor.Telefono;
            btnGuardar.Text = "Actualizar";
        }

        private void LimpiarFormulario()
        {
            editando = false;
            txtNit.Enabled = true;
            txtNit.Clear(); txtRazonSocial.Clear(); txtTelefono.Clear();
            btnGuardar.Text = "Guardar";
            errores.SetError(txtNit, string.Empty);
            errores.SetError(txtRazonSocial, string.Empty);
            grid.ClearSelection();
            txtNit.Focus();
        }

        private static TextBox CrearTexto(int maximo)
        {
            return new TextBox { Dock = DockStyle.Fill, MaxLength = maximo, Margin = new Padding(4, 7, 10, 4) };
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
            return new DataGridViewTextBoxColumn { HeaderText = encabezado, DataPropertyName = propiedad, SortMode = DataGridViewColumnSortMode.NotSortable };
        }

        private static string TextoOpcional(TextBox texto)
        {
            return string.IsNullOrWhiteSpace(texto.Text) ? null : texto.Text.Trim();
        }
    }
}
