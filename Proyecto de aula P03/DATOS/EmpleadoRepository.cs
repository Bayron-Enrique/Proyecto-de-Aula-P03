using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace DATOS
{
    public class EmpleadoRepository : ManejadorConexion, IRepository<Empleado>
    {
        public void Guardar(Empleado empleado)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                con.Open();
                using (OracleTransaction tx = con.BeginTransaction())
                {
                    try
                    {
                        using (OracleCommand cmd = new OracleCommand(
                            @"INSERT INTO TALLER.PERSONA (CEDULA, NOMBRES, APELLIDOS, SEXO, TELEFONO, DIRECCION, CORREOELECTRONICO)
                              VALUES (:cedula, :nombres, :apellidos, :sexo, :telefono, :direccion, :correo)", con))
                        {
                            cmd.Transaction = tx;
                            cmd.BindByName = true;
                            AgregarParametrosPersona(cmd, empleado);
                            cmd.ExecuteNonQuery();
                        }
                        using (OracleCommand cmd = new OracleCommand("INSERT INTO TALLER.EMPLEADO (CEDULA, CARGO_CARGOID) VALUES (:cedula, :cargoId)", con))
                        {
                            cmd.Transaction = tx;
                            cmd.Parameters.Add(new OracleParameter("cedula", empleado.Cedula));
                            cmd.Parameters.Add(new OracleParameter("cargoId", empleado.Cargo.CargoId));
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
            const string sql = @"SELECT P.CEDULA, P.NOMBRES, P.APELLIDOS, P.SEXO, P.TELEFONO,
                                        P.DIRECCION, P.CORREOELECTRONICO, C.CARGOID, C.NOMBRECARGO
                                 FROM TALLER.EMPLEADO E
                                 JOIN TALLER.PERSONA P ON P.CEDULA = E.CEDULA
                                 JOIN TALLER.CARGO C ON C.CARGOID = E.CARGO_CARGOID
                                 ORDER BY P.APELLIDOS, P.NOMBRES";
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
            const string sql = @"SELECT P.CEDULA, P.NOMBRES, P.APELLIDOS, P.SEXO, P.TELEFONO,
                                        P.DIRECCION, P.CORREOELECTRONICO, C.CARGOID, C.NOMBRECARGO
                                 FROM TALLER.EMPLEADO E
                                 JOIN TALLER.PERSONA P ON P.CEDULA = E.CEDULA
                                 JOIN TALLER.CARGO C ON C.CARGOID = E.CARGO_CARGOID
                                 WHERE E.CEDULA = :cedula";
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(sql, con))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add(new OracleParameter("cedula", cedula));

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
                            @"UPDATE TALLER.PERSONA SET NOMBRES = :nombres, APELLIDOS = :apellidos, SEXO = :sexo,
                              TELEFONO = :telefono, DIRECCION = :direccion, CORREOELECTRONICO = :correo
                              WHERE CEDULA = :cedula AND EXISTS (SELECT 1 FROM TALLER.EMPLEADO E WHERE E.CEDULA = :cedula)", con))
                        {
                            cmd.Transaction = tx;
                            cmd.BindByName = true;
                            AgregarParametrosPersona(cmd, empleado);
                            cmd.ExecuteNonQuery();
                        }
                        using (OracleCommand cmd = new OracleCommand("UPDATE TALLER.EMPLEADO SET CARGO_CARGOID = :cargoId WHERE CEDULA = :cedula", con))
                        {
                            cmd.Transaction = tx;
                            cmd.BindByName = true;
                            cmd.Parameters.Add(new OracleParameter("cargoId", empleado.Cargo.CargoId));
                            cmd.Parameters.Add(new OracleParameter("cedula", empleado.Cedula));
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
                        using (OracleCommand cmd = new OracleCommand("DELETE FROM TALLER.EMPLEADO WHERE CEDULA = :cedula", con))
                        {
                            cmd.Transaction = tx;
                            cmd.Parameters.Add(new OracleParameter("cedula", empleado.Cedula));
                            cmd.ExecuteNonQuery();
                        }
                        using (OracleCommand cmd = new OracleCommand("DELETE FROM TALLER.PERSONA WHERE CEDULA = :cedula", con))
                        {
                            cmd.Transaction = tx;
                            cmd.Parameters.Add(new OracleParameter("cedula", empleado.Cedula));
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
                Cedula = reader["CEDULA"].ToString().Trim(), Nombre = reader["NOMBRES"].ToString(),
                Apellido = reader["APELLIDOS"].ToString(), Sexo = reader["SEXO"].ToString().Trim(),
                Telefono = reader["TELEFONO"] == DBNull.Value ? null : reader["TELEFONO"].ToString(),
                Direccion = reader["DIRECCION"] == DBNull.Value ? null : reader["DIRECCION"].ToString(),
                CorreoElectronico = reader["CORREOELECTRONICO"] == DBNull.Value ? null : reader["CORREOELECTRONICO"].ToString(),
                Cargo = new Cargo { CargoId = Convert.ToInt32(reader["CARGOID"]), NombreCargo = reader["NOMBRECARGO"].ToString() }
            };
        }

        private void AgregarParametrosPersona(OracleCommand cmd, Empleado empleado)
        {
            cmd.Parameters.Add(new OracleParameter("cedula", empleado.Cedula));
            cmd.Parameters.Add(new OracleParameter("nombres", empleado.Nombre));
            cmd.Parameters.Add(new OracleParameter("apellidos", empleado.Apellido));
            cmd.Parameters.Add(new OracleParameter("sexo", empleado.Sexo));
            cmd.Parameters.Add(new OracleParameter("telefono", (object)empleado.Telefono ?? DBNull.Value));
            cmd.Parameters.Add(new OracleParameter("direccion", (object)empleado.Direccion ?? DBNull.Value));
            cmd.Parameters.Add(new OracleParameter("correo", (object)empleado.CorreoElectronico ?? DBNull.Value));
        }
    }
}
