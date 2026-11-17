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
    public partial class FrmPanel : Form
    {
        private Label lblClientes, lblVehiculos, lblArticulos, lblCitas, lblVentas, lblActualizado;

        public FrmPanel()
        {
            InitializeComponent();
            ConstruirDashboard();
        }

        private void ConstruirDashboard()
        {
            label1.Visible = false;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            var titulo = new Label { Text = "RESUMEN DEL TALLER", Dock = DockStyle.Top, Height = 58, Padding = new Padding(24, 14, 0, 0), Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold), ForeColor = System.Drawing.Color.FromArgb(35, 45, 60) };
            Controls.Add(titulo);
            var tarjetas = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(18), AutoScroll = true, BackColor = BackColor };
            lblClientes = AgregarTarjeta(tarjetas, "CLIENTES");
            lblVehiculos = AgregarTarjeta(tarjetas, "VEHÍCULOS");
            lblArticulos = AgregarTarjeta(tarjetas, "ARTÍCULOS / REPUESTOS");
            lblCitas = AgregarTarjeta(tarjetas, "CITAS PENDIENTES");
            lblVentas = AgregarTarjeta(tarjetas, "VENTAS");
            Controls.Add(tarjetas);
            lblActualizado = new Label { Text = "", Dock = DockStyle.Bottom, Height = 30, Padding = new Padding(18, 4, 0, 0), ForeColor = System.Drawing.Color.DimGray };
            Controls.Add(lblActualizado);
            Controls.SetChildIndex(titulo, 0);
            Controls.SetChildIndex(tarjetas, 1);
            Controls.SetChildIndex(lblActualizado, 2);
        }

        private static Label AgregarTarjeta(FlowLayoutPanel panel, string encabezado)
        {
            var tarjeta = new Panel { Width = 270, Height = 150, Margin = new Padding(12), BackColor = System.Drawing.Color.White, BorderStyle = BorderStyle.FixedSingle };
            tarjeta.Controls.Add(new Label { Text = encabezado, Dock = DockStyle.Top, Height = 46, Padding = new Padding(15, 14, 0, 0), Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold), ForeColor = System.Drawing.Color.FromArgb(75, 85, 100) });
            var valor = new Label { Text = "—", Dock = DockStyle.Fill, Padding = new Padding(15, 0, 0, 0), Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Bold), ForeColor = System.Drawing.Color.FromArgb(29, 111, 180), TextAlign = System.Drawing.ContentAlignment.MiddleLeft };
            tarjeta.Controls.Add(valor);
            panel.Controls.Add(tarjeta);
            return valor;
        }

        public void EstablecerMetricas(int clientes, int vehiculos, int articulos, int citasPendientes, decimal ventas)
        {
            lblClientes.Text = clientes.ToString("N0");
            lblVehiculos.Text = vehiculos.ToString("N0");
            lblArticulos.Text = articulos.ToString("N0");
            lblCitas.Text = citasPendientes.ToString("N0");
            lblVentas.Text = ventas.ToString("C2");
            lblActualizado.Text = "Actualizado: " + DateTime.Now.ToString("g");
        }

        public void MostrarError(string mensaje)
        {
            lblActualizado.Text = "No se pudieron actualizar las métricas: " + mensaje;
        }
    }
}
