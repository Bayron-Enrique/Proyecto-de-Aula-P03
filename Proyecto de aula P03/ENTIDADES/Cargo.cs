using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Cargo
    {
        public Cargo(int cargoId, string nombreCargo)
        {
            if (string.IsNullOrEmpty(nombreCargo))
            {
                throw new NullReferenceException("EL NOMBRE DEL CARGO NO PUEDE ESTAR VACIO");
            }

            CargoId = cargoId;
            NombreCargo = nombreCargo;
        }

        public int CargoId { get; set; }
        public string NombreCargo { get; set; }
    }
}