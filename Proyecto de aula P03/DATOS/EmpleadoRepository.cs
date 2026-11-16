using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class EmpleadoRepository : IRepository<Empleado>
    {
        List<Empleado> empleados;

        public EmpleadoRepository()
        {
            empleados = new List<Empleado>();
        }

        public void Guardar(Empleado empleado)
        {
            empleados.Add(empleado);
        }

        public List<Empleado> Consultar()
        {
            return empleados;
        }

        public Empleado BuscarCedula(string cedula)
        {
            foreach (var empleado in empleados)
            {
                if (empleado.Cedula == cedula)
                {
                    return empleado;
                }
            }
            return null;
        }

        public void Eliminar(Empleado empleado)
        {
            empleados.Remove(empleado);
        }
        public void Modificar(Empleado entidad)
        {
            throw new NotImplementedException();
        }
    }
}
