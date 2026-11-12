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
        int siguienteAgendaId;

        public AgendaService(AgendaRepository repositorio, ClienteRepository clienteRepositorio, VehiculoRepository vehiculoRepositorio, EmpleadoRepository empleadoRepositorio)
        {
            this.repositorio = repositorio;
            this.clienteRepositorio = clienteRepositorio;
            this.vehiculoRepositorio = vehiculoRepositorio;
            this.empleadoRepositorio = empleadoRepositorio;
            siguienteAgendaId = 1;
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

                // Cruces de horario
                foreach (var existente in repositorio.Consultar())
                {
                    if (existente.Estado != "Cancelada" && existente.Fecha == fecha)
                    {
                        if (existente.Empleado.Cedula == cedulaEmpleado)
                        {
                            return "Ese empleado ya tiene una cita en esa fecha y hora ...";
                        }
                        if (existente.Vehiculo.Placa == placa)
                        {
                            return "Ese vehiculo ya tiene una cita en esa fecha y hora ...";
                        }
                    }
                }

                int agendaId = GenerarAgendaId();

                Agenda agenda = new Agenda();
                agenda.AgendaID = agendaId;
                agenda.Fecha = fecha;
                agenda.Comentario = comentario;
                agenda.NombreServicio = nombreServicio;
                agenda.Estado = estado;
                agenda.Cliente = cliente;
                agenda.Vehiculo = vehiculo;
                agenda.Empleado = empleado;

                repositorio.Guardar(agenda);

                return $"se registro la agenda numero {agendaId} desde la logica ...";
            }
            catch (Exception ex)
            {
                return $"ocurrio un error al guardar: {ex.Message}";
            }
        }

        private int GenerarAgendaId()
        {
            int numero = siguienteAgendaId;

            while (BuscarId(numero) != null)
            {
                numero++;
            }

            siguienteAgendaId = numero + 1;
            return numero;
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