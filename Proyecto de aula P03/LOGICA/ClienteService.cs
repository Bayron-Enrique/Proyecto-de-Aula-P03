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
                if (string.IsNullOrEmpty(cedula) || string.IsNullOrEmpty(nombres) || string.IsNullOrEmpty(apellidos) || string.IsNullOrEmpty(sexo))
                {
                    return "La cedula, los nombres, los apellidos y el sexo son obligatorios ...";
                }

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