using DATOS;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGICA
{
    public class NotificacionMantenimientoService
    {
        NotificacionMantenimientoRepository repositorio;
        VehiculoRepository vehiculoRepositorio;
        ArticuloRepository articuloRepositorio;
        int siguienteNotificacionId;

        public NotificacionMantenimientoService(NotificacionMantenimientoRepository repositorio, VehiculoRepository vehiculoRepositorio, ArticuloRepository articuloRepositorio)
        {
            this.repositorio = repositorio;
            this.vehiculoRepositorio = vehiculoRepositorio;
            this.articuloRepositorio = articuloRepositorio;
            siguienteNotificacionId = 1;
        }

        public string Guardar(DateTime fecha, string comentario, string placa, int articuloId)
        {
            try
            {
                Vehiculo vehiculo = vehiculoRepositorio.BuscarPlaca(placa);

                if (vehiculo == null)
                {
                    return "Debe seleccionar un vehiculo valido ...";
                }

                Articulo articulo = articuloRepositorio.BuscarId(articuloId);

                if (articulo == null)
                {
                    return "Debe seleccionar un articulo valido ...";
                }

                int notificacionId = GenerarNotificacionId();

                NotificacionMantenimiento notificacion = new NotificacionMantenimiento();
                notificacion.NotificacionID = notificacionId;
                notificacion.Fecha = fecha;
                notificacion.Comentario = comentario;
                notificacion.Vehiculo = vehiculo;
                notificacion.Articulo = articulo;

                repositorio.Guardar(notificacion);

                return $"se registro la notificacion numero {notificacionId} desde la logica ...";
            }
            catch (Exception ex)
            {
                return $"ocurrio un error al guardar: {ex.Message}";
            }
        }

        private int GenerarNotificacionId()
        {
            int numero = siguienteNotificacionId;

            while (BuscarId(numero) != null)
            {
                numero++;
            }

            siguienteNotificacionId = numero + 1;
            return numero;
        }

        public NotificacionMantenimiento BuscarId(int notificacionId)
        {
            return repositorio.BuscarId(notificacionId);
        }

        public List<NotificacionMantenimiento> Consultar()
        {
            return repositorio.Consultar();
        }

        public string Eliminar(int notificacionId)
        {
            try
            {
                NotificacionMantenimiento notificacion = BuscarId(notificacionId);

                if (notificacion == null)
                {
                    return "No se encontro la notificacion ...";
                }

                repositorio.Eliminar(notificacion);

                return "La notificacion se elimino correctamente ...";
            }
            catch (Exception ex)
            {
                return $"Ocurrio un error al eliminar: {ex.Message}";
            }
        }
    }
}