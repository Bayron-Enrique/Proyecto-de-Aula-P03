using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PRESENTACION
{
    partial class FrmProveedores
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
            this.errores = new System.Windows.Forms.ErrorProvider(this.components);
            this.encabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.acentoTitulo = new System.Windows.Forms.Panel();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.txtNit = new System.Windows.Forms.TextBox();
            this.txtRazonSocial = new System.Windows.Forms.TextBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.editorProveedor = new System.Windows.Forms.Panel();
            this.editorProveedorAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.editorProveedorCampos = new System.Windows.Forms.TableLayoutPanel();
            this.lblTxtNit = new System.Windows.Forms.Label();
            this.lblTxtRazonSocial = new System.Windows.Forms.Label();
            this.lblTxtTelefono = new System.Windows.Forms.Label();
            this.editorProveedorTitulo = new System.Windows.Forms.Label();
            this.espacioProveedores = new System.Windows.Forms.Panel();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.listadoProveedores = new System.Windows.Forms.Panel();
            this.grid = new System.Windows.Forms.DataGridView();
            this.gridColNit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColRazonSocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColTelefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listadoProveedoresFiltros = new System.Windows.Forms.TableLayoutPanel();
            this.lblTxtBuscar = new System.Windows.Forms.Label();
            this.listadoProveedoresTitulo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errores)).BeginInit();
            this.encabezado.SuspendLayout();
            this.panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.editorProveedor.SuspendLayout();
            this.editorProveedorAcciones.SuspendLayout();
            this.editorProveedorCampos.SuspendLayout();
            this.listadoProveedores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.listadoProveedoresFiltros.SuspendLayout();
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
            this.encabezado.Controls.Add(this.panelLogo);
            this.encabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.encabezado.Location = new System.Drawing.Point(20, 8);
            this.encabezado.Margin = new System.Windows.Forms.Padding(0);
            this.encabezado.Name = "encabezado";
            this.encabezado.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.encabezado.Size = new System.Drawing.Size(940, 68);
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
            this.lblTitulo.Size = new System.Drawing.Size(782, 48);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Proveedores";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // acentoTitulo
            // 
            this.acentoTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.acentoTitulo.Dock = System.Windows.Forms.DockStyle.Left;
            this.acentoTitulo.Location = new System.Drawing.Point(0, 10);
            this.acentoTitulo.Name = "acentoTitulo";
            this.acentoTitulo.Size = new System.Drawing.Size(4, 48);
            this.acentoTitulo.TabIndex = 1;
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.panelLogo.Controls.Add(this.picLogo);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelLogo.Location = new System.Drawing.Point(786, 10);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Padding = new System.Windows.Forms.Padding(10, 2, 10, 2);
            this.panelLogo.Size = new System.Drawing.Size(154, 48);
            this.panelLogo.TabIndex = 2;
            // 
            // picLogo
            // 
            this.picLogo.AccessibleName = "GOT-AUTO";
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picLogo.Image = global::PRESENTACION.Properties.Resources.LogotipoGotAuto;
            this.picLogo.Location = new System.Drawing.Point(10, 2);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(134, 44);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // txtNit
            // 
            this.txtNit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtNit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNit.Location = new System.Drawing.Point(6, 23);
            this.txtNit.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtNit.MaxLength = 15;
            this.txtNit.Name = "txtNit";
            this.txtNit.Size = new System.Drawing.Size(216, 27);
            this.txtNit.TabIndex = 1;
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtRazonSocial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRazonSocial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRazonSocial.Location = new System.Drawing.Point(234, 23);
            this.txtRazonSocial.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtRazonSocial.MaxLength = 100;
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.Size = new System.Drawing.Size(425, 27);
            this.txtRazonSocial.TabIndex = 3;
            // 
            // txtTelefono
            // 
            this.txtTelefono.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtTelefono.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTelefono.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTelefono.Location = new System.Drawing.Point(671, 23);
            this.txtTelefono.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtTelefono.MaxLength = 20;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(235, 27);
            this.txtTelefono.TabIndex = 5;
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
            // editorProveedor
            // 
            this.editorProveedor.AutoSize = true;
            this.editorProveedor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editorProveedor.BackColor = System.Drawing.Color.White;
            this.editorProveedor.Controls.Add(this.editorProveedorAcciones);
            this.editorProveedor.Controls.Add(this.editorProveedorCampos);
            this.editorProveedor.Controls.Add(this.editorProveedorTitulo);
            this.editorProveedor.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorProveedor.Location = new System.Drawing.Point(20, 76);
            this.editorProveedor.Margin = new System.Windows.Forms.Padding(0);
            this.editorProveedor.MinimumSize = new System.Drawing.Size(0, 143);
            this.editorProveedor.Name = "editorProveedor";
            this.editorProveedor.Padding = new System.Windows.Forms.Padding(14, 4, 14, 10);
            this.editorProveedor.Size = new System.Drawing.Size(940, 143);
            this.editorProveedor.TabIndex = 1;
            // 
            // editorProveedorAcciones
            // 
            this.editorProveedorAcciones.AutoSize = true;
            this.editorProveedorAcciones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editorProveedorAcciones.Controls.Add(this.btnGuardar);
            this.editorProveedorAcciones.Controls.Add(this.btnNuevo);
            this.editorProveedorAcciones.Controls.Add(this.btnEliminar);
            this.editorProveedorAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorProveedorAcciones.Location = new System.Drawing.Point(14, 87);
            this.editorProveedorAcciones.Name = "editorProveedorAcciones";
            this.editorProveedorAcciones.Padding = new System.Windows.Forms.Padding(6, 6, 0, 0);
            this.editorProveedorAcciones.Size = new System.Drawing.Size(912, 44);
            this.editorProveedorAcciones.TabIndex = 2;
            // 
            // editorProveedorCampos
            // 
            this.editorProveedorCampos.ColumnCount = 3;
            this.editorProveedorCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.editorProveedorCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.editorProveedorCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.editorProveedorCampos.Controls.Add(this.lblTxtNit, 0, 0);
            this.editorProveedorCampos.Controls.Add(this.txtNit, 0, 1);
            this.editorProveedorCampos.Controls.Add(this.lblTxtRazonSocial, 1, 0);
            this.editorProveedorCampos.Controls.Add(this.txtRazonSocial, 1, 1);
            this.editorProveedorCampos.Controls.Add(this.lblTxtTelefono, 2, 0);
            this.editorProveedorCampos.Controls.Add(this.txtTelefono, 2, 1);
            this.editorProveedorCampos.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorProveedorCampos.Location = new System.Drawing.Point(14, 36);
            this.editorProveedorCampos.Margin = new System.Windows.Forms.Padding(0);
            this.editorProveedorCampos.Name = "editorProveedorCampos";
            this.editorProveedorCampos.RowCount = 2;
            this.editorProveedorCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.editorProveedorCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.editorProveedorCampos.Size = new System.Drawing.Size(912, 51);
            this.editorProveedorCampos.TabIndex = 1;
            // 
            // lblTxtNit
            // 
            this.lblTxtNit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtNit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtNit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtNit.Location = new System.Drawing.Point(6, 0);
            this.lblTxtNit.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtNit.Name = "lblTxtNit";
            this.lblTxtNit.Size = new System.Drawing.Size(216, 20);
            this.lblTxtNit.TabIndex = 0;
            this.lblTxtNit.Text = "NIT";
            this.lblTxtNit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTxtRazonSocial
            // 
            this.lblTxtRazonSocial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtRazonSocial.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtRazonSocial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtRazonSocial.Location = new System.Drawing.Point(234, 0);
            this.lblTxtRazonSocial.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtRazonSocial.Name = "lblTxtRazonSocial";
            this.lblTxtRazonSocial.Size = new System.Drawing.Size(425, 20);
            this.lblTxtRazonSocial.TabIndex = 2;
            this.lblTxtRazonSocial.Text = "Razón social";
            this.lblTxtRazonSocial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTxtTelefono
            // 
            this.lblTxtTelefono.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtTelefono.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtTelefono.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtTelefono.Location = new System.Drawing.Point(671, 0);
            this.lblTxtTelefono.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtTelefono.Name = "lblTxtTelefono";
            this.lblTxtTelefono.Size = new System.Drawing.Size(235, 20);
            this.lblTxtTelefono.TabIndex = 4;
            this.lblTxtTelefono.Text = "Teléfono";
            this.lblTxtTelefono.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // editorProveedorTitulo
            // 
            this.editorProveedorTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorProveedorTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.editorProveedorTitulo.Location = new System.Drawing.Point(14, 4);
            this.editorProveedorTitulo.Name = "editorProveedorTitulo";
            this.editorProveedorTitulo.Size = new System.Drawing.Size(912, 32);
            this.editorProveedorTitulo.TabIndex = 3;
            this.editorProveedorTitulo.Text = "Datos del proveedor";
            this.editorProveedorTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // espacioProveedores
            // 
            this.espacioProveedores.Dock = System.Windows.Forms.DockStyle.Top;
            this.espacioProveedores.Location = new System.Drawing.Point(20, 219);
            this.espacioProveedores.Name = "espacioProveedores";
            this.espacioProveedores.Size = new System.Drawing.Size(940, 10);
            this.espacioProveedores.TabIndex = 2;
            // 
            // txtBuscar
            // 
            this.txtBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBuscar.Location = new System.Drawing.Point(6, 20);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtBuscar.MaxLength = 100;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(580, 27);
            this.txtBuscar.TabIndex = 1;
            // 
            // listadoProveedores
            // 
            this.listadoProveedores.BackColor = System.Drawing.Color.White;
            this.listadoProveedores.Controls.Add(this.grid);
            this.listadoProveedores.Controls.Add(this.listadoProveedoresFiltros);
            this.listadoProveedores.Controls.Add(this.listadoProveedoresTitulo);
            this.listadoProveedores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listadoProveedores.Location = new System.Drawing.Point(20, 229);
            this.listadoProveedores.Name = "listadoProveedores";
            this.listadoProveedores.Padding = new System.Windows.Forms.Padding(14, 4, 14, 12);
            this.listadoProveedores.Size = new System.Drawing.Size(940, 375);
            this.listadoProveedores.TabIndex = 3;
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
            this.gridColNit,
            this.gridColRazonSocial,
            this.gridColTelefono});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(5, 0, 3, 0);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid.DefaultCellStyle = dataGridViewCellStyle3;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.EnableHeadersVisualStyles = false;
            this.grid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(227)))), ((int)(((byte)(238)))));
            this.grid.Location = new System.Drawing.Point(14, 82);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersVisible = false;
            this.grid.RowHeadersWidth = 51;
            this.grid.RowTemplate.Height = 30;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(912, 281);
            this.grid.TabIndex = 2;
            this.grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellContentClick);
            // 
            // gridColNit
            // 
            this.gridColNit.DataPropertyName = "Nit";
            this.gridColNit.FillWeight = 85F;
            this.gridColNit.HeaderText = "NIT";
            this.gridColNit.MinimumWidth = 84;
            this.gridColNit.Name = "gridColNit";
            this.gridColNit.ReadOnly = true;
            this.gridColNit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridColRazonSocial
            // 
            this.gridColRazonSocial.DataPropertyName = "RazonSocial";
            this.gridColRazonSocial.FillWeight = 150F;
            this.gridColRazonSocial.HeaderText = "Razón social";
            this.gridColRazonSocial.MinimumWidth = 120;
            this.gridColRazonSocial.Name = "gridColRazonSocial";
            this.gridColRazonSocial.ReadOnly = true;
            this.gridColRazonSocial.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridColTelefono
            // 
            this.gridColTelefono.DataPropertyName = "Telefono";
            this.gridColTelefono.FillWeight = 75F;
            this.gridColTelefono.HeaderText = "Teléfono";
            this.gridColTelefono.MinimumWidth = 84;
            this.gridColTelefono.Name = "gridColTelefono";
            this.gridColTelefono.ReadOnly = true;
            this.gridColTelefono.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // listadoProveedoresFiltros
            // 
            this.listadoProveedoresFiltros.ColumnCount = 2;
            this.listadoProveedoresFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.listadoProveedoresFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.listadoProveedoresFiltros.Controls.Add(this.lblTxtBuscar, 0, 0);
            this.listadoProveedoresFiltros.Controls.Add(this.txtBuscar, 0, 1);
            this.listadoProveedoresFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.listadoProveedoresFiltros.Location = new System.Drawing.Point(14, 36);
            this.listadoProveedoresFiltros.Name = "listadoProveedoresFiltros";
            this.listadoProveedoresFiltros.RowCount = 2;
            this.listadoProveedoresFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.listadoProveedoresFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.listadoProveedoresFiltros.Size = new System.Drawing.Size(912, 46);
            this.listadoProveedoresFiltros.TabIndex = 1;
            // 
            // lblTxtBuscar
            // 
            this.lblTxtBuscar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtBuscar.Location = new System.Drawing.Point(6, 0);
            this.lblTxtBuscar.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtBuscar.Name = "lblTxtBuscar";
            this.lblTxtBuscar.Size = new System.Drawing.Size(580, 17);
            this.lblTxtBuscar.TabIndex = 0;
            this.lblTxtBuscar.Text = "Buscar por NIT o razón social";
            this.lblTxtBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listadoProveedoresTitulo
            // 
            this.listadoProveedoresTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.listadoProveedoresTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.listadoProveedoresTitulo.Location = new System.Drawing.Point(14, 4);
            this.listadoProveedoresTitulo.Name = "listadoProveedoresTitulo";
            this.listadoProveedoresTitulo.Size = new System.Drawing.Size(912, 32);
            this.listadoProveedoresTitulo.TabIndex = 3;
            this.listadoProveedoresTitulo.Text = "Proveedores registrados";
            this.listadoProveedoresTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmProveedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(980, 620);
            this.Controls.Add(this.listadoProveedores);
            this.Controls.Add(this.espacioProveedores);
            this.Controls.Add(this.editorProveedor);
            this.Controls.Add(this.encabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.MinimumSize = new System.Drawing.Size(680, 440);
            this.Name = "FrmProveedores";
            this.Padding = new System.Windows.Forms.Padding(20, 8, 20, 16);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gestión de proveedores";
            ((System.ComponentModel.ISupportInitialize)(this.errores)).EndInit();
            this.encabezado.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.editorProveedor.ResumeLayout(false);
            this.editorProveedor.PerformLayout();
            this.editorProveedorAcciones.ResumeLayout(false);
            this.editorProveedorCampos.ResumeLayout(false);
            this.editorProveedorCampos.PerformLayout();
            this.listadoProveedores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.listadoProveedoresFiltros.ResumeLayout(false);
            this.listadoProveedoresFiltros.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel encabezado;
        private Label lblTitulo;
        private Panel acentoTitulo;
        private Panel panelLogo;
        private PictureBox picLogo;
        private TextBox txtNit;
        private TextBox txtRazonSocial;
        private TextBox txtTelefono;
        private Button btnGuardar;
        private Button btnNuevo;
        private Button btnEliminar;
        private Panel editorProveedor;
        private TableLayoutPanel editorProveedorCampos;
        private Label lblTxtNit;
        private Label lblTxtRazonSocial;
        private Label lblTxtTelefono;
        private FlowLayoutPanel editorProveedorAcciones;
        private Label editorProveedorTitulo;
        private Panel espacioProveedores;
        private TextBox txtBuscar;
        private Panel listadoProveedores;
        private Label listadoProveedoresTitulo;
        private TableLayoutPanel listadoProveedoresFiltros;
        private Label lblTxtBuscar;
        private DataGridView grid;
        private DataGridViewTextBoxColumn gridColNit;
        private DataGridViewTextBoxColumn gridColRazonSocial;
        private DataGridViewTextBoxColumn gridColTelefono;
    }
}