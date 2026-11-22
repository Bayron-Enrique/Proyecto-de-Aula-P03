using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Cliente : Persona
    {
        public long? TelegramChatId { get; set; }

        public Cliente(string cedula, string nombres, string apellidos, string sexo, string telefono, string direccion, string correoElectronico)
            : base(cedula, nombres, apellidos, sexo, telefono, direccion, correoElectronico)
        {
        }

        public Cliente(string cedula, string primerNombre, string segundoNombre, string primerApellido,
            string segundoApellido, string sexo, string telefono, string direccion, string correoElectronico)
            : base(cedula, primerNombre, segundoNombre, primerApellido, segundoApellido, sexo, telefono, direccion, correoElectronico)
        {
        }

        public Cliente()
        {

        }

        public override string ToString()
        {
            return $"|Cedula: {Cedula} |Nombre: {Nombre} |Telefono: {Telefono} |Direccion: {Direccion} |Correo: {CorreoElectronico}";
        }
    }
}
