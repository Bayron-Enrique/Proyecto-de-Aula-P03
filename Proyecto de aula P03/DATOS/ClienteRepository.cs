using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class ClienteRepository
    {
        List<Cliente> clientes;

        public ClienteRepository()
        {
            clientes = new List<Cliente>();
        }

        public void Guardar(Cliente cliente)
        {
            clientes.Add(cliente);
        }

        public List<Cliente> Consultar()
        {
            return clientes;
        }

        public Cliente BuscarCedula(string cedula)
        {
            foreach (var cliente in clientes)
            {
                if (cliente.Cedula == cedula)
                {
                    return cliente;
                }
            }
            return null;
        }

        public void Eliminar(Cliente cliente)
        {
            clientes.Remove(cliente);
        }
    }
}