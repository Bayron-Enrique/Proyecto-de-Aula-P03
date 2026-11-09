using DATOS;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGICA
{
    public class EmpleadoService
    {
        EmpleadoRepository repositorio;
        CargoRepository cargoRepositorio;

        public EmpleadoService(EmpleadoRepository repositorio, CargoRepository cargoRepositorio)
        {
            this.repositorio = repositorio;
            this.cargoRepositorio = cargoRepositorio;
        }

        public string Guardar(string cedula, string nombres, string apellidos, string sexo, string telefono, string direccion, string correoElectronico, int cargoId)
        {
            try
            {
                if (string.IsNullOrEmpty(cedula) || string.IsNullOrEmpty(nombres) || string.IsNullOrEmpty(apellidos) || string.IsNullOrEmpty(sexo))
                {
                    return "La cedula, los nombres, los apellidos y el sexo son obligatorios ...";
                }

                if (BuscarCedula(cedula) != null)
                {
                    return "Ya existe un empleado con esa cedula ...";
                }

                Cargo cargo = cargoRepositorio.BuscarId(cargoId);

                if (cargo == null)
                {
                    return "Debe seleccionar un cargo valido ...";
                }

                Empleado empleado = new Empleado(cedula, nombres, apellidos, sexo, telefono, direccion, correoElectronico, cargo);
                repositorio.Guardar(empleado);

                return $"se registro el empleado con cedula {cedula} desde la logica ...";
            }
            catch (Exception ex)
            {
                return $"ocurrio un error al guardar: {ex.Message}";
            }
        }

        public Empleado BuscarCedula(string cedula)
        {
            return repositorio.BuscarCedula(cedula);
        }

        public List<Empleado> Consultar()
        {
            return repositorio.Consultar();
        }

        public string Eliminar(string cedula)
        {
            try
            {
                Empleado empleado = BuscarCedula(cedula);

                if (empleado == null)
                {
                    return "No se encontro el empleado ...";
                }

                repositorio.Eliminar(empleado);

                return "El empleado se elimino correctamente ...";
            }
            catch (Exception ex)
            {
                return $"Ocurrio un error al eliminar: {ex.Message}";
            }
        }
    }
}