using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ENTIDADES;

namespace PRESENTACION
{
    public partial class FrmPanel : Form
    {
        private Label lblClientes, lblVehiculos, lblArticulos, lblCitas, lblVentas, lblActualizado;
        private DataGridView gridCitas, gridAlertas;

        public FrmPanel()
        {
            InitializeComponent();
            ConstruirDashboard();
            EstiloUI.Aplicar(this);
        }

        private void ConstruirDashboard()
        {
            label1.Visible = false;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            var titulo = new Label { Text = "RESUMEN DEL TALLER", Dock = DockStyle.Top, Height = 58, Padding = new Padding(24, 14, 0, 0), Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold), ForeColor = System.Drawing.Color.FromArgb(35, 45, 60) };
            Controls.Add(titulo);
            var tarjetas = new TableLayoutPanel { Dock = DockStyle.Top, Height = 158, Padding = new Padding(12), ColumnCount = 5, RowCount = 1, BackColor = BackColor };
            for (int i = 0; i < 5; i++) tarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            tarjetas.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            lblClientes = AgregarTarjeta(tarjetas, "CLIENTES");
            lblVehiculos = AgregarTarjeta(tarjetas, "VEHÍCULOS");
            lblArticulos = AgregarTarjeta(tarjetas, "ARTÍCULOS / REPUESTOS");
            lblCitas = AgregarTarjeta(tarjetas, "CITAS PENDIENTES");
            lblVentas = AgregarTarjeta(tarjetas, "VENTAS");
            Controls.Add(tarjetas);
            var resumen = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, Padding = new Padding(18, 4, 18, 12), BackColor = BackColor };
            resumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            resumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            resumen.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            var citasPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 0, 8, 0) };
            var alertasPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8, 0, 0, 0) };
            citasPanel.Controls.Add(new Label { Text = "PRÓXIMAS CITAS PENDIENTES", Dock = DockStyle.Top, Height = 28, Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold), ForeColor = System.Drawing.Color.FromArgb(35, 45, 60) });
            alertasPanel.Controls.Add(new Label { Text = "ALERTAS DE STOCK Y SOAT", Dock = DockStyle.Top, Height = 28, Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold), ForeColor = System.Drawing.Color.FromArgb(35, 45, 60) });
            gridCitas = CrearGrid(); gridAlertas = CrearGrid();
            foreach (string campo in new[] { "Fecha", "Placa", "Cliente", "Servicio", "Empleado" })
                gridCitas.Columns.Add(new DataGridViewTextBoxColumn { Name = campo, HeaderText = campo, DataPropertyName = campo });
            foreach (string campo in new[] { "Tipo", "Detalle", "Estado" })
                gridAlertas.Columns.Add(new DataGridViewTextBoxColumn { Name = campo, HeaderText = campo, DataPropertyName = campo });
            citasPanel.Controls.Add(gridCitas); alertasPanel.Controls.Add(gridAlertas);
            resumen.Controls.Add(citasPanel, 0, 0); resumen.Controls.Add(alertasPanel, 1, 0);
            Controls.Add(resumen);
            lblActualizado = new Label { Text = "", Dock = DockStyle.Bottom, Height = 30, Padding = new Padding(18, 4, 0, 0), ForeColor = System.Drawing.Color.DimGray };
            Controls.Add(lblActualizado);
            Controls.SetChildIndex(titulo, 0);
            Controls.SetChildIndex(tarjetas, 1);
            Controls.SetChildIndex(lblActualizado, 2);
            Controls.SetChildIndex(resumen, 2);
            Controls.SetChildIndex(lblActualizado, 3);
        }

        private static Label AgregarTarjeta(TableLayoutPanel panel, string encabezado)
        {
            var tarjeta = new Panel { Dock = DockStyle.Fill, Margin = new Padding(5), BackColor = System.Drawing.Color.White, BorderStyle = BorderStyle.FixedSingle };
            tarjeta.Controls.Add(new Label { Text = encabezado, Dock = DockStyle.Top, Height = 46, Padding = new Padding(15, 14, 0, 0), Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold), ForeColor = System.Drawing.Color.FromArgb(75, 85, 100) });
            var valor = new Label { Text = "—", Dock = DockStyle.Fill, AutoEllipsis = true, Padding = new Padding(8, 0, 0, 0), Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold), ForeColor = System.Drawing.Color.FromArgb(29, 111, 180), TextAlign = System.Drawing.ContentAlignment.MiddleLeft };
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

        public void EstablecerResumen(IEnumerable<Agenda> citas, IEnumerable<AlertaTaller> alertas)
        {
            gridCitas.DataSource = (citas ?? Enumerable.Empty<Agenda>()).Select(a => new
            {
                Fecha = a.Fecha.ToString("dd/MM HH:mm"), Placa = a.Vehiculo == null ? "" : a.Vehiculo.Placa,
                Cliente = a.Vehiculo == null || a.Vehiculo.Cliente == null ? "" : a.Vehiculo.Cliente.NombreCompleto,
                Servicio = a.ArticuloServicio == null ? "" : a.ArticuloServicio.Nombre,
                Empleado = a.Empleado == null ? "" : a.Empleado.NombreCompleto
            }).ToList();
            gridAlertas.DataSource = (alertas ?? Enumerable.Empty<AlertaTaller>()).ToList();
        }

        private static DataGridView CrearGrid()
        {
            return new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                AutoGenerateColumns = false, MultiSelect = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false, BackgroundColor = System.Drawing.Color.White, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
        }

        public sealed class AlertaTaller
        {
            public string Tipo { get; set; }
            public string Detalle { get; set; }
            public string Estado { get; set; }
        }
    }
}
