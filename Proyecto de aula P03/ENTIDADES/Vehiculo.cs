using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Vehiculo
    {
        public Vehiculo(string placa, int modelo, string marca, string tipoVehiculo, Cliente cliente)
        {
            if (cliente == null)
            {
                throw new NullReferenceException("EL CLIENTE NO PUEDE SER NULO");
            }

            Placa = placa;
            Modelo = modelo;
            Marca = marca;
            TipoVehiculo = tipoVehiculo;
            Cliente = cliente;
        }

        public string Placa { get; set; }
        public int Modelo { get; set; }
        public string Marca { get; set; }
        public string TipoVehiculo { get; set; }
        public Cliente Cliente { get; set; }
    }
}