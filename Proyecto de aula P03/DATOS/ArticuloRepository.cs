using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace DATOS
{
    public class ArticuloRepository : ManejadorConexion, IRepository<Articulo>
    {
        public int Contar()
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_ARTICULO.CONTAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_total", OracleDbType.Int32).Direction = ParameterDirection.Output;
                conexion.Open(); comando.ExecuteNonQuery();
                return Convert.ToInt32(comando.Parameters["p_total"].Value.ToString());
            }
        }
        public void Guardar(Articulo articulo)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_ARTICULO.GUARDAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                AgregarParametros(comando, articulo);
                OracleParameter parametroId = comando.Parameters.Add("p_id", OracleDbType.Int32);
                parametroId.Direction = ParameterDirection.Output;
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); articulo.ArticuloId = Convert.ToInt32(parametroId.Value.ToString()); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }
        public void Modificar(Articulo articulo)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_ARTICULO.MODIFICAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                AgregarParametros(comando, articulo);
                comando.Parameters.Add("p_id", OracleDbType.Int32).Value = articulo.ArticuloId;
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }
        public List<Articulo> Consultar()
        {
            List<Articulo> articulos = new List<Articulo>();
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_ARTICULO.CONSULTAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                conexion.Open(); using (OracleDataReader reader = comando.ExecuteReader())
                    while (reader.Read()) articulos.Add(Mapear(reader));
            }
            return articulos;
        }
        public Articulo BuscarId(int id)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_ARTICULO.BUSCAR_ID", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_id", OracleDbType.Int32).Value = id;
                comando.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                conexion.Open(); using (OracleDataReader reader = comando.ExecuteReader()) return reader.Read() ? Mapear(reader) : null;
            }
        }
        public bool TieneMovimientos(int id)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_ARTICULO.TIENE_MOVIMIENTOS", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_id", OracleDbType.Int32).Value = id;
                comando.Parameters.Add("p_resultado", OracleDbType.Int32).Direction = ParameterDirection.Output;
                conexion.Open(); comando.ExecuteNonQuery();
                return Convert.ToInt32(comando.Parameters["p_resultado"].Value.ToString()) > 0;
            }
        }
        public void Eliminar(int id)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_ARTICULO.ELIMINAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_id", OracleDbType.Int32).Value = id;
                conexion.Open(); comando.ExecuteNonQuery();
            }
        }
        private static void AgregarParametros(OracleCommand comando, Articulo articulo)
        {
            comando.BindByName = true;
            comando.Parameters.Add("p_nombre", OracleDbType.Varchar2).Value = articulo.Nombre;
            comando.Parameters.Add("p_precio", OracleDbType.Decimal).Value = articulo.PrecioVenta;
            comando.Parameters.Add("p_iva", OracleDbType.Decimal).Value = articulo.PorcentajeIVA;
            if (articulo is Repuesto repuesto)
            {
                if (repuesto.Proveedor == null || string.IsNullOrWhiteSpace(repuesto.Proveedor.Nit))
                    throw new ArgumentException("El repuesto debe estar asociado a un proveedor.");
                comando.Parameters.Add("p_cantidad", OracleDbType.Decimal).Value = repuesto.Cantidad;
                comando.Parameters.Add("p_descripcion", OracleDbType.Varchar2).Value = DBNull.Value;
                comando.Parameters.Add("p_duracion", OracleDbType.Decimal).Value = DBNull.Value;
                comando.Parameters.Add("p_proveedor", OracleDbType.Varchar2).Value = repuesto.Proveedor.Nit;
                comando.Parameters.Add("p_tipo", OracleDbType.Varchar2).Value = "Repuesto";
            }
            else if (articulo is Servicio servicio)
            {
                comando.Parameters.Add("p_cantidad", OracleDbType.Decimal).Value = DBNull.Value;
                comando.Parameters.Add("p_descripcion", OracleDbType.Varchar2).Value = (object)servicio.Descripcion ?? DBNull.Value;
                comando.Parameters.Add("p_duracion", OracleDbType.Decimal).Value = servicio.Duracion;
                comando.Parameters.Add("p_proveedor", OracleDbType.Varchar2).Value = DBNull.Value;
                comando.Parameters.Add("p_tipo", OracleDbType.Varchar2).Value = "Servicio";
            }
            else throw new ArgumentException("El artículo debe ser un Repuesto o un Servicio.");
        }
        private static Articulo Mapear(OracleDataReader reader)
        {
            string tipo = reader["TIPO_ARTICULO"].ToString();
            int id = Convert.ToInt32(reader["ARTICULOID"]); string nombre = reader["NOMBRE"].ToString();
            decimal precio = Convert.ToDecimal(reader["PRECIO"]); decimal iva = Convert.ToDecimal(reader["PORCENTAJEIVA"]);
            if (string.Equals(tipo, "Repuesto", StringComparison.OrdinalIgnoreCase))
                return new Repuesto { ArticuloId=id, Nombre=nombre, PrecioVenta=precio, PorcentajeIVA=iva,
                    Cantidad=reader["CANTIDAD"]==DBNull.Value?0:Convert.ToInt32(reader["CANTIDAD"]),
                    Proveedor=reader["PROVEEDOR_NIT"]==DBNull.Value?null:new Proveedor { Nit=reader["PROVEEDOR_NIT"].ToString(),
                        RazonSocial=reader["PROVEEDOR_NOMBRE"]==DBNull.Value?null:reader["PROVEEDOR_NOMBRE"].ToString() } };
            if (string.Equals(tipo, "Servicio", StringComparison.OrdinalIgnoreCase))
                return new Servicio { ArticuloId=id, Nombre=nombre, PrecioVenta=precio, PorcentajeIVA=iva,
                    Duracion=reader["DURACION"]==DBNull.Value?0:Convert.ToInt32(reader["DURACION"]),
                    Descripcion=reader["DESCRIPCION"]==DBNull.Value?null:reader["DESCRIPCION"].ToString() };
            throw new InvalidOperationException("El tipo de artículo guardado en Oracle no es Repuesto ni Servicio: " + tipo);
        }
    }
}
