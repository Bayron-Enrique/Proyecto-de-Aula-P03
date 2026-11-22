
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRESENTACION
{
    public class Validacion
    {
        // EVENTO ENTER DE TEXTBOX
        public Boolean EventoEnterCajaDeTexto(Control objeto, TextBox textBox)
        {
            Boolean Error = false;

            if (textBox.Text.Equals("Escribir..."))
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
                Error = true;
            }

            return Error;
        }

        // EVENTO LEAVE DE TEXTBOX
        public Boolean EventoLeaveCajaDeTexto(Control objeto, TextBox textBox)
        {
            Boolean Error = false;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Escribir...";
                textBox.ForeColor = Color.Gray;
                Error = true;
            }

            return Error;
        }

        // LIMPIAR TEXTBOX Y COMBOBOX
        public void LimpiarCajasDeTextoYComboBox(Control control)
        {
            foreach (Control item in control.Controls)
            {
                if (item is TextBox)
                {
                    ((TextBox)item).Clear();
                }
                else if (item is ComboBox)
                {
                    ComboBox combo = (ComboBox)item;
                    combo.SelectedIndex = -1;
                    combo.Text = "";
                }

                // Limpiar controles dentro de paneles o grupos
                if (item.HasChildren)
                {
                    LimpiarCajasDeTextoYComboBox(item);
                }
            }
        }

        // LIMPIAR SOLO TEXTBOX
        public void LimpiarCajasDeTexto(Control control)
        {
            foreach (Control item in control.Controls)
            {
                if (item is TextBox)
                {
                    ((TextBox)item).Clear();
                }

                if (item.HasChildren)
                {
                    LimpiarCajasDeTexto(item);
                }
            }
        }

        // VALIDAR CAMPOS VACIOS
        public bool ValidarCampoVacio(Control objeto, ErrorProvider errorProvider)
        {
            bool error = false;

            foreach (Control item in objeto.Controls)
            {
                if (item is TextBox)
                {
                    TextBox obj = (TextBox)item;

                    if (string.IsNullOrWhiteSpace(obj.Text) ||
                        obj.Text.Equals("Escribir..."))
                    {
                        errorProvider.SetError(
                            obj, "No puede estar vacio");

                        error = true;
                    }
                    else
                    {
                        errorProvider.SetError(obj, "");
                    }
                }

                // Revisar también controles dentro de paneles
                if (item.HasChildren)
                {
                    if (ValidarCampoVacio(item, errorProvider))
                    {
                        error = true;
                    }
                }
            }

            return error;
        }

        // VALIDAR TEXTBOX ESPECIFICO
        public bool ValidarCampoVacioEspecifico(
            TextBox obj, ErrorProvider errorProvider)
        {
            bool error = false;

            if (string.IsNullOrWhiteSpace(obj.Text) ||
                obj.Text.Equals("Escribir..."))
            {
                errorProvider.SetError(
                    obj, "No puede estar vacio");

                error = true;
            }
            else
            {
                errorProvider.SetError(obj, "");
            }

            return error;
        }

        // VALIDAR COMBOBOX ESPECIFICO
        public bool ValidarCampoVacioEspecificoCombo(
            ComboBox obj, ErrorProvider errorProvider)
        {
            bool error = obj.SelectedItem == null || string.IsNullOrWhiteSpace(obj.Text) ||
                (obj.DataSource != null && obj.SelectedValue == null) ||
                (obj.DropDownStyle == ComboBoxStyle.DropDown &&
                 !string.Equals(obj.Text.Trim(), obj.GetItemText(obj.SelectedItem).Trim(), StringComparison.CurrentCultureIgnoreCase));
            errorProvider.SetError(obj, error ? "Selecciona una opción de la lista." : string.Empty);
            return error;
        }

        // VALIDAR DATETIMEPICKER
        public bool ValidarCampoVacioEspecificoDataPiker(
            DateTimePicker obj, ErrorProvider errorProvider)
        {
            bool error = false;

            if ((obj.ShowCheckBox && !obj.Checked) ||
                string.IsNullOrWhiteSpace(obj.Text))
            {
                errorProvider.SetError(
                    obj, "Debe seleccionar una fecha");

                error = true;
            }
            else
            {
                errorProvider.SetError(obj, "");
            }

            return error;
        }

        // VALIDAR SOLO LETRAS
        public bool ValidarCampoCaracter(
            TextBox textBox, ErrorProvider errorProvider)
        {
            bool error = false;

            if (string.IsNullOrWhiteSpace(textBox.Text) ||
                !textBox.Text.All(c => char.IsLetter(c) ||
                                      char.IsWhiteSpace(c)))
            {
                errorProvider.SetError(
                    textBox, "Debe digitar solo letras");

                error = true;
            }
            else
            {
                errorProvider.SetError(textBox, "");
            }

            return error;
        }

        // VALIDAR CAMPOS NUMERICOS
        public bool ValidarCampoNumero(
            TextBox textBox, ErrorProvider errorProvider)
        {
            decimal numero;

            if (decimal.TryParse(textBox.Text, out numero))
            {
                errorProvider.SetError(textBox, "");
                return true;
            }
            else
            {
                errorProvider.SetError(
                    textBox, "Debe digitar solo numeros");

                return false;
            }
        }
    }
}
