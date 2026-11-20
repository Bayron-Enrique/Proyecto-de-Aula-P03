using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PRESENTACION
{
    partial class FrmVehiculos
    {
        private IContainer components;
        private ErrorProvider errores;

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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.errores = new System.Windows.Forms.ErrorProvider(this.components);
            this.encabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.acentoTitulo = new System.Windows.Forms.Panel();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabVehiculos = new System.Windows.Forms.TabPage();
            this.listadoVehiculos = new System.Windows.Forms.Panel();
            this.gridVehiculos = new System.Windows.Forms.DataGridView();
            this.gridVehiculosColPlaca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridVehiculosColMarca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridVehiculosColLinea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridVehiculosColModelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridVehiculosColTipoVehiculo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridVehiculosColColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridVehiculosColPropietario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridVehiculosColCedulaPropietario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listadoVehiculosFiltros = new System.Windows.Forms.TableLayoutPanel();
            this.lblTxtBuscarVehiculo = new System.Windows.Forms.Label();
            this.txtBuscarVehiculo = new System.Windows.Forms.TextBox();
            this.listadoVehiculosTitulo = new System.Windows.Forms.Label();
            this.espacioVehiculos = new System.Windows.Forms.Panel();
            this.editorVehiculo = new System.Windows.Forms.Panel();
            this.editorVehiculoAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGuardarVehiculo = new System.Windows.Forms.Button();
            this.btnNuevoVehiculo = new System.Windows.Forms.Button();
            this.btnEliminarVehiculo = new System.Windows.Forms.Button();
            this.editorVehiculoCampos = new System.Windows.Forms.TableLayoutPanel();
            this.lblTxtPlaca = new System.Windows.Forms.Label();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.lblTxtMarca = new System.Windows.Forms.Label();
            this.txtMarca = new System.Windows.Forms.TextBox();
            this.lblTxtLinea = new System.Windows.Forms.Label();
            this.txtLinea = new System.Windows.Forms.TextBox();
            this.lblTxtModelo = new System.Windows.Forms.Label();
            this.txtModelo = new System.Windows.Forms.TextBox();
            this.lblCmbTipoVehiculo = new System.Windows.Forms.Label();
            this.cmbTipoVehiculo = new System.Windows.Forms.ComboBox();
            this.lblTxtColor = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.lblCmbPropietario = new System.Windows.Forms.Label();
            this.cmbPropietario = new System.Windows.Forms.ComboBox();
            this.editorVehiculoTitulo = new System.Windows.Forms.Label();
            this.tabSoat = new System.Windows.Forms.TabPage();
            this.listadoSoat = new System.Windows.Forms.Panel();
            this.gridSoat = new System.Windows.Forms.DataGridView();
            this.gridSoatColSoatId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridSoatColNumeroSoat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridSoatColPlaca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridSoatColVehiculo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridSoatColPropietario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridSoatColFechaInicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridSoatColFechaFin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridSoatColEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listadoSoatTitulo = new System.Windows.Forms.Label();
            this.espacioSoat = new System.Windows.Forms.Panel();
            this.editorSoat = new System.Windows.Forms.Panel();
            this.editorSoatAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGuardarSoat = new System.Windows.Forms.Button();
            this.btnNuevoSoat = new System.Windows.Forms.Button();
            this.btnEliminarSoat = new System.Windows.Forms.Button();
            this.editorSoatCampos = new System.Windows.Forms.TableLayoutPanel();
            this.lblCmbPlacaSoat = new System.Windows.Forms.Label();
            this.cmbPlacaSoat = new System.Windows.Forms.ComboBox();
            this.lblTxtNumeroSoat = new System.Windows.Forms.Label();
            this.txtNumeroSoat = new System.Windows.Forms.TextBox();
            this.lblDtpFechaInicio = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.lblDtpFechaFin = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.editorSoatTitulo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errores)).BeginInit();
            this.encabezado.SuspendLayout();
            this.tabs.SuspendLayout();
            this.tabVehiculos.SuspendLayout();
            this.listadoVehiculos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridVehiculos)).BeginInit();
            this.listadoVehiculosFiltros.SuspendLayout();
            this.editorVehiculo.SuspendLayout();
            this.editorVehiculoAcciones.SuspendLayout();
            this.editorVehiculoCampos.SuspendLayout();
            this.tabSoat.SuspendLayout();
            this.listadoSoat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSoat)).BeginInit();
            this.editorSoat.SuspendLayout();
            this.editorSoatAcciones.SuspendLayout();
            this.editorSoatCampos.SuspendLayout();
            this.SuspendLayout();
            // 
            // errores
            // 
            this.errores.ContainerControl = this;
            // 
            // encabezado
            // 
            this.encabezado.Controls.Add(this.lblTitulo);
            this.encabezado.Controls.Add(this.acentoTitulo);
            this.encabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.encabezado.Location = new System.Drawing.Point(20, 8);
            this.encabezado.Margin = new System.Windows.Forms.Padding(0);
            this.encabezado.Name = "encabezado";
            this.encabezado.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.encabezado.Size = new System.Drawing.Size(1080, 57);
            this.encabezado.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoEllipsis = true;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 23F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblTitulo.Location = new System.Drawing.Point(4, 10);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.lblTitulo.Size = new System.Drawing.Size(1076, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Vehículos y SOAT";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // acentoTitulo
            // 
            this.acentoTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.acentoTitulo.Dock = System.Windows.Forms.DockStyle.Left;
            this.acentoTitulo.Location = new System.Drawing.Point(0, 10);
            this.acentoTitulo.Name = "acentoTitulo";
            this.acentoTitulo.Size = new System.Drawing.Size(4, 37);
            this.acentoTitulo.TabIndex = 1;
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabVehiculos);
            this.tabs.Controls.Add(this.tabSoat);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.ItemSize = new System.Drawing.Size(150, 31);
            this.tabs.Location = new System.Drawing.Point(20, 65);
            this.tabs.Name = "tabs";
            this.tabs.Padding = new System.Drawing.Point(13, 5);
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(1080, 599);
            this.tabs.TabIndex = 1;
            // 
            // tabVehiculos
            // 
            this.tabVehiculos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.tabVehiculos.Controls.Add(this.listadoVehiculos);
            this.tabVehiculos.Controls.Add(this.espacioVehiculos);
            this.tabVehiculos.Controls.Add(this.editorVehiculo);
            this.tabVehiculos.Location = new System.Drawing.Point(4, 35);
            this.tabVehiculos.Name = "tabVehiculos";
            this.tabVehiculos.Padding = new System.Windows.Forms.Padding(12);
            this.tabVehiculos.Size = new System.Drawing.Size(1072, 560);
            this.tabVehiculos.TabIndex = 0;
            this.tabVehiculos.Text = "Vehículos";
            // 
            // listadoVehiculos
            // 
            this.listadoVehiculos.BackColor = System.Drawing.Color.White;
            this.listadoVehiculos.Controls.Add(this.gridVehiculos);
            this.listadoVehiculos.Controls.Add(this.listadoVehiculosFiltros);
            this.listadoVehiculos.Controls.Add(this.listadoVehiculosTitulo);
            this.listadoVehiculos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listadoVehiculos.Location = new System.Drawing.Point(12, 216);
            this.listadoVehiculos.Margin = new System.Windows.Forms.Padding(0);
            this.listadoVehiculos.Name = "listadoVehiculos";
            this.listadoVehiculos.Padding = new System.Windows.Forms.Padding(14, 4, 14, 12);
            this.listadoVehiculos.Size = new System.Drawing.Size(1048, 332);
            this.listadoVehiculos.TabIndex = 2;
            // 
            // gridVehiculos
            // 
            this.gridVehiculos.AllowUserToAddRows = false;
            this.gridVehiculos.AllowUserToDeleteRows = false;
            this.gridVehiculos.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.gridVehiculos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridVehiculos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridVehiculos.BackgroundColor = System.Drawing.Color.White;
            this.gridVehiculos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridVehiculos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridVehiculos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5, 0, 2, 0);
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridVehiculos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridVehiculos.ColumnHeadersHeight = 35;
            this.gridVehiculos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridVehiculos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gridVehiculosColPlaca,
            this.gridVehiculosColMarca,
            this.gridVehiculosColLinea,
            this.gridVehiculosColModelo,
            this.gridVehiculosColTipoVehiculo,
            this.gridVehiculosColColor,
            this.gridVehiculosColPropietario,
            this.gridVehiculosColCedulaPropietario});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(5, 0, 3, 0);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridVehiculos.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridVehiculos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridVehiculos.EnableHeadersVisualStyles = false;
            this.gridVehiculos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(227)))), ((int)(((byte)(238)))));
            this.gridVehiculos.Location = new System.Drawing.Point(14, 82);
            this.gridVehiculos.Margin = new System.Windows.Forms.Padding(0);
            this.gridVehiculos.MultiSelect = false;
            this.gridVehiculos.Name = "gridVehiculos";
            this.gridVehiculos.ReadOnly = true;
            this.gridVehiculos.RowHeadersVisible = false;
            this.gridVehiculos.RowTemplate.Height = 30;
            this.gridVehiculos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridVehiculos.Size = new System.Drawing.Size(1020, 238);
            this.gridVehiculos.TabIndex = 2;
            // 
            // gridVehiculosColPlaca
            // 
            this.gridVehiculosColPlaca.DataPropertyName = "Placa";
            this.gridVehiculosColPlaca.FillWeight = 74F;
            this.gridVehiculosColPlaca.HeaderText = "Placa";
            this.gridVehiculosColPlaca.MinimumWidth = 69;
            this.gridVehiculosColPlaca.Name = "gridVehiculosColPlaca";
            this.gridVehiculosColPlaca.ReadOnly = true;
            this.gridVehiculosColPlaca.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridVehiculosColMarca
            // 
            this.gridVehiculosColMarca.DataPropertyName = "Marca";
            this.gridVehiculosColMarca.FillWeight = 92F;
            this.gridVehiculosColMarca.HeaderText = "Marca";
            this.gridVehiculosColMarca.MinimumWidth = 70;
            this.gridVehiculosColMarca.Name = "gridVehiculosColMarca";
            this.gridVehiculosColMarca.ReadOnly = true;
            this.gridVehiculosColMarca.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridVehiculosColLinea
            // 
            this.gridVehiculosColLinea.DataPropertyName = "Linea";
            this.gridVehiculosColLinea.FillWeight = 99F;
            this.gridVehiculosColLinea.HeaderText = "Línea";
            this.gridVehiculosColLinea.MinimumWidth = 67;
            this.gridVehiculosColLinea.Name = "gridVehiculosColLinea";
            this.gridVehiculosColLinea.ReadOnly = true;
            this.gridVehiculosColLinea.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridVehiculosColModelo
            // 
            this.gridVehiculosColModelo.DataPropertyName = "Modelo";
            this.gridVehiculosColModelo.FillWeight = 88F;
            this.gridVehiculosColModelo.HeaderText = "Modelo / año";
            this.gridVehiculosColModelo.MinimumWidth = 83;
            this.gridVehiculosColModelo.Name = "gridVehiculosColModelo";
            this.gridVehiculosColModelo.ReadOnly = true;
            this.gridVehiculosColModelo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridVehiculosColTipoVehiculo
            // 
            this.gridVehiculosColTipoVehiculo.DataPropertyName = "TipoVehiculo";
            this.gridVehiculosColTipoVehiculo.FillWeight = 103F;
            this.gridVehiculosColTipoVehiculo.HeaderText = "Tipo";
            this.gridVehiculosColTipoVehiculo.MinimumWidth = 86;
            this.gridVehiculosColTipoVehiculo.Name = "gridVehiculosColTipoVehiculo";
            this.gridVehiculosColTipoVehiculo.ReadOnly = true;
            this.gridVehiculosColTipoVehiculo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridVehiculosColColor
            // 
            this.gridVehiculosColColor.DataPropertyName = "Color";
            this.gridVehiculosColColor.FillWeight = 70F;
            this.gridVehiculosColColor.HeaderText = "Color";
            this.gridVehiculosColColor.MinimumWidth = 65;
            this.gridVehiculosColColor.Name = "gridVehiculosColColor";
            this.gridVehiculosColColor.ReadOnly = true;
            this.gridVehiculosColColor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridVehiculosColPropietario
            // 
            this.gridVehiculosColPropietario.DataPropertyName = "Propietario";
            this.gridVehiculosColPropietario.FillWeight = 128F;
            this.gridVehiculosColPropietario.HeaderText = "Propietario";
            this.gridVehiculosColPropietario.MinimumWidth = 109;
            this.gridVehiculosColPropietario.Name = "gridVehiculosColPropietario";
            this.gridVehiculosColPropietario.ReadOnly = true;
            this.gridVehiculosColPropietario.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridVehiculosColCedulaPropietario
            // 
            this.gridVehiculosColCedulaPropietario.DataPropertyName = "CedulaPropietario";
            this.gridVehiculosColCedulaPropietario.HeaderText = "Cédula";
            this.gridVehiculosColCedulaPropietario.MinimumWidth = 83;
            this.gridVehiculosColCedulaPropietario.Name = "gridVehiculosColCedulaPropietario";
            this.gridVehiculosColCedulaPropietario.ReadOnly = true;
            this.gridVehiculosColCedulaPropietario.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // listadoVehiculosFiltros
            // 
            this.listadoVehiculosFiltros.ColumnCount = 2;
            this.listadoVehiculosFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.listadoVehiculosFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.listadoVehiculosFiltros.Controls.Add(this.lblTxtBuscarVehiculo, 0, 0);
            this.listadoVehiculosFiltros.Controls.Add(this.txtBuscarVehiculo, 0, 1);
            this.listadoVehiculosFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.listadoVehiculosFiltros.Location = new System.Drawing.Point(14, 36);
            this.listadoVehiculosFiltros.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.listadoVehiculosFiltros.Name = "listadoVehiculosFiltros";
            this.listadoVehiculosFiltros.RowCount = 2;
            this.listadoVehiculosFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.listadoVehiculosFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.listadoVehiculosFiltros.Size = new System.Drawing.Size(1020, 46);
            this.listadoVehiculosFiltros.TabIndex = 1;
            // 
            // lblTxtBuscarVehiculo
            // 
            this.lblTxtBuscarVehiculo.AutoEllipsis = true;
            this.lblTxtBuscarVehiculo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtBuscarVehiculo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtBuscarVehiculo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtBuscarVehiculo.Location = new System.Drawing.Point(6, 0);
            this.lblTxtBuscarVehiculo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtBuscarVehiculo.Name = "lblTxtBuscarVehiculo";
            this.lblTxtBuscarVehiculo.Size = new System.Drawing.Size(600, 17);
            this.lblTxtBuscarVehiculo.TabIndex = 0;
            this.lblTxtBuscarVehiculo.Text = "Buscar por placa o cédula del propietario";
            this.lblTxtBuscarVehiculo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBuscarVehiculo
            // 
            this.txtBuscarVehiculo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtBuscarVehiculo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscarVehiculo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBuscarVehiculo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtBuscarVehiculo.Location = new System.Drawing.Point(6, 20);
            this.txtBuscarVehiculo.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtBuscarVehiculo.MaxLength = 100;
            this.txtBuscarVehiculo.Name = "txtBuscarVehiculo";
            this.txtBuscarVehiculo.Size = new System.Drawing.Size(600, 23);
            this.txtBuscarVehiculo.TabIndex = 1;
            // 
            // listadoVehiculosTitulo
            // 
            this.listadoVehiculosTitulo.AutoEllipsis = true;
            this.listadoVehiculosTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.listadoVehiculosTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.listadoVehiculosTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.listadoVehiculosTitulo.Location = new System.Drawing.Point(14, 4);
            this.listadoVehiculosTitulo.Margin = new System.Windows.Forms.Padding(0);
            this.listadoVehiculosTitulo.Name = "listadoVehiculosTitulo";
            this.listadoVehiculosTitulo.Size = new System.Drawing.Size(1020, 32);
            this.listadoVehiculosTitulo.TabIndex = 0;
            this.listadoVehiculosTitulo.Text = "Vehículos registrados";
            this.listadoVehiculosTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // espacioVehiculos
            // 
            this.espacioVehiculos.Dock = System.Windows.Forms.DockStyle.Top;
            this.espacioVehiculos.Location = new System.Drawing.Point(12, 206);
            this.espacioVehiculos.Name = "espacioVehiculos";
            this.espacioVehiculos.Size = new System.Drawing.Size(1048, 10);
            this.espacioVehiculos.TabIndex = 1;
            // 
            // editorVehiculo
            // 
            this.editorVehiculo.AutoSize = true;
            this.editorVehiculo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editorVehiculo.BackColor = System.Drawing.Color.White;
            this.editorVehiculo.Controls.Add(this.editorVehiculoAcciones);
            this.editorVehiculo.Controls.Add(this.editorVehiculoCampos);
            this.editorVehiculo.Controls.Add(this.editorVehiculoTitulo);
            this.editorVehiculo.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorVehiculo.Location = new System.Drawing.Point(12, 12);
            this.editorVehiculo.Margin = new System.Windows.Forms.Padding(0);
            this.editorVehiculo.MinimumSize = new System.Drawing.Size(0, 194);
            this.editorVehiculo.Name = "editorVehiculo";
            this.editorVehiculo.Padding = new System.Windows.Forms.Padding(14, 4, 14, 10);
            this.editorVehiculo.Size = new System.Drawing.Size(1048, 194);
            this.editorVehiculo.TabIndex = 0;
            // 
            // editorVehiculoAcciones
            // 
            this.editorVehiculoAcciones.AutoSize = true;
            this.editorVehiculoAcciones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editorVehiculoAcciones.Controls.Add(this.btnGuardarVehiculo);
            this.editorVehiculoAcciones.Controls.Add(this.btnNuevoVehiculo);
            this.editorVehiculoAcciones.Controls.Add(this.btnEliminarVehiculo);
            this.editorVehiculoAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorVehiculoAcciones.Location = new System.Drawing.Point(14, 138);
            this.editorVehiculoAcciones.Name = "editorVehiculoAcciones";
            this.editorVehiculoAcciones.Padding = new System.Windows.Forms.Padding(6, 6, 0, 0);
            this.editorVehiculoAcciones.Size = new System.Drawing.Size(1020, 44);
            this.editorVehiculoAcciones.TabIndex = 2;
            // 
            // btnGuardarVehiculo
            // 
            this.btnGuardarVehiculo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnGuardarVehiculo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarVehiculo.FlatAppearance.BorderSize = 0;
            this.btnGuardarVehiculo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnGuardarVehiculo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarVehiculo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGuardarVehiculo.ForeColor = System.Drawing.Color.White;
            this.btnGuardarVehiculo.Location = new System.Drawing.Point(6, 9);
            this.btnGuardarVehiculo.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnGuardarVehiculo.Name = "btnGuardarVehiculo";
            this.btnGuardarVehiculo.Size = new System.Drawing.Size(112, 32);
            this.btnGuardarVehiculo.TabIndex = 0;
            this.btnGuardarVehiculo.Text = "Guardar";
            this.btnGuardarVehiculo.UseVisualStyleBackColor = false;
            // 
            // btnNuevoVehiculo
            // 
            this.btnNuevoVehiculo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.btnNuevoVehiculo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoVehiculo.FlatAppearance.BorderSize = 0;
            this.btnNuevoVehiculo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.btnNuevoVehiculo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoVehiculo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevoVehiculo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.btnNuevoVehiculo.Location = new System.Drawing.Point(126, 9);
            this.btnNuevoVehiculo.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnNuevoVehiculo.Name = "btnNuevoVehiculo";
            this.btnNuevoVehiculo.Size = new System.Drawing.Size(86, 32);
            this.btnNuevoVehiculo.TabIndex = 1;
            this.btnNuevoVehiculo.Text = "Nuevo";
            this.btnNuevoVehiculo.UseVisualStyleBackColor = false;
            // 
            // btnEliminarVehiculo
            // 
            this.btnEliminarVehiculo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnEliminarVehiculo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarVehiculo.FlatAppearance.BorderSize = 0;
            this.btnEliminarVehiculo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnEliminarVehiculo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarVehiculo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEliminarVehiculo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.btnEliminarVehiculo.Location = new System.Drawing.Point(220, 9);
            this.btnEliminarVehiculo.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnEliminarVehiculo.Name = "btnEliminarVehiculo";
            this.btnEliminarVehiculo.Size = new System.Drawing.Size(90, 32);
            this.btnEliminarVehiculo.TabIndex = 2;
            this.btnEliminarVehiculo.Text = "Eliminar";
            this.btnEliminarVehiculo.UseVisualStyleBackColor = false;
            // 
            // editorVehiculoCampos
            // 
            this.editorVehiculoCampos.ColumnCount = 4;
            this.editorVehiculoCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.editorVehiculoCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.editorVehiculoCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.editorVehiculoCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.editorVehiculoCampos.Controls.Add(this.lblTxtPlaca, 0, 0);
            this.editorVehiculoCampos.Controls.Add(this.txtPlaca, 0, 1);
            this.editorVehiculoCampos.Controls.Add(this.lblTxtMarca, 1, 0);
            this.editorVehiculoCampos.Controls.Add(this.txtMarca, 1, 1);
            this.editorVehiculoCampos.Controls.Add(this.lblTxtLinea, 2, 0);
            this.editorVehiculoCampos.Controls.Add(this.txtLinea, 2, 1);
            this.editorVehiculoCampos.Controls.Add(this.lblTxtModelo, 3, 0);
            this.editorVehiculoCampos.Controls.Add(this.txtModelo, 3, 1);
            this.editorVehiculoCampos.Controls.Add(this.lblCmbTipoVehiculo, 0, 2);
            this.editorVehiculoCampos.Controls.Add(this.cmbTipoVehiculo, 0, 3);
            this.editorVehiculoCampos.Controls.Add(this.lblTxtColor, 1, 2);
            this.editorVehiculoCampos.Controls.Add(this.txtColor, 1, 3);
            this.editorVehiculoCampos.Controls.Add(this.lblCmbPropietario, 2, 2);
            this.editorVehiculoCampos.Controls.Add(this.cmbPropietario, 2, 3);
            this.editorVehiculoCampos.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorVehiculoCampos.Location = new System.Drawing.Point(14, 36);
            this.editorVehiculoCampos.Margin = new System.Windows.Forms.Padding(0);
            this.editorVehiculoCampos.Name = "editorVehiculoCampos";
            this.editorVehiculoCampos.RowCount = 4;
            this.editorVehiculoCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.editorVehiculoCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.editorVehiculoCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.editorVehiculoCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.editorVehiculoCampos.Size = new System.Drawing.Size(1020, 102);
            this.editorVehiculoCampos.TabIndex = 1;
            // 
            // lblTxtPlaca
            // 
            this.lblTxtPlaca.AutoEllipsis = true;
            this.lblTxtPlaca.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtPlaca.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtPlaca.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtPlaca.Location = new System.Drawing.Point(6, 0);
            this.lblTxtPlaca.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtPlaca.Name = "lblTxtPlaca";
            this.lblTxtPlaca.Size = new System.Drawing.Size(243, 20);
            this.lblTxtPlaca.TabIndex = 0;
            this.lblTxtPlaca.Text = "Placa";
            this.lblTxtPlaca.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPlaca
            // 
            this.txtPlaca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtPlaca.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPlaca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlaca.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPlaca.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtPlaca.Location = new System.Drawing.Point(6, 23);
            this.txtPlaca.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtPlaca.MaxLength = 7;
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(243, 23);
            this.txtPlaca.TabIndex = 1;
            // 
            // lblTxtMarca
            // 
            this.lblTxtMarca.AutoEllipsis = true;
            this.lblTxtMarca.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtMarca.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtMarca.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtMarca.Location = new System.Drawing.Point(261, 0);
            this.lblTxtMarca.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtMarca.Name = "lblTxtMarca";
            this.lblTxtMarca.Size = new System.Drawing.Size(243, 20);
            this.lblTxtMarca.TabIndex = 2;
            this.lblTxtMarca.Text = "Marca";
            this.lblTxtMarca.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMarca
            // 
            this.txtMarca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtMarca.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMarca.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMarca.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtMarca.Location = new System.Drawing.Point(261, 23);
            this.txtMarca.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtMarca.MaxLength = 50;
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.Size = new System.Drawing.Size(243, 23);
            this.txtMarca.TabIndex = 3;
            // 
            // lblTxtLinea
            // 
            this.lblTxtLinea.AutoEllipsis = true;
            this.lblTxtLinea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtLinea.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtLinea.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtLinea.Location = new System.Drawing.Point(516, 0);
            this.lblTxtLinea.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtLinea.Name = "lblTxtLinea";
            this.lblTxtLinea.Size = new System.Drawing.Size(243, 20);
            this.lblTxtLinea.TabIndex = 4;
            this.lblTxtLinea.Text = "Línea";
            this.lblTxtLinea.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLinea
            // 
            this.txtLinea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtLinea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLinea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLinea.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtLinea.Location = new System.Drawing.Point(516, 23);
            this.txtLinea.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtLinea.MaxLength = 50;
            this.txtLinea.Name = "txtLinea";
            this.txtLinea.Size = new System.Drawing.Size(243, 23);
            this.txtLinea.TabIndex = 5;
            // 
            // lblTxtModelo
            // 
            this.lblTxtModelo.AutoEllipsis = true;
            this.lblTxtModelo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtModelo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtModelo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtModelo.Location = new System.Drawing.Point(771, 0);
            this.lblTxtModelo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtModelo.Name = "lblTxtModelo";
            this.lblTxtModelo.Size = new System.Drawing.Size(243, 20);
            this.lblTxtModelo.TabIndex = 6;
            this.lblTxtModelo.Text = "Modelo / año";
            this.lblTxtModelo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtModelo
            // 
            this.txtModelo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtModelo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtModelo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtModelo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtModelo.Location = new System.Drawing.Point(771, 23);
            this.txtModelo.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtModelo.MaxLength = 4;
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.Size = new System.Drawing.Size(243, 23);
            this.txtModelo.TabIndex = 7;
            // 
            // lblCmbTipoVehiculo
            // 
            this.lblCmbTipoVehiculo.AutoEllipsis = true;
            this.lblCmbTipoVehiculo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCmbTipoVehiculo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCmbTipoVehiculo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCmbTipoVehiculo.Location = new System.Drawing.Point(6, 51);
            this.lblCmbTipoVehiculo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCmbTipoVehiculo.Name = "lblCmbTipoVehiculo";
            this.lblCmbTipoVehiculo.Size = new System.Drawing.Size(243, 20);
            this.lblCmbTipoVehiculo.TabIndex = 8;
            this.lblCmbTipoVehiculo.Text = "Tipo";
            this.lblCmbTipoVehiculo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbTipoVehiculo
            // 
            this.cmbTipoVehiculo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.cmbTipoVehiculo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTipoVehiculo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoVehiculo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.cmbTipoVehiculo.Items.AddRange(new object[] {
            "Automovil",
            "Motocicleta"});
            this.cmbTipoVehiculo.Location = new System.Drawing.Point(6, 74);
            this.cmbTipoVehiculo.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbTipoVehiculo.Name = "cmbTipoVehiculo";
            this.cmbTipoVehiculo.Size = new System.Drawing.Size(243, 23);
            this.cmbTipoVehiculo.TabIndex = 9;
            // 
            // lblTxtColor
            // 
            this.lblTxtColor.AutoEllipsis = true;
            this.lblTxtColor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtColor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtColor.Location = new System.Drawing.Point(261, 51);
            this.lblTxtColor.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtColor.Name = "lblTxtColor";
            this.lblTxtColor.Size = new System.Drawing.Size(243, 20);
            this.lblTxtColor.TabIndex = 10;
            this.lblTxtColor.Text = "Color";
            this.lblTxtColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtColor
            // 
            this.txtColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtColor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtColor.Location = new System.Drawing.Point(261, 74);
            this.txtColor.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtColor.MaxLength = 50;
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(243, 23);
            this.txtColor.TabIndex = 11;
            // 
            // lblCmbPropietario
            // 
            this.lblCmbPropietario.AutoEllipsis = true;
            this.editorVehiculoCampos.SetColumnSpan(this.lblCmbPropietario, 2);
            this.lblCmbPropietario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCmbPropietario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCmbPropietario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCmbPropietario.Location = new System.Drawing.Point(516, 51);
            this.lblCmbPropietario.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCmbPropietario.Name = "lblCmbPropietario";
            this.lblCmbPropietario.Size = new System.Drawing.Size(498, 20);
            this.lblCmbPropietario.TabIndex = 12;
            this.lblCmbPropietario.Text = "Propietario";
            this.lblCmbPropietario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbPropietario
            // 
            this.cmbPropietario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.editorVehiculoCampos.SetColumnSpan(this.cmbPropietario, 2);
            this.cmbPropietario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbPropietario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPropietario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.cmbPropietario.Location = new System.Drawing.Point(516, 74);
            this.cmbPropietario.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbPropietario.Name = "cmbPropietario";
            this.cmbPropietario.Size = new System.Drawing.Size(498, 23);
            this.cmbPropietario.TabIndex = 13;
            // 
            // editorVehiculoTitulo
            // 
            this.editorVehiculoTitulo.AutoEllipsis = true;
            this.editorVehiculoTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorVehiculoTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.editorVehiculoTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.editorVehiculoTitulo.Location = new System.Drawing.Point(14, 4);
            this.editorVehiculoTitulo.Margin = new System.Windows.Forms.Padding(0);
            this.editorVehiculoTitulo.Name = "editorVehiculoTitulo";
            this.editorVehiculoTitulo.Size = new System.Drawing.Size(1020, 32);
            this.editorVehiculoTitulo.TabIndex = 0;
            this.editorVehiculoTitulo.Text = "Datos del vehículo";
            this.editorVehiculoTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabSoat
            // 
            this.tabSoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.tabSoat.Controls.Add(this.listadoSoat);
            this.tabSoat.Controls.Add(this.espacioSoat);
            this.tabSoat.Controls.Add(this.editorSoat);
            this.tabSoat.Location = new System.Drawing.Point(4, 35);
            this.tabSoat.Name = "tabSoat";
            this.tabSoat.Padding = new System.Windows.Forms.Padding(12);
            this.tabSoat.Size = new System.Drawing.Size(1072, 560);
            this.tabSoat.TabIndex = 1;
            this.tabSoat.Text = "SOAT";
            // 
            // listadoSoat
            // 
            this.listadoSoat.BackColor = System.Drawing.Color.White;
            this.listadoSoat.Controls.Add(this.gridSoat);
            this.listadoSoat.Controls.Add(this.listadoSoatTitulo);
            this.listadoSoat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listadoSoat.Location = new System.Drawing.Point(12, 165);
            this.listadoSoat.Margin = new System.Windows.Forms.Padding(0);
            this.listadoSoat.Name = "listadoSoat";
            this.listadoSoat.Padding = new System.Windows.Forms.Padding(14, 4, 14, 12);
            this.listadoSoat.Size = new System.Drawing.Size(1048, 383);
            this.listadoSoat.TabIndex = 2;
            // 
            // gridSoat
            // 
            this.gridSoat.AllowUserToAddRows = false;
            this.gridSoat.AllowUserToDeleteRows = false;
            this.gridSoat.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.gridSoat.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gridSoat.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridSoat.BackgroundColor = System.Drawing.Color.White;
            this.gridSoat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridSoat.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridSoat.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(5, 0, 2, 0);
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridSoat.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gridSoat.ColumnHeadersHeight = 35;
            this.gridSoat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridSoat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gridSoatColSoatId,
            this.gridSoatColNumeroSoat,
            this.gridSoatColPlaca,
            this.gridSoatColVehiculo,
            this.gridSoatColPropietario,
            this.gridSoatColFechaInicio,
            this.gridSoatColFechaFin,
            this.gridSoatColEstado});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(5, 0, 3, 0);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridSoat.DefaultCellStyle = dataGridViewCellStyle6;
            this.gridSoat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSoat.EnableHeadersVisualStyles = false;
            this.gridSoat.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(227)))), ((int)(((byte)(238)))));
            this.gridSoat.Location = new System.Drawing.Point(14, 36);
            this.gridSoat.Margin = new System.Windows.Forms.Padding(0);
            this.gridSoat.MultiSelect = false;
            this.gridSoat.Name = "gridSoat";
            this.gridSoat.ReadOnly = true;
            this.gridSoat.RowHeadersVisible = false;
            this.gridSoat.RowTemplate.Height = 30;
            this.gridSoat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridSoat.Size = new System.Drawing.Size(1020, 335);
            this.gridSoat.TabIndex = 1;
            // 
            // gridSoatColSoatId
            // 
            this.gridSoatColSoatId.DataPropertyName = "SoatId";
            this.gridSoatColSoatId.FillWeight = 47F;
            this.gridSoatColSoatId.HeaderText = "ID";
            this.gridSoatColSoatId.MinimumWidth = 44;
            this.gridSoatColSoatId.Name = "gridSoatColSoatId";
            this.gridSoatColSoatId.ReadOnly = true;
            this.gridSoatColSoatId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridSoatColNumeroSoat
            // 
            this.gridSoatColNumeroSoat.DataPropertyName = "NumeroSoat";
            this.gridSoatColNumeroSoat.FillWeight = 109F;
            this.gridSoatColNumeroSoat.HeaderText = "Número SOAT";
            this.gridSoatColNumeroSoat.MinimumWidth = 91;
            this.gridSoatColNumeroSoat.Name = "gridSoatColNumeroSoat";
            this.gridSoatColNumeroSoat.ReadOnly = true;
            this.gridSoatColNumeroSoat.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridSoatColPlaca
            // 
            this.gridSoatColPlaca.DataPropertyName = "Placa";
            this.gridSoatColPlaca.FillWeight = 78F;
            this.gridSoatColPlaca.HeaderText = "Placa";
            this.gridSoatColPlaca.MinimumWidth = 69;
            this.gridSoatColPlaca.Name = "gridSoatColPlaca";
            this.gridSoatColPlaca.ReadOnly = true;
            this.gridSoatColPlaca.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridSoatColVehiculo
            // 
            this.gridSoatColVehiculo.DataPropertyName = "Vehiculo";
            this.gridSoatColVehiculo.FillWeight = 120F;
            this.gridSoatColVehiculo.HeaderText = "Marca / línea";
            this.gridSoatColVehiculo.MinimumWidth = 93;
            this.gridSoatColVehiculo.Name = "gridSoatColVehiculo";
            this.gridSoatColVehiculo.ReadOnly = true;
            this.gridSoatColVehiculo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridSoatColPropietario
            // 
            this.gridSoatColPropietario.DataPropertyName = "Propietario";
            this.gridSoatColPropietario.FillWeight = 128F;
            this.gridSoatColPropietario.HeaderText = "Propietario";
            this.gridSoatColPropietario.MinimumWidth = 108;
            this.gridSoatColPropietario.Name = "gridSoatColPropietario";
            this.gridSoatColPropietario.ReadOnly = true;
            this.gridSoatColPropietario.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridSoatColFechaInicio
            // 
            this.gridSoatColFechaInicio.DataPropertyName = "FechaInicio";
            this.gridSoatColFechaInicio.HeaderText = "Fecha de inicio";
            this.gridSoatColFechaInicio.MinimumWidth = 90;
            this.gridSoatColFechaInicio.Name = "gridSoatColFechaInicio";
            this.gridSoatColFechaInicio.ReadOnly = true;
            this.gridSoatColFechaInicio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridSoatColFechaFin
            // 
            this.gridSoatColFechaFin.DataPropertyName = "FechaFin";
            this.gridSoatColFechaFin.HeaderText = "Vencimiento";
            this.gridSoatColFechaFin.MinimumWidth = 90;
            this.gridSoatColFechaFin.Name = "gridSoatColFechaFin";
            this.gridSoatColFechaFin.ReadOnly = true;
            this.gridSoatColFechaFin.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridSoatColEstado
            // 
            this.gridSoatColEstado.DataPropertyName = "Estado";
            this.gridSoatColEstado.FillWeight = 104F;
            this.gridSoatColEstado.HeaderText = "Estado";
            this.gridSoatColEstado.MinimumWidth = 99;
            this.gridSoatColEstado.Name = "gridSoatColEstado";
            this.gridSoatColEstado.ReadOnly = true;
            this.gridSoatColEstado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // listadoSoatTitulo
            // 
            this.listadoSoatTitulo.AutoEllipsis = true;
            this.listadoSoatTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.listadoSoatTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.listadoSoatTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.listadoSoatTitulo.Location = new System.Drawing.Point(14, 4);
            this.listadoSoatTitulo.Margin = new System.Windows.Forms.Padding(0);
            this.listadoSoatTitulo.Name = "listadoSoatTitulo";
            this.listadoSoatTitulo.Size = new System.Drawing.Size(1020, 32);
            this.listadoSoatTitulo.TabIndex = 0;
            this.listadoSoatTitulo.Text = "Pólizas SOAT registradas";
            this.listadoSoatTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // espacioSoat
            // 
            this.espacioSoat.Dock = System.Windows.Forms.DockStyle.Top;
            this.espacioSoat.Location = new System.Drawing.Point(12, 155);
            this.espacioSoat.Name = "espacioSoat";
            this.espacioSoat.Size = new System.Drawing.Size(1048, 10);
            this.espacioSoat.TabIndex = 1;
            // 
            // editorSoat
            // 
            this.editorSoat.AutoSize = true;
            this.editorSoat.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editorSoat.BackColor = System.Drawing.Color.White;
            this.editorSoat.Controls.Add(this.editorSoatAcciones);
            this.editorSoat.Controls.Add(this.editorSoatCampos);
            this.editorSoat.Controls.Add(this.editorSoatTitulo);
            this.editorSoat.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorSoat.Location = new System.Drawing.Point(12, 12);
            this.editorSoat.Margin = new System.Windows.Forms.Padding(0);
            this.editorSoat.MinimumSize = new System.Drawing.Size(0, 143);
            this.editorSoat.Name = "editorSoat";
            this.editorSoat.Padding = new System.Windows.Forms.Padding(14, 4, 14, 10);
            this.editorSoat.Size = new System.Drawing.Size(1048, 143);
            this.editorSoat.TabIndex = 0;
            // 
            // editorSoatAcciones
            // 
            this.editorSoatAcciones.AutoSize = true;
            this.editorSoatAcciones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editorSoatAcciones.Controls.Add(this.btnGuardarSoat);
            this.editorSoatAcciones.Controls.Add(this.btnNuevoSoat);
            this.editorSoatAcciones.Controls.Add(this.btnEliminarSoat);
            this.editorSoatAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorSoatAcciones.Location = new System.Drawing.Point(14, 87);
            this.editorSoatAcciones.Name = "editorSoatAcciones";
            this.editorSoatAcciones.Padding = new System.Windows.Forms.Padding(6, 6, 0, 0);
            this.editorSoatAcciones.Size = new System.Drawing.Size(1020, 44);
            this.editorSoatAcciones.TabIndex = 2;
            // 
            // btnGuardarSoat
            // 
            this.btnGuardarSoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnGuardarSoat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarSoat.FlatAppearance.BorderSize = 0;
            this.btnGuardarSoat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnGuardarSoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarSoat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGuardarSoat.ForeColor = System.Drawing.Color.White;
            this.btnGuardarSoat.Location = new System.Drawing.Point(6, 9);
            this.btnGuardarSoat.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnGuardarSoat.Name = "btnGuardarSoat";
            this.btnGuardarSoat.Size = new System.Drawing.Size(112, 32);
            this.btnGuardarSoat.TabIndex = 0;
            this.btnGuardarSoat.Text = "Guardar";
            this.btnGuardarSoat.UseVisualStyleBackColor = false;
            // 
            // btnNuevoSoat
            // 
            this.btnNuevoSoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.btnNuevoSoat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoSoat.FlatAppearance.BorderSize = 0;
            this.btnNuevoSoat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.btnNuevoSoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoSoat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevoSoat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.btnNuevoSoat.Location = new System.Drawing.Point(126, 9);
            this.btnNuevoSoat.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnNuevoSoat.Name = "btnNuevoSoat";
            this.btnNuevoSoat.Size = new System.Drawing.Size(86, 32);
            this.btnNuevoSoat.TabIndex = 1;
            this.btnNuevoSoat.Text = "Nuevo";
            this.btnNuevoSoat.UseVisualStyleBackColor = false;
            // 
            // btnEliminarSoat
            // 
            this.btnEliminarSoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnEliminarSoat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarSoat.FlatAppearance.BorderSize = 0;
            this.btnEliminarSoat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnEliminarSoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarSoat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEliminarSoat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.btnEliminarSoat.Location = new System.Drawing.Point(220, 9);
            this.btnEliminarSoat.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnEliminarSoat.Name = "btnEliminarSoat";
            this.btnEliminarSoat.Size = new System.Drawing.Size(90, 32);
            this.btnEliminarSoat.TabIndex = 2;
            this.btnEliminarSoat.Text = "Eliminar";
            this.btnEliminarSoat.UseVisualStyleBackColor = false;
            // 
            // editorSoatCampos
            // 
            this.editorSoatCampos.ColumnCount = 4;
            this.editorSoatCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31F));
            this.editorSoatCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.editorSoatCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.editorSoatCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.editorSoatCampos.Controls.Add(this.lblCmbPlacaSoat, 0, 0);
            this.editorSoatCampos.Controls.Add(this.cmbPlacaSoat, 0, 1);
            this.editorSoatCampos.Controls.Add(this.lblTxtNumeroSoat, 1, 0);
            this.editorSoatCampos.Controls.Add(this.txtNumeroSoat, 1, 1);
            this.editorSoatCampos.Controls.Add(this.lblDtpFechaInicio, 2, 0);
            this.editorSoatCampos.Controls.Add(this.dtpFechaInicio, 2, 1);
            this.editorSoatCampos.Controls.Add(this.lblDtpFechaFin, 3, 0);
            this.editorSoatCampos.Controls.Add(this.dtpFechaFin, 3, 1);
            this.editorSoatCampos.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorSoatCampos.Location = new System.Drawing.Point(14, 36);
            this.editorSoatCampos.Margin = new System.Windows.Forms.Padding(0);
            this.editorSoatCampos.Name = "editorSoatCampos";
            this.editorSoatCampos.RowCount = 2;
            this.editorSoatCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.editorSoatCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.editorSoatCampos.Size = new System.Drawing.Size(1020, 51);
            this.editorSoatCampos.TabIndex = 1;
            // 
            // lblCmbPlacaSoat
            // 
            this.lblCmbPlacaSoat.AutoEllipsis = true;
            this.lblCmbPlacaSoat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCmbPlacaSoat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCmbPlacaSoat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCmbPlacaSoat.Location = new System.Drawing.Point(6, 0);
            this.lblCmbPlacaSoat.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCmbPlacaSoat.Name = "lblCmbPlacaSoat";
            this.lblCmbPlacaSoat.Size = new System.Drawing.Size(304, 20);
            this.lblCmbPlacaSoat.TabIndex = 0;
            this.lblCmbPlacaSoat.Text = "Vehículo";
            this.lblCmbPlacaSoat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbPlacaSoat
            // 
            this.cmbPlacaSoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.cmbPlacaSoat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbPlacaSoat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlacaSoat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.cmbPlacaSoat.Location = new System.Drawing.Point(6, 23);
            this.cmbPlacaSoat.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbPlacaSoat.Name = "cmbPlacaSoat";
            this.cmbPlacaSoat.Size = new System.Drawing.Size(304, 23);
            this.cmbPlacaSoat.TabIndex = 1;
            // 
            // lblTxtNumeroSoat
            // 
            this.lblTxtNumeroSoat.AutoEllipsis = true;
            this.lblTxtNumeroSoat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtNumeroSoat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtNumeroSoat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtNumeroSoat.Location = new System.Drawing.Point(322, 0);
            this.lblTxtNumeroSoat.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtNumeroSoat.Name = "lblTxtNumeroSoat";
            this.lblTxtNumeroSoat.Size = new System.Drawing.Size(222, 20);
            this.lblTxtNumeroSoat.TabIndex = 2;
            this.lblTxtNumeroSoat.Text = "Número SOAT";
            this.lblTxtNumeroSoat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNumeroSoat
            // 
            this.txtNumeroSoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtNumeroSoat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNumeroSoat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNumeroSoat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtNumeroSoat.Location = new System.Drawing.Point(322, 23);
            this.txtNumeroSoat.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtNumeroSoat.MaxLength = 20;
            this.txtNumeroSoat.Name = "txtNumeroSoat";
            this.txtNumeroSoat.Size = new System.Drawing.Size(222, 23);
            this.txtNumeroSoat.TabIndex = 3;
            // 
            // lblDtpFechaInicio
            // 
            this.lblDtpFechaInicio.AutoEllipsis = true;
            this.lblDtpFechaInicio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDtpFechaInicio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDtpFechaInicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblDtpFechaInicio.Location = new System.Drawing.Point(556, 0);
            this.lblDtpFechaInicio.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDtpFechaInicio.Name = "lblDtpFechaInicio";
            this.lblDtpFechaInicio.Size = new System.Drawing.Size(222, 20);
            this.lblDtpFechaInicio.TabIndex = 4;
            this.lblDtpFechaInicio.Text = "Fecha de inicio";
            this.lblDtpFechaInicio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new System.Drawing.Point(556, 23);
            this.dtpFechaInicio.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.dtpFechaInicio.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(222, 23);
            this.dtpFechaInicio.TabIndex = 5;
            // 
            // lblDtpFechaFin
            // 
            this.lblDtpFechaFin.AutoEllipsis = true;
            this.lblDtpFechaFin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDtpFechaFin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDtpFechaFin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblDtpFechaFin.Location = new System.Drawing.Point(790, 0);
            this.lblDtpFechaFin.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDtpFechaFin.Name = "lblDtpFechaFin";
            this.lblDtpFechaFin.Size = new System.Drawing.Size(224, 20);
            this.lblDtpFechaFin.TabIndex = 6;
            this.lblDtpFechaFin.Text = "Fecha de vencimiento";
            this.lblDtpFechaFin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(790, 23);
            this.dtpFechaFin.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.dtpFechaFin.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(224, 23);
            this.dtpFechaFin.TabIndex = 7;
            // 
            // editorSoatTitulo
            // 
            this.editorSoatTitulo.AutoEllipsis = true;
            this.editorSoatTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorSoatTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.editorSoatTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.editorSoatTitulo.Location = new System.Drawing.Point(14, 4);
            this.editorSoatTitulo.Margin = new System.Windows.Forms.Padding(0);
            this.editorSoatTitulo.Name = "editorSoatTitulo";
            this.editorSoatTitulo.Size = new System.Drawing.Size(1020, 32);
            this.editorSoatTitulo.TabIndex = 0;
            this.editorSoatTitulo.Text = "Datos de la póliza";
            this.editorSoatTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmVehiculos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1120, 680);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.encabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.MinimumSize = new System.Drawing.Size(680, 440);
            this.Name = "FrmVehiculos";
            this.Padding = new System.Windows.Forms.Padding(20, 8, 20, 16);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vehículos y SOAT";
            ((System.ComponentModel.ISupportInitialize)(this.errores)).EndInit();
            this.encabezado.ResumeLayout(false);
            this.tabs.ResumeLayout(false);
            this.tabVehiculos.ResumeLayout(false);
            this.tabVehiculos.PerformLayout();
            this.listadoVehiculos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridVehiculos)).EndInit();
            this.listadoVehiculosFiltros.ResumeLayout(false);
            this.listadoVehiculosFiltros.PerformLayout();
            this.editorVehiculo.ResumeLayout(false);
            this.editorVehiculo.PerformLayout();
            this.editorVehiculoAcciones.ResumeLayout(false);
            this.editorVehiculoCampos.ResumeLayout(false);
            this.editorVehiculoCampos.PerformLayout();
            this.tabSoat.ResumeLayout(false);
            this.tabSoat.PerformLayout();
            this.listadoSoat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSoat)).EndInit();
            this.editorSoat.ResumeLayout(false);
            this.editorSoat.PerformLayout();
            this.editorSoatAcciones.ResumeLayout(false);
            this.editorSoatCampos.ResumeLayout(false);
            this.editorSoatCampos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel encabezado;
        private Label lblTitulo;
        private Panel acentoTitulo;
        private TabControl tabs;
        private TabPage tabVehiculos;
        private TabPage tabSoat;
        private TextBox txtPlaca;
        private TextBox txtMarca;
        private TextBox txtLinea;
        private TextBox txtModelo;
        private ComboBox cmbTipoVehiculo;
        private TextBox txtColor;
        private ComboBox cmbPropietario;
        private Button btnGuardarVehiculo;
        private Button btnNuevoVehiculo;
        private Button btnEliminarVehiculo;
        private Panel editorVehiculo;
        private TableLayoutPanel editorVehiculoCampos;
        private Label lblTxtPlaca;
        private Label lblTxtMarca;
        private Label lblTxtLinea;
        private Label lblTxtModelo;
        private Label lblCmbTipoVehiculo;
        private Label lblTxtColor;
        private Label lblCmbPropietario;
        private FlowLayoutPanel editorVehiculoAcciones;
        private Label editorVehiculoTitulo;
        private Panel espacioVehiculos;
        private TextBox txtBuscarVehiculo;
        private Panel listadoVehiculos;
        private Label listadoVehiculosTitulo;
        private TableLayoutPanel listadoVehiculosFiltros;
        private Label lblTxtBuscarVehiculo;
        private DataGridView gridVehiculos;
        private DataGridViewTextBoxColumn gridVehiculosColPlaca;
        private DataGridViewTextBoxColumn gridVehiculosColMarca;
        private DataGridViewTextBoxColumn gridVehiculosColLinea;
        private DataGridViewTextBoxColumn gridVehiculosColModelo;
        private DataGridViewTextBoxColumn gridVehiculosColTipoVehiculo;
        private DataGridViewTextBoxColumn gridVehiculosColColor;
        private DataGridViewTextBoxColumn gridVehiculosColPropietario;
        private DataGridViewTextBoxColumn gridVehiculosColCedulaPropietario;
        private ComboBox cmbPlacaSoat;
        private TextBox txtNumeroSoat;
        private DateTimePicker dtpFechaInicio;
        private DateTimePicker dtpFechaFin;
        private Button btnGuardarSoat;
        private Button btnNuevoSoat;
        private Button btnEliminarSoat;
        private Panel editorSoat;
        private TableLayoutPanel editorSoatCampos;
        private Label lblCmbPlacaSoat;
        private Label lblTxtNumeroSoat;
        private Label lblDtpFechaInicio;
        private Label lblDtpFechaFin;
        private FlowLayoutPanel editorSoatAcciones;
        private Label editorSoatTitulo;
        private Panel espacioSoat;
        private Panel listadoSoat;
        private Label listadoSoatTitulo;
        private DataGridView gridSoat;
        private DataGridViewTextBoxColumn gridSoatColSoatId;
        private DataGridViewTextBoxColumn gridSoatColNumeroSoat;
        private DataGridViewTextBoxColumn gridSoatColPlaca;
        private DataGridViewTextBoxColumn gridSoatColVehiculo;
        private DataGridViewTextBoxColumn gridSoatColPropietario;
        private DataGridViewTextBoxColumn gridSoatColFechaInicio;
        private DataGridViewTextBoxColumn gridSoatColFechaFin;
        private DataGridViewTextBoxColumn gridSoatColEstado;
    }
}
