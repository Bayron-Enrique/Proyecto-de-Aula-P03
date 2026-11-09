using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class ProveedorRepository
    {
        List<Proveedor> proveedores;

        public ProveedorRepository()
        {
            proveedores = new List<Proveedor>();
        }

        public void Guardar(Proveedor proveedor)
        {
            proveedores.Add(proveedor);
        }

        public List<Proveedor> Consultar()
        {
            return proveedores;
        }

        public Proveedor BuscarNit(string nit)
        {
            foreach (var proveedor in proveedores)
            {
                if (proveedor.Nit == nit)
                {
                    return proveedor;
                }
            }
            return null;
        }

        public void Eliminar(Proveedor proveedor)
        {
            proveedores.Remove(proveedor);
        }
    }
}