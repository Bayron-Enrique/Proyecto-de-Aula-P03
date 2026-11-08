using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Proveedor
    {
        public Proveedor(string nit, string razonSocial, string telefono)
        {
            Nit = nit;
            RazonSocial = razonSocial;
            Telefono = telefono;
        }

        public string Nit { get; set; }
        public string RazonSocial { get; set; }
        public string Telefono { get; set; }
    }
}