using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PRESENTACION
{
    partial class FrmEmpleados
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.errores = new System.Windows.Forms.ErrorProvider(this.components);
            this.encabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.acentoTitulo = new System.Windows.Forms.Panel();
            this.columnasEmpleados = new System.Windows.Forms.TableLayoutPanel();
            this.panelEmpleados = new System.Windows.Forms.Panel();
            this.listadoEmpleados = new System.Windows.Forms.Panel();
            this.gridEmpleados = new System.Windows.Forms.DataGridView();
            this.Cedula = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrimerNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SegundoNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrimerApellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SegundoApellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sexo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Telefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CorreoElectronico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cargo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listadoEmpleadosFiltros = new System.Windows.Forms.TableLayoutPanel();
            this.lblTxtBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.listadoEmpleadosTitulo = new System.Windows.Forms.Label();
            this.espacioEmpleados = new System.Windows.Forms.Panel();
            this.editorEmpleado = new System.Windows.Forms.Panel();
            this.editorEmpleadoAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGuardarEmpleado = new System.Windows.Forms.Button();
            this.btnNuevoEmpleado = new System.Windows.Forms.Button();
            this.btnEliminarEmpleado = new System.Windows.Forms.Button();
            this.btnClaveEmpleado = new System.Windows.Forms.Button();
            this.editorEmpleadoCampos = new System.Windows.Forms.TableLayoutPanel();
            this.lblTxtCedula = new System.Windows.Forms.Label();
            this.txtCedula = new System.Windows.Forms.TextBox();
            this.lblTxtPrimerNombre = new System.Windows.Forms.Label();
            this.txtPrimerNombre = new System.Windows.Forms.TextBox();
            this.lblTxtSegundoNombre = new System.Windows.Forms.Label();
            this.txtSegundoNombre = new System.Windows.Forms.TextBox();
            this.lblTxtPrimerApellido = new System.Windows.Forms.Label();
            this.txtPrimerApellido = new System.Windows.Forms.TextBox();
            this.lblTxtSegundoApellido = new System.Windows.Forms.Label();
            this.txtSegundoApellido = new System.Windows.Forms.TextBox();
            this.lblCmbSexo = new System.Windows.Forms.Label();
            this.cmbSexo = new System.Windows.Forms.ComboBox();
            this.lblTxtTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblTxtCorreo = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.lblTxtDireccion = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.lblCmbCargo = new System.Windows.Forms.Label();
            this.cmbCargo = new System.Windows.Forms.ComboBox();
            this.chkActivo = new System.Windows.Forms.CheckBox();
            this.editorEmpleadoTitulo = new System.Windows.Forms.Label();
            this.panelCargos = new System.Windows.Forms.Panel();
            this.gestionCargos = new System.Windows.Forms.Panel();
            this.gridCargos = new System.Windows.Forms.DataGridView();
            this.CargoId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreCargo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.espacioCargos = new System.Windows.Forms.Panel();
            this.accionesCargo = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCrearCargo = new System.Windows.Forms.Button();
            this.btnEliminarCargo = new System.Windows.Forms.Button();
            this.camposCargo = new System.Windows.Forms.TableLayoutPanel();
            this.lblTxtCargo = new System.Windows.Forms.Label();
            this.txtCargo = new System.Windows.Forms.TextBox();
            this.lblTituloCargos = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errores)).BeginInit();
            this.encabezado.SuspendLayout();
            this.columnasEmpleados.SuspendLayout();
            this.panelEmpleados.SuspendLayout();
            this.listadoEmpleados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridEmpleados)).BeginInit();
            this.listadoEmpleadosFiltros.SuspendLayout();
            this.editorEmpleado.SuspendLayout();
            this.editorEmpleadoAcciones.SuspendLayout();
            this.editorEmpleadoCampos.SuspendLayout();
            this.panelCargos.SuspendLayout();
            this.gestionCargos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCargos)).BeginInit();
            this.accionesCargo.SuspendLayout();
            this.camposCargo.SuspendLayout();
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
            this.encabezado.Size = new System.Drawing.Size(1180, 57);
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
            this.lblTitulo.Size = new System.Drawing.Size(1176, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Empleados y cargos";
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
            // columnasEmpleados
            // 
            this.columnasEmpleados.ColumnCount = 2;
            this.columnasEmpleados.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.columnasEmpleados.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 270F));
            this.columnasEmpleados.Controls.Add(this.panelEmpleados, 0, 0);
            this.columnasEmpleados.Controls.Add(this.panelCargos, 1, 0);
            this.columnasEmpleados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.columnasEmpleados.Location = new System.Drawing.Point(20, 65);
            this.columnasEmpleados.Margin = new System.Windows.Forms.Padding(0);
            this.columnasEmpleados.Name = "columnasEmpleados";
            this.columnasEmpleados.RowCount = 1;
            this.columnasEmpleados.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.columnasEmpleados.Size = new System.Drawing.Size(1180, 619);
            this.columnasEmpleados.TabIndex = 1;
            // 
            // panelEmpleados
            // 
            this.panelEmpleados.Controls.Add(this.listadoEmpleados);
            this.panelEmpleados.Controls.Add(this.espacioEmpleados);
            this.panelEmpleados.Controls.Add(this.editorEmpleado);
            this.panelEmpleados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEmpleados.Location = new System.Drawing.Point(0, 0);
            this.panelEmpleados.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.panelEmpleados.Name = "panelEmpleados";
            this.panelEmpleados.Size = new System.Drawing.Size(900, 619);
            this.panelEmpleados.TabIndex = 0;
            // 
            // listadoEmpleados
            // 
            this.listadoEmpleados.BackColor = System.Drawing.Color.White;
            this.listadoEmpleados.Controls.Add(this.gridEmpleados);
            this.listadoEmpleados.Controls.Add(this.listadoEmpleadosFiltros);
            this.listadoEmpleados.Controls.Add(this.listadoEmpleadosTitulo);
            this.listadoEmpleados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listadoEmpleados.Location = new System.Drawing.Point(0, 286);
            this.listadoEmpleados.Margin = new System.Windows.Forms.Padding(0);
            this.listadoEmpleados.Name = "listadoEmpleados";
            this.listadoEmpleados.Padding = new System.Windows.Forms.Padding(14, 4, 14, 12);
            this.listadoEmpleados.Size = new System.Drawing.Size(900, 333);
            this.listadoEmpleados.TabIndex = 2;
            // 
            // gridEmpleados
            // 
            this.gridEmpleados.AllowUserToAddRows = false;
            this.gridEmpleados.AllowUserToDeleteRows = false;
            this.gridEmpleados.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.gridEmpleados.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridEmpleados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridEmpleados.BackgroundColor = System.Drawing.Color.White;
            this.gridEmpleados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridEmpleados.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridEmpleados.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5, 0, 2, 0);
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridEmpleados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridEmpleados.ColumnHeadersHeight = 35;
            this.gridEmpleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridEmpleados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Cedula,
            this.PrimerNombre,
            this.SegundoNombre,
            this.PrimerApellido,
            this.SegundoApellido,
            this.Sexo,
            this.Telefono,
            this.CorreoElectronico,
            this.Cargo});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(5, 0, 3, 0);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridEmpleados.DefaultCellStyle = dataGridViewCellStyle4;
            this.gridEmpleados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridEmpleados.EnableHeadersVisualStyles = false;
            this.gridEmpleados.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(227)))), ((int)(((byte)(238)))));
            this.gridEmpleados.Location = new System.Drawing.Point(14, 82);
            this.gridEmpleados.Margin = new System.Windows.Forms.Padding(0);
            this.gridEmpleados.MultiSelect = false;
            this.gridEmpleados.Name = "gridEmpleados";
            this.gridEmpleados.ReadOnly = true;
            this.gridEmpleados.RowHeadersVisible = false;
            this.gridEmpleados.RowHeadersWidth = 51;
            this.gridEmpleados.RowTemplate.Height = 30;
            this.gridEmpleados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridEmpleados.Size = new System.Drawing.Size(872, 239);
            this.gridEmpleados.TabIndex = 2;
            // 
            // Cedula
            // 
            this.Cedula.DataPropertyName = "Cedula";
            this.Cedula.FillWeight = 94F;
            this.Cedula.HeaderText = "Cédula";
            this.Cedula.MinimumWidth = 72;
            this.Cedula.Name = "Cedula";
            this.Cedula.ReadOnly = true;
            this.Cedula.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PrimerNombre
            // 
            this.PrimerNombre.DataPropertyName = "PrimerNombre";
            this.PrimerNombre.FillWeight = 106F;
            this.PrimerNombre.HeaderText = "Primer nombre";
            this.PrimerNombre.MinimumWidth = 78;
            this.PrimerNombre.Name = "PrimerNombre";
            this.PrimerNombre.ReadOnly = true;
            this.PrimerNombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SegundoNombre
            // 
            this.SegundoNombre.DataPropertyName = "SegundoNombre";
            this.SegundoNombre.HeaderText = "Segundo nombre";
            this.SegundoNombre.MinimumWidth = 77;
            this.SegundoNombre.Name = "SegundoNombre";
            this.SegundoNombre.ReadOnly = true;
            this.SegundoNombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PrimerApellido
            // 
            this.PrimerApellido.DataPropertyName = "PrimerApellido";
            this.PrimerApellido.FillWeight = 106F;
            this.PrimerApellido.HeaderText = "Primer apellido";
            this.PrimerApellido.MinimumWidth = 78;
            this.PrimerApellido.Name = "PrimerApellido";
            this.PrimerApellido.ReadOnly = true;
            this.PrimerApellido.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SegundoApellido
            // 
            this.SegundoApellido.DataPropertyName = "SegundoApellido";
            this.SegundoApellido.HeaderText = "Segundo apellido";
            this.SegundoApellido.MinimumWidth = 77;
            this.SegundoApellido.Name = "SegundoApellido";
            this.SegundoApellido.ReadOnly = true;
            this.SegundoApellido.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Sexo
            // 
            this.Sexo.DataPropertyName = "Sexo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Sexo.DefaultCellStyle = dataGridViewCellStyle3;
            this.Sexo.FillWeight = 49F;
            this.Sexo.HeaderText = "Sexo";
            this.Sexo.MinimumWidth = 44;
            this.Sexo.Name = "Sexo";
            this.Sexo.ReadOnly = true;
            this.Sexo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Telefono
            // 
            this.Telefono.DataPropertyName = "Telefono";
            this.Telefono.FillWeight = 96F;
            this.Telefono.HeaderText = "Teléfono";
            this.Telefono.MinimumWidth = 78;
            this.Telefono.Name = "Telefono";
            this.Telefono.ReadOnly = true;
            this.Telefono.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CorreoElectronico
            // 
            this.CorreoElectronico.DataPropertyName = "CorreoElectronico";
            this.CorreoElectronico.FillWeight = 135F;
            this.CorreoElectronico.HeaderText = "Correo";
            this.CorreoElectronico.MinimumWidth = 108;
            this.CorreoElectronico.Name = "CorreoElectronico";
            this.CorreoElectronico.ReadOnly = true;
            this.CorreoElectronico.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Cargo
            // 
            this.Cargo.FillWeight = 104F;
            this.Cargo.HeaderText = "Cargo";
            this.Cargo.MinimumWidth = 88;
            this.Cargo.Name = "Cargo";
            this.Cargo.ReadOnly = true;
            this.Cargo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // listadoEmpleadosFiltros
            // 
            this.listadoEmpleadosFiltros.ColumnCount = 1;
            this.listadoEmpleadosFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.listadoEmpleadosFiltros.Controls.Add(this.lblTxtBuscar, 0, 0);
            this.listadoEmpleadosFiltros.Controls.Add(this.txtBuscar, 0, 1);
            this.listadoEmpleadosFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.listadoEmpleadosFiltros.Location = new System.Drawing.Point(14, 36);
            this.listadoEmpleadosFiltros.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.listadoEmpleadosFiltros.Name = "listadoEmpleadosFiltros";
            this.listadoEmpleadosFiltros.RowCount = 2;
            this.listadoEmpleadosFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.listadoEmpleadosFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.listadoEmpleadosFiltros.Size = new System.Drawing.Size(872, 46);
            this.listadoEmpleadosFiltros.TabIndex = 1;
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
            this.lblTxtBuscar.Size = new System.Drawing.Size(860, 17);
            this.lblTxtBuscar.TabIndex = 0;
            this.lblTxtBuscar.Text = "Buscar por cédula o nombre";
            this.lblTxtBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.txtBuscar.Size = new System.Drawing.Size(860, 27);
            this.txtBuscar.TabIndex = 1;
            // 
            // listadoEmpleadosTitulo
            // 
            this.listadoEmpleadosTitulo.AutoEllipsis = true;
            this.listadoEmpleadosTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.listadoEmpleadosTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.listadoEmpleadosTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.listadoEmpleadosTitulo.Location = new System.Drawing.Point(14, 4);
            this.listadoEmpleadosTitulo.Margin = new System.Windows.Forms.Padding(0);
            this.listadoEmpleadosTitulo.Name = "listadoEmpleadosTitulo";
            this.listadoEmpleadosTitulo.Size = new System.Drawing.Size(872, 32);
            this.listadoEmpleadosTitulo.TabIndex = 0;
            this.listadoEmpleadosTitulo.Text = "Empleados registrados";
            this.listadoEmpleadosTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // espacioEmpleados
            // 
            this.espacioEmpleados.Dock = System.Windows.Forms.DockStyle.Top;
            this.espacioEmpleados.Location = new System.Drawing.Point(0, 276);
            this.espacioEmpleados.Name = "espacioEmpleados";
            this.espacioEmpleados.Size = new System.Drawing.Size(900, 10);
            this.espacioEmpleados.TabIndex = 1;
            // 
            // editorEmpleado
            // 
            this.editorEmpleado.AutoSize = true;
            this.editorEmpleado.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editorEmpleado.BackColor = System.Drawing.Color.White;
            this.editorEmpleado.Controls.Add(this.editorEmpleadoAcciones);
            this.editorEmpleado.Controls.Add(this.editorEmpleadoCampos);
            this.editorEmpleado.Controls.Add(this.editorEmpleadoTitulo);
            this.editorEmpleado.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorEmpleado.Location = new System.Drawing.Point(0, 0);
            this.editorEmpleado.Margin = new System.Windows.Forms.Padding(0);
            this.editorEmpleado.MinimumSize = new System.Drawing.Size(2, 276);
            this.editorEmpleado.Name = "editorEmpleado";
            this.editorEmpleado.Padding = new System.Windows.Forms.Padding(14, 4, 14, 10);
            this.editorEmpleado.Size = new System.Drawing.Size(900, 276);
            this.editorEmpleado.TabIndex = 0;
            // 
            // editorEmpleadoAcciones
            // 
            this.editorEmpleadoAcciones.AutoSize = true;
            this.editorEmpleadoAcciones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editorEmpleadoAcciones.Controls.Add(this.btnGuardarEmpleado);
            this.editorEmpleadoAcciones.Controls.Add(this.btnNuevoEmpleado);
            this.editorEmpleadoAcciones.Controls.Add(this.btnEliminarEmpleado);
            this.editorEmpleadoAcciones.Controls.Add(this.btnClaveEmpleado);
            this.editorEmpleadoAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorEmpleadoAcciones.Location = new System.Drawing.Point(14, 220);
            this.editorEmpleadoAcciones.Name = "editorEmpleadoAcciones";
            this.editorEmpleadoAcciones.Padding = new System.Windows.Forms.Padding(6, 6, 0, 0);
            this.editorEmpleadoAcciones.Size = new System.Drawing.Size(872, 44);
            this.editorEmpleadoAcciones.TabIndex = 2;
            // 
            // btnGuardarEmpleado
            // 
            this.btnGuardarEmpleado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnGuardarEmpleado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarEmpleado.FlatAppearance.BorderSize = 0;
            this.btnGuardarEmpleado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnGuardarEmpleado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarEmpleado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGuardarEmpleado.ForeColor = System.Drawing.Color.White;
            this.btnGuardarEmpleado.Location = new System.Drawing.Point(6, 9);
            this.btnGuardarEmpleado.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnGuardarEmpleado.Name = "btnGuardarEmpleado";
            this.btnGuardarEmpleado.Size = new System.Drawing.Size(112, 32);
            this.btnGuardarEmpleado.TabIndex = 0;
            this.btnGuardarEmpleado.Text = "Guardar";
            this.btnGuardarEmpleado.UseVisualStyleBackColor = false;
            // 
            // btnNuevoEmpleado
            // 
            this.btnNuevoEmpleado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.btnNuevoEmpleado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoEmpleado.FlatAppearance.BorderSize = 0;
            this.btnNuevoEmpleado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.btnNuevoEmpleado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoEmpleado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevoEmpleado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.btnNuevoEmpleado.Location = new System.Drawing.Point(126, 9);
            this.btnNuevoEmpleado.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnNuevoEmpleado.Name = "btnNuevoEmpleado";
            this.btnNuevoEmpleado.Size = new System.Drawing.Size(86, 32);
            this.btnNuevoEmpleado.TabIndex = 1;
            this.btnNuevoEmpleado.Text = "Nuevo";
            this.btnNuevoEmpleado.UseVisualStyleBackColor = false;
            // 
            // btnEliminarEmpleado
            // 
            this.btnEliminarEmpleado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnEliminarEmpleado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarEmpleado.FlatAppearance.BorderSize = 0;
            this.btnEliminarEmpleado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnEliminarEmpleado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarEmpleado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEliminarEmpleado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.btnEliminarEmpleado.Location = new System.Drawing.Point(220, 9);
            this.btnEliminarEmpleado.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnEliminarEmpleado.Name = "btnEliminarEmpleado";
            this.btnEliminarEmpleado.Size = new System.Drawing.Size(90, 32);
            this.btnEliminarEmpleado.TabIndex = 2;
            this.btnEliminarEmpleado.Text = "Eliminar";
            this.btnEliminarEmpleado.UseVisualStyleBackColor = false;
            // 
            // btnClaveEmpleado
            // 
            this.btnClaveEmpleado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.btnClaveEmpleado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClaveEmpleado.FlatAppearance.BorderSize = 0;
            this.btnClaveEmpleado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.btnClaveEmpleado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClaveEmpleado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClaveEmpleado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.btnClaveEmpleado.Location = new System.Drawing.Point(318, 9);
            this.btnClaveEmpleado.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnClaveEmpleado.Name = "btnClaveEmpleado";
            this.btnClaveEmpleado.Size = new System.Drawing.Size(136, 32);
            this.btnClaveEmpleado.TabIndex = 3;
            this.btnClaveEmpleado.Text = "Restablecer clave";
            this.btnClaveEmpleado.UseVisualStyleBackColor = false;
            // 
            // editorEmpleadoCampos
            // 
            this.editorEmpleadoCampos.ColumnCount = 3;
            this.editorEmpleadoCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.editorEmpleadoCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.editorEmpleadoCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.editorEmpleadoCampos.Controls.Add(this.lblTxtCedula, 0, 0);
            this.editorEmpleadoCampos.Controls.Add(this.txtCedula, 0, 1);
            this.editorEmpleadoCampos.Controls.Add(this.lblTxtPrimerNombre, 1, 0);
            this.editorEmpleadoCampos.Controls.Add(this.txtPrimerNombre, 1, 1);
            this.editorEmpleadoCampos.Controls.Add(this.lblTxtSegundoNombre, 2, 0);
            this.editorEmpleadoCampos.Controls.Add(this.txtSegundoNombre, 2, 1);
            this.editorEmpleadoCampos.Controls.Add(this.lblTxtPrimerApellido, 0, 2);
            this.editorEmpleadoCampos.Controls.Add(this.txtPrimerApellido, 0, 3);
            this.editorEmpleadoCampos.Controls.Add(this.lblTxtSegundoApellido, 1, 2);
            this.editorEmpleadoCampos.Controls.Add(this.txtSegundoApellido, 1, 3);
            this.editorEmpleadoCampos.Controls.Add(this.lblCmbSexo, 2, 2);
            this.editorEmpleadoCampos.Controls.Add(this.cmbSexo, 2, 3);
            this.editorEmpleadoCampos.Controls.Add(this.lblTxtTelefono, 0, 4);
            this.editorEmpleadoCampos.Controls.Add(this.txtTelefono, 0, 5);
            this.editorEmpleadoCampos.Controls.Add(this.lblTxtCorreo, 1, 4);
            this.editorEmpleadoCampos.Controls.Add(this.txtCorreo, 1, 5);
            this.editorEmpleadoCampos.Controls.Add(this.lblTxtDireccion, 2, 4);
            this.editorEmpleadoCampos.Controls.Add(this.txtDireccion, 2, 5);
            this.editorEmpleadoCampos.Controls.Add(this.lblCmbCargo, 0, 6);
            this.editorEmpleadoCampos.Controls.Add(this.cmbCargo, 0, 7);
            this.editorEmpleadoCampos.Controls.Add(this.chkActivo, 2, 6);
            this.editorEmpleadoCampos.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorEmpleadoCampos.Location = new System.Drawing.Point(14, 36);
            this.editorEmpleadoCampos.Margin = new System.Windows.Forms.Padding(0);
            this.editorEmpleadoCampos.Name = "editorEmpleadoCampos";
            this.editorEmpleadoCampos.RowCount = 8;
            this.editorEmpleadoCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.editorEmpleadoCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.editorEmpleadoCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.editorEmpleadoCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.editorEmpleadoCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.editorEmpleadoCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.editorEmpleadoCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.editorEmpleadoCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.editorEmpleadoCampos.Size = new System.Drawing.Size(872, 184);
            this.editorEmpleadoCampos.TabIndex = 1;
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
            this.lblTxtCedula.Size = new System.Drawing.Size(284, 17);
            this.lblTxtCedula.TabIndex = 0;
            this.lblTxtCedula.Text = "Cédula";
            this.lblTxtCedula.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCedula
            // 
            this.txtCedula.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtCedula.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCedula.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCedula.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtCedula.Location = new System.Drawing.Point(6, 20);
            this.txtCedula.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtCedula.MaxLength = 10;
            this.txtCedula.Name = "txtCedula";
            this.txtCedula.Size = new System.Drawing.Size(284, 27);
            this.txtCedula.TabIndex = 1;
            // 
            // lblTxtPrimerNombre
            // 
            this.lblTxtPrimerNombre.AutoEllipsis = true;
            this.lblTxtPrimerNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtPrimerNombre.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtPrimerNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtPrimerNombre.Location = new System.Drawing.Point(302, 0);
            this.lblTxtPrimerNombre.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtPrimerNombre.Name = "lblTxtPrimerNombre";
            this.lblTxtPrimerNombre.Size = new System.Drawing.Size(275, 17);
            this.lblTxtPrimerNombre.TabIndex = 2;
            this.lblTxtPrimerNombre.Text = "Primer nombre";
            this.lblTxtPrimerNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPrimerNombre
            // 
            this.txtPrimerNombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtPrimerNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrimerNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPrimerNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtPrimerNombre.Location = new System.Drawing.Point(302, 20);
            this.txtPrimerNombre.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtPrimerNombre.MaxLength = 100;
            this.txtPrimerNombre.Name = "txtPrimerNombre";
            this.txtPrimerNombre.Size = new System.Drawing.Size(275, 27);
            this.txtPrimerNombre.TabIndex = 3;
            // 
            // lblTxtSegundoNombre
            // 
            this.lblTxtSegundoNombre.AutoEllipsis = true;
            this.lblTxtSegundoNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtSegundoNombre.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtSegundoNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtSegundoNombre.Location = new System.Drawing.Point(589, 0);
            this.lblTxtSegundoNombre.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtSegundoNombre.Name = "lblTxtSegundoNombre";
            this.lblTxtSegundoNombre.Size = new System.Drawing.Size(277, 17);
            this.lblTxtSegundoNombre.TabIndex = 4;
            this.lblTxtSegundoNombre.Text = "Segundo nombre (opcional)";
            this.lblTxtSegundoNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSegundoNombre
            // 
            this.txtSegundoNombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtSegundoNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSegundoNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSegundoNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtSegundoNombre.Location = new System.Drawing.Point(589, 20);
            this.txtSegundoNombre.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtSegundoNombre.MaxLength = 100;
            this.txtSegundoNombre.Name = "txtSegundoNombre";
            this.txtSegundoNombre.Size = new System.Drawing.Size(277, 27);
            this.txtSegundoNombre.TabIndex = 5;
            // 
            // lblTxtPrimerApellido
            // 
            this.lblTxtPrimerApellido.AutoEllipsis = true;
            this.lblTxtPrimerApellido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtPrimerApellido.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtPrimerApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtPrimerApellido.Location = new System.Drawing.Point(6, 46);
            this.lblTxtPrimerApellido.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtPrimerApellido.Name = "lblTxtPrimerApellido";
            this.lblTxtPrimerApellido.Size = new System.Drawing.Size(284, 17);
            this.lblTxtPrimerApellido.TabIndex = 6;
            this.lblTxtPrimerApellido.Text = "Primer apellido";
            this.lblTxtPrimerApellido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPrimerApellido
            // 
            this.txtPrimerApellido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtPrimerApellido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrimerApellido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPrimerApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtPrimerApellido.Location = new System.Drawing.Point(6, 66);
            this.txtPrimerApellido.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtPrimerApellido.MaxLength = 100;
            this.txtPrimerApellido.Name = "txtPrimerApellido";
            this.txtPrimerApellido.Size = new System.Drawing.Size(284, 27);
            this.txtPrimerApellido.TabIndex = 7;
            // 
            // lblTxtSegundoApellido
            // 
            this.lblTxtSegundoApellido.AutoEllipsis = true;
            this.lblTxtSegundoApellido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtSegundoApellido.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtSegundoApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtSegundoApellido.Location = new System.Drawing.Point(302, 46);
            this.lblTxtSegundoApellido.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtSegundoApellido.Name = "lblTxtSegundoApellido";
            this.lblTxtSegundoApellido.Size = new System.Drawing.Size(275, 17);
            this.lblTxtSegundoApellido.TabIndex = 8;
            this.lblTxtSegundoApellido.Text = "Segundo apellido (opcional)";
            this.lblTxtSegundoApellido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSegundoApellido
            // 
            this.txtSegundoApellido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtSegundoApellido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSegundoApellido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSegundoApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtSegundoApellido.Location = new System.Drawing.Point(302, 66);
            this.txtSegundoApellido.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtSegundoApellido.MaxLength = 100;
            this.txtSegundoApellido.Name = "txtSegundoApellido";
            this.txtSegundoApellido.Size = new System.Drawing.Size(275, 27);
            this.txtSegundoApellido.TabIndex = 9;
            // 
            // lblCmbSexo
            // 
            this.lblCmbSexo.AutoEllipsis = true;
            this.lblCmbSexo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCmbSexo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCmbSexo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCmbSexo.Location = new System.Drawing.Point(589, 46);
            this.lblCmbSexo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCmbSexo.Name = "lblCmbSexo";
            this.lblCmbSexo.Size = new System.Drawing.Size(277, 17);
            this.lblCmbSexo.TabIndex = 10;
            this.lblCmbSexo.Text = "Sexo";
            this.lblCmbSexo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.cmbSexo.Location = new System.Drawing.Point(589, 66);
            this.cmbSexo.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbSexo.Name = "cmbSexo";
            this.cmbSexo.Size = new System.Drawing.Size(277, 28);
            this.cmbSexo.TabIndex = 11;
            // 
            // lblTxtTelefono
            // 
            this.lblTxtTelefono.AutoEllipsis = true;
            this.lblTxtTelefono.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtTelefono.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtTelefono.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtTelefono.Location = new System.Drawing.Point(6, 92);
            this.lblTxtTelefono.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtTelefono.Name = "lblTxtTelefono";
            this.lblTxtTelefono.Size = new System.Drawing.Size(284, 17);
            this.lblTxtTelefono.TabIndex = 12;
            this.lblTxtTelefono.Text = "Teléfono";
            this.lblTxtTelefono.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTelefono
            // 
            this.txtTelefono.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtTelefono.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTelefono.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTelefono.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtTelefono.Location = new System.Drawing.Point(6, 112);
            this.txtTelefono.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtTelefono.MaxLength = 15;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(284, 27);
            this.txtTelefono.TabIndex = 13;
            // 
            // lblTxtCorreo
            // 
            this.lblTxtCorreo.AutoEllipsis = true;
            this.lblTxtCorreo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtCorreo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtCorreo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtCorreo.Location = new System.Drawing.Point(302, 92);
            this.lblTxtCorreo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtCorreo.Name = "lblTxtCorreo";
            this.lblTxtCorreo.Size = new System.Drawing.Size(275, 17);
            this.lblTxtCorreo.TabIndex = 14;
            this.lblTxtCorreo.Text = "Correo";
            this.lblTxtCorreo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCorreo
            // 
            this.txtCorreo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtCorreo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCorreo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCorreo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtCorreo.Location = new System.Drawing.Point(302, 112);
            this.txtCorreo.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtCorreo.MaxLength = 100;
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new System.Drawing.Size(275, 27);
            this.txtCorreo.TabIndex = 15;
            // 
            // lblTxtDireccion
            // 
            this.lblTxtDireccion.AutoEllipsis = true;
            this.lblTxtDireccion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtDireccion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtDireccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtDireccion.Location = new System.Drawing.Point(589, 92);
            this.lblTxtDireccion.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtDireccion.Name = "lblTxtDireccion";
            this.lblTxtDireccion.Size = new System.Drawing.Size(277, 17);
            this.lblTxtDireccion.TabIndex = 16;
            this.lblTxtDireccion.Text = "Dirección";
            this.lblTxtDireccion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDireccion
            // 
            this.txtDireccion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtDireccion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDireccion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDireccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtDireccion.Location = new System.Drawing.Point(589, 112);
            this.txtDireccion.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtDireccion.MaxLength = 150;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(277, 27);
            this.txtDireccion.TabIndex = 17;
            // 
            // lblCmbCargo
            // 
            this.lblCmbCargo.AutoEllipsis = true;
            this.editorEmpleadoCampos.SetColumnSpan(this.lblCmbCargo, 2);
            this.lblCmbCargo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCmbCargo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCmbCargo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCmbCargo.Location = new System.Drawing.Point(6, 138);
            this.lblCmbCargo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCmbCargo.Name = "lblCmbCargo";
            this.lblCmbCargo.Size = new System.Drawing.Size(571, 17);
            this.lblCmbCargo.TabIndex = 18;
            this.lblCmbCargo.Text = "Cargo";
            this.lblCmbCargo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCargo
            // 
            this.cmbCargo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.editorEmpleadoCampos.SetColumnSpan(this.cmbCargo, 2);
            this.cmbCargo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCargo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCargo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.cmbCargo.Location = new System.Drawing.Point(6, 158);
            this.cmbCargo.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.cmbCargo.Name = "cmbCargo";
            this.cmbCargo.Size = new System.Drawing.Size(571, 28);
            this.cmbCargo.TabIndex = 19;
            // 
            // chkActivo
            // 
            this.chkActivo.AutoSize = true;
            this.chkActivo.Checked = true;
            this.chkActivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActivo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkActivo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.chkActivo.Location = new System.Drawing.Point(589, 141);
            this.chkActivo.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.chkActivo.Name = "chkActivo";
            this.editorEmpleadoCampos.SetRowSpan(this.chkActivo, 2);
            this.chkActivo.Size = new System.Drawing.Size(277, 42);
            this.chkActivo.TabIndex = 20;
            this.chkActivo.Text = "Empleado activo";
            // 
            // editorEmpleadoTitulo
            // 
            this.editorEmpleadoTitulo.AutoEllipsis = true;
            this.editorEmpleadoTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.editorEmpleadoTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.editorEmpleadoTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.editorEmpleadoTitulo.Location = new System.Drawing.Point(14, 4);
            this.editorEmpleadoTitulo.Margin = new System.Windows.Forms.Padding(0);
            this.editorEmpleadoTitulo.Name = "editorEmpleadoTitulo";
            this.editorEmpleadoTitulo.Size = new System.Drawing.Size(872, 32);
            this.editorEmpleadoTitulo.TabIndex = 0;
            this.editorEmpleadoTitulo.Text = "Datos del empleado";
            this.editorEmpleadoTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelCargos
            // 
            this.panelCargos.Controls.Add(this.gestionCargos);
            this.panelCargos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCargos.Location = new System.Drawing.Point(910, 0);
            this.panelCargos.Margin = new System.Windows.Forms.Padding(0);
            this.panelCargos.Name = "panelCargos";
            this.panelCargos.Size = new System.Drawing.Size(270, 619);
            this.panelCargos.TabIndex = 1;
            // 
            // gestionCargos
            // 
            this.gestionCargos.BackColor = System.Drawing.Color.White;
            this.gestionCargos.Controls.Add(this.gridCargos);
            this.gestionCargos.Controls.Add(this.espacioCargos);
            this.gestionCargos.Controls.Add(this.accionesCargo);
            this.gestionCargos.Controls.Add(this.camposCargo);
            this.gestionCargos.Controls.Add(this.lblTituloCargos);
            this.gestionCargos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gestionCargos.Location = new System.Drawing.Point(0, 0);
            this.gestionCargos.Margin = new System.Windows.Forms.Padding(0);
            this.gestionCargos.Name = "gestionCargos";
            this.gestionCargos.Padding = new System.Windows.Forms.Padding(12, 4, 12, 12);
            this.gestionCargos.Size = new System.Drawing.Size(270, 619);
            this.gestionCargos.TabIndex = 0;
            // 
            // gridCargos
            // 
            this.gridCargos.AllowUserToAddRows = false;
            this.gridCargos.AllowUserToDeleteRows = false;
            this.gridCargos.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.gridCargos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gridCargos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridCargos.BackgroundColor = System.Drawing.Color.White;
            this.gridCargos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridCargos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridCargos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(5, 0, 2, 0);
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridCargos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gridCargos.ColumnHeadersHeight = 35;
            this.gridCargos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridCargos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CargoId,
            this.NombreCargo});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            dataGridViewCellStyle8.Padding = new System.Windows.Forms.Padding(5, 0, 3, 0);
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridCargos.DefaultCellStyle = dataGridViewCellStyle8;
            this.gridCargos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCargos.EnableHeadersVisualStyles = false;
            this.gridCargos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(227)))), ((int)(((byte)(238)))));
            this.gridCargos.Location = new System.Drawing.Point(12, 134);
            this.gridCargos.Margin = new System.Windows.Forms.Padding(0);
            this.gridCargos.MultiSelect = false;
            this.gridCargos.Name = "gridCargos";
            this.gridCargos.ReadOnly = true;
            this.gridCargos.RowHeadersVisible = false;
            this.gridCargos.RowHeadersWidth = 51;
            this.gridCargos.RowTemplate.Height = 30;
            this.gridCargos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridCargos.Size = new System.Drawing.Size(246, 473);
            this.gridCargos.TabIndex = 4;
            // 
            // CargoId
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CargoId.DefaultCellStyle = dataGridViewCellStyle7;
            this.CargoId.FillWeight = 48F;
            this.CargoId.HeaderText = "ID";
            this.CargoId.MinimumWidth = 44;
            this.CargoId.Name = "CargoId";
            this.CargoId.ReadOnly = true;
            this.CargoId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // NombreCargo
            // 
            this.NombreCargo.FillWeight = 152F;
            this.NombreCargo.HeaderText = "Cargo";
            this.NombreCargo.MinimumWidth = 90;
            this.NombreCargo.Name = "NombreCargo";
            this.NombreCargo.ReadOnly = true;
            this.NombreCargo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // espacioCargos
            // 
            this.espacioCargos.Dock = System.Windows.Forms.DockStyle.Top;
            this.espacioCargos.Location = new System.Drawing.Point(12, 126);
            this.espacioCargos.Name = "espacioCargos";
            this.espacioCargos.Size = new System.Drawing.Size(246, 8);
            this.espacioCargos.TabIndex = 3;
            // 
            // accionesCargo
            // 
            this.accionesCargo.AutoSize = true;
            this.accionesCargo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.accionesCargo.Controls.Add(this.btnCrearCargo);
            this.accionesCargo.Controls.Add(this.btnEliminarCargo);
            this.accionesCargo.Dock = System.Windows.Forms.DockStyle.Top;
            this.accionesCargo.Location = new System.Drawing.Point(12, 82);
            this.accionesCargo.Name = "accionesCargo";
            this.accionesCargo.Padding = new System.Windows.Forms.Padding(6, 6, 0, 0);
            this.accionesCargo.Size = new System.Drawing.Size(246, 44);
            this.accionesCargo.TabIndex = 2;
            // 
            // btnCrearCargo
            // 
            this.btnCrearCargo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnCrearCargo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCrearCargo.FlatAppearance.BorderSize = 0;
            this.btnCrearCargo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnCrearCargo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrearCargo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCrearCargo.ForeColor = System.Drawing.Color.White;
            this.btnCrearCargo.Location = new System.Drawing.Point(6, 9);
            this.btnCrearCargo.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnCrearCargo.Name = "btnCrearCargo";
            this.btnCrearCargo.Size = new System.Drawing.Size(100, 32);
            this.btnCrearCargo.TabIndex = 0;
            this.btnCrearCargo.Text = "Guardar cargo";
            this.btnCrearCargo.UseVisualStyleBackColor = false;
            // 
            // btnEliminarCargo
            // 
            this.btnEliminarCargo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnEliminarCargo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarCargo.FlatAppearance.BorderSize = 0;
            this.btnEliminarCargo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnEliminarCargo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarCargo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEliminarCargo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.btnEliminarCargo.Location = new System.Drawing.Point(114, 9);
            this.btnEliminarCargo.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnEliminarCargo.Name = "btnEliminarCargo";
            this.btnEliminarCargo.Size = new System.Drawing.Size(104, 32);
            this.btnEliminarCargo.TabIndex = 1;
            this.btnEliminarCargo.Text = "Eliminar cargo";
            this.btnEliminarCargo.UseVisualStyleBackColor = false;
            // 
            // camposCargo
            // 
            this.camposCargo.ColumnCount = 1;
            this.camposCargo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.camposCargo.Controls.Add(this.lblTxtCargo, 0, 0);
            this.camposCargo.Controls.Add(this.txtCargo, 0, 1);
            this.camposCargo.Dock = System.Windows.Forms.DockStyle.Top;
            this.camposCargo.Location = new System.Drawing.Point(12, 36);
            this.camposCargo.Margin = new System.Windows.Forms.Padding(0);
            this.camposCargo.Name = "camposCargo";
            this.camposCargo.RowCount = 2;
            this.camposCargo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.camposCargo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.camposCargo.Size = new System.Drawing.Size(246, 46);
            this.camposCargo.TabIndex = 1;
            // 
            // lblTxtCargo
            // 
            this.lblTxtCargo.AutoEllipsis = true;
            this.lblTxtCargo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTxtCargo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxtCargo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtCargo.Location = new System.Drawing.Point(6, 0);
            this.lblTxtCargo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTxtCargo.Name = "lblTxtCargo";
            this.lblTxtCargo.Size = new System.Drawing.Size(234, 17);
            this.lblTxtCargo.TabIndex = 0;
            this.lblTxtCargo.Text = "Nombre del cargo";
            this.lblTxtCargo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCargo
            // 
            this.txtCargo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtCargo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCargo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCargo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.txtCargo.Location = new System.Drawing.Point(6, 20);
            this.txtCargo.Margin = new System.Windows.Forms.Padding(6, 3, 6, 1);
            this.txtCargo.MaxLength = 100;
            this.txtCargo.Name = "txtCargo";
            this.txtCargo.Size = new System.Drawing.Size(234, 27);
            this.txtCargo.TabIndex = 1;
            // 
            // lblTituloCargos
            // 
            this.lblTituloCargos.AutoEllipsis = true;
            this.lblTituloCargos.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloCargos.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTituloCargos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblTituloCargos.Location = new System.Drawing.Point(12, 4);
            this.lblTituloCargos.Margin = new System.Windows.Forms.Padding(0);
            this.lblTituloCargos.Name = "lblTituloCargos";
            this.lblTituloCargos.Size = new System.Drawing.Size(246, 32);
            this.lblTituloCargos.TabIndex = 0;
            this.lblTituloCargos.Text = "Cargos";
            this.lblTituloCargos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmEmpleados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1220, 700);
            this.Controls.Add(this.columnasEmpleados);
            this.Controls.Add(this.encabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.MinimumSize = new System.Drawing.Size(680, 440);
            this.Name = "FrmEmpleados";
            this.Padding = new System.Windows.Forms.Padding(20, 8, 20, 16);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Empleados y cargos";
            ((System.ComponentModel.ISupportInitialize)(this.errores)).EndInit();
            this.encabezado.ResumeLayout(false);
            this.columnasEmpleados.ResumeLayout(false);
            this.panelEmpleados.ResumeLayout(false);
            this.panelEmpleados.PerformLayout();
            this.listadoEmpleados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridEmpleados)).EndInit();
            this.listadoEmpleadosFiltros.ResumeLayout(false);
            this.listadoEmpleadosFiltros.PerformLayout();
            this.editorEmpleado.ResumeLayout(false);
            this.editorEmpleado.PerformLayout();
            this.editorEmpleadoAcciones.ResumeLayout(false);
            this.editorEmpleadoCampos.ResumeLayout(false);
            this.editorEmpleadoCampos.PerformLayout();
            this.panelCargos.ResumeLayout(false);
            this.gestionCargos.ResumeLayout(false);
            this.gestionCargos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCargos)).EndInit();
            this.accionesCargo.ResumeLayout(false);
            this.camposCargo.ResumeLayout(false);
            this.camposCargo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel encabezado;
        private Label lblTitulo;
        private Panel acentoTitulo;
        private TableLayoutPanel columnasEmpleados;
        private Panel panelEmpleados;
        private Panel panelCargos;
        private TextBox txtCedula;
        private TextBox txtPrimerNombre;
        private TextBox txtSegundoNombre;
        private TextBox txtPrimerApellido;
        private TextBox txtSegundoApellido;
        private ComboBox cmbSexo;
        private TextBox txtTelefono;
        private TextBox txtCorreo;
        private TextBox txtDireccion;
        private ComboBox cmbCargo;
        private CheckBox chkActivo;
        private Button btnGuardarEmpleado;
        private Button btnNuevoEmpleado;
        private Button btnEliminarEmpleado;
        private Button btnClaveEmpleado;
        private Panel editorEmpleado;
        private TableLayoutPanel editorEmpleadoCampos;
        private Label lblTxtCedula;
        private Label lblTxtPrimerNombre;
        private Label lblTxtSegundoNombre;
        private Label lblTxtPrimerApellido;
        private Label lblTxtSegundoApellido;
        private Label lblCmbSexo;
        private Label lblTxtTelefono;
        private Label lblTxtCorreo;
        private Label lblTxtDireccion;
        private Label lblCmbCargo;
        private FlowLayoutPanel editorEmpleadoAcciones;
        private Label editorEmpleadoTitulo;
        private Panel espacioEmpleados;
        private TextBox txtBuscar;
        private Panel listadoEmpleados;
        private Label listadoEmpleadosTitulo;
        private TableLayoutPanel listadoEmpleadosFiltros;
        private Label lblTxtBuscar;
        private DataGridView gridEmpleados;
        private Panel gestionCargos;
        private Label lblTituloCargos;
        private TextBox txtCargo;
        private TableLayoutPanel camposCargo;
        private Label lblTxtCargo;
        private Button btnCrearCargo;
        private Button btnEliminarCargo;
        private FlowLayoutPanel accionesCargo;
        private Panel espacioCargos;
        private DataGridView gridCargos;
        private DataGridViewTextBoxColumn Cargo;
        private DataGridViewTextBoxColumn CargoId;
        private DataGridViewTextBoxColumn NombreCargo;
        private DataGridViewTextBoxColumn Cedula;
        private DataGridViewTextBoxColumn PrimerNombre;
        private DataGridViewTextBoxColumn SegundoNombre;
        private DataGridViewTextBoxColumn PrimerApellido;
        private DataGridViewTextBoxColumn SegundoApellido;
        private DataGridViewTextBoxColumn Sexo;
        private DataGridViewTextBoxColumn Telefono;
        private DataGridViewTextBoxColumn CorreoElectronico;
    }
}
