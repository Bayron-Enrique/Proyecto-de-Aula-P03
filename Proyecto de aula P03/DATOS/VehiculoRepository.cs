using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace DATOS
{
    public class VehiculoRepository : ManejadorConexion, IRepository<Vehiculo>
    {
        public int Contar()
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_VEHICULO.PRC_CONTAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_total", OracleDbType.Int32).Direction = ParameterDirection.Output;
                conexion.Open();
                comando.ExecuteNonQuery();
                return Convert.ToInt32(comando.Parameters["p_total"].Value.ToString());
            }
        }

        public void Guardar(Vehiculo vehiculo)
        {
            if (vehiculo.Cliente == null || string.IsNullOrWhiteSpace(vehiculo.Cliente.Cedula))
                throw new ArgumentException("Debe seleccionar un cliente válido para el vehículo.");

            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_VEHICULO.PRC_GUARDAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                AgregarParams(comando, vehiculo);

                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }

        public void Modificar(Vehiculo vehiculo)
        {
            if (vehiculo.Cliente == null || string.IsNullOrWhiteSpace(vehiculo.Cliente.Cedula))
                throw new ArgumentException("Debe seleccionar un cliente válido para el vehículo.");

            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_VEHICULO.PRC_MODIFICAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                AgregarParams(comando, vehiculo);

                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }

        public List<Vehiculo> Consultar()
        {
            List<Vehiculo> vehiculos = new List<Vehiculo>();
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_VEHICULO.PRC_CONSULTAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                conexion.Open();
                using (OracleDataReader reader = comando.ExecuteReader())
                    while (reader.Read()) vehiculos.Add(Mapear(reader));
            }
            return vehiculos;
        }

        public Vehiculo BuscarPlaca(string placa)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_VEHICULO.PRC_BUSCAR_PLACA", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_placa", OracleDbType.Varchar2, 6).Value = placa;
                comando.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                conexion.Open();
                using (OracleDataReader reader = comando.ExecuteReader())
                    return reader.Read() ? Mapear(reader) : null;
            }
        }

        public void Eliminar(Vehiculo vehiculo)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_VEHICULO.PRC_ELIMINAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_placa", OracleDbType.Varchar2 ,6).Value = vehiculo.Placa;
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }

        private static void AgregarParams(OracleCommand comando, Vehiculo vehiculo)
        {
            comando.BindByName = true;
            comando.Parameters.Add("p_placa", OracleDbType.Varchar2, 6).Value = vehiculo.Placa;
            comando.Parameters.Add("p_modelo", OracleDbType.Decimal).Value = vehiculo.Modelo;
            comando.Parameters.Add("p_marca", OracleDbType.Varchar2).Value = (object)vehiculo.Marca ?? DBNull.Value;
            comando.Parameters.Add("p_cliente", OracleDbType.Char, 10).Value = vehiculo.Cliente.Cedula;
            comando.Parameters.Add("p_tipo", OracleDbType.Varchar2).Value = vehiculo.TipoVehiculo;
            comando.Parameters.Add("p_color", OracleDbType.Varchar2).Value = (object)vehiculo.Color ?? DBNull.Value;
            comando.Parameters.Add("p_linea", OracleDbType.Varchar2).Value = (object)vehiculo.Linea ?? DBNull.Value;
        }

        private static Vehiculo Mapear(OracleDataReader reader)
        {
            return new Vehiculo
            {
                Placa = reader["PLACA"].ToString().Trim(),
                Modelo = reader["MODELO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MODELO"]),
                Marca = reader["MARCA"] == DBNull.Value ? null : reader["MARCA"].ToString(),
                TipoVehiculo = reader["TIPO_VEHICULO"].ToString().Trim(),
                Color = reader["COLOR"] == DBNull.Value ? null : reader["COLOR"].ToString(),
                Linea = reader["LINEA"] == DBNull.Value ? null : reader["LINEA"].ToString(),
                Cliente = new Cliente
                {
                    Cedula = reader["CLIENTE_CEDULA"].ToString().Trim(),
                    PrimerNombre = reader["PRIMER_NOMBRE"].ToString(),
                    SegundoNombre = reader["SEGUNDO_NOMBRE"] == DBNull.Value ? null : reader["SEGUNDO_NOMBRE"].ToString(),
                    PrimerApellido = reader["PRIMER_APELLIDO"].ToString(),
                    SegundoApellido = reader["SEGUNDO_APELLIDO"] == DBNull.Value ? null : reader["SEGUNDO_APELLIDO"].ToString(),
                    Sexo = reader["SEXO"].ToString().Trim(),
                    Telefono = reader["TELEFONO"] == DBNull.Value ? null : reader["TELEFONO"].ToString(),
                    Direccion = reader["DIRECCION"] == DBNull.Value ? null : reader["DIRECCION"].ToString(),
                    CorreoElectronico = reader["CORREOELECTRONICO"] == DBNull.Value ? null : reader["CORREOELECTRONICO"].ToString()
                }
            };
        }
    }
}
