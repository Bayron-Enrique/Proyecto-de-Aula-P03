using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Agenda
    {
        public int AgendaID { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentario { get; set; }
        public Empleado Empleado { get; set; }
        public Vehiculo Vehiculo { get; set; }
        public string NombreServicio { get; set; }

        public Agenda()
        {
            
        }

        public override string ToString()
        {
            return $"|AgendaID: {AgendaID} |Fecha: {Fecha} |Comentario: {Comentario} |Empleado Nombre: {Empleado.Nombre} " +
                $"|Vehiculo Placa: {Vehiculo.Placa} |Nombre Servicio: {NombreServicio}";
        }
    }
}
