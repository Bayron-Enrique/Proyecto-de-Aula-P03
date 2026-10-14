using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Empleado : Persona
    {
        public Empleado(string cedula, string nombre, string telefono, string direccion, string correoElectronico, Cargo cargo)
            : base(cedula, nombre, telefono, direccion, correoElectronico)
        {
            if (cargo == null)
            {
                throw new NullReferenceException("EL CARGO NO PUEDE SER NULO");
            }

            Cargo = cargo;
        }

        public Cargo Cargo { get; set; }
    }
}