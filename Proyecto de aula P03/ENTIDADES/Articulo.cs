using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Articulo
    {
        public Articulo(int articuloId, string nombre, double precio, double porcentajeIVA, string tipoArticulo, int cantidad, string descripcion, int duracion, Proveedor proveedor)
        {
            if (proveedor == null)
            {
                throw new NullReferenceException("EL PROVEEDOR NO PUEDE SER NULO");
            }

            ArticuloId = articuloId;
            Nombre = nombre;
            Precio = precio;
            PorcentajeIVA = porcentajeIVA;
            TipoArticulo = tipoArticulo;
            Cantidad = cantidad;
            Descripcion = descripcion;
            Duracion = duracion;
            Proveedor = proveedor;
        }

        public int ArticuloId { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public double PorcentajeIVA { get; set; }
        public string TipoArticulo { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public int Duracion { get; set; }
        public Proveedor Proveedor { get; set; }
    }
}