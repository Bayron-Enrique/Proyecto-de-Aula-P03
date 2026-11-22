using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace PRESENTACION
{
    internal static class EstiloUI
    {
        private static readonly Color Tinta = Color.FromArgb(23, 24, 29);
        private static readonly Color Azul = Color.FromArgb(29, 111, 180);

        public static void Aplicar(Form formulario)
        {
            OrdenarDock(formulario);
            Aplicar(formulario.Controls);
        }

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

        // WinForms distribuye Dock desde el último control de la colección.
        // Los formularios declaran primero sus barras superiores y al final la tabla.
        private static void OrdenarDock(Control padre)
        {
            var hijos = padre.Controls.Cast<Control>().ToArray();
            if (!(padre is TableLayoutPanel) && !(padre is FlowLayoutPanel) && !(padre is SplitContainer))
            {
                var orden = hijos.Where(c => c.Dock == DockStyle.Fill)
                    .Concat(hijos.Where(c => c.Dock == DockStyle.Bottom))
                    .Concat(hijos.Where(c => c.Dock == DockStyle.Top).Reverse())
                    .Concat(hijos.Where(c => c.Dock != DockStyle.Fill && c.Dock != DockStyle.Bottom && c.Dock != DockStyle.Top)).ToArray();
                for (int i = 0; i < orden.Length; i++) padre.Controls.SetChildIndex(orden[i], i);
            }
            foreach (Control hijo in hijos) OrdenarDock(hijo);
        }

        public static void AjustarBusqueda(TextBox texto, int margenDerecho = 18)
        {
            texto.Dock = DockStyle.None;
            texto.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Action ajustar = () => texto.Width = Math.Max(40, texto.Parent.ClientSize.Width - texto.Left - margenDerecho);
            texto.Parent.SizeChanged += (s, e) => ajustar();
            ajustar();
        }

        private static void Aplicar(Control.ControlCollection controles)
        {
            foreach (Control control in controles)
            {
                DataGridView grid = control as DataGridView;
                if (grid != null)
                {
                    grid.ColumnHeadersVisible = true;
                    grid.EnableHeadersVisualStyles = false;
                    grid.ColumnHeadersDefaultCellStyle.BackColor = Tinta;
                    grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    grid.ColumnHeadersHeight = 34;
                    grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                    grid.DefaultCellStyle.BackColor = Color.White;
                    grid.DefaultCellStyle.ForeColor = Color.FromArgb(35, 45, 60);
                    grid.DefaultCellStyle.SelectionBackColor = Azul;
                    grid.DefaultCellStyle.SelectionForeColor = Color.White;
                    grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
                    grid.RowTemplate.Height = 28;
                    grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                    grid.GridColor = Color.FromArgb(225, 230, 236);
                    grid.BorderStyle = BorderStyle.None;
                    grid.CellFormatting += FormatearCelda;
                    grid.DataSourceChanged += (s, e) =>
                    {
                        grid.ClearSelection();
                        grid.CurrentCell = null;
                    };
                }
                if (control.HasChildren) Aplicar(control.Controls);
            }
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
            e.CellStyle.Font = new Font(grid.Font, FontStyle.Bold);
        }
    }
}
