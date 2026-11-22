using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PRESENTACION
{
    partial class FrmPanel
    {
        private static readonly Color FondoPanel = Color.FromArgb(0xF3, 0xF5, 0xF9);
        private static readonly Color AzulPanel = Color.FromArgb(0x1A, 0x6D, 0xFF);
        private static readonly Color TintaPanel = Color.FromArgb(0x1E, 0x1E, 0x24);
        private static readonly Color GrisPanel = Color.FromArgb(0x64, 0x74, 0x8B);
        private static readonly Color AmbarPanel = Color.FromArgb(0xF5, 0x9E, 0x0B);
        private static readonly Color[] ColoresFinancieros = { AzulPanel, AmbarPanel, GrisPanel };
        private static readonly Color[] ColoresCitas = { AmbarPanel, AzulPanel, TintaPanel, GrisPanel };

        private IContainer components;
        private ToolTip ayudas;
        private TableLayoutPanel contenido, encabezado, metricas, graficos, resumen;
        private Panel zonaDesplazable, bloqueTitulo, panelLogo;
        private FlowLayoutPanel acciones;
        private Button btnActualizar, btnNuevaCita, btnNuevaVenta;
        private Button[] tarjetas;
        private Label[] cifras, submetricas;
        private Label lblActualizado, vacioFinanciero, vacioEstados, vacioCitas, vacioAlertas;
        private Chart graficoFinanciero, graficoCitas;
        private DataGridView gridCitas, gridAlertas;
        private Panel tarjetaFinanciera, tarjetaEstados, tarjetaCitas, tarjetaAlertas;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            ayudas = new ToolTip(components);
            SuspendLayout();
            Font = new Font("Segoe UI", 9F);
            BackColor = FondoPanel;
            ForeColor = TintaPanel;
            ClientSize = new Size(1120, 680);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmPanel";
            Text = "Resumen del taller";

            zonaDesplazable = new Panel { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(16, 8, 16, 10) };
            contenido = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4, Margin = Padding.Empty };
            contenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 68));
            contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 106));
            contenido.RowStyles.Add(new RowStyle(SizeType.Percent, 45));
            contenido.RowStyles.Add(new RowStyle(SizeType.Percent, 55));

            ConstruirEncabezado();
            ConstruirMetricas();
            ConstruirTarjetas();
            contenido.Controls.Add(encabezado, 0, 0);
            contenido.Controls.Add(metricas, 0, 1);
            contenido.Controls.Add(graficos, 0, 2);
            contenido.Controls.Add(resumen, 0, 3);
            zonaDesplazable.Controls.Add(contenido);
            Controls.Add(zonaDesplazable);
            ResumeLayout(true);
        }

        private void ConstruirEncabezado()
        {
            encabezado = CrearDistribucion();
            encabezado.Margin = new Padding(0, 0, 0, 8);
            bloqueTitulo = new Panel { Dock = DockStyle.Fill };
            var titulo = new Label
            {
                Text = "Resumen del taller", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold), AutoEllipsis = true, Padding = new Padding(10, 0, 0, 0)
            };
            lblActualizado = new Label
            {
                Dock = DockStyle.Bottom, Height = 19, ForeColor = GrisPanel, Padding = new Padding(10, 0, 0, 0),
                Text = "Cargando resumen…", AutoEllipsis = true
            };
            bloqueTitulo.Controls.Add(titulo);
            bloqueTitulo.Controls.Add(lblActualizado);
            bloqueTitulo.Controls.Add(new Panel { Dock = DockStyle.Left, Width = 4, BackColor = AzulPanel });

            btnActualizar = CrearAccion("↻ Actualizar", false);
            btnNuevaCita = CrearAccion("+ Nueva cita", false);
            btnNuevaVenta = CrearAccion("+ Nueva venta", true);
            acciones = new FlowLayoutPanel
            {
                AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Anchor = AnchorStyles.Right, Margin = new Padding(8, 0, 8, 0), WrapContents = false
            };
            acciones.Controls.AddRange(new Control[] { btnActualizar, btnNuevaCita, btnNuevaVenta });
            panelLogo = new Panel { Dock = DockStyle.Fill, BackColor = TintaPanel, Padding = new Padding(9, 4, 9, 4), Margin = Padding.Empty };
            panelLogo.Controls.Add(new PictureBox
            {
                AccessibleName = "GOT-AUTO", Dock = DockStyle.Fill, Image = Properties.Resources.LogotipoGotAuto,
                SizeMode = PictureBoxSizeMode.Zoom, TabStop = false
            });
        }

        private static Button CrearAccion(string texto, bool primaria)
        {
            var boton = new Button
            {
                Text = texto, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink,
                MinimumSize = new Size(0, 30), Padding = new Padding(6, 0, 6, 0), Margin = new Padding(4, 0, 0, 0),
                FlatStyle = FlatStyle.Flat, BackColor = primaria ? AzulPanel : Color.White,
                ForeColor = primaria ? Color.White : TintaPanel, UseVisualStyleBackColor = false, Cursor = Cursors.Hand
            };
            boton.FlatAppearance.BorderColor = primaria ? AzulPanel : Color.FromArgb(0xDF, 0xE5, 0xEE);
            boton.FlatAppearance.MouseOverBackColor = primaria ? Color.FromArgb(0x16, 0x5D, 0xDC) : FondoPanel;
            return boton;
        }

        private void ConstruirMetricas()
        {
            metricas = CrearDistribucion();
            metricas.Margin = new Padding(0, 0, 0, 8);
            var titulos = new[] { "Clientes", "Vehículos", "Artículos y repuestos", "Citas pendientes", "Ventas" };
            tarjetas = new Button[titulos.Length];
            cifras = new Label[titulos.Length];
            submetricas = new Label[titulos.Length];
            for (int i = 0; i < titulos.Length; i++)
            {
                var tarjeta = new Button
                {
                    AccessibleName = titulos[i], Dock = DockStyle.Fill, BackColor = Color.White,
                    FlatStyle = FlatStyle.Flat, UseVisualStyleBackColor = false, Cursor = Cursors.Hand,
                    Padding = new Padding(10, 5, 10, 5)
                };
                tarjeta.FlatAppearance.BorderSize = 0;
                tarjeta.FlatAppearance.MouseOverBackColor = Color.FromArgb(0xE9, 0xF1, 0xFF);
                var titulo = new Label
                {
                    Text = titulos[i], Dock = DockStyle.Top, Height = 21, AutoEllipsis = true,
                    ForeColor = TintaPanel, Font = new Font("Segoe UI", 9F, FontStyle.Bold), Cursor = Cursors.Hand
                };
                var cifra = new Label
                {
                    Text = "—", Dock = DockStyle.Fill, ForeColor = AzulPanel,
                    TextAlign = ContentAlignment.MiddleLeft, Font = new Font("Segoe UI", 19F, FontStyle.Bold),
                    AutoEllipsis = true, Cursor = Cursors.Hand
                };
                var detalle = new Label
                {
                    Dock = DockStyle.Bottom, Height = 30, ForeColor = GrisPanel,
                    AutoEllipsis = true, Cursor = Cursors.Hand
                };
                tarjeta.Controls.Add(cifra);
                tarjeta.Controls.Add(detalle);
                tarjeta.Controls.Add(titulo);
                tarjetas[i] = tarjeta;
                cifras[i] = cifra;
                submetricas[i] = detalle;
            }
        }

        private void ConstruirTarjetas()
        {
            graficos = CrearDistribucion();
            graficos.Margin = new Padding(0, 0, 0, 8);
            resumen = CrearDistribucion();
            graficoFinanciero = NuevoGrafico();
            graficoCitas = NuevoGrafico();
            gridCitas = NuevaTabla();
            gridAlertas = NuevaTabla();
            gridCitas.Cursor = Cursors.Hand;
            gridCitas.Columns.AddRange(Columna("Fecha", 80, 100), Columna("Placa", 52, 61),
                Columna("Cliente", 58, 117), Columna("Servicio", 58, 113), Columna("Empleado", 62, 108), Columna("Estado", 78, 83));
            gridAlertas.Columns.AddRange(Columna("Tipo", 67, 82), Columna("Detalle", 82, 180), Columna("Estado", 85, 90));
            tarjetaFinanciera = CrearTarjeta("Resumen financiero", graficoFinanciero, "Sin movimientos financieros", out vacioFinanciero);
            tarjetaEstados = CrearTarjeta("Estado de citas", graficoCitas, "Sin citas registradas", out vacioEstados);
            tarjetaCitas = CrearTarjeta("Próximas citas pendientes", gridCitas, "No hay próximas citas activas.", out vacioCitas);
            tarjetaAlertas = CrearTarjeta("Alertas operativas\n(Stock, SOAT, Mantenimiento y Cartera)", gridAlertas,
                "No hay alertas operativas.", out vacioAlertas, 44);
        }

        private static TableLayoutPanel CrearDistribucion()
        {
            return new TableLayoutPanel { Dock = DockStyle.Fill, Margin = Padding.Empty, GrowStyle = TableLayoutPanelGrowStyle.FixedSize };
        }

        private static Chart NuevoGrafico()
        {
            return new Chart { Dock = DockStyle.Fill, BackColor = Color.White, TabStop = false, Palette = ChartColorPalette.None };
        }

        private static Panel CrearTarjeta(string titulo, Control cuerpo, string textoVacio, out Label vacio, int alturaTitulo = 30)
        {
            var tarjeta = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(9, 4, 9, 8) };
            var zona = new Panel { Dock = DockStyle.Fill };
            vacio = new Label
            {
                Dock = DockStyle.Fill, Text = textoVacio, ForeColor = GrisPanel,
                TextAlign = ContentAlignment.MiddleCenter
            };
            zona.Controls.Add(cuerpo);
            zona.Controls.Add(vacio);
            tarjeta.Controls.Add(zona);
            tarjeta.Controls.Add(new Label
            {
                Dock = DockStyle.Top, Text = titulo, TextAlign = ContentAlignment.MiddleLeft,
                Height = alturaTitulo, Font = new Font("Segoe UI", 10F, FontStyle.Bold), AutoEllipsis = true
            });
            return tarjeta;
        }

        private static DataGridView NuevaTabla()
        {
            var tabla = new DataGridView
            {
                Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false, MultiSelect = false, RowHeadersVisible = false, AutoGenerateColumns = false,
                BackgroundColor = Color.White, BorderStyle = BorderStyle.None, GridColor = FondoPanel,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None, EnableHeadersVisualStyles = false,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing, ColumnHeadersHeight = 29,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = TintaPanel, ForeColor = Color.White, Font = new Font("Segoe UI", 8.5F, FontStyle.Bold)
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.White, ForeColor = TintaPanel, SelectionBackColor = AzulPanel, SelectionForeColor = Color.White
                },
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = FondoPanel }
            };
            tabla.RowTemplate.Height = 27;
            return tabla;
        }

        private static DataGridViewTextBoxColumn Columna(string etiqueta, int minimo, float peso)
        {
            return new DataGridViewTextBoxColumn
            {
                Name = etiqueta, HeaderText = etiqueta, DataPropertyName = etiqueta,
                FillWeight = peso, MinimumWidth = minimo, SortMode = DataGridViewColumnSortMode.NotSortable
            };
        }
    }
}
