using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Persona
    {
        public Persona(string cedula, string nombre, string telefono, string direccion, string correoElectronico)
        {
            Cedula = cedula;
            Nombre = nombre;
            Telefono = telefono;
            Direccion = direccion;
            CorreoElectronico = correoElectronico;
        }

        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string CorreoElectronico { get; set; }
    }
}