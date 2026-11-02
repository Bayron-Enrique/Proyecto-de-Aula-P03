using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Cargo
    {
        public int CargoID{ get; set; }
        public string NombreCargo { get; set; }
        public Cargo()
        {
            
        }

        public override string ToString()
        {
            return NombreCargo;
        }
    }
}
