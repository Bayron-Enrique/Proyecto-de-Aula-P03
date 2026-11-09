using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class ArticuloRepository
    {
        List<Articulo> articulos;

        public ArticuloRepository()
        {
            articulos = new List<Articulo>();
        }

        public void Guardar(Articulo articulo)
        {
            articulos.Add(articulo);
        }

        public List<Articulo> Consultar()
        {
            return articulos;
        }

        public Articulo BuscarId(int articuloId)
        {
            foreach (var articulo in articulos)
            {
                if (articulo.ArticuloId == articuloId)
                {
                    return articulo;
                }
            }
            return null;
        }

        public void Eliminar(Articulo articulo)
        {
            articulos.Remove(articulo);
        }
    }
}