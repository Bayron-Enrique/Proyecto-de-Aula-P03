using DATOS;
using ENTIDADES;
using System;
using System.Collections.Generic;

namespace LOGICA
{
    public class ProveedorService
    {
        private readonly ProveedorRepository repositorio;

        public ProveedorService(ProveedorRepository repositorio)
        {
            this.repositorio = repositorio ?? throw new ArgumentNullException("repositorio");
        }

        public string Guardar(string nit, string razonSocial, string telefono)
        {
            try
            {
                string error = ValidarDatos(nit, razonSocial, telefono);
                if (error != null) return error;
                if (repositorio.BuscarNit(nit) != null) return "Ya existe un proveedor con ese NIT.";
                if (NombreDuplicado(razonSocial, null)) return "Ya existe un proveedor con esa razón social.";

                repositorio.Guardar(new Proveedor(nit, razonSocial, telefono));
                return "Se registró el proveedor con NIT " + nit + ".";
            }
            catch (Exception ex)
            {
                return "No se pudo guardar el proveedor: " + ex.Message;
            }
        }

        public Proveedor BuscarNit(string nit)
        {
            return repositorio.BuscarNit(nit);
        }

        public string Modificar(string nit, string razonSocial, string telefono)
        {
            try
            {
                Proveedor proveedor = repositorio.BuscarNit(nit);
                if (proveedor == null) return "No se encontró el proveedor.";
                string error = ValidarDatos(nit, razonSocial, telefono);
                if (error != null) return error;
                if (NombreDuplicado(razonSocial, nit)) return "Ya existe otro proveedor con esa razón social.";

                proveedor.RazonSocial = razonSocial;
                proveedor.Telefono = string.IsNullOrWhiteSpace(telefono) ? null : telefono;
                repositorio.Modificar(proveedor);
                return "El proveedor se modificó correctamente.";
            }
            catch (Exception ex)
            {
                return "No se pudo modificar el proveedor: " + ex.Message;
            }
        }

        public List<Proveedor> Consultar()
        {
            return repositorio.Consultar();
        }

        public string Eliminar(string nit)
        {
            try
            {
                Proveedor proveedor = repositorio.BuscarNit(nit);
                if (proveedor == null) return "No se encontró el proveedor.";
                repositorio.Eliminar(proveedor);
                return "El proveedor se eliminó correctamente.";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el proveedor: " + ex.Message;
            }
        }

        private bool NombreDuplicado(string razonSocial, string nitExcluir)
        {
            foreach (Proveedor existente in repositorio.Consultar())
            {
                if (existente.Nit != nitExcluir &&
                    string.Equals(existente.RazonSocial, razonSocial, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        private static string ValidarDatos(string nit, string razonSocial, string telefono)
        {
            if (string.IsNullOrWhiteSpace(nit) || nit != nit.Trim() || nit.Length > 15)
                return "El NIT es obligatorio, no puede tener espacios y admite hasta 15 caracteres.";

            string numero = nit;
            int guion = nit.IndexOf('-');
            if (guion >= 0)
            {
                if (guion != nit.LastIndexOf('-') || guion != nit.Length - 2 || nit[guion + 1] < '0' || nit[guion + 1] > '9')
                    return "El dígito de verificación debe escribirse como un solo número después de un guion.";
                numero = nit.Substring(0, guion);
            }
            if (numero.Length < 8 || numero.Length > 10 || numero.StartsWith("0", StringComparison.Ordinal))
                return "El NIT debe tener entre 8 y 10 dígitos y no comenzar por cero.";
            foreach (char c in numero)
                if (c < '0' || c > '9') return "El NIT solo admite números y un guion para el dígito de verificación.";

            if (string.IsNullOrWhiteSpace(razonSocial) || razonSocial != razonSocial.Trim() || razonSocial.Contains("  ") ||
                razonSocial.Length < 3 || razonSocial.Length > 100)
                return "La razón social debe tener entre 3 y 100 caracteres, sin espacios sobrantes.";
            foreach (char c in razonSocial)
                if (!char.IsLetterOrDigit(c) && c != ' ' && ".,&-".IndexOf(c) < 0)
                    return "La razón social contiene caracteres no permitidos.";

            if (!string.IsNullOrWhiteSpace(telefono))
            {
                if (telefono.Length != 7 && telefono.Length != 10)
                    return "El teléfono debe tener 7 dígitos (fijo) o 10 (celular).";
                foreach (char c in telefono)
                    if (c < '0' || c > '9') return "El teléfono solo admite números, sin espacios ni signos.";
                if (telefono.Length == 10 && !telefono.StartsWith("3", StringComparison.Ordinal) && !telefono.StartsWith("60", StringComparison.Ordinal))
                    return "El teléfono de 10 dígitos debe comenzar por 3 o por 60.";
            }
            return null;
        }
    }
}
