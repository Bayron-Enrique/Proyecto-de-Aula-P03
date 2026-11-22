using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ENTIDADES;

namespace PRESENTACION
{
    public partial class FrmPanel : Form
    {
        internal enum Seccion { Clientes, Vehiculos, Articulos, Agenda, Ventas }

        private static readonly CultureInfo Cultura = CultureInfo.GetCultureInfo("es-CO");
        private bool? distribucionCompacta;

        internal event Action SolicitarActualizacion;
        internal event Action<Seccion> AbrirSeccion;
        internal event Action<int> AbrirCita;
        internal bool Actualizando { get; private set; }

        public FrmPanel()
        {
            InitializeComponent();
            ConfigurarGraficos();
            ConfigurarAcciones();
            zonaDesplazable.SizeChanged += (s, e) => AdaptarDistribucion();
            AdaptarDistribucion();
            EstablecerResumen(Array.Empty<Agenda>(), Array.Empty<AlertaTaller>());
            MostrarVacio(graficoFinanciero, vacioFinanciero, true);
            MostrarVacio(graficoCitas, vacioEstados, true);
        }

        private void ConfigurarAcciones()
        {
            btnActualizar.Click += (s, e) => SolicitarActualizacion?.Invoke();
            btnNuevaCita.Click += (s, e) => AbrirSeccion?.Invoke(Seccion.Agenda);
            btnNuevaVenta.Click += (s, e) => AbrirSeccion?.Invoke(Seccion.Ventas);
            var secciones = new[] { Seccion.Clientes, Seccion.Vehiculos, Seccion.Articulos, Seccion.Agenda, Seccion.Ventas };
            for (int i = 0; i < tarjetas.Length; i++)
            {
                Seccion destino = secciones[i];
                EventHandler navegar = (s, e) => AbrirSeccion?.Invoke(destino);
                tarjetas[i].Click += navegar;
                foreach (Control etiqueta in tarjetas[i].Controls) etiqueta.Click += navegar;
            }

            gridCitas.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0 && gridCitas.Rows[e.RowIndex].DataBoundItem is CitaFila cita)
                    AbrirCita?.Invoke(cita.Id);
            };
            gridCitas.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0 || gridCitas.Columns[e.ColumnIndex].Name != "Estado") return;
                if (e.Value as string == "PENDIENTE") PintarEstado(e.CellStyle, PrioridadAlerta.Advertencia);
                if (e.Value as string == "EN PROCESO") PintarEstado(e.CellStyle, PrioridadAlerta.Pendiente);
            };
            gridAlertas.CellFormatting += (s, e) =>
            {
                if (e.RowIndex >= 0 && gridAlertas.Columns[e.ColumnIndex].Name == "Estado" &&
                    gridAlertas.Rows[e.RowIndex].DataBoundItem is AlertaOperativa alerta)
                    PintarEstado(e.CellStyle, alerta.Prioridad);
            };
        }

        private void AdaptarDistribucion()
        {
            bool compacta = zonaDesplazable.Width < 860;
            if (distribucionCompacta == compacta) return;
            distribucionCompacta = compacta;
            contenido.SuspendLayout();
            encabezado.SuspendLayout();
            metricas.SuspendLayout();
            graficos.SuspendLayout();
            resumen.SuspendLayout();
            try
            {
                contenido.RowStyles[0].Height = compacta ? 102 : 68;
                contenido.RowStyles[1].Height = compacta ? 204 : 106;
                zonaDesplazable.AutoScrollMinSize = new Size(0, compacta ? 1150 : 615);
                contenido.MinimumSize = new Size(0, compacta ? 1132 : 597);

                PrepararTabla(encabezado, compacta ? 2 : 3, compacta ? 2 : 1);
                encabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                if (!compacta) encabezado.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                encabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, compacta ? 102 : 116));
                encabezado.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                encabezado.Controls.Add(bloqueTitulo, 0, 0);
                encabezado.Controls.Add(panelLogo, compacta ? 1 : 2, 0);
                if (compacta)
                {
                    encabezado.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
                    acciones.Anchor = AnchorStyles.Left;
                    acciones.Margin = Padding.Empty;
                    encabezado.Controls.Add(acciones, 0, 1);
                    encabezado.SetColumnSpan(acciones, 2);
                }
                else
                {
                    acciones.Anchor = AnchorStyles.Right;
                    acciones.Margin = new Padding(8, 0, 8, 0);
                    encabezado.SetColumnSpan(acciones, 1);
                    encabezado.Controls.Add(acciones, 1, 0);
                }

                int columnas = compacta ? 6 : tarjetas.Length;
                PrepararTabla(metricas, columnas, compacta ? 2 : 1);
                for (int i = 0; i < columnas; i++) metricas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / columnas));
                metricas.RowStyles.Add(new RowStyle(SizeType.Percent, compacta ? 50 : 100));
                if (compacta) metricas.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
                for (int i = 0; i < tarjetas.Length; i++)
                {
                    bool segundaFila = compacta && i >= 3;
                    int columna = !compacta ? i : segundaFila ? (i - 3) * 3 : i * 2;
                    int ancho = compacta ? (segundaFila ? 3 : 2) : 1;
                    var tarjeta = tarjetas[i];
                    tarjeta.Margin = new Padding(0, 0, columna + ancho < columnas ? 8 : 0, compacta && !segundaFila ? 8 : 0);
                    metricas.SetColumnSpan(tarjeta, ancho);
                    metricas.Controls.Add(tarjeta, columna, segundaFila ? 1 : 0);
                }

                DistribuirPar(graficos, tarjetaFinanciera, tarjetaEstados, compacta, 50);
                DistribuirPar(resumen, tarjetaCitas, tarjetaAlertas, compacta, 57);
            }
            finally
            {
                encabezado.ResumeLayout(false);
                metricas.ResumeLayout(false);
                graficos.ResumeLayout(false);
                resumen.ResumeLayout(false);
                contenido.ResumeLayout(true);
            }
        }

        private static void PrepararTabla(TableLayoutPanel tabla, int columnas, int filas)
        {
            tabla.Controls.Clear();
            tabla.ColumnStyles.Clear();
            tabla.RowStyles.Clear();
            tabla.ColumnCount = columnas;
            tabla.RowCount = filas;
        }

        private static void DistribuirPar(TableLayoutPanel tabla, Control izquierda, Control derecha, bool compacta, float peso)
        {
            PrepararTabla(tabla, compacta ? 1 : 2, compacta ? 2 : 1);
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, compacta ? 100 : peso));
            if (!compacta) tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 - peso));
            tabla.RowStyles.Add(new RowStyle(SizeType.Percent, compacta ? 50 : 100));
            if (compacta) tabla.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            izquierda.Margin = new Padding(0, 0, compacta ? 0 : 8, compacta ? 8 : 0);
            derecha.Margin = Padding.Empty;
            tabla.Controls.Add(izquierda, 0, 0);
            tabla.Controls.Add(derecha, compacta ? 0 : 1, compacta ? 1 : 0);
        }

        private void ConfigurarGraficos()
        {
            var area = new ChartArea("Finanzas") { BackColor = Color.White };
            area.AxisX.Interval = 1;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisX.MajorTickMark.Enabled = false;
            area.AxisX.LineColor = FondoPanel;
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8.5F);
            area.AxisX.LabelStyle.ForeColor = TintaPanel;
            area.AxisY.LineColor = FondoPanel;
            area.AxisY.MajorGrid.LineColor = FondoPanel;
            area.AxisY.MajorTickMark.Enabled = false;
            area.AxisY.LabelStyle.Enabled = false;
            graficoFinanciero.ChartAreas.Add(area);
            graficoFinanciero.Series.Add(new Series("Monto")
            {
                ChartType = SeriesChartType.Column, Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                LabelForeColor = TintaPanel, IsValueShownAsLabel = true, IsVisibleInLegend = false
            });

            graficoCitas.ChartAreas.Add(new ChartArea("Citas") { BackColor = Color.White });
            var dona = new Series("Citas")
            {
                ChartType = SeriesChartType.Doughnut, IsVisibleInLegend = false,
                BorderColor = Color.White, BorderWidth = 2
            };
            dona["DoughnutRadius"] = "70";
            dona["PieLabelStyle"] = "Disabled";
            graficoCitas.Series.Add(dona);
            graficoCitas.Legends.Add(new Legend("Estados")
            {
                Docking = Docking.Right, Alignment = StringAlignment.Center, IsDockedInsideChartArea = false,
                Font = new Font("Segoe UI", 9F), ForeColor = TintaPanel, BackColor = Color.White
            });
        }

        public void MostrarResumen(ResumenDashboard datos)
        {
            EstablecerMetricas(datos.Clientes, datos.Vehiculos, datos.Articulos, datos.CitasPendientes, datos.VentasActivas);
            submetricas[0].Text = $"{datos.ClientesTelegram:N0} con Telegram";
            submetricas[1].Text = $"{datos.Automoviles:N0} autos · {datos.Motocicletas:N0} motos";
            submetricas[2].Text = $"{datos.Repuestos:N0} repuestos · {datos.Servicios:N0} servicios";
            submetricas[3].Text = $"{datos.CitasEnProceso:N0} en proceso · {datos.CitasHoy:N0} hoy";
            submetricas[4].Text = datos.FacturasPendientes.ToString("C0", Cultura) + " en facturas pendientes";
            foreach (var etiqueta in submetricas) ayudas.SetToolTip(etiqueta, etiqueta.Text);
            lblActualizado.Text = "Actualizado: " + datos.Actualizado.ToString("dd/MM/yyyy hh:mm tt", Cultura);
            ayudas.SetToolTip(lblActualizado, lblActualizado.Text);
            LlenarFinanzas(datos.ResumenFinanciero);
            LlenarDona(datos.EstadosCitas);
            LlenarTablas(datos.ProximasCitas, datos.Alertas);
        }

        public void EstablecerMetricas(int clientes, int vehiculos, int articulos, int citasPendientes, decimal ventas)
        {
            cifras[0].Text = clientes.ToString("N0", Cultura);
            cifras[1].Text = vehiculos.ToString("N0", Cultura);
            cifras[2].Text = articulos.ToString("N0", Cultura);
            cifras[3].Text = citasPendientes.ToString("N0", Cultura);
            cifras[4].Text = ventas.ToString("C0", Cultura);
            ayudas.SetToolTip(cifras[4], cifras[4].Text);
            lblActualizado.Text = "Actualizado: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt", Cultura);
        }

        internal void EstablecerCarga(bool cargando)
        {
            Actualizando = cargando;
            btnActualizar.Enabled = !cargando;
            if (cargando)
            {
                lblActualizado.Text = "Actualizando…";
                ayudas.SetToolTip(lblActualizado, string.Empty);
            }
        }

        public void MostrarError(string mensaje)
        {
            lblActualizado.Text = "No se pudo actualizar. Vuelve a intentarlo.";
            ayudas.SetToolTip(lblActualizado, mensaje);
        }

        private void LlenarFinanzas(IReadOnlyList<PuntoGrafico> puntos)
        {
            var serie = graficoFinanciero.Series[0];
            serie.Points.Clear();
            for (int i = 0; i < puntos.Count; i++)
            {
                var valor = puntos[i];
                int indice = serie.Points.AddXY(i + 1, (double)valor.Valor);
                var punto = serie.Points[indice];
                punto.AxisLabel = valor.Etiqueta;
                punto.Color = ColoresFinancieros[i % ColoresFinancieros.Length];
                punto.Label = valor.Valor.ToString("C0", Cultura);
                punto.ToolTip = valor.Etiqueta + ": " + punto.Label;
            }
            MostrarVacio(graficoFinanciero, vacioFinanciero, !puntos.Any(p => p.Valor != 0));
        }

        private void LlenarDona(IReadOnlyList<PuntoGrafico> puntos)
        {
            var serie = graficoCitas.Series[0];
            var leyenda = graficoCitas.Legends[0];
            serie.Points.Clear();
            leyenda.CustomItems.Clear();
            decimal total = puntos.Sum(p => p.Valor);
            if (total > 0)
            {
                for (int i = 0; i < puntos.Count; i++)
                {
                    var valor = puntos[i];
                    Color color = ColoresCitas[i % ColoresCitas.Length];
                    string porcentaje = (valor.Valor / total).ToString("0.#%", Cultura);
                    var indicador = new LegendItem(valor.Etiqueta + " · " + porcentaje, color, string.Empty);
                    leyenda.CustomItems.Add(indicador);
                    indicador.ImageStyle = LegendImageStyle.Marker;
                    indicador.MarkerStyle = MarkerStyle.Square;
                    indicador.MarkerColor = color;
                    indicador.MarkerSize = 10;
                    if (valor.Valor > 0)
                    {
                        int indice = serie.Points.AddXY(i + 1, (double)valor.Valor);
                        var punto = serie.Points[indice];
                        punto.Color = color;
                        punto.ToolTip = valor.Etiqueta + ": " + valor.Valor.ToString("N0", Cultura) + " (" + porcentaje + ")";
                    }
                }
            }
            MostrarVacio(graficoCitas, vacioEstados, total <= 0);
        }

        public void EstablecerResumen(IEnumerable<Agenda> citas, IEnumerable<AlertaTaller> alertas)
        {
            LlenarTablas(citas ?? Enumerable.Empty<Agenda>(), alertas ?? Enumerable.Empty<AlertaTaller>());
        }

        private void LlenarTablas(IEnumerable<Agenda> citas, IEnumerable<AlertaOperativa> alertas)
        {
            var filas = citas.Select(a => new CitaFila
            {
                Id = a.AgendaID, Fecha = a.Fecha.ToString("dd/MM HH:mm", Cultura),
                Placa = a.Vehiculo?.Placa, Cliente = a.Vehiculo?.Cliente?.NombreCompleto,
                Servicio = a.ArticuloServicio?.Nombre, Empleado = a.Empleado?.NombreCompleto,
                Estado = a.Estado?.Trim().ToUpperInvariant()
            }).ToList();
            var listaAlertas = alertas.ToList();
            gridCitas.DataSource = filas;
            gridAlertas.DataSource = listaAlertas;
            gridCitas.ClearSelection();
            gridAlertas.ClearSelection();
            MostrarVacio(gridCitas, vacioCitas, filas.Count == 0);
            MostrarVacio(gridAlertas, vacioAlertas, listaAlertas.Count == 0);
        }

        private static void MostrarVacio(Control contenido, Label mensaje, bool vacio)
        {
            contenido.Visible = !vacio;
            mensaje.Visible = vacio;
        }

        private static void PintarEstado(DataGridViewCellStyle estilo, PrioridadAlerta prioridad)
        {
            switch (prioridad)
            {
                case PrioridadAlerta.Critica:
                    estilo.BackColor = Color.FromArgb(0xFE, 0xE2, 0xE2);
                    estilo.ForeColor = Color.FromArgb(0x99, 0x1B, 0x1B);
                    break;
                case PrioridadAlerta.Advertencia:
                    estilo.BackColor = Color.FromArgb(0xFF, 0xF1, 0xD6);
                    estilo.ForeColor = Color.FromArgb(0x8A, 0x50, 0x00);
                    break;
                default:
                    estilo.BackColor = Color.FromArgb(0xE9, 0xF1, 0xFF);
                    estilo.ForeColor = Color.FromArgb(0x14, 0x49, 0xB3);
                    break;
            }
            estilo.SelectionBackColor = estilo.BackColor;
            estilo.SelectionForeColor = estilo.ForeColor;
        }

        public sealed class AlertaTaller : AlertaOperativa { }

        private sealed class CitaFila
        {
            public int Id { get; set; }
            public string Fecha { get; set; }
            public string Placa { get; set; }
            public string Cliente { get; set; }
            public string Servicio { get; set; }
            public string Empleado { get; set; }
            public string Estado { get; set; }
        }
    }
}
