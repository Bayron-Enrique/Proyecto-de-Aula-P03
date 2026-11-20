using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace PRESENTACION
{
    internal static class EstiloUI
    {
        public static void ConectarTabla(DataGridView grid)
        {
            grid.AutoGenerateColumns = false;
            grid.CellFormatting += FormatearCelda;
            grid.DataSourceChanged += (s, e) =>
            {
                grid.ClearSelection();
                grid.CurrentCell = null;
            };
        }

        private static void FormatearCelda(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if (grid == null || e.RowIndex < 0 || e.ColumnIndex < 0) return;
            string columna = (grid.Columns[e.ColumnIndex].Name + " " + grid.Columns[e.ColumnIndex].DataPropertyName).ToUpperInvariant();
            if (columna.Contains("PORCENTAJE") && e.Value != null && decimal.TryParse(Convert.ToString(e.Value), out decimal porcentaje))
            {
                e.Value = porcentaje.ToString("0.##'%'", CultureInfo.CurrentCulture);
                e.FormattingApplied = true;
                return;
            }
            if ((columna.Contains("PRECIO") || columna.Contains("VALOR") || columna.Contains("SUBTOTAL") ||
                 columna.Contains("TOTAL") || columna.Contains("IVA") || columna.Contains("VENTAS")) &&
                e.Value != null && decimal.TryParse(Convert.ToString(e.Value, CultureInfo.CurrentCulture), out decimal dinero))
            {
                e.Value = dinero.ToString("C2", CultureInfo.CurrentCulture);
                e.FormattingApplied = true;
            }

            if (!columna.Contains("ESTADO") || e.Value == null) return;
            string estado = Convert.ToString(e.Value).Trim().ToUpperInvariant();
            Color fondo;
            Color texto;
            switch (estado)
            {
                case "PENDIENTE": fondo = Color.FromArgb(255, 243, 205); texto = Color.FromArgb(130, 86, 0); break;
                case "EN PROCESO": fondo = Color.FromArgb(207, 226, 255); texto = Color.FromArgb(24, 72, 130); break;
                case "COMPLETADA":
                case "ENVIADA":
                case "VIGENTE": fondo = Color.FromArgb(212, 237, 218); texto = Color.FromArgb(30, 100, 55); break;
                case "PENDIENTE VIGENCIA": fondo = Color.FromArgb(227, 235, 244); texto = Color.FromArgb(55, 78, 105); break;
                case "POR VENCER": fondo = Color.FromArgb(255, 230, 190); texto = Color.FromArgb(130, 75, 0); break;
                case "VENCIDO":
                case "CANCELADA":
                case "ANULADA": fondo = Color.FromArgb(248, 215, 218); texto = Color.FromArgb(135, 35, 45); break;
                default: return;
            }
            e.CellStyle.BackColor = fondo;
            e.CellStyle.ForeColor = texto;
            e.CellStyle.SelectionBackColor = fondo;
            e.CellStyle.SelectionForeColor = texto;
            e.CellStyle.Font = grid.ColumnHeadersDefaultCellStyle.Font;
        }
    }
}
