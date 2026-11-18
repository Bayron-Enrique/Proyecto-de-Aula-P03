
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

namespace PRESENTACION
{
    public partial class FrmLogin : Form
    {
        private readonly EmpleadoService empleadoService;
        private readonly Validacion validacion = new Validacion();
        private readonly ErrorProvider errores;

        public FrmLogin()
        {
            InitializeComponent();
            if (components == null) components = new System.ComponentModel.Container();
            empleadoService = new EmpleadoService(new EmpleadoRepository(), new CargoRepository());
            errores = new ErrorProvider(components) { ContainerControl = this };
            button1.Click += button1_Click;
            AcceptButton = button1;
        }

        private void chckPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chckPassword.Checked)
            {
                txtPassword.PasswordChar = '\0';

            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }

        // AGREGADO: abrir Form1 desde el login
        private void button1_Click(object sender, EventArgs e)
        {
            // ValidarCampoVacioEspecifico devuelve true cuando detecta un error.
            if (validacion.ValidarCampoVacioEspecifico(textBox1, errores) ||
                validacion.ValidarCampoVacioEspecifico(txtPassword, errores))
            {
                MessageBox.Show(this, "Ingresa usuario y contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string usuario = textBox1.Text.Trim();
                string clave = txtPassword.Text;
                int cantidadEmpleados = empleadoService.TotalEmpleados();
                if (cantidadEmpleados == 0 && usuario.Equals("admin", StringComparison.OrdinalIgnoreCase) && clave == "admin123")
                {
                    Empleado respaldo = empleadoService.AutenticarRespaldo(usuario, clave);
                    if (respaldo != null) AbrirPrincipal(respaldo);
                    else MessageBox.Show(this, "El acceso de respaldo ya no está disponible.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cantidadEmpleados == 0)
                {
                    MessageBox.Show(this, "Todavía no hay empleados. Acceso de respaldo: usuario admin, contraseña admin123.", "Acceso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Empleado empleado = empleadoService.BuscarCedula(usuario);
                if (empleado == null)
                {
                    MessageBox.Show(this, "No existe un empleado con esa cédula.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!empleado.Activo)
                {
                    MessageBox.Show(this, "El empleado está inactivo.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!empleadoService.TieneClaveConfigurada(usuario))
                {
                    if (clave != usuario)
                    {
                        MessageBox.Show(this, "Es el primer ingreso. Usa la cédula como clave temporal para definir una contraseña personal.", "Configurar acceso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    using (var dialogo = new DialogoConfigurarClave())
                    {
                        if (dialogo.ShowDialog(this) != DialogResult.OK) return;
                        string error = empleadoService.ConfigurarClaveInicial(usuario, clave, dialogo.ClaveNueva);
                        if (error != null)
                        {
                            MessageBox.Show(this, error, "No se pudo configurar el acceso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    MessageBox.Show(this, "Contraseña configurada. Ya puedes ingresar.", "Acceso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPassword.Clear();
                    return;
                }

                empleado = empleadoService.Autenticar(usuario, clave);
                if (empleado == null)
                {
                    MessageBox.Show(this, "La cédula o contraseña no es correcta.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                AbrirPrincipal(empleado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "No se pudo validar el acceso contra Oracle. " + ex.Message, "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AbrirPrincipal(Empleado empleado)
        {
            var formularioPrincipal = new Form1(empleado);
            formularioPrincipal.FormClosed += (s, args) =>
            {
                SesionActual.Cerrar();
                txtPassword.Clear();
                Show();
                Activate();
                textBox1.Focus();
            };
            Hide();
            formularioPrincipal.Show();
        }

        private sealed class DialogoConfigurarClave : Form
        {
            private readonly TextBox txtClave = new TextBox { UseSystemPasswordChar = true, Width = 250 };
            private readonly TextBox txtConfirmar = new TextBox { UseSystemPasswordChar = true, Width = 250 };
            public string ClaveNueva { get { return txtClave.Text; } }

            public DialogoConfigurarClave()
            {
                Text = "Crear contraseña"; StartPosition = FormStartPosition.CenterParent; FormBorderStyle = FormBorderStyle.FixedDialog;
                MinimizeBox = false; MaximizeBox = false; ClientSize = new System.Drawing.Size(340, 190);
                var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(14), ColumnCount = 1, RowCount = 5 };
                layout.Controls.Add(new Label { Text = "Nueva contraseña (mínimo 8 caracteres):", AutoSize = true }, 0, 0);
                layout.Controls.Add(txtClave, 0, 1);
                layout.Controls.Add(new Label { Text = "Confirmar contraseña:", AutoSize = true }, 0, 2);
                layout.Controls.Add(txtConfirmar, 0, 3);
                var botones = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft };
                var aceptar = new Button { Text = "Guardar", DialogResult = DialogResult.None, Width = 90 };
                var cancelar = new Button { Text = "Cancelar", DialogResult = DialogResult.Cancel, Width = 90 };
                aceptar.Click += (s, e) =>
                {
                    if (txtClave.Text.Length < 8 || txtClave.Text.Length > 100 || txtClave.Text != txtConfirmar.Text)
                    {
                        MessageBox.Show(this, "Las contraseñas deben coincidir y tener entre 8 y 100 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
