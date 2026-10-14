using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Proveedor
    {
        public Proveedor(string nit, string razonSocial)
        {
            Nit = nit;
            RazonSocial = razonSocial;
        }

        public string Nit { get; set; }
        public string RazonSocial { get; set; }
    }
}