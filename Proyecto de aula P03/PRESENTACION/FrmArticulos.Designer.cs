using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PRESENTACION
{
    partial class FrmArticulos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.errores = new System.Windows.Forms.ErrorProvider(this.components);
            this.encabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.acentoTitulo = new System.Windows.Forms.Panel();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.cmbIva = new System.Windows.Forms.ComboBox();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.txtDuracion = new System.Windows.Forms.TextBox();
            this.cmbProveedor = new System.Windows.Forms.ComboBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnProveedores = new System.Windows.Forms.Button();
            this.editorArticulo = new System.Windows.Forms.Panel();
            this.editorArticuloAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.editorArticuloCampos = new System.Windows.Forms.TableLayoutPanel();
            this.lblTxtNombre = new System.Windows.Forms.Label();
            this.lblCmbTipo = new System.Windows.Forms.Label();
            this.lblTxtPrecio = new System.Windows.Forms.Label();
            this.lblCmbIva = new System.Windows.Forms.Label();
            this.lblStock = new System.Windows.Forms.Label();
            this.lblDuracion = new System.Windows.Forms.Label();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.lblTxtDescripcion = new System.Windows.Forms.Label();
            this.editorArticuloTitulo = new System.Windows.Forms.Label();
            this.espacioArticulos = new System.Windows.Forms.Panel();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.cmbFiltroTipo = new System.Windows.Forms.ComboBox();
            this.listadoArticulos = new System.Windows.Forms.Panel();
            this.grid = new System.Windows.Forms.DataGridView();
            this.gridColArticuloId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColPorcentajeIVA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColDuracion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColProveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listadoArticulosFiltros = new System.Windows.Forms.TableLayoutPanel();
            this.lblTxtBuscar = new System.Windows.Forms.Label();
            this.lblCmbFiltroTipo = new System.Windows.Forms.Label();
            this.listadoArticulosTitulo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errores)).BeginInit();
            this.encabezado.SuspendLayout();
            this.editorArticulo.SuspendLayout();
            this.editorArticuloAcciones.SuspendLayout();
            this.editorArticuloCampos.SuspendLayout();
            this.listadoArticulos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.listadoArticulosFiltros.SuspendLayout();
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
            this.lblTitulo.Text = "Inventario de artículos";
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
            // txtNombre
            // 
            this.txtNombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtNombre.Location = new System.Drawing.Point(6, 23);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtNombre.MaxLength = 100;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(251, 23);
            this.txtNombre.TabIndex = 1;
            // 
            // cmbTipo
            // 
            this.cmbTipo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.cmbTipo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.cmbTipo.Items.AddRange(new object[] {
            "Repuesto",
            "Servicio"});
            this.cmbTipo.Location = new System.Drawing.Point(269, 23);
            this.cmbTipo.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(251, 23);
            this.cmbTipo.TabIndex = 3;
            // 
            // txtPrecio
            // 
            this.txtPrecio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtPrecio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrecio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPrecio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtPrecio.Location = new System.Drawing.Point(532, 23);
            this.txtPrecio.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtPrecio.MaxLength = 20;
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(251, 23);
            this.txtPrecio.TabIndex = 5;
            // 
            // cmbIva
            // 
            this.cmbIva.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.cmbIva.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbIva.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIva.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.cmbIva.Items.AddRange(new object[] {
            "0",
            "5",
            "19"});
            this.cmbIva.Location = new System.Drawing.Point(795, 23);
            this.cmbIva.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbIva.Name = "cmbIva";
            this.cmbIva.Size = new System.Drawing.Size(251, 23);
            this.cmbIva.TabIndex = 7;
            // 
            // txtStock
            // 
            this.txtStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtStock.Location = new System.Drawing.Point(6, 74);
            this.txtStock.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtStock.MaxLength = 7;
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(251, 23);
            this.txtStock.TabIndex = 9;
            // 
            // txtDuracion
            // 
            this.txtDuracion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtDuracion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDuracion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDuracion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtDuracion.Location = new System.Drawing.Point(269, 74);
            this.txtDuracion.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtDuracion.MaxLength = 5;
            this.txtDuracion.Name = "txtDuracion";
            this.txtDuracion.Size = new System.Drawing.Size(251, 23);
            this.txtDuracion.TabIndex = 11;
            // 
            // cmbProveedor
            // 
            this.cmbProveedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.editorArticuloCampos.SetColumnSpan(this.cmbProveedor, 2);
            this.cmbProveedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProveedor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.cmbProveedor.Location = new System.Drawing.Point(532, 74);
            this.cmbProveedor.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbProveedor.Name = "cmbProveedor";
            this.cmbProveedor.Size = new System.Drawing.Size(514, 23);
            this.cmbProveedor.TabIndex = 13;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editorArticuloCampos.SetColumnSpan(this.txtDescripcion, 4);
            this.txtDescripcion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtDescripcion.Location = new System.Drawing.Point(6, 125);
            this.txtDescripcion.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtDescripcion.MaxLength = 200;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescripcion.Size = new System.Drawing.Size(1040, 34);
            this.txtDescripcion.TabIndex = 15;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(6, 9);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(112, 32);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.btnNuevo.Location = new System.Drawing.Point(126, 9);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(86, 32);
            this.btnNuevo.TabIndex = 1;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEliminar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.btnEliminar.Location = new System.Drawing.Point(220, 9);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(90, 32);
            this.btnEliminar.TabIndex = 2;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            // 
            // btnProveedores
            // 
            this.btnProveedores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.btnProveedores.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProveedores.FlatAppearance.BorderSize = 0;
            this.btnProveedores.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.btnProveedores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProveedores.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnProveedores.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.btnProveedores.Location = new System.Drawing.Point(318, 9);
            this.btnProveedores.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnProveedores.Name = "btnProveedores";
            this.btnProveedores.Size = new System.Drawing.Size(172, 32);
            this.btnProveedores.TabIndex = 3;
            this.btnProveedores.Text = "Gestionar proveedores";
            this.btnProveedores.UseVisualStyleBackColor = false;
            // 
            // editorArticulo
            // 
            this.editorArticulo.AutoSize = true;
            this.editorArticulo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editorArticulo.BackColor = System.Drawing.Color.White;
            this.editorArticulo.Controls.Add(this.editorArticuloAcciones);
            this.editorArticulo.Controls.Add(this.editorArticuloCampos);
            this.editorArticulo.Controls.Add(this.editorArticuloTitulo);
            this.editorArticulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorArticulo.Location = new System.Drawing.Point(20, 65);
            this.editorArticulo.Margin = new System.Windows.Forms.Padding(0);
            this.editorArticulo.MinimumSize = new System.Drawing.Size(0, 252);
            this.editorArticulo.Name = "editorArticulo";
            this.editorArticulo.Padding = new System.Windows.Forms.Padding(14, 4, 14, 10);
            this.editorArticulo.Size = new System.Drawing.Size(1080, 252);
            this.editorArticulo.TabIndex = 1;
            // 
            // editorArticuloAcciones
            // 
            this.editorArticuloAcciones.AutoSize = true;
            this.editorArticuloAcciones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editorArticuloAcciones.Controls.Add(this.btnGuardar);
            this.editorArticuloAcciones.Controls.Add(this.btnNuevo);
            this.editorArticuloAcciones.Controls.Add(this.btnEliminar);
            this.editorArticuloAcciones.Controls.Add(this.btnProveedores);
            this.editorArticuloAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorArticuloAcciones.Location = new System.Drawing.Point(14, 196);
            this.editorArticuloAcciones.Name = "editorArticuloAcciones";
            this.editorArticuloAcciones.Padding = new System.Windows.Forms.Padding(6, 6, 0, 0);
            this.editorArticuloAcciones.Size = new System.Drawing.Size(1052, 44);
            this.editorArticuloAcciones.TabIndex = 2;
            // 
            // editorArticuloCampos
            // 
            this.editorArticuloCampos.ColumnCount = 4;
            this.editorArticuloCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.editorArticuloCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.editorArticuloCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.editorArticuloCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.editorArticuloCampos.Controls.Add(this.lblTxtNombre, 0, 0);
            this.editorArticuloCampos.Controls.Add(this.txtNombre, 0, 1);
            this.editorArticuloCampos.Controls.Add(this.lblCmbTipo, 1, 0);
            this.editorArticuloCampos.Controls.Add(this.cmbTipo, 1, 1);
            this.editorArticuloCampos.Controls.Add(this.lblTxtPrecio, 2, 0);
            this.editorArticuloCampos.Controls.Add(this.txtPrecio, 2, 1);
            this.editorArticuloCampos.Controls.Add(this.lblCmbIva, 3, 0);
            this.editorArticuloCampos.Controls.Add(this.cmbIva, 3, 1);
            this.editorArticuloCampos.Controls.Add(this.lblStock, 0, 2);
            this.editorArticuloCampos.Controls.Add(this.txtStock, 0, 3);
            this.editorArticuloCampos.Controls.Add(this.lblDuracion, 1, 2);
            this.editorArticuloCampos.Controls.Add(this.txtDuracion, 1, 3);
            this.editorArticuloCampos.Controls.Add(this.lblProveedor, 2, 2);
            this.editorArticuloCampos.Controls.Add(this.cmbProveedor, 2, 3);
            this.editorArticuloCampos.Controls.Add(this.lblTxtDescripcion, 0, 4);
            this.editorArticuloCampos.Controls.Add(this.txtDescripcion, 0, 5);
            this.editorArticuloCampos.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorArticuloCampos.Location = new System.Drawing.Point(14, 36);
            this.editorArticuloCampos.Margin = new System.Windows.Forms.Padding(0);
            this.editorArticuloCampos.Name = "editorArticuloCampos";
            this.editorArticuloCampos.RowCount = 6;
            this.editorArticuloCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.editorArticuloCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.editorArticuloCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.editorArticuloCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.editorArticuloCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.editorArticuloCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.editorArticuloCampos.Size = new System.Drawing.Size(1052, 160);
            this.editorArticuloCampos.TabIndex = 1;
            // 
            // lblTxtNombre
            // 
            this.lblTxtNombre.AutoEllipsis = true;
            this.lblTxtNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtNombre.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtNombre.Location = new System.Drawing.Point(6, 0);
            this.lblTxtNombre.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtNombre.Name = "lblTxtNombre";
            this.lblTxtNombre.Size = new System.Drawing.Size(251, 20);
            this.lblTxtNombre.TabIndex = 0;
            this.lblTxtNombre.Text = "Nombre";
            this.lblTxtNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCmbTipo
            // 
            this.lblCmbTipo.AutoEllipsis = true;
            this.lblCmbTipo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCmbTipo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCmbTipo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCmbTipo.Location = new System.Drawing.Point(269, 0);
            this.lblCmbTipo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCmbTipo.Name = "lblCmbTipo";
            this.lblCmbTipo.Size = new System.Drawing.Size(251, 20);
            this.lblCmbTipo.TabIndex = 2;
            this.lblCmbTipo.Text = "Tipo";
            this.lblCmbTipo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTxtPrecio
            // 
            this.lblTxtPrecio.AutoEllipsis = true;
            this.lblTxtPrecio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtPrecio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtPrecio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtPrecio.Location = new System.Drawing.Point(532, 0);
            this.lblTxtPrecio.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtPrecio.Name = "lblTxtPrecio";
            this.lblTxtPrecio.Size = new System.Drawing.Size(251, 20);
            this.lblTxtPrecio.TabIndex = 4;
            this.lblTxtPrecio.Text = "Precio";
            this.lblTxtPrecio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCmbIva
            // 
            this.lblCmbIva.AutoEllipsis = true;
            this.lblCmbIva.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCmbIva.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCmbIva.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCmbIva.Location = new System.Drawing.Point(795, 0);
            this.lblCmbIva.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCmbIva.Name = "lblCmbIva";
            this.lblCmbIva.Size = new System.Drawing.Size(251, 20);
            this.lblCmbIva.TabIndex = 6;
            this.lblCmbIva.Text = "IVA (%)";
            this.lblCmbIva.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStock
            // 
            this.lblStock.AutoEllipsis = true;
            this.lblStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStock.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblStock.Location = new System.Drawing.Point(6, 51);
            this.lblStock.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(251, 20);
            this.lblStock.TabIndex = 8;
            this.lblStock.Text = "Stock";
            this.lblStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDuracion
            // 
            this.lblDuracion.AutoEllipsis = true;
            this.lblDuracion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDuracion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDuracion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblDuracion.Location = new System.Drawing.Point(269, 51);
            this.lblDuracion.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDuracion.Name = "lblDuracion";
            this.lblDuracion.Size = new System.Drawing.Size(251, 20);
            this.lblDuracion.TabIndex = 10;
            this.lblDuracion.Text = "Duración (min)";
            this.lblDuracion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProveedor
            // 
            this.lblProveedor.AutoEllipsis = true;
            this.editorArticuloCampos.SetColumnSpan(this.lblProveedor, 2);
            this.lblProveedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProveedor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblProveedor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblProveedor.Location = new System.Drawing.Point(532, 51);
            this.lblProveedor.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(514, 20);
            this.lblProveedor.TabIndex = 12;
            this.lblProveedor.Text = "Proveedor";
            this.lblProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTxtDescripcion
            // 
            this.lblTxtDescripcion.AutoEllipsis = true;
            this.editorArticuloCampos.SetColumnSpan(this.lblTxtDescripcion, 4);
            this.lblTxtDescripcion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtDescripcion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtDescripcion.Location = new System.Drawing.Point(6, 102);
            this.lblTxtDescripcion.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtDescripcion.Name = "lblTxtDescripcion";
            this.lblTxtDescripcion.Size = new System.Drawing.Size(1040, 20);
            this.lblTxtDescripcion.TabIndex = 14;
            this.lblTxtDescripcion.Text = "Descripción";
            this.lblTxtDescripcion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // editorArticuloTitulo
            // 
            this.editorArticuloTitulo.AutoEllipsis = true;
            this.editorArticuloTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorArticuloTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.editorArticuloTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.editorArticuloTitulo.Location = new System.Drawing.Point(14, 4);
            this.editorArticuloTitulo.Margin = new System.Windows.Forms.Padding(0);
            this.editorArticuloTitulo.Name = "editorArticuloTitulo";
            this.editorArticuloTitulo.Size = new System.Drawing.Size(1052, 32);
            this.editorArticuloTitulo.TabIndex = 0;
            this.editorArticuloTitulo.Text = "Datos del artículo";
            this.editorArticuloTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // espacioArticulos
            // 
            this.espacioArticulos.Dock = System.Windows.Forms.DockStyle.Top;
            this.espacioArticulos.Location = new System.Drawing.Point(20, 317);
            this.espacioArticulos.Name = "espacioArticulos";
            this.espacioArticulos.Size = new System.Drawing.Size(1080, 10);
            this.espacioArticulos.TabIndex = 2;
            // 
            // txtBuscar
            // 
            this.txtBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtBuscar.Location = new System.Drawing.Point(6, 20);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtBuscar.MaxLength = 100;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(619, 23);
            this.txtBuscar.TabIndex = 1;
            // 
            // cmbFiltroTipo
            // 
            this.cmbFiltroTipo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.cmbFiltroTipo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbFiltroTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroTipo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.cmbFiltroTipo.Items.AddRange(new object[] {
            "Todos",
            "Repuesto",
            "Servicio"});
            this.cmbFiltroTipo.Location = new System.Drawing.Point(637, 20);
            this.cmbFiltroTipo.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbFiltroTipo.Name = "cmbFiltroTipo";
            this.cmbFiltroTipo.Size = new System.Drawing.Size(409, 23);
            this.cmbFiltroTipo.TabIndex = 3;
            // 
            // listadoArticulos
            // 
            this.listadoArticulos.BackColor = System.Drawing.Color.White;
            this.listadoArticulos.Controls.Add(this.grid);
            this.listadoArticulos.Controls.Add(this.listadoArticulosFiltros);
            this.listadoArticulos.Controls.Add(this.listadoArticulosTitulo);
            this.listadoArticulos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listadoArticulos.Location = new System.Drawing.Point(20, 327);
            this.listadoArticulos.Margin = new System.Windows.Forms.Padding(0);
            this.listadoArticulos.Name = "listadoArticulos";
            this.listadoArticulos.Padding = new System.Windows.Forms.Padding(14, 4, 14, 12);
            this.listadoArticulos.Size = new System.Drawing.Size(1080, 337);
            this.listadoArticulos.TabIndex = 3;
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.BackgroundColor = System.Drawing.Color.White;
            this.grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5, 0, 2, 0);
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grid.ColumnHeadersHeight = 35;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gridColArticuloId,
            this.gridColNombre,
            this.gridColTipo,
            this.gridColPrecio,
            this.gridColPorcentajeIVA,
            this.gridColStock,
            this.gridColDuracion,
            this.gridColProveedor,
            this.gridColDescripcion});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(5, 0, 3, 0);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid.DefaultCellStyle = dataGridViewCellStyle4;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.EnableHeadersVisualStyles = false;
            this.grid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(227)))), ((int)(((byte)(238)))));
            this.grid.Location = new System.Drawing.Point(14, 82);
            this.grid.Margin = new System.Windows.Forms.Padding(0);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersVisible = false;
            this.grid.RowTemplate.Height = 30;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(1052, 243);
            this.grid.TabIndex = 2;
            // 
            // gridColArticuloId
            // 
            this.gridColArticuloId.DataPropertyName = "ArticuloId";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.gridColArticuloId.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridColArticuloId.FillWeight = 47F;
            this.gridColArticuloId.HeaderText = "ID";
            this.gridColArticuloId.MinimumWidth = 44;
            this.gridColArticuloId.Name = "gridColArticuloId";
            this.gridColArticuloId.ReadOnly = true;
            this.gridColArticuloId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridColNombre
            // 
            this.gridColNombre.DataPropertyName = "Nombre";
            this.gridColNombre.FillWeight = 128F;
            this.gridColNombre.HeaderText = "Nombre";
            this.gridColNombre.MinimumWidth = 90;
            this.gridColNombre.Name = "gridColNombre";
            this.gridColNombre.ReadOnly = true;
            this.gridColNombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridColTipo
            // 
            this.gridColTipo.DataPropertyName = "Tipo";
            this.gridColTipo.FillWeight = 85F;
            this.gridColTipo.HeaderText = "Tipo";
            this.gridColTipo.MinimumWidth = 74;
            this.gridColTipo.Name = "gridColTipo";
            this.gridColTipo.ReadOnly = true;
            this.gridColTipo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridColPrecio
            // 
            this.gridColPrecio.DataPropertyName = "Precio";
            this.gridColPrecio.FillWeight = 93F;
            this.gridColPrecio.HeaderText = "Precio";
            this.gridColPrecio.MinimumWidth = 84;
            this.gridColPrecio.Name = "gridColPrecio";
            this.gridColPrecio.ReadOnly = true;
            this.gridColPrecio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridColPorcentajeIVA
            // 
            this.gridColPorcentajeIVA.DataPropertyName = "PorcentajeIVA";
            this.gridColPorcentajeIVA.FillWeight = 60F;
            this.gridColPorcentajeIVA.HeaderText = "IVA %";
            this.gridColPorcentajeIVA.MinimumWidth = 52;
            this.gridColPorcentajeIVA.Name = "gridColPorcentajeIVA";
            this.gridColPorcentajeIVA.ReadOnly = true;
            this.gridColPorcentajeIVA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridColStock
            // 
            this.gridColStock.DataPropertyName = "Stock";
            this.gridColStock.FillWeight = 63F;
            this.gridColStock.HeaderText = "Stock";
            this.gridColStock.MinimumWidth = 55;
            this.gridColStock.Name = "gridColStock";
            this.gridColStock.ReadOnly = true;
            this.gridColStock.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridColDuracion
            // 
            this.gridColDuracion.DataPropertyName = "Duracion";
            this.gridColDuracion.FillWeight = 75F;
            this.gridColDuracion.HeaderText = "Duración";
            this.gridColDuracion.MinimumWidth = 73;
            this.gridColDuracion.Name = "gridColDuracion";
            this.gridColDuracion.ReadOnly = true;
            this.gridColDuracion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridColProveedor
            // 
            this.gridColProveedor.DataPropertyName = "Proveedor";
            this.gridColProveedor.FillWeight = 110F;
            this.gridColProveedor.HeaderText = "Proveedor";
            this.gridColProveedor.MinimumWidth = 90;
            this.gridColProveedor.Name = "gridColProveedor";
            this.gridColProveedor.ReadOnly = true;
            this.gridColProveedor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridColDescripcion
            // 
            this.gridColDescripcion.DataPropertyName = "Descripcion";
            this.gridColDescripcion.FillWeight = 159F;
            this.gridColDescripcion.HeaderText = "Descripción";
            this.gridColDescripcion.MinimumWidth = 105;
            this.gridColDescripcion.Name = "gridColDescripcion";
            this.gridColDescripcion.ReadOnly = true;
            this.gridColDescripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // listadoArticulosFiltros
            // 
            this.listadoArticulosFiltros.ColumnCount = 2;
            this.listadoArticulosFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.listadoArticulosFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.listadoArticulosFiltros.Controls.Add(this.lblTxtBuscar, 0, 0);
            this.listadoArticulosFiltros.Controls.Add(this.txtBuscar, 0, 1);
            this.listadoArticulosFiltros.Controls.Add(this.lblCmbFiltroTipo, 1, 0);
            this.listadoArticulosFiltros.Controls.Add(this.cmbFiltroTipo, 1, 1);
            this.listadoArticulosFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.listadoArticulosFiltros.Location = new System.Drawing.Point(14, 36);
            this.listadoArticulosFiltros.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.listadoArticulosFiltros.Name = "listadoArticulosFiltros";
            this.listadoArticulosFiltros.RowCount = 2;
            this.listadoArticulosFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.listadoArticulosFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.listadoArticulosFiltros.Size = new System.Drawing.Size(1052, 46);
            this.listadoArticulosFiltros.TabIndex = 1;
            // 
            // lblTxtBuscar
            // 
            this.lblTxtBuscar.AutoEllipsis = true;
            this.lblTxtBuscar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtBuscar.Location = new System.Drawing.Point(6, 0);
            this.lblTxtBuscar.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtBuscar.Name = "lblTxtBuscar";
            this.lblTxtBuscar.Size = new System.Drawing.Size(619, 17);
            this.lblTxtBuscar.TabIndex = 0;
            this.lblTxtBuscar.Text = "Buscar por nombre";
            this.lblTxtBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCmbFiltroTipo
            // 
            this.lblCmbFiltroTipo.AutoEllipsis = true;
            this.lblCmbFiltroTipo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCmbFiltroTipo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCmbFiltroTipo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCmbFiltroTipo.Location = new System.Drawing.Point(637, 0);
            this.lblCmbFiltroTipo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCmbFiltroTipo.Name = "lblCmbFiltroTipo";
            this.lblCmbFiltroTipo.Size = new System.Drawing.Size(409, 17);
            this.lblCmbFiltroTipo.TabIndex = 2;
            this.lblCmbFiltroTipo.Text = "Filtrar por tipo";
            this.lblCmbFiltroTipo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listadoArticulosTitulo
            // 
            this.listadoArticulosTitulo.AutoEllipsis = true;
            this.listadoArticulosTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.listadoArticulosTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.listadoArticulosTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.listadoArticulosTitulo.Location = new System.Drawing.Point(14, 4);
            this.listadoArticulosTitulo.Margin = new System.Windows.Forms.Padding(0);
            this.listadoArticulosTitulo.Name = "listadoArticulosTitulo";
            this.listadoArticulosTitulo.Size = new System.Drawing.Size(1052, 32);
            this.listadoArticulosTitulo.TabIndex = 0;
            this.listadoArticulosTitulo.Text = "Artículos registrados";
            this.listadoArticulosTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmArticulos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1120, 680);
            this.Controls.Add(this.listadoArticulos);
            this.Controls.Add(this.espacioArticulos);
            this.Controls.Add(this.editorArticulo);
            this.Controls.Add(this.encabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.MinimumSize = new System.Drawing.Size(680, 440);
            this.Name = "FrmArticulos";
            this.Padding = new System.Windows.Forms.Padding(20, 8, 20, 16);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Inventario de artículos";
            ((System.ComponentModel.ISupportInitialize)(this.errores)).EndInit();
            this.encabezado.ResumeLayout(false);
            this.editorArticulo.ResumeLayout(false);
            this.editorArticulo.PerformLayout();
            this.editorArticuloAcciones.ResumeLayout(false);
            this.editorArticuloCampos.ResumeLayout(false);
            this.editorArticuloCampos.PerformLayout();
            this.listadoArticulos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.listadoArticulosFiltros.ResumeLayout(false);
            this.listadoArticulosFiltros.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel encabezado;
        private Label lblTitulo;
        private Panel acentoTitulo;
        private TextBox txtNombre;
        private ComboBox cmbTipo;
        private TextBox txtPrecio;
        private ComboBox cmbIva;
        private TextBox txtStock;
        private TextBox txtDuracion;
        private ComboBox cmbProveedor;
        private TextBox txtDescripcion;
        private Button btnGuardar;
        private Button btnNuevo;
        private Button btnEliminar;
        private Button btnProveedores;
        private Panel editorArticulo;
        private TableLayoutPanel editorArticuloCampos;
        private Label lblTxtNombre;
        private Label lblCmbTipo;
        private Label lblTxtPrecio;
        private Label lblCmbIva;
        private Label lblStock;
        private Label lblDuracion;
        private Label lblProveedor;
        private Label lblTxtDescripcion;
        private FlowLayoutPanel editorArticuloAcciones;
        private Label editorArticuloTitulo;
        private Panel espacioArticulos;
        private TextBox txtBuscar;
        private ComboBox cmbFiltroTipo;
        private Panel listadoArticulos;
        private Label listadoArticulosTitulo;
        private TableLayoutPanel listadoArticulosFiltros;
        private Label lblTxtBuscar;
        private Label lblCmbFiltroTipo;
        private DataGridView grid;
        private DataGridViewTextBoxColumn gridColArticuloId;
        private DataGridViewTextBoxColumn gridColNombre;
        private DataGridViewTextBoxColumn gridColTipo;
        private DataGridViewTextBoxColumn gridColPrecio;
        private DataGridViewTextBoxColumn gridColPorcentajeIVA;
        private DataGridViewTextBoxColumn gridColStock;
        private DataGridViewTextBoxColumn gridColDuracion;
        private DataGridViewTextBoxColumn gridColProveedor;
        private DataGridViewTextBoxColumn gridColDescripcion;
    }
}
