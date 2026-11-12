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
                if (string.IsNullOrWhiteSpace(nombreCargo))
                {
                    return "El nombre del cargo es obligatorio y no puede estar vacio ni tener solo espacios ...";
                }
                if (nombreCargo != nombreCargo.Trim())
                {
                    return "El nombre del cargo no puede empezar ni terminar con espacios ...";
                }
                if (nombreCargo.Contains("  "))
                {
                    return "El nombre del cargo no puede tener espacios seguidos ...";
                }
                if (nombreCargo.Length < 3 || nombreCargo.Length > 100)
                {
                    return "El nombre del cargo debe tener entre 3 y 100 caracteres ...";
                }
                foreach (char c in nombreCargo)
                {
                    if (!char.IsLetter(c) && c != ' ')
                    {
                        return $"El nombre del cargo tiene el caracter '{c}' que no esta permitido, solo letras ...";
                    }
                }

                foreach (var existente in repositorio.Consultar())
                {
                    if (string.Equals(existente.NombreCargo, nombreCargo, StringComparison.OrdinalIgnoreCase))
                    {
                        return "Ya existe un cargo con ese nombre ...";
                    }
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