using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace DATOS
{
    public class ClienteRepository : ManejadorConexion, IRepository<Cliente>
    {
        public void Guardar(Cliente cliente)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = @"INSERT INTO TALLER.PERSONA (CEDULA, NOMBRES, APELLIDOS, SEXO, TELEFONO, DIRECCION, CORREOELECTRONICO)
                               VALUES (:cedula, :nombres, :apellidos, :sexo, :telefono, :direccion, :correo)";
                con.Open();
                using (OracleTransaction tx = con.BeginTransaction())
                {
                    try
                    {
                        using (OracleCommand cmd = new OracleCommand(sql,con))
                        {
                            cmd.Transaction = tx;
                            cmd.BindByName = true;
                            AgregarParametrosPersona(cmd, cliente);
                            cmd.ExecuteNonQuery();
                        }
                        using (OracleCommand cmd = new OracleCommand("INSERT INTO TALLER.CLIENTE (CEDULA) VALUES (:cedula)", con))
                        {
                            cmd.Transaction = tx;
                            cmd.Parameters.Add(new OracleParameter("cedula", cliente.Cedula));
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                    }
                    catch { tx.Rollback(); throw; }
                }
            }
        }

        public List<Cliente> Consultar()
        {
            List<Cliente> clientes = new List<Cliente>();
            const string sql = @"SELECT P.CEDULA, P.NOMBRES, P.APELLIDOS, P.SEXO, P.TELEFONO, P.DIRECCION, P.CORREOELECTRONICO
                                 FROM TALLER.CLIENTE C JOIN TALLER.PERSONA P ON P.CEDULA = C.CEDULA";
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(sql, con))
            {
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                    while (reader.Read()) clientes.Add(Mapear(reader));
            }
            return clientes;
        }

        public Cliente BuscarCedula(string cedula)
        {
            const string sql = @"SELECT P.CEDULA, P.NOMBRES, P.APELLIDOS, P.SEXO, P.TELEFONO, P.DIRECCION, P.CORREOELECTRONICO
                                 FROM TALLER.CLIENTE C JOIN TALLER.PERSONA P ON P.CEDULA = C.CEDULA WHERE C.CEDULA = :cedula";
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(sql, con))
            {
                cmd.Parameters.Add(new OracleParameter("cedula", cedula));
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader()) return reader.Read() ? Mapear(reader) : null;
            }
        }

        public void Modificar(Cliente cliente)
        {
            const string sql = @"UPDATE TALLER.PERSONA SET NOMBRES = :nombres, APELLIDOS = :apellidos, SEXO = :sexo,
                                 TELEFONO = :telefono, DIRECCION = :direccion, CORREOELECTRONICO = :correo
                                 WHERE CEDULA = :cedula AND EXISTS (SELECT 1 FROM TALLER.CLIENTE C WHERE C.CEDULA = :cedula)";
            using (OracleConnection con = ObtenerConexion())
            {
                con.Open();
                using (OracleTransaction tx = con.BeginTransaction())
                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    try
                    {
                        cmd.Transaction = tx;
                        cmd.BindByName = true;
                        AgregarParametrosPersona(cmd, cliente);
                        cmd.ExecuteNonQuery();
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Eliminar(Cliente cliente)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                con.Open();
                using (OracleTransaction tx = con.BeginTransaction())
                {
                    try
                    {
                        using (OracleCommand cmd = new OracleCommand("DELETE FROM TALLER.CLIENTE WHERE CEDULA = :cedula", con))
                        {
                            cmd.Transaction = tx;
                            cmd.Parameters.Add(new OracleParameter("cedula", cliente.Cedula));
                            cmd.ExecuteNonQuery();
                        }
                        using (OracleCommand cmd = new OracleCommand("DELETE FROM TALLER.PERSONA WHERE CEDULA = :cedula", con))
                        {
                            cmd.Transaction = tx;
                            cmd.Parameters.Add(new OracleParameter("cedula", cliente.Cedula));
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                    }
                    catch { tx.Rollback(); throw; }
                }
            }
        }

        private static Cliente Mapear(OracleDataReader reader)
        {
            return new Cliente
            {
                Cedula = reader["CEDULA"].ToString().Trim(), Nombre = reader["NOMBRES"].ToString(),
                Apellido = reader["APELLIDOS"].ToString(), Sexo = reader["SEXO"].ToString().Trim(),
                Telefono = reader["TELEFONO"] == DBNull.Value ? null : reader["TELEFONO"].ToString(),
                Direccion = reader["DIRECCION"] == DBNull.Value ? null : reader["DIRECCION"].ToString(),
                CorreoElectronico = reader["CORREOELECTRONICO"] == DBNull.Value ? null : reader["CORREOELECTRONICO"].ToString()
            };
        }

        private void AgregarParametrosPersona(OracleCommand cmd, Cliente cliente)
        {
            cmd.Parameters.Add(new OracleParameter("cedula", cliente.Cedula));
            cmd.Parameters.Add(new OracleParameter("nombres", cliente.Nombre));
            cmd.Parameters.Add(new OracleParameter("apellidos", cliente.Apellido));
            cmd.Parameters.Add(new OracleParameter("sexo", cliente.Sexo));
            cmd.Parameters.Add(new OracleParameter("telefono", (object)cliente.Telefono ?? DBNull.Value));
            cmd.Parameters.Add(new OracleParameter("direccion", (object)cliente.Direccion ?? DBNull.Value));
            cmd.Parameters.Add(new OracleParameter("correo", (object)cliente.CorreoElectronico ?? DBNull.Value));
        }
    }
}
