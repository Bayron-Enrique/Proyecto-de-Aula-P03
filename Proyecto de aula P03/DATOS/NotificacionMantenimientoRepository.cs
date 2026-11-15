using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class NotificacionMantenimientoRepository : IRepository<NotificacionMantenimiento>
    {
        List<NotificacionMantenimiento> notificaciones;

        public NotificacionMantenimientoRepository()
        {
            notificaciones = new List<NotificacionMantenimiento>();
        }

        public void Guardar(NotificacionMantenimiento notificacion)
        {
            notificaciones.Add(notificacion);
        }

        public List<NotificacionMantenimiento> Consultar()
        {
            return notificaciones;
        }

        public NotificacionMantenimiento BuscarId(int notificacionId)
        {
            foreach (var notificacion in notificaciones)
            {
                if (notificacion.NotificacionID == notificacionId)
                {
                    return notificacion;
                }
            }
            return null;
        }

        public void Eliminar(NotificacionMantenimiento notificacion)
        {
            notificaciones.Remove(notificacion);
        }
        public void Modificar(NotificacionMantenimiento entidad)
        {
            throw new NotImplementedException();
        }
    }
}
