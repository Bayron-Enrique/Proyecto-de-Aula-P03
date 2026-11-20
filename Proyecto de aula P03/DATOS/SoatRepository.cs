using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace DATOS
{
    public class SoatRepository : ManejadorConexion, IRepository<Soat>
    {
        public void Guardar(Soat soat)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_SOAT.GUARDAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                AgregarParametros(comando, soat);
                OracleParameter parametroId = comando.Parameters.Add("p_id", OracleDbType.Int32);
                parametroId.Direction = ParameterDirection.Output;
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); soat.SoatId = Convert.ToInt32(parametroId.Value.ToString()); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }
        public void Modificar(Soat soat)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_SOAT.MODIFICAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                AgregarParametros(comando, soat);
                comando.Parameters.Add("p_id", OracleDbType.Int32).Value = soat.SoatId;
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }
        public List<Soat> Consultar()
        {
            List<Soat> soats = new List<Soat>();
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_SOAT.CONSULTAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                conexion.Open(); using (OracleDataReader reader = comando.ExecuteReader())
                    while (reader.Read()) soats.Add(Mapear(reader));
            }
            return soats;
        }
        public Soat BuscarId(int id)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_SOAT.BUSCAR_ID", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_id", OracleDbType.Int32).Value = id;
                comando.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                conexion.Open(); using (OracleDataReader reader = comando.ExecuteReader()) return reader.Read() ? Mapear(reader) : null;
            }
        }
        public void Eliminar(Soat soat)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_SOAT.ELIMINAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_id", OracleDbType.Int32).Value = soat.SoatId;
                conexion.Open(); comando.ExecuteNonQuery();
            }
        }
        private static void AgregarParametros(OracleCommand comando, Soat soat)
        {
            if (soat.Vehiculo == null || string.IsNullOrWhiteSpace(soat.Vehiculo.Placa))
                throw new ArgumentException("Debe seleccionar un vehículo para el SOAT.");
            comando.BindByName = true;
            comando.Parameters.Add("p_numero", OracleDbType.Varchar2).Value = soat.NumeroSoat;
            comando.Parameters.Add("p_inicio", OracleDbType.Date).Value = soat.FechaInicio;
            comando.Parameters.Add("p_fin", OracleDbType.Date).Value = soat.FechaFin;
            comando.Parameters.Add("p_placa", OracleDbType.Char, 7).Value = soat.Vehiculo.Placa;
        }
        private static Soat Mapear(OracleDataReader reader)
        {
            return new Soat
            {
                SoatId = Convert.ToInt32(reader["SOATID"]), NumeroSoat = reader["NUMEROSOAT"] == DBNull.Value ? null : reader["NUMEROSOAT"].ToString(),
                FechaInicio = reader["FECHAINICIO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FECHAINICIO"]),
                FechaFin = reader["FECHAFIN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FECHAFIN"]),
                Vehiculo = new Vehiculo { Placa = reader["VEHICULO_PLACA"].ToString().Trim(), Marca = reader["MARCA"].ToString(), Linea = reader["LINEA"].ToString(),
                    Modelo = reader["MODELO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MODELO"]), Color = reader["COLOR"] == DBNull.Value ? null : reader["COLOR"].ToString(),
                    TipoVehiculo = reader["TIPO_VEHICULO"].ToString(), Cliente = new Cliente { Cedula = reader["CLIENTE_CEDULA"].ToString().Trim(),
                        PrimerNombre = reader["PRIMER_NOMBRE"].ToString(), SegundoNombre = reader["SEGUNDO_NOMBRE"] == DBNull.Value ? null : reader["SEGUNDO_NOMBRE"].ToString(),
                        PrimerApellido = reader["PRIMER_APELLIDO"].ToString(), SegundoApellido = reader["SEGUNDO_APELLIDO"] == DBNull.Value ? null : reader["SEGUNDO_APELLIDO"].ToString(),
                        CorreoElectronico = reader["CORREOELECTRONICO"] == DBNull.Value ? null : reader["CORREOELECTRONICO"].ToString() }
                }
            };
        }
    }
}
