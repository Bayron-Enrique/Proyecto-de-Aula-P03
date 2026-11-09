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
        VehiculoRepository repositorio;
        ClienteRepository clienteRepositorio;

        public VehiculoService(VehiculoRepository repositorio, ClienteRepository clienteRepositorio)
        {
            this.repositorio = repositorio;
            this.clienteRepositorio = clienteRepositorio;
        }

        public string Guardar(string placa, int modelo, string marca, string tipoVehiculo, string color, string linea, string cedulaCliente)
        {
            try
            {
                if (string.IsNullOrEmpty(placa) || string.IsNullOrEmpty(tipoVehiculo))
                {
                    return "La placa y el tipo de vehiculo son obligatorios ...";
                }

                if (BuscarPlaca(placa) != null)
                {
                    return "Ya existe un vehiculo con esa placa ...";
                }

                Cliente cliente = clienteRepositorio.BuscarCedula(cedulaCliente);

                if (cliente == null)
                {
                    return "Debe seleccionar un cliente valido ...";
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