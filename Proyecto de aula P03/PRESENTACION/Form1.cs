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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        bool menuExpand = false;
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
            this.WindowState = FormWindowState.Maximized;
        }

        
    }
}
