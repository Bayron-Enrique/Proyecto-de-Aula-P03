using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace DATOS
{
    public class CargoRepository : ManejadorConexion, IRepository<Cargo>
    {
        public int ObtenerSiguienteCargoId()
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_CARGO.SIGUIENTE_ID", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_id", OracleDbType.Int32).Direction = ParameterDirection.Output;
                conexion.Open(); comando.ExecuteNonQuery();
                return Convert.ToInt32(comando.Parameters["p_id"].Value.ToString());
            }
        }
        public void Guardar(Cargo cargo)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_CARGO.GUARDAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                AgregarParametros(comando, cargo);
                conexion.Open(); comando.ExecuteNonQuery();
            }
        }
        public void Modificar(Cargo cargo)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_CARGO.MODIFICAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                AgregarParametros(comando, cargo);
                conexion.Open(); comando.ExecuteNonQuery();
            }
        }
        public List<Cargo> Consultar()
        {
            List<Cargo> cargos = new List<Cargo>();
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_CARGO.CONSULTAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                conexion.Open(); using (OracleDataReader reader = comando.ExecuteReader())
                    while (reader.Read()) cargos.Add(Mapear(reader));
            }
            return cargos;
        }
        public Cargo BuscarId(int cargoId)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_CARGO.BUSCAR_ID", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_id", OracleDbType.Int32).Value = cargoId;
                comando.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                conexion.Open(); using (OracleDataReader reader = comando.ExecuteReader())
                    return reader.Read() ? Mapear(reader) : null;
            }
        }
        public void Eliminar(Cargo cargo)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_CARGO.ELIMINAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_id", OracleDbType.Int32).Value = cargo.CargoId;
                conexion.Open(); comando.ExecuteNonQuery();
            }
        }
        private static Cargo Mapear(OracleDataReader reader)
        { return new Cargo { CargoId = Convert.ToInt32(reader["CARGOID"]), NombreCargo = reader["NOMBRECARGO"].ToString() }; }
        private static void AgregarParametros(OracleCommand comando, Cargo cargo)
        {
            comando.BindByName = true;
            comando.Parameters.Add("p_id", OracleDbType.Int32).Value = cargo.CargoId;
            comando.Parameters.Add("p_nombre", OracleDbType.Varchar2).Value = cargo.NombreCargo;
        }
    }
}
