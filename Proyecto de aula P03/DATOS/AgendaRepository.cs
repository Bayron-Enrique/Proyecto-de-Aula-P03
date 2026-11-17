using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class AgendaRepository : ManejadorConexion, IRepository<Agenda>
    {
        public bool EmpleadoDisponible(string cedulaEmpleado, DateTime fecha)
        {
            const string sql = "SELECT TALLER.FN_VALIDAR_DISPONIBILIDAD_EMPLEADO(:empleadoCedula, :fecha) FROM DUAL";
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(sql, con))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("empleadoCedula", cedulaEmpleado);
                cmd.Parameters.Add("fecha", fecha);
                con.Open();
                object resultado = cmd.ExecuteScalar();
                return resultado != null && resultado != DBNull.Value && Convert.ToInt32(resultado) == 1;
            }
        }

        private const string SelectAgenda = @"SELECT A.AGENDAID, A.FECHA, A.COMENTARIO, A.VEHICULO_PLACA,
                                      A.EMPLEADO_CEDULA, A.ESTADO, A.ARTICULO_ARTICULOID,
                                      VP.NOMBRES AS CLIENTE_NOMBRES, VP.APELLIDOS AS CLIENTE_APELLIDOS,
                                      EP.NOMBRES AS EMPLEADO_NOMBRES, EP.APELLIDOS AS EMPLEADO_APELLIDOS,
                                      C.CARGOID, C.NOMBRECARGO, AR.NOMBRE AS NOMBRE_ARTICULO, AR.DURACION AS DURACION_SERVICIO
                                      FROM TALLER.AGENDA A
                                      JOIN TALLER.VEHICULO V ON V.PLACA = A.VEHICULO_PLACA
                                      JOIN TALLER.CLIENTE CL ON CL.CEDULA = V.CLIENTE_CEDULA
                                      JOIN TALLER.PERSONA VP ON VP.CEDULA = CL.CEDULA
                                      JOIN TALLER.EMPLEADO E ON E.CEDULA = A.EMPLEADO_CEDULA
                                      JOIN TALLER.PERSONA EP ON EP.CEDULA = E.CEDULA
                                      JOIN TALLER.CARGO C ON C.CARGOID = E.CARGO_CARGOID
                                      LEFT JOIN TALLER.ARTICULO AR ON AR.ARTICULOID = A.ARTICULO_ARTICULOID";

        public void Guardar(Agenda agenda)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                con.Open();
                using (OracleCommand secuencia = new OracleCommand("SELECT TALLER.SEQ_AGENDAID.NEXTVAL FROM DUAL", con))
                    agenda.AgendaID = Convert.ToInt32(secuencia.ExecuteScalar());
                string sql = @"INSERT INTO TALLER.AGENDA
                                 (AGENDAID, FECHA, COMENTARIO, VEHICULO_PLACA, EMPLEADO_CEDULA, ESTADO, ARTICULO_ARTICULOID)
                                 VALUES (:agendaId, :fecha, :comentario, :vehiculoPlaca, :empleadoCedula, :estado, :articuloId)";
                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    AgregarParams(cmd, agenda);
                    cmd.Parameters.Add("agendaId", agenda.AgendaID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private void AgregarParams(OracleCommand cmd, Agenda agenda)
        {
            cmd.BindByName = true;

            cmd.Parameters.Add(new OracleParameter("fecha", agenda.Fecha));
            cmd.Parameters.Add(new OracleParameter("comentario", string.IsNullOrEmpty(agenda.Comentario) ? (object)DBNull.Value : agenda.Comentario));
            cmd.Parameters.Add(new OracleParameter("vehiculoPlaca", agenda.Vehiculo.Placa));
            cmd.Parameters.Add(new OracleParameter("empleadoCedula", agenda.Empleado.Cedula));
            cmd.Parameters.Add(new OracleParameter("estado", agenda.Estado));

            if (agenda.ArticuloServicio != null)
            {
                cmd.Parameters.Add(new OracleParameter("articuloId", agenda.ArticuloServicio.ArticuloId));
            }
            else
            {
                cmd.Parameters.Add(new OracleParameter("articuloId", DBNull.Value));
            }
        }

        public List<Agenda> Consultar()
        {
            List<Agenda> agendas = new List<Agenda>();
            using (OracleConnection con = ObtenerConexion())
            {
                using (OracleCommand cmd = new OracleCommand(SelectAgenda, con))
                {
                    con.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            agendas.Add(Mapear(reader));
                        }
                    }
                }
            }
            return agendas;
        }

        private Agenda Mapear(OracleDataReader reader)
        {
            return new Agenda
            {
                AgendaID = Convert.ToInt32(reader["AGENDAID"]),
                Fecha = Convert.ToDateTime(reader["FECHA"]),
                Comentario = reader["COMENTARIO"] != DBNull.Value ? reader["COMENTARIO"].ToString() : string.Empty,
                Estado = reader["ESTADO"].ToString(),

                Vehiculo = new Vehiculo
                {
                    Placa = reader["VEHICULO_PLACA"].ToString().Trim(),
                    Cliente = new Cliente
                    {
                        Nombre = reader["CLIENTE_NOMBRES"].ToString(),
                        Apellido = reader["CLIENTE_APELLIDOS"].ToString()
                    }
                },
                Empleado = new Empleado
                {
                    Cedula = reader["EMPLEADO_CEDULA"].ToString().Trim(),
                    Nombre = reader["EMPLEADO_NOMBRES"].ToString(),
                    Apellido = reader["EMPLEADO_APELLIDOS"].ToString(),
                    Cargo = new Cargo
                    {
                        CargoId = Convert.ToInt32(reader["CARGOID"]),
                        NombreCargo = reader["NOMBRECARGO"].ToString()
                    }
                },
                ArticuloServicio = reader["ARTICULO_ARTICULOID"] != DBNull.Value
                    ? new Servicio
                    {
                        ArticuloId = Convert.ToInt32(reader["ARTICULO_ARTICULOID"]),
                        Nombre = reader["NOMBRE_ARTICULO"].ToString(),
                        Duracion = reader["DURACION_SERVICIO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DURACION_SERVICIO"])
                    }
                    : null
            };
        }

        public void Modificar(Agenda agenda)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = @"UPDATE TALLER.AGENDA SET
                         FECHA = :fecha, 
                         COMENTARIO = :comentario, 
                         VEHICULO_PLACA = :vehiculoPlaca, 
                         EMPLEADO_CEDULA = :empleadoCedula, 
                         ESTADO = :estado, 
                         ARTICULO_ARTICULOID = :articuloId 
                       WHERE AGENDAID = :agendaId";

                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    AgregarParams(cmd, agenda);
                    cmd.Parameters.Add(new OracleParameter("agendaId", agenda.AgendaID));

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Agenda BuscarId(int agendaId)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = SelectAgenda + " WHERE A.AGENDAID = :agendaId";

                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    cmd.Parameters.Add(new OracleParameter("agendaId", agendaId));
                    con.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return Mapear(reader);
                        }
                    }
                }
            }

            return null;
        }

        public void Eliminar(Agenda agenda)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = @"DELETE FROM TALLER.AGENDA WHERE AGENDAID = :agendaId";

                using (OracleCommand cmd = new OracleCommand(sql,con))
                {
                    cmd.Parameters.Add(new OracleParameter("agendaId", agenda.AgendaID));
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
