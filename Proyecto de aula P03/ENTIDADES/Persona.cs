using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Persona
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Sexo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string CorreoElectronico { get; set; }

        public Persona()
        {

        }

        public Persona(string cedula, string nombres, string apellidos, string sexo, string telefono, string direccion, string correoElectronico)
        {
            Cedula = cedula;
            Nombre = nombres;
            Apellido = apellidos;
            Sexo = sexo;
            Telefono = telefono;
            Direccion = direccion;
            CorreoElectronico = correoElectronico;
        }
    }
}