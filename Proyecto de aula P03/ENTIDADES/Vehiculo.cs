using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Vehiculo
    {
        public string Placa { get; set; }
        public int Modelo { get; set; }
        public string Marca { get; set; }
        public string Color { get; set; }
        public string Linea { get; set; }
        public string TipoVehiculo { get; set; }
        public string ClienteVehiculo { get; set; }

        public Vehiculo()
        {
            
        }

        public Vehiculo(string placa, int modelo, string marca, string color, string linea, string tipoVehiculo, string clienteVehiculo)
        {
            Placa = placa;
            Modelo = modelo;
            Marca = marca;
            Color = color;
            Linea = linea;
            TipoVehiculo = tipoVehiculo;
            ClienteVehiculo = clienteVehiculo;
        }

        public override string ToString()
        {
            return $"| Placa: {Placa} | Tipo: {TipoVehiculo} | Marca: {Marca} {Linea} ({Modelo}) | Color: {Color} | Cédula Dueño: {ClienteVehiculo} |";
        }
    }
}
