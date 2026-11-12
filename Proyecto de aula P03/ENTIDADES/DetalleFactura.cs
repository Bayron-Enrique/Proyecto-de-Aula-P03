using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class DetalleFactura
    {
        public int DetalleFacturaID { get; set; }
        public int Cantidad { get; set; }
        public double ValorUnitario { get; set; }
        public double ValorIVA { get; set; }
        public Articulo Articulo { get; set; }

        public DetalleFactura()
        {

        }

        public override string ToString()
        {
            return $"|DetalleID: {DetalleFacturaID} |Articulo: {Articulo.Nombre} |Cantidad: {Cantidad} " +
                $"|Valor Unitario: {ValorUnitario} |Valor IVA: {ValorIVA}";
        }
    }
}