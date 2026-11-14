using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class SoatRepository : IRepository<Soat>
    {
        List<Soat> soats;

        public SoatRepository()
        {
            soats = new List<Soat>();
        }

        public void Guardar(Soat soat)
        {
            soats.Add(soat);
        }

        public List<Soat> Consultar()
        {
            return soats;
        }

        public Soat BuscarId(int soatId)
        {
            foreach (var soat in soats)
            {
                if (soat.SoatId == soatId)
                {
                    return soat;
                }
            }
            return null;
        }

        public void Eliminar(Soat soat)
        {
            soats.Remove(soat);
        }
        public void Modificar(Soat entidad)
        {
            throw new NotImplementedException();
        }
    }
}
