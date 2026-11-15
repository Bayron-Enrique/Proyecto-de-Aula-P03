using DATOS;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGICA
{
    public class ProveedorService
    {
        ProveedorRepository repositorio;

        public ProveedorService(ProveedorRepository repositorio)
        {
            this.repositorio = repositorio;
        }

        public string Guardar(string nit, string razonSocial, string telefono)
        {
            try
            {
                // NIT (900123456 o 900123456-7)
                if (string.IsNullOrWhiteSpace(nit))
                {
                    return "El NIT es obligatorio y no puede tener espacios ...";
                }
                string numeroNit = nit;
                int guion = nit.IndexOf('-');
                if (guion >= 0)
                {
                    numeroNit = nit.Substring(0, guion);
                    string verificacion = nit.Substring(guion + 1);
                    if (verificacion.Length != 1 || verificacion[0] < '0' || verificacion[0] > '9')
                    {
                        return "El digito de verificacion del NIT debe ser un solo numero (ejemplo: 900123456-7) ...";
                    }
                }
                foreach (char c in numeroNit)
                {
                    if (c < '0' || c > '9')
                    {
                        return "El NIT solo puede tener numeros y un guion antes del digito de verificacion ...";
                    }
                }
                if (numeroNit.Length < 8 || numeroNit.Length > 10)
                {
                    return "El NIT debe tener entre 8 y 10 digitos ...";
                }
                if (numeroNit.StartsWith("0"))
                {
                    return "El NIT no puede empezar por cero ...";
                }

                // Razon social
                if (string.IsNullOrWhiteSpace(razonSocial))
                {
                    return "La razon social es obligatoria y no puede estar vacia ni tener solo espacios ...";
                }
                if (razonSocial != razonSocial.Trim())
                {
                    return "La razon social no puede empezar ni terminar con espacios ...";
                }
                if (razonSocial.Contains("  "))
                {
                    return "La razon social no puede tener espacios seguidos ...";
                }
                if (razonSocial.Length < 3 || razonSocial.Length > 100)
                {
                    return "La razon social debe tener entre 3 y 100 caracteres ...";
                }
                foreach (char c in razonSocial)
                {
                    if (!char.IsLetterOrDigit(c) && c != ' ' && ".,&-".IndexOf(c) < 0)
                    {
                        return $"La razon social tiene el caracter '{c}' que no esta permitido ...";
                    }
                }

                // Telefono (opcional)
                if (!string.IsNullOrEmpty(telefono))
                {
                    foreach (char c in telefono)
                    {
                        if (c < '0' || c > '9')
                        {
                            return "El telefono solo puede tener numeros, sin espacios ni signos ...";
                        }
                    }
                    if (telefono.Length != 7 && telefono.Length != 10)
                    {
                        return "El telefono debe tener 7 digitos (fijo) o 10 digitos (celular) ...";
                    }
                    if (telefono.Length == 10 && !telefono.StartsWith("3") && !telefono.StartsWith("60"))
                    {
                        return "El telefono de 10 digitos debe empezar por 3 (celular) o por 60 (fijo) ...";
                    }
                }

                // Duplicados
                if (BuscarNit(nit) != null)
                {
                    return "Ya existe un proveedor con ese NIT ...";
                }
                foreach (var existente in repositorio.Consultar())
                {
                    if (string.Equals(existente.RazonSocial, razonSocial, StringComparison.OrdinalIgnoreCase))
                    {
                        return "Ya existe un proveedor con esa razon social ...";
                    }
                }

                Proveedor proveedor = new Proveedor(nit, razonSocial, telefono);
                repositorio.Guardar(proveedor);

                return $"se registro el proveedor con NIT {nit} desde la logica ...";
            }
            catch (Exception ex)
            {
                return $"ocurrio un error al guardar: {ex.Message}";
            }
        }

        public Proveedor BuscarNit(string nit)
        {
            return repositorio.BuscarNit(nit);
        }

        public List<Proveedor> Consultar()
        {
            return repositorio.Consultar();
        }

        public string Eliminar(string nit)
        {
            try
            {
                Proveedor proveedor = BuscarNit(nit);

                if (proveedor == null)
                {
                    return "No se encontro el proveedor ...";
                }

                repositorio.Eliminar(proveedor);

                return "El proveedor se elimino correctamente ...";
            }
            catch (Exception ex)
            {
                return $"Ocurrio un error al eliminar: {ex.Message}";
            }
        }
    }
}