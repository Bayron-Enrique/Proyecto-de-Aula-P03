using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace DATOS
{
    public class ProveedorRepository : ManejadorConexion, IRepository<Proveedor>
    {
        public void Guardar(Proveedor proveedor)
        {
            const string sql = @"INSERT INTO TALLER.PROVEEDOR (NIT, RAZONSOCIAL, TELEFONO)
                                 VALUES (:nit, :razonSocial, :telefono)";
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(sql, con))
            {
                AgregarParametros(cmd, proveedor);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Proveedor> Consultar()
        {
            var proveedores = new List<Proveedor>();
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("SELECT NIT, RAZONSOCIAL, TELEFONO FROM TALLER.PROVEEDOR", con))
            {
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                    while (reader.Read()) proveedores.Add(Mapear(reader));
            }
            return proveedores;
        }

        public Proveedor BuscarNit(string nit)
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("SELECT NIT, RAZONSOCIAL, TELEFONO FROM TALLER.PROVEEDOR WHERE NIT = :nit", con))
            {
                cmd.Parameters.Add("nit", nit);
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                    return reader.Read() ? Mapear(reader) : null;
            }
        }

        public void Eliminar(Proveedor proveedor)
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("DELETE FROM TALLER.PROVEEDOR WHERE NIT = :nit", con))
            {
                cmd.Parameters.Add("nit", proveedor.Nit);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Modificar(Proveedor proveedor)
        {
            const string sql = @"UPDATE TALLER.PROVEEDOR SET RAZONSOCIAL = :razonSocial,
                                 TELEFONO = :telefono WHERE NIT = :nit";
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(sql, con))
            {
                AgregarParametros(cmd, proveedor);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private static void AgregarParametros(OracleCommand cmd, Proveedor proveedor)
        {
            cmd.BindByName = true;
            cmd.Parameters.Add("nit", proveedor.Nit);
            cmd.Parameters.Add("razonSocial", proveedor.RazonSocial);
            cmd.Parameters.Add("telefono", (object)proveedor.Telefono ?? DBNull.Value);
        }

        private static Proveedor Mapear(OracleDataReader reader)
        {
            return new Proveedor
            {
                Nit = reader["NIT"].ToString(),
                RazonSocial = reader["RAZONSOCIAL"].ToString(),
                Telefono = reader["TELEFONO"] == DBNull.Value ? null : reader["TELEFONO"].ToString(),
                Email = null,
                Direccion = null
            };
        }
    }
}
