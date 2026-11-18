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
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("SELECT COUNT(*) FROM TALLER.EMPLEADO", con))
            {
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public string ObtenerClaveHash(string cedula)
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("SELECT CLAVE_HASH FROM TALLER.EMPLEADO WHERE CEDULA = :cedula", con))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("cedula", OracleDbType.Char, 10).Value = cedula;
                con.Open();
                object resultado = cmd.ExecuteScalar();
                return resultado == null || resultado == DBNull.Value ? null : resultado.ToString();
            }
        }

        public void GuardarClaveHash(string cedula, string hash)
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("UPDATE TALLER.EMPLEADO SET CLAVE_HASH = :hash WHERE CEDULA = :cedula AND CLAVE_HASH IS NULL AND ACTIVO = 'S'", con))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("hash", hash);
                cmd.Parameters.Add("cedula", OracleDbType.Char, 10).Value = cedula;
                con.Open();
                if (cmd.ExecuteNonQuery() != 1) throw new InvalidOperationException("No se pudo asignar la contraseña inicial del empleado.");
            }
        }

        public void CambiarClaveHash(string cedula, string hash)
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("UPDATE TALLER.EMPLEADO SET CLAVE_HASH = :hash WHERE CEDULA = :cedula", con))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("hash", hash);
                cmd.Parameters.Add("cedula", OracleDbType.Char, 10).Value = cedula;
                con.Open();
                if (cmd.ExecuteNonQuery() != 1) throw new InvalidOperationException("No se pudo cambiar la clave del empleado.");
            }
        }

        public void Guardar(Empleado empleado)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                con.Open();
                using (OracleTransaction tx = con.BeginTransaction())
                {
                    try
                    {
                        BloquearIdentidades(con, tx);
                        using (OracleCommand cmd = new OracleCommand(
                            SqlGuardarPersona, con))
                        {
                            cmd.Transaction = tx;
                            cmd.BindByName = true;
                            AgregarParametrosPersona(cmd, empleado);
                            cmd.ExecuteNonQuery();
                        }
                        using (OracleCommand cmd = new OracleCommand("INSERT INTO TALLER.EMPLEADO (CEDULA, CARGO_CARGOID, ACTIVO) VALUES (:cedula, :cargoId, :activo)", con))
                        {
                            cmd.Transaction = tx;
                            cmd.BindByName = true;
                            cmd.Parameters.Add("cedula", OracleDbType.Char, 10).Value = empleado.Cedula;
                            cmd.Parameters.Add(new OracleParameter("cargoId", empleado.Cargo.CargoId));
                            cmd.Parameters.Add("activo", OracleDbType.Char, 1).Value = empleado.Activo ? "S" : "N";
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                    }
                    catch { tx.Rollback(); throw; }
                }
            }
        }

        public List<Empleado> Consultar()
        {
            List<Empleado> empleados = new List<Empleado>();
            const string sql = @"SELECT P.CEDULA, P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE, P.PRIMER_APELLIDO, P.SEGUNDO_APELLIDO, P.SEXO, P.TELEFONO,
                                        P.DIRECCION, P.CORREOELECTRONICO, E.ACTIVO, C.CARGOID, C.NOMBRECARGO
                                 FROM TALLER.EMPLEADO E
                                 JOIN TALLER.PERSONA P ON P.CEDULA = E.CEDULA
                                 JOIN TALLER.CARGO C ON C.CARGOID = E.CARGO_CARGOID
                                 ORDER BY P.PRIMER_APELLIDO, P.SEGUNDO_APELLIDO, P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE";
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(sql, con))
            {
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) empleados.Add(Mapear(reader));
                }
                    
            }
            return empleados;
        }

        public Empleado BuscarCedula(string cedula)
        {
            const string sql = @"SELECT P.CEDULA, P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE, P.PRIMER_APELLIDO, P.SEGUNDO_APELLIDO, P.SEXO, P.TELEFONO,
                                        P.DIRECCION, P.CORREOELECTRONICO, E.ACTIVO, C.CARGOID, C.NOMBRECARGO
                                 FROM TALLER.EMPLEADO E
                                 JOIN TALLER.PERSONA P ON P.CEDULA = E.CEDULA
                                 JOIN TALLER.CARGO C ON C.CARGOID = E.CARGO_CARGOID
                                 WHERE E.CEDULA = :cedula";
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(sql, con))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("cedula", OracleDbType.Char, 10).Value = cedula;

                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    return reader.Read() ? Mapear(reader) : null;
                }
                   
            }
        }

        public void Modificar(Empleado empleado)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                con.Open();
                using (OracleTransaction tx = con.BeginTransaction())
                {
                    try
                    {
                        using (OracleCommand cmd = new OracleCommand(
                            @"UPDATE TALLER.PERSONA SET PRIMER_NOMBRE = :primerNombre, SEGUNDO_NOMBRE = :segundoNombre,
                              PRIMER_APELLIDO = :primerApellido, SEGUNDO_APELLIDO = :segundoApellido, SEXO = :sexo,
                              TELEFONO = :telefono, DIRECCION = :direccion, CORREOELECTRONICO = :correo
                              WHERE CEDULA = :cedula AND EXISTS (SELECT 1 FROM TALLER.EMPLEADO E WHERE E.CEDULA = :cedula)", con))
                        {
                            cmd.Transaction = tx;
                            cmd.BindByName = true;
                            AgregarParametrosPersona(cmd, empleado);
                            cmd.ExecuteNonQuery();
                        }
                        using (OracleCommand cmd = new OracleCommand("UPDATE TALLER.EMPLEADO SET CARGO_CARGOID = :cargoId, ACTIVO = :activo WHERE CEDULA = :cedula", con))
                        {
                            cmd.Transaction = tx;
                            cmd.BindByName = true;
                            cmd.Parameters.Add(new OracleParameter("cargoId", empleado.Cargo.CargoId));
                            cmd.Parameters.Add("activo", OracleDbType.Char, 1).Value = empleado.Activo ? "S" : "N";
                            cmd.Parameters.Add("cedula", OracleDbType.Char, 10).Value = empleado.Cedula;
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                    }
                    catch { tx.Rollback(); throw; }
                }
            }
        }

        public void Eliminar(Empleado empleado)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                con.Open();
                using (OracleTransaction tx = con.BeginTransaction())
                {
                    try
                    {
                        BloquearIdentidades(con, tx);
                        using (OracleCommand cmd = new OracleCommand("DELETE FROM TALLER.EMPLEADO WHERE CEDULA = :cedula", con))
                        {
                            cmd.Transaction = tx;
                            cmd.Parameters.Add("cedula", OracleDbType.Char, 10).Value = empleado.Cedula;
                            cmd.ExecuteNonQuery();
                        }
                        using (OracleCommand cmd = new OracleCommand("DELETE FROM TALLER.PERSONA P WHERE P.CEDULA = :cedula AND NOT EXISTS (SELECT 1 FROM TALLER.CLIENTE C WHERE C.CEDULA = P.CEDULA) AND NOT EXISTS (SELECT 1 FROM TALLER.EMPLEADO E WHERE E.CEDULA = P.CEDULA)", con))
                        {
                            cmd.Transaction = tx;
                            cmd.Parameters.Add("cedula", OracleDbType.Char, 10).Value = empleado.Cedula;
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                    }
                    catch { tx.Rollback(); throw; }
                }
            }
        }

        private static Empleado Mapear(OracleDataReader reader)
        {
            return new Empleado
            {
                Cedula = reader["CEDULA"].ToString().Trim(),
                PrimerNombre = reader["PRIMER_NOMBRE"].ToString(),
                SegundoNombre = reader["SEGUNDO_NOMBRE"] == DBNull.Value ? null : reader["SEGUNDO_NOMBRE"].ToString(),
                PrimerApellido = reader["PRIMER_APELLIDO"].ToString(),
                SegundoApellido = reader["SEGUNDO_APELLIDO"] == DBNull.Value ? null : reader["SEGUNDO_APELLIDO"].ToString(),
                Sexo = reader["SEXO"].ToString().Trim(),
                Activo = reader["ACTIVO"] != DBNull.Value && reader["ACTIVO"].ToString().Trim() == "S",
                Telefono = reader["TELEFONO"] == DBNull.Value ? null : reader["TELEFONO"].ToString(),
                Direccion = reader["DIRECCION"] == DBNull.Value ? null : reader["DIRECCION"].ToString(),
                CorreoElectronico = reader["CORREOELECTRONICO"] == DBNull.Value ? null : reader["CORREOELECTRONICO"].ToString(),
                Cargo = new Cargo { CargoId = Convert.ToInt32(reader["CARGOID"]), NombreCargo = reader["NOMBRECARGO"].ToString() }
            };
        }

        private void AgregarParametrosPersona(OracleCommand cmd, Empleado empleado)
        {
            cmd.Parameters.Add("cedula", OracleDbType.Char, 10).Value = empleado.Cedula;
            cmd.Parameters.Add(new OracleParameter("primerNombre", empleado.PrimerNombre));
            cmd.Parameters.Add(new OracleParameter("segundoNombre", (object)empleado.SegundoNombre ?? DBNull.Value));
            cmd.Parameters.Add(new OracleParameter("primerApellido", empleado.PrimerApellido));
            cmd.Parameters.Add(new OracleParameter("segundoApellido", (object)empleado.SegundoApellido ?? DBNull.Value));
            cmd.Parameters.Add(new OracleParameter("sexo", empleado.Sexo));
            cmd.Parameters.Add(new OracleParameter("telefono", (object)empleado.Telefono ?? DBNull.Value));
            cmd.Parameters.Add(new OracleParameter("direccion", (object)empleado.Direccion ?? DBNull.Value));
            cmd.Parameters.Add(new OracleParameter("correo", (object)empleado.CorreoElectronico ?? DBNull.Value));
        }
    }
}
