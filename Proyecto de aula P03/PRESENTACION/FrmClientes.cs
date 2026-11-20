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
    public partial class FrmClientes : Form
    {
        private readonly ClienteService servicio;
        private readonly Validacion validacion = new Validacion();
        private List<Cliente> clientes = new List<Cliente>();
        private bool editando;

        public FrmClientes()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            servicio = new ClienteService(new ClienteRepository());
            ConfigurarInteraccion();
            Load += (s, e) => CargarClientes();
        }

        private void ConfigurarInteraccion()
        {
            btnGuardar.Click += Guardar_Click;
            btnNuevo.Click += (s, e) => LimpiarFormulario();
            btnEliminar.Click += Eliminar_Click;
            txtBuscar.TextChanged += (s, e) => AplicarBusqueda();
            grid.CellClick += (s, e) => Grid_SelectionChanged(s, EventArgs.Empty);
            EstiloUI.ConectarTabla(grid);
        }

        private bool ValidarFormulario()
        {
            bool valido = true;
            // Los métodos de Validacion devuelven true cuando encuentran un campo inválido.
            valido &= !validacion.ValidarCampoVacioEspecifico(txtCedula, errores);
            valido &= !validacion.ValidarCampoVacioEspecifico(txtPrimerNombre, errores);
            valido &= !validacion.ValidarCampoVacioEspecifico(txtPrimerApellido, errores);
            valido &= !validacion.ValidarCampoVacioEspecificoCombo(cmbSexo, errores);
            if (!valido) MessageBox.Show(this, "Completa los campos obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return valido;
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;
            string resultado = editando
                ? servicio.Modificar(txtCedula.Text.Trim(), txtPrimerNombre.Text.Trim(), TextoOpcional(txtSegundoNombre), txtPrimerApellido.Text.Trim(), TextoOpcional(txtSegundoApellido), cmbSexo.Text,
                    TextoOpcional(txtTelefono), TextoOpcional(txtDireccion), TextoOpcional(txtCorreo))
                : servicio.Guardar(txtCedula.Text.Trim(), txtPrimerNombre.Text.Trim(), TextoOpcional(txtSegundoNombre), txtPrimerApellido.Text.Trim(), TextoOpcional(txtSegundoApellido), cmbSexo.Text,
                    TextoOpcional(txtTelefono), TextoOpcional(txtDireccion), TextoOpcional(txtCorreo));

            bool guardado = resultado.StartsWith("se registro el cliente", StringComparison.OrdinalIgnoreCase) ||
                            resultado.StartsWith("El cliente se modifico", StringComparison.OrdinalIgnoreCase) ||
                            resultado.StartsWith("El cliente se registro correctamente", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, resultado, guardado && resultado.IndexOf("no se pudo enviar el correo", StringComparison.OrdinalIgnoreCase) < 0
                ? "Clientes" : (guardado ? "Aviso" : "No se pudo guardar"), MessageBoxButtons.OK,
                guardado ? (resultado.IndexOf("no se pudo enviar el correo", StringComparison.OrdinalIgnoreCase) >= 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information) : MessageBoxIcon.Error);
            if (guardado) { CargarClientes(); LimpiarFormulario(); }
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0) return;
            Cliente cliente = grid.SelectedRows[0].DataBoundItem as Cliente;
            if (cliente == null) return;
            if (MessageBox.Show(this, "¿Eliminar el cliente seleccionado?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            string resultado = servicio.Eliminar(cliente.Cedula);
            bool eliminado = resultado.StartsWith("El cliente se elimino", StringComparison.OrdinalIgnoreCase);
            MessageBox.Show(this, resultado, eliminado ? "Clientes" : "No se pudo eliminar", MessageBoxButtons.OK,
                eliminado ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (eliminado) { CargarClientes(); LimpiarFormulario(); }
        }

        private void CargarClientes()
        {
            try
            {
                clientes = servicio.Consultar();
                AplicarBusqueda();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "No se pudieron cargar los clientes. " + ex.Message, "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AplicarBusqueda()
        {
            string filtro = txtBuscar == null ? string.Empty : txtBuscar.Text.Trim();
            IEnumerable<Cliente> resultado = clientes;
            if (filtro.Length > 0)
                resultado = clientes.Where(c => c.Cedula.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    c.NombreCompleto.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (c.PrimerNombre ?? "").IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (c.SegundoNombre ?? "").IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (c.PrimerApellido ?? "").IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (c.SegundoApellido ?? "").IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0);
            grid.DataSource = resultado.ToList();
        }

        private void Grid_SelectionChanged(object sender, EventArgs e)
        {
            Cliente cliente = grid.SelectedRows.Count == 0 ? null : grid.SelectedRows[0].DataBoundItem as Cliente;
            if (cliente == null) return;
            editando = true;
            txtCedula.Text = cliente.Cedula;
            txtCedula.Enabled = false;
            txtPrimerNombre.Text = cliente.PrimerNombre;
            txtSegundoNombre.Text = cliente.SegundoNombre;
            txtPrimerApellido.Text = cliente.PrimerApellido;
            txtSegundoApellido.Text = cliente.SegundoApellido;
            cmbSexo.SelectedItem = cliente.Sexo;
            txtTelefono.Text = cliente.Telefono;
            txtCorreo.Text = cliente.CorreoElectronico;
            txtDireccion.Text = cliente.Direccion;
            btnGuardar.Text = "Actualizar";
        }

        private void LimpiarFormulario()
        {
            editando = false;
            txtCedula.Enabled = true;
            txtCedula.Clear(); txtPrimerNombre.Clear(); txtSegundoNombre.Clear(); txtPrimerApellido.Clear(); txtSegundoApellido.Clear(); txtTelefono.Clear(); txtCorreo.Clear(); txtDireccion.Clear();
            cmbSexo.SelectedIndex = -1;
            btnGuardar.Text = "Guardar";
            foreach (Control control in new Control[] { txtCedula, txtPrimerNombre, txtSegundoNombre, txtPrimerApellido, txtSegundoApellido, cmbSexo }) errores.SetError(control, string.Empty);
            grid.ClearSelection();
            txtCedula.Focus();
        }

        private static string TextoOpcional(TextBox control)
        {
            return string.IsNullOrWhiteSpace(control.Text) ? null : control.Text.Trim();
        }
    }
}
