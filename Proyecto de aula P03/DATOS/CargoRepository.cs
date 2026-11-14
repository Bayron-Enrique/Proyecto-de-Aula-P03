using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class CargoRepository : IRepository<Cargo>
    {
        List<Cargo> cargos;

        public CargoRepository()
        {
            cargos = new List<Cargo>();
        }

        public void Guardar(Cargo cargo)
        {
            cargos.Add(cargo);
        }

        public List<Cargo> Consultar()
        {
            return cargos;
        }

        public Cargo BuscarId(int cargoId)
        {
            foreach (var cargo in cargos)
            {
                if (cargo.CargoId == cargoId)
                {
                    return cargo;
                }
            }
            return null;
        }

        public void Eliminar(Cargo cargo)
        {
            cargos.Remove(cargo);
        }
        public void Modificar(Cargo entidad)
        {
            throw new NotImplementedException();
        }
    }
}
