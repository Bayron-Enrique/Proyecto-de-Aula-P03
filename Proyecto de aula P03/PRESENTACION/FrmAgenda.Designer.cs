using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PRESENTACION
{
    partial class FrmAgenda
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
            this.cmbServicio = new System.Windows.Forms.ComboBox();
            this.cmbEmpleado = new System.Windows.Forms.ComboBox();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.txtComentario = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnEstado = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnMantenimiento = new System.Windows.Forms.Button();
            this.editorCita = new System.Windows.Forms.Panel();
            this.editorCitaAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.editorCitaCampos = new System.Windows.Forms.TableLayoutPanel();
            this.lblCmbVehiculo = new System.Windows.Forms.Label();
            this.lblCmbServicio = new System.Windows.Forms.Label();
            this.lblCmbEmpleado = new System.Windows.Forms.Label();
            this.lblDtpFecha = new System.Windows.Forms.Label();
            this.lblCmbEstado = new System.Windows.Forms.Label();
            this.lblTxtComentario = new System.Windows.Forms.Label();
            this.editorCitaTitulo = new System.Windows.Forms.Label();
            this.espacioCitas = new System.Windows.Forms.Panel();
            this.dtpFiltroFecha = new System.Windows.Forms.DateTimePicker();
            this.cmbFiltroEstado = new System.Windows.Forms.ComboBox();
            this.listadoCitas = new System.Windows.Forms.Panel();
            this.grid = new System.Windows.Forms.DataGridView();
            this.gridColId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColPlaca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColEmpleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColCargo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColServicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColComentario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listadoCitasFiltros = new System.Windows.Forms.TableLayoutPanel();
            this.lblDtpFiltroFecha = new System.Windows.Forms.Label();
            this.lblCmbFiltroEstado = new System.Windows.Forms.Label();
            this.listadoCitasTitulo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errores)).BeginInit();
            this.encabezado.SuspendLayout();
            this.editorCita.SuspendLayout();
            this.editorCitaAcciones.SuspendLayout();
            this.editorCitaCampos.SuspendLayout();
            this.listadoCitas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.listadoCitasFiltros.SuspendLayout();
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
            this.lblTitulo.Text = "Agenda de citas";
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
            this.cmbVehiculo.Size = new System.Drawing.Size(251, 23);
            this.cmbVehiculo.TabIndex = 1;
            // 
            // cmbServicio
            // 
            this.cmbServicio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.cmbServicio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbServicio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.cmbServicio.Location = new System.Drawing.Point(269, 23);
            this.cmbServicio.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbServicio.Name = "cmbServicio";
            this.cmbServicio.Size = new System.Drawing.Size(251, 23);
            this.cmbServicio.TabIndex = 3;
            // 
            // cmbEmpleado
            // 
            this.cmbEmpleado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.cmbEmpleado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbEmpleado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmpleado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.cmbEmpleado.Location = new System.Drawing.Point(532, 23);
            this.cmbEmpleado.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbEmpleado.Name = "cmbEmpleado";
            this.cmbEmpleado.Size = new System.Drawing.Size(251, 23);
            this.cmbEmpleado.TabIndex = 5;
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpFecha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(795, 23);
            this.dtpFecha.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.ShowUpDown = true;
            this.dtpFecha.Size = new System.Drawing.Size(251, 23);
            this.dtpFecha.TabIndex = 7;
            // 
            // cmbEstado
            // 
            this.cmbEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.cmbEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.cmbEstado.Items.AddRange(new object[] {
            "PENDIENTE",
            "EN PROCESO",
            "COMPLETADA",
            "CANCELADA"});
            this.cmbEstado.Location = new System.Drawing.Point(6, 74);
            this.cmbEstado.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(251, 23);
            this.cmbEstado.TabIndex = 9;
            // 
            // txtComentario
            // 
            this.txtComentario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtComentario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editorCitaCampos.SetColumnSpan(this.txtComentario, 3);
            this.txtComentario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtComentario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtComentario.Location = new System.Drawing.Point(269, 74);
            this.txtComentario.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtComentario.MaxLength = 200;
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.Size = new System.Drawing.Size(777, 23);
            this.txtComentario.TabIndex = 11;
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
            this.btnGuardar.Text = "Guardar cita";
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
            // btnEstado
            // 
            this.btnEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.btnEstado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEstado.FlatAppearance.BorderSize = 0;
            this.btnEstado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.btnEstado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEstado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.btnEstado.Location = new System.Drawing.Point(220, 9);
            this.btnEstado.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnEstado.Name = "btnEstado";
            this.btnEstado.Size = new System.Drawing.Size(128, 32);
            this.btnEstado.TabIndex = 2;
            this.btnEstado.Text = "Cambiar estado";
            this.btnEstado.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.btnCancelar.Location = new System.Drawing.Point(356, 9);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(112, 32);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar cita";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnMantenimiento
            // 
            this.btnMantenimiento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.btnMantenimiento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMantenimiento.FlatAppearance.BorderSize = 0;
            this.btnMantenimiento.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.btnMantenimiento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMantenimiento.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnMantenimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.btnMantenimiento.Location = new System.Drawing.Point(476, 9);
            this.btnMantenimiento.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnMantenimiento.Name = "btnMantenimiento";
            this.btnMantenimiento.Size = new System.Drawing.Size(218, 32);
            this.btnMantenimiento.TabIndex = 4;
            this.btnMantenimiento.Text = "Notificaciones de mantenimiento";
            this.btnMantenimiento.UseVisualStyleBackColor = false;
            // 
            // editorCita
            // 
            this.editorCita.AutoSize = true;
            this.editorCita.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editorCita.BackColor = System.Drawing.Color.White;
            this.editorCita.Controls.Add(this.editorCitaAcciones);
            this.editorCita.Controls.Add(this.editorCitaCampos);
            this.editorCita.Controls.Add(this.editorCitaTitulo);
            this.editorCita.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorCita.Location = new System.Drawing.Point(20, 65);
            this.editorCita.Margin = new System.Windows.Forms.Padding(0);
            this.editorCita.MinimumSize = new System.Drawing.Size(0, 194);
            this.editorCita.Name = "editorCita";
            this.editorCita.Padding = new System.Windows.Forms.Padding(14, 4, 14, 10);
            this.editorCita.Size = new System.Drawing.Size(1080, 194);
            this.editorCita.TabIndex = 1;
            // 
            // editorCitaAcciones
            // 
            this.editorCitaAcciones.AutoSize = true;
            this.editorCitaAcciones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editorCitaAcciones.Controls.Add(this.btnGuardar);
            this.editorCitaAcciones.Controls.Add(this.btnNuevo);
            this.editorCitaAcciones.Controls.Add(this.btnEstado);
            this.editorCitaAcciones.Controls.Add(this.btnCancelar);
            this.editorCitaAcciones.Controls.Add(this.btnMantenimiento);
            this.editorCitaAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorCitaAcciones.Location = new System.Drawing.Point(14, 138);
            this.editorCitaAcciones.Name = "editorCitaAcciones";
            this.editorCitaAcciones.Padding = new System.Windows.Forms.Padding(6, 6, 0, 0);
            this.editorCitaAcciones.Size = new System.Drawing.Size(1052, 44);
            this.editorCitaAcciones.TabIndex = 2;
            // 
            // editorCitaCampos
            // 
            this.editorCitaCampos.ColumnCount = 4;
            this.editorCitaCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.editorCitaCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.editorCitaCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.editorCitaCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.editorCitaCampos.Controls.Add(this.lblCmbVehiculo, 0, 0);
            this.editorCitaCampos.Controls.Add(this.cmbVehiculo, 0, 1);
            this.editorCitaCampos.Controls.Add(this.lblCmbServicio, 1, 0);
            this.editorCitaCampos.Controls.Add(this.cmbServicio, 1, 1);
            this.editorCitaCampos.Controls.Add(this.lblCmbEmpleado, 2, 0);
            this.editorCitaCampos.Controls.Add(this.cmbEmpleado, 2, 1);
            this.editorCitaCampos.Controls.Add(this.lblDtpFecha, 3, 0);
            this.editorCitaCampos.Controls.Add(this.dtpFecha, 3, 1);
            this.editorCitaCampos.Controls.Add(this.lblCmbEstado, 0, 2);
            this.editorCitaCampos.Controls.Add(this.cmbEstado, 0, 3);
            this.editorCitaCampos.Controls.Add(this.lblTxtComentario, 1, 2);
            this.editorCitaCampos.Controls.Add(this.txtComentario, 1, 3);
            this.editorCitaCampos.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorCitaCampos.Location = new System.Drawing.Point(14, 36);
            this.editorCitaCampos.Margin = new System.Windows.Forms.Padding(0);
            this.editorCitaCampos.Name = "editorCitaCampos";
            this.editorCitaCampos.RowCount = 4;
            this.editorCitaCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.editorCitaCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.editorCitaCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.editorCitaCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.editorCitaCampos.Size = new System.Drawing.Size(1052, 102);
            this.editorCitaCampos.TabIndex = 1;
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
            this.lblCmbVehiculo.Size = new System.Drawing.Size(251, 20);
            this.lblCmbVehiculo.TabIndex = 0;
            this.lblCmbVehiculo.Text = "Vehículo";
            this.lblCmbVehiculo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCmbServicio
            // 
            this.lblCmbServicio.AutoEllipsis = true;
            this.lblCmbServicio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCmbServicio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCmbServicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCmbServicio.Location = new System.Drawing.Point(269, 0);
            this.lblCmbServicio.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCmbServicio.Name = "lblCmbServicio";
            this.lblCmbServicio.Size = new System.Drawing.Size(251, 20);
            this.lblCmbServicio.TabIndex = 2;
            this.lblCmbServicio.Text = "Servicio";
            this.lblCmbServicio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCmbEmpleado
            // 
            this.lblCmbEmpleado.AutoEllipsis = true;
            this.lblCmbEmpleado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCmbEmpleado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCmbEmpleado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCmbEmpleado.Location = new System.Drawing.Point(532, 0);
            this.lblCmbEmpleado.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCmbEmpleado.Name = "lblCmbEmpleado";
            this.lblCmbEmpleado.Size = new System.Drawing.Size(251, 20);
            this.lblCmbEmpleado.TabIndex = 4;
            this.lblCmbEmpleado.Text = "Empleado";
            this.lblCmbEmpleado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDtpFecha
            // 
            this.lblDtpFecha.AutoEllipsis = true;
            this.lblDtpFecha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDtpFecha.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDtpFecha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblDtpFecha.Location = new System.Drawing.Point(795, 0);
            this.lblDtpFecha.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDtpFecha.Name = "lblDtpFecha";
            this.lblDtpFecha.Size = new System.Drawing.Size(251, 20);
            this.lblDtpFecha.TabIndex = 6;
            this.lblDtpFecha.Text = "Fecha y hora";
            this.lblDtpFecha.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCmbEstado
            // 
            this.lblCmbEstado.AutoEllipsis = true;
            this.lblCmbEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCmbEstado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCmbEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCmbEstado.Location = new System.Drawing.Point(6, 51);
            this.lblCmbEstado.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCmbEstado.Name = "lblCmbEstado";
            this.lblCmbEstado.Size = new System.Drawing.Size(251, 20);
            this.lblCmbEstado.TabIndex = 8;
            this.lblCmbEstado.Text = "Estado";
            this.lblCmbEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTxtComentario
            // 
            this.lblTxtComentario.AutoEllipsis = true;
            this.editorCitaCampos.SetColumnSpan(this.lblTxtComentario, 3);
            this.lblTxtComentario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtComentario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtComentario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtComentario.Location = new System.Drawing.Point(269, 51);
            this.lblTxtComentario.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtComentario.Name = "lblTxtComentario";
            this.lblTxtComentario.Size = new System.Drawing.Size(777, 20);
            this.lblTxtComentario.TabIndex = 10;
            this.lblTxtComentario.Text = "Observación";
            this.lblTxtComentario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // editorCitaTitulo
            // 
            this.editorCitaTitulo.AutoEllipsis = true;
            this.editorCitaTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorCitaTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.editorCitaTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.editorCitaTitulo.Location = new System.Drawing.Point(14, 4);
            this.editorCitaTitulo.Margin = new System.Windows.Forms.Padding(0);
            this.editorCitaTitulo.Name = "editorCitaTitulo";
            this.editorCitaTitulo.Size = new System.Drawing.Size(1052, 32);
            this.editorCitaTitulo.TabIndex = 0;
            this.editorCitaTitulo.Text = "Datos de la cita";
            this.editorCitaTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // espacioCitas
            // 
            this.espacioCitas.Dock = System.Windows.Forms.DockStyle.Top;
            this.espacioCitas.Location = new System.Drawing.Point(20, 259);
            this.espacioCitas.Name = "espacioCitas";
            this.espacioCitas.Size = new System.Drawing.Size(1080, 10);
            this.espacioCitas.TabIndex = 2;
            // 
            // dtpFiltroFecha
            // 
            this.dtpFiltroFecha.Checked = false;
            this.dtpFiltroFecha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFiltroFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltroFecha.Location = new System.Drawing.Point(6, 20);
            this.dtpFiltroFecha.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.dtpFiltroFecha.Name = "dtpFiltroFecha";
            this.dtpFiltroFecha.ShowCheckBox = true;
            this.dtpFiltroFecha.Size = new System.Drawing.Size(153, 23);
            this.dtpFiltroFecha.TabIndex = 1;
            // 
            // cmbFiltroEstado
            // 
            this.cmbFiltroEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.cmbFiltroEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbFiltroEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.cmbFiltroEstado.Items.AddRange(new object[] {
            "Todos",
            "PENDIENTE",
            "EN PROCESO",
            "COMPLETADA",
            "CANCELADA"});
            this.cmbFiltroEstado.Location = new System.Drawing.Point(171, 20);
            this.cmbFiltroEstado.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbFiltroEstado.Name = "cmbFiltroEstado";
            this.cmbFiltroEstado.Size = new System.Drawing.Size(173, 23);
            this.cmbFiltroEstado.TabIndex = 3;
            // 
            // listadoCitas
            // 
            this.listadoCitas.BackColor = System.Drawing.Color.White;
            this.listadoCitas.Controls.Add(this.grid);
            this.listadoCitas.Controls.Add(this.listadoCitasFiltros);
            this.listadoCitas.Controls.Add(this.listadoCitasTitulo);
            this.listadoCitas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listadoCitas.Location = new System.Drawing.Point(20, 269);
            this.listadoCitas.Margin = new System.Windows.Forms.Padding(0);
            this.listadoCitas.Name = "listadoCitas";
            this.listadoCitas.Padding = new System.Windows.Forms.Padding(14, 4, 14, 12);
            this.listadoCitas.Size = new System.Drawing.Size(1080, 395);
            this.listadoCitas.TabIndex = 3;
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
            this.gridColCliente,
            this.gridColEmpleado,
            this.gridColCargo,
            this.gridColServicio,
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
            this.grid.RowTemplate.Height = 30;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(1052, 301);
            this.grid.TabIndex = 2;
            // 
            // gridColId
            // 
            this.gridColId.DataPropertyName = "Id";
            this.gridColId.FillWeight = 43F;
            this.gridColId.HeaderText = "ID";
            this.gridColId.MinimumWidth = 44;
            this.gridColId.Name = "gridColId";
            this.gridColId.ReadOnly = true;
            this.gridColId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridColFecha
            // 
            this.gridColFecha.DataPropertyName = "Fecha";
            this.gridColFecha.FillWeight = 114F;
            this.gridColFecha.HeaderText = "Fecha y hora";
            this.gridColFecha.MinimumWidth = 105;
            this.gridColFecha.Name = "gridColFecha";
            this.gridColFecha.ReadOnly = true;
            this.gridColFecha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridColPlaca
            // 
            this.gridColPlaca.DataPropertyName = "Placa";
            this.gridColPlaca.FillWeight = 74F;
            this.gridColPlaca.HeaderText = "Placa";
            this.gridColPlaca.MinimumWidth = 65;
            this.gridColPlaca.Name = "gridColPlaca";
            this.gridColPlaca.ReadOnly = true;
            this.gridColPlaca.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridColCliente
            // 
            this.gridColCliente.DataPropertyName = "Cliente";
            this.gridColCliente.FillWeight = 116F;
            this.gridColCliente.HeaderText = "Cliente";
            this.gridColCliente.MinimumWidth = 95;
            this.gridColCliente.Name = "gridColCliente";
            this.gridColCliente.ReadOnly = true;
            this.gridColCliente.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridColEmpleado
            // 
            this.gridColEmpleado.DataPropertyName = "Empleado";
            this.gridColEmpleado.FillWeight = 116F;
            this.gridColEmpleado.HeaderText = "Empleado";
            this.gridColEmpleado.MinimumWidth = 90;
            this.gridColEmpleado.Name = "gridColEmpleado";
            this.gridColEmpleado.ReadOnly = true;
            this.gridColEmpleado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridColCargo
            // 
            this.gridColCargo.DataPropertyName = "Cargo";
            this.gridColCargo.FillWeight = 78F;
            this.gridColCargo.HeaderText = "Cargo";
            this.gridColCargo.MinimumWidth = 65;
            this.gridColCargo.Name = "gridColCargo";
            this.gridColCargo.ReadOnly = true;
            this.gridColCargo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridColServicio
            // 
            this.gridColServicio.DataPropertyName = "Servicio";
            this.gridColServicio.FillWeight = 115F;
            this.gridColServicio.HeaderText = "Servicio";
            this.gridColServicio.MinimumWidth = 90;
            this.gridColServicio.Name = "gridColServicio";
            this.gridColServicio.ReadOnly = true;
            this.gridColServicio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridColEstado
            // 
            this.gridColEstado.DataPropertyName = "Estado";
            this.gridColEstado.FillWeight = 104F;
            this.gridColEstado.HeaderText = "Estado";
            this.gridColEstado.MinimumWidth = 98;
            this.gridColEstado.Name = "gridColEstado";
            this.gridColEstado.ReadOnly = true;
            this.gridColEstado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridColComentario
            // 
            this.gridColComentario.DataPropertyName = "Comentario";
            this.gridColComentario.FillWeight = 142F;
            this.gridColComentario.HeaderText = "Observación";
            this.gridColComentario.MinimumWidth = 102;
            this.gridColComentario.Name = "gridColComentario";
            this.gridColComentario.ReadOnly = true;
            this.gridColComentario.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // listadoCitasFiltros
            // 
            this.listadoCitasFiltros.ColumnCount = 3;
            this.listadoCitasFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 165F));
            this.listadoCitasFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.listadoCitasFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.listadoCitasFiltros.Controls.Add(this.lblDtpFiltroFecha, 0, 0);
            this.listadoCitasFiltros.Controls.Add(this.dtpFiltroFecha, 0, 1);
            this.listadoCitasFiltros.Controls.Add(this.lblCmbFiltroEstado, 1, 0);
            this.listadoCitasFiltros.Controls.Add(this.cmbFiltroEstado, 1, 1);
            this.listadoCitasFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.listadoCitasFiltros.Location = new System.Drawing.Point(14, 36);
            this.listadoCitasFiltros.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.listadoCitasFiltros.Name = "listadoCitasFiltros";
            this.listadoCitasFiltros.RowCount = 2;
            this.listadoCitasFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.listadoCitasFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.listadoCitasFiltros.Size = new System.Drawing.Size(1052, 46);
            this.listadoCitasFiltros.TabIndex = 1;
            // 
            // lblDtpFiltroFecha
            // 
            this.lblDtpFiltroFecha.AutoEllipsis = true;
            this.lblDtpFiltroFecha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDtpFiltroFecha.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDtpFiltroFecha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblDtpFiltroFecha.Location = new System.Drawing.Point(6, 0);
            this.lblDtpFiltroFecha.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDtpFiltroFecha.Name = "lblDtpFiltroFecha";
            this.lblDtpFiltroFecha.Size = new System.Drawing.Size(153, 17);
            this.lblDtpFiltroFecha.TabIndex = 0;
            this.lblDtpFiltroFecha.Text = "Filtrar por fecha";
            this.lblDtpFiltroFecha.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCmbFiltroEstado
            // 
            this.lblCmbFiltroEstado.AutoEllipsis = true;
            this.lblCmbFiltroEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCmbFiltroEstado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCmbFiltroEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCmbFiltroEstado.Location = new System.Drawing.Point(171, 0);
            this.lblCmbFiltroEstado.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCmbFiltroEstado.Name = "lblCmbFiltroEstado";
            this.lblCmbFiltroEstado.Size = new System.Drawing.Size(173, 17);
            this.lblCmbFiltroEstado.TabIndex = 2;
            this.lblCmbFiltroEstado.Text = "Filtrar por estado";
            this.lblCmbFiltroEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listadoCitasTitulo
            // 
            this.listadoCitasTitulo.AutoEllipsis = true;
            this.listadoCitasTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.listadoCitasTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.listadoCitasTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.listadoCitasTitulo.Location = new System.Drawing.Point(14, 4);
            this.listadoCitasTitulo.Margin = new System.Windows.Forms.Padding(0);
            this.listadoCitasTitulo.Name = "listadoCitasTitulo";
            this.listadoCitasTitulo.Size = new System.Drawing.Size(1052, 32);
            this.listadoCitasTitulo.TabIndex = 0;
            this.listadoCitasTitulo.Text = "Citas programadas";
            this.listadoCitasTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmAgenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1120, 680);
            this.Controls.Add(this.listadoCitas);
            this.Controls.Add(this.espacioCitas);
            this.Controls.Add(this.editorCita);
            this.Controls.Add(this.encabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.MinimumSize = new System.Drawing.Size(680, 440);
            this.Name = "FrmAgenda";
            this.Padding = new System.Windows.Forms.Padding(20, 8, 20, 16);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Agenda de citas";
            ((System.ComponentModel.ISupportInitialize)(this.errores)).EndInit();
            this.encabezado.ResumeLayout(false);
            this.editorCita.ResumeLayout(false);
            this.editorCita.PerformLayout();
            this.editorCitaAcciones.ResumeLayout(false);
            this.editorCitaCampos.ResumeLayout(false);
            this.editorCitaCampos.PerformLayout();
            this.listadoCitas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.listadoCitasFiltros.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel encabezado;
        private Label lblTitulo;
        private Panel acentoTitulo;
        private ComboBox cmbVehiculo;
        private ComboBox cmbServicio;
        private ComboBox cmbEmpleado;
        private DateTimePicker dtpFecha;
        private ComboBox cmbEstado;
        private TextBox txtComentario;
        private Button btnGuardar;
        private Button btnNuevo;
        private Button btnEstado;
        private Button btnCancelar;
        private Button btnMantenimiento;
        private Panel editorCita;
        private TableLayoutPanel editorCitaCampos;
        private Label lblCmbVehiculo;
        private Label lblCmbServicio;
        private Label lblCmbEmpleado;
        private Label lblDtpFecha;
        private Label lblCmbEstado;
        private Label lblTxtComentario;
        private FlowLayoutPanel editorCitaAcciones;
        private Label editorCitaTitulo;
        private Panel espacioCitas;
        private DateTimePicker dtpFiltroFecha;
        private ComboBox cmbFiltroEstado;
        private Panel listadoCitas;
        private Label listadoCitasTitulo;
        private TableLayoutPanel listadoCitasFiltros;
        private Label lblDtpFiltroFecha;
        private Label lblCmbFiltroEstado;
        private DataGridView grid;
        private DataGridViewTextBoxColumn gridColId;
        private DataGridViewTextBoxColumn gridColFecha;
        private DataGridViewTextBoxColumn gridColPlaca;
        private DataGridViewTextBoxColumn gridColCliente;
        private DataGridViewTextBoxColumn gridColEmpleado;
        private DataGridViewTextBoxColumn gridColCargo;
        private DataGridViewTextBoxColumn gridColServicio;
        private DataGridViewTextBoxColumn gridColEstado;
        private DataGridViewTextBoxColumn gridColComentario;
    }
}
