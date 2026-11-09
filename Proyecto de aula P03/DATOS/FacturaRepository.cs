using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class FacturaRepository
    {
        List<Factura> facturas;

        public FacturaRepository()
        {
            facturas = new List<Factura>();
        }

        public void Guardar(Factura factura)
        {
            facturas.Add(factura);
        }

        public List<Factura> Consultar()
        {
            return facturas;
        }

        public Factura BuscarId(int facturaId)
        {
            foreach (var factura in facturas)
            {
                if (factura.FacturaID == facturaId)
                {
                    return factura;
                }
            }
            return null;
        }

        public void Eliminar(Factura factura)
        {
            facturas.Remove(factura);
        }
    }
}