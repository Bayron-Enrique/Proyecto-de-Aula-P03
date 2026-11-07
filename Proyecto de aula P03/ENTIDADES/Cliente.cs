using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Cliente : Persona
    {
        public Cliente()
        {

        }

        public override string ToString()
        {
            return $"|Cedula: {Cedula} |Nombre: {Nombre} |Telefono: {Telefono} |Direccion: {Direccion} |Correo: {Email}";
        }
    }
}
