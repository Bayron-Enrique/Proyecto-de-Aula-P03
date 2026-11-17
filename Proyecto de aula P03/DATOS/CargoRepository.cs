using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace DATOS
{
    public class CargoRepository : ManejadorConexion, IRepository<Cargo>
    {
        public void Guardar(Cargo cargo)
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(
                "INSERT INTO CARGO (CARGOID, NOMBRECARGO) VALUES (:cargoId, :nombreCargo)", con))
            {
                AgregarParametros(cmd, cargo);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Cargo> Consultar()
        {
            List<Cargo> cargos = new List<Cargo>();
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("SELECT CARGOID, NOMBRECARGO FROM CARGO", con))
            {
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                    while (reader.Read()) cargos.Add(Mapear(reader));
            }
            return cargos;
        }

        public Cargo BuscarId(int cargoId)
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("SELECT CARGOID, NOMBRECARGO FROM CARGO WHERE CARGOID = :cargoId", con))
            {
                cmd.Parameters.Add(new OracleParameter("cargoId", cargoId));
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                    return reader.Read() ? Mapear(reader) : null;
            }
        }

        public void Modificar(Cargo cargo)
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("UPDATE CARGO SET NOMBRECARGO = :nombreCargo WHERE CARGOID = :cargoId", con))
            {
                AgregarParametros(cmd, cargo);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(Cargo cargo)
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("DELETE FROM CARGO WHERE CARGOID = :cargoId", con))
            {
                cmd.Parameters.Add(new OracleParameter("cargoId", cargo.CargoId));
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private static Cargo Mapear(OracleDataReader reader)
        {
            return new Cargo { CargoId = Convert.ToInt32(reader["CARGOID"]), NombreCargo = reader["NOMBRECARGO"].ToString() };
        }

        private void AgregarParametros(OracleCommand cmd, Cargo cargo)
        {
            cmd.Parameters.Add(new OracleParameter("cargoId", cargo.CargoId));
            cmd.Parameters.Add(new OracleParameter("nombreCargo", cargo.NombreCargo));
        }
    }
}
