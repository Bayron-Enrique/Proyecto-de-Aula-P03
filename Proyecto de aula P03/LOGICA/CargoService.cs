using DATOS;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGICA
{
    public class CargoService
    {
        CargoRepository repositorio;
        int siguienteCargoId;

        public CargoService(CargoRepository repositorio)
        {
            this.repositorio = repositorio;
            siguienteCargoId = 1;
        }

        public string Guardar(string nombreCargo)
        {
            try
            {
                if (string.IsNullOrEmpty(nombreCargo))
                {
                    return "El nombre del cargo es obligatorio ...";
                }

                int cargoId = GenerarCargoId();

                Cargo cargo = new Cargo(cargoId, nombreCargo);
                repositorio.Guardar(cargo);

                return $"se registro el cargo numero {cargoId} desde la logica ...";
            }
            catch (Exception ex)
            {
                return $"ocurrio un error al guardar: {ex.Message}";
            }
        }

        private int GenerarCargoId()
        {
            int numero = siguienteCargoId;

            while (BuscarId(numero) != null)
            {
                numero++;
            }

            siguienteCargoId = numero + 1;
            return numero;
        }

        public Cargo BuscarId(int cargoId)
        {
            return repositorio.BuscarId(cargoId);
        }

        public List<Cargo> Consultar()
        {
            return repositorio.Consultar();
        }

        public string Eliminar(int cargoId)
        {
            try
            {
                Cargo cargo = BuscarId(cargoId);

                if (cargo == null)
                {
                    return "No se encontro el cargo ...";
                }

                repositorio.Eliminar(cargo);

                return "El cargo se elimino correctamente ...";
            }
            catch (Exception ex)
            {
                return $"Ocurrio un error al eliminar: {ex.Message}";
            }
        }
    }
}