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
                // Fecha
                if (fecha.Year < 2000 || fecha > DateTime.Now.AddYears(5))
                {
                    return "La fecha de la notificacion no es valida ...";
                }

                // Comentario (opcional)
                if (!string.IsNullOrEmpty(comentario))
                {
                    if (string.IsNullOrWhiteSpace(comentario))
                    {
                        return "El comentario no puede tener solo espacios ...";
                    }
                    if (comentario != comentario.Trim() || comentario.Contains("  "))
                    {
                        return "El comentario no puede tener espacios al inicio, al final ni seguidos ...";
                    }
                    if (comentario.Length < 3 || comentario.Length > 200)
                    {
                        return "El comentario debe tener entre 3 y 200 caracteres ...";
                    }
                    foreach (char c in comentario)
                    {
                        if (char.IsControl(c))
                        {
                            return "El comentario tiene caracteres no permitidos ...";
                        }
                    }
                }

                // Vehiculo y articulo
                if (string.IsNullOrWhiteSpace(placa))
                {
                    return "Debe seleccionar un vehiculo valido ...";
                }
                if (articuloId <= 0)
                {
                    return "Debe seleccionar un articulo valido ...";
                }

                Vehiculo vehiculo = vehiculoRepositorio.BuscarPlaca(placa);

                if (vehiculo == null)
                {
                    return "El vehiculo seleccionado no existe ...";
                }

                Articulo articulo = articuloRepositorio.BuscarId(articuloId);

                if (articulo == null)
                {
                    return "El articulo seleccionado no existe ...";
                }

                // Duplicados
                foreach (var existente in repositorio.Consultar())
                {
                    if (existente.Vehiculo.Placa == placa && existente.Articulo.ArticuloId == articuloId && existente.Fecha.Date == fecha.Date)
                    {
                        return "Ya existe una notificacion de ese articulo para ese vehiculo en esa fecha ...";
                    }
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