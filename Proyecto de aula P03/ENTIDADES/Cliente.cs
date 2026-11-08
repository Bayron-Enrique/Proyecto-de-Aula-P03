using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Cliente : Persona
    {
        public Cliente(string cedula, string nombres, string apellidos, string sexo, string telefono, string direccion, string correoElectronico)
            : base(cedula, nombres, apellidos, sexo, telefono, direccion, correoElectronico)
        {
        }
    }
}