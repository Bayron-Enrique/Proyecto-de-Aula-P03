using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class TipoArticulo
    {
        public int TipoArticuloID { get; set; }
        public string Nombre { get; set; }

        public TipoArticulo()
        {
            
        }

        public TipoArticulo(int tipoArticuloID, string nombre)
        {
            TipoArticuloID = tipoArticuloID;
            Nombre = nombre;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
