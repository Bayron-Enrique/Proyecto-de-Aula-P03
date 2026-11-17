using DATOS;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGICA
{
    public class EmpleadoService
    {
        EmpleadoRepository repositorio;
        CargoRepository cargoRepositorio;

        public EmpleadoService(EmpleadoRepository repositorio, CargoRepository cargoRepositorio)
        {
            this.repositorio = repositorio;
            this.cargoRepositorio = cargoRepositorio;
        }

        public string Guardar(string cedula, string nombres, string apellidos, string sexo, string telefono, string direccion, string correoElectronico, int cargoId)
        {
            SepararCompleto(nombres, out string primerNombre, out string segundoNombre);
            SepararCompleto(apellidos, out string primerApellido, out string segundoApellido);
            return Guardar(cedula, primerNombre, segundoNombre, primerApellido, segundoApellido, sexo, telefono, direccion, correoElectronico, cargoId);
        }

        public string Guardar(string cedula, string primerNombre, string segundoNombre, string primerApellido,
            string segundoApellido, string sexo, string telefono, string direccion, string correoElectronico, int cargoId)
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
                if (string.IsNullOrWhiteSpace(primerNombre))
                {
                    return "Los nombres son obligatorios y no pueden estar vacios ni tener solo espacios ...";
                }
                if (primerNombre != primerNombre.Trim())
                {
                    return "Los nombres no pueden empezar ni terminar con espacios ...";
                }
                if (primerNombre.Contains("  "))
                {
                    return "Los nombres no pueden tener espacios seguidos ...";
                }
                if (primerNombre.Length < 2 || primerNombre.Length > 100)
                {
                    return "Los nombres deben tener entre 2 y 100 caracteres ...";
                }
                foreach (char c in primerNombre)
                {
                    if (!char.IsLetter(c) && c != ' ')
                    {
                        return $"Los nombres tienen el caracter '{c}' que no esta permitido, solo letras ...";
                    }
                }

                // Apellidos
                if (string.IsNullOrWhiteSpace(primerApellido))
                {
                    return "Los apellidos son obligatorios y no pueden estar vacios ni tener solo espacios ...";
                }
                if (primerApellido != primerApellido.Trim())
                {
                    return "Los apellidos no pueden empezar ni terminar con espacios ...";
                }
                if (primerApellido.Contains("  "))
                {
                    return "Los apellidos no pueden tener espacios seguidos ...";
                }
                if (primerApellido.Length < 2 || primerApellido.Length > 100)
                {
                    return "Los apellidos deben tener entre 2 y 100 caracteres ...";
                }
                foreach (char c in primerApellido)
                {
                    if (!char.IsLetter(c) && c != ' ')
                    {
                        return $"Los apellidos tienen el caracter '{c}' que no esta permitido, solo letras ...";
                    }
                }

                string errorSegundoNombre = ValidarNombreOpcional(segundoNombre, "segundo nombre");
                if (errorSegundoNombre != null) return errorSegundoNombre;
                string errorSegundoApellido = ValidarNombreOpcional(segundoApellido, "segundo apellido");
                if (errorSegundoApellido != null) return errorSegundoApellido;

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

                // Duplicados y cargo
                if (BuscarCedula(cedula) != null)
                {
                    return "Ya existe un empleado con esa cedula ...";
                }

                if (cargoId <= 0)
                {
                    return "Debe seleccionar un cargo valido ...";
                }

                Cargo cargo = cargoRepositorio.BuscarId(cargoId);

                if (cargo == null)
                {
                    return "El cargo seleccionado no existe ...";
                }

                Empleado empleado = new Empleado(cedula, primerNombre, segundoNombre, primerApellido, segundoApellido,
                    sexo, telefono, direccion, correoElectronico, cargo);
                repositorio.Guardar(empleado);

                return $"se registro el empleado con cedula {cedula} desde la logica ...";
            }
            catch (Exception ex)
            {
                return $"ocurrio un error al guardar: {ex.Message}";
            }
        }

        public Empleado BuscarCedula(string cedula)
        {
            return repositorio.BuscarCedula(cedula);
        }

        public string Modificar(string cedula, string nombres, string apellidos, string sexo,
            string telefono, string direccion, string correoElectronico, int cargoId)
        {
            SepararCompleto(nombres, out string primerNombre, out string segundoNombre);
            SepararCompleto(apellidos, out string primerApellido, out string segundoApellido);
            return Modificar(cedula, primerNombre, segundoNombre, primerApellido, segundoApellido, sexo, telefono, direccion, correoElectronico, cargoId);
        }

        public string Modificar(string cedula, string primerNombre, string segundoNombre, string primerApellido,
            string segundoApellido, string sexo, string telefono, string direccion, string correoElectronico, int cargoId)
        {
            try
            {
                Empleado empleado = BuscarCedula(cedula);
                if (empleado == null)
                {
                    return "No se encontro el empleado para modificar ...";
                }

                string error = ValidarDatosPersona(primerNombre, segundoNombre, primerApellido, segundoApellido, sexo, telefono, direccion, correoElectronico);
                if (error != null)
                {
                    return error;
                }

                if (cargoId <= 0)
                {
                    return "Debe seleccionar un cargo valido ...";
                }
                Cargo cargo = cargoRepositorio.BuscarId(cargoId);
                if (cargo == null)
                {
                    return "El cargo seleccionado no existe ...";
                }

                empleado.PrimerNombre = primerNombre;
                empleado.SegundoNombre = segundoNombre;
                empleado.PrimerApellido = primerApellido;
                empleado.SegundoApellido = segundoApellido;
                empleado.Sexo = sexo;
                empleado.Telefono = telefono;
                empleado.Direccion = direccion;
                empleado.CorreoElectronico = correoElectronico;
                empleado.Cargo = cargo;

                repositorio.Modificar(empleado);
                return "El empleado se modifico correctamente ...";
            }
            catch (Exception ex)
            {
                return $"Ocurrio un error al modificar el empleado: {ex.Message}";
            }
        }

        private static string ValidarDatosPersona(string primerNombre, string segundoNombre, string primerApellido, string segundoApellido, string sexo,
            string telefono, string direccion, string correoElectronico)
        {
            if (string.IsNullOrWhiteSpace(primerNombre) || primerNombre != primerNombre.Trim() || primerNombre.Contains("  ") || primerNombre.Length < 2 || primerNombre.Length > 100)
                return "El primer nombre debe tener entre 2 y 100 caracteres y no tener espacios sobrantes ...";
            string nombreError = ValidarNombreOpcional(segundoNombre, "segundo nombre"); if (nombreError != null) return nombreError;
            if (string.IsNullOrWhiteSpace(primerApellido) || primerApellido != primerApellido.Trim() || primerApellido.Contains("  ") || primerApellido.Length < 2 || primerApellido.Length > 100)
                return "El primer apellido debe tener entre 2 y 100 caracteres y no tener espacios sobrantes ...";
            nombreError = ValidarNombreOpcional(segundoApellido, "segundo apellido"); if (nombreError != null) return nombreError;

            if (sexo != "M" && sexo != "F") return "El sexo debe ser M o F (en mayuscula) ...";

            if (!string.IsNullOrEmpty(telefono))
            {
                foreach (char c in telefono)
                    if (c < '0' || c > '9') return "El telefono solo puede contener numeros ...";
                if (telefono.Length != 7 && telefono.Length != 10)
                    return "El telefono debe tener 7 digitos (fijo) o 10 digitos (celular) ...";
                if (telefono.Length == 10 && !telefono.StartsWith("3") && !telefono.StartsWith("60"))
                    return "El telefono de 10 digitos debe empezar por 3 o por 60 ...";
            }

            if (!string.IsNullOrEmpty(direccion) &&
                (string.IsNullOrWhiteSpace(direccion) || direccion != direccion.Trim() || direccion.Contains("  ") || direccion.Length < 5 || direccion.Length > 150))
                return "La direccion debe tener entre 5 y 150 caracteres y no tener espacios sobrantes ...";

            if (!string.IsNullOrEmpty(correoElectronico))
            {
                int arroba = correoElectronico.IndexOf('@');
                if (correoElectronico.Length > 100 || correoElectronico.Contains(" ") || arroba <= 0 ||
                    arroba != correoElectronico.LastIndexOf('@') || !correoElectronico.Substring(arroba + 1).Contains("."))
                    return "El correo electronico no tiene un formato valido ...";
            }

            return null;
        }

        private static string ValidarNombreOpcional(string valor, string etiqueta)
        {
            if (string.IsNullOrWhiteSpace(valor)) return null;
            if (valor != valor.Trim() || valor.Length > 100 || valor.Any(c => !char.IsLetter(c) && c != ' '))
                return "El " + etiqueta + " solo puede contener letras y espacios (máximo 100 caracteres) ...";
            return null;
        }

        private static void SepararCompleto(string valor, out string primero, out string segundo)
        {
            string texto = (valor ?? string.Empty).Trim();
            int espacio = texto.IndexOf(' ');
            primero = espacio < 0 ? texto : texto.Substring(0, espacio);
            segundo = espacio < 0 ? null : texto.Substring(espacio + 1).Trim();
            if (segundo != null && segundo.Length == 0) segundo = null;
        }

        public List<Empleado> Consultar()
        {
            return repositorio.Consultar();
        }

        public int TotalEmpleados()
        {
            return repositorio.Contar();
        }

        public bool TieneClaveConfigurada(string cedula)
        {
            return !string.IsNullOrWhiteSpace(repositorio.ObtenerClaveHash(cedula));
        }

        public Empleado Autenticar(string cedula, string clave)
        {
            if (string.IsNullOrWhiteSpace(cedula) || string.IsNullOrEmpty(clave)) return null;
            Empleado empleado = repositorio.BuscarCedula(cedula.Trim());
            if (empleado == null) return null;
            string hash = repositorio.ObtenerClaveHash(empleado.Cedula);
            return !string.IsNullOrEmpty(hash) && PasswordHasher.Verificar(clave, hash) ? empleado : null;
        }

        public string ConfigurarClaveInicial(string cedula, string claveTemporal, string nuevaClave)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cedula) || claveTemporal != cedula)
                    return "La clave temporal inicial debe ser la cédula del empleado.";
                if (string.IsNullOrEmpty(nuevaClave) || nuevaClave.Length < 8 || nuevaClave.Length > 100 || nuevaClave.Any(char.IsControl))
                    return "La contraseña nueva debe tener entre 8 y 100 caracteres válidos.";
                if (repositorio.BuscarCedula(cedula) == null) return "No se encontró el empleado.";
                if (!string.IsNullOrEmpty(repositorio.ObtenerClaveHash(cedula))) return "El empleado ya tiene una contraseña configurada.";
                repositorio.GuardarClaveHash(cedula, PasswordHasher.Crear(nuevaClave));
                return null;
            }
            catch (Exception ex) { return "No se pudo configurar la contraseña: " + ex.Message; }
        }

        public string Eliminar(string cedula)
        {
            try
            {
                Empleado empleado = BuscarCedula(cedula);

                if (empleado == null)
                {
                    return "No se encontro el empleado ...";
                }

                repositorio.Eliminar(empleado);

                return "El empleado se elimino correctamente ...";
            }
            catch (Exception ex)
            {
                return $"Ocurrio un error al eliminar: {ex.Message}";
            }
        }
    }
}
