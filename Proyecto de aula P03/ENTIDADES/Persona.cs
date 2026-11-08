using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Persona
    {
        public Persona(string cedula, string nombres, string apellidos, string sexo, string telefono, string direccion, string correoElectronico)
        {
            Cedula = cedula;
            Nombres = nombres;
            Apellidos = apellidos;
            Sexo = sexo;
            Telefono = telefono;
            Direccion = direccion;
            CorreoElectronico = correoElectronico;
        }

        public string Cedula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Sexo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string CorreoElectronico { get; set; }
    }
}