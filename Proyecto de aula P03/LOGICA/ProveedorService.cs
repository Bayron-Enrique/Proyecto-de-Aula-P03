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
                if (string.IsNullOrEmpty(nit) || string.IsNullOrEmpty(razonSocial))
                {
                    return "El NIT y la razon social son obligatorios ...";
                }

                if (BuscarNit(nit) != null)
                {
                    return "Ya existe un proveedor con ese NIT ...";
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