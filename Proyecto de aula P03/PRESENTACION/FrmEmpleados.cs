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
    public partial class FrmEmpleados : Form
    {
        private readonly CargoService cargoService;
        private readonly EmpleadoService empleadoService;
        private readonly Validacion validacion = new Validacion();
        private List<Empleado> empleados = new List<Empleado>();
        private List<Cargo> cargos = new List<Cargo>();
        private bool editando;

        public FrmEmpleados()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            var cargoRepository = new CargoRepository();
            cargoService = new CargoService(cargoRepository);
            empleadoService = new EmpleadoService(new EmpleadoRepository(), cargoRepository);
            ConfigurarInteraccion();
            Load += (s, e) => CargarTodo();
        }

        private void ConfigurarInteraccion()
        {
            Empleado usuario = SesionActual.Empleado;
            btnClaveEmpleado.Visible = usuario?.Activo == true &&
                string.Equals(usuario.Cargo?.NombreCargo?.Trim(), "Administrador", StringComparison.OrdinalIgnoreCase);

            btnGuardarEmpleado.Click += GuardarEmpleado_Click;
            btnNuevoEmpleado.Click += (s, e) => LimpiarEmpleado();
            btnEliminarEmpleado.Click += EliminarEmpleado_Click;
            btnClaveEmpleado.Click += RestablecerClaveEmpleado_Click;
            btnCrearCargo.Click += CrearCargo_Click;
            btnEliminarCargo.Click += EliminarCargo_Click;
            txtBuscar.TextChanged += (s, e) => MostrarEmpleados();
            gridEmpleados.CellClick += (s, e) => GridEmpleados_SelectionChanged(s, EventArgs.Empty);
            EstiloUI.ConectarTabla(gridEmpleados);
            EstiloUI.ConectarTabla(gridCargos);
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
