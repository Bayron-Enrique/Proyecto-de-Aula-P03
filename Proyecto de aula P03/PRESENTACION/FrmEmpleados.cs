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
    public partial class FrmEmpleados : Form
    {
        private readonly CargoService cargoService;
        private readonly EmpleadoService empleadoService;
        private readonly Validacion validacion = new Validacion();
        private readonly ErrorProvider errores;
        private List<Empleado> empleados = new List<Empleado>();
        private List<Cargo> cargos = new List<Cargo>();
        private TextBox txtCedula, txtPrimerNombre, txtSegundoNombre, txtPrimerApellido, txtSegundoApellido, txtTelefono, txtCorreo, txtDireccion, txtBuscar, txtCargo;
        private ComboBox cmbSexo, cmbCargo;
        private CheckBox chkActivo;
        private DataGridView gridEmpleados, gridCargos;
        private Button btnGuardarEmpleado, btnNuevoEmpleado;
        private bool editando;

        public FrmEmpleados()
        {
            InitializeComponent();
            var cargoRepository = new CargoRepository();
            cargoService = new CargoService(cargoRepository);
            empleadoService = new EmpleadoService(new EmpleadoRepository(), cargoRepository);
            errores = new ErrorProvider(components) { ContainerControl = this };
            ConstruirInterfaz();
            EstiloUI.Aplicar(this);
            Load += (s, e) => CargarTodo();
        }

        private void ConstruirInterfaz()
        {
            Text = "Gestión de empleados y cargos";
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 9F);
            var encabezado = new Label
            {
                Text = "EMPLEADOS Y CARGOS", Dock = DockStyle.Top, Height = 42,
                Padding = new Padding(18, 8, 0, 0), Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 45, 60)
            };
            Controls.Add(encabezado);

            var split = new SplitContainer
            {
                Dock = DockStyle.Fill, FixedPanel = FixedPanel.Panel2,
                BackColor = BackColor, BorderStyle = BorderStyle.None
            };
            split.SizeChanged += (s, e) =>
            {
                int maximo = split.ClientSize.Width - split.SplitterWidth - split.Panel2MinSize;
                if (maximo >= split.Panel1MinSize)
                    split.SplitterDistance = Math.Max(split.Panel1MinSize, Math.Min(maximo, split.ClientSize.Width - 310 - split.SplitterWidth));
            };
            Controls.Add(split);
            split.PerformLayout();
            int distanciaMaxima = split.ClientSize.Width - split.SplitterWidth - split.Panel2MinSize;
            if (distanciaMaxima >= split.Panel1MinSize)
                split.SplitterDistance = Math.Max(split.Panel1MinSize, Math.Min(540, distanciaMaxima));
            encabezado.BringToFront();

            var busqueda = new Panel { Dock = DockStyle.Top, Height = 40, Padding = new Padding(14, 3, 14, 3) };
            busqueda.Controls.Add(new Label { Text = "Buscar por cédula o nombre:", AutoSize = true, Location = new Point(14, 11) });
            txtBuscar = new TextBox { Location = new Point(195, 5), Width = 300, Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right };
            txtBuscar.TextChanged += (s, e) => MostrarEmpleados();
            busqueda.Controls.Add(txtBuscar);
            EstiloUI.AjustarBusqueda(txtBuscar);
            split.Panel1.Controls.Add(busqueda);

            var campos = new TableLayoutPanel
            {
                Dock = DockStyle.Top, Height = 200, Padding = new Padding(14, 2, 14, 2),
                BackColor = Color.White, ColumnCount = 4, RowCount = 5
            };
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 92));
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 92));
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            for (int i = 0; i < 5; i++) campos.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            txtCedula = CrearTexto(10);
            txtPrimerNombre = CrearTexto(100);
            txtSegundoNombre = CrearTexto(100);
            txtPrimerApellido = CrearTexto(100);
            txtSegundoApellido = CrearTexto(100);
            cmbSexo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Fill };
            cmbSexo.Items.AddRange(new object[] { "M", "F" });
            cmbCargo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Fill };
            txtTelefono = CrearTexto(15);
            txtCorreo = CrearTexto(100);
            txtDireccion = CrearTexto(150);
            AgregarCampo(campos, 0, "Cédula", txtCedula, 0);
            AgregarCampo(campos, 0, "Primer nombre", txtPrimerNombre, 2);
            AgregarCampo(campos, 1, "Segundo nombre (opcional)", txtSegundoNombre, 0);
            AgregarCampo(campos, 1, "Primer apellido", txtPrimerApellido, 2);
            AgregarCampo(campos, 2, "Segundo apellido (opcional)", txtSegundoApellido, 0);
            AgregarCampo(campos, 2, "Sexo", cmbSexo, 2);
            AgregarCampo(campos, 3, "Teléfono", txtTelefono, 0);
            AgregarCampo(campos, 3, "Correo", txtCorreo, 2);
            AgregarCampo(campos, 4, "Cargo", cmbCargo, 0);
            AgregarCampo(campos, 4, "Dirección", txtDireccion, 2);
            split.Panel1.Controls.Add(campos);

            var acciones = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, MinimumSize = new Size(0, 44), Padding = new Padding(10, 3, 0, 3), BackColor = Color.White };
            btnGuardarEmpleado = CrearBoton("Guardar", Color.FromArgb(29, 111, 180));
            btnNuevoEmpleado = CrearBoton("Nuevo", Color.FromArgb(85, 95, 110));
            var btnEliminarEmpleado = CrearBoton("Eliminar", Color.FromArgb(173, 58, 58));
            var btnClaveEmpleado = CrearBoton("Restablecer clave", Color.FromArgb(85, 95, 110));
            Empleado usuario = SesionActual.Empleado;
            btnClaveEmpleado.Visible = usuario?.Activo == true &&
                string.Equals(usuario.Cargo?.NombreCargo?.Trim(), "Administrador", StringComparison.OrdinalIgnoreCase);
            chkActivo = new CheckBox { Text = "Empleado activo", Checked = true, AutoSize = true, Anchor = AnchorStyles.Left };
            btnGuardarEmpleado.Click += GuardarEmpleado_Click;
            btnNuevoEmpleado.Click += (s, e) => LimpiarEmpleado();
            btnEliminarEmpleado.Click += EliminarEmpleado_Click;
            btnClaveEmpleado.Click += RestablecerClaveEmpleado_Click;
            acciones.Controls.AddRange(new Control[] { chkActivo, btnGuardarEmpleado, btnNuevoEmpleado, btnEliminarEmpleado, btnClaveEmpleado });
            split.Panel1.Controls.Add(acciones);

            gridEmpleados = CrearGrid();
            gridEmpleados.Columns.Add(Columna("Cédula", "Cedula"));
            gridEmpleados.Columns.Add(Columna("Primer nombre", "PrimerNombre"));
            gridEmpleados.Columns.Add(Columna("Segundo nombre", "SegundoNombre"));
            gridEmpleados.Columns.Add(Columna("Primer apellido", "PrimerApellido"));
            gridEmpleados.Columns.Add(Columna("Segundo apellido", "SegundoApellido"));
            gridEmpleados.Columns.Add(Columna("Sexo", "Sexo"));
            gridEmpleados.Columns.Add(Columna("Teléfono", "Telefono"));
            gridEmpleados.Columns.Add(Columna("Correo", "CorreoElectronico"));
            gridEmpleados.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cargo", Name = "Cargo", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            gridEmpleados.CellClick += (s, e) => GridEmpleados_SelectionChanged(s, EventArgs.Empty);
            split.Panel1.Controls.Add(gridEmpleados);

            var panelCargo = split.Panel2;
            panelCargo.Padding = new Padding(10);
            var encabezadoCargo = new Label
            {
                Text = "GESTIÓN RÁPIDA DE CARGOS", Dock = DockStyle.Top, Height = 34,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft
            };
            panelCargo.Controls.Add(encabezadoCargo);
            txtCargo = new TextBox { Dock = DockStyle.Top, MaxLength = 100, Margin = new Padding(0, 5, 0, 5) };
            panelCargo.Controls.Add(txtCargo);
            var accionesCargo = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, MinimumSize = new Size(0, 44), Padding = new Padding(0, 4, 0, 4), WrapContents = true };
            var btnCrearCargo = CrearBoton("Guardar cargo", Color.FromArgb(29, 111, 180));
            var btnEliminarCargo = CrearBoton("Eliminar cargo", Color.FromArgb(173, 58, 58));
            btnCrearCargo.Width = 120;
            btnEliminarCargo.Width = 120;
            btnCrearCargo.Click += CrearCargo_Click;
            btnEliminarCargo.Click += EliminarCargo_Click;
            accionesCargo.Controls.AddRange(new Control[] { btnCrearCargo, btnEliminarCargo });
            panelCargo.Controls.Add(accionesCargo);
            gridCargos = CrearGrid();
            gridCargos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", Name = "CargoId", Width = 55 });
            gridCargos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cargo", Name = "NombreCargo", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            panelCargo.Controls.Add(gridCargos);

            split.Panel1.Controls.SetChildIndex(busqueda, 0);
            split.Panel1.Controls.SetChildIndex(campos, 1);
            split.Panel1.Controls.SetChildIndex(acciones, 2);
            split.Panel1.Controls.SetChildIndex(gridEmpleados, 3);
            panelCargo.Controls.SetChildIndex(encabezadoCargo, 0);
            panelCargo.Controls.SetChildIndex(txtCargo, 1);
            panelCargo.Controls.SetChildIndex(accionesCargo, 2);
            panelCargo.Controls.SetChildIndex(gridCargos, 3);
        }

        private void CargarTodo()
        {
            try
            {
                cargos = cargoService.Consultar();
                MostrarCargos();
                empleados = empleadoService.Consultar();
                MostrarEmpleados();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "No se pudieron cargar empleados y cargos. " + ex.Message, "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarCargos()
        {
            gridCargos.Rows.Clear();
            foreach (Cargo cargo in cargos)
            {
                int fila = gridCargos.Rows.Add(cargo.CargoId, cargo.NombreCargo);
                gridCargos.Rows[fila].Tag = cargo;
            }
            cmbCargo.DataSource = null;
            cmbCargo.DataSource = cargos.ToList();
            cmbCargo.DisplayMember = "NombreCargo";
            cmbCargo.ValueMember = "CargoId";
            cmbCargo.SelectedIndex = cargos.Count > 0 ? 0 : -1;
        }

        private void MostrarEmpleados()
        {
            if (gridEmpleados == null) return;
            string filtro = txtBuscar == null ? string.Empty : txtBuscar.Text.Trim();
            IEnumerable<Empleado> lista = empleados;
            if (filtro.Length > 0)
                lista = empleados.Where(x => x.Cedula.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    x.NombreCompleto.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0);
            gridEmpleados.Rows.Clear();
            foreach (Empleado empleado in lista)
            {
                int fila = gridEmpleados.Rows.Add(empleado.Cedula, empleado.PrimerNombre, empleado.SegundoNombre, empleado.PrimerApellido, empleado.SegundoApellido, empleado.Sexo,
                    empleado.Telefono, empleado.CorreoElectronico, empleado.Cargo == null ? "" : empleado.Cargo.NombreCargo);
                gridEmpleados.Rows[fila].Tag = empleado;
            }
        }

        private void GuardarEmpleado_Click(object sender, EventArgs e)
        {
            if (!ValidarEmpleado()) return;
            int cargoId = Convert.ToInt32(cmbCargo.SelectedValue);
            string resultado = editando
                ? empleadoService.Modificar(txtCedula.Text.Trim(), txtPrimerNombre.Text.Trim(), TextoOpcional(txtSegundoNombre), txtPrimerApellido.Text.Trim(), TextoOpcional(txtSegundoApellido), cmbSexo.Text,
                    TextoOpcional(txtTelefono), TextoOpcional(txtDireccion), TextoOpcional(txtCorreo), cargoId, chkActivo.Checked)
                : empleadoService.Guardar(txtCedula.Text.Trim(), txtPrimerNombre.Text.Trim(), TextoOpcional(txtSegundoNombre), txtPrimerApellido.Text.Trim(), TextoOpcional(txtSegundoApellido), cmbSexo.Text,
                    TextoOpcional(txtTelefono), TextoOpcional(txtDireccion), TextoOpcional(txtCorreo), cargoId, chkActivo.Checked);
            bool correcto = resultado.StartsWith("se registro el empleado", StringComparison.OrdinalIgnoreCase) ||
                            resultado.StartsWith("El empleado se modifico", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, resultado, correcto ? "Empleados" : "No se pudo guardar", MessageBoxButtons.OK,
                correcto ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (correcto) { CargarTodo(); LimpiarEmpleado(); }
        }

        private bool ValidarEmpleado()
        {
            bool valido = true;
            valido &= !validacion.ValidarCampoVacioEspecifico(txtCedula, errores);
            valido &= !validacion.ValidarCampoVacioEspecifico(txtPrimerNombre, errores);
            valido &= !validacion.ValidarCampoVacioEspecifico(txtPrimerApellido, errores);
            valido &= !validacion.ValidarCampoVacioEspecificoCombo(cmbSexo, errores);
            valido &= !validacion.ValidarCampoVacioEspecificoCombo(cmbCargo, errores);
            if (!valido) MessageBox.Show(this, "Completa los campos obligatorios y selecciona un cargo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return valido;
        }

        private void EliminarEmpleado_Click(object sender, EventArgs e)
        {
            Empleado empleado = gridEmpleados.SelectedRows.Count == 0 ? null : gridEmpleados.SelectedRows[0].Tag as Empleado;
            if (empleado == null) return;
            if (MessageBox.Show(this, "¿Eliminar el empleado seleccionado?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            string resultado = empleadoService.Eliminar(empleado.Cedula);
            bool correcto = resultado.StartsWith("El empleado se elimino", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, resultado, correcto ? "Empleados" : "No se pudo eliminar", MessageBoxButtons.OK, correcto ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (correcto) { CargarTodo(); LimpiarEmpleado(); }
        }

        private void RestablecerClaveEmpleado_Click(object sender, EventArgs e)
        {
            Empleado empleado = gridEmpleados.SelectedRows.Count == 0 ? null : gridEmpleados.SelectedRows[0].Tag as Empleado;
            if (empleado == null) { MessageBox.Show(this, "Selecciona un empleado.", "Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            using (var dialogo = new DialogoNuevaClave())
            {
                if (dialogo.ShowDialog(this) != DialogResult.OK) return;
                string mensaje = empleadoService.CambiarORestablecerClave(empleado.Cedula, dialogo.Clave);
                bool correcto = mensaje.StartsWith("La clave del empleado", StringComparison.OrdinalIgnoreCase);
                MessageBox.Show(this, mensaje, "Autenticación", MessageBoxButtons.OK, correcto ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
        }

        private void CrearCargo_Click(object sender, EventArgs e)
        {
            string resultado = cargoService.Guardar(txtCargo.Text.Trim());
            bool correcto = resultado.StartsWith("se registro el cargo", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, resultado, correcto ? "Cargos" : "No se pudo guardar", MessageBoxButtons.OK, correcto ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (correcto) { txtCargo.Clear(); CargarTodo(); }
        }

        private void EliminarCargo_Click(object sender, EventArgs e)
        {
            Cargo cargo = gridCargos.SelectedRows.Count == 0 ? null : gridCargos.SelectedRows[0].Tag as Cargo;
            if (cargo == null) return;
            if (MessageBox.Show(this, "¿Eliminar el cargo seleccionado? Si tiene empleados asignados Oracle rechazará la operación.",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            string resultado = cargoService.Eliminar(cargo.CargoId);
            bool correcto = resultado.StartsWith("El cargo se elimino", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, resultado, correcto ? "Cargos" : "No se pudo eliminar", MessageBoxButtons.OK,
                correcto ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (correcto) CargarTodo();
        }

        private void GridEmpleados_SelectionChanged(object sender, EventArgs e)
        {
            Empleado empleado = gridEmpleados.SelectedRows.Count == 0 ? null : gridEmpleados.SelectedRows[0].Tag as Empleado;
            if (empleado == null) return;
            editando = true;
            chkActivo.Checked = empleado.Activo;
            txtCedula.Text = empleado.Cedula;
            txtCedula.Enabled = false;
            txtPrimerNombre.Text = empleado.PrimerNombre;
            txtSegundoNombre.Text = empleado.SegundoNombre;
            txtPrimerApellido.Text = empleado.PrimerApellido;
            txtSegundoApellido.Text = empleado.SegundoApellido;
            cmbSexo.SelectedItem = empleado.Sexo;
            txtTelefono.Text = empleado.Telefono;
            txtCorreo.Text = empleado.CorreoElectronico;
            txtDireccion.Text = empleado.Direccion;
            cmbCargo.SelectedValue = empleado.Cargo == null ? -1 : empleado.Cargo.CargoId;
            btnGuardarEmpleado.Text = "Actualizar";
        }

        private void LimpiarEmpleado()
        {
            editando = false;
            chkActivo.Checked = true;
            txtCedula.Enabled = true;
            txtCedula.Clear(); txtPrimerNombre.Clear(); txtSegundoNombre.Clear(); txtPrimerApellido.Clear(); txtSegundoApellido.Clear(); txtTelefono.Clear(); txtCorreo.Clear(); txtDireccion.Clear();
            cmbSexo.SelectedIndex = -1;
            cmbCargo.SelectedIndex = cargos.Count > 0 ? 0 : -1;
            btnGuardarEmpleado.Text = "Guardar";
            foreach (Control control in new Control[] { txtCedula, txtPrimerNombre, txtSegundoNombre, txtPrimerApellido, txtSegundoApellido, cmbSexo, cmbCargo }) errores.SetError(control, string.Empty);
            gridEmpleados.ClearSelection();
            txtCedula.Focus();
        }

        private static TextBox CrearTexto(int maximo)
        {
            return new TextBox { Dock = DockStyle.Fill, MaxLength = maximo, Margin = new Padding(4, 7, 8, 3) };
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
            return new DataGridViewTextBoxColumn { HeaderText = encabezado, DataPropertyName = propiedad, SortMode = DataGridViewColumnSortMode.NotSortable };
        }

        private static string TextoOpcional(TextBox control)
        {
            return string.IsNullOrWhiteSpace(control.Text) ? null : control.Text.Trim();
        }

        private sealed class DialogoNuevaClave : Form
        {
            private readonly TextBox clave = new TextBox { UseSystemPasswordChar = true, Dock = DockStyle.Fill };
            private readonly TextBox confirmar = new TextBox { UseSystemPasswordChar = true, Dock = DockStyle.Fill };
            public string Clave { get { return clave.Text; } }

            public DialogoNuevaClave()
            {
                Text = "Restablecer contraseña"; StartPosition = FormStartPosition.CenterParent;
                FormBorderStyle = FormBorderStyle.FixedDialog; MinimizeBox = false; MaximizeBox = false;
                ClientSize = new Size(370, 190);
                var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(14), ColumnCount = 1, RowCount = 5 };
                layout.Controls.Add(new Label { Text = "Nueva contraseña (8 a 100 caracteres):", AutoSize = true }, 0, 0);
                layout.Controls.Add(clave, 0, 1);
                layout.Controls.Add(new Label { Text = "Confirmar contraseña:", AutoSize = true }, 0, 2);
                layout.Controls.Add(confirmar, 0, 3);
                var botones = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft };
                var aceptar = new Button { Text = "Guardar", Width = 90 };
                var cancelar = new Button { Text = "Cancelar", DialogResult = DialogResult.Cancel, Width = 90 };
                aceptar.Click += (s, e) =>
                {
                    if (clave.Text.Length < 8 || clave.Text.Length > 100 || clave.Text != confirmar.Text)
                    {
                        MessageBox.Show(this, "Las claves deben coincidir y tener entre 8 y 100 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    DialogResult = DialogResult.OK;
                };
                botones.Controls.Add(aceptar); botones.Controls.Add(cancelar); layout.Controls.Add(botones, 0, 4);
                Controls.Add(layout); AcceptButton = aceptar; CancelButton = cancelar;
            }
        }
    }
}
