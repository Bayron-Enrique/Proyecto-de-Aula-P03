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
                if (string.IsNullOrEmpty(estado))
                {
                    return "El estado de la agenda es obligatorio ...";
                }

                Cliente cliente = clienteRepositorio.BuscarCedula(cedulaCliente);

                if (cliente == null)
                {
                    return "Debe seleccionar un cliente valido ...";
                }

                Vehiculo vehiculo = vehiculoRepositorio.BuscarPlaca(placa);

                if (vehiculo == null)
                {
                    return "Debe seleccionar un vehiculo valido ...";
                }

                Empleado empleado = empleadoRepositorio.BuscarCedula(cedulaEmpleado);

                if (empleado == null)
                {
                    return "Debe seleccionar un empleado valido ...";
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