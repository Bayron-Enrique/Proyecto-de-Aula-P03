using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace DATOS
{
    public class VehiculoRepository : ManejadorConexion, IRepository<Vehiculo>
    {
        public void Guardar(Vehiculo vehiculo)
        {
            const string sql = @"INSERT INTO VEHICULO (PLACA, MODELO, MARCA, CLIENTE_CEDULA, TIPO_VEHICULO, COLOR, LINEA)
                                 VALUES (:placa, :modelo, :marca, :clienteCedula, :tipoVehiculo, :color, :linea)";
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(sql, con))
            {
                AgregarParametros(cmd, vehiculo);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Vehiculo> Consultar()
        {
            List<Vehiculo> vehiculos = new List<Vehiculo>();
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(SelectVehiculos, con))
            {
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                    while (reader.Read()) vehiculos.Add(Mapear(reader));
            }
            return vehiculos;
        }

        public Vehiculo BuscarPlaca(string placa)
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(SelectVehiculos + " WHERE V.PLACA = :placa", con))
            {
                cmd.Parameters.Add(new OracleParameter("placa", placa));
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader()) return reader.Read() ? Mapear(reader) : null;
            }
        }

        public void Modificar(Vehiculo vehiculo)
        {
            const string sql = @"UPDATE VEHICULO SET MODELO = :modelo, MARCA = :marca, CLIENTE_CEDULA = :clienteCedula,
                                 TIPO_VEHICULO = :tipoVehiculo, COLOR = :color, LINEA = :linea WHERE PLACA = :placa";
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(sql, con))
            {
                AgregarParametros(cmd, vehiculo);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(Vehiculo vehiculo)
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("DELETE FROM VEHICULO WHERE PLACA = :placa", con))
            {
                cmd.Parameters.Add(new OracleParameter("placa", vehiculo.Placa));
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private const string SelectVehiculos = @"SELECT V.PLACA, V.MODELO, V.MARCA, V.TIPO_VEHICULO, V.COLOR, V.LINEA, V.CLIENTE_CEDULA
                                                 FROM VEHICULO V";

        private static Vehiculo Mapear(OracleDataReader reader)
        {
            return new Vehiculo
            {
                Placa = reader["PLACA"].ToString().Trim(),
                Modelo = reader["MODELO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MODELO"]),
                Marca = reader["MARCA"] == DBNull.Value ? null : reader["MARCA"].ToString(),
                TipoVehiculo = reader["TIPO_VEHICULO"].ToString(),
                Color = reader["COLOR"] == DBNull.Value ? null : reader["COLOR"].ToString(),
                Linea = reader["LINEA"] == DBNull.Value ? null : reader["LINEA"].ToString(),
                Cliente = new Cliente { Cedula = reader["CLIENTE_CEDULA"].ToString().Trim() },
                ClienteVehiculo = reader["CLIENTE_CEDULA"].ToString().Trim()
            };
        }

        private static void AgregarParametros(OracleCommand cmd, Vehiculo vehiculo)
        {
            cmd.Parameters.Add(new OracleParameter("placa", vehiculo.Placa));
            cmd.Parameters.Add(new OracleParameter("modelo", vehiculo.Modelo));
            cmd.Parameters.Add(new OracleParameter("marca", vehiculo.Marca !=null? (object)vehiculo.Marca :DBNull.Value));
            cmd.Parameters.Add(new OracleParameter("clienteCedula", vehiculo.Cliente != null ? (object)vehiculo.Cliente.Cedula : vehiculo.ClienteVehiculo));
            cmd.Parameters.Add(new OracleParameter("tipoVehiculo", vehiculo.TipoVehiculo));
            cmd.Parameters.Add(new OracleParameter("color", (object)vehiculo.Color ?? DBNull.Value));
            cmd.Parameters.Add(new OracleParameter("linea", (object)vehiculo.Linea ?? DBNull.Value));
        }
    }
}
