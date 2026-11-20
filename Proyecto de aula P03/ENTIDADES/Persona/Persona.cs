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
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        // Alias de compatibilidad para los formularios y módulos existentes.
        public string Nombre
        {
            get { return Unir(PrimerNombre, SegundoNombre); }
            set { Separar(value, out string primero, out string segundo); PrimerNombre = primero; SegundoNombre = segundo; }
        }
        public string Apellido
        {
            get { return Unir(PrimerApellido, SegundoApellido); }
            set { Separar(value, out string primero, out string segundo); PrimerApellido = primero; SegundoApellido = segundo; }
        }
        public string NombreCompleto { get { return Unir(PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido); } }
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

        public Persona(string cedula, string primerNombre, string segundoNombre, string primerApellido,
            string segundoApellido, string sexo, string telefono, string direccion, string correoElectronico)
        {
            Cedula = cedula;
            PrimerNombre = primerNombre;
            SegundoNombre = segundoNombre;
            PrimerApellido = primerApellido;
            SegundoApellido = segundoApellido;
            Sexo = sexo;
            Telefono = telefono;
            Direccion = direccion;
            CorreoElectronico = correoElectronico;
        }

        private static void Separar(string valor, out string primero, out string resto)
        {
            string limpio = (valor ?? string.Empty).Trim();
            int espacio = limpio.IndexOf(' ');
            primero = espacio < 0 ? limpio : limpio.Substring(0, espacio);
            resto = espacio < 0 ? null : limpio.Substring(espacio + 1).Trim();
            if (resto != null && resto.Length == 0) resto = null;
        }

        private static string Unir(params string[] partes)
        {
            return string.Join(" ", (partes ?? new string[0])
                .Where(p => !string.IsNullOrWhiteSpace(p)).Select(p => p.Trim()));
        }
    }
}
