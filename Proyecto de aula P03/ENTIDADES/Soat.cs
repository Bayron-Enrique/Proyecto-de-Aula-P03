using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Soat
    {
        public Soat(int soatId, string numeroSoat, DateTime fechaInicio, Vehiculo vehiculo)
        {
            if (vehiculo == null)
            {
                throw new NullReferenceException("EL VEHICULO NO PUEDE SER NULO");
            }

            SoatId = soatId;
            NumeroSoat = numeroSoat;
            FechaInicio = fechaInicio;
            Vehiculo = vehiculo;
        }

        public int SoatId { get; set; }
        public string NumeroSoat { get; set; }
        public DateTime FechaInicio { get; set; }
        public Vehiculo Vehiculo { get; set; }
    }
}