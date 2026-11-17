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
            using (OracleCommand cmd = new OracleCommand("SELECT SEQ_SOATID.NEXTVAL FROM DUAL", con))
            {
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Guardar(Soat soat)
        {
            const string sql = @"INSERT INTO SOAT
                                 (SOATID, NUMEROSOAT, FECHAINICIO, VEHICULO_PLACA, FECHAFIN)
                                 VALUES (:soatId, :numeroSoat, :fechaInicio, :placa, :fechaFin)";

            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(sql, con))
            {
                cmd.BindByName = true;
                AgregarParametros(cmd, soat);
                cmd.Parameters.Add(new OracleParameter("soatId", soat.SoatId));

                con.Open();
                cmd.ExecuteNonQuery();
            }
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
            const string sql = @"UPDATE SOAT
                                 SET NUMEROSOAT = :numeroSoat,
                                     FECHAINICIO = :fechaInicio,
                                     VEHICULO_PLACA = :placa,
                                     FECHAFIN = :fechaFin
                                 WHERE SOATID = :soatId";

            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(sql, con))
            {
                cmd.BindByName = true;
                AgregarParametros(cmd, soat);
                cmd.Parameters.Add(new OracleParameter("soatId", soat.SoatId));
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(Soat soat)
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("DELETE FROM SOAT WHERE SOATID = :soatId", con))
            {
                cmd.Parameters.Add(new OracleParameter("soatId", soat.SoatId));
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private const string SelectSoats = @"SELECT S.SOATID, S.NUMEROSOAT, S.FECHAINICIO,
                                                    S.FECHAFIN, S.VEHICULO_PLACA
                                             FROM SOAT S";

        private static Soat Mapear(OracleDataReader reader)
        {
            return new Soat
            {
                SoatId = Convert.ToInt32(reader["SOATID"]),
                NumeroSoat = reader["NUMEROSOAT"] == DBNull.Value ? null : reader["NUMEROSOAT"].ToString(),
                FechaInicio = reader["FECHAINICIO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FECHAINICIO"]),
                FechaFin = reader["FECHAFIN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FECHAFIN"]),
                Vehiculo = new Vehiculo { Placa = reader["VEHICULO_PLACA"].ToString().Trim() }
            };
        }

        private static void AgregarParametros(OracleCommand cmd, Soat soat)
        {
            cmd.Parameters.Add(new OracleParameter("numeroSoat", soat.NumeroSoat));
            cmd.Parameters.Add(new OracleParameter("fechaInicio", soat.FechaInicio));
            cmd.Parameters.Add(new OracleParameter("placa", soat.Vehiculo.Placa));
            cmd.Parameters.Add(new OracleParameter("fechaFin", soat.FechaFin));
        }
    }
}
