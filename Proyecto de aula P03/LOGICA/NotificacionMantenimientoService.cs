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

        public NotificacionMantenimientoService(NotificacionMantenimientoRepository repositorio, VehiculoRepository vehiculoRepositorio, ArticuloRepository articuloRepositorio)
        {
            this.repositorio = repositorio;
            this.vehiculoRepositorio = vehiculoRepositorio;
            this.articuloRepositorio = articuloRepositorio;
        }

        public string Guardar(DateTime fecha, string comentario, string placa, int articuloId)
        {
            return Guardar(fecha, comentario, "PENDIENTE", placa, articuloId);
        }

        public string Guardar(DateTime fecha, string comentario, string estado, string placa, int articuloId)
        {
            return GuardarOActualizar(0, fecha, comentario, estado, placa, articuloId);
        }

        public string Modificar(int notificacionId, DateTime fecha, string comentario, string estado, string placa, int articuloId)
        {
            return GuardarOActualizar(notificacionId, fecha, comentario, estado, placa, articuloId);
        }

        private string GuardarOActualizar(int id, DateTime fecha, string comentario, string estado, string placa, int articuloId)
        {
            try
            {
                NotificacionMantenimiento actual = id > 0 ? repositorio.BuscarId(id) : null;
                if (id > 0 && actual == null) return "No se encontró la notificación seleccionada.";
                if (fecha.Year < 2000 || fecha > DateTime.Now.AddYears(5)) return "La fecha programada debe ser válida y no superar cinco años.";
                if (!string.IsNullOrWhiteSpace(comentario) && (comentario != comentario.Trim() || comentario.Length > 200 || comentario.Any(char.IsControl)))
                    return "La observación admite hasta 200 caracteres y no debe tener espacios sobrantes ni controles.";
                string estadoNormalizado = NormalizarEstado(estado);
                if (estadoNormalizado == null) return "El estado debe ser PENDIENTE, ENVIADA, COMPLETADA o CANCELADA.";
                if (string.IsNullOrWhiteSpace(placa) || articuloId <= 0) return "Selecciona un vehículo y un artículo o servicio.";
                Vehiculo vehiculo = vehiculoRepositorio.BuscarPlaca(placa);
                if (vehiculo == null) return "El vehículo seleccionado ya no existe.";
                Articulo articulo = articuloRepositorio.BuscarId(articuloId);
                if (articulo == null) return "El artículo seleccionado ya no existe.";
                bool duplicado = repositorio.Consultar().Any(n => n.NotificacionID != id && n.Vehiculo.Placa == vehiculo.Placa &&
                    n.Articulo.ArticuloId == articuloId && n.Fecha.Date == fecha.Date);
                if (duplicado) return "Ya existe una notificación para ese vehículo y artículo en esa fecha.";

                var notificacion = new NotificacionMantenimiento
                {
                    NotificacionID = id,
                    Fecha = fecha,
                    Comentario = string.IsNullOrWhiteSpace(comentario) ? null : comentario.Trim(),
                    Estado = estadoNormalizado,
                    Vehiculo = vehiculo,
                    Articulo = articulo
                };
                if (actual == null)
                {
                    repositorio.Guardar(notificacion);
                    return "Se registró la notificación número " + notificacion.NotificacionID + ".";
                }
                repositorio.Modificar(notificacion);
                return "La notificación se modificó correctamente.";
            }
            catch (Exception ex) { return "No se pudo guardar la notificación: " + ex.Message; }
        }

        public string CambiarEstado(int notificacionId, string estado)
        {
            try
            {
                NotificacionMantenimiento notificacion = repositorio.BuscarId(notificacionId);
                if (notificacion == null) return "No se encontró la notificación.";
                string normalizado = NormalizarEstado(estado);
                if (normalizado == null) return "Selecciona un estado válido.";
                notificacion.Estado = normalizado;
                repositorio.Modificar(notificacion);
                return "El estado de la notificación se actualizó.";
            }
            catch (Exception ex) { return "No se pudo actualizar el estado: " + ex.Message; }
        }

        private static string NormalizarEstado(string estado)
        {
            string[] estados = { "PENDIENTE", "ENVIADA", "COMPLETADA", "CANCELADA" };
            return estados.FirstOrDefault(e => string.Equals(e, estado == null ? null : estado.Trim(), StringComparison.OrdinalIgnoreCase));
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
