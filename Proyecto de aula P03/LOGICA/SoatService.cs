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
                // Numero del SOAT
                if (string.IsNullOrWhiteSpace(numeroSoat))
                {
                    return "El numero del SOAT es obligatorio y no puede tener espacios ...";
                }
                foreach (char c in numeroSoat)
                {
                    if (c < '0' || c > '9')
                    {
                        return "El numero del SOAT solo puede tener numeros, sin espacios ni signos ...";
                    }
                }
                if (numeroSoat.Length < 5 || numeroSoat.Length > 20)
                {
                    return "El numero del SOAT debe tener entre 5 y 20 digitos ...";
                }

                // Fechas
                if (fechaInicio.Year < 2000 || fechaInicio > DateTime.Now.AddYears(5))
                {
                    return "La fecha de inicio no es valida ...";
                }
                if (fechaFin.Year < 2000 || fechaFin > DateTime.Now.AddYears(5))
                {
                    return "La fecha fin no es valida ...";
                }
                if (fechaFin <= fechaInicio)
                {
                    return "La fecha fin debe ser posterior a la fecha de inicio ...";
                }
                if (fechaFin > fechaInicio.AddYears(1))
                {
                    return "El SOAT no puede durar mas de un año ...";
                }

                // Vehiculo
                if (string.IsNullOrWhiteSpace(placa))
                {
                    return "Debe seleccionar un vehiculo valido ...";
                }

                Vehiculo vehiculo = vehiculoRepositorio.BuscarPlaca(placa);

                if (vehiculo == null)
                {
                    return "El vehiculo seleccionado no existe ...";
                }

                // Duplicados y cruces
                foreach (var existente in repositorio.Consultar())
                {
                    if (existente.NumeroSoat == numeroSoat)
                    {
                        return "Ya existe un SOAT con ese numero ...";
                    }
                    if (existente.Vehiculo.Placa == placa && fechaInicio <= existente.FechaFin && fechaFin >= existente.FechaInicio)
                    {
                        return "Ese vehiculo ya tiene un SOAT que se cruza con esas fechas ...";
                    }
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