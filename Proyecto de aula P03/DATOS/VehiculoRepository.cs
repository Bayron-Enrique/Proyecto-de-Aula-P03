using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace DATOS
{
    public class VehiculoRepository : ManejadorConexion, IRepository<Vehiculo>
    {
        public int Contar()
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("SELECT COUNT(*) FROM TALLER.VEHICULO", con))
            {
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private const string SelectVehiculos = @"SELECT V.PLACA, V.MODELO, V.MARCA, V.TIPO_VEHICULO,
                                                        V.COLOR, V.LINEA, V.CLIENTE_CEDULA,
                                                        P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE, P.PRIMER_APELLIDO, P.SEGUNDO_APELLIDO, P.SEXO, P.TELEFONO,
                                                        P.DIRECCION, P.CORREOELECTRONICO
                                                 FROM TALLER.VEHICULO V
                                                 JOIN TALLER.CLIENTE C ON C.CEDULA = V.CLIENTE_CEDULA
                                                 JOIN TALLER.PERSONA P ON P.CEDULA = C.CEDULA";

        public void Guardar(Vehiculo vehiculo)
        {
            const string sql = @"INSERT INTO TALLER.VEHICULO
                                 (PLACA, MODELO, MARCA, CLIENTE_CEDULA, TIPO_VEHICULO, COLOR, LINEA)
                                 VALUES (:placa, :modelo, :marca, :clienteCedula, :tipoVehiculo, :color, :linea)";
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(sql, con))
            {
                cmd.BindByName = true;
                AgregarParametros(cmd, vehiculo);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Vehiculo> Consultar()
        {
            var lista = new List<Vehiculo>();
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(SelectVehiculos + " ORDER BY V.PLACA", con))
            {
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                    while (reader.Read()) lista.Add(Mapear(reader));
            }
            return lista;
        }

        public Vehiculo BuscarPlaca(string placa)
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(SelectVehiculos + " WHERE V.PLACA = :placa", con))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("placa", placa);
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                    return reader.Read() ? Mapear(reader) : null;
            }
        }

        public void Modificar(Vehiculo vehiculo)
        {
            const string sql = @"UPDATE TALLER.VEHICULO SET MODELO = :modelo, MARCA = :marca,
                                 CLIENTE_CEDULA = :clienteCedula, TIPO_VEHICULO = :tipoVehiculo,
                                 COLOR = :color, LINEA = :linea WHERE PLACA = :placa";
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(sql, con))
            {
                cmd.BindByName = true;
                AgregarParametros(cmd, vehiculo);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(Vehiculo vehiculo)
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("DELETE FROM TALLER.VEHICULO WHERE PLACA = :placa", con))
            {
                cmd.Parameters.Add("placa", vehiculo.Placa);
                con.Open();
                cmd.ExecuteNonQuery();
            }
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

        private static void AgregarParametros(OracleCommand cmd, Vehiculo vehiculo)
        {
            if (vehiculo.Cliente == null || string.IsNullOrWhiteSpace(vehiculo.Cliente.Cedula))
                throw new ArgumentException("Debe seleccionar un cliente válido para el vehículo.");
            cmd.Parameters.Add("placa", vehiculo.Placa);
            cmd.Parameters.Add("modelo", vehiculo.Modelo);
            cmd.Parameters.Add("marca", (object)vehiculo.Marca ?? DBNull.Value);
            cmd.Parameters.Add("clienteCedula", vehiculo.Cliente.Cedula);
            cmd.Parameters.Add("tipoVehiculo", vehiculo.TipoVehiculo);
            cmd.Parameters.Add("color", (object)vehiculo.Color ?? DBNull.Value);
            cmd.Parameters.Add("linea", (object)vehiculo.Linea ?? DBNull.Value);
        }
    }
}
