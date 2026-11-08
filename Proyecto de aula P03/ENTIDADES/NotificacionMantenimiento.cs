using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class NotificacionMantenimiento
    {
        public int NotificacionID { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentario { get; set; }
        public Vehiculo Vehiculo { get; set; }
        public Articulo Articulo { get; set; }

        public NotificacionMantenimiento()
        {

        }

        public override string ToString()
        {
            return $"|NotificacionID: {NotificacionID} |Fecha: {Fecha} |Comentario: {Comentario} " +
                $"|Vehiculo Placa: {Vehiculo.Placa} |Articulo: {Articulo.Nombre}";
        }
    }
}