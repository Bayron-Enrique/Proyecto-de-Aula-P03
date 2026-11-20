using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace DATOS
{
    public class ProveedorRepository : ManejadorConexion, IRepository<Proveedor>
    {
        public void Guardar(Proveedor proveedor)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_PROVEEDOR.GUARDAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure; AgregarParams(comando, proveedor);
                conexion.Open(); comando.ExecuteNonQuery();
            }
        }
        public void Modificar(Proveedor proveedor)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_PROVEEDOR.MODIFICAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure; AgregarParams(comando, proveedor);
                conexion.Open(); comando.ExecuteNonQuery();
            }
        }
        public List<Proveedor> Consultar()
        {
            List<Proveedor> proveedores = new List<Proveedor>();
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_PROVEEDOR.CONSULTAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                conexion.Open(); using (OracleDataReader reader = comando.ExecuteReader())
                    while (reader.Read()) proveedores.Add(Mapear(reader));
            }
            return proveedores;
        }
        public Proveedor BuscarNit(string nit)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_PROVEEDOR.BUSCAR_NIT", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_nit", OracleDbType.Varchar2).Value = nit;
                comando.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                conexion.Open(); using (OracleDataReader reader = comando.ExecuteReader()) return reader.Read() ? Mapear(reader) : null;
            }
        }
        public void Eliminar(Proveedor proveedor)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_PROVEEDOR.ELIMINAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_nit", OracleDbType.Varchar2).Value = proveedor.Nit;
                conexion.Open(); comando.ExecuteNonQuery();
            }
        }
        private static void AgregarParams(OracleCommand comando, Proveedor proveedor)
        {
            comando.BindByName = true;
            comando.Parameters.Add("p_nit", OracleDbType.Varchar2).Value = proveedor.Nit;
            comando.Parameters.Add("p_razon_social", OracleDbType.Varchar2).Value = proveedor.RazonSocial;
            comando.Parameters.Add("p_telefono", OracleDbType.Varchar2).Value = (object)proveedor.Telefono ?? System.DBNull.Value;
        }
        private static Proveedor Mapear(OracleDataReader reader)
        {
            return new Proveedor { Nit = reader["NIT"].ToString(), RazonSocial = reader["RAZONSOCIAL"].ToString(),
                Telefono = reader["TELEFONO"] == System.DBNull.Value ? null : reader["TELEFONO"].ToString(), Email = null, Direccion = null };
        }
    }
}
