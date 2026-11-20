using DATOS;
using ENTIDADES;
using LOGICA;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PRESENTACION
{
    public partial class FrmProveedores : Form
    {
        private readonly ProveedorService servicio;
        private readonly Validacion validacion = new Validacion();
        private List<Proveedor> proveedores = new List<Proveedor>();
        private bool editando;

        public FrmProveedores()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            servicio = new ProveedorService(new ProveedorRepository());
            ConfigurarInteraccion();
            Load += (s, e) => CargarProveedores();
        }

        private void ConfigurarInteraccion()
        {
            btnGuardar.Click += Guardar_Click;
            btnNuevo.Click += (s, e) => LimpiarFormulario();
            btnEliminar.Click += Eliminar_Click;
            txtBuscar.TextChanged += (s, e) => AplicarFiltro();
            grid.CellClick += (s, e) => Grid_SelectionChanged(s, EventArgs.Empty);
            EstiloUI.ConectarTabla(grid);
        }

        private void CargarProveedores()
        {
            try
            {
                proveedores = servicio.Consultar();
                AplicarFiltro();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "No se pudieron consultar los proveedores. " + ex.Message,
                    "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AplicarFiltro()
        {
            if (grid == null) return;
            string filtro = txtBuscar == null ? string.Empty : txtBuscar.Text.Trim();
            IEnumerable<Proveedor> lista = proveedores;
            if (filtro.Length > 0)
                lista = proveedores.Where(p => p.Nit.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    p.RazonSocial.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0);
            grid.DataSource = lista.ToList();
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            bool valido = !validacion.ValidarCampoVacioEspecifico(txtNit, errores) &
                          !validacion.ValidarCampoVacioEspecifico(txtRazonSocial, errores);
            if (!valido)
            {
                MessageBox.Show(this, "Completa el NIT y la razón social.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string resultado = editando
                ? servicio.Modificar(txtNit.Text.Trim(), txtRazonSocial.Text.Trim(), TextoOpcional(txtTelefono))
                : servicio.Guardar(txtNit.Text.Trim(), txtRazonSocial.Text.Trim(), TextoOpcional(txtTelefono));
            bool correcto = resultado.StartsWith("Se registró el proveedor", StringComparison.OrdinalIgnoreCase) ||
                            resultado.StartsWith("El proveedor se modificó", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, resultado, correcto ? "Proveedores" : "No se pudo guardar",
                MessageBoxButtons.OK, correcto ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (correcto)
            {
                CargarProveedores();
                LimpiarFormulario();
            }
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0) return;
            Proveedor proveedor = grid.SelectedRows[0].DataBoundItem as Proveedor;
            if (proveedor == null) return;
            if (MessageBox.Show(this, "¿Eliminar el proveedor seleccionado? Si está asociado a repuestos, Oracle impedirá eliminarlo.",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            string resultado = servicio.Eliminar(proveedor.Nit);
            bool correcto = resultado.StartsWith("El proveedor se eliminó", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, resultado, correcto ? "Proveedores" : "No se pudo eliminar",
                MessageBoxButtons.OK, correcto ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (correcto)
            {
                CargarProveedores();
                LimpiarFormulario();
            }
        }

        private void Grid_SelectionChanged(object sender, EventArgs e)
        {
            Proveedor proveedor = grid.SelectedRows.Count == 0
                ? null
                : grid.SelectedRows[0].DataBoundItem as Proveedor;
            if (proveedor == null) return;

            editando = true;
            txtNit.Text = proveedor.Nit;
            txtNit.Enabled = false;
            txtRazonSocial.Text = proveedor.RazonSocial;
            txtTelefono.Text = proveedor.Telefono;
            btnGuardar.Text = "Actualizar";
        }

        private void LimpiarFormulario()
        {
            editando = false;
            txtNit.Enabled = true;
            txtNit.Clear();
            txtRazonSocial.Clear();
            txtTelefono.Clear();
            btnGuardar.Text = "Guardar";
            errores.SetError(txtNit, string.Empty);
            errores.SetError(txtRazonSocial, string.Empty);
            grid.ClearSelection();
            txtNit.Focus();
        }

        private static string TextoOpcional(TextBox texto)
        {
            return string.IsNullOrWhiteSpace(texto.Text) ? null : texto.Text.Trim();
        }

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}