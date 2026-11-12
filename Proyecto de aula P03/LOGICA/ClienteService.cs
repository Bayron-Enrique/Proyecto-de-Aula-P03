using DATOS;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGICA
{
    public class ClienteService
    {
        ClienteRepository repositorio;

        public ClienteService(ClienteRepository repositorio)
        {
            this.repositorio = repositorio;
        }

        public string Guardar(string cedula, string nombres, string apellidos, string sexo, string telefono, string direccion, string correoElectronico)
        {
            try
            {
                // Cedula
                if (string.IsNullOrWhiteSpace(cedula))
                {
                    return "La cedula es obligatoria y no puede tener espacios ...";
                }
                foreach (char c in cedula)
                {
                    if (c < '0' || c > '9')
                    {
                        return "La cedula solo puede tener numeros, sin espacios ni signos ...";
                    }
                }
                if (cedula.Length < 6 || cedula.Length > 10)
                {
                    return "La cedula debe tener entre 6 y 10 digitos ...";
                }
                if (cedula.StartsWith("0"))
                {
                    return "La cedula no puede empezar por cero ...";
                }

                // Nombres
                if (string.IsNullOrWhiteSpace(nombres))
                {
                    return "Los nombres son obligatorios y no pueden estar vacios ni tener solo espacios ...";
                }
                if (nombres != nombres.Trim())
                {
                    return "Los nombres no pueden empezar ni terminar con espacios ...";
                }
                if (nombres.Contains("  "))
                {
                    return "Los nombres no pueden tener espacios seguidos ...";
                }
                if (nombres.Length < 2 || nombres.Length > 100)
                {
                    return "Los nombres deben tener entre 2 y 100 caracteres ...";
                }
                foreach (char c in nombres)
                {
                    if (!char.IsLetter(c) && c != ' ')
                    {
                        return $"Los nombres tienen el caracter '{c}' que no esta permitido, solo letras ...";
                    }
                }

                // Apellidos
                if (string.IsNullOrWhiteSpace(apellidos))
                {
                    return "Los apellidos son obligatorios y no pueden estar vacios ni tener solo espacios ...";
                }
                if (apellidos != apellidos.Trim())
                {
                    return "Los apellidos no pueden empezar ni terminar con espacios ...";
                }
                if (apellidos.Contains("  "))
                {
                    return "Los apellidos no pueden tener espacios seguidos ...";
                }
                if (apellidos.Length < 2 || apellidos.Length > 100)
                {
                    return "Los apellidos deben tener entre 2 y 100 caracteres ...";
                }
                foreach (char c in apellidos)
                {
                    if (!char.IsLetter(c) && c != ' ')
                    {
                        return $"Los apellidos tienen el caracter '{c}' que no esta permitido, solo letras ...";
                    }
                }

                // Sexo
                if (sexo != "M" && sexo != "F")
                {
                    return "El sexo debe ser M o F (en mayuscula) ...";
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

                // Direccion (opcional)
                if (!string.IsNullOrEmpty(direccion))
                {
                    if (string.IsNullOrWhiteSpace(direccion))
                    {
                        return "La direccion no puede tener solo espacios ...";
                    }
                    if (direccion != direccion.Trim())
                    {
                        return "La direccion no puede empezar ni terminar con espacios ...";
                    }
                    if (direccion.Contains("  "))
                    {
                        return "La direccion no puede tener espacios seguidos ...";
                    }
                    if (direccion.Length < 5 || direccion.Length > 150)
                    {
                        return "La direccion debe tener entre 5 y 150 caracteres ...";
                    }
                    foreach (char c in direccion)
                    {
                        if (char.IsControl(c))
                        {
                            return "La direccion tiene caracteres no permitidos ...";
                        }
                    }
                }

                // Correo (opcional)
                if (!string.IsNullOrEmpty(correoElectronico))
                {
                    if (correoElectronico.Length > 100)
                    {
                        return "El correo no puede pasar de 100 caracteres ...";
                    }
                    if (correoElectronico.Contains(" "))
                    {
                        return "El correo no puede tener espacios ...";
                    }
                    int arroba = correoElectronico.IndexOf('@');
                    if (arroba <= 0 || arroba != correoElectronico.LastIndexOf('@'))
                    {
                        return "El correo debe tener una sola @ y texto antes de ella ...";
                    }
                    foreach (char c in correoElectronico)
                    {
                        if (!(c < 128 && char.IsLetterOrDigit(c)) && "@._-+".IndexOf(c) < 0)
                        {
                            return $"El correo tiene el caracter '{c}' que no esta permitido ...";
                        }
                    }
                    string dominio = correoElectronico.Substring(arroba + 1);
                    if (correoElectronico.Contains("..") || correoElectronico.StartsWith(".") || !dominio.Contains(".") || dominio.StartsWith(".") || dominio.StartsWith("-") || dominio.EndsWith("."))
                    {
                        return "El correo no es valido, revisa los puntos y el dominio (ejemplo: nombre@gmail.com) ...";
                    }
                    if (dominio.Length - dominio.LastIndexOf('.') - 1 < 2)
                    {
                        return "La terminacion del correo no es valida (ejemplo: .com) ...";
                    }
                }

                // Duplicados
                if (BuscarCedula(cedula) != null)
                {
                    return "Ya existe un cliente con esa cedula ...";
                }

                Cliente cliente = new Cliente(cedula, nombres, apellidos, sexo, telefono, direccion, correoElectronico);
                repositorio.Guardar(cliente);

                return $"se registro el cliente con cedula {cedula} desde la logica ...";
            }
            catch (Exception ex)
            {
                return $"ocurrio un error al guardar: {ex.Message}";
            }
        }

        public Cliente BuscarCedula(string cedula)
        {
            return repositorio.BuscarCedula(cedula);
        }

        public List<Cliente> Consultar()
        {
            return repositorio.Consultar();
        }

        public string Eliminar(string cedula)
        {
            try
            {
                Cliente cliente = BuscarCedula(cedula);

                if (cliente == null)
                {
                    return "No se encontro el cliente ...";
                }

                repositorio.Eliminar(cliente);

                return "El cliente se elimino correctamente ...";
            }
            catch (Exception ex)
            {
                return $"Ocurrio un error al eliminar: {ex.Message}";
            }
        }
    }
}