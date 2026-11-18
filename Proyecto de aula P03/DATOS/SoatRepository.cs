using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace DATOS
{
    public class SoatRepository : ManejadorConexion, IRepository<Soat>
    {
        public int ObtenerSiguienteSoatId()
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("SELECT TALLER.SEQ_SOATID.NEXTVAL FROM DUAL", con))
            {
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Guardar(Soat soat)
        {
            Persistir(soat, true);
        }

        public List<Soat> Consultar()
        {
            List<Soat> soats = new List<Soat>();
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(SelectSoats, con))
            {
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        soats.Add(Mapear(reader));
                    }
                }
            }
            return soats;
        }

        public Soat BuscarId(int soatId)
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(SelectSoats + " WHERE S.SOATID = :soatId", con))
            {
                cmd.Parameters.Add(new OracleParameter("soatId", soatId));
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    return reader.Read() ? Mapear(reader) : null;
                }
            }
        }

        public void Modificar(Soat soat)
        {
            Persistir(soat, false);
        }

        public void Eliminar(Soat soat)
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("DELETE FROM TALLER.SOAT WHERE SOATID = :soatId", con))
            {
                cmd.Parameters.Add(new OracleParameter("soatId", soat.SoatId));
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private const string SelectSoats = @"SELECT S.SOATID, S.NUMEROSOAT, S.FECHAINICIO, S.FECHAFIN,
                                                    V.PLACA AS VEHICULO_PLACA, V.MARCA, V.LINEA, V.MODELO, V.COLOR,
                                                    V.TIPO_VEHICULO, V.CLIENTE_CEDULA,
                                                    P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE, P.PRIMER_APELLIDO, P.SEGUNDO_APELLIDO,
                                                    P.CORREOELECTRONICO
                                             FROM TALLER.SOAT S
                                             JOIN TALLER.VEHICULO V ON V.PLACA = S.VEHICULO_PLACA
                                             JOIN TALLER.CLIENTE C ON C.CEDULA = V.CLIENTE_CEDULA
                                             JOIN TALLER.PERSONA P ON P.CEDULA = C.CEDULA";

        private static Soat Mapear(OracleDataReader reader)
        {
            return new Soat
            {
                SoatId = Convert.ToInt32(reader["SOATID"]),
                NumeroSoat = reader["NUMEROSOAT"] == DBNull.Value ? null : reader["NUMEROSOAT"].ToString(),
                FechaInicio = reader["FECHAINICIO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FECHAINICIO"]),
                FechaFin = reader["FECHAFIN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FECHAFIN"]),
                Vehiculo = new Vehiculo
                {
                    Placa = reader["VEHICULO_PLACA"].ToString().Trim(),
                    Marca = reader["MARCA"].ToString(), Linea = reader["LINEA"].ToString(),
                    Modelo = reader["MODELO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MODELO"]),
                    Color = reader["COLOR"] == DBNull.Value ? null : reader["COLOR"].ToString(),
                    TipoVehiculo = reader["TIPO_VEHICULO"].ToString(),
                    Cliente = new Cliente
                    {
                        Cedula = reader["CLIENTE_CEDULA"].ToString().Trim(),
                        PrimerNombre = reader["PRIMER_NOMBRE"].ToString(),
                        SegundoNombre = reader["SEGUNDO_NOMBRE"] == DBNull.Value ? null : reader["SEGUNDO_NOMBRE"].ToString(),
                        PrimerApellido = reader["PRIMER_APELLIDO"].ToString(),
                        SegundoApellido = reader["SEGUNDO_APELLIDO"] == DBNull.Value ? null : reader["SEGUNDO_APELLIDO"].ToString(),
                        CorreoElectronico = reader["CORREOELECTRONICO"] == DBNull.Value ? null : reader["CORREOELECTRONICO"].ToString()
                    }
                }
            };
        }

        private static void AgregarParametros(OracleCommand cmd, Soat soat)
        {
            if (soat.Vehiculo == null || string.IsNullOrWhiteSpace(soat.Vehiculo.Placa))
                throw new ArgumentException("Debe seleccionar un vehículo para el SOAT.");
            cmd.BindByName = true;
            cmd.Parameters.Add(new OracleParameter("numeroSoat", soat.NumeroSoat));
            cmd.Parameters.Add(new OracleParameter("fechaInicio", soat.FechaInicio));
            cmd.Parameters.Add("placa", OracleDbType.Char, 7).Value = soat.Vehiculo.Placa;
            cmd.Parameters.Add(new OracleParameter("fechaFin", soat.FechaFin));
        }

        private void Persistir(Soat soat, bool nuevo)
        {
            if (soat == null || soat.Vehiculo == null || string.IsNullOrWhiteSpace(soat.Vehiculo.Placa))
                throw new ArgumentException("Selecciona un vehículo para el SOAT.");
            if (string.IsNullOrWhiteSpace(soat.NumeroSoat) || soat.FechaFin <= soat.FechaInicio ||
                soat.FechaFin > soat.FechaInicio.AddYears(1))
                throw new ArgumentException("El SOAT requiere un número y fechas válidas sin superar un año.");
            using (OracleConnection con = ObtenerConexion())
            {
                con.Open();
                using (OracleTransaction tx = con.BeginTransaction())
                {
                    try
                    {
                        using (var bloqueo = new OracleCommand("LOCK TABLE TALLER.SOAT IN SHARE ROW EXCLUSIVE MODE", con))
                        {
                            bloqueo.Transaction = tx;
                            bloqueo.ExecuteNonQuery();
                        }
                        const string sqlValidar = @"SELECT COUNT(*) FROM TALLER.SOAT
                            WHERE SOATID <> :id AND (NUMEROSOAT = :numero OR
                            (VEHICULO_PLACA = :placa AND (FECHAINICIO IS NULL OR FECHAFIN IS NULL
                             OR (:inicio <= FECHAFIN AND :fin >= FECHAINICIO))))";
                        using (var validar = new OracleCommand(sqlValidar, con))
                        {
                            validar.Transaction = tx;
                            validar.BindByName = true;
                            validar.Parameters.Add("id", nuevo ? 0 : soat.SoatId);
                            validar.Parameters.Add("numero", soat.NumeroSoat);
                            validar.Parameters.Add("placa", OracleDbType.Char, 7).Value = soat.Vehiculo.Placa;
                            validar.Parameters.Add("inicio", OracleDbType.Date).Value = soat.FechaInicio;
                            validar.Parameters.Add("fin", OracleDbType.Date).Value = soat.FechaFin;
                            if (Convert.ToInt32(validar.ExecuteScalar()) > 0)
                                throw new InvalidOperationException("Ya existe ese número de SOAT o una póliza superpuesta para el vehículo.");
                        }
                        if (nuevo)
                        {
                            using (var siguiente = new OracleCommand("SELECT TALLER.SEQ_SOATID.NEXTVAL FROM DUAL", con))
                            {
                                siguiente.Transaction = tx;
                                soat.SoatId = Convert.ToInt32(siguiente.ExecuteScalar());
                            }
                        }
                        string sql = nuevo
                            ? @"INSERT INTO TALLER.SOAT (SOATID, NUMEROSOAT, FECHAINICIO, VEHICULO_PLACA, FECHAFIN)
                                VALUES (:soatId, :numeroSoat, :fechaInicio, :placa, :fechaFin)"
                            : @"UPDATE TALLER.SOAT SET NUMEROSOAT = :numeroSoat, FECHAINICIO = :fechaInicio,
                                VEHICULO_PLACA = :placa, FECHAFIN = :fechaFin WHERE SOATID = :soatId";
                        using (var cmd = new OracleCommand(sql, con))
                        {
                            cmd.Transaction = tx;
                            AgregarParametros(cmd, soat);
                            cmd.Parameters.Add("soatId", soat.SoatId);
                            if (cmd.ExecuteNonQuery() != 1) throw new InvalidOperationException("La póliza ya no existe.");
                        }
                        tx.Commit();
                    }
                    catch { tx.Rollback(); throw; }
                }
            }
        }
    }
}
