using DATOS;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGICA
{
    public class ArticuloService
    {
        ArticuloRepository repositorio;
        ProveedorRepository proveedorRepositorio;
        int siguienteArticuloId;

        public ArticuloService(ArticuloRepository repositorio, ProveedorRepository proveedorRepositorio)
        {
            this.repositorio = repositorio;
            this.proveedorRepositorio = proveedorRepositorio;
            siguienteArticuloId = 1;
        }

        public string Guardar(string nombre, double precio, double porcentajeIVA, string tipoArticulo, int cantidad, string descripcion, int duracion, string nitProveedor)
        {
            try
            {
                if (string.IsNullOrEmpty(nombre))
                {
                    return "El nombre del articulo es obligatorio ...";
                }
                if (precio <= 0)
                {
                    return "El precio debe ser mayor que cero ...";
                }
                if (cantidad < 0)
                {
                    return "La cantidad no puede ser negativa ...";
                }

                Proveedor proveedor = proveedorRepositorio.BuscarNit(nitProveedor);

                if (proveedor == null)
                {
                    return "Debe seleccionar un proveedor valido ...";
                }

                int articuloId = GenerarArticuloId();

                Articulo articulo = new Articulo(articuloId, nombre, precio, porcentajeIVA, tipoArticulo, cantidad, descripcion, duracion, proveedor);
                repositorio.Guardar(articulo);

                return $"se registro el articulo numero {articuloId} desde la logica ...";
            }
            catch (Exception ex)
            {
                return $"ocurrio un error al guardar: {ex.Message}";
            }
        }

        private int GenerarArticuloId()
        {
            int numero = siguienteArticuloId;

            while (BuscarId(numero) != null)
            {
                numero++;
            }

            siguienteArticuloId = numero + 1;
            return numero;
        }

        public Articulo BuscarId(int articuloId)
        {
            return repositorio.BuscarId(articuloId);
        }

        public List<Articulo> Consultar()
        {
            return repositorio.Consultar();
        }

        public string Eliminar(int articuloId)
        {
            try
            {
                Articulo articulo = BuscarId(articuloId);

                if (articulo == null)
                {
                    return "No se encontro el articulo ...";
                }

                repositorio.Eliminar(articulo);

                return "El articulo se elimino correctamente ...";
            }
            catch (Exception ex)
            {
                return $"Ocurrio un error al eliminar: {ex.Message}";
            }
        }
    }
}