using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace DATOS
{
    public class ClienteRepository : ManejadorConexion, IRepository<Cliente>
    {
        public int Contar()
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_CLIENTE.PRC_CONTAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_total", OracleDbType.Int32).Direction = ParameterDirection.Output;
                conexion.Open(); comando.ExecuteNonQuery();
                return Convert.ToInt32(comando.Parameters["p_total"].Value.ToString());
            }
        }

        public void Guardar(Cliente cliente)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_CLIENTE.PRC_GUARDAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                AgregarParams(comando, cliente);

                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }

        public void Modificar(Cliente cliente)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_CLIENTE.PRC_MODIFICAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                AgregarParams(comando, cliente);

                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }

        public List<Cliente> Consultar()
        {
            List<Cliente> clientes = new List<Cliente>();
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_CLIENTE.PRC_CONSULTAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                conexion.Open();
                using (OracleDataReader reader = comando.ExecuteReader())
                    while (reader.Read()) clientes.Add(Mapear(reader));
            }
            return clientes;
        }

        public Cliente BuscarCedula(string cedula)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_CLIENTE.PRC_BUSCAR_CEDULA", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_cedula", OracleDbType.Char, 10).Value = cedula;
                comando.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                conexion.Open();
                using (OracleDataReader reader = comando.ExecuteReader())
                    return reader.Read() ? Mapear(reader) : null;
            }
        }

        public void VincularTelegram(string cedula, long chatId)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_CLIENTE.PRC_VINCULAR_TELEGRAM", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.BindByName = true;
                comando.Parameters.Add("p_cedula", OracleDbType.Char, 10).Value = cedula;
                comando.Parameters.Add("p_chat_id", OracleDbType.Int64).Value = chatId;
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }

        public Cliente BuscarPorTelegramChatId(long chatId)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_CLIENTE.PRC_BUSCAR_POR_TELEGRAM_CHAT_ID", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.BindByName = true;
                comando.Parameters.Add("p_chat_id", OracleDbType.Int64).Value = chatId;
                comando.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                conexion.Open();
                using (OracleDataReader reader = comando.ExecuteReader())
                    return reader.Read() ? Mapear(reader) : null;
            }
        }

        public void Eliminar(Cliente cliente)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_CLIENTE.PRC_ELIMINAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_cedula", OracleDbType.Char, 10).Value = cliente.Cedula;
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }

        private static void AgregarParams(OracleCommand comando, Cliente cliente)
        {
            comando.BindByName = true;
            comando.Parameters.Add("p_cedula", OracleDbType.Char, 10).Value = cliente.Cedula;
            comando.Parameters.Add("p_pnombre", OracleDbType.Varchar2).Value = cliente.PrimerNombre;
            comando.Parameters.Add("p_snombre", OracleDbType.Varchar2).Value = (object)cliente.SegundoNombre ?? DBNull.Value;
            comando.Parameters.Add("p_papellido", OracleDbType.Varchar2).Value = cliente.PrimerApellido;
            comando.Parameters.Add("p_sapellido", OracleDbType.Varchar2).Value = (object)cliente.SegundoApellido ?? DBNull.Value;
            comando.Parameters.Add("p_sexo", OracleDbType.Char, 1).Value = cliente.Sexo;
            comando.Parameters.Add("p_telefono", OracleDbType.Varchar2).Value = (object)cliente.Telefono ?? DBNull.Value;
            comando.Parameters.Add("p_direccion", OracleDbType.Varchar2).Value = (object)cliente.Direccion ?? DBNull.Value;
            comando.Parameters.Add("p_correo", OracleDbType.Varchar2).Value = (object)cliente.CorreoElectronico ?? DBNull.Value;
        }

        private static Cliente Mapear(OracleDataReader reader)
        {
            return new Cliente
            {
                Cedula = reader["CEDULA"].ToString().Trim(),
                PrimerNombre = reader["PRIMER_NOMBRE"].ToString(),
                SegundoNombre = reader["SEGUNDO_NOMBRE"] == DBNull.Value ? null : reader["SEGUNDO_NOMBRE"].ToString(),
                PrimerApellido = reader["PRIMER_APELLIDO"].ToString(),
                SegundoApellido = reader["SEGUNDO_APELLIDO"] == DBNull.Value ? null : reader["SEGUNDO_APELLIDO"].ToString(),
                Sexo = reader["SEXO"].ToString().Trim(),
                Telefono = reader["TELEFONO"] == DBNull.Value ? null : reader["TELEFONO"].ToString(),
                Direccion = reader["DIRECCION"] == DBNull.Value ? null : reader["DIRECCION"].ToString(),
                CorreoElectronico = reader["CORREOELECTRONICO"] == DBNull.Value ? null : reader["CORREOELECTRONICO"].ToString(),
                TelegramChatId = TieneColumna(reader, "TELEGRAM_CHAT_ID") && reader["TELEGRAM_CHAT_ID"] != DBNull.Value
                    ? (long?)Convert.ToInt64(reader["TELEGRAM_CHAT_ID"]) : null
            };
        }

        private static bool TieneColumna(OracleDataReader reader, string nombre)
        {
            for (int i = 0; i < reader.FieldCount; i++)
                if (string.Equals(reader.GetName(i), nombre, StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }
    }
}
