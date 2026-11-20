using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PRESENTACION
{
    partial class FrmFacturacion
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.errores = new System.Windows.Forms.ErrorProvider(this.components);
            this.encabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.acentoTitulo = new System.Windows.Forms.Panel();
            this.tabsFacturacion = new System.Windows.Forms.TabControl();
            this.tabNuevo = new System.Windows.Forms.TabPage();
            this.detalleDocumento = new System.Windows.Forms.Panel();
            this.gridCarrito = new System.Windows.Forms.DataGridView();
            this.Articulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PorcentajeIVA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Iva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resumenTotales = new System.Windows.Forms.TableLayoutPanel();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblIva = new System.Windows.Forms.Label();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.camposPartida = new System.Windows.Forms.TableLayoutPanel();
            this.lblCmbArticulo = new System.Windows.Forms.Label();
            this.cmbArticulo = new System.Windows.Forms.ComboBox();
            this.lblNudCantidad = new System.Windows.Forms.Label();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.lblNudDescuento = new System.Windows.Forms.Label();
            this.nudDescuento = new System.Windows.Forms.NumericUpDown();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.lblPartidas = new System.Windows.Forms.Label();
            this.espacioDocumento = new System.Windows.Forms.Panel();
            this.editorDocumento = new System.Windows.Forms.Panel();
            this.camposDocumento = new System.Windows.Forms.TableLayoutPanel();
            this.lblCmbTipo = new System.Windows.Forms.Label();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.lblCmbCliente = new System.Windows.Forms.Label();
            this.cmbCliente = new System.Windows.Forms.ComboBox();
            this.lblCmbEmpleado = new System.Windows.Forms.Label();
            this.cmbEmpleado = new System.Windows.Forms.ComboBox();
            this.lblDtpFecha = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.lblCmbEstadoPago = new System.Windows.Forms.Label();
            this.cmbEstadoPago = new System.Windows.Forms.ComboBox();
            this.lblCmbVehiculo = new System.Windows.Forms.Label();
            this.cmbVehiculo = new System.Windows.Forms.ComboBox();
            this.lblCmbAgenda = new System.Windows.Forms.Label();
            this.cmbAgenda = new System.Windows.Forms.ComboBox();
            this.chkDomicilio = new System.Windows.Forms.CheckBox();
            this.lblTxtDireccion = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.lblTxtObservaciones = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.tabHistorial = new System.Windows.Forms.TabPage();
            this.detalleHistorial = new System.Windows.Forms.TableLayoutPanel();
            this.panelDocumentos = new System.Windows.Forms.Panel();
            this.documentosRegistrados = new System.Windows.Forms.Panel();
            this.gridHistorial = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridHistorialColTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Empleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vehiculo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridHistorialColSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridHistorialColIva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridHistorialColDescuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridHistorialColTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstadoPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.documentosRegistradosAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnConvertir = new System.Windows.Forms.Button();
            this.btnAnular = new System.Windows.Forms.Button();
            this.documentosRegistradosTitulo = new System.Windows.Forms.Label();
            this.panelLineas = new System.Windows.Forms.Panel();
            this.lineasHistorial = new System.Windows.Forms.Panel();
            this.gridDetalles = new System.Windows.Forms.DataGridView();
            this.gridDetallesColArticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridDetallesColCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridDetallesColPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridDetallesColDescuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridDetallesColIva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridDetallesColTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblDetalleDocumento = new System.Windows.Forms.Label();
            this.lblLineasHistorial = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errores)).BeginInit();
            this.encabezado.SuspendLayout();
            this.tabsFacturacion.SuspendLayout();
            this.tabNuevo.SuspendLayout();
            this.detalleDocumento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCarrito)).BeginInit();
            this.resumenTotales.SuspendLayout();
            this.camposPartida.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDescuento)).BeginInit();
            this.editorDocumento.SuspendLayout();
            this.camposDocumento.SuspendLayout();
            this.tabHistorial.SuspendLayout();
            this.detalleHistorial.SuspendLayout();
            this.panelDocumentos.SuspendLayout();
            this.documentosRegistrados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridHistorial)).BeginInit();
            this.documentosRegistradosAcciones.SuspendLayout();
            this.panelLineas.SuspendLayout();
            this.lineasHistorial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDetalles)).BeginInit();
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
            this.encabezado.Size = new System.Drawing.Size(1120, 57);
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
            this.lblTitulo.Size = new System.Drawing.Size(1116, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Facturación y cotizaciones";
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
            // tabsFacturacion
            // 
            this.tabsFacturacion.Controls.Add(this.tabNuevo);
            this.tabsFacturacion.Controls.Add(this.tabHistorial);
            this.tabsFacturacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabsFacturacion.ItemSize = new System.Drawing.Size(150, 31);
            this.tabsFacturacion.Location = new System.Drawing.Point(20, 65);
            this.tabsFacturacion.Name = "tabsFacturacion";
            this.tabsFacturacion.Padding = new System.Drawing.Point(13, 5);
            this.tabsFacturacion.SelectedIndex = 0;
            this.tabsFacturacion.Size = new System.Drawing.Size(1120, 639);
            this.tabsFacturacion.TabIndex = 1;
            // 
            // tabNuevo
            // 
            this.tabNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.tabNuevo.Controls.Add(this.detalleDocumento);
            this.tabNuevo.Controls.Add(this.espacioDocumento);
            this.tabNuevo.Controls.Add(this.editorDocumento);
            this.tabNuevo.Location = new System.Drawing.Point(4, 35);
            this.tabNuevo.Name = "tabNuevo";
            this.tabNuevo.Padding = new System.Windows.Forms.Padding(12);
            this.tabNuevo.Size = new System.Drawing.Size(1112, 600);
            this.tabNuevo.TabIndex = 0;
            this.tabNuevo.Text = "Nuevo documento";
            // 
            // detalleDocumento
            // 
            this.detalleDocumento.BackColor = System.Drawing.Color.White;
            this.detalleDocumento.Controls.Add(this.gridCarrito);
            this.detalleDocumento.Controls.Add(this.resumenTotales);
            this.detalleDocumento.Controls.Add(this.camposPartida);
            this.detalleDocumento.Controls.Add(this.lblPartidas);
            this.detalleDocumento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detalleDocumento.Location = new System.Drawing.Point(12, 172);
            this.detalleDocumento.Margin = new System.Windows.Forms.Padding(0);
            this.detalleDocumento.Name = "detalleDocumento";
            this.detalleDocumento.Padding = new System.Windows.Forms.Padding(12, 4, 12, 10);
            this.detalleDocumento.Size = new System.Drawing.Size(1088, 416);
            this.detalleDocumento.TabIndex = 2;
            // 
            // gridCarrito
            // 
            this.gridCarrito.AllowUserToAddRows = false;
            this.gridCarrito.AllowUserToDeleteRows = false;
            this.gridCarrito.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.gridCarrito.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridCarrito.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridCarrito.BackgroundColor = System.Drawing.Color.White;
            this.gridCarrito.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridCarrito.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridCarrito.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5, 0, 2, 0);
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridCarrito.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridCarrito.ColumnHeadersHeight = 35;
            this.gridCarrito.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridCarrito.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Articulo,
            this.Tipo,
            this.Cantidad,
            this.Precio,
            this.PorcentajeIVA,
            this.Descuento,
            this.Subtotal,
            this.Iva,
            this.Total});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(5, 0, 3, 0);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridCarrito.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridCarrito.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCarrito.EnableHeadersVisualStyles = false;
            this.gridCarrito.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(227)))), ((int)(((byte)(238)))));
            this.gridCarrito.Location = new System.Drawing.Point(12, 78);
            this.gridCarrito.Margin = new System.Windows.Forms.Padding(0);
            this.gridCarrito.MultiSelect = false;
            this.gridCarrito.Name = "gridCarrito";
            this.gridCarrito.ReadOnly = true;
            this.gridCarrito.RowHeadersVisible = false;
            this.gridCarrito.RowTemplate.Height = 30;
            this.gridCarrito.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridCarrito.Size = new System.Drawing.Size(1064, 256);
            this.gridCarrito.TabIndex = 5;
            this.gridCarrito.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCarrito_CellContentClick);
            // 
            // Articulo
            // 
            this.Articulo.DataPropertyName = "Articulo";
            this.Articulo.FillWeight = 146F;
            this.Articulo.HeaderText = "Artículo";
            this.Articulo.MinimumWidth = 99;
            this.Articulo.Name = "Articulo";
            this.Articulo.ReadOnly = true;
            this.Articulo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Tipo
            // 
            this.Tipo.DataPropertyName = "Tipo";
            this.Tipo.FillWeight = 87F;
            this.Tipo.HeaderText = "Tipo";
            this.Tipo.MinimumWidth = 76;
            this.Tipo.Name = "Tipo";
            this.Tipo.ReadOnly = true;
            this.Tipo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Cantidad
            // 
            this.Cantidad.DataPropertyName = "Cantidad";
            this.Cantidad.FillWeight = 68F;
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.MinimumWidth = 63;
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            this.Cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Precio
            // 
            this.Precio.DataPropertyName = "Precio";
            this.Precio.FillWeight = 99F;
            this.Precio.HeaderText = "Precio unitario";
            this.Precio.MinimumWidth = 87;
            this.Precio.Name = "Precio";
            this.Precio.ReadOnly = true;
            this.Precio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PorcentajeIVA
            // 
            this.PorcentajeIVA.DataPropertyName = "PorcentajeIVA";
            this.PorcentajeIVA.FillWeight = 58F;
            this.PorcentajeIVA.HeaderText = "IVA %";
            this.PorcentajeIVA.MinimumWidth = 51;
            this.PorcentajeIVA.Name = "PorcentajeIVA";
            this.PorcentajeIVA.ReadOnly = true;
            this.PorcentajeIVA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Descuento
            // 
            this.Descuento.DataPropertyName = "Descuento";
            this.Descuento.FillWeight = 92F;
            this.Descuento.HeaderText = "Descuento";
            this.Descuento.MinimumWidth = 86;
            this.Descuento.Name = "Descuento";
            this.Descuento.ReadOnly = true;
            this.Descuento.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Subtotal
            // 
            this.Subtotal.DataPropertyName = "Subtotal";
            this.Subtotal.FillWeight = 97F;
            this.Subtotal.HeaderText = "Subtotal";
            this.Subtotal.MinimumWidth = 90;
            this.Subtotal.Name = "Subtotal";
            this.Subtotal.ReadOnly = true;
            this.Subtotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Iva
            // 
            this.Iva.DataPropertyName = "Iva";
            this.Iva.FillWeight = 89F;
            this.Iva.HeaderText = "IVA";
            this.Iva.MinimumWidth = 74;
            this.Iva.Name = "Iva";
            this.Iva.ReadOnly = true;
            this.Iva.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Total
            // 
            this.Total.DataPropertyName = "Total";
            this.Total.HeaderText = "Total";
            this.Total.MinimumWidth = 90;
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            this.Total.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // resumenTotales
            // 
            this.resumenTotales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.resumenTotales.ColumnCount = 4;
            this.resumenTotales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.resumenTotales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.resumenTotales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.resumenTotales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.resumenTotales.Controls.Add(this.lblSubtotal, 0, 0);
            this.resumenTotales.Controls.Add(this.lblIva, 1, 0);
            this.resumenTotales.Controls.Add(this.lblDescuento, 2, 0);
            this.resumenTotales.Controls.Add(this.lblTotal, 3, 0);
            this.resumenTotales.Controls.Add(this.btnNuevo, 2, 1);
            this.resumenTotales.Controls.Add(this.btnGuardar, 3, 1);
            this.resumenTotales.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.resumenTotales.Location = new System.Drawing.Point(12, 334);
            this.resumenTotales.Margin = new System.Windows.Forms.Padding(0);
            this.resumenTotales.Name = "resumenTotales";
            this.resumenTotales.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.resumenTotales.RowCount = 2;
            this.resumenTotales.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.resumenTotales.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.resumenTotales.Size = new System.Drawing.Size(1064, 72);
            this.resumenTotales.TabIndex = 4;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoEllipsis = true;
            this.lblSubtotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSubtotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblSubtotal.Location = new System.Drawing.Point(8, 2);
            this.lblSubtotal.Margin = new System.Windows.Forms.Padding(0);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Padding = new System.Windows.Forms.Padding(5, 0, 4, 0);
            this.lblSubtotal.Size = new System.Drawing.Size(262, 32);
            this.lblSubtotal.TabIndex = 0;
            this.lblSubtotal.Text = "Subtotal: —";
            this.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIva
            // 
            this.lblIva.AutoEllipsis = true;
            this.lblIva.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIva.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblIva.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblIva.Location = new System.Drawing.Point(270, 2);
            this.lblIva.Margin = new System.Windows.Forms.Padding(0);
            this.lblIva.Name = "lblIva";
            this.lblIva.Padding = new System.Windows.Forms.Padding(5, 0, 4, 0);
            this.lblIva.Size = new System.Drawing.Size(262, 32);
            this.lblIva.TabIndex = 1;
            this.lblIva.Text = "IVA: —";
            this.lblIva.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDescuento
            // 
            this.lblDescuento.AutoEllipsis = true;
            this.lblDescuento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescuento.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescuento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblDescuento.Location = new System.Drawing.Point(532, 2);
            this.lblDescuento.Margin = new System.Windows.Forms.Padding(0);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Padding = new System.Windows.Forms.Padding(5, 0, 4, 0);
            this.lblDescuento.Size = new System.Drawing.Size(262, 32);
            this.lblDescuento.TabIndex = 2;
            this.lblDescuento.Text = "Descuento: —";
            this.lblDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoEllipsis = true;
            this.lblTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblTotal.Location = new System.Drawing.Point(794, 2);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Padding = new System.Windows.Forms.Padding(5, 0, 4, 0);
            this.lblTotal.Size = new System.Drawing.Size(262, 32);
            this.lblTotal.TabIndex = 3;
            this.lblTotal.Text = "Total: —";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.btnNuevo.Location = new System.Drawing.Point(537, 34);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(5, 0, 5, 1);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(252, 35);
            this.btnNuevo.TabIndex = 4;
            this.btnNuevo.Text = "Limpiar / Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(799, 34);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(5, 0, 5, 1);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(252, 35);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Text = "Guardar documento";
            this.btnGuardar.UseVisualStyleBackColor = false;
            // 
            // camposPartida
            // 
            this.camposPartida.ColumnCount = 5;
            this.camposPartida.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.camposPartida.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.camposPartida.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.camposPartida.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.camposPartida.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.camposPartida.Controls.Add(this.lblCmbArticulo, 0, 0);
            this.camposPartida.Controls.Add(this.cmbArticulo, 0, 1);
            this.camposPartida.Controls.Add(this.lblNudCantidad, 1, 0);
            this.camposPartida.Controls.Add(this.nudCantidad, 1, 1);
            this.camposPartida.Controls.Add(this.lblNudDescuento, 2, 0);
            this.camposPartida.Controls.Add(this.nudDescuento, 2, 1);
            this.camposPartida.Controls.Add(this.btnAgregar, 3, 1);
            this.camposPartida.Controls.Add(this.btnQuitar, 4, 1);
            this.camposPartida.Dock = System.Windows.Forms.DockStyle.Top;
            this.camposPartida.Location = new System.Drawing.Point(12, 32);
            this.camposPartida.Margin = new System.Windows.Forms.Padding(0);
            this.camposPartida.Name = "camposPartida";
            this.camposPartida.RowCount = 2;
            this.camposPartida.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.camposPartida.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.camposPartida.Size = new System.Drawing.Size(1064, 46);
            this.camposPartida.TabIndex = 1;
            // 
            // lblCmbArticulo
            // 
            this.lblCmbArticulo.AutoEllipsis = true;
            this.lblCmbArticulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCmbArticulo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCmbArticulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCmbArticulo.Location = new System.Drawing.Point(6, 0);
            this.lblCmbArticulo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCmbArticulo.Name = "lblCmbArticulo";
            this.lblCmbArticulo.Size = new System.Drawing.Size(371, 17);
            this.lblCmbArticulo.TabIndex = 0;
            this.lblCmbArticulo.Text = "Artículo";
            this.lblCmbArticulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbArticulo
            // 
            this.cmbArticulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.cmbArticulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbArticulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbArticulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.cmbArticulo.Location = new System.Drawing.Point(6, 20);
            this.cmbArticulo.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbArticulo.Name = "cmbArticulo";
            this.cmbArticulo.Size = new System.Drawing.Size(371, 23);
            this.cmbArticulo.TabIndex = 1;
            // 
            // lblNudCantidad
            // 
            this.lblNudCantidad.AutoEllipsis = true;
            this.lblNudCantidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNudCantidad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNudCantidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblNudCantidad.Location = new System.Drawing.Point(389, 0);
            this.lblNudCantidad.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblNudCantidad.Name = "lblNudCantidad";
            this.lblNudCantidad.Size = new System.Drawing.Size(126, 17);
            this.lblNudCantidad.TabIndex = 2;
            this.lblNudCantidad.Text = "Cantidad";
            this.lblNudCantidad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNudCantidad.Click += new System.EventHandler(this.lblNudCantidad_Click);
            // 
            // nudCantidad
            // 
            this.nudCantidad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.nudCantidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudCantidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudCantidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.nudCantidad.Location = new System.Drawing.Point(389, 20);
            this.nudCantidad.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.nudCantidad.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudCantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(126, 23);
            this.nudCantidad.TabIndex = 3;
            this.nudCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudCantidad.ThousandsSeparator = true;
            this.nudCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblNudDescuento
            // 
            this.lblNudDescuento.AutoEllipsis = true;
            this.lblNudDescuento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNudDescuento.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNudDescuento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblNudDescuento.Location = new System.Drawing.Point(527, 0);
            this.lblNudDescuento.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblNudDescuento.Name = "lblNudDescuento";
            this.lblNudDescuento.Size = new System.Drawing.Size(190, 17);
            this.lblNudDescuento.TabIndex = 4;
            this.lblNudDescuento.Text = "Descuento por ítem";
            this.lblNudDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudDescuento
            // 
            this.nudDescuento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.nudDescuento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudDescuento.DecimalPlaces = 2;
            this.nudDescuento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudDescuento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.nudDescuento.Location = new System.Drawing.Point(527, 20);
            this.nudDescuento.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.nudDescuento.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            131072});
            this.nudDescuento.Name = "nudDescuento";
            this.nudDescuento.Size = new System.Drawing.Size(190, 23);
            this.nudDescuento.TabIndex = 5;
            this.nudDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudDescuento.ThousandsSeparator = true;
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAgregar.FlatAppearance.BorderSize = 0;
            this.btnAgregar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAgregar.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.Location = new System.Drawing.Point(729, 17);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(6, 0, 6, 2);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(158, 27);
            this.btnAgregar.TabIndex = 6;
            this.btnAgregar.Text = "Agregar ítem";
            this.btnAgregar.UseVisualStyleBackColor = false;
            // 
            // btnQuitar
            // 
            this.btnQuitar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.btnQuitar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuitar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnQuitar.FlatAppearance.BorderSize = 0;
            this.btnQuitar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.btnQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnQuitar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.btnQuitar.Location = new System.Drawing.Point(899, 17);
            this.btnQuitar.Margin = new System.Windows.Forms.Padding(6, 0, 6, 2);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(159, 27);
            this.btnQuitar.TabIndex = 7;
            this.btnQuitar.Text = "Quitar ítem";
            this.btnQuitar.UseVisualStyleBackColor = false;
            // 
            // lblPartidas
            // 
            this.lblPartidas.AutoEllipsis = true;
            this.lblPartidas.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPartidas.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPartidas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblPartidas.Location = new System.Drawing.Point(12, 4);
            this.lblPartidas.Margin = new System.Windows.Forms.Padding(0);
            this.lblPartidas.Name = "lblPartidas";
            this.lblPartidas.Size = new System.Drawing.Size(1064, 28);
            this.lblPartidas.TabIndex = 0;
            this.lblPartidas.Text = "Ítems del documento";
            this.lblPartidas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // espacioDocumento
            // 
            this.espacioDocumento.Dock = System.Windows.Forms.DockStyle.Top;
            this.espacioDocumento.Location = new System.Drawing.Point(12, 164);
            this.espacioDocumento.Name = "espacioDocumento";
            this.espacioDocumento.Size = new System.Drawing.Size(1088, 8);
            this.espacioDocumento.TabIndex = 1;
            // 
            // editorDocumento
            // 
            this.editorDocumento.BackColor = System.Drawing.Color.White;
            this.editorDocumento.Controls.Add(this.camposDocumento);
            this.editorDocumento.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorDocumento.Location = new System.Drawing.Point(12, 12);
            this.editorDocumento.Margin = new System.Windows.Forms.Padding(0);
            this.editorDocumento.Name = "editorDocumento";
            this.editorDocumento.Padding = new System.Windows.Forms.Padding(12, 4, 12, 8);
            this.editorDocumento.Size = new System.Drawing.Size(1088, 152);
            this.editorDocumento.TabIndex = 0;
            // 
            // camposDocumento
            // 
            this.camposDocumento.ColumnCount = 4;
            this.camposDocumento.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.camposDocumento.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.camposDocumento.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.camposDocumento.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.camposDocumento.Controls.Add(this.lblCmbTipo, 0, 0);
            this.camposDocumento.Controls.Add(this.cmbTipo, 0, 1);
            this.camposDocumento.Controls.Add(this.lblCmbCliente, 1, 0);
            this.camposDocumento.Controls.Add(this.cmbCliente, 1, 1);
            this.camposDocumento.Controls.Add(this.lblCmbEmpleado, 2, 0);
            this.camposDocumento.Controls.Add(this.cmbEmpleado, 2, 1);
            this.camposDocumento.Controls.Add(this.lblDtpFecha, 3, 0);
            this.camposDocumento.Controls.Add(this.dtpFecha, 3, 1);
            this.camposDocumento.Controls.Add(this.lblCmbEstadoPago, 0, 2);
            this.camposDocumento.Controls.Add(this.cmbEstadoPago, 0, 3);
            this.camposDocumento.Controls.Add(this.lblCmbVehiculo, 1, 2);
            this.camposDocumento.Controls.Add(this.cmbVehiculo, 1, 3);
            this.camposDocumento.Controls.Add(this.lblCmbAgenda, 2, 2);
            this.camposDocumento.Controls.Add(this.cmbAgenda, 2, 3);
            this.camposDocumento.Controls.Add(this.chkDomicilio, 3, 2);
            this.camposDocumento.Controls.Add(this.lblTxtDireccion, 0, 4);
            this.camposDocumento.Controls.Add(this.txtDireccion, 0, 5);
            this.camposDocumento.Controls.Add(this.lblTxtObservaciones, 2, 4);
            this.camposDocumento.Controls.Add(this.txtObservaciones, 2, 5);
            this.camposDocumento.Dock = System.Windows.Forms.DockStyle.Top;
            this.camposDocumento.Location = new System.Drawing.Point(12, 4);
            this.camposDocumento.Margin = new System.Windows.Forms.Padding(0);
            this.camposDocumento.Name = "camposDocumento";
            this.camposDocumento.RowCount = 6;
            this.camposDocumento.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.camposDocumento.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.camposDocumento.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.camposDocumento.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.camposDocumento.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.camposDocumento.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.camposDocumento.Size = new System.Drawing.Size(1064, 138);
            this.camposDocumento.TabIndex = 0;
            // 
            // lblCmbTipo
            // 
            this.lblCmbTipo.AutoEllipsis = true;
            this.lblCmbTipo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCmbTipo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCmbTipo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCmbTipo.Location = new System.Drawing.Point(6, 0);
            this.lblCmbTipo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCmbTipo.Name = "lblCmbTipo";
            this.lblCmbTipo.Size = new System.Drawing.Size(222, 17);
            this.lblCmbTipo.TabIndex = 0;
            this.lblCmbTipo.Text = "Documento";
            this.lblCmbTipo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbTipo
            // 
            this.cmbTipo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.cmbTipo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.cmbTipo.Items.AddRange(new object[] {
            "VENTA",
            "COTIZACION"});
            this.cmbTipo.Location = new System.Drawing.Point(6, 20);
            this.cmbTipo.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(222, 23);
            this.cmbTipo.TabIndex = 1;
            // 
            // lblCmbCliente
            // 
            this.lblCmbCliente.AutoEllipsis = true;
            this.lblCmbCliente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCmbCliente.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCmbCliente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCmbCliente.Location = new System.Drawing.Point(240, 0);
            this.lblCmbCliente.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCmbCliente.Name = "lblCmbCliente";
            this.lblCmbCliente.Size = new System.Drawing.Size(264, 17);
            this.lblCmbCliente.TabIndex = 2;
            this.lblCmbCliente.Text = "Cliente";
            this.lblCmbCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCliente
            // 
            this.cmbCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.cmbCliente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCliente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.cmbCliente.Location = new System.Drawing.Point(240, 20);
            this.cmbCliente.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbCliente.Name = "cmbCliente";
            this.cmbCliente.Size = new System.Drawing.Size(264, 23);
            this.cmbCliente.TabIndex = 3;
            // 
            // lblCmbEmpleado
            // 
            this.lblCmbEmpleado.AutoEllipsis = true;
            this.lblCmbEmpleado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCmbEmpleado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCmbEmpleado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCmbEmpleado.Location = new System.Drawing.Point(516, 0);
            this.lblCmbEmpleado.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCmbEmpleado.Name = "lblCmbEmpleado";
            this.lblCmbEmpleado.Size = new System.Drawing.Size(264, 17);
            this.lblCmbEmpleado.TabIndex = 4;
            this.lblCmbEmpleado.Text = "Atiende";
            this.lblCmbEmpleado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbEmpleado
            // 
            this.cmbEmpleado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.cmbEmpleado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbEmpleado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmpleado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.cmbEmpleado.Location = new System.Drawing.Point(516, 20);
            this.cmbEmpleado.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbEmpleado.Name = "cmbEmpleado";
            this.cmbEmpleado.Size = new System.Drawing.Size(264, 23);
            this.cmbEmpleado.TabIndex = 5;
            // 
            // lblDtpFecha
            // 
            this.lblDtpFecha.AutoEllipsis = true;
            this.lblDtpFecha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDtpFecha.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDtpFecha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblDtpFecha.Location = new System.Drawing.Point(792, 0);
            this.lblDtpFecha.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDtpFecha.Name = "lblDtpFecha";
            this.lblDtpFecha.Size = new System.Drawing.Size(266, 17);
            this.lblDtpFecha.TabIndex = 6;
            this.lblDtpFecha.Text = "Fecha y hora";
            this.lblDtpFecha.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpFecha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(792, 20);
            this.dtpFecha.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.ShowUpDown = true;
            this.dtpFecha.Size = new System.Drawing.Size(266, 23);
            this.dtpFecha.TabIndex = 7;
            // 
            // lblCmbEstadoPago
            // 
            this.lblCmbEstadoPago.AutoEllipsis = true;
            this.lblCmbEstadoPago.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCmbEstadoPago.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCmbEstadoPago.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCmbEstadoPago.Location = new System.Drawing.Point(6, 46);
            this.lblCmbEstadoPago.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCmbEstadoPago.Name = "lblCmbEstadoPago";
            this.lblCmbEstadoPago.Size = new System.Drawing.Size(222, 17);
            this.lblCmbEstadoPago.TabIndex = 8;
            this.lblCmbEstadoPago.Text = "Estado de pago";
            this.lblCmbEstadoPago.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbEstadoPago
            // 
            this.cmbEstadoPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.cmbEstadoPago.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbEstadoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstadoPago.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.cmbEstadoPago.Items.AddRange(new object[] {
            "Pendiente",
            "Pagada"});
            this.cmbEstadoPago.Location = new System.Drawing.Point(6, 66);
            this.cmbEstadoPago.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbEstadoPago.Name = "cmbEstadoPago";
            this.cmbEstadoPago.Size = new System.Drawing.Size(222, 23);
            this.cmbEstadoPago.TabIndex = 9;
            // 
            // lblCmbVehiculo
            // 
            this.lblCmbVehiculo.AutoEllipsis = true;
            this.lblCmbVehiculo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCmbVehiculo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCmbVehiculo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCmbVehiculo.Location = new System.Drawing.Point(240, 46);
            this.lblCmbVehiculo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCmbVehiculo.Name = "lblCmbVehiculo";
            this.lblCmbVehiculo.Size = new System.Drawing.Size(264, 17);
            this.lblCmbVehiculo.TabIndex = 10;
            this.lblCmbVehiculo.Text = "Vehículo (opcional)";
            this.lblCmbVehiculo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbVehiculo
            // 
            this.cmbVehiculo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.cmbVehiculo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbVehiculo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVehiculo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.cmbVehiculo.Location = new System.Drawing.Point(240, 66);
            this.cmbVehiculo.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbVehiculo.Name = "cmbVehiculo";
            this.cmbVehiculo.Size = new System.Drawing.Size(264, 23);
            this.cmbVehiculo.TabIndex = 11;
            // 
            // lblCmbAgenda
            // 
            this.lblCmbAgenda.AutoEllipsis = true;
            this.lblCmbAgenda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCmbAgenda.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCmbAgenda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCmbAgenda.Location = new System.Drawing.Point(516, 46);
            this.lblCmbAgenda.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCmbAgenda.Name = "lblCmbAgenda";
            this.lblCmbAgenda.Size = new System.Drawing.Size(264, 17);
            this.lblCmbAgenda.TabIndex = 12;
            this.lblCmbAgenda.Text = "Cita (opcional)";
            this.lblCmbAgenda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbAgenda
            // 
            this.cmbAgenda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.cmbAgenda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbAgenda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAgenda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.cmbAgenda.Location = new System.Drawing.Point(516, 66);
            this.cmbAgenda.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbAgenda.Name = "cmbAgenda";
            this.cmbAgenda.Size = new System.Drawing.Size(264, 23);
            this.cmbAgenda.TabIndex = 13;
            // 
            // chkDomicilio
            // 
            this.chkDomicilio.AutoSize = true;
            this.chkDomicilio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkDomicilio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.chkDomicilio.Location = new System.Drawing.Point(792, 49);
            this.chkDomicilio.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.chkDomicilio.Name = "chkDomicilio";
            this.camposDocumento.SetRowSpan(this.chkDomicilio, 2);
            this.chkDomicilio.Size = new System.Drawing.Size(266, 42);
            this.chkDomicilio.TabIndex = 14;
            this.chkDomicilio.Text = "Domicilio";
            // 
            // lblTxtDireccion
            // 
            this.lblTxtDireccion.AutoEllipsis = true;
            this.camposDocumento.SetColumnSpan(this.lblTxtDireccion, 2);
            this.lblTxtDireccion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtDireccion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtDireccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtDireccion.Location = new System.Drawing.Point(6, 92);
            this.lblTxtDireccion.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtDireccion.Name = "lblTxtDireccion";
            this.lblTxtDireccion.Size = new System.Drawing.Size(498, 17);
            this.lblTxtDireccion.TabIndex = 15;
            this.lblTxtDireccion.Text = "Dirección de domicilio";
            this.lblTxtDireccion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDireccion
            // 
            this.txtDireccion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtDireccion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.camposDocumento.SetColumnSpan(this.txtDireccion, 2);
            this.txtDireccion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDireccion.Enabled = false;
            this.txtDireccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtDireccion.Location = new System.Drawing.Point(6, 112);
            this.txtDireccion.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtDireccion.MaxLength = 150;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(498, 23);
            this.txtDireccion.TabIndex = 16;
            // 
            // lblTxtObservaciones
            // 
            this.lblTxtObservaciones.AutoEllipsis = true;
            this.camposDocumento.SetColumnSpan(this.lblTxtObservaciones, 2);
            this.lblTxtObservaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtObservaciones.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtObservaciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtObservaciones.Location = new System.Drawing.Point(516, 92);
            this.lblTxtObservaciones.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtObservaciones.Name = "lblTxtObservaciones";
            this.lblTxtObservaciones.Size = new System.Drawing.Size(542, 17);
            this.lblTxtObservaciones.TabIndex = 17;
            this.lblTxtObservaciones.Text = "Observaciones";
            this.lblTxtObservaciones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtObservaciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.camposDocumento.SetColumnSpan(this.txtObservaciones, 2);
            this.txtObservaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtObservaciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtObservaciones.Location = new System.Drawing.Point(516, 112);
            this.txtObservaciones.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtObservaciones.MaxLength = 200;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(542, 23);
            this.txtObservaciones.TabIndex = 18;
            // 
            // tabHistorial
            // 
            this.tabHistorial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.tabHistorial.Controls.Add(this.detalleHistorial);
            this.tabHistorial.Location = new System.Drawing.Point(4, 35);
            this.tabHistorial.Name = "tabHistorial";
            this.tabHistorial.Padding = new System.Windows.Forms.Padding(12);
            this.tabHistorial.Size = new System.Drawing.Size(1112, 600);
            this.tabHistorial.TabIndex = 1;
            this.tabHistorial.Text = "Historial";
            // 
            // detalleHistorial
            // 
            this.detalleHistorial.ColumnCount = 1;
            this.detalleHistorial.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.detalleHistorial.Controls.Add(this.panelDocumentos, 0, 0);
            this.detalleHistorial.Controls.Add(this.panelLineas, 0, 1);
            this.detalleHistorial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detalleHistorial.Location = new System.Drawing.Point(12, 12);
            this.detalleHistorial.Margin = new System.Windows.Forms.Padding(0);
            this.detalleHistorial.Name = "detalleHistorial";
            this.detalleHistorial.RowCount = 2;
            this.detalleHistorial.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.detalleHistorial.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.detalleHistorial.Size = new System.Drawing.Size(1088, 576);
            this.detalleHistorial.TabIndex = 0;
            // 
            // panelDocumentos
            // 
            this.panelDocumentos.Controls.Add(this.documentosRegistrados);
            this.panelDocumentos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDocumentos.Location = new System.Drawing.Point(0, 0);
            this.panelDocumentos.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.panelDocumentos.Name = "panelDocumentos";
            this.panelDocumentos.Size = new System.Drawing.Size(1088, 326);
            this.panelDocumentos.TabIndex = 0;
            // 
            // documentosRegistrados
            // 
            this.documentosRegistrados.BackColor = System.Drawing.Color.White;
            this.documentosRegistrados.Controls.Add(this.gridHistorial);
            this.documentosRegistrados.Controls.Add(this.documentosRegistradosAcciones);
            this.documentosRegistrados.Controls.Add(this.documentosRegistradosTitulo);
            this.documentosRegistrados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentosRegistrados.Location = new System.Drawing.Point(0, 0);
            this.documentosRegistrados.Margin = new System.Windows.Forms.Padding(0);
            this.documentosRegistrados.Name = "documentosRegistrados";
            this.documentosRegistrados.Padding = new System.Windows.Forms.Padding(14, 4, 14, 12);
            this.documentosRegistrados.Size = new System.Drawing.Size(1088, 326);
            this.documentosRegistrados.TabIndex = 0;
            // 
            // gridHistorial
            // 
            this.gridHistorial.AllowUserToAddRows = false;
            this.gridHistorial.AllowUserToDeleteRows = false;
            this.gridHistorial.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.gridHistorial.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gridHistorial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridHistorial.BackgroundColor = System.Drawing.Color.White;
            this.gridHistorial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridHistorial.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridHistorial.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(5, 0, 2, 0);
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridHistorial.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gridHistorial.ColumnHeadersHeight = 35;
            this.gridHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridHistorial.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.gridHistorialColTipo,
            this.Fecha,
            this.Cliente,
            this.Empleado,
            this.Vehiculo,
            this.gridHistorialColSubtotal,
            this.gridHistorialColIva,
            this.gridHistorialColDescuento,
            this.gridHistorialColTotal,
            this.EstadoPago});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(5, 0, 3, 0);
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridHistorial.DefaultCellStyle = dataGridViewCellStyle7;
            this.gridHistorial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridHistorial.EnableHeadersVisualStyles = false;
            this.gridHistorial.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(227)))), ((int)(((byte)(238)))));
            this.gridHistorial.Location = new System.Drawing.Point(14, 80);
            this.gridHistorial.Margin = new System.Windows.Forms.Padding(0);
            this.gridHistorial.MultiSelect = false;
            this.gridHistorial.Name = "gridHistorial";
            this.gridHistorial.ReadOnly = true;
            this.gridHistorial.RowHeadersVisible = false;
            this.gridHistorial.RowTemplate.Height = 30;
            this.gridHistorial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridHistorial.Size = new System.Drawing.Size(1060, 234);
            this.gridHistorial.TabIndex = 2;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Id.DefaultCellStyle = dataGridViewCellStyle6;
            this.Id.FillWeight = 45F;
            this.Id.HeaderText = "ID";
            this.Id.MinimumWidth = 44;
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridHistorialColTipo
            // 
            this.gridHistorialColTipo.DataPropertyName = "Tipo";
            this.gridHistorialColTipo.FillWeight = 88F;
            this.gridHistorialColTipo.HeaderText = "Tipo";
            this.gridHistorialColTipo.MinimumWidth = 72;
            this.gridHistorialColTipo.Name = "gridHistorialColTipo";
            this.gridHistorialColTipo.ReadOnly = true;
            this.gridHistorialColTipo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Fecha
            // 
            this.Fecha.DataPropertyName = "Fecha";
            this.Fecha.FillWeight = 101F;
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.MinimumWidth = 88;
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Cliente
            // 
            this.Cliente.DataPropertyName = "Cliente";
            this.Cliente.FillWeight = 126F;
            this.Cliente.HeaderText = "Cliente";
            this.Cliente.MinimumWidth = 92;
            this.Cliente.Name = "Cliente";
            this.Cliente.ReadOnly = true;
            this.Cliente.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Empleado
            // 
            this.Empleado.DataPropertyName = "Empleado";
            this.Empleado.HeaderText = "Empleado";
            this.Empleado.MinimumWidth = 85;
            this.Empleado.Name = "Empleado";
            this.Empleado.ReadOnly = true;
            this.Empleado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Vehiculo
            // 
            this.Vehiculo.DataPropertyName = "Vehiculo";
            this.Vehiculo.FillWeight = 80F;
            this.Vehiculo.HeaderText = "Vehículo";
            this.Vehiculo.MinimumWidth = 68;
            this.Vehiculo.Name = "Vehiculo";
            this.Vehiculo.ReadOnly = true;
            this.Vehiculo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridHistorialColSubtotal
            // 
            this.gridHistorialColSubtotal.DataPropertyName = "Subtotal";
            this.gridHistorialColSubtotal.FillWeight = 94F;
            this.gridHistorialColSubtotal.HeaderText = "Subtotal";
            this.gridHistorialColSubtotal.MinimumWidth = 84;
            this.gridHistorialColSubtotal.Name = "gridHistorialColSubtotal";
            this.gridHistorialColSubtotal.ReadOnly = true;
            this.gridHistorialColSubtotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridHistorialColIva
            // 
            this.gridHistorialColIva.DataPropertyName = "Iva";
            this.gridHistorialColIva.FillWeight = 82F;
            this.gridHistorialColIva.HeaderText = "IVA";
            this.gridHistorialColIva.MinimumWidth = 75;
            this.gridHistorialColIva.Name = "gridHistorialColIva";
            this.gridHistorialColIva.ReadOnly = true;
            this.gridHistorialColIva.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridHistorialColDescuento
            // 
            this.gridHistorialColDescuento.DataPropertyName = "Descuento";
            this.gridHistorialColDescuento.FillWeight = 94F;
            this.gridHistorialColDescuento.HeaderText = "Descuento";
            this.gridHistorialColDescuento.MinimumWidth = 86;
            this.gridHistorialColDescuento.Name = "gridHistorialColDescuento";
            this.gridHistorialColDescuento.ReadOnly = true;
            this.gridHistorialColDescuento.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridHistorialColTotal
            // 
            this.gridHistorialColTotal.DataPropertyName = "Total";
            this.gridHistorialColTotal.FillWeight = 94F;
            this.gridHistorialColTotal.HeaderText = "Total";
            this.gridHistorialColTotal.MinimumWidth = 84;
            this.gridHistorialColTotal.Name = "gridHistorialColTotal";
            this.gridHistorialColTotal.ReadOnly = true;
            this.gridHistorialColTotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EstadoPago
            // 
            this.EstadoPago.DataPropertyName = "EstadoPago";
            this.EstadoPago.FillWeight = 83F;
            this.EstadoPago.HeaderText = "Pago";
            this.EstadoPago.MinimumWidth = 77;
            this.EstadoPago.Name = "EstadoPago";
            this.EstadoPago.ReadOnly = true;
            this.EstadoPago.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // documentosRegistradosAcciones
            // 
            this.documentosRegistradosAcciones.AutoSize = true;
            this.documentosRegistradosAcciones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.documentosRegistradosAcciones.Controls.Add(this.btnActualizar);
            this.documentosRegistradosAcciones.Controls.Add(this.btnConvertir);
            this.documentosRegistradosAcciones.Controls.Add(this.btnAnular);
            this.documentosRegistradosAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.documentosRegistradosAcciones.Location = new System.Drawing.Point(14, 36);
            this.documentosRegistradosAcciones.Name = "documentosRegistradosAcciones";
            this.documentosRegistradosAcciones.Padding = new System.Windows.Forms.Padding(6, 6, 0, 0);
            this.documentosRegistradosAcciones.Size = new System.Drawing.Size(1060, 44);
            this.documentosRegistradosAcciones.TabIndex = 1;
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.FlatAppearance.BorderSize = 0;
            this.btnActualizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnActualizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.btnActualizar.Location = new System.Drawing.Point(6, 9);
            this.btnActualizar.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(103, 32);
            this.btnActualizar.TabIndex = 0;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = false;
            // 
            // btnConvertir
            // 
            this.btnConvertir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnConvertir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConvertir.FlatAppearance.BorderSize = 0;
            this.btnConvertir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnConvertir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConvertir.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnConvertir.ForeColor = System.Drawing.Color.White;
            this.btnConvertir.Location = new System.Drawing.Point(117, 9);
            this.btnConvertir.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnConvertir.Name = "btnConvertir";
            this.btnConvertir.Size = new System.Drawing.Size(220, 32);
            this.btnConvertir.TabIndex = 1;
            this.btnConvertir.Text = "Convertir cotización en venta";
            this.btnConvertir.UseVisualStyleBackColor = false;
            // 
            // btnAnular
            // 
            this.btnAnular.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnAnular.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAnular.FlatAppearance.BorderSize = 0;
            this.btnAnular.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnAnular.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnular.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAnular.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.btnAnular.Location = new System.Drawing.Point(345, 9);
            this.btnAnular.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Size = new System.Drawing.Size(145, 32);
            this.btnAnular.TabIndex = 2;
            this.btnAnular.Text = "Anular documento";
            this.btnAnular.UseVisualStyleBackColor = false;
            // 
            // documentosRegistradosTitulo
            // 
            this.documentosRegistradosTitulo.AutoEllipsis = true;
            this.documentosRegistradosTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.documentosRegistradosTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.documentosRegistradosTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.documentosRegistradosTitulo.Location = new System.Drawing.Point(14, 4);
            this.documentosRegistradosTitulo.Margin = new System.Windows.Forms.Padding(0);
            this.documentosRegistradosTitulo.Name = "documentosRegistradosTitulo";
            this.documentosRegistradosTitulo.Size = new System.Drawing.Size(1060, 32);
            this.documentosRegistradosTitulo.TabIndex = 0;
            this.documentosRegistradosTitulo.Text = "Documentos registrados";
            this.documentosRegistradosTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelLineas
            // 
            this.panelLineas.Controls.Add(this.lineasHistorial);
            this.panelLineas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLineas.Location = new System.Drawing.Point(0, 334);
            this.panelLineas.Margin = new System.Windows.Forms.Padding(0);
            this.panelLineas.Name = "panelLineas";
            this.panelLineas.Size = new System.Drawing.Size(1088, 242);
            this.panelLineas.TabIndex = 1;
            // 
            // lineasHistorial
            // 
            this.lineasHistorial.BackColor = System.Drawing.Color.White;
            this.lineasHistorial.Controls.Add(this.gridDetalles);
            this.lineasHistorial.Controls.Add(this.lblDetalleDocumento);
            this.lineasHistorial.Controls.Add(this.lblLineasHistorial);
            this.lineasHistorial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lineasHistorial.Location = new System.Drawing.Point(0, 0);
            this.lineasHistorial.Margin = new System.Windows.Forms.Padding(0);
            this.lineasHistorial.Name = "lineasHistorial";
            this.lineasHistorial.Padding = new System.Windows.Forms.Padding(14, 4, 14, 10);
            this.lineasHistorial.Size = new System.Drawing.Size(1088, 242);
            this.lineasHistorial.TabIndex = 0;
            // 
            // gridDetalles
            // 
            this.gridDetalles.AllowUserToAddRows = false;
            this.gridDetalles.AllowUserToDeleteRows = false;
            this.gridDetalles.AllowUserToResizeRows = false;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.gridDetalles.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle8;
            this.gridDetalles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridDetalles.BackgroundColor = System.Drawing.Color.White;
            this.gridDetalles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridDetalles.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridDetalles.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Padding = new System.Windows.Forms.Padding(5, 0, 2, 0);
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridDetalles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.gridDetalles.ColumnHeadersHeight = 35;
            this.gridDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridDetalles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gridDetallesColArticulo,
            this.gridDetallesColCantidad,
            this.gridDetallesColPrecio,
            this.gridDetallesColDescuento,
            this.gridDetallesColIva,
            this.gridDetallesColTotal});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            dataGridViewCellStyle10.Padding = new System.Windows.Forms.Padding(5, 0, 3, 0);
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridDetalles.DefaultCellStyle = dataGridViewCellStyle10;
            this.gridDetalles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDetalles.EnableHeadersVisualStyles = false;
            this.gridDetalles.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(227)))), ((int)(((byte)(238)))));
            this.gridDetalles.Location = new System.Drawing.Point(14, 61);
            this.gridDetalles.Margin = new System.Windows.Forms.Padding(0);
            this.gridDetalles.MultiSelect = false;
            this.gridDetalles.Name = "gridDetalles";
            this.gridDetalles.ReadOnly = true;
            this.gridDetalles.RowHeadersVisible = false;
            this.gridDetalles.RowTemplate.Height = 30;
            this.gridDetalles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridDetalles.Size = new System.Drawing.Size(1060, 171);
            this.gridDetalles.TabIndex = 2;
            // 
            // gridDetallesColArticulo
            // 
            this.gridDetallesColArticulo.DataPropertyName = "Articulo";
            this.gridDetallesColArticulo.FillWeight = 146F;
            this.gridDetallesColArticulo.HeaderText = "Artículo";
            this.gridDetallesColArticulo.MinimumWidth = 110;
            this.gridDetallesColArticulo.Name = "gridDetallesColArticulo";
            this.gridDetallesColArticulo.ReadOnly = true;
            this.gridDetallesColArticulo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridDetallesColCantidad
            // 
            this.gridDetallesColCantidad.DataPropertyName = "Cantidad";
            this.gridDetallesColCantidad.FillWeight = 68F;
            this.gridDetallesColCantidad.HeaderText = "Cantidad";
            this.gridDetallesColCantidad.MinimumWidth = 70;
            this.gridDetallesColCantidad.Name = "gridDetallesColCantidad";
            this.gridDetallesColCantidad.ReadOnly = true;
            this.gridDetallesColCantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridDetallesColPrecio
            // 
            this.gridDetallesColPrecio.DataPropertyName = "Precio";
            this.gridDetallesColPrecio.HeaderText = "Precio unitario";
            this.gridDetallesColPrecio.MinimumWidth = 88;
            this.gridDetallesColPrecio.Name = "gridDetallesColPrecio";
            this.gridDetallesColPrecio.ReadOnly = true;
            this.gridDetallesColPrecio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridDetallesColDescuento
            // 
            this.gridDetallesColDescuento.DataPropertyName = "Descuento";
            this.gridDetallesColDescuento.FillWeight = 95F;
            this.gridDetallesColDescuento.HeaderText = "Descuento";
            this.gridDetallesColDescuento.MinimumWidth = 90;
            this.gridDetallesColDescuento.Name = "gridDetallesColDescuento";
            this.gridDetallesColDescuento.ReadOnly = true;
            this.gridDetallesColDescuento.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridDetallesColIva
            // 
            this.gridDetallesColIva.DataPropertyName = "Iva";
            this.gridDetallesColIva.FillWeight = 83F;
            this.gridDetallesColIva.HeaderText = "IVA";
            this.gridDetallesColIva.MinimumWidth = 78;
            this.gridDetallesColIva.Name = "gridDetallesColIva";
            this.gridDetallesColIva.ReadOnly = true;
            this.gridDetallesColIva.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridDetallesColTotal
            // 
            this.gridDetallesColTotal.DataPropertyName = "Total";
            this.gridDetallesColTotal.FillWeight = 95F;
            this.gridDetallesColTotal.HeaderText = "Total";
            this.gridDetallesColTotal.MinimumWidth = 84;
            this.gridDetallesColTotal.Name = "gridDetallesColTotal";
            this.gridDetallesColTotal.ReadOnly = true;
            this.gridDetallesColTotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lblDetalleDocumento
            // 
            this.lblDetalleDocumento.AutoEllipsis = true;
            this.lblDetalleDocumento.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetalleDocumento.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDetalleDocumento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblDetalleDocumento.Location = new System.Drawing.Point(14, 36);
            this.lblDetalleDocumento.Margin = new System.Windows.Forms.Padding(0);
            this.lblDetalleDocumento.Name = "lblDetalleDocumento";
            this.lblDetalleDocumento.Size = new System.Drawing.Size(1060, 25);
            this.lblDetalleDocumento.TabIndex = 1;
            this.lblDetalleDocumento.Text = "Selecciona un documento para consultar sus líneas.";
            this.lblDetalleDocumento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLineasHistorial
            // 
            this.lblLineasHistorial.AutoEllipsis = true;
            this.lblLineasHistorial.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLineasHistorial.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblLineasHistorial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblLineasHistorial.Location = new System.Drawing.Point(14, 4);
            this.lblLineasHistorial.Margin = new System.Windows.Forms.Padding(0);
            this.lblLineasHistorial.Name = "lblLineasHistorial";
            this.lblLineasHistorial.Size = new System.Drawing.Size(1060, 32);
            this.lblLineasHistorial.TabIndex = 0;
            this.lblLineasHistorial.Text = "Detalle del documento";
            this.lblLineasHistorial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmFacturacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1160, 720);
            this.Controls.Add(this.tabsFacturacion);
            this.Controls.Add(this.encabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.MinimumSize = new System.Drawing.Size(800, 560);
            this.Name = "FrmFacturacion";
            this.Padding = new System.Windows.Forms.Padding(20, 8, 20, 16);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Facturación y cotizaciones";
            ((System.ComponentModel.ISupportInitialize)(this.errores)).EndInit();
            this.encabezado.ResumeLayout(false);
            this.tabsFacturacion.ResumeLayout(false);
            this.tabNuevo.ResumeLayout(false);
            this.detalleDocumento.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCarrito)).EndInit();
            this.resumenTotales.ResumeLayout(false);
            this.camposPartida.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDescuento)).EndInit();
            this.editorDocumento.ResumeLayout(false);
            this.camposDocumento.ResumeLayout(false);
            this.camposDocumento.PerformLayout();
            this.tabHistorial.ResumeLayout(false);
            this.detalleHistorial.ResumeLayout(false);
            this.panelDocumentos.ResumeLayout(false);
            this.documentosRegistrados.ResumeLayout(false);
            this.documentosRegistrados.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridHistorial)).EndInit();
            this.documentosRegistradosAcciones.ResumeLayout(false);
            this.panelLineas.ResumeLayout(false);
            this.lineasHistorial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDetalles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel encabezado;
        private Label lblTitulo;
        private Panel acentoTitulo;
        private TabControl tabsFacturacion;
        private TabPage tabNuevo;
        private TabPage tabHistorial;
        private Panel editorDocumento;
        private ComboBox cmbTipo;
        private ComboBox cmbCliente;
        private ComboBox cmbEmpleado;
        private DateTimePicker dtpFecha;
        private ComboBox cmbEstadoPago;
        private ComboBox cmbVehiculo;
        private ComboBox cmbAgenda;
        private CheckBox chkDomicilio;
        private TextBox txtDireccion;
        private TextBox txtObservaciones;
        private TableLayoutPanel camposDocumento;
        private Label lblCmbTipo;
        private Label lblCmbCliente;
        private Label lblCmbEmpleado;
        private Label lblDtpFecha;
        private Label lblCmbEstadoPago;
        private Label lblCmbVehiculo;
        private Label lblCmbAgenda;
        private Label lblTxtDireccion;
        private Label lblTxtObservaciones;
        private Panel espacioDocumento;
        private Panel detalleDocumento;
        private Label lblPartidas;
        private ComboBox cmbArticulo;
        private NumericUpDown nudCantidad;
        private NumericUpDown nudDescuento;
        private TableLayoutPanel camposPartida;
        private Label lblCmbArticulo;
        private Label lblNudCantidad;
        private Label lblNudDescuento;
        private Button btnAgregar;
        private Button btnQuitar;
        private TableLayoutPanel resumenTotales;
        private Label lblSubtotal;
        private Label lblIva;
        private Label lblDescuento;
        private Label lblTotal;
        private Button btnNuevo;
        private Button btnGuardar;
        private DataGridView gridCarrito;
        private TableLayoutPanel detalleHistorial;
        private Panel panelDocumentos;
        private Panel panelLineas;
        private Button btnActualizar;
        private Button btnConvertir;
        private Button btnAnular;
        private Panel documentosRegistrados;
        private Label documentosRegistradosTitulo;
        private FlowLayoutPanel documentosRegistradosAcciones;
        private DataGridView gridHistorial;
        private DataGridViewTextBoxColumn gridHistorialColTipo;
        private DataGridViewTextBoxColumn gridHistorialColSubtotal;
        private DataGridViewTextBoxColumn gridHistorialColIva;
        private DataGridViewTextBoxColumn gridHistorialColDescuento;
        private DataGridViewTextBoxColumn gridHistorialColTotal;
        private Panel lineasHistorial;
        private Label lblLineasHistorial;
        private Label lblDetalleDocumento;
        private DataGridView gridDetalles;
        private DataGridViewTextBoxColumn gridDetallesColArticulo;
        private DataGridViewTextBoxColumn gridDetallesColCantidad;
        private DataGridViewTextBoxColumn gridDetallesColPrecio;
        private DataGridViewTextBoxColumn gridDetallesColDescuento;
        private DataGridViewTextBoxColumn gridDetallesColIva;
        private DataGridViewTextBoxColumn gridDetallesColTotal;
        private DataGridViewTextBoxColumn Articulo;
        private DataGridViewTextBoxColumn Tipo;
        private DataGridViewTextBoxColumn Cantidad;
        private DataGridViewTextBoxColumn Precio;
        private DataGridViewTextBoxColumn PorcentajeIVA;
        private DataGridViewTextBoxColumn Descuento;
        private DataGridViewTextBoxColumn Subtotal;
        private DataGridViewTextBoxColumn Iva;
        private DataGridViewTextBoxColumn Total;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn Fecha;
        private DataGridViewTextBoxColumn Cliente;
        private DataGridViewTextBoxColumn Empleado;
        private DataGridViewTextBoxColumn Vehiculo;
        private DataGridViewTextBoxColumn EstadoPago;
    }
}
