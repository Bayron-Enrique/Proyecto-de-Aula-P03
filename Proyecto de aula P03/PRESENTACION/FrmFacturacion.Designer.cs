namespace PRESENTACION
{
    partial class FrmFacturacion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlSugerencias = new System.Windows.Forms.Panel();
            this.lstSugerencias = new System.Windows.Forms.ListBox();
            this.lblSinSugerencias = new System.Windows.Forms.Label();
            this.pnlSugerencias.SuspendLayout();
            this.pnlSugerencias.BackColor = System.Drawing.Color.White;
            this.pnlSugerencias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSugerencias.Location = new System.Drawing.Point(300, 140);
            this.pnlSugerencias.Name = "pnlSugerencias";
            this.pnlSugerencias.Size = new System.Drawing.Size(320, 170);
            this.pnlSugerencias.Controls.Add(this.lstSugerencias);
            this.pnlSugerencias.Controls.Add(this.lblSinSugerencias);
            this.lstSugerencias.BackColor = System.Drawing.Color.White;
            this.lstSugerencias.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstSugerencias.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstSugerencias.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lstSugerencias.IntegralHeight = false;
            this.lstSugerencias.ItemHeight = 56;
            this.lstSugerencias.Location = new System.Drawing.Point(1, 1);
            this.lstSugerencias.Name = "lstSugerencias";
            this.lstSugerencias.Size = new System.Drawing.Size(316, 166);
            this.lstSugerencias.TabStop = false;
            this.lstSugerencias.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstSugerencias_DrawItem);
            this.lstSugerencias.Items.AddRange(new object[] { "Cliente de ejemplo", "ABC123 — Vehículo" });
            this.lblSinSugerencias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSinSugerencias.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSinSugerencias.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblSinSugerencias.Text = "No se encontraron resultados";
            this.lblSinSugerencias.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSinSugerencias.Visible = false;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 760);
            this.Text = "Facturación y cotizaciones";
            this.Controls.Add(this.pnlSugerencias);
            this.Controls.SetChildIndex(this.pnlSugerencias, 0);
            this.pnlSugerencias.ResumeLayout(false);
        }

        #endregion
    }
}
