using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class DetalleFactura
    {
        public int DetalleFacturaID { get; set; }
        public int Cantidad { get; set; }
        public decimal ValorUnitario { get; set; }
        public int FacturaID { get; set; }
        public decimal PorcentajeIVA { get; set; }
        public string Descripcion { get; set; }
        public Articulo Articulo { get; set; }

        public DetalleFactura()
        {

        }

        public DetalleFactura(int cantidad, Articulo articulo)
        {
            Cantidad = cantidad;
            Articulo = articulo;
            ValorUnitario = articulo.PrecioVenta;
            PorcentajeIVA = articulo.PorcentajeIVA;
            Descripcion = articulo.Nombre;

        }

        public decimal Subtotal
        {
            get { return ValorUnitario * Cantidad; }
        }

        public decimal ValorIVA
        {
            get { return Subtotal * (PorcentajeIVA / 100); }
        }

        public decimal Total
        {
            get { return Subtotal + ValorIVA; }
        }

        public override string ToString()
        {
            return $"|DetalleID: {DetalleFacturaID} " +
                $"|Articulo: {Descripcion} " +
                $"|Cantidad: {Cantidad} " +
                $"|Valor Unitario: {ValorUnitario} " +
                $"|Subtotal: {Subtotal}" +
                $"|IVA:({PorcentajeIVA}%): {ValorIVA}" +
                $"Total: {Total}";
        }
    }
}