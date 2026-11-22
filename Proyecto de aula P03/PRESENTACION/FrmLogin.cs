using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DATOS;
using ENTIDADES;
using LOGICA;

namespace PRESENTACION
{
    public partial class FrmLogin : Form
    {
        private readonly EmpleadoService empleadoService;
        private readonly ErrorProvider errores;
        private List<string> cedulasRegistradas;   // todas las cédulas que vienen de la lógica
        private ListaSugerencias listaCedulas;     // la lista desplegable que se dibuja bajo el campo
        private bool seleccionando;                // evita reabrir la lista al elegir una cédula

        private const string PlaceholderUsuario = "Ingrese su cédula";
        private const string PlaceholderClave = "Ingrese su contraseña";
        private const int MaxFilasVisibles = 8;    // máximo de cédulas que muestra la lista

        public FrmLogin()
        {
            InitializeComponent();

            empleadoService = new EmpleadoService(
                new EmpleadoRepository(),
                new CargoRepository());

            errores = new ErrorProvider { ContainerControl = this };

            ConfigurarFormulario();
            ConfigurarCampos();
            ConfigurarAutocompletadoCedulas();
            ConfigurarEventos();
            ConfigurarPaneles();

        }

        private void ConfigurarFormulario()
        {
            WindowState = FormWindowState.Maximized;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;
            MinimumSize = new Size(900, 600);
            Text = "GOT-AUTO | Iniciar sesión";
            AcceptButton = button1;
        }

        private void ConfigurarCampos()
        {
            txtUsuario.Text = PlaceholderUsuario;
            txtUsuario.ForeColor = Color.Gray;
            txtPassword.UseSystemPasswordChar = false;
            txtPassword.Text = PlaceholderClave;
            txtPassword.ForeColor = Color.Gray;
            button1.Text = "INICIAR SESIÓN";
            button1.BackColor = Color.FromArgb(37, 99, 235);
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
        }

        // ═════════════ Lista de cédulas ═════════════
        //
        // Cómo funciona, en pocas palabras:
        //   1. Al abrir el login se piden las cédulas a la capa LOGICA (una sola vez).
        //   2. Cuando el usuario entra al campo, o escribe, se llama a ActualizarSugerencias().
        //   3. ActualizarSugerencias() filtra las cédulas y le pide a la lista que las dibuje.

        private void ConfigurarAutocompletadoCedulas()
        {
            try
            {
                cedulasRegistradas = empleadoService.ObtenerCedulasRegistradas();
            }
            catch
            {
                // El formulario puede abrir aunque Oracle no esté disponible.
                cedulasRegistradas = new List<string>();
            }

            // La lista se agrega al formulario (no al panel) para que ningún contenedor la recorte
            listaCedulas = new ListaSugerencias { Font = new Font("Segoe UI", 10.5f) };
            Controls.Add(listaCedulas);
            listaCedulas.BringToFront();

            // Cuando el usuario hace clic en una cédula de la lista, se ejecuta AplicarCedula
            listaCedulas.ItemSeleccionado += AplicarCedula;
        }

        // Paso 2: decide qué mostrar según lo que haya escrito el usuario
        private void ActualizarSugerencias()
        {
            string escrito = TextoEscrito();
            List<string> coincidencias = BuscarCoincidencias(escrito);

            // Si no hay nada que sugerir (o ya escribió la cédula completa), se cierra la lista
            bool yaEscribioLaUnica = coincidencias.Count == 1 && coincidencias[0] == escrito;
            if (coincidencias.Count == 0 || yaEscribioLaUnica)
            {
                OcultarSugerencias();
                return;
            }

            MostrarSugerencias(coincidencias, escrito);
        }

        // Devuelve lo que el usuario escribió; si el campo muestra el texto gris de ejemplo, devuelve vacío
        private string TextoEscrito()
        {
            if (txtUsuario.ForeColor == Color.Gray) return "";
            return txtUsuario.Text.Trim();
        }

        // Paso 3: filtra las cédulas que empiezan con lo escrito.
        // Si no se escribió nada (texto vacío), TODAS empiezan con "", así que se devuelven todas.
        private List<string> BuscarCoincidencias(string escrito)
        {
            List<string> resultado = new List<string>();

            foreach (string cedula in cedulasRegistradas)
            {
                if (cedula.StartsWith(escrito, StringComparison.OrdinalIgnoreCase))
                {
                    resultado.Add(cedula);
                    if (resultado.Count == MaxFilasVisibles) break;
                }
            }

            return resultado;
        }

        private void MostrarSugerencias(List<string> coincidencias, string escrito)
        {
            // La lista va justo debajo del campo de usuario y con su mismo ancho
            Point debajoDelCampo = new Point(0, pnUsuario.Height + 2);
            Point posicion = PointToClient(pnUsuario.PointToScreen(debajoDelCampo));

            listaCedulas.Mostrar(coincidencias, escrito, posicion, pnUsuario.Width);

            // Mientras la lista está abierta, Enter sirve para elegir y no para iniciar sesión
            AcceptButton = null;
        }

        private void OcultarSugerencias()
        {
            if (!listaCedulas.Visible) return;

            listaCedulas.Ocultar();
            AcceptButton = button1;
        }

        // Se ejecuta cuando el usuario elige una cédula de la lista
        private void AplicarCedula(string cedula)
        {
            seleccionando = true;          // así el cambio de texto no vuelve a abrir la lista
            txtUsuario.Text = cedula;
            seleccionando = false;

            OcultarSugerencias();
            txtUsuario.Focus();
            txtUsuario.SelectionStart = txtUsuario.Text.Length;
        }

        // Eventos del campo de usuario que activan la lista
        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            if (seleccionando) return;
            ActualizarSugerencias();
        }

        private void txtUsuario_Click(object sender, EventArgs e)
        {
            // Si el usuario cerró la lista con Esc o eligió una cédula, un clic la vuelve a abrir
            ActualizarSugerencias();
        }

        // Flechas arriba y abajo para recorrer, Enter para elegir, Esc para cerrar
        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (!listaCedulas.Visible) return;

            if (e.KeyCode == Keys.Down)
            {
                listaCedulas.Mover(1);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                listaCedulas.Mover(-1);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                OcultarSugerencias();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                string elegida = listaCedulas.ItemActual;
                e.Handled = true;
                e.SuppressKeyPress = true;

                if (elegida != null)
                {
                    AplicarCedula(elegida);
                }
                else
                {
                    // No eligió ninguna: se cierra la lista y se continúa con el inicio de sesión
                    OcultarSugerencias();
                    button1.PerformClick();
                }
            }
        }

        // Todas las suscripciones se hacen aquí; el Designer no conecta eventos.
        private void ConfigurarEventos()
        {
            txtUsuario.Enter += txtUsuario_Enter;
            txtUsuario.Leave += txtUsuario_Leave;
            txtUsuario.TextChanged += txtUsuario_TextChanged;
            txtUsuario.Click += txtUsuario_Click;
            txtUsuario.KeyDown += txtUsuario_KeyDown;
            txtPassword.Enter += txtPassword_Enter;
            txtPassword.Leave += txtPassword_Leave;
            chckPassword.CheckedChanged += chckPassword_CheckedChanged;
            button1.Click += button1_Click;
            Shown += FrmLogin_Shown;

            // Un clic en cualquier zona vacía cierra la lista
            Click += (s, e) => OcultarSugerencias();
            panelCamposLogin.Click += (s, e) => OcultarSugerencias();
        }

        private void FrmLogin_Shown(object sender, EventArgs e)
        {
            // El Panel permite abrir sin el cursor parpadeando en un TextBox.
            ActiveControl = panelCamposLogin;
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if (txtUsuario.ForeColor == Color.Gray && txtUsuario.Text == PlaceholderUsuario)
            {
                txtUsuario.Clear();
                txtUsuario.ForeColor = Color.FromArgb(31, 41, 55);
            }

            // Al entrar al campo se muestran de inmediato las cédulas
            ActualizarSugerencias();
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            OcultarSugerencias();

            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                txtUsuario.Text = PlaceholderUsuario;
                txtUsuario.ForeColor = Color.Gray;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.ForeColor == Color.Gray && txtPassword.Text == PlaceholderClave)
            {
                txtPassword.Clear();
                txtPassword.ForeColor = Color.FromArgb(31, 41, 55);
                txtPassword.UseSystemPasswordChar = !chckPassword.Checked;
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.Text = PlaceholderClave;
                txtPassword.ForeColor = Color.Gray;
            }
        }

        private void chckPassword_CheckedChanged(object sender, EventArgs e)
        {
            bool esPlaceholder = txtPassword.ForeColor == Color.Gray;
            txtPassword.UseSystemPasswordChar = !esPlaceholder && !chckPassword.Checked;
        }

        private void ConfigurarPaneles()
        {
            RedondearControl(pnUsuario, 10);
            RedondearControl(pnContraseña, 10);
            RedondearControl(button1, 10);
            pnUsuario.Resize += (s, e) => RedondearControl(pnUsuario, 10);
            pnContraseña.Resize += (s, e) => RedondearControl(pnContraseña, 10);
            button1.Resize += (s, e) => RedondearControl(button1, 10);
        }

        private void RedondearControl(Control control, int radio)
        {
            if (control.Width <= 0 || control.Height <= 0) return;

            int diametro = Math.Min(radio * 2, Math.Min(control.Width, control.Height));
            using (GraphicsPath path = new GraphicsPath())
            {
                int ancho = control.Width;
                int alto = control.Height;
                path.AddArc(0, 0, diametro, diametro, 180, 90);
                path.AddArc(ancho - diametro, 0, diametro, diametro, 270, 90);
                path.AddArc(ancho - diametro, alto - diametro, diametro, diametro, 0, 90);
                path.AddArc(0, alto - diametro, diametro, diametro, 90, 90);
                path.CloseFigure();
                Region anterior = control.Region;
                control.Region = new Region(path);
                if (anterior != null) anterior.Dispose();
            }
        }

        private bool ValidarCampos()
        {
            errores.Clear();
            bool usuarioVacio = string.IsNullOrWhiteSpace(txtUsuario.Text) || txtUsuario.ForeColor == Color.Gray;
            bool claveVacia = string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.ForeColor == Color.Gray;

            if (usuarioVacio) errores.SetError(pnUsuario, "Ingrese su cédula.");
            if (claveVacia) errores.SetError(pnContraseña, "Ingrese su contraseña.");
            if (usuarioVacio) txtUsuario.Focus();
            else if (claveVacia) txtPassword.Focus();
            return !usuarioVacio && !claveVacia;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidarCampos()) IniciarSesion();
        }

        private void IniciarSesion()
        {
            string cedula = txtUsuario.Text.Trim();
            string clave = txtPassword.Text;

            try
            {
                button1.Enabled = false;
                Empleado empleado = empleadoService.Autenticar(cedula, clave);

                if (empleado == null || !empleado.Activo)
                {
                    MessageBox.Show(this, "Credenciales incorrectas o cuenta inactiva.", "Acceso denegado",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Clear();
                    txtPassword.ForeColor = Color.FromArgb(31, 41, 55);
                    txtPassword.UseSystemPasswordChar = !chckPassword.Checked;
                    txtPassword.Focus();
                    return;
                }

                AbrirPrincipal(empleado);
            }
            catch (Exception)
            {
                MessageBox.Show(this,
                    "No fue posible validar el acceso. Verifique la conexión con Oracle.",
                    "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button1.Enabled = true;
            }
        }

        private void AbrirPrincipal(Empleado empleado)
        {
            Form1 principal = new Form1(empleado);
            principal.FormClosed += (s, e) =>
            {
                SesionActual.Cerrar();
                txtPassword.Clear();
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.Text = PlaceholderClave;
                txtPassword.ForeColor = Color.Gray;
                Show();
                WindowState = FormWindowState.Maximized;
                Activate();
            };
            Hide();
            principal.Show();
        }

        // ═════════════ Control que dibuja la lista desplegable ═════════════
        //
        // Es un control propio: solo se encarga de DIBUJAR las filas y avisar cuando
        // el usuario hace clic en una. No sabe nada de cédulas, empleados ni de Oracle.

        private class ListaSugerencias : Control
        {
            // ── Medidas (en píxeles) ──
            private const int AltoFlecha = 8;           // altura del triángulo de arriba
            private const int AltoFila = 36;            // altura de cada cédula
            private const int Margen = 6;               // espacio entre el borde y las filas
            private const int RadioBorde = 10;          // qué tan redondas son las esquinas
            private const int PosicionFlecha = 28;      // distancia de la flecha al borde izquierdo

            // ── Colores (cámbialos aquí para ajustar el aspecto) ──

            private readonly Color colorFondo = Color.FromArgb(249, 250, 251);   // fondo de la lista
            private readonly Color colorTexto = Color.FromArgb(31, 41, 55);      // texto normal
            private readonly Color colorBorde = Color.FromArgb(156, 163, 175);
            private readonly Color colorEscrito = Color.FromArgb(37, 99, 235);      // azul: lo que el usuario ya escribió
            private readonly Color colorResaltado = Color.FromArgb(219, 234, 254);  // azul claro: fila bajo el mouse

            private List<string> items = new List<string>();   // las cédulas que se muestran
            private string textoEscrito = "";                   // lo que el usuario lleva escrito
            private int filaConMouse = -1;                      // fila sobre la que está el mouse (-1 = ninguna)

            // Se dispara cuando el usuario hace clic en una cédula
            public event Action<string> ItemSeleccionado;

            // Fila marcada con el teclado (-1 = ninguna)
            public int Indice { get; private set; }

            public string ItemActual
            {
                get { return Indice >= 0 && Indice < items.Count ? items[Indice] : null; }
            }

            public ListaSugerencias()
            {
                SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

                // No toma el foco: así el campo de texto sigue activo mientras se hace clic en la lista
                SetStyle(ControlStyles.Selectable, false);
                TabStop = false;

                Cursor = Cursors.Hand;
                Indice = -1;
                Visible = false;
            }

            // ───────────── Lo que el formulario puede pedirle ─────────────

            public void Mostrar(IEnumerable<string> coincidencias, string escrito, Point ubicacion, int ancho)
            {
                items = new List<string>(coincidencias);
                textoEscrito = escrito ?? "";
                Indice = -1;
                filaConMouse = -1;

                Width = ancho;
                Height = AltoFlecha + Margen * 2 + items.Count * AltoFila;
                Location = ubicacion;

                RecortarForma();
                Visible = true;
                BringToFront();
                Invalidate();
            }

            public void Ocultar()
            {
                Visible = false;
                Indice = -1;
                filaConMouse = -1;
            }

            // Mueve la fila marcada con las flechas del teclado (1 = baja, -1 = sube)
            public void Mover(int paso)
            {
                if (items.Count == 0) return;

                if (Indice == -1)
                    Indice = paso > 0 ? 0 : items.Count - 1;
                else
                    Indice = (Indice + paso + items.Count) % items.Count;

                Invalidate();
            }

            // ───────────── Dibujo ─────────────

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                DibujarFondo(g);

                for (int i = 0; i < items.Count; i++)
                {
                    Rectangle fila = RectanguloDeFila(i);

                    // La fila se resalta si el mouse está encima o si se marcó con el teclado
                    if (i == filaConMouse || i == Indice)
                    {
                        DibujarResaltado(g, fila);
                    }

                    DibujarTexto(g, items[i], fila);
                }
            }

            private Rectangle RectanguloDeFila(int numero)
            {
                int arriba = AltoFlecha + Margen + numero * AltoFila;
                return new Rectangle(Margen, arriba, Width - Margen * 2 - 1, AltoFila);
            }

            // Fondo claro con borde fino y la flecha de arriba
            private void DibujarFondo(Graphics g)
            {
                Rectangle cuerpo = new Rectangle(0, AltoFlecha, Width - 1, Height - AltoFlecha - 1);

                using (SolidBrush pincel = new SolidBrush(colorFondo))
                using (Pen lapiz = new Pen(colorBorde))
                using (GraphicsPath formaCuerpo = RectanguloRedondeado(cuerpo, RadioBorde))
                using (GraphicsPath formaFlecha = FormaDeLaFlecha())
                {
                    g.FillPath(pincel, formaCuerpo);
                    g.DrawPath(lapiz, formaCuerpo);

                    // La flecha se pinta encima: tapa el borde del cuerpo y solo se le dibujan sus dos lados
                    g.FillPath(pincel, formaFlecha);
                    g.DrawLines(lapiz, new Point[]
                    {
                        new Point(PosicionFlecha - AltoFlecha + 1, AltoFlecha),
                        new Point(PosicionFlecha, 1),
                        new Point(PosicionFlecha + AltoFlecha - 1, AltoFlecha)
                    });
                }
            }

            private void DibujarResaltado(Graphics g, Rectangle fila)
            {
                using (SolidBrush pincel = new SolidBrush(colorResaltado))
                using (GraphicsPath forma = RectanguloRedondeado(fila, 6))
                {
                    g.FillPath(pincel, forma);
                }
            }

            // Lo que el usuario ya escribió va en azul y negrita; el resto, en gris oscuro
            private void DibujarTexto(Graphics g, string texto, Rectangle fila)
            {
                TextFormatFlags formato = TextFormatFlags.NoPadding | TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                int izquierda = fila.X + 14;
                int anchoDisponible = fila.Width - 28;

                string parteEscrita = texto.Substring(0, Math.Min(textoEscrito.Length, texto.Length));
                string parteRestante = texto.Substring(parteEscrita.Length);

                int anchoEscrito = 0;
                if (parteEscrita.Length > 0)
                {
                    using (Font negrita = new Font(Font, FontStyle.Bold))
                    {
                        Size tamano = TextRenderer.MeasureText(g, parteEscrita, negrita,
                            new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding);
                        anchoEscrito = tamano.Width;

                        Rectangle zona = new Rectangle(izquierda, fila.Y, anchoDisponible, fila.Height);
                        TextRenderer.DrawText(g, parteEscrita, negrita, zona, colorEscrito, formato);
                    }
                }

                Rectangle zonaResto = new Rectangle(izquierda + anchoEscrito, fila.Y, anchoDisponible - anchoEscrito, fila.Height);
                TextRenderer.DrawText(g, parteRestante, Font, zonaResto, colorTexto, formato);
            }

            // ───────────── Formas ─────────────

            // Recorta el control con la forma redondeada y la flecha, para que las esquinas queden transparentes
            private void RecortarForma()
            {
                Rectangle cuerpo = new Rectangle(0, AltoFlecha, Width, Height - AltoFlecha);

                using (GraphicsPath formaCuerpo = RectanguloRedondeado(cuerpo, RadioBorde))
                using (GraphicsPath formaFlecha = FormaDeLaFlecha())
                {
                    Region nueva = new Region(formaCuerpo);
                    nueva.Union(formaFlecha);

                    Region anterior = Region;
                    Region = nueva;
                    if (anterior != null) anterior.Dispose();
                }
            }

            // Triángulo con la punta hacia arriba
            private GraphicsPath FormaDeLaFlecha()
            {
                GraphicsPath forma = new GraphicsPath();
                forma.AddPolygon(new Point[]
                {
                    new Point(PosicionFlecha, 1),                              // punta
                    new Point(PosicionFlecha + AltoFlecha, AltoFlecha + 1),    // esquina derecha de la base
                    new Point(PosicionFlecha - AltoFlecha, AltoFlecha + 1)     // esquina izquierda de la base
                });
                return forma;
            }

            private static GraphicsPath RectanguloRedondeado(Rectangle r, int radio)
            {
                int d = radio * 2;
                GraphicsPath forma = new GraphicsPath();
                forma.AddArc(r.X, r.Y, d, d, 180, 90);
                forma.AddArc(r.Right - d, r.Y, d, d, 270, 90);
                forma.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
                forma.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
                forma.CloseFigure();
                return forma;
            }

            // ───────────── Mouse ─────────────

            // Devuelve el número de fila que hay bajo un punto, o -1 si no hay ninguna
            private int FilaEn(Point punto)
            {
                int y = punto.Y - AltoFlecha - Margen;
                if (y < 0) return -1;

                int fila = y / AltoFila;
                return fila < items.Count ? fila : -1;
            }

            protected override void OnMouseMove(MouseEventArgs e)
            {
                int fila = FilaEn(e.Location);
                if (fila != filaConMouse)
                {
                    filaConMouse = fila;
                    Invalidate();
                }
                base.OnMouseMove(e);
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                filaConMouse = -1;
                Invalidate();
                base.OnMouseLeave(e);
            }

            protected override void OnMouseDown(MouseEventArgs e)
            {
                int fila = FilaEn(e.Location);
                if (fila >= 0 && ItemSeleccionado != null)
                {
                    ItemSeleccionado(items[fila]);
                }
                base.OnMouseDown(e);
            }
        }

        private sealed class DialogoConfigurarClave : Form
        {
            private readonly TextBox txtClave = new TextBox { UseSystemPasswordChar = true, Width = 250 };
            private readonly TextBox txtConfirmar = new TextBox { UseSystemPasswordChar = true, Width = 250 };
            public string ClaveNueva { get { return txtClave.Text; } }

            public DialogoConfigurarClave()
            {
                Text = "Crear contraseña"; StartPosition = FormStartPosition.CenterParent; FormBorderStyle = FormBorderStyle.FixedDialog;
                MinimizeBox = false; MaximizeBox = false; ClientSize = new System.Drawing.Size(340, 190);
                var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(14), ColumnCount = 1, RowCount = 5 };
                layout.Controls.Add(new Label { Text = "Nueva contraseña (mínimo 8 caracteres):", AutoSize = true }, 0, 0);
                layout.Controls.Add(txtClave, 0, 1);
                layout.Controls.Add(new Label { Text = "Confirmar contraseña:", AutoSize = true }, 0, 2);
                layout.Controls.Add(txtConfirmar, 0, 3);
                var botones = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft };
                var aceptar = new Button { Text = "Guardar", DialogResult = DialogResult.None, Width = 90 };
                var cancelar = new Button { Text = "Cancelar", DialogResult = DialogResult.Cancel, Width = 90 };
                aceptar.Click += (s, e) =>
                {
                    if (txtClave.Text.Length < 8 || txtClave.Text.Length > 100 || txtClave.Text != txtConfirmar.Text)
                    {
                        MessageBox.Show(this, "Las contraseñas deben coincidir y tener entre 8 y 100 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    DialogResult = DialogResult.OK;
                };
                botones.Controls.Add(aceptar); botones.Controls.Add(cancelar); layout.Controls.Add(botones, 0, 4);
                Controls.Add(layout); AcceptButton = aceptar; CancelButton = cancelar;
            }
        }
    }
}