using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class VehiculoRepository : IRepository<Vehiculo>
    {
        List<Vehiculo> vehiculos;

        public VehiculoRepository()
        {
            vehiculos = new List<Vehiculo>();
        }

        public void Guardar(Vehiculo vehiculo)
        {
            vehiculos.Add(vehiculo);
        }

        public List<Vehiculo> Consultar()
        {
            return vehiculos;
        }

        public Vehiculo BuscarPlaca(string placa)
        {
            foreach (var vehiculo in vehiculos)
            {
                if (vehiculo.Placa == placa)
                {
                    return vehiculo;
                }
            }
            return null;
        }

        public void Eliminar(Vehiculo vehiculo)
        {
            vehiculos.Remove(vehiculo);
        }
        public void Modificar(Vehiculo entidad)
        {
            throw new NotImplementedException();
        }
    }
}
