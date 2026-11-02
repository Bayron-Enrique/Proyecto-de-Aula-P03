using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Empleado : Persona
    {
        public Cargo Cargo { get; set; }
        public Empleado()
        {
            
        }

        public Empleado(Cargo cargo)
        {
            Cargo = cargo;
        }

        public override string ToString()
        {
            return $"|Cedula: {Cedula} |Nombre: {Nombre} |Telefono: {Telefono} |Direccion: {Direccion}" +
                $" |Correo: {Email} |Cargo: {Cargo}";
        }
    }
}
