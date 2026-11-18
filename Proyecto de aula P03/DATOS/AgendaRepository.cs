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
        public int ContarPendientes()
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("SELECT COUNT(*) FROM TALLER.AGENDA WHERE UPPER(ESTADO) = 'PENDIENTE'", con))
            {
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public bool EmpleadoDisponible(string cedulaEmpleado, DateTime fecha)
        {
            const string sql = "SELECT TALLER.FN_VALIDAR_DISPONIBILIDAD_EMPLEADO(:empleadoCedula, :fecha) FROM DUAL";
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(sql, con))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("empleadoCedula", OracleDbType.Char, 10).Value = cedulaEmpleado;
                cmd.Parameters.Add("fecha", OracleDbType.Date).Value = fecha;
                con.Open();
                object resultado = cmd.ExecuteScalar();
                return resultado != null && resultado != DBNull.Value && Convert.ToInt32(resultado) == 1;
            }
        }

        private const string SelectAgenda = @"SELECT A.AGENDAID, A.FECHA, A.COMENTARIO, A.VEHICULO_PLACA,
                                      A.EMPLEADO_CEDULA, A.ESTADO, A.ARTICULO_ARTICULOID,
                                      CL.CEDULA AS CLIENTE_CEDULA, VP.PRIMER_NOMBRE AS CLIENTE_PRIMER_NOMBRE, VP.SEGUNDO_NOMBRE AS CLIENTE_SEGUNDO_NOMBRE,
                                      VP.PRIMER_APELLIDO AS CLIENTE_PRIMER_APELLIDO, VP.SEGUNDO_APELLIDO AS CLIENTE_SEGUNDO_APELLIDO,
                                      EP.PRIMER_NOMBRE AS EMPLEADO_PRIMER_NOMBRE, EP.SEGUNDO_NOMBRE AS EMPLEADO_SEGUNDO_NOMBRE,
                                      EP.PRIMER_APELLIDO AS EMPLEADO_PRIMER_APELLIDO, EP.SEGUNDO_APELLIDO AS EMPLEADO_SEGUNDO_APELLIDO,
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
            Persistir(agenda, true);
        }
        private void AgregarParams(OracleCommand cmd, Agenda agenda)
        {
            cmd.BindByName = true;

            cmd.Parameters.Add("fecha", OracleDbType.Date).Value = agenda.Fecha;
            cmd.Parameters.Add(new OracleParameter("comentario", string.IsNullOrEmpty(agenda.Comentario) ? (object)DBNull.Value : agenda.Comentario));
            cmd.Parameters.Add("vehiculoPlaca", OracleDbType.Char, 7).Value = agenda.Vehiculo.Placa;
            cmd.Parameters.Add("empleadoCedula", OracleDbType.Char, 10).Value = agenda.Empleado.Cedula;
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
                Fecha = reader["FECHA"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FECHA"]),
                Comentario = reader["COMENTARIO"] != DBNull.Value ? reader["COMENTARIO"].ToString() : string.Empty,
                Estado = reader["ESTADO"].ToString(),

                Vehiculo = new Vehiculo
                {
                    Placa = reader["VEHICULO_PLACA"].ToString().Trim(),
                    Cliente = new Cliente
                    {
                        Cedula = reader["CLIENTE_CEDULA"].ToString().Trim(),
                        PrimerNombre = reader["CLIENTE_PRIMER_NOMBRE"].ToString(),
                        SegundoNombre = reader["CLIENTE_SEGUNDO_NOMBRE"] == DBNull.Value ? null : reader["CLIENTE_SEGUNDO_NOMBRE"].ToString(),
                        PrimerApellido = reader["CLIENTE_PRIMER_APELLIDO"].ToString(),
                        SegundoApellido = reader["CLIENTE_SEGUNDO_APELLIDO"] == DBNull.Value ? null : reader["CLIENTE_SEGUNDO_APELLIDO"].ToString()
                    }
                },
                Empleado = new Empleado
                {
                    Cedula = reader["EMPLEADO_CEDULA"].ToString().Trim(),
                    PrimerNombre = reader["EMPLEADO_PRIMER_NOMBRE"].ToString(),
                    SegundoNombre = reader["EMPLEADO_SEGUNDO_NOMBRE"] == DBNull.Value ? null : reader["EMPLEADO_SEGUNDO_NOMBRE"].ToString(),
                    PrimerApellido = reader["EMPLEADO_PRIMER_APELLIDO"].ToString(),
                    SegundoApellido = reader["EMPLEADO_SEGUNDO_APELLIDO"] == DBNull.Value ? null : reader["EMPLEADO_SEGUNDO_APELLIDO"].ToString(),
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
            Persistir(agenda, false);
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

        public void CambiarEstado(int agendaId, string estado)
        {
            Persistir(new Agenda { AgendaID = agendaId, Estado = estado }, false, true);
        }

        private void Persistir(Agenda agenda, bool nueva, bool soloEstado = false)
        {
            if (agenda == null || (!soloEstado && (agenda.Vehiculo == null || agenda.Empleado == null || agenda.ArticuloServicio == null)))
                throw new ArgumentException("La cita requiere vehículo, empleado y servicio.");
            agenda.Fecha = new DateTime(agenda.Fecha.Year, agenda.Fecha.Month, agenda.Fecha.Day, agenda.Fecha.Hour, agenda.Fecha.Minute, 0);
            using (OracleConnection con = ObtenerConexion())
            {
                con.Open();
                using (OracleTransaction tx = con.BeginTransaction())
                {
                    try
                    {
                        // Todas las altas, ediciones y reactivaciones se serializan antes de validar.
                        using (var bloqueo = new OracleCommand("LOCK TABLE TALLER.AGENDA IN SHARE ROW EXCLUSIVE MODE", con))
                        {
                            bloqueo.Transaction = tx;
                            bloqueo.ExecuteNonQuery();
                        }
                        if (soloEstado)
                        {
                            string estado = agenda.Estado;
                            using (var consultar = new OracleCommand(SelectAgenda + " WHERE A.AGENDAID = :id", con))
                            {
                                consultar.Transaction = tx;
                                consultar.Parameters.Add("id", agenda.AgendaID);
                                using (var reader = consultar.ExecuteReader())
                                {
                                    if (!reader.Read()) throw new InvalidOperationException("La cita ya no existe.");
                                    agenda = Mapear(reader);
                                }
                            }
                            agenda.Estado = estado;
                        }
                        if (agenda.Estado != "PENDIENTE" && agenda.Estado != "EN PROCESO" &&
                            agenda.Estado != "COMPLETADA" && agenda.Estado != "CANCELADA")
                            throw new ArgumentException("Estado de cita inválido.");
                        int duracion = 1;
                        if (agenda.Estado != "CANCELADA" && (agenda.Fecha == DateTime.MinValue || agenda.ArticuloServicio == null))
                            throw new InvalidOperationException("La cita necesita una fecha y un servicio válidos antes de activarla.");
                        if (agenda.Estado != "CANCELADA")
                        using (var servicio = new OracleCommand(@"SELECT A.DURACION FROM TALLER.ARTICULO A
                            JOIN TALLER.TIPO_ARTICULO T ON T.TIPO_ARTICULO_ID = A.TIPO_ARTICULO_ID
                            WHERE A.ARTICULOID = :id AND UPPER(T.NOMBRE) = 'SERVICIO'", con))
                        {
                            servicio.Transaction = tx;
                            servicio.Parameters.Add("id", agenda.ArticuloServicio.ArticuloId);
                            object valor = servicio.ExecuteScalar();
                            if (valor == null || valor == DBNull.Value || Convert.ToInt32(valor) < 1)
                                throw new InvalidOperationException("El servicio no existe o no tiene una duración válida.");
                            duracion = Convert.ToInt32(valor);
                        }
                        if (agenda.Estado != "CANCELADA")
                        {
                            using (var empleado = new OracleCommand("SELECT ACTIVO FROM TALLER.EMPLEADO WHERE CEDULA = :cedula", con))
                            {
                                empleado.Transaction = tx;
                                empleado.Parameters.Add("cedula", OracleDbType.Char, 10).Value = agenda.Empleado.Cedula;
                                if (Convert.ToString(empleado.ExecuteScalar()).Trim() != "S")
                                    throw new InvalidOperationException("El empleado no existe o está inactivo.");
                            }
                            const string cruces = @"SELECT COUNT(*) FROM TALLER.AGENDA A
                                LEFT JOIN TALLER.ARTICULO S ON S.ARTICULOID = A.ARTICULO_ARTICULOID
                                WHERE A.AGENDAID <> :id AND UPPER(TRIM(A.ESTADO)) <> 'CANCELADA'
                                  AND (A.EMPLEADO_CEDULA = :cedula OR A.VEHICULO_PLACA = :placa)
                                  AND (A.FECHA IS NULL OR (A.FECHA < :fin
                                       AND :inicio < A.FECHA + GREATEST(NVL(S.DURACION, 1), 1) / 1440))";
                            using (var validar = new OracleCommand(cruces, con))
                            {
                                validar.Transaction = tx;
                                validar.BindByName = true;
                                validar.Parameters.Add("id", nueva ? 0 : agenda.AgendaID);
                                validar.Parameters.Add("cedula", OracleDbType.Char, 10).Value = agenda.Empleado.Cedula;
                                validar.Parameters.Add("placa", OracleDbType.Char, 7).Value = agenda.Vehiculo.Placa;
                                validar.Parameters.Add("inicio", OracleDbType.Date).Value = agenda.Fecha;
                                validar.Parameters.Add("fin", OracleDbType.Date).Value = agenda.Fecha.AddMinutes(duracion);
                                if (Convert.ToInt32(validar.ExecuteScalar()) > 0)
                                    throw new InvalidOperationException("El empleado o el vehículo ya tiene una cita que se cruza con ese horario.");
                            }
                        }
                        if (nueva)
                        {
                            using (var secuencia = new OracleCommand("SELECT TALLER.SEQ_AGENDAID.NEXTVAL FROM DUAL", con))
                            {
                                secuencia.Transaction = tx;
                                agenda.AgendaID = Convert.ToInt32(secuencia.ExecuteScalar());
                            }
                        }
                        string sql = soloEstado
                            ? "UPDATE TALLER.AGENDA SET ESTADO = :estado WHERE AGENDAID = :agendaId"
                            : nueva
                            ? @"INSERT INTO TALLER.AGENDA (AGENDAID, FECHA, COMENTARIO, VEHICULO_PLACA, EMPLEADO_CEDULA, ESTADO, ARTICULO_ARTICULOID)
                                VALUES (:agendaId, :fecha, :comentario, :vehiculoPlaca, :empleadoCedula, :estado, :articuloId)"
                            : @"UPDATE TALLER.AGENDA SET FECHA = :fecha, COMENTARIO = :comentario, VEHICULO_PLACA = :vehiculoPlaca,
                                EMPLEADO_CEDULA = :empleadoCedula, ESTADO = :estado, ARTICULO_ARTICULOID = :articuloId WHERE AGENDAID = :agendaId";
                        using (var cmd = new OracleCommand(sql, con))
                        {
                            cmd.Transaction = tx;
                            if (soloEstado)
                            {
                                cmd.BindByName = true;
                                cmd.Parameters.Add("estado", agenda.Estado);
                            }
                            else AgregarParams(cmd, agenda);
                            cmd.Parameters.Add("agendaId", agenda.AgendaID);
                            if (cmd.ExecuteNonQuery() != 1) throw new InvalidOperationException("La cita ya no existe.");
                        }
                        tx.Commit();
                    }
                    catch { tx.Rollback(); throw; }
                }
            }
        }
    }
}
