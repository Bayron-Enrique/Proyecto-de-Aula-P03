using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Factura
    {
        public int FacturaID { get; set; }
        public DateTime Fecha { get; set; }
        public double Descuento { get; set; }
        public double SubTotal { get; set; }
        public double Iva { get; set; }
        public double Total { get; set; }
        public string Observaciones { get; set; }
        public string EstadoPago { get; set; }
        public Cliente Cliente { get; set; }
        public Empleado Empleado { get; set; }
        public List<DetalleFactura> Detalles { get; set; }

        public Factura()
        {
            Detalles = new List<DetalleFactura>();
        }

        public void CalcularTotales()
        {
            SubTotal = 0;
            Iva = 0;

            foreach (var detalle in Detalles)
            {
                SubTotal += detalle.ValorUnitario * detalle.Cantidad;
                Iva += detalle.ValorIVA;
            }

            Total = SubTotal - Descuento + Iva;
        }

        public override string ToString()
        {
            return $"|FacturaID: {FacturaID} |Fecha: {Fecha} |Cliente: {Cliente.Nombre} |Empleado: {Empleado.Nombre} " +
                $"|SubTotal: {SubTotal} |Descuento: {Descuento} |Iva: {Iva} |Total: {Total} |Estado Pago: {EstadoPago}";
        }
    }
}