using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Cliente : Persona
    {
        public Cliente(string cedula, string nombre, string telefono, string direccion, string correoElectronico)
            : base(cedula, nombre, telefono, direccion, correoElectronico)
        {
        }
    }
}