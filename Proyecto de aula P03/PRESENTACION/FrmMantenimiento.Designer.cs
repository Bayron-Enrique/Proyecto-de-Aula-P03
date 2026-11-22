using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PRESENTACION
{
    partial class FrmMantenimiento
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
            this.cmbVehiculo = new System.Windows.Forms.ComboBox();
            this.cmbArticulo = new System.Windows.Forms.ComboBox();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.txtComentario = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnEstado = new System.Windows.Forms.Button();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.editorAviso = new System.Windows.Forms.Panel();
            this.editorAvisoAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.editorAvisoCampos = new System.Windows.Forms.TableLayoutPanel();
            this.lblCmbVehiculo = new System.Windows.Forms.Label();
            this.lblCmbArticulo = new System.Windows.Forms.Label();
            this.lblDtpFecha = new System.Windows.Forms.Label();
            this.lblCmbEstado = new System.Windows.Forms.Label();
            this.lblTxtComentario = new System.Windows.Forms.Label();
            this.editorAvisoTitulo = new System.Windows.Forms.Label();
            this.espacioRecordatorios = new System.Windows.Forms.Panel();
            this.txtFiltroPlaca = new System.Windows.Forms.TextBox();
            this.listadoRecordatorios = new System.Windows.Forms.Panel();
            this.grid = new System.Windows.Forms.DataGridView();
            this.gridColId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColPlaca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColArticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColComentario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listadoRecordatoriosFiltros = new System.Windows.Forms.TableLayoutPanel();
            this.lblTxtFiltroPlaca = new System.Windows.Forms.Label();
            this.listadoRecordatoriosTitulo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errores)).BeginInit();
            this.encabezado.SuspendLayout();
            this.editorAviso.SuspendLayout();
            this.editorAvisoAcciones.SuspendLayout();
            this.editorAvisoCampos.SuspendLayout();
            this.listadoRecordatorios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.listadoRecordatoriosFiltros.SuspendLayout();
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
            this.encabezado.Size = new System.Drawing.Size(1010, 57);
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
            this.lblTitulo.Size = new System.Drawing.Size(1006, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Notificaciones de mantenimiento";
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
            // cmbVehiculo
            // 
            this.cmbVehiculo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.cmbVehiculo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbVehiculo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVehiculo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.cmbVehiculo.Location = new System.Drawing.Point(6, 23);
            this.cmbVehiculo.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbVehiculo.Name = "cmbVehiculo";
            this.cmbVehiculo.Size = new System.Drawing.Size(243, 28);
            this.cmbVehiculo.TabIndex = 1;
            // 
            // cmbArticulo
            // 
            this.cmbArticulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.cmbArticulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbArticulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbArticulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.cmbArticulo.Location = new System.Drawing.Point(261, 23);
            this.cmbArticulo.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbArticulo.Name = "cmbArticulo";
            this.cmbArticulo.Size = new System.Drawing.Size(243, 28);
            this.cmbArticulo.TabIndex = 3;
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpFecha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(516, 23);
            this.dtpFecha.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.ShowUpDown = true;
            this.dtpFecha.Size = new System.Drawing.Size(223, 27);
            this.dtpFecha.TabIndex = 5;
            // 
            // cmbEstado
            // 
            this.cmbEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.cmbEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.cmbEstado.Items.AddRange(new object[] {
            "PENDIENTE",
            "ENVIADA",
            "COMPLETADA",
            "CANCELADA"});
            this.cmbEstado.Location = new System.Drawing.Point(751, 23);
            this.cmbEstado.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(225, 28);
            this.cmbEstado.TabIndex = 7;
            // 
            // txtComentario
            // 
            this.txtComentario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtComentario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editorAvisoCampos.SetColumnSpan(this.txtComentario, 4);
            this.txtComentario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtComentario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtComentario.Location = new System.Drawing.Point(6, 74);
            this.txtComentario.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtComentario.MaxLength = 200;
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.Size = new System.Drawing.Size(970, 27);
            this.txtComentario.TabIndex = 9;
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
            // btnEstado
            // 
            this.btnEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.btnEstado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEstado.FlatAppearance.BorderSize = 0;
            this.btnEstado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.btnEstado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEstado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.btnEstado.Location = new System.Drawing.Point(318, 9);
            this.btnEstado.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnEstado.Name = "btnEstado";
            this.btnEstado.Size = new System.Drawing.Size(134, 32);
            this.btnEstado.TabIndex = 3;
            this.btnEstado.Text = "Actualizar estado";
            this.btnEstado.UseVisualStyleBackColor = false;
            // 
            // btnEnviar
            // 
            this.btnEnviar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.btnEnviar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnviar.FlatAppearance.BorderSize = 0;
            this.btnEnviar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.btnEnviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnviar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEnviar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.btnEnviar.Location = new System.Drawing.Point(460, 9);
            this.btnEnviar.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(146, 32);
            this.btnEnviar.TabIndex = 4;
            this.btnEnviar.Text = "Enviar recordatorio";
            this.btnEnviar.UseVisualStyleBackColor = false;
            // 
            // editorAviso
            // 
            this.editorAviso.AutoSize = true;
            this.editorAviso.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editorAviso.BackColor = System.Drawing.Color.White;
            this.editorAviso.Controls.Add(this.editorAvisoAcciones);
            this.editorAviso.Controls.Add(this.editorAvisoCampos);
            this.editorAviso.Controls.Add(this.editorAvisoTitulo);
            this.editorAviso.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorAviso.Location = new System.Drawing.Point(20, 65);
            this.editorAviso.Margin = new System.Windows.Forms.Padding(0);
            this.editorAviso.MinimumSize = new System.Drawing.Size(0, 194);
            this.editorAviso.Name = "editorAviso";
            this.editorAviso.Padding = new System.Windows.Forms.Padding(14, 4, 14, 10);
            this.editorAviso.Size = new System.Drawing.Size(1010, 194);
            this.editorAviso.TabIndex = 1;
            // 
            // editorAvisoAcciones
            // 
            this.editorAvisoAcciones.AutoSize = true;
            this.editorAvisoAcciones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editorAvisoAcciones.Controls.Add(this.btnGuardar);
            this.editorAvisoAcciones.Controls.Add(this.btnNuevo);
            this.editorAvisoAcciones.Controls.Add(this.btnEliminar);
            this.editorAvisoAcciones.Controls.Add(this.btnEstado);
            this.editorAvisoAcciones.Controls.Add(this.btnEnviar);
            this.editorAvisoAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorAvisoAcciones.Location = new System.Drawing.Point(14, 138);
            this.editorAvisoAcciones.Name = "editorAvisoAcciones";
            this.editorAvisoAcciones.Padding = new System.Windows.Forms.Padding(6, 6, 0, 0);
            this.editorAvisoAcciones.Size = new System.Drawing.Size(982, 44);
            this.editorAvisoAcciones.TabIndex = 2;
            // 
            // editorAvisoCampos
            // 
            this.editorAvisoCampos.ColumnCount = 4;
            this.editorAvisoCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.editorAvisoCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.editorAvisoCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.editorAvisoCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.editorAvisoCampos.Controls.Add(this.lblCmbVehiculo, 0, 0);
            this.editorAvisoCampos.Controls.Add(this.cmbVehiculo, 0, 1);
            this.editorAvisoCampos.Controls.Add(this.lblCmbArticulo, 1, 0);
            this.editorAvisoCampos.Controls.Add(this.cmbArticulo, 1, 1);
            this.editorAvisoCampos.Controls.Add(this.lblDtpFecha, 2, 0);
            this.editorAvisoCampos.Controls.Add(this.dtpFecha, 2, 1);
            this.editorAvisoCampos.Controls.Add(this.lblCmbEstado, 3, 0);
            this.editorAvisoCampos.Controls.Add(this.cmbEstado, 3, 1);
            this.editorAvisoCampos.Controls.Add(this.lblTxtComentario, 0, 2);
            this.editorAvisoCampos.Controls.Add(this.txtComentario, 0, 3);
            this.editorAvisoCampos.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorAvisoCampos.Location = new System.Drawing.Point(14, 36);
            this.editorAvisoCampos.Margin = new System.Windows.Forms.Padding(0);
            this.editorAvisoCampos.Name = "editorAvisoCampos";
            this.editorAvisoCampos.RowCount = 4;
            this.editorAvisoCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.editorAvisoCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.editorAvisoCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.editorAvisoCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.editorAvisoCampos.Size = new System.Drawing.Size(982, 102);
            this.editorAvisoCampos.TabIndex = 1;
            // 
            // lblCmbVehiculo
            // 
            this.lblCmbVehiculo.AutoEllipsis = true;
            this.lblCmbVehiculo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCmbVehiculo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCmbVehiculo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCmbVehiculo.Location = new System.Drawing.Point(6, 0);
            this.lblCmbVehiculo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCmbVehiculo.Name = "lblCmbVehiculo";
            this.lblCmbVehiculo.Size = new System.Drawing.Size(243, 20);
            this.lblCmbVehiculo.TabIndex = 0;
            this.lblCmbVehiculo.Text = "Vehículo";
            this.lblCmbVehiculo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCmbArticulo
            // 
            this.lblCmbArticulo.AutoEllipsis = true;
            this.lblCmbArticulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCmbArticulo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCmbArticulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCmbArticulo.Location = new System.Drawing.Point(261, 0);
            this.lblCmbArticulo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCmbArticulo.Name = "lblCmbArticulo";
            this.lblCmbArticulo.Size = new System.Drawing.Size(243, 20);
            this.lblCmbArticulo.TabIndex = 2;
            this.lblCmbArticulo.Text = "Artículo o servicio";
            this.lblCmbArticulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDtpFecha
            // 
            this.lblDtpFecha.AutoEllipsis = true;
            this.lblDtpFecha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDtpFecha.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDtpFecha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblDtpFecha.Location = new System.Drawing.Point(516, 0);
            this.lblDtpFecha.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDtpFecha.Name = "lblDtpFecha";
            this.lblDtpFecha.Size = new System.Drawing.Size(223, 20);
            this.lblDtpFecha.TabIndex = 4;
            this.lblDtpFecha.Text = "Fecha programada";
            this.lblDtpFecha.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCmbEstado
            // 
            this.lblCmbEstado.AutoEllipsis = true;
            this.lblCmbEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCmbEstado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCmbEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCmbEstado.Location = new System.Drawing.Point(751, 0);
            this.lblCmbEstado.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCmbEstado.Name = "lblCmbEstado";
            this.lblCmbEstado.Size = new System.Drawing.Size(225, 20);
            this.lblCmbEstado.TabIndex = 6;
            this.lblCmbEstado.Text = "Estado";
            this.lblCmbEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTxtComentario
            // 
            this.lblTxtComentario.AutoEllipsis = true;
            this.editorAvisoCampos.SetColumnSpan(this.lblTxtComentario, 4);
            this.lblTxtComentario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtComentario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtComentario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtComentario.Location = new System.Drawing.Point(6, 51);
            this.lblTxtComentario.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtComentario.Name = "lblTxtComentario";
            this.lblTxtComentario.Size = new System.Drawing.Size(970, 20);
            this.lblTxtComentario.TabIndex = 8;
            this.lblTxtComentario.Text = "Observación";
            this.lblTxtComentario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // editorAvisoTitulo
            // 
            this.editorAvisoTitulo.AutoEllipsis = true;
            this.editorAvisoTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorAvisoTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.editorAvisoTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.editorAvisoTitulo.Location = new System.Drawing.Point(14, 4);
            this.editorAvisoTitulo.Margin = new System.Windows.Forms.Padding(0);
            this.editorAvisoTitulo.Name = "editorAvisoTitulo";
            this.editorAvisoTitulo.Size = new System.Drawing.Size(982, 32);
            this.editorAvisoTitulo.TabIndex = 0;
            this.editorAvisoTitulo.Text = "Datos del recordatorio";
            this.editorAvisoTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // espacioRecordatorios
            // 
            this.espacioRecordatorios.Dock = System.Windows.Forms.DockStyle.Top;
            this.espacioRecordatorios.Location = new System.Drawing.Point(20, 259);
            this.espacioRecordatorios.Name = "espacioRecordatorios";
            this.espacioRecordatorios.Size = new System.Drawing.Size(1010, 10);
            this.espacioRecordatorios.TabIndex = 2;
            // 
            // txtFiltroPlaca
            // 
            this.txtFiltroPlaca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtFiltroPlaca.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFiltroPlaca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFiltroPlaca.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFiltroPlaca.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtFiltroPlaca.Location = new System.Drawing.Point(6, 20);
            this.txtFiltroPlaca.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtFiltroPlaca.MaxLength = 100;
            this.txtFiltroPlaca.Name = "txtFiltroPlaca";
            this.txtFiltroPlaca.Size = new System.Drawing.Size(528, 27);
            this.txtFiltroPlaca.TabIndex = 1;
            // 
            // listadoRecordatorios
            // 
            this.listadoRecordatorios.BackColor = System.Drawing.Color.White;
            this.listadoRecordatorios.Controls.Add(this.grid);
            this.listadoRecordatorios.Controls.Add(this.listadoRecordatoriosFiltros);
            this.listadoRecordatorios.Controls.Add(this.listadoRecordatoriosTitulo);
            this.listadoRecordatorios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listadoRecordatorios.Location = new System.Drawing.Point(20, 269);
            this.listadoRecordatorios.Margin = new System.Windows.Forms.Padding(0);
            this.listadoRecordatorios.Name = "listadoRecordatorios";
            this.listadoRecordatorios.Padding = new System.Windows.Forms.Padding(14, 4, 14, 12);
            this.listadoRecordatorios.Size = new System.Drawing.Size(1010, 365);
            this.listadoRecordatorios.TabIndex = 3;
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
            this.gridColId,
            this.gridColFecha,
            this.gridColPlaca,
            this.gridColArticulo,
            this.gridColEstado,
            this.gridColComentario});
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
            this.grid.Margin = new System.Windows.Forms.Padding(0);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersVisible = false;
            this.grid.RowHeadersWidth = 51;
            this.grid.RowTemplate.Height = 30;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(982, 271);
            this.grid.TabIndex = 2;
            // 
            // gridColId
            // 
            this.gridColId.DataPropertyName = "Id";
            this.gridColId.FillWeight = 47F;
            this.gridColId.HeaderText = "ID";
            this.gridColId.MinimumWidth = 44;
            this.gridColId.Name = "gridColId";
            this.gridColId.ReadOnly = true;
            this.gridColId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridColFecha
            // 
            this.gridColFecha.DataPropertyName = "Fecha";
            this.gridColFecha.FillWeight = 112F;
            this.gridColFecha.HeaderText = "Fecha programada";
            this.gridColFecha.MinimumWidth = 112;
            this.gridColFecha.Name = "gridColFecha";
            this.gridColFecha.ReadOnly = true;
            this.gridColFecha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridColPlaca
            // 
            this.gridColPlaca.DataPropertyName = "Placa";
            this.gridColPlaca.FillWeight = 75F;
            this.gridColPlaca.HeaderText = "Placa";
            this.gridColPlaca.MinimumWidth = 65;
            this.gridColPlaca.Name = "gridColPlaca";
            this.gridColPlaca.ReadOnly = true;
            this.gridColPlaca.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridColArticulo
            // 
            this.gridColArticulo.DataPropertyName = "Articulo";
            this.gridColArticulo.FillWeight = 141F;
            this.gridColArticulo.HeaderText = "Artículo o servicio";
            this.gridColArticulo.MinimumWidth = 110;
            this.gridColArticulo.Name = "gridColArticulo";
            this.gridColArticulo.ReadOnly = true;
            this.gridColArticulo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridColEstado
            // 
            this.gridColEstado.DataPropertyName = "Estado";
            this.gridColEstado.HeaderText = "Estado";
            this.gridColEstado.MinimumWidth = 100;
            this.gridColEstado.Name = "gridColEstado";
            this.gridColEstado.ReadOnly = true;
            this.gridColEstado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridColComentario
            // 
            this.gridColComentario.DataPropertyName = "Comentario";
            this.gridColComentario.FillWeight = 154F;
            this.gridColComentario.HeaderText = "Observación";
            this.gridColComentario.MinimumWidth = 105;
            this.gridColComentario.Name = "gridColComentario";
            this.gridColComentario.ReadOnly = true;
            this.gridColComentario.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // listadoRecordatoriosFiltros
            // 
            this.listadoRecordatoriosFiltros.ColumnCount = 2;
            this.listadoRecordatoriosFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.listadoRecordatoriosFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.listadoRecordatoriosFiltros.Controls.Add(this.lblTxtFiltroPlaca, 0, 0);
            this.listadoRecordatoriosFiltros.Controls.Add(this.txtFiltroPlaca, 0, 1);
            this.listadoRecordatoriosFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.listadoRecordatoriosFiltros.Location = new System.Drawing.Point(14, 36);
            this.listadoRecordatoriosFiltros.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.listadoRecordatoriosFiltros.Name = "listadoRecordatoriosFiltros";
            this.listadoRecordatoriosFiltros.RowCount = 2;
            this.listadoRecordatoriosFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.listadoRecordatoriosFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.listadoRecordatoriosFiltros.Size = new System.Drawing.Size(982, 46);
            this.listadoRecordatoriosFiltros.TabIndex = 1;
            // 
            // lblTxtFiltroPlaca
            // 
            this.lblTxtFiltroPlaca.AutoEllipsis = true;
            this.lblTxtFiltroPlaca.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtFiltroPlaca.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtFiltroPlaca.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtFiltroPlaca.Location = new System.Drawing.Point(6, 0);
            this.lblTxtFiltroPlaca.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtFiltroPlaca.Name = "lblTxtFiltroPlaca";
            this.lblTxtFiltroPlaca.Size = new System.Drawing.Size(528, 17);
            this.lblTxtFiltroPlaca.TabIndex = 0;
            this.lblTxtFiltroPlaca.Text = "Filtrar por placa";
            this.lblTxtFiltroPlaca.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listadoRecordatoriosTitulo
            // 
            this.listadoRecordatoriosTitulo.AutoEllipsis = true;
            this.listadoRecordatoriosTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.listadoRecordatoriosTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.listadoRecordatoriosTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.listadoRecordatoriosTitulo.Location = new System.Drawing.Point(14, 4);
            this.listadoRecordatoriosTitulo.Margin = new System.Windows.Forms.Padding(0);
            this.listadoRecordatoriosTitulo.Name = "listadoRecordatoriosTitulo";
            this.listadoRecordatoriosTitulo.Size = new System.Drawing.Size(982, 32);
            this.listadoRecordatoriosTitulo.TabIndex = 0;
            this.listadoRecordatoriosTitulo.Text = "Recordatorios programados";
            this.listadoRecordatoriosTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmMantenimiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1050, 650);
            this.Controls.Add(this.listadoRecordatorios);
            this.Controls.Add(this.espacioRecordatorios);
            this.Controls.Add(this.editorAviso);
            this.Controls.Add(this.encabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.MinimumSize = new System.Drawing.Size(680, 440);
            this.Name = "FrmMantenimiento";
            this.Padding = new System.Windows.Forms.Padding(20, 8, 20, 16);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Notificaciones de mantenimiento";
            ((System.ComponentModel.ISupportInitialize)(this.errores)).EndInit();
            this.encabezado.ResumeLayout(false);
            this.editorAviso.ResumeLayout(false);
            this.editorAviso.PerformLayout();
            this.editorAvisoAcciones.ResumeLayout(false);
            this.editorAvisoCampos.ResumeLayout(false);
            this.editorAvisoCampos.PerformLayout();
            this.listadoRecordatorios.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.listadoRecordatoriosFiltros.ResumeLayout(false);
            this.listadoRecordatoriosFiltros.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel encabezado;
        private Label lblTitulo;
        private Panel acentoTitulo;
        private ComboBox cmbVehiculo;
        private ComboBox cmbArticulo;
        private DateTimePicker dtpFecha;
        private ComboBox cmbEstado;
        private TextBox txtComentario;
        private Button btnGuardar;
        private Button btnNuevo;
        private Button btnEliminar;
        private Button btnEstado;
        private Button btnEnviar;
        private Panel editorAviso;
        private TableLayoutPanel editorAvisoCampos;
        private Label lblCmbVehiculo;
        private Label lblCmbArticulo;
        private Label lblDtpFecha;
        private Label lblCmbEstado;
        private Label lblTxtComentario;
        private FlowLayoutPanel editorAvisoAcciones;
        private Label editorAvisoTitulo;
        private Panel espacioRecordatorios;
        private TextBox txtFiltroPlaca;
        private Panel listadoRecordatorios;
        private Label listadoRecordatoriosTitulo;
        private TableLayoutPanel listadoRecordatoriosFiltros;
        private Label lblTxtFiltroPlaca;
        private DataGridView grid;
        private DataGridViewTextBoxColumn gridColId;
        private DataGridViewTextBoxColumn gridColFecha;
        private DataGridViewTextBoxColumn gridColPlaca;
        private DataGridViewTextBoxColumn gridColArticulo;
        private DataGridViewTextBoxColumn gridColEstado;
        private DataGridViewTextBoxColumn gridColComentario;
    }
}
