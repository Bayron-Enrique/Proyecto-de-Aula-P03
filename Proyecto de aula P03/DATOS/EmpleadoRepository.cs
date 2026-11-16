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
                            @"INSERT INTO PERSONA (CEDULA, NOMBRES, APELLIDOS, SEXO, TELEFONO, DIRECCION, CORREOELECTRONICO)
                              VALUES (:cedula, :nombres, :apellidos, :sexo, :telefono, :direccion, :correo)", con))
                        {
                            cmd.Transaction = tx;
                            AgregarParametrosPersona(cmd, empleado);
                            cmd.ExecuteNonQuery();
                        }
                        using (OracleCommand cmd = new OracleCommand("INSERT INTO EMPLEADO (CEDULA, CARGO_CARGOID) VALUES (:cedula, :cargoId)", con))
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
            // consulta una funcion que retorna un cursor de empleados
            List<Empleado> empleados = new List<Empleado>();
            const string NombreFuncion = "FN_OBTENER_EMPLEADOS";
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(NombreFuncion, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter paramCursor = new OracleParameter("p_cursor", OracleDbType.RefCursor);
                paramCursor.Direction = ParameterDirection.ReturnValue; // obtener el cursor de retorno
                cmd.Parameters.Add(paramCursor);

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
            const string NombreFuncion = "FN_OBTENER_EMPLEADO_POR_CEDULA";
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(NombreFuncion, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter paramCursor = new OracleParameter("p_cursor", OracleDbType.RefCursor);
                paramCursor.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(paramCursor);
                cmd.Parameters.Add(new OracleParameter("p_cedula", cedula));

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
                            @"UPDATE PERSONA P SET NOMBRES = :nombres, APELLIDOS = :apellidos, SEXO = :sexo,
                              TELEFONO = :telefono, DIRECCION = :direccion, CORREOELECTRONICO = :correo
                              WHERE P.CEDULA = :cedula AND EXISTS (SELECT 1 FROM EMPLEADO E WHERE E.CEDULA = P.CEDULA)", con))
                        {
                            cmd.Transaction = tx;
                            AgregarParametrosPersona(cmd, empleado);
                            cmd.ExecuteNonQuery();
                        }
                        using (OracleCommand cmd = new OracleCommand("UPDATE EMPLEADO SET CARGO_CARGOID = :cargoId WHERE CEDULA = :cedula", con))
                        {
                            cmd.Transaction = tx;
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
                        using (OracleCommand cmd = new OracleCommand("DELETE FROM EMPLEADO WHERE CEDULA = :cedula", con))
                        {
                            cmd.Transaction = tx;
                            cmd.Parameters.Add(new OracleParameter("cedula", empleado.Cedula));
                            cmd.ExecuteNonQuery();
                        }
                        using (OracleCommand cmd = new OracleCommand("DELETE FROM PERSONA WHERE CEDULA = :cedula", con))
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
