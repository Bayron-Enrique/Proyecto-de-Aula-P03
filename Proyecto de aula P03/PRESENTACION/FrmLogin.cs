
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRESENTACION
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();

            // AGREGADO: conectar el botón Ingresar
            button1.Click += button1_Click;
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
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show(
                    "Debe ingresar un usuario.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                textBox1.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show(
                    "Debe ingresar una contraseña.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPassword.Focus();
                return;
            }

            Form1 formularioPrincipal = new Form1();

            formularioPrincipal.FormClosed += (s, args) =>
            {
                this.Close();
            };

            this.Hide();
            formularioPrincipal.Show();
        }
    }
}
