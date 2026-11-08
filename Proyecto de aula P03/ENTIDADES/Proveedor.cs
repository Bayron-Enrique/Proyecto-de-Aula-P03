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
        public string Email { get; set; }
        public string Direccion { get; set; }

        public Proveedor()
        {

        }

        public Proveedor(string nit, string razonSocial, string email, string telefono, string direccion)
        {
            Nit = nit;
            RazonSocial = razonSocial;
            Email = email;
            Telefono = telefono;
            Direccion = direccion;
        }

        public override string ToString()
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