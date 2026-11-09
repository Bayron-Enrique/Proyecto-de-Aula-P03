using DATOS;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGICA
{
    public class SoatService
    {
        SoatRepository repositorio;
        VehiculoRepository vehiculoRepositorio;
        int siguienteSoatId;

        public SoatService(SoatRepository repositorio, VehiculoRepository vehiculoRepositorio)
        {
            this.repositorio = repositorio;
            this.vehiculoRepositorio = vehiculoRepositorio;
            siguienteSoatId = 1;
        }

        public string Guardar(string numeroSoat, DateTime fechaInicio, DateTime fechaFin, string placa)
        {
            try
            {
                if (string.IsNullOrEmpty(numeroSoat))
                {
                    return "El numero del SOAT es obligatorio ...";
                }
                if (fechaFin <= fechaInicio)
                {
                    return "La fecha fin debe ser posterior a la fecha de inicio ...";
                }

                Vehiculo vehiculo = vehiculoRepositorio.BuscarPlaca(placa);

                if (vehiculo == null)
                {
                    return "Debe seleccionar un vehiculo valido ...";
                }

                int soatId = GenerarSoatId();

                Soat soat = new Soat(soatId, numeroSoat, fechaInicio, fechaFin, vehiculo);
                repositorio.Guardar(soat);

                return $"se registro el SOAT numero {soatId} desde la logica ...";
            }
            catch (Exception ex)
            {
                return $"ocurrio un error al guardar: {ex.Message}";
            }
        }

        private int GenerarSoatId()
        {
            int numero = siguienteSoatId;

            while (BuscarId(numero) != null)
            {
                numero++;
            }

            siguienteSoatId = numero + 1;
            return numero;
        }

        public Soat BuscarId(int soatId)
        {
            return repositorio.BuscarId(soatId);
        }

        public List<Soat> Consultar()
        {
            return repositorio.Consultar();
        }

        public string Eliminar(int soatId)
        {
            try
            {
                Soat soat = BuscarId(soatId);

                if (soat == null)
                {
                    return "No se encontro el SOAT ...";
                }

                repositorio.Eliminar(soat);

                return "El SOAT se elimino correctamente ...";
            }
            catch (Exception ex)
            {
                return $"Ocurrio un error al eliminar: {ex.Message}";
            }
        }
    }
}