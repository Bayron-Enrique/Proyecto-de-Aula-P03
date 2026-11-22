using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PRESENTACION
{
    partial class FrmClientes
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
            this.txtCedula = new System.Windows.Forms.TextBox();
            this.txtPrimerNombre = new System.Windows.Forms.TextBox();
            this.txtSegundoNombre = new System.Windows.Forms.TextBox();
            this.txtPrimerApellido = new System.Windows.Forms.TextBox();
            this.txtSegundoApellido = new System.Windows.Forms.TextBox();
            this.cmbSexo = new System.Windows.Forms.ComboBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.editorCliente = new System.Windows.Forms.Panel();
            this.editorClienteAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.editorClienteCampos = new System.Windows.Forms.TableLayoutPanel();
            this.lblTxtCedula = new System.Windows.Forms.Label();
            this.lblTxtPrimerNombre = new System.Windows.Forms.Label();
            this.lblTxtSegundoNombre = new System.Windows.Forms.Label();
            this.lblTxtPrimerApellido = new System.Windows.Forms.Label();
            this.lblTxtSegundoApellido = new System.Windows.Forms.Label();
            this.lblCmbSexo = new System.Windows.Forms.Label();
            this.lblTxtTelefono = new System.Windows.Forms.Label();
            this.lblTxtCorreo = new System.Windows.Forms.Label();
            this.lblTxtDireccion = new System.Windows.Forms.Label();
            this.editorClienteTitulo = new System.Windows.Forms.Label();
            this.espacioClientes = new System.Windows.Forms.Panel();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.listadoClientes = new System.Windows.Forms.Panel();
            this.grid = new System.Windows.Forms.DataGridView();
            this.gridColCedula = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColPrimerNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColSegundoNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColPrimerApellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColSegundoApellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColSexo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColTelefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColCorreoElectronico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridColDireccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listadoClientesFiltros = new System.Windows.Forms.TableLayoutPanel();
            this.lblTxtBuscar = new System.Windows.Forms.Label();
            this.listadoClientesTitulo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errores)).BeginInit();
            this.encabezado.SuspendLayout();
            this.editorCliente.SuspendLayout();
            this.editorClienteAcciones.SuspendLayout();
            this.editorClienteCampos.SuspendLayout();
            this.listadoClientes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.listadoClientesFiltros.SuspendLayout();
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
            this.lblTitulo.Text = "Clientes";
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
            // txtCedula
            // 
            this.txtCedula.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtCedula.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCedula.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCedula.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtCedula.Location = new System.Drawing.Point(6, 23);
            this.txtCedula.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtCedula.MaxLength = 10;
            this.txtCedula.Name = "txtCedula";
            this.txtCedula.Size = new System.Drawing.Size(345, 27);
            this.txtCedula.TabIndex = 1;
            // 
            // txtPrimerNombre
            // 
            this.txtPrimerNombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtPrimerNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrimerNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPrimerNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtPrimerNombre.Location = new System.Drawing.Point(363, 23);
            this.txtPrimerNombre.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtPrimerNombre.MaxLength = 100;
            this.txtPrimerNombre.Name = "txtPrimerNombre";
            this.txtPrimerNombre.Size = new System.Drawing.Size(335, 27);
            this.txtPrimerNombre.TabIndex = 3;
            // 
            // txtSegundoNombre
            // 
            this.txtSegundoNombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtSegundoNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSegundoNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSegundoNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtSegundoNombre.Location = new System.Drawing.Point(710, 23);
            this.txtSegundoNombre.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtSegundoNombre.MaxLength = 100;
            this.txtSegundoNombre.Name = "txtSegundoNombre";
            this.txtSegundoNombre.Size = new System.Drawing.Size(336, 27);
            this.txtSegundoNombre.TabIndex = 5;
            // 
            // txtPrimerApellido
            // 
            this.txtPrimerApellido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtPrimerApellido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrimerApellido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPrimerApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtPrimerApellido.Location = new System.Drawing.Point(6, 74);
            this.txtPrimerApellido.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtPrimerApellido.MaxLength = 100;
            this.txtPrimerApellido.Name = "txtPrimerApellido";
            this.txtPrimerApellido.Size = new System.Drawing.Size(345, 27);
            this.txtPrimerApellido.TabIndex = 7;
            // 
            // txtSegundoApellido
            // 
            this.txtSegundoApellido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtSegundoApellido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSegundoApellido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSegundoApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtSegundoApellido.Location = new System.Drawing.Point(363, 74);
            this.txtSegundoApellido.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtSegundoApellido.MaxLength = 100;
            this.txtSegundoApellido.Name = "txtSegundoApellido";
            this.txtSegundoApellido.Size = new System.Drawing.Size(335, 27);
            this.txtSegundoApellido.TabIndex = 9;
            // 
            // cmbSexo
            // 
            this.cmbSexo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.cmbSexo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSexo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSexo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.cmbSexo.Items.AddRange(new object[] {
            "M",
            "F"});
            this.cmbSexo.Location = new System.Drawing.Point(710, 74);
            this.cmbSexo.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbSexo.Name = "cmbSexo";
            this.cmbSexo.Size = new System.Drawing.Size(336, 28);
            this.cmbSexo.TabIndex = 11;
            // 
            // txtTelefono
            // 
            this.txtTelefono.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtTelefono.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTelefono.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTelefono.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtTelefono.Location = new System.Drawing.Point(6, 125);
            this.txtTelefono.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtTelefono.MaxLength = 15;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(345, 27);
            this.txtTelefono.TabIndex = 13;
            // 
            // txtCorreo
            // 
            this.txtCorreo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtCorreo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCorreo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCorreo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtCorreo.Location = new System.Drawing.Point(363, 125);
            this.txtCorreo.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtCorreo.MaxLength = 100;
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new System.Drawing.Size(335, 27);
            this.txtCorreo.TabIndex = 15;
            // 
            // txtDireccion
            // 
            this.txtDireccion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtDireccion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDireccion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDireccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtDireccion.Location = new System.Drawing.Point(710, 125);
            this.txtDireccion.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtDireccion.MaxLength = 150;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(336, 27);
            this.txtDireccion.TabIndex = 17;
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
            // editorCliente
            // 
            this.editorCliente.AutoSize = true;
            this.editorCliente.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editorCliente.BackColor = System.Drawing.Color.White;
            this.editorCliente.Controls.Add(this.editorClienteAcciones);
            this.editorCliente.Controls.Add(this.editorClienteCampos);
            this.editorCliente.Controls.Add(this.editorClienteTitulo);
            this.editorCliente.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorCliente.Location = new System.Drawing.Point(20, 65);
            this.editorCliente.Margin = new System.Windows.Forms.Padding(0);
            this.editorCliente.MinimumSize = new System.Drawing.Size(0, 245);
            this.editorCliente.Name = "editorCliente";
            this.editorCliente.Padding = new System.Windows.Forms.Padding(14, 4, 14, 10);
            this.editorCliente.Size = new System.Drawing.Size(1080, 245);
            this.editorCliente.TabIndex = 1;
            // 
            // editorClienteAcciones
            // 
            this.editorClienteAcciones.AutoSize = true;
            this.editorClienteAcciones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editorClienteAcciones.Controls.Add(this.btnGuardar);
            this.editorClienteAcciones.Controls.Add(this.btnNuevo);
            this.editorClienteAcciones.Controls.Add(this.btnEliminar);
            this.editorClienteAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorClienteAcciones.Location = new System.Drawing.Point(14, 189);
            this.editorClienteAcciones.Name = "editorClienteAcciones";
            this.editorClienteAcciones.Padding = new System.Windows.Forms.Padding(6, 6, 0, 0);
            this.editorClienteAcciones.Size = new System.Drawing.Size(1052, 44);
            this.editorClienteAcciones.TabIndex = 2;
            // 
            // editorClienteCampos
            // 
            this.editorClienteCampos.ColumnCount = 3;
            this.editorClienteCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.editorClienteCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.editorClienteCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.editorClienteCampos.Controls.Add(this.lblTxtCedula, 0, 0);
            this.editorClienteCampos.Controls.Add(this.txtCedula, 0, 1);
            this.editorClienteCampos.Controls.Add(this.lblTxtPrimerNombre, 1, 0);
            this.editorClienteCampos.Controls.Add(this.txtPrimerNombre, 1, 1);
            this.editorClienteCampos.Controls.Add(this.lblTxtSegundoNombre, 2, 0);
            this.editorClienteCampos.Controls.Add(this.txtSegundoNombre, 2, 1);
            this.editorClienteCampos.Controls.Add(this.lblTxtPrimerApellido, 0, 2);
            this.editorClienteCampos.Controls.Add(this.txtPrimerApellido, 0, 3);
            this.editorClienteCampos.Controls.Add(this.lblTxtSegundoApellido, 1, 2);
            this.editorClienteCampos.Controls.Add(this.txtSegundoApellido, 1, 3);
            this.editorClienteCampos.Controls.Add(this.lblCmbSexo, 2, 2);
            this.editorClienteCampos.Controls.Add(this.cmbSexo, 2, 3);
            this.editorClienteCampos.Controls.Add(this.lblTxtTelefono, 0, 4);
            this.editorClienteCampos.Controls.Add(this.txtTelefono, 0, 5);
            this.editorClienteCampos.Controls.Add(this.lblTxtCorreo, 1, 4);
            this.editorClienteCampos.Controls.Add(this.txtCorreo, 1, 5);
            this.editorClienteCampos.Controls.Add(this.lblTxtDireccion, 2, 4);
            this.editorClienteCampos.Controls.Add(this.txtDireccion, 2, 5);
            this.editorClienteCampos.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorClienteCampos.Location = new System.Drawing.Point(14, 36);
            this.editorClienteCampos.Margin = new System.Windows.Forms.Padding(0);
            this.editorClienteCampos.Name = "editorClienteCampos";
            this.editorClienteCampos.RowCount = 6;
            this.editorClienteCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.editorClienteCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.editorClienteCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.editorClienteCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.editorClienteCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.editorClienteCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.editorClienteCampos.Size = new System.Drawing.Size(1052, 153);
            this.editorClienteCampos.TabIndex = 1;
            // 
            // lblTxtCedula
            // 
            this.lblTxtCedula.AutoEllipsis = true;
            this.lblTxtCedula.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtCedula.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtCedula.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtCedula.Location = new System.Drawing.Point(6, 0);
            this.lblTxtCedula.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtCedula.Name = "lblTxtCedula";
            this.lblTxtCedula.Size = new System.Drawing.Size(345, 20);
            this.lblTxtCedula.TabIndex = 0;
            this.lblTxtCedula.Text = "Cédula";
            this.lblTxtCedula.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTxtPrimerNombre
            // 
            this.lblTxtPrimerNombre.AutoEllipsis = true;
            this.lblTxtPrimerNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtPrimerNombre.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtPrimerNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtPrimerNombre.Location = new System.Drawing.Point(363, 0);
            this.lblTxtPrimerNombre.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtPrimerNombre.Name = "lblTxtPrimerNombre";
            this.lblTxtPrimerNombre.Size = new System.Drawing.Size(335, 20);
            this.lblTxtPrimerNombre.TabIndex = 2;
            this.lblTxtPrimerNombre.Text = "Primer nombre";
            this.lblTxtPrimerNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTxtSegundoNombre
            // 
            this.lblTxtSegundoNombre.AutoEllipsis = true;
            this.lblTxtSegundoNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtSegundoNombre.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtSegundoNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtSegundoNombre.Location = new System.Drawing.Point(710, 0);
            this.lblTxtSegundoNombre.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtSegundoNombre.Name = "lblTxtSegundoNombre";
            this.lblTxtSegundoNombre.Size = new System.Drawing.Size(336, 20);
            this.lblTxtSegundoNombre.TabIndex = 4;
            this.lblTxtSegundoNombre.Text = "Segundo nombre (opcional)";
            this.lblTxtSegundoNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTxtPrimerApellido
            // 
            this.lblTxtPrimerApellido.AutoEllipsis = true;
            this.lblTxtPrimerApellido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtPrimerApellido.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtPrimerApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtPrimerApellido.Location = new System.Drawing.Point(6, 51);
            this.lblTxtPrimerApellido.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtPrimerApellido.Name = "lblTxtPrimerApellido";
            this.lblTxtPrimerApellido.Size = new System.Drawing.Size(345, 20);
            this.lblTxtPrimerApellido.TabIndex = 6;
            this.lblTxtPrimerApellido.Text = "Primer apellido";
            this.lblTxtPrimerApellido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTxtSegundoApellido
            // 
            this.lblTxtSegundoApellido.AutoEllipsis = true;
            this.lblTxtSegundoApellido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtSegundoApellido.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtSegundoApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtSegundoApellido.Location = new System.Drawing.Point(363, 51);
            this.lblTxtSegundoApellido.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtSegundoApellido.Name = "lblTxtSegundoApellido";
            this.lblTxtSegundoApellido.Size = new System.Drawing.Size(335, 20);
            this.lblTxtSegundoApellido.TabIndex = 8;
            this.lblTxtSegundoApellido.Text = "Segundo apellido (opcional)";
            this.lblTxtSegundoApellido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCmbSexo
            // 
            this.lblCmbSexo.AutoEllipsis = true;
            this.lblCmbSexo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCmbSexo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCmbSexo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCmbSexo.Location = new System.Drawing.Point(710, 51);
            this.lblCmbSexo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCmbSexo.Name = "lblCmbSexo";
            this.lblCmbSexo.Size = new System.Drawing.Size(336, 20);
            this.lblCmbSexo.TabIndex = 10;
            this.lblCmbSexo.Text = "Sexo";
            this.lblCmbSexo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTxtTelefono
            // 
            this.lblTxtTelefono.AutoEllipsis = true;
            this.lblTxtTelefono.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtTelefono.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtTelefono.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtTelefono.Location = new System.Drawing.Point(6, 102);
            this.lblTxtTelefono.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtTelefono.Name = "lblTxtTelefono";
            this.lblTxtTelefono.Size = new System.Drawing.Size(345, 20);
            this.lblTxtTelefono.TabIndex = 12;
            this.lblTxtTelefono.Text = "Teléfono";
            this.lblTxtTelefono.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTxtCorreo
            // 
            this.lblTxtCorreo.AutoEllipsis = true;
            this.lblTxtCorreo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtCorreo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtCorreo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtCorreo.Location = new System.Drawing.Point(363, 102);
            this.lblTxtCorreo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtCorreo.Name = "lblTxtCorreo";
            this.lblTxtCorreo.Size = new System.Drawing.Size(335, 20);
            this.lblTxtCorreo.TabIndex = 14;
            this.lblTxtCorreo.Text = "Correo";
            this.lblTxtCorreo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTxtDireccion
            // 
            this.lblTxtDireccion.AutoEllipsis = true;
            this.lblTxtDireccion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtDireccion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtDireccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtDireccion.Location = new System.Drawing.Point(710, 102);
            this.lblTxtDireccion.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtDireccion.Name = "lblTxtDireccion";
            this.lblTxtDireccion.Size = new System.Drawing.Size(336, 20);
            this.lblTxtDireccion.TabIndex = 16;
            this.lblTxtDireccion.Text = "Dirección";
            this.lblTxtDireccion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // editorClienteTitulo
            // 
            this.editorClienteTitulo.AutoEllipsis = true;
            this.editorClienteTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorClienteTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.editorClienteTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.editorClienteTitulo.Location = new System.Drawing.Point(14, 4);
            this.editorClienteTitulo.Margin = new System.Windows.Forms.Padding(0);
            this.editorClienteTitulo.Name = "editorClienteTitulo";
            this.editorClienteTitulo.Size = new System.Drawing.Size(1052, 32);
            this.editorClienteTitulo.TabIndex = 0;
            this.editorClienteTitulo.Text = "Datos del cliente";
            this.editorClienteTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // espacioClientes
            // 
            this.espacioClientes.Dock = System.Windows.Forms.DockStyle.Top;
            this.espacioClientes.Location = new System.Drawing.Point(20, 310);
            this.espacioClientes.Name = "espacioClientes";
            this.espacioClientes.Size = new System.Drawing.Size(1080, 10);
            this.espacioClientes.TabIndex = 2;
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
            this.txtBuscar.Size = new System.Drawing.Size(619, 27);
            this.txtBuscar.TabIndex = 1;
            // 
            // listadoClientes
            // 
            this.listadoClientes.BackColor = System.Drawing.Color.White;
            this.listadoClientes.Controls.Add(this.grid);
            this.listadoClientes.Controls.Add(this.listadoClientesFiltros);
            this.listadoClientes.Controls.Add(this.listadoClientesTitulo);
            this.listadoClientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listadoClientes.Location = new System.Drawing.Point(20, 320);
            this.listadoClientes.Margin = new System.Windows.Forms.Padding(0);
            this.listadoClientes.Name = "listadoClientes";
            this.listadoClientes.Padding = new System.Windows.Forms.Padding(14, 4, 14, 12);
            this.listadoClientes.Size = new System.Drawing.Size(1080, 344);
            this.listadoClientes.TabIndex = 3;
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
            this.gridColCedula,
            this.gridColPrimerNombre,
            this.gridColSegundoNombre,
            this.gridColPrimerApellido,
            this.gridColSegundoApellido,
            this.gridColSexo,
            this.gridColTelefono,
            this.gridColCorreoElectronico,
            this.gridColDireccion});
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
            this.grid.Size = new System.Drawing.Size(1052, 250);
            this.grid.TabIndex = 2;
            // 
            // gridColCedula
            // 
            this.gridColCedula.DataPropertyName = "Cedula";
            this.gridColCedula.FillWeight = 94F;
            this.gridColCedula.HeaderText = "Cédula";
            this.gridColCedula.MinimumWidth = 72;
            this.gridColCedula.Name = "gridColCedula";
            this.gridColCedula.ReadOnly = true;
            // 
            // gridColPrimerNombre
            // 
            this.gridColPrimerNombre.DataPropertyName = "PrimerNombre";
            this.gridColPrimerNombre.FillWeight = 106F;
            this.gridColPrimerNombre.HeaderText = "Primer nombre";
            this.gridColPrimerNombre.MinimumWidth = 78;
            this.gridColPrimerNombre.Name = "gridColPrimerNombre";
            this.gridColPrimerNombre.ReadOnly = true;
            // 
            // gridColSegundoNombre
            // 
            this.gridColSegundoNombre.DataPropertyName = "SegundoNombre";
            this.gridColSegundoNombre.HeaderText = "Segundo nombre";
            this.gridColSegundoNombre.MinimumWidth = 77;
            this.gridColSegundoNombre.Name = "gridColSegundoNombre";
            this.gridColSegundoNombre.ReadOnly = true;
            // 
            // gridColPrimerApellido
            // 
            this.gridColPrimerApellido.DataPropertyName = "PrimerApellido";
            this.gridColPrimerApellido.FillWeight = 106F;
            this.gridColPrimerApellido.HeaderText = "Primer apellido";
            this.gridColPrimerApellido.MinimumWidth = 78;
            this.gridColPrimerApellido.Name = "gridColPrimerApellido";
            this.gridColPrimerApellido.ReadOnly = true;
            // 
            // gridColSegundoApellido
            // 
            this.gridColSegundoApellido.DataPropertyName = "SegundoApellido";
            this.gridColSegundoApellido.HeaderText = "Segundo apellido";
            this.gridColSegundoApellido.MinimumWidth = 77;
            this.gridColSegundoApellido.Name = "gridColSegundoApellido";
            this.gridColSegundoApellido.ReadOnly = true;
            // 
            // gridColSexo
            // 
            this.gridColSexo.DataPropertyName = "Sexo";
            this.gridColSexo.FillWeight = 49F;
            this.gridColSexo.HeaderText = "Sexo";
            this.gridColSexo.MinimumWidth = 44;
            this.gridColSexo.Name = "gridColSexo";
            this.gridColSexo.ReadOnly = true;
            // 
            // gridColTelefono
            // 
            this.gridColTelefono.DataPropertyName = "Telefono";
            this.gridColTelefono.FillWeight = 96F;
            this.gridColTelefono.HeaderText = "Teléfono";
            this.gridColTelefono.MinimumWidth = 78;
            this.gridColTelefono.Name = "gridColTelefono";
            this.gridColTelefono.ReadOnly = true;
            // 
            // gridColCorreoElectronico
            // 
            this.gridColCorreoElectronico.DataPropertyName = "CorreoElectronico";
            this.gridColCorreoElectronico.FillWeight = 135F;
            this.gridColCorreoElectronico.HeaderText = "Correo";
            this.gridColCorreoElectronico.MinimumWidth = 108;
            this.gridColCorreoElectronico.Name = "gridColCorreoElectronico";
            this.gridColCorreoElectronico.ReadOnly = true;
            // 
            // gridColDireccion
            // 
            this.gridColDireccion.DataPropertyName = "Direccion";
            this.gridColDireccion.FillWeight = 138F;
            this.gridColDireccion.HeaderText = "Dirección";
            this.gridColDireccion.MinimumWidth = 109;
            this.gridColDireccion.Name = "gridColDireccion";
            this.gridColDireccion.ReadOnly = true;
            // 
            // listadoClientesFiltros
            // 
            this.listadoClientesFiltros.ColumnCount = 2;
            this.listadoClientesFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.listadoClientesFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.listadoClientesFiltros.Controls.Add(this.lblTxtBuscar, 0, 0);
            this.listadoClientesFiltros.Controls.Add(this.txtBuscar, 0, 1);
            this.listadoClientesFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.listadoClientesFiltros.Location = new System.Drawing.Point(14, 36);
            this.listadoClientesFiltros.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.listadoClientesFiltros.Name = "listadoClientesFiltros";
            this.listadoClientesFiltros.RowCount = 2;
            this.listadoClientesFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.listadoClientesFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.listadoClientesFiltros.Size = new System.Drawing.Size(1052, 46);
            this.listadoClientesFiltros.TabIndex = 1;
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
            this.lblTxtBuscar.Text = "Buscar por cédula o nombre";
            this.lblTxtBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listadoClientesTitulo
            // 
            this.listadoClientesTitulo.AutoEllipsis = true;
            this.listadoClientesTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.listadoClientesTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.listadoClientesTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.listadoClientesTitulo.Location = new System.Drawing.Point(14, 4);
            this.listadoClientesTitulo.Margin = new System.Windows.Forms.Padding(0);
            this.listadoClientesTitulo.Name = "listadoClientesTitulo";
            this.listadoClientesTitulo.Size = new System.Drawing.Size(1052, 32);
            this.listadoClientesTitulo.TabIndex = 0;
            this.listadoClientesTitulo.Text = "Clientes registrados";
            this.listadoClientesTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1120, 680);
            this.Controls.Add(this.listadoClientes);
            this.Controls.Add(this.espacioClientes);
            this.Controls.Add(this.editorCliente);
            this.Controls.Add(this.encabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.MinimumSize = new System.Drawing.Size(680, 440);
            this.Name = "FrmClientes";
            this.Padding = new System.Windows.Forms.Padding(20, 8, 20, 16);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gestión de clientes";
            ((System.ComponentModel.ISupportInitialize)(this.errores)).EndInit();
            this.encabezado.ResumeLayout(false);
            this.editorCliente.ResumeLayout(false);
            this.editorCliente.PerformLayout();
            this.editorClienteAcciones.ResumeLayout(false);
            this.editorClienteCampos.ResumeLayout(false);
            this.editorClienteCampos.PerformLayout();
            this.listadoClientes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.listadoClientesFiltros.ResumeLayout(false);
            this.listadoClientesFiltros.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel encabezado;
        private Label lblTitulo;
        private Panel acentoTitulo;
        private TextBox txtCedula;
        private TextBox txtPrimerNombre;
        private TextBox txtSegundoNombre;
        private TextBox txtPrimerApellido;
        private TextBox txtSegundoApellido;
        private ComboBox cmbSexo;
        private TextBox txtTelefono;
        private TextBox txtCorreo;
        private TextBox txtDireccion;
        private Button btnGuardar;
        private Button btnNuevo;
        private Button btnEliminar;
        private Panel editorCliente;
        private TableLayoutPanel editorClienteCampos;
        private Label lblTxtCedula;
        private Label lblTxtPrimerNombre;
        private Label lblTxtSegundoNombre;
        private Label lblTxtPrimerApellido;
        private Label lblTxtSegundoApellido;
        private Label lblCmbSexo;
        private Label lblTxtTelefono;
        private Label lblTxtCorreo;
        private Label lblTxtDireccion;
        private FlowLayoutPanel editorClienteAcciones;
        private Label editorClienteTitulo;
        private Panel espacioClientes;
        private TextBox txtBuscar;
        private Panel listadoClientes;
        private Label listadoClientesTitulo;
        private TableLayoutPanel listadoClientesFiltros;
        private Label lblTxtBuscar;
        private DataGridView grid;
        private DataGridViewTextBoxColumn gridColCedula;
        private DataGridViewTextBoxColumn gridColPrimerNombre;
        private DataGridViewTextBoxColumn gridColSegundoNombre;
        private DataGridViewTextBoxColumn gridColPrimerApellido;
        private DataGridViewTextBoxColumn gridColSegundoApellido;
        private DataGridViewTextBoxColumn gridColSexo;
        private DataGridViewTextBoxColumn gridColTelefono;
        private DataGridViewTextBoxColumn gridColCorreoElectronico;
        private DataGridViewTextBoxColumn gridColDireccion;
    }
}
