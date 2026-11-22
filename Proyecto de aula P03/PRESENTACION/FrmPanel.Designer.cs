using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PRESENTACION
{
    partial class FrmPanel
    {
        private IContainer components;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Forms Designer generated code

        private void InitializeComponent()
        {
            DataGridViewCellStyle gridCitasColumnHeadersDefaultCellStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle gridCitasDefaultCellStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle gridCitasAlternatingRowsDefaultCellStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle gridAlertasColumnHeadersDefaultCellStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle gridAlertasDefaultCellStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle gridAlertasAlternatingRowsDefaultCellStyle = new DataGridViewCellStyle();

            this.components = new Container();
            this.encabezado = new Panel();
            this.lblTitulo = new Label();
            this.acentoTitulo = new Panel();
            this.panelLogo = new Panel();
            this.picLogo = new PictureBox();
            this.metricas = new TableLayoutPanel();
            this.tarjetaClientes = new Panel();
            this.tituloClientes = new Label();
            this.lblClientes = new Label();
            this.tarjetaVehiculos = new Panel();
            this.tituloVehiculos = new Label();
            this.lblVehiculos = new Label();
            this.tarjetaArticulos = new Panel();
            this.tituloArticulos = new Label();
            this.lblArticulos = new Label();
            this.tarjetaCitas = new Panel();
            this.tituloCitas = new Label();
            this.lblCitas = new Label();
            this.tarjetaVentas = new Panel();
            this.tituloVentas = new Label();
            this.lblVentas = new Label();
            this.espacioPanel = new Panel();
            this.lblActualizado = new Label();
            this.resumen = new TableLayoutPanel();
            this.resumenCitas = new Panel();
            this.resumenAlertas = new Panel();
            this.proximas = new Panel();
            this.proximasTitulo = new Label();
            this.gridCitas = new DataGridView();
            this.gridCitasColFecha = new DataGridViewTextBoxColumn();
            this.gridCitasColPlaca = new DataGridViewTextBoxColumn();
            this.gridCitasColCliente = new DataGridViewTextBoxColumn();
            this.gridCitasColServicio = new DataGridViewTextBoxColumn();
            this.gridCitasColEmpleado = new DataGridViewTextBoxColumn();
            this.alertas = new Panel();
            this.alertasTitulo = new Label();
            this.gridAlertas = new DataGridView();
            this.gridAlertasColTipo = new DataGridViewTextBoxColumn();
            this.gridAlertasColDetalle = new DataGridViewTextBoxColumn();
            this.gridAlertasColEstado = new DataGridViewTextBoxColumn();

            ((ISupportInitialize)this.gridCitas).BeginInit();
            ((ISupportInitialize)this.gridAlertas).BeginInit();
            ((ISupportInitialize)this.picLogo).BeginInit();
            this.encabezado.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.acentoTitulo.SuspendLayout();
            this.metricas.SuspendLayout();
            this.tarjetaClientes.SuspendLayout();
            this.tarjetaVehiculos.SuspendLayout();
            this.tarjetaArticulos.SuspendLayout();
            this.tarjetaCitas.SuspendLayout();
            this.tarjetaVentas.SuspendLayout();
            this.espacioPanel.SuspendLayout();
            this.resumen.SuspendLayout();
            this.resumenCitas.SuspendLayout();
            this.resumenAlertas.SuspendLayout();
            this.proximas.SuspendLayout();
            this.alertas.SuspendLayout();
            this.SuspendLayout();

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Font = new Font("Segoe UI", 9F);
            this.BackColor = Color.FromArgb(243, 246, 251);
            this.ForeColor = Color.FromArgb(17, 24, 39);
            this.ClientSize = new Size(1120, 680);
            this.MinimumSize = new Size(680, 440);
            this.Padding = new Padding(20, 8, 20, 16);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Name = "FrmPanel";
            this.Text = "Resumen del taller";
            this.FormBorderStyle = FormBorderStyle.None;

            // encabezado
            this.encabezado.Dock = DockStyle.Top;
            this.encabezado.Height = 76;
            this.encabezado.TabIndex = 0;
            this.encabezado.Margin = new Padding(0);
            this.encabezado.Padding = new Padding(0, 10, 0, 10);
            this.encabezado.Name = "encabezado";

            // lblTitulo
            this.lblTitulo.Text = "Resumen del taller";
            this.lblTitulo.Dock = DockStyle.Fill;
            this.lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            this.lblTitulo.ForeColor = Color.FromArgb(17, 24, 39);
            this.lblTitulo.Font = new Font("Segoe UI", 23F, FontStyle.Bold);
            this.lblTitulo.Margin = new Padding(0);
            this.lblTitulo.AutoEllipsis = true;
            this.lblTitulo.TabStop = false;
            this.lblTitulo.Padding = new Padding(9, 0, 0, 0);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Name = "lblTitulo";

            // acentoTitulo
            this.acentoTitulo.Dock = DockStyle.Left;
            this.acentoTitulo.Width = 4;
            this.acentoTitulo.BackColor = Color.FromArgb(37, 99, 235);
            this.acentoTitulo.TabIndex = 1;
            this.acentoTitulo.Name = "acentoTitulo";

            // panelLogo
            this.panelLogo.BackColor = Color.FromArgb(17, 24, 39);
            this.panelLogo.Dock = DockStyle.Right;
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Padding = new Padding(12, 5, 12, 5);
            this.panelLogo.TabIndex = 2;
            this.panelLogo.Width = 160;

            // picLogo
            this.picLogo.AccessibleName = "GOT-AUTO";
            this.picLogo.Dock = DockStyle.Fill;
            this.picLogo.Image = global::PRESENTACION.Properties.Resources.LogotipoGotAuto;
            this.picLogo.Name = "picLogo";
            this.picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            this.picLogo.TabStop = false;

            // metricas
            this.metricas.ColumnCount = 5;
            this.metricas.RowCount = 1;
            this.metricas.Dock = DockStyle.Top;
            this.metricas.Margin = new Padding(0);
            this.metricas.Height = 98;
            this.metricas.TabIndex = 1;
            this.metricas.Name = "metricas";
            this.metricas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            this.metricas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            this.metricas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            this.metricas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            this.metricas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            this.metricas.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            // tarjetaClientes
            this.tarjetaClientes.Dock = DockStyle.Fill;
            this.tarjetaClientes.BackColor = Color.White;
            this.tarjetaClientes.Padding = new Padding(12, 7, 12, 12);
            this.tarjetaClientes.Margin = new Padding(0, 0, 9, 0);
            this.tarjetaClientes.TabIndex = 0;
            this.tarjetaClientes.Name = "tarjetaClientes";

            // tituloClientes
            this.tituloClientes.Text = "Clientes";
            this.tituloClientes.Dock = DockStyle.Top;
            this.tituloClientes.TextAlign = ContentAlignment.MiddleLeft;
            this.tituloClientes.ForeColor = Color.FromArgb(75, 85, 99);
            this.tituloClientes.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.tituloClientes.Margin = new Padding(0);
            this.tituloClientes.AutoEllipsis = true;
            this.tituloClientes.TabStop = false;
            this.tituloClientes.Height = 26;
            this.tituloClientes.TabIndex = 0;
            this.tituloClientes.Name = "tituloClientes";

            // lblClientes
            this.lblClientes.Text = "—";
            this.lblClientes.Dock = DockStyle.Fill;
            this.lblClientes.TextAlign = ContentAlignment.MiddleLeft;
            this.lblClientes.ForeColor = Color.FromArgb(37, 99, 235);
            this.lblClientes.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            this.lblClientes.Margin = new Padding(0);
            this.lblClientes.AutoEllipsis = true;
            this.lblClientes.TabStop = false;
            this.lblClientes.TabIndex = 1;
            this.lblClientes.Name = "lblClientes";

            // tarjetaVehiculos
            this.tarjetaVehiculos.Dock = DockStyle.Fill;
            this.tarjetaVehiculos.BackColor = Color.White;
            this.tarjetaVehiculos.Padding = new Padding(12, 7, 12, 12);
            this.tarjetaVehiculos.Margin = new Padding(0, 0, 9, 0);
            this.tarjetaVehiculos.TabIndex = 1;
            this.tarjetaVehiculos.Name = "tarjetaVehiculos";

            // tituloVehiculos
            this.tituloVehiculos.Text = "Vehículos";
            this.tituloVehiculos.Dock = DockStyle.Top;
            this.tituloVehiculos.TextAlign = ContentAlignment.MiddleLeft;
            this.tituloVehiculos.ForeColor = Color.FromArgb(75, 85, 99);
            this.tituloVehiculos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.tituloVehiculos.Margin = new Padding(0);
            this.tituloVehiculos.AutoEllipsis = true;
            this.tituloVehiculos.TabStop = false;
            this.tituloVehiculos.Height = 26;
            this.tituloVehiculos.TabIndex = 0;
            this.tituloVehiculos.Name = "tituloVehiculos";

            // lblVehiculos
            this.lblVehiculos.Text = "—";
            this.lblVehiculos.Dock = DockStyle.Fill;
            this.lblVehiculos.TextAlign = ContentAlignment.MiddleLeft;
            this.lblVehiculos.ForeColor = Color.FromArgb(37, 99, 235);
            this.lblVehiculos.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            this.lblVehiculos.Margin = new Padding(0);
            this.lblVehiculos.AutoEllipsis = true;
            this.lblVehiculos.TabStop = false;
            this.lblVehiculos.TabIndex = 1;
            this.lblVehiculos.Name = "lblVehiculos";

            // tarjetaArticulos
            this.tarjetaArticulos.Dock = DockStyle.Fill;
            this.tarjetaArticulos.BackColor = Color.White;
            this.tarjetaArticulos.Padding = new Padding(12, 7, 12, 12);
            this.tarjetaArticulos.Margin = new Padding(0, 0, 9, 0);
            this.tarjetaArticulos.TabIndex = 2;
            this.tarjetaArticulos.Name = "tarjetaArticulos";

            // tituloArticulos
            this.tituloArticulos.Text = "Artículos y repuestos";
            this.tituloArticulos.Dock = DockStyle.Top;
            this.tituloArticulos.TextAlign = ContentAlignment.MiddleLeft;
            this.tituloArticulos.ForeColor = Color.FromArgb(75, 85, 99);
            this.tituloArticulos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.tituloArticulos.Margin = new Padding(0);
            this.tituloArticulos.AutoEllipsis = true;
            this.tituloArticulos.TabStop = false;
            this.tituloArticulos.Height = 26;
            this.tituloArticulos.TabIndex = 0;
            this.tituloArticulos.Name = "tituloArticulos";

            // lblArticulos
            this.lblArticulos.Text = "—";
            this.lblArticulos.Dock = DockStyle.Fill;
            this.lblArticulos.TextAlign = ContentAlignment.MiddleLeft;
            this.lblArticulos.ForeColor = Color.FromArgb(37, 99, 235);
            this.lblArticulos.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            this.lblArticulos.Margin = new Padding(0);
            this.lblArticulos.AutoEllipsis = true;
            this.lblArticulos.TabStop = false;
            this.lblArticulos.TabIndex = 1;
            this.lblArticulos.Name = "lblArticulos";

            // tarjetaCitas
            this.tarjetaCitas.Dock = DockStyle.Fill;
            this.tarjetaCitas.BackColor = Color.White;
            this.tarjetaCitas.Padding = new Padding(12, 7, 12, 12);
            this.tarjetaCitas.Margin = new Padding(0, 0, 9, 0);
            this.tarjetaCitas.TabIndex = 3;
            this.tarjetaCitas.Name = "tarjetaCitas";

            // tituloCitas
            this.tituloCitas.Text = "Citas pendientes";
            this.tituloCitas.Dock = DockStyle.Top;
            this.tituloCitas.TextAlign = ContentAlignment.MiddleLeft;
            this.tituloCitas.ForeColor = Color.FromArgb(75, 85, 99);
            this.tituloCitas.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.tituloCitas.Margin = new Padding(0);
            this.tituloCitas.AutoEllipsis = true;
            this.tituloCitas.TabStop = false;
            this.tituloCitas.Height = 26;
            this.tituloCitas.TabIndex = 0;
            this.tituloCitas.Name = "tituloCitas";

            // lblCitas
            this.lblCitas.Text = "—";
            this.lblCitas.Dock = DockStyle.Fill;
            this.lblCitas.TextAlign = ContentAlignment.MiddleLeft;
            this.lblCitas.ForeColor = Color.FromArgb(37, 99, 235);
            this.lblCitas.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            this.lblCitas.Margin = new Padding(0);
            this.lblCitas.AutoEllipsis = true;
            this.lblCitas.TabStop = false;
            this.lblCitas.TabIndex = 1;
            this.lblCitas.Name = "lblCitas";

            // tarjetaVentas
            this.tarjetaVentas.Dock = DockStyle.Fill;
            this.tarjetaVentas.BackColor = Color.White;
            this.tarjetaVentas.Padding = new Padding(12, 7, 12, 12);
            this.tarjetaVentas.Margin = new Padding(0, 0, 0, 0);
            this.tarjetaVentas.TabIndex = 4;
            this.tarjetaVentas.Name = "tarjetaVentas";

            // tituloVentas
            this.tituloVentas.Text = "Ventas";
            this.tituloVentas.Dock = DockStyle.Top;
            this.tituloVentas.TextAlign = ContentAlignment.MiddleLeft;
            this.tituloVentas.ForeColor = Color.FromArgb(75, 85, 99);
            this.tituloVentas.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.tituloVentas.Margin = new Padding(0);
            this.tituloVentas.AutoEllipsis = true;
            this.tituloVentas.TabStop = false;
            this.tituloVentas.Height = 26;
            this.tituloVentas.TabIndex = 0;
            this.tituloVentas.Name = "tituloVentas";

            // lblVentas
            this.lblVentas.Text = "—";
            this.lblVentas.Dock = DockStyle.Fill;
            this.lblVentas.TextAlign = ContentAlignment.MiddleLeft;
            this.lblVentas.ForeColor = Color.FromArgb(37, 99, 235);
            this.lblVentas.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            this.lblVentas.Margin = new Padding(0);
            this.lblVentas.AutoEllipsis = true;
            this.lblVentas.TabStop = false;
            this.lblVentas.TabIndex = 1;
            this.lblVentas.Name = "lblVentas";

            // espacioPanel
            this.espacioPanel.Dock = DockStyle.Top;
            this.espacioPanel.Height = 12;
            this.espacioPanel.TabIndex = 2;
            this.espacioPanel.Name = "espacioPanel";

            // lblActualizado
            this.lblActualizado.Text = "";
            this.lblActualizado.Dock = DockStyle.Bottom;
            this.lblActualizado.TextAlign = ContentAlignment.MiddleLeft;
            this.lblActualizado.ForeColor = Color.FromArgb(75, 85, 99);
            this.lblActualizado.Font = new Font("Segoe UI", 9F);
            this.lblActualizado.Margin = new Padding(0);
            this.lblActualizado.AutoEllipsis = true;
            this.lblActualizado.TabStop = false;
            this.lblActualizado.Height = 28;
            this.lblActualizado.TabIndex = 3;
            this.lblActualizado.Name = "lblActualizado";

            // resumen
            this.resumen.ColumnCount = 2;
            this.resumen.RowCount = 1;
            this.resumen.Dock = DockStyle.Fill;
            this.resumen.Margin = new Padding(0);
            this.resumen.TabIndex = 4;
            this.resumen.Name = "resumen";
            this.resumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 56F));
            this.resumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44F));
            this.resumen.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            // resumenCitas
            this.resumenCitas.Dock = DockStyle.Fill;
            this.resumenCitas.Margin = new Padding(0, 0, 5, 0);
            this.resumenCitas.TabIndex = 0;
            this.resumenCitas.Name = "resumenCitas";

            // resumenAlertas
            this.resumenAlertas.Dock = DockStyle.Fill;
            this.resumenAlertas.Margin = new Padding(5, 0, 0, 0);
            this.resumenAlertas.TabIndex = 1;
            this.resumenAlertas.Name = "resumenAlertas";

            // proximas
            this.proximas.Dock = DockStyle.Fill;
            this.proximas.Padding = new Padding(14, 4, 14, 12);
            this.proximas.BackColor = Color.White;
            this.proximas.Margin = new Padding(0);
            this.proximas.TabIndex = 0;
            this.proximas.Name = "proximas";

            // proximasTitulo
            this.proximasTitulo.Text = "Próximas citas pendientes";
            this.proximasTitulo.Dock = DockStyle.Top;
            this.proximasTitulo.TextAlign = ContentAlignment.MiddleLeft;
            this.proximasTitulo.ForeColor = Color.FromArgb(17, 24, 39);
            this.proximasTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.proximasTitulo.Margin = new Padding(0);
            this.proximasTitulo.AutoEllipsis = true;
            this.proximasTitulo.TabStop = false;
            this.proximasTitulo.Height = 32;
            this.proximasTitulo.TabIndex = 0;
            this.proximasTitulo.Name = "proximasTitulo";

            // gridCitas
            this.gridCitas.Dock = DockStyle.Fill;
            this.gridCitas.Margin = new Padding(0);
            this.gridCitas.AllowUserToAddRows = false;
            this.gridCitas.AllowUserToDeleteRows = false;
            this.gridCitas.AllowUserToResizeRows = false;
            this.gridCitas.AutoGenerateColumns = false;
            this.gridCitas.ReadOnly = true;
            this.gridCitas.MultiSelect = false;
            this.gridCitas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridCitas.RowHeadersVisible = false;
            this.gridCitas.GridColor = Color.FromArgb(219, 227, 238);
            this.gridCitas.BackgroundColor = Color.White;
            this.gridCitas.BorderStyle = BorderStyle.None;
            this.gridCitas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridCitas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.gridCitas.EnableHeadersVisualStyles = false;
            this.gridCitas.ColumnHeadersHeight = 35;
            this.gridCitas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            gridCitasColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(23, 24, 29);
            gridCitasColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gridCitasColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gridCitasColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridCitasColumnHeadersDefaultCellStyle.Padding = new Padding(5, 0, 2, 0);
            this.gridCitas.ColumnHeadersDefaultCellStyle = gridCitasColumnHeadersDefaultCellStyle;
            gridCitasDefaultCellStyle.BackColor = Color.White;
            gridCitasDefaultCellStyle.ForeColor = Color.FromArgb(17, 24, 39);
            gridCitasDefaultCellStyle.SelectionBackColor = Color.FromArgb(37, 99, 235);
            gridCitasDefaultCellStyle.SelectionForeColor = Color.White;
            gridCitasDefaultCellStyle.Padding = new Padding(5, 0, 3, 0);
            this.gridCitas.DefaultCellStyle = gridCitasDefaultCellStyle;
            gridCitasAlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            this.gridCitas.AlternatingRowsDefaultCellStyle = gridCitasAlternatingRowsDefaultCellStyle;
            this.gridCitas.RowTemplate.Height = 30;
            this.gridCitas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridCitas.TabIndex = 1;
            this.gridCitas.Name = "gridCitas";
            this.gridCitas.Columns.AddRange(new DataGridViewColumn[] { this.gridCitasColFecha, this.gridCitasColPlaca, this.gridCitasColCliente, this.gridCitasColServicio, this.gridCitasColEmpleado });

            // gridCitasColFecha
            this.gridCitasColFecha.HeaderText = "Fecha";
            this.gridCitasColFecha.Name = "gridCitasColFecha";
            this.gridCitasColFecha.DataPropertyName = "Fecha";
            this.gridCitasColFecha.MinimumWidth = 84;
            this.gridCitasColFecha.FillWeight = 95F;
            this.gridCitasColFecha.SortMode = DataGridViewColumnSortMode.NotSortable;

            // gridCitasColPlaca
            this.gridCitasColPlaca.HeaderText = "Placa";
            this.gridCitasColPlaca.Name = "gridCitasColPlaca";
            this.gridCitasColPlaca.DataPropertyName = "Placa";
            this.gridCitasColPlaca.MinimumWidth = 58;
            this.gridCitasColPlaca.FillWeight = 71F;
            this.gridCitasColPlaca.SortMode = DataGridViewColumnSortMode.NotSortable;

            // gridCitasColCliente
            this.gridCitasColCliente.HeaderText = "Cliente";
            this.gridCitasColCliente.Name = "gridCitasColCliente";
            this.gridCitasColCliente.DataPropertyName = "Cliente";
            this.gridCitasColCliente.MinimumWidth = 85;
            this.gridCitasColCliente.FillWeight = 111F;
            this.gridCitasColCliente.SortMode = DataGridViewColumnSortMode.NotSortable;

            // gridCitasColServicio
            this.gridCitasColServicio.HeaderText = "Servicio";
            this.gridCitasColServicio.Name = "gridCitasColServicio";
            this.gridCitasColServicio.DataPropertyName = "Servicio";
            this.gridCitasColServicio.MinimumWidth = 90;
            this.gridCitasColServicio.FillWeight = 116F;
            this.gridCitasColServicio.SortMode = DataGridViewColumnSortMode.NotSortable;

            // gridCitasColEmpleado
            this.gridCitasColEmpleado.HeaderText = "Empleado";
            this.gridCitasColEmpleado.Name = "gridCitasColEmpleado";
            this.gridCitasColEmpleado.DataPropertyName = "Empleado";
            this.gridCitasColEmpleado.MinimumWidth = 84;
            this.gridCitasColEmpleado.FillWeight = 107F;
            this.gridCitasColEmpleado.SortMode = DataGridViewColumnSortMode.NotSortable;

            // alertas
            this.alertas.Dock = DockStyle.Fill;
            this.alertas.Padding = new Padding(14, 4, 14, 12);
            this.alertas.BackColor = Color.White;
            this.alertas.Margin = new Padding(0);
            this.alertas.TabIndex = 0;
            this.alertas.Name = "alertas";

            // alertasTitulo
            this.alertasTitulo.Text = "Alertas de stock y SOAT";
            this.alertasTitulo.Dock = DockStyle.Top;
            this.alertasTitulo.TextAlign = ContentAlignment.MiddleLeft;
            this.alertasTitulo.ForeColor = Color.FromArgb(17, 24, 39);
            this.alertasTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.alertasTitulo.Margin = new Padding(0);
            this.alertasTitulo.AutoEllipsis = true;
            this.alertasTitulo.TabStop = false;
            this.alertasTitulo.Height = 32;
            this.alertasTitulo.TabIndex = 0;
            this.alertasTitulo.Name = "alertasTitulo";

            // gridAlertas
            this.gridAlertas.Dock = DockStyle.Fill;
            this.gridAlertas.Margin = new Padding(0);
            this.gridAlertas.AllowUserToAddRows = false;
            this.gridAlertas.AllowUserToDeleteRows = false;
            this.gridAlertas.AllowUserToResizeRows = false;
            this.gridAlertas.AutoGenerateColumns = false;
            this.gridAlertas.ReadOnly = true;
            this.gridAlertas.MultiSelect = false;
            this.gridAlertas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridAlertas.RowHeadersVisible = false;
            this.gridAlertas.GridColor = Color.FromArgb(219, 227, 238);
            this.gridAlertas.BackgroundColor = Color.White;
            this.gridAlertas.BorderStyle = BorderStyle.None;
            this.gridAlertas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridAlertas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.gridAlertas.EnableHeadersVisualStyles = false;
            this.gridAlertas.ColumnHeadersHeight = 35;
            this.gridAlertas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            gridAlertasColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(23, 24, 29);
            gridAlertasColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gridAlertasColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gridAlertasColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridAlertasColumnHeadersDefaultCellStyle.Padding = new Padding(5, 0, 2, 0);
            this.gridAlertas.ColumnHeadersDefaultCellStyle = gridAlertasColumnHeadersDefaultCellStyle;
            gridAlertasDefaultCellStyle.BackColor = Color.White;
            gridAlertasDefaultCellStyle.ForeColor = Color.FromArgb(17, 24, 39);
            gridAlertasDefaultCellStyle.SelectionBackColor = Color.FromArgb(37, 99, 235);
            gridAlertasDefaultCellStyle.SelectionForeColor = Color.White;
            gridAlertasDefaultCellStyle.Padding = new Padding(5, 0, 3, 0);
            this.gridAlertas.DefaultCellStyle = gridAlertasDefaultCellStyle;
            gridAlertasAlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            this.gridAlertas.AlternatingRowsDefaultCellStyle = gridAlertasAlternatingRowsDefaultCellStyle;
            this.gridAlertas.RowTemplate.Height = 30;
            this.gridAlertas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridAlertas.TabIndex = 1;
            this.gridAlertas.Name = "gridAlertas";
            this.gridAlertas.Columns.AddRange(new DataGridViewColumn[] { this.gridAlertasColTipo, this.gridAlertasColDetalle, this.gridAlertasColEstado });

            // gridAlertasColTipo
            this.gridAlertasColTipo.HeaderText = "Tipo";
            this.gridAlertasColTipo.Name = "gridAlertasColTipo";
            this.gridAlertasColTipo.DataPropertyName = "Tipo";
            this.gridAlertasColTipo.MinimumWidth = 84;
            this.gridAlertasColTipo.FillWeight = 85F;
            this.gridAlertasColTipo.SortMode = DataGridViewColumnSortMode.NotSortable;

            // gridAlertasColDetalle
            this.gridAlertasColDetalle.HeaderText = "Detalle";
            this.gridAlertasColDetalle.Name = "gridAlertasColDetalle";
            this.gridAlertasColDetalle.DataPropertyName = "Detalle";
            this.gridAlertasColDetalle.MinimumWidth = 113;
            this.gridAlertasColDetalle.FillWeight = 138F;
            this.gridAlertasColDetalle.SortMode = DataGridViewColumnSortMode.NotSortable;

            // gridAlertasColEstado
            this.gridAlertasColEstado.HeaderText = "Estado";
            this.gridAlertasColEstado.Name = "gridAlertasColEstado";
            this.gridAlertasColEstado.DataPropertyName = "Estado";
            this.gridAlertasColEstado.MinimumWidth = 102;
            this.gridAlertasColEstado.FillWeight = 112F;
            this.gridAlertasColEstado.SortMode = DataGridViewColumnSortMode.NotSortable;

            this.alertas.Controls.Add(this.gridAlertas);
            this.alertas.Controls.Add(this.alertasTitulo);
            this.proximas.Controls.Add(this.gridCitas);
            this.proximas.Controls.Add(this.proximasTitulo);
            this.resumenAlertas.Controls.Add(this.alertas);
            this.resumenCitas.Controls.Add(this.proximas);
            this.resumen.Controls.Add(this.resumenCitas, 0, 0);
            this.resumen.Controls.Add(this.resumenAlertas, 1, 0);
            this.tarjetaVentas.Controls.Add(this.lblVentas);
            this.tarjetaVentas.Controls.Add(this.tituloVentas);
            this.tarjetaCitas.Controls.Add(this.lblCitas);
            this.tarjetaCitas.Controls.Add(this.tituloCitas);
            this.tarjetaArticulos.Controls.Add(this.lblArticulos);
            this.tarjetaArticulos.Controls.Add(this.tituloArticulos);
            this.tarjetaVehiculos.Controls.Add(this.lblVehiculos);
            this.tarjetaVehiculos.Controls.Add(this.tituloVehiculos);
            this.tarjetaClientes.Controls.Add(this.lblClientes);
            this.tarjetaClientes.Controls.Add(this.tituloClientes);
            this.metricas.Controls.Add(this.tarjetaClientes, 0, 0);
            this.metricas.Controls.Add(this.tarjetaVehiculos, 1, 0);
            this.metricas.Controls.Add(this.tarjetaArticulos, 2, 0);
            this.metricas.Controls.Add(this.tarjetaCitas, 3, 0);
            this.metricas.Controls.Add(this.tarjetaVentas, 4, 0);
            this.encabezado.Controls.Add(this.lblTitulo);
            this.encabezado.Controls.Add(this.acentoTitulo);
            this.encabezado.Controls.Add(this.panelLogo);
            this.panelLogo.Controls.Add(this.picLogo);
            this.Controls.Add(this.resumen);
            this.Controls.Add(this.lblActualizado);
            this.Controls.Add(this.espacioPanel);
            this.Controls.Add(this.metricas);
            this.Controls.Add(this.encabezado);

            ((ISupportInitialize)this.gridCitas).EndInit();
            ((ISupportInitialize)this.gridAlertas).EndInit();
            ((ISupportInitialize)this.picLogo).EndInit();
            this.panelLogo.ResumeLayout(false);
            this.alertas.ResumeLayout(false);
            this.proximas.ResumeLayout(false);
            this.resumenAlertas.ResumeLayout(false);
            this.resumenCitas.ResumeLayout(false);
            this.resumen.ResumeLayout(false);
            this.espacioPanel.ResumeLayout(false);
            this.tarjetaVentas.ResumeLayout(false);
            this.tarjetaCitas.ResumeLayout(false);
            this.tarjetaArticulos.ResumeLayout(false);
            this.tarjetaVehiculos.ResumeLayout(false);
            this.tarjetaClientes.ResumeLayout(false);
            this.metricas.ResumeLayout(false);
            this.acentoTitulo.ResumeLayout(false);
            this.encabezado.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Panel encabezado;
        private Label lblTitulo;
        private Panel acentoTitulo;
        private Panel panelLogo;
        private PictureBox picLogo;
        private TableLayoutPanel metricas;
        private Panel tarjetaClientes;
        private Label tituloClientes;
        private Label lblClientes;
        private Panel tarjetaVehiculos;
        private Label tituloVehiculos;
        private Label lblVehiculos;
        private Panel tarjetaArticulos;
        private Label tituloArticulos;
        private Label lblArticulos;
        private Panel tarjetaCitas;
        private Label tituloCitas;
        private Label lblCitas;
        private Panel tarjetaVentas;
        private Label tituloVentas;
        private Label lblVentas;
        private Panel espacioPanel;
        private Label lblActualizado;
        private TableLayoutPanel resumen;
        private Panel resumenCitas;
        private Panel resumenAlertas;
        private Panel proximas;
        private Label proximasTitulo;
        private DataGridView gridCitas;
        private DataGridViewTextBoxColumn gridCitasColFecha;
        private DataGridViewTextBoxColumn gridCitasColPlaca;
        private DataGridViewTextBoxColumn gridCitasColCliente;
        private DataGridViewTextBoxColumn gridCitasColServicio;
        private DataGridViewTextBoxColumn gridCitasColEmpleado;
        private Panel alertas;
        private Label alertasTitulo;
        private DataGridView gridAlertas;
        private DataGridViewTextBoxColumn gridAlertasColTipo;
        private DataGridViewTextBoxColumn gridAlertasColDetalle;
        private DataGridViewTextBoxColumn gridAlertasColEstado;
    }
}
