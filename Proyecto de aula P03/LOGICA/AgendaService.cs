using DATOS;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGICA
{
    public class AgendaService
    {
        AgendaRepository repositorio;
        ClienteRepository clienteRepositorio;
        VehiculoRepository vehiculoRepositorio;
        EmpleadoRepository empleadoRepositorio;
        ArticuloRepository articuloRepositorio;

        public AgendaService(AgendaRepository repositorio, ClienteRepository clienteRepositorio, VehiculoRepository vehiculoRepositorio, EmpleadoRepository empleadoRepositorio, ArticuloRepository articuloRepositorio)
        {
            this.repositorio = repositorio;
            this.clienteRepositorio = clienteRepositorio;
            this.vehiculoRepositorio = vehiculoRepositorio;
            this.empleadoRepositorio = empleadoRepositorio;
            this.articuloRepositorio = articuloRepositorio;
        }

        public string Guardar(DateTime fecha, string comentario, string estado, string placa, string cedulaEmpleado, int servicioId)
        {
            return GuardarOActualizar(0, fecha, comentario, estado, placa, cedulaEmpleado, servicioId);
        }

        public string Modificar(int agendaId, DateTime fecha, string comentario, string estado, string placa, string cedulaEmpleado, int servicioId)
        {
            return GuardarOActualizar(agendaId, fecha, comentario, estado, placa, cedulaEmpleado, servicioId);
        }

        private string GuardarOActualizar(int agendaId, DateTime fecha, string comentario, string estado, string placa, string cedulaEmpleado, int servicioId)
        {
            try
            {
                Agenda actual = agendaId > 0 ? repositorio.BuscarId(agendaId) : null;
                if (agendaId > 0 && actual == null) return "No se encontró la cita seleccionada.";
                if (fecha < DateTime.Now.AddMinutes(-1) || fecha > DateTime.Now.AddYears(5))
                    return "Selecciona una fecha y hora válida, posterior al momento actual.";
                if (!string.IsNullOrWhiteSpace(comentario) && (comentario != comentario.Trim() || comentario.Length > 200 || comentario.Any(char.IsControl)))
                    return "La observación admite hasta 200 caracteres y no debe tener espacios sobrantes ni controles.";
                string estadoNormalizado = NormalizarEstado(estado);
                if (estadoNormalizado == null) return "El estado debe ser PENDIENTE, EN PROCESO, COMPLETADA o CANCELADA.";
                if (string.IsNullOrWhiteSpace(placa) || string.IsNullOrWhiteSpace(cedulaEmpleado) || servicioId <= 0)
                    return "Selecciona un vehículo, un empleado y un servicio.";

                Vehiculo vehiculo = vehiculoRepositorio.BuscarPlaca(placa.Trim());
                Empleado empleado = empleadoRepositorio.BuscarCedula(cedulaEmpleado.Trim());
                Servicio servicio = articuloRepositorio.BuscarId(servicioId) as Servicio;
                if (vehiculo == null) return "El vehículo seleccionado ya no existe.";
                if (empleado == null) return "El empleado seleccionado ya no existe.";
                if (servicio == null) return "El artículo seleccionado no es un servicio válido.";

                // La función Oracle consulta la disponibilidad exacta de fecha y hora.
                // También se revisa el intervalo real según la duración del servicio.
                bool disponible = repositorio.EmpleadoDisponible(empleado.Cedula, fecha);
                List<Agenda> citas = repositorio.Consultar();
                TimeSpan duracionNueva = TimeSpan.FromMinutes(Math.Max(1, servicio.Duracion));
                bool empleadoOcupado = citas.Any(a => a.AgendaID != agendaId && a.Estado != "CANCELADA" &&
                    a.Empleado.Cedula == empleado.Cedula && SeCruzan(fecha, duracionNueva, a.Fecha,
                        TimeSpan.FromMinutes(Math.Max(1, a.ArticuloServicio is Servicio s ? s.Duracion : 1))));
                if (empleadoOcupado || (!disponible && actual == null && citas.Any(a => a.Estado != "CANCELADA" &&
                    a.Empleado.Cedula == empleado.Cedula && a.Fecha == fecha)))
                    return "El empleado ya tiene una cita que se cruza con ese horario. Elige otro horario o empleado.";

                bool vehiculoOcupado = citas.Any(a => a.AgendaID != agendaId && a.Estado != "CANCELADA" &&
                    a.Vehiculo.Placa == vehiculo.Placa && SeCruzan(fecha, duracionNueva, a.Fecha,
                        TimeSpan.FromMinutes(Math.Max(1, a.ArticuloServicio is Servicio servicioExistente ? servicioExistente.Duracion : 1))));
                if (vehiculoOcupado) return "El vehículo ya tiene una cita a esa fecha y hora.";

                var agenda = new Agenda
                {
                    AgendaID = agendaId,
                    Fecha = fecha,
                    Comentario = string.IsNullOrWhiteSpace(comentario) ? null : comentario.Trim(),
                    Estado = estadoNormalizado,
                    Vehiculo = vehiculo,
                    Empleado = empleado,
                    ArticuloServicio = servicio
                };
                if (actual == null)
                {
                    repositorio.Guardar(agenda);
                    return "Se registró la cita número " + agenda.AgendaID + ".";
                }
                repositorio.Modificar(agenda);
                return "La cita se modificó correctamente.";
            }
            catch (Exception ex)
            {
                return "No se pudo guardar la cita: " + ex.Message;
            }
        }

        public string CambiarEstado(int agendaId, string estado)
        {
            try
            {
                Agenda agenda = repositorio.BuscarId(agendaId);
                if (agenda == null) return "No se encontró la cita.";
                string normalizado = NormalizarEstado(estado);
                if (normalizado == null) return "Selecciona un estado válido.";
                agenda.Estado = normalizado;
                repositorio.Modificar(agenda);
                return "El estado de la cita se actualizó.";
            }
            catch (Exception ex) { return "No se pudo actualizar el estado: " + ex.Message; }
        }

        private static string NormalizarEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado)) return null;
            string[] estados = { "PENDIENTE", "EN PROCESO", "COMPLETADA", "CANCELADA" };
            return estados.FirstOrDefault(e => string.Equals(e, estado.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        private static bool SeCruzan(DateTime inicioA, TimeSpan duracionA, DateTime inicioB, TimeSpan duracionB)
        {
            return inicioA < inicioB.Add(duracionB) && inicioB < inicioA.Add(duracionA);
        }

        public string Guardar(DateTime fecha, string comentario, string nombreServicio, string estado, string cedulaCliente, string placa, string cedulaEmpleado)
        {
            try
            {
                // Fecha
                if (fecha.Year < 2000 || fecha > DateTime.Now.AddYears(5))
                {
                    return "La fecha de la agenda no es valida ...";
                }
                if (fecha < DateTime.Now)
                {
                    return "La fecha y hora de la agenda no pueden ser anteriores a este momento ...";
                }

                // Nombre del servicio
                if (string.IsNullOrWhiteSpace(nombreServicio))
                {
                    return "El nombre del servicio es obligatorio y no puede estar vacio ni tener solo espacios ...";
                }
                if (nombreServicio != nombreServicio.Trim())
                {
                    return "El nombre del servicio no puede empezar ni terminar con espacios ...";
                }
                if (nombreServicio.Contains("  "))
                {
                    return "El nombre del servicio no puede tener espacios seguidos ...";
                }
                if (nombreServicio.Length < 3 || nombreServicio.Length > 100)
                {
                    return "El nombre del servicio debe tener entre 3 y 100 caracteres ...";
                }
                foreach (char c in nombreServicio)
                {
                    if (!char.IsLetterOrDigit(c) && c != ' ' && "-./".IndexOf(c) < 0)
                    {
                        return $"El nombre del servicio tiene el caracter '{c}' que no esta permitido ...";
                    }
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

                // Estado
                if (estado != "Pendiente" && estado != "En proceso" && estado != "Completada" && estado != "Cancelada")
                {
                    return "El estado debe ser uno de estos: Pendiente, En proceso, Completada, Cancelada ...";
                }

                // Cliente, vehiculo y empleado
                if (string.IsNullOrWhiteSpace(cedulaCliente))
                {
                    return "Debe seleccionar un cliente valido ...";
                }
                if (string.IsNullOrWhiteSpace(placa))
                {
                    return "Debe seleccionar un vehiculo valido ...";
                }
                if (string.IsNullOrWhiteSpace(cedulaEmpleado))
                {
                    return "Debe seleccionar un empleado valido ...";
                }

                Cliente cliente = clienteRepositorio.BuscarCedula(cedulaCliente);

                if (cliente == null)
                {
                    return "El cliente seleccionado no existe ...";
                }

                Vehiculo vehiculo = vehiculoRepositorio.BuscarPlaca(placa);

                if (vehiculo == null)
                {
                    return "El vehiculo seleccionado no existe ...";
                }

                if (vehiculo.Cliente == null || vehiculo.Cliente.Cedula != cedulaCliente)
                {
                    return "Ese vehiculo no pertenece al cliente seleccionado ...";
                }

                Empleado empleado = empleadoRepositorio.BuscarCedula(cedulaEmpleado);

                if (empleado == null)
                {
                    return "El empleado seleccionado no existe ...";
                }

                Servicio servicio = articuloRepositorio.Consultar()
                    .OfType<Servicio>()
                    .FirstOrDefault(a => string.Equals(a.Nombre, nombreServicio, StringComparison.OrdinalIgnoreCase));
                if (servicio == null)
                {
                    return "El servicio seleccionado no existe ...";
                }

                // La función Oracle centraliza la validación de cruces del empleado.
                if (!repositorio.EmpleadoDisponible(cedulaEmpleado, fecha))
                    return "Ese empleado ya tiene una cita en ese horario ...";

                foreach (var existente in repositorio.Consultar())
                    if (existente.Estado != "Cancelada" && existente.Fecha == fecha && existente.Vehiculo.Placa == placa)
                        return "Ese vehiculo ya tiene una cita en esa fecha y hora ...";

                Agenda agenda = new Agenda();
                agenda.Fecha = fecha;
                agenda.Comentario = comentario;
                agenda.Estado = estado;
                agenda.Vehiculo = vehiculo;
                agenda.Empleado = empleado;
                agenda.ArticuloServicio = servicio;

                repositorio.Guardar(agenda);

                return $"se registro la agenda numero {agenda.AgendaID} desde la logica ...";
            }
            catch (Exception ex)
            {
                return $"ocurrio un error al guardar: {ex.Message}";
            }
        }

        public Agenda BuscarId(int agendaId)
        {
            return repositorio.BuscarId(agendaId);
        }

        public List<Agenda> Consultar()
        {
            return repositorio.Consultar();
        }

        public string Eliminar(int agendaId)
        {
            try
            {
                Agenda agenda = BuscarId(agendaId);

                if (agenda == null)
                {
                    return "No se encontro la agenda ...";
                }

                repositorio.Eliminar(agenda);

                return "La agenda se elimino correctamente ...";
            }
            catch (Exception ex)
            {
                return $"Ocurrio un error al eliminar: {ex.Message}";
            }
        }
    }
}
