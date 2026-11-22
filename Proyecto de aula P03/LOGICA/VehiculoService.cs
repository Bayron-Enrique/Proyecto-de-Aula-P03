using DATOS;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGICA
{
    public class VehiculoService
    {
        public int TotalVehiculos() { return repositorio.Contar(); }
        VehiculoRepository repositorio;
        ClienteRepository clienteRepositorio;

        public VehiculoService(VehiculoRepository repositorio, ClienteRepository clienteRepositorio)
        {
            this.repositorio = repositorio;
            this.clienteRepositorio = clienteRepositorio;
        }

        public List<Vehiculo> ConsultarPorClienteCedula(string cedula)
        {
            return repositorio.Consultar().Where(v => v.Cliente != null &&
                string.Equals(v.Cliente.Cedula, cedula, StringComparison.Ordinal)).ToList();
        }

        public string Guardar(string placa, int modelo, string marca, string tipoVehiculo, string color, string linea, string cedulaCliente)
        {
            try
            {
                // Placa: ABC123 (carro) o ABC12D (moto)
                if (string.IsNullOrWhiteSpace(placa))
                {
                    return "La placa es obligatoria y no puede tener espacios ...";
                }
                if (placa.Length != 6)
                {
                    return "La placa debe tener 6 caracteres, sin espacios ni guiones (ejemplo: ABC123 o ABC12D) ...";
                }
                for (int i = 0; i < 3; i++)
                {
                    if (placa[i] < 'A' || placa[i] > 'Z')
                    {
                        return "Las 3 primeras posiciones de la placa deben ser letras en mayuscula ...";
                    }
                }
                for (int i = 3; i < 5; i++)
                {
                    if (placa[i] < '0' || placa[i] > '9')
                    {
                        return "Las posiciones 4 y 5 de la placa deben ser numeros ...";
                    }
                }
                if ((placa[5] < '0' || placa[5] > '9') && (placa[5] < 'A' || placa[5] > 'Z'))
                {
                    return "La ultima posicion de la placa debe ser un numero (carro) o una letra mayuscula (moto) ...";
                }

                // Modelo
                if (modelo < 1950 || modelo > DateTime.Now.Year + 1)
                {
                    return $"El modelo debe estar entre 1950 y {DateTime.Now.Year + 1} ...";
                }

                // Marca (opcional)
                if (!string.IsNullOrEmpty(marca))
                {
                    if (string.IsNullOrWhiteSpace(marca))
                    {
                        return "La marca no puede tener solo espacios ...";
                    }
                    if (marca != marca.Trim() || marca.Contains("  "))
                    {
                        return "La marca no puede tener espacios al inicio, al final ni seguidos ...";
                    }
                    if (marca.Length < 2 || marca.Length > 50)
                    {
                        return "La marca debe tener entre 2 y 50 caracteres ...";
                    }
                    foreach (char c in marca)
                    {
                        if (!char.IsLetterOrDigit(c) && c != ' ')
                        {
                            return $"La marca tiene el caracter '{c}' que no esta permitido ...";
                        }
                    }
                }

                // Estos son los únicos valores aceptados por el CHECK de Oracle.
                if (tipoVehiculo != "Automovil" && tipoVehiculo != "Motocicleta")
                    return "El tipo de vehiculo debe ser Automovil o Motocicleta ...";

                // Color (opcional)
                if (!string.IsNullOrEmpty(color))
                {
                    if (string.IsNullOrWhiteSpace(color))
                    {
                        return "El color no puede tener solo espacios ...";
                    }
                    if (color != color.Trim() || color.Contains("  "))
                    {
                        return "El color no puede tener espacios al inicio, al final ni seguidos ...";
                    }
                    if (color.Length < 3 || color.Length > 50)
                    {
                        return "El color debe tener entre 3 y 50 caracteres ...";
                    }
                    foreach (char c in color)
                    {
                        if (!char.IsLetter(c) && c != ' ')
                        {
                            return $"El color tiene el caracter '{c}' que no esta permitido, solo letras ...";
                        }
                    }
                }

                // Linea (opcional)
                if (!string.IsNullOrEmpty(linea))
                {
                    if (string.IsNullOrWhiteSpace(linea))
                    {
                        return "La linea no puede tener solo espacios ...";
                    }
                    if (linea != linea.Trim() || linea.Contains("  "))
                    {
                        return "La linea no puede tener espacios al inicio, al final ni seguidos ...";
                    }
                    if (linea.Length > 50)
                    {
                        return "La linea no puede pasar de 50 caracteres ...";
                    }
                    foreach (char c in linea)
                    {
                        if (!char.IsLetterOrDigit(c) && c != ' ' && c != '-')
                        {
                            return $"La linea tiene el caracter '{c}' que no esta permitido ...";
                        }
                    }
                }

                // Cliente
                if (string.IsNullOrWhiteSpace(cedulaCliente))
                {
                    return "Debe seleccionar un cliente valido ...";
                }

                // Duplicados
                if (BuscarPlaca(placa) != null)
                {
                    return "Ya existe un vehiculo con esa placa ...";
                }

                Cliente cliente = clienteRepositorio.BuscarCedula(cedulaCliente);

                if (cliente == null)
                {
                    return "El cliente seleccionado no existe ...";
                }

                Vehiculo vehiculo = new Vehiculo(placa, modelo, marca, tipoVehiculo, color, linea, cliente);
                repositorio.Guardar(vehiculo);

                return $"se registro el vehiculo con placa {placa} desde la logica ...";
            }
            catch (Exception ex)
            {
                return $"ocurrio un error al guardar: {ex.Message}";
            }
        }

        public Vehiculo BuscarPlaca(string placa)
        {
            return repositorio.BuscarPlaca(placa);
        }

        public string Modificar(string placa, int modelo, string marca, string tipoVehiculo,
            string color, string linea, string cedulaCliente)
        {
            try
            {
                Vehiculo vehiculo = BuscarPlaca(placa);
                if (vehiculo == null)
                {
                    return "No se encontro el vehiculo para modificar ...";
                }
                if (modelo < 1950 || modelo > DateTime.Now.Year + 1)
                {
                    return $"El modelo debe estar entre 1950 y {DateTime.Now.Year + 1} ...";
                }
                if (tipoVehiculo != "Automovil" && tipoVehiculo != "Motocicleta")
                {
                    return "El tipo de vehiculo debe ser Automovil o Motocicleta ...";
                }

                string error = ValidarCampoOpcional(marca, 2, 50, "marca", permitirGuion: false);
                if (error != null) return error;
                error = ValidarCampoOpcional(color, 3, 50, "color", permitirGuion: false);
                if (error != null) return error;
                error = ValidarCampoOpcional(linea, 1, 50, "linea", permitirGuion: true);
                if (error != null) return error;

                if (string.IsNullOrWhiteSpace(cedulaCliente))
                {
                    return "Debe seleccionar un cliente valido ...";
                }
                Cliente cliente = clienteRepositorio.BuscarCedula(cedulaCliente);
                if (cliente == null)
                {
                    return "El cliente seleccionado no existe ...";
                }

                vehiculo.Modelo = modelo;
                vehiculo.Marca = marca;
                vehiculo.TipoVehiculo = tipoVehiculo;
                vehiculo.Color = color;
                vehiculo.Linea = linea;
                vehiculo.Cliente = cliente;

                repositorio.Modificar(vehiculo);
                return "El vehiculo se modifico correctamente ...";
            }
            catch (Exception ex)
            {
                return $"Ocurrio un error al modificar el vehiculo: {ex.Message}";
            }
        }

        private static string ValidarCampoOpcional(string valor, int minimo, int maximo, string nombreCampo, bool permitirGuion)
        {
            if (string.IsNullOrEmpty(valor)) return null;
            if (string.IsNullOrWhiteSpace(valor) || valor != valor.Trim() || valor.Contains("  ") ||
                valor.Length < minimo || valor.Length > maximo)
            {
                return $"El campo {nombreCampo} no cumple la longitud o el formato requerido ...";
            }
            foreach (char c in valor)
            {
                if (!char.IsLetterOrDigit(c) && c != ' ' && !(permitirGuion && c == '-'))
                {
                    return $"El campo {nombreCampo} contiene caracteres no permitidos ...";
                }
            }
            return null;
        }

        public List<Vehiculo> Consultar()
        {
            return repositorio.Consultar();
        }

        public string Eliminar(string placa)
        {
            try
            {
                Vehiculo vehiculo = BuscarPlaca(placa);

                if (vehiculo == null)
                {
                    return "No se encontro el vehiculo ...";
                }

                repositorio.Eliminar(vehiculo);

                return "El vehiculo se elimino correctamente ...";
            }
            catch (Exception ex)
            {
                return $"Ocurrio un error al eliminar: {ex.Message}";
            }
        }
    }
}
