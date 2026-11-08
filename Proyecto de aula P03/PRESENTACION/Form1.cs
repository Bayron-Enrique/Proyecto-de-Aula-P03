using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRESENTACION
{
    public partial class Form1 : Form
    {
        FrmPanel frmPanel;
        public Form1()
        {
            InitializeComponent();
            
        }

        bool menuExpand = false;

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        Color colorNormal = Color.FromArgb(23, 24, 29);
        Color colorActivo = Color.FromArgb(85, 85, 85);

        private void MenuTransition_Tick(object sender, EventArgs e)
        {
            if(menuExpand == false)
            {
                menuContainer.Height += 10;

                if(menuContainer.Height >= 165)
                {
                    MenuTransition.Stop();
                    menuExpand = true;
                }
            }

            else
            {
                menuContainer.Height -= 10;
                if(menuContainer.Height <= 53)
                {
                    MenuTransition.Stop();
                    menuExpand = false;
                }
            }
        }

        private void Menu_Click(object sender, EventArgs e)
        {
            MenuTransition.Start();
            PintarBotonActivo(btnAgenda);
        }

        bool sidebarExpand = true;
        private void sidebarTransition_Tick(object sender, EventArgs e)
        {
            sidebar.SuspendLayout();

            if (sidebarExpand)
            {
                sidebar.Width -= 10;

                PnPanel.Width = sidebar.Width;
                menuContainer.Width = sidebar.Width;
                pnServicio.Width = sidebar.Width;
                pnVehiculos.Width = sidebar.Width;
                pnClientes.Width = sidebar.Width;
                pnConfiguracion.Width = sidebar.Width;

                if(sidebar.Width < 150)
                {
                    button2.Text = "";
                    btnAgenda.Text = "";
                    btnServicio.Text = "";
                    btnVehiculos.Text = "";
                    btnClientes.Text = "";
                    btnConfiguracion.Text = "";
                }

                if (sidebar.Width <= 57)
                {
                    sidebarExpand = false;
                    sidebarTransition.Stop();

                }
            }

            else
            {
                sidebar.Width += 10;

                PnPanel.Width = sidebar.Width;
                menuContainer.Width = sidebar.Width;
                pnServicio.Width = sidebar.Width;
                pnVehiculos.Width = sidebar.Width;
                pnClientes.Width = sidebar.Width;
                pnConfiguracion.Width = sidebar.Width;

                if (sidebar.Width >= 251)
                {
                    RellenarTexto();

                    sidebarExpand = true;
                    sidebarTransition.Stop();
                }
            }

            sidebar.ResumeLayout();
        }

        private void RellenarTexto()
        {
            button2.Text = "             Panel";
            btnAgenda.Text = "            Agenda";
            btnServicio.Text = "           Ordenes de servicio";
            btnVehiculos.Text = "            Vehiculos";
            btnClientes.Text = "           Clientes";
            btnConfiguracion.Text = "           Configuracion";
        }

        private void btnHam_Click(object sender, EventArgs e)
        {
            sidebarTransition.Start();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                
                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.WindowState = FormWindowState.Maximized;

            }
            else
            {
                this.WindowState = FormWindowState.Normal;

            }
        }

        private void pnBarraSuperior_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pnBarraSuperior_MouseUp(object sender, MouseEventArgs e)
        {
            pnBarraSuperior.Cursor = Cursors.Default;
        }

        
        private void PintarBotonActivo(Button activo)
        {
            button2.BackColor = colorNormal;
            btnAgenda.BackColor = colorNormal;
            btnServicio.BackColor = colorNormal;
            btnVehiculos.BackColor = colorNormal;
            btnClientes.BackColor = colorNormal;
            btnConfiguracion.BackColor = colorNormal;

            activo.BackColor = colorActivo;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PintarBotonActivo(button2);

            if(frmPanel == null)
            {
                frmPanel = new FrmPanel();
                frmPanel.FormClosed += FrmPanel_FormClosed;
                frmPanel.MdiParent = this;
                frmPanel.Dock = DockStyle.Fill;
                frmPanel.Show();
            }
            else
            {
                frmPanel.Activate();
            }
            
        }

        private void FrmPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmPanel = null;
        }

        private void btnServicio_Click(object sender, EventArgs e)
        {
            PintarBotonActivo(btnServicio);
        }

        private void btnVehiculos_Click(object sender, EventArgs e)
        {
            PintarBotonActivo(btnVehiculos);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            PintarBotonActivo(btnClientes);
        }
    }
}
