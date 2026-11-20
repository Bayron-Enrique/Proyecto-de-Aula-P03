using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace DATOS
{
    public class EmpleadoRepository : ManejadorConexion, IRepository<Empleado>
    {
        public int Contar()
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_EMPLEADO.PRC_CONTAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_total", OracleDbType.Int32).Direction = ParameterDirection.Output;
                conexion.Open(); comando.ExecuteNonQuery();
                return Convert.ToInt32(comando.Parameters["p_total"].Value.ToString());
            }
        }

        public void Guardar(Empleado empleado)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_EMPLEADO.PRC_GUARDAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                AgregarParams(comando, empleado);
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }

        public void Modificar(Empleado empleado)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_EMPLEADO.PRC_MODIFICAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                AgregarParams(comando, empleado);
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }

        public List<Empleado> Consultar()
        {
            List<Empleado> empleados = new List<Empleado>();
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_EMPLEADO.PRC_CONSULTAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                conexion.Open();
                using (OracleDataReader reader = comando.ExecuteReader())
                    while (reader.Read()) empleados.Add(Mapear(reader));
            }
            return empleados;
        }

        public Empleado BuscarCedula(string cedula)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_EMPLEADO.PRC_BUSCAR_CEDULA", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_cedula", OracleDbType.Char, 10).Value = cedula;
                comando.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                conexion.Open();
                using (OracleDataReader reader = comando.ExecuteReader())
                    return reader.Read() ? Mapear(reader) : null;
            }
        }

        public void Eliminar(Empleado empleado)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_EMPLEADO.PRC_ELIMINAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_cedula", OracleDbType.Char, 10).Value = empleado.Cedula;
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }

        public string ObtenerClaveHash(string cedula)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_EMPLEADO.PRC_OBTENER_CLAVE", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_cedula", OracleDbType.Char, 10).Value = cedula;
                comando.Parameters.Add("p_hash", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;
                conexion.Open(); comando.ExecuteNonQuery();
                return comando.Parameters["p_hash"].Value == DBNull.Value ? null : comando.Parameters["p_hash"].Value.ToString();
            }
        }

        public void GuardarClaveHash(string cedula, string hash)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_EMPLEADO.PRC_GUARDAR_CLAVE", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_cedula", OracleDbType.Char, 10).Value = cedula;
                comando.Parameters.Add("p_hash", OracleDbType.Varchar2, 200).Value = hash;
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }

        public void CambiarClaveHash(string cedula, string hash)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_EMPLEADO.PRC_CAMBIAR_CLAVE", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_cedula", OracleDbType.Char, 10).Value = cedula;
                comando.Parameters.Add("p_hash", OracleDbType.Varchar2, 200).Value = hash;
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }

        private static void AgregarParams(OracleCommand comando, Empleado empleado)
        {
            comando.BindByName = true;
            comando.Parameters.Add("p_cedula", OracleDbType.Char, 10).Value = empleado.Cedula;
            comando.Parameters.Add("p_pnombre", OracleDbType.Varchar2).Value = empleado.PrimerNombre;
            comando.Parameters.Add("p_snombre", OracleDbType.Varchar2).Value = (object)empleado.SegundoNombre ?? DBNull.Value;
            comando.Parameters.Add("p_papellido", OracleDbType.Varchar2).Value = empleado.PrimerApellido;
            comando.Parameters.Add("p_sapellido", OracleDbType.Varchar2).Value = (object)empleado.SegundoApellido ?? DBNull.Value;
            comando.Parameters.Add("p_sexo", OracleDbType.Char, 1).Value = empleado.Sexo;
            comando.Parameters.Add("p_telefono", OracleDbType.Varchar2).Value = (object)empleado.Telefono ?? DBNull.Value;
            comando.Parameters.Add("p_direccion", OracleDbType.Varchar2).Value = (object)empleado.Direccion ?? DBNull.Value;
            comando.Parameters.Add("p_correo", OracleDbType.Varchar2).Value = (object)empleado.CorreoElectronico ?? DBNull.Value;
            comando.Parameters.Add("p_cargo", OracleDbType.Int32).Value = empleado.Cargo.CargoId;
            comando.Parameters.Add("p_activo", OracleDbType.Char, 1).Value = empleado.Activo ? "S" : "N";
        }

        private static Empleado Mapear(OracleDataReader reader)
        {
            return new Empleado
            {
                Cedula = reader["CEDULA"].ToString().Trim(), PrimerNombre = reader["PRIMER_NOMBRE"].ToString(),
                SegundoNombre = reader["SEGUNDO_NOMBRE"] == DBNull.Value ? null : reader["SEGUNDO_NOMBRE"].ToString(),
                PrimerApellido = reader["PRIMER_APELLIDO"].ToString(),
                SegundoApellido = reader["SEGUNDO_APELLIDO"] == DBNull.Value ? null : reader["SEGUNDO_APELLIDO"].ToString(),
                Sexo = reader["SEXO"].ToString().Trim(), Activo = reader["ACTIVO"].ToString().Trim() == "S",
                Telefono = reader["TELEFONO"] == DBNull.Value ? null : reader["TELEFONO"].ToString(),
                Direccion = reader["DIRECCION"] == DBNull.Value ? null : reader["DIRECCION"].ToString(),
                CorreoElectronico = reader["CORREOELECTRONICO"] == DBNull.Value ? null : reader["CORREOELECTRONICO"].ToString(),
                Cargo = new Cargo { CargoId = Convert.ToInt32(reader["CARGOID"]), NombreCargo = reader["NOMBRECARGO"].ToString() }
            };
        }
    }
}
