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
    public partial class FrmClientes : Form
    {
        private readonly ClienteService servicio;
        private readonly Validacion validacion = new Validacion();
        private readonly ErrorProvider errores;
        private List<Cliente> clientes = new List<Cliente>();
        private TextBox txtCedula, txtPrimerNombre, txtSegundoNombre, txtPrimerApellido, txtSegundoApellido, txtTelefono, txtCorreo, txtDireccion, txtBuscar;
        private ComboBox cmbSexo;
        private DataGridView grid;
        private Button btnGuardar, btnNuevo, btnEliminar;
        private bool editando;

        public FrmClientes()
        {
            InitializeComponent();
            servicio = new ClienteService(new ClienteRepository());
            errores = new ErrorProvider(components) { ContainerControl = this };
            ConstruirInterfaz();
            EstiloUI.Aplicar(this);
            Load += (s, e) => CargarClientes();
        }

        private void ConstruirInterfaz()
        {
            Text = "Gestión de clientes";
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 9F);
            var encabezado = new Label
            {
                Text = "CLIENTES", Dock = DockStyle.Top, Height = 42, Padding = new Padding(18, 8, 0, 0),
                Font = new Font("Segoe UI", 16F, FontStyle.Bold), ForeColor = Color.FromArgb(35, 45, 60)
            };
            Controls.Add(encabezado);

            var formulario = new TableLayoutPanel
            {
                Dock = DockStyle.Top, Height = 205, Padding = new Padding(18, 4, 18, 0),
                ColumnCount = 4, RowCount = 5, BackColor = Color.White
            };
            formulario.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            formulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            formulario.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            formulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            for (int i = 0; i < 5; i++) formulario.RowStyles.Add(new RowStyle(SizeType.Percent, 20));

            txtCedula = CrearTexto(10);
            txtPrimerNombre = CrearTexto(100);
            txtSegundoNombre = CrearTexto(100);
            txtPrimerApellido = CrearTexto(100);
            txtSegundoApellido = CrearTexto(100);
            cmbSexo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Fill };
            cmbSexo.Items.AddRange(new object[] { "M", "F" });
            txtTelefono = CrearTexto(15);
            txtCorreo = CrearTexto(100);
            txtDireccion = CrearTexto(150);

            AgregarCampo(formulario, 0, "Cédula", txtCedula, 0);
            AgregarCampo(formulario, 0, "Primer nombre", txtPrimerNombre, 2);
            AgregarCampo(formulario, 1, "Segundo nombre (opcional)", txtSegundoNombre, 0);
            AgregarCampo(formulario, 1, "Primer apellido", txtPrimerApellido, 2);
            AgregarCampo(formulario, 2, "Segundo apellido (opcional)", txtSegundoApellido, 0);
            AgregarCampo(formulario, 2, "Sexo", cmbSexo, 2);
            AgregarCampo(formulario, 3, "Telefono", txtTelefono, 0);
            AgregarCampo(formulario, 3, "Correo", txtCorreo, 2);
            AgregarCampo(formulario, 4, "Direccion", txtDireccion, 0);
            formulario.SetColumnSpan(txtDireccion, 3);
            Controls.Add(formulario);

            var acciones = new FlowLayoutPanel
            {
                Dock = DockStyle.Top, Height = 48, Padding = new Padding(14, 4, 0, 4),
                BackColor = Color.White, FlowDirection = FlowDirection.LeftToRight
            };
            btnGuardar = CrearBoton("Guardar", Color.FromArgb(29, 111, 180));
            btnNuevo = CrearBoton("Nuevo", Color.FromArgb(85, 95, 110));
            btnEliminar = CrearBoton("Eliminar", Color.FromArgb(173, 58, 58));
            btnGuardar.Click += Guardar_Click;
            btnNuevo.Click += (s, e) => LimpiarFormulario();
            btnEliminar.Click += Eliminar_Click;
            acciones.Controls.AddRange(new Control[] { btnGuardar, btnNuevo, btnEliminar });
            Controls.Add(acciones);

            var buscar = new Panel { Dock = DockStyle.Top, Height = 42, Padding = new Padding(18, 4, 18, 4), BackColor = BackColor };
            buscar.Controls.Add(new Label { Text = "Buscar cédula o nombre:", AutoSize = true, Location = new Point(18, 12) });
            txtBuscar = CrearTexto(100);
            txtBuscar.Location = new Point(175, 6);
            txtBuscar.Width = 310;
            txtBuscar.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            txtBuscar.TextChanged += (s, e) => AplicarBusqueda();
            buscar.Controls.Add(txtBuscar);
            EstiloUI.AjustarBusqueda(txtBuscar);
            Controls.Add(buscar);

            grid = new DataGridView
            {
                Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                AutoGenerateColumns = false, MultiSelect = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White, BorderStyle = BorderStyle.None, RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            grid.Columns.Add(Columna("Cédula", "Cedula"));
            grid.Columns.Add(Columna("Primer nombre", "PrimerNombre"));
            grid.Columns.Add(Columna("Segundo nombre", "SegundoNombre"));
            grid.Columns.Add(Columna("Primer apellido", "PrimerApellido"));
            grid.Columns.Add(Columna("Segundo apellido", "SegundoApellido"));
            grid.Columns.Add(Columna("Sexo", "Sexo"));
            grid.Columns.Add(Columna("Teléfono", "Telefono"));
            grid.Columns.Add(Columna("Correo", "CorreoElectronico"));
            grid.Columns.Add(Columna("Dirección", "Direccion"));
            grid.CellClick += (s, e) => Grid_SelectionChanged(s, EventArgs.Empty);
            Controls.Add(grid);
            grid.BringToFront();
            buscar.BringToFront();
            acciones.BringToFront();
            formulario.BringToFront();
            encabezado.BringToFront();
        }

        private static TextBox CrearTexto(int maximo)
        {
            return new TextBox { Dock = DockStyle.Fill, MaxLength = maximo, Margin = new Padding(4, 7, 10, 4) };
        }

        private static void AgregarCampo(TableLayoutPanel panel, int fila, string texto, Control control, int columna)
        {
            panel.Controls.Add(new Label { Text = texto, TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, columna, fila);
            panel.Controls.Add(control, columna + 1, fila);
        }

        private static Button CrearBoton(string texto, Color color)
        {
            return new Button
            {
                Text = texto, Width = 104, Height = 34, FlatStyle = FlatStyle.Flat, ForeColor = Color.White,
                BackColor = color, Margin = new Padding(4), Cursor = Cursors.Hand
            };
        }

        private static DataGridViewTextBoxColumn Columna(string encabezado, string propiedad)
        {
            return new DataGridViewTextBoxColumn { HeaderText = encabezado, DataPropertyName = propiedad, SortMode = DataGridViewColumnSortMode.Automatic };
        }

        private bool ValidarFormulario()
        {
            bool valido = true;
            // Los métodos de Validacion devuelven true cuando encuentran un campo inválido.
            valido &= !validacion.ValidarCampoVacioEspecifico(txtCedula, errores);
            valido &= !validacion.ValidarCampoVacioEspecifico(txtPrimerNombre, errores);
            valido &= !validacion.ValidarCampoVacioEspecifico(txtPrimerApellido, errores);
            valido &= !validacion.ValidarCampoVacioEspecificoCombo(cmbSexo, errores);
            if (!valido) MessageBox.Show(this, "Completa los campos obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return valido;
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;
            string resultado = editando
                ? servicio.Modificar(txtCedula.Text.Trim(), txtPrimerNombre.Text.Trim(), TextoOpcional(txtSegundoNombre), txtPrimerApellido.Text.Trim(), TextoOpcional(txtSegundoApellido), cmbSexo.Text,
                    TextoOpcional(txtTelefono), TextoOpcional(txtDireccion), TextoOpcional(txtCorreo))
                : servicio.Guardar(txtCedula.Text.Trim(), txtPrimerNombre.Text.Trim(), TextoOpcional(txtSegundoNombre), txtPrimerApellido.Text.Trim(), TextoOpcional(txtSegundoApellido), cmbSexo.Text,
                    TextoOpcional(txtTelefono), TextoOpcional(txtDireccion), TextoOpcional(txtCorreo));

            bool guardado = resultado.StartsWith("se registro el cliente", StringComparison.OrdinalIgnoreCase) ||
                            resultado.StartsWith("El cliente se modifico", StringComparison.OrdinalIgnoreCase) ||
                            resultado.StartsWith("El cliente se registro correctamente", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, resultado, guardado && resultado.IndexOf("no se pudo enviar el correo", StringComparison.OrdinalIgnoreCase) < 0
                ? "Clientes" : (guardado ? "Aviso" : "No se pudo guardar"), MessageBoxButtons.OK,
                guardado ? (resultado.IndexOf("no se pudo enviar el correo", StringComparison.OrdinalIgnoreCase) >= 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information) : MessageBoxIcon.Error);
            if (guardado) { CargarClientes(); LimpiarFormulario(); }
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0) return;
            Cliente cliente = grid.SelectedRows[0].DataBoundItem as Cliente;
            if (cliente == null) return;
            if (MessageBox.Show(this, "¿Eliminar el cliente seleccionado?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            string resultado = servicio.Eliminar(cliente.Cedula);
            bool eliminado = resultado.StartsWith("El cliente se elimino", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, resultado, eliminado ? "Clientes" : "No se pudo eliminar", MessageBoxButtons.OK,
                eliminado ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (eliminado) { CargarClientes(); LimpiarFormulario(); }
        }

        private void CargarClientes()
        {
            try
            {
                clientes = servicio.Consultar();
                AplicarBusqueda();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "No se pudieron cargar los clientes. " + ex.Message, "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AplicarBusqueda()
        {
            string filtro = txtBuscar == null ? string.Empty : txtBuscar.Text.Trim();
            IEnumerable<Cliente> resultado = clientes;
            if (filtro.Length > 0)
                resultado = clientes.Where(c => c.Cedula.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    c.NombreCompleto.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (c.PrimerNombre ?? "").IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (c.SegundoNombre ?? "").IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (c.PrimerApellido ?? "").IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (c.SegundoApellido ?? "").IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0);
            grid.DataSource = resultado.ToList();
        }

        private void Grid_SelectionChanged(object sender, EventArgs e)
        {
            Cliente cliente = grid.SelectedRows.Count == 0 ? null : grid.SelectedRows[0].DataBoundItem as Cliente;
            if (cliente == null) return;
            editando = true;
            txtCedula.Text = cliente.Cedula;
            txtCedula.Enabled = false;
            txtPrimerNombre.Text = cliente.PrimerNombre;
            txtSegundoNombre.Text = cliente.SegundoNombre;
            txtPrimerApellido.Text = cliente.PrimerApellido;
            txtSegundoApellido.Text = cliente.SegundoApellido;
            cmbSexo.SelectedItem = cliente.Sexo;
            txtTelefono.Text = cliente.Telefono;
            txtCorreo.Text = cliente.CorreoElectronico;
            txtDireccion.Text = cliente.Direccion;
            btnGuardar.Text = "Actualizar";
        }

        private void LimpiarFormulario()
        {
            editando = false;
            txtCedula.Enabled = true;
            txtCedula.Clear(); txtPrimerNombre.Clear(); txtSegundoNombre.Clear(); txtPrimerApellido.Clear(); txtSegundoApellido.Clear(); txtTelefono.Clear(); txtCorreo.Clear(); txtDireccion.Clear();
            cmbSexo.SelectedIndex = -1;
            btnGuardar.Text = "Guardar";
            foreach (Control control in new Control[] { txtCedula, txtPrimerNombre, txtSegundoNombre, txtPrimerApellido, txtSegundoApellido, cmbSexo }) errores.SetError(control, string.Empty);
            grid.ClearSelection();
            txtCedula.Focus();
        }

        private static string TextoOpcional(TextBox control)
        {
            return string.IsNullOrWhiteSpace(control.Text) ? null : control.Text.Trim();
        }
    }
}
