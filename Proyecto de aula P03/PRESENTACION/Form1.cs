
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ENTIDADES;
using LOGICA;

namespace PRESENTACION
{
    public partial class Form1 : Form
    {
        FrmPanel frmPanel;

        // AGREGADO: botones de los modulos faltantes
        private Button btnArticulosMenu;
        private Button btnFacturacionMenu;
        private Button btnEmpleadosMenu;
        private Button btnProveedoresMenu;
        private Button btnMantenimientoMenu;
        private Button btnCerrarSesionMenu;
        private Label labelUsuario;
        private Panel separadorCierre;
        private bool cambiandoVista;
        private bool cerrandoForm;
        private readonly Empleado empleadoAutenticado;
        public event EventHandler SolicitarCierreSesion;
        public Empleado UsuarioActual { get { return empleadoAutenticado; } }

        public Form1()
            : this(null)
        {
        }

        public Form1(Empleado empleado)
        {
            InitializeComponent();
            empleadoAutenticado = empleado;
            PrepararMenuLateral();
            CrearBotonesAdicionales();
            MostrarEmpleadoAutenticado();
            Load += (s, e) => MostrarPanelDashboard();
            FormClosing += (s, e) => cerrandoForm = true;
        }

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern bool RedrawWindow(IntPtr hWnd, IntPtr rectangulo, IntPtr region, uint opciones);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int WM_SETREDRAW = 0x000B;
        private const int HT_CAPTION = 0x2;
        private const uint RDW_INVALIDATE = 0x0001;
        private const uint RDW_ERASE = 0x0004;
        private const uint RDW_ALLCHILDREN = 0x0080;
        private const uint RDW_FRAME = 0x0400;

        Color colorNormal = Color.FromArgb(23, 24, 29);
        Color colorActivo = Color.FromArgb(85, 85, 85);

        private void Menu_Click(object sender, EventArgs e)
        {
            PintarBotonActivo(btnAgenda);
            AbrirFormularioHijo(new FrmAgenda());
        }

        bool sidebarExpand = true;

        private void sidebarTransition_Tick(object sender, EventArgs e)
        {
            sidebar.SuspendLayout();

            if (sidebarExpand)
            {
                sidebar.Width = Math.Max(57, sidebar.Width - 10);

                PnPanel.Width = sidebar.Width;
                menuContainer.Width = sidebar.Width;
                pnServicio.Width = sidebar.Width;
                pnVehiculos.Width = sidebar.Width;
                pnClientes.Width = sidebar.Width;
                pnConfiguracion.Width = sidebar.Width;

                // AGREGADO
                if (btnArticulosMenu != null)
                {
                    btnArticulosMenu.Parent.Width = sidebar.Width;
                    btnFacturacionMenu.Parent.Width = sidebar.Width;
                    btnEmpleadosMenu.Parent.Width = sidebar.Width;
                    btnProveedoresMenu.Parent.Width = sidebar.Width;
                    btnMantenimientoMenu.Parent.Width = sidebar.Width;
                    btnCerrarSesionMenu.Parent.Width = sidebar.Width;
                }

                if (sidebar.Width < 150)
                {
                    button2.Text = "";
                    btnAgenda.Text = "";
                    btnServicio.Text = "";
                    btnVehiculos.Text = "";
                    btnClientes.Text = "";
                    btnConfiguracion.Text = "";

                    // AGREGADO
                    btnArticulosMenu.Text = "";
                    btnFacturacionMenu.Text = "";
                    btnEmpleadosMenu.Text = "";
                    btnProveedoresMenu.Text = "";
                    btnMantenimientoMenu.Text = "";
                    btnCerrarSesionMenu.Text = "";
                }

                if (sidebar.Width <= 57)
                {
                    sidebarExpand = false;
                    sidebarTransition.Stop();

                }
            }

            else
            {
                sidebar.Width = Math.Min(251, sidebar.Width + 10);

                PnPanel.Width = sidebar.Width;
                menuContainer.Width = sidebar.Width;
                pnServicio.Width = sidebar.Width;
                pnVehiculos.Width = sidebar.Width;
                pnClientes.Width = sidebar.Width;
                pnConfiguracion.Width = sidebar.Width;

                // AGREGADO
                if (btnArticulosMenu != null)
                {
                    btnArticulosMenu.Parent.Width = sidebar.Width;
                    btnFacturacionMenu.Parent.Width = sidebar.Width;
                    btnEmpleadosMenu.Parent.Width = sidebar.Width;
                    btnProveedoresMenu.Parent.Width = sidebar.Width;
                    btnMantenimientoMenu.Parent.Width = sidebar.Width;
                    btnCerrarSesionMenu.Parent.Width = sidebar.Width;
                }

                if (sidebar.Width >= 251)
                {
                    RellenarTexto();

                    sidebarExpand = true;
                    sidebarTransition.Stop();
                }
            }

            AjustarAnchoMenu(sidebar.Width);
            sidebar.ResumeLayout();
        }

        private void RellenarTexto()
        {
            button2.Text = "             Panel";
            btnAgenda.Text = "            Agenda";
            btnServicio.Text = "           Facturación y Cotiz.";
            btnVehiculos.Text = "            Vehiculos";
            btnClientes.Text = "           Clientes";

            // AGREGADO
            btnArticulosMenu.Text = "           Inventario";
            btnEmpleadosMenu.Text = "           Empleados y cargos";
            btnProveedoresMenu.Text = "           Proveedores";
            btnMantenimientoMenu.Text = "           Mantenimiento";
            btnCerrarSesionMenu.Text = "           Cerrar sesión";
        }

        private void btnHam_Click(object sender, EventArgs e)
        {
            sidebarTransition.Start();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {

                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.WindowState = FormWindowState.Maximized;

            }
            else
            {
                this.WindowState = FormWindowState.Normal;

            }
        }

        private void pnBarraSuperior_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();

                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pnBarraSuperior_MouseUp(object sender, MouseEventArgs e)
        {
            pnBarraSuperior.Cursor = Cursors.Default;
        }


        private void PintarBotonActivo(Button activo)
        {
            button2.BackColor = colorNormal;
            btnAgenda.BackColor = colorNormal;
            btnServicio.BackColor = colorNormal;
            btnVehiculos.BackColor = colorNormal;
            btnClientes.BackColor = colorNormal;
            btnConfiguracion.BackColor = colorNormal;

            // AGREGADO
            btnArticulosMenu.BackColor = colorNormal;
            btnFacturacionMenu.BackColor = colorNormal;
            btnEmpleadosMenu.BackColor = colorNormal;
            btnProveedoresMenu.BackColor = colorNormal;
            btnMantenimientoMenu.BackColor = colorNormal;
            btnCerrarSesionMenu.BackColor = colorNormal;

            activo.BackColor = colorActivo;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PintarBotonActivo(button2);
            MostrarPanelDashboard();
        }

        private void btnServicio_Click(object sender, EventArgs e)
        {
            PintarBotonActivo(btnServicio);
            AbrirFormularioHijo(new FrmFacturacion());
        }

        private void btnVehiculos_Click(object sender, EventArgs e)
        {
            PintarBotonActivo(btnVehiculos);

            // AGREGADO
            AbrirFormularioHijo(new FrmVehiculos());
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            PintarBotonActivo(btnClientes);

            // AGREGADO
            AbrirFormularioHijo(new FrmClientes());
        }

        // ==========================================
        // AGREGADO: CREAR LOS BOTONES FALTANTES
        // ==========================================

        private void CrearBotonesAdicionales()
        {
            btnFacturacionMenu = btnServicio;
            btnArticulosMenu = CrearBotonLateral(
                "btnArticulosMenu",
                "           Inventario",
                btnArticulosMenu_Click);

            btnEmpleadosMenu = CrearBotonLateral(
                "btnEmpleadosMenu",
                "           Empleados y cargos",
                btnEmpleadosMenu_Click);

            btnProveedoresMenu = CrearBotonLateral("btnProveedoresMenu", "           Proveedores", btnProveedoresMenu_Click);
            btnMantenimientoMenu = CrearBotonLateral("btnMantenimientoMenu", "           Mantenimiento", btnMantenimientoMenu_Click);
            btnCerrarSesionMenu = CrearBotonLateral("btnCerrarSesionMenu", "           Cerrar sesión", btnCerrarSesionMenu_Click);
            sidebar.Controls.SetChildIndex(btnCerrarSesionMenu.Parent, sidebar.Controls.Count - 1);
            separadorCierre = new Panel { Width = 251, Height = 10, Margin = Padding.Empty };
            sidebar.Controls.Add(separadorCierre);
            sidebar.Controls.SetChildIndex(separadorCierre, sidebar.Controls.Count - 2);
            btnCerrarSesionMenu.ForeColor = Color.FromArgb(225, 190, 190);
            sidebar.SizeChanged += (s, e) => AjustarAnchoMenu(sidebar.Width);
            UniformarBotones();
            AjustarAnchoMenu(sidebar.Width);
        }

        private void UniformarBotones()
        {
            Button[] botones = { button2, btnAgenda, btnServicio, btnVehiculos, btnClientes, btnArticulosMenu, btnEmpleadosMenu, btnProveedoresMenu, btnMantenimientoMenu, btnCerrarSesionMenu };
            string[] textos = { "Panel", "Agenda", "Facturación y Cotiz.", "Vehículos", "Clientes", "Inventario", "Empleados y cargos", "Proveedores", "Mantenimiento", "Cerrar sesión" };
            string[] iconos = { "\uE80F", "\uE787", "\uE8A5", "\uE804", "\uE77B", "\uE7B8", "\uE716", "\uE821", "\uE90F", "\uE8AC" };
            for (int i = 0; i < botones.Length; i++)
            {
                Button boton = botones[i];
                boton.Text = textos[i]; boton.Tag = textos[i];
                boton.Padding = new Padding(12, 0, 8, 0);
                boton.ImageAlign = ContentAlignment.MiddleLeft; boton.TextAlign = ContentAlignment.MiddleLeft;
                boton.TextImageRelation = TextImageRelation.ImageBeforeText;
                var icono = new Bitmap(32, 26);
                using (Graphics grafico = Graphics.FromImage(icono))
                using (var fuente = new Font("Segoe MDL2 Assets", 15F))
                    TextRenderer.DrawText(grafico, iconos[i], fuente, new Rectangle(0, 0, 26, 26), Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                boton.Image = icono;
                boton.Disposed += (s, e) => icono.Dispose();
            }
        }

        private void PrepararMenuLateral()
        {
            sidebar.WrapContents = false;
            sidebar.AutoScroll = true;
            menuContainer.Height = 53;
            menuContainer.Margin = new Padding(0);
            menuContainer.Padding = Padding.Empty;
            btnConfiguracion.Visible = false;
            pnConfiguracion.Visible = false;
            btnServicio.Text = "           Facturación y Cotiz.";
            AjustarAnchoMenu(251);
        }

        private void AjustarAnchoMenu(int ancho)
        {
            sidebar.SuspendLayout();
            foreach (Control contenedor in sidebar.Controls)
            {
                contenedor.Width = ancho;
                contenedor.Margin = Padding.Empty;
                if (contenedor == menuContainer)
                {
                    menuContainer.Width = ancho;
                    pnAgenda.Location = Point.Empty;
                    pnAgenda.Margin = Padding.Empty;
                    pnAgenda.Width = ancho;
                    btnAgenda.Location = Point.Empty;
                    btnAgenda.Size = new Size(ancho, 53);
                }
                foreach (Control control in contenedor.Controls)
                {
                    Button boton = control as Button;
                    if (boton == null) continue;
                    boton.Location = Point.Empty;
                    boton.Size = new Size(ancho, 53);
                    if (boton.Tag is string) boton.Text = ancho < 150 ? "" : (string)boton.Tag;
                }
            }
            if (btnAgenda.Tag is string) btnAgenda.Text = ancho < 150 ? "" : (string)btnAgenda.Tag;
            if (separadorCierre != null)
                separadorCierre.Height = Math.Max(10, sidebar.ClientSize.Height - 10 * 53);
            sidebar.ResumeLayout();
        }

        private Button CrearBotonLateral(
            string nombre,
            string texto,
            EventHandler eventoClick)
        {
            Panel panel = new Panel();

            panel.Name = "pn" + nombre;
            panel.Size = new Size(251, 53);
            panel.Margin = new Padding(0);
            panel.Cursor = Cursors.Hand;

            Button boton = new Button();

            boton.Name = nombre;
            boton.Text = texto;
            boton.BackColor = colorNormal;
            boton.ForeColor = Color.White;
            boton.Font = btnClientes.Font;
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(40, 40, 40);

            boton.TextAlign = ContentAlignment.MiddleLeft;
            boton.Size = new Size(251, 53);
            boton.Location = Point.Empty;
            boton.Padding = new Padding(25, 0, 0, 0);
            boton.Cursor = Cursors.Hand;
            boton.UseVisualStyleBackColor = false;

            boton.Click += eventoClick;

            panel.Controls.Add(boton);
            sidebar.Controls.Add(panel);

            // Colocar antes de Configuracion
            int indiceConfiguracion = sidebar.Controls.GetChildIndex(pnConfiguracion);
            if (indiceConfiguracion >= 0) sidebar.Controls.SetChildIndex(panel, indiceConfiguracion);

            return boton;
        }

        // ==========================================
        // AGREGADO: ABRIR FORMULARIOS HIJOS
        // ==========================================

        private void AbrirFormularioHijo(Form formulario)
        {
            MdiClient areaContenido = Controls.OfType<MdiClient>().Single();
            IntPtr ventana = areaContenido.IsHandleCreated && areaContenido.Visible
                ? areaContenido.Handle
                : IntPtr.Zero;

            cambiandoVista = true;
            try
            {
                // Mantener el último dibujo mientras MDI cierra una vista y ajusta la siguiente.
                if (ventana != IntPtr.Zero) SendMessage(ventana, WM_SETREDRAW, 0, 0);

                foreach (Form hijo in MdiChildren.ToArray()) hijo.Close();
                frmPanel = formulario as FrmPanel;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.StartPosition = FormStartPosition.Manual;
                formulario.MdiParent = this;
                formulario.Bounds = areaContenido.ClientRectangle;
                formulario.Dock = DockStyle.Fill;
                formulario.FormClosed += Hijo_FormClosed;
                formulario.Show();
                areaContenido.PerformLayout();
                if (frmPanel != null) ActualizarDashboard();
            }
            finally
            {
                cambiandoVista = false;
                if (ventana != IntPtr.Zero && areaContenido.IsHandleCreated && areaContenido.Handle == ventana)
                {
                    SendMessage(ventana, WM_SETREDRAW, 1, 0);
                    RedrawWindow(ventana, IntPtr.Zero, IntPtr.Zero, RDW_INVALIDATE | RDW_ERASE | RDW_FRAME | RDW_ALLCHILDREN);
                }
            }
        }

        private void Hijo_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ReferenceEquals(sender, frmPanel)) frmPanel = null;
            if (!cambiandoVista && !cerrandoForm && !IsDisposed) MostrarPanelDashboard();
        }

        private void MostrarPanelDashboard()
        {
            if (frmPanel != null && !frmPanel.IsDisposed)
            {
                frmPanel.Activate();
                ActualizarDashboard();
                return;
            }
            PintarBotonActivo(button2);
            var panel = new FrmPanel();
            panel.SolicitarActualizacion += ActualizarDashboard;
            panel.AbrirSeccion += NavegarDesdeDashboard;
            panel.AbrirCita += id =>
            {
                PintarBotonActivo(btnAgenda);
                AbrirFormularioHijo(new FrmAgenda(id));
            };
            AbrirFormularioHijo(panel);
        }

        private void NavegarDesdeDashboard(FrmPanel.Seccion destino)
        {
            switch (destino)
            {
                case FrmPanel.Seccion.Clientes: btnClientes_Click(this, EventArgs.Empty); break;
                case FrmPanel.Seccion.Vehiculos: btnVehiculos_Click(this, EventArgs.Empty); break;
                case FrmPanel.Seccion.Articulos: btnArticulosMenu_Click(this, EventArgs.Empty); break;
                case FrmPanel.Seccion.Agenda: Menu_Click(this, EventArgs.Empty); break;
                case FrmPanel.Seccion.Ventas: btnServicio_Click(this, EventArgs.Empty); break;
            }
        }

        public async void ActualizarDashboard()
        {
            FrmPanel panel = frmPanel;
            if (panel == null || panel.IsDisposed || panel.Actualizando) return;

            panel.EstablecerCarga(true);
            try
            {
                var datos = await Task.Run(() => new DashboardService().Cargar());
                if (!panel.IsDisposed) panel.MostrarResumen(datos);
            }
            catch (Exception ex)
            {
                if (!panel.IsDisposed) panel.MostrarError(ex.Message);
            }
            finally
            {
                if (!panel.IsDisposed) panel.EstablecerCarga(false);
            }
        }

        private void MostrarEmpleadoAutenticado()
        {
            string nombre = empleadoAutenticado == null ? "Sesión" : (empleadoAutenticado.Nombre + " " + empleadoAutenticado.Apellido).Trim();
            string cargo = empleadoAutenticado == null || empleadoAutenticado.Cargo == null ? "" : " · " + empleadoAutenticado.Cargo.NombreCargo;
            labelUsuario = new Label { Text = nombre + cargo, AutoSize = false, AutoEllipsis = true, TextAlign = ContentAlignment.MiddleRight, Size = new Size(340, 32), Anchor = AnchorStyles.Top | AnchorStyles.Right, ForeColor = Color.FromArgb(23, 24, 29), Font = new Font("Segoe UI", 9F, FontStyle.Bold), Location = new Point(pnBarraSuperior.ClientSize.Width - 520, 10) };
            pnBarraSuperior.Controls.Add(labelUsuario);
            labelUsuario.BringToFront();
        }

        private void btnProveedoresMenu_Click(object sender, EventArgs e)
        {
            PintarBotonActivo(btnProveedoresMenu);
            AbrirFormularioHijo(new FrmProveedores());
        }

        private void btnMantenimientoMenu_Click(object sender, EventArgs e)
        {
            PintarBotonActivo(btnMantenimientoMenu);
            AbrirFormularioHijo(new FrmMantenimiento());
        }

        private void btnCerrarSesionMenu_Click(object sender, EventArgs e)
        {
            DialogResult confirmar = MessageBox.Show(this, "¿Deseas cerrar la sesión actual?", "Cerrar sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar != DialogResult.Yes) return;
            EventHandler handler = SolicitarCierreSesion;
            if (handler != null) handler(this, EventArgs.Empty);
            else Close();
        }

        // ==========================================
        // AGREGADO: EVENTOS DE INVENTARIO Y FACTURACION
        // ==========================================

        private void btnArticulosMenu_Click(object sender, EventArgs e)
        {
            PintarBotonActivo(btnArticulosMenu);

            AbrirFormularioHijo(new FrmArticulos());
        }

        private void btnFacturacionMenu_Click(object sender, EventArgs e)
        {
            PintarBotonActivo(btnFacturacionMenu);

            AbrirFormularioHijo(new FrmFacturacion());
        }

        private void btnEmpleadosMenu_Click(object sender, EventArgs e)
        {
            PintarBotonActivo(btnEmpleadosMenu);
            AbrirFormularioHijo(new FrmEmpleados());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
