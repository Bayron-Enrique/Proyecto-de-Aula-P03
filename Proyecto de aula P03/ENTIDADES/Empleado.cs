using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Empleado : Persona
    {
        public Empleado(string cedula, string nombres, string apellidos, string sexo, string telefono, string direccion, string correoElectronico, Cargo cargo)
            : base(cedula, nombres, apellidos, sexo, telefono, direccion, correoElectronico)
        {
            if (cargo == null)
            {
                throw new NullReferenceException("EL CARGO NO PUEDE SER NULO");
            }

            Cargo = cargo;
        }

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
                $" |Correo: {CorreoElectronico} |Cargo: {Cargo}";
        }
    }
}