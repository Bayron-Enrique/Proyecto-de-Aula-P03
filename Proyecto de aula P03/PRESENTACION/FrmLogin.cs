using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DATOS;
using ENTIDADES;
using LOGICA;

namespace PRESENTACION
{
    public partial class FrmLogin : Form
    {
        private readonly EmpleadoService empleadoService;
        private readonly ErrorProvider errores;

        private const string PlaceholderUsuario = "Ingrese su cédula";
        private const string PlaceholderClave = "Ingrese su contraseña";

        public FrmLogin()
        {
            InitializeComponent();

            empleadoService = new EmpleadoService(
                new EmpleadoRepository(),
                new CargoRepository());

            errores = new ErrorProvider { ContainerControl = this };

            ConfigurarFormulario();
            ConfigurarCampos();
            ConfigurarEventos();
            ConfigurarPaneles();
        }

        private void ConfigurarFormulario()
        {
            WindowState = FormWindowState.Maximized;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;
            MinimumSize = new Size(900, 600);
            Text = "GOT-AUTO | Iniciar sesión";
            AcceptButton = button1;
        }

        private void ConfigurarCampos()
        {
            txtUsuario.Text = PlaceholderUsuario;
            txtUsuario.ForeColor = Color.Gray;
            txtPassword.UseSystemPasswordChar = false;
            txtPassword.Text = PlaceholderClave;
            txtPassword.ForeColor = Color.Gray;
            button1.Text = "INICIAR SESIÓN";
            button1.BackColor = Color.FromArgb(37, 99, 235);
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
        }

        // Todas las suscripciones se hacen aquí; el Designer no conecta eventos.
        private void ConfigurarEventos()
        {
            txtUsuario.Enter += txtUsuario_Enter;
            txtUsuario.Leave += txtUsuario_Leave;
            txtPassword.Enter += txtPassword_Enter;
            txtPassword.Leave += txtPassword_Leave;
            chckPassword.CheckedChanged += chckPassword_CheckedChanged;
            button1.Click += button1_Click;
            Shown += FrmLogin_Shown;
        }

        private void FrmLogin_Shown(object sender, EventArgs e)
        {
            // El Panel permite abrir sin el cursor parpadeando en un TextBox.
            ActiveControl = panelCamposLogin;
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if (txtUsuario.ForeColor == Color.Gray && txtUsuario.Text == PlaceholderUsuario)
            {
                txtUsuario.Clear();
                txtUsuario.ForeColor = Color.FromArgb(31, 41, 55);
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                txtUsuario.Text = PlaceholderUsuario;
                txtUsuario.ForeColor = Color.Gray;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.ForeColor == Color.Gray && txtPassword.Text == PlaceholderClave)
            {
                txtPassword.Clear();
                txtPassword.ForeColor = Color.FromArgb(31, 41, 55);
                txtPassword.UseSystemPasswordChar = !chckPassword.Checked;
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.Text = PlaceholderClave;
                txtPassword.ForeColor = Color.Gray;
            }
        }

        private void chckPassword_CheckedChanged(object sender, EventArgs e)
        {
            bool esPlaceholder = txtPassword.ForeColor == Color.Gray;
            txtPassword.UseSystemPasswordChar = !esPlaceholder && !chckPassword.Checked;
        }

        private void ConfigurarPaneles()
        {
            RedondearControl(pnUsuario, 10);
            RedondearControl(pnContraseña, 10);
            RedondearControl(button1, 10);
            pnUsuario.Resize += (s, e) => RedondearControl(pnUsuario, 10);
            pnContraseña.Resize += (s, e) => RedondearControl(pnContraseña, 10);
            button1.Resize += (s, e) => RedondearControl(button1, 10);
        }

        private void RedondearControl(Control control, int radio)
        {
            if (control.Width <= 0 || control.Height <= 0) return;

            int diametro = Math.Min(radio * 2, Math.Min(control.Width, control.Height));
            using (GraphicsPath path = new GraphicsPath())
            {
                int ancho = control.Width;
                int alto = control.Height;
                path.AddArc(0, 0, diametro, diametro, 180, 90);
                path.AddArc(ancho - diametro, 0, diametro, diametro, 270, 90);
                path.AddArc(ancho - diametro, alto - diametro, diametro, diametro, 0, 90);
                path.AddArc(0, alto - diametro, diametro, diametro, 90, 90);
                path.CloseFigure();
                Region anterior = control.Region;
                control.Region = new Region(path);
                if (anterior != null) anterior.Dispose();
            }
        }

        private bool ValidarCampos()
        {
            errores.Clear();
            bool usuarioVacio = string.IsNullOrWhiteSpace(txtUsuario.Text) || txtUsuario.ForeColor == Color.Gray;
            bool claveVacia = string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.ForeColor == Color.Gray;

            if (usuarioVacio) errores.SetError(pnUsuario, "Ingrese su cédula.");
            if (claveVacia) errores.SetError(pnContraseña, "Ingrese su contraseña.");
            if (usuarioVacio) txtUsuario.Focus();
            else if (claveVacia) txtPassword.Focus();
            return !usuarioVacio && !claveVacia;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidarCampos()) IniciarSesion();
        }

        private void IniciarSesion()
        {
            string cedula = txtUsuario.Text.Trim();
            string clave = txtPassword.Text;

            try
            {
                button1.Enabled = false;
                Empleado empleado = empleadoService.Autenticar(cedula, clave);

                if (empleado == null || !empleado.Activo)
                {
                    MessageBox.Show(this, "Credenciales incorrectas o cuenta inactiva.", "Acceso denegado",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Clear();
                    txtPassword.ForeColor = Color.FromArgb(31, 41, 55);
                    txtPassword.UseSystemPasswordChar = !chckPassword.Checked;
                    txtPassword.Focus();
                    return;
                }

                AbrirPrincipal(empleado);
            }
            catch (Exception)
            {
                MessageBox.Show(this,
                    "No fue posible validar el acceso. Verifique la conexión con Oracle.",
                    "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button1.Enabled = true;
            }
        }

        private void AbrirPrincipal(Empleado empleado)
        {
            Form1 principal = new Form1(empleado);
            principal.FormClosed += (s, e) =>
            {
                SesionActual.Cerrar();
                txtPassword.Clear();
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.Text = PlaceholderClave;
                txtPassword.ForeColor = Color.Gray;
                Show();
                WindowState = FormWindowState.Maximized;
                Activate();
            };
            Hide();
            principal.Show();
        }
    }
}
