using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace DATOS
{
    public class ArticuloRepository : ManejadorConexion, IRepository<Articulo>
    {
        public int Contar()
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("SELECT COUNT(*) FROM TALLER.ARTICULO", con))
            {
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private const string SelectSql = @"SELECT A.ARTICULOID, A.NOMBRE, A.PRECIO, A.PORCENTAJEIVA,
                                                 A.CANTIDAD, A.DESCRIPCION, A.DURACION, A.PROVEEDOR_NIT,
                                                 T.NOMBRE AS TIPO_ARTICULO, P.RAZONSOCIAL AS PROVEEDOR_NOMBRE
                                          FROM TALLER.ARTICULO A
                                          JOIN TALLER.TIPO_ARTICULO T ON T.TIPO_ARTICULO_ID = A.TIPO_ARTICULO_ID
                                          LEFT JOIN TALLER.PROVEEDOR P ON P.NIT = A.PROVEEDOR_NIT";

        public void Guardar(Articulo articulo)
        {
            const string sql = @"INSERT INTO TALLER.ARTICULO
                                 (ARTICULOID, NOMBRE, PRECIO, PORCENTAJEIVA, CANTIDAD, DESCRIPCION,
                                  DURACION, PROVEEDOR_NIT, TIPO_ARTICULO_ID)
                                 VALUES (:articuloId, :nombre, :precio, :iva, :cantidad, :descripcion,
                                         :duracion, :nitProveedor,
                                         (SELECT TIPO_ARTICULO_ID FROM TALLER.TIPO_ARTICULO WHERE UPPER(NOMBRE) = UPPER(:tipo)))";
            using (OracleConnection con = ObtenerConexion())
            {
                con.Open();
                using (OracleCommand secuencia = new OracleCommand("SELECT TALLER.SEQ_ARTICULOID.NEXTVAL FROM DUAL", con))
                    articulo.ArticuloId = Convert.ToInt32(secuencia.ExecuteScalar());
                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    cmd.BindByName = true;
                    AgregarParametros(cmd, articulo);
                    cmd.Parameters.Add("articuloId", articulo.ArticuloId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Articulo> Consultar()
        {
            var articulos = new List<Articulo>();
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(SelectSql + " ORDER BY A.NOMBRE", con))
            {
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                    while (reader.Read()) articulos.Add(Mapear(reader));
            }
            return articulos;
        }

        public Articulo BuscarId(int articuloId)
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(SelectSql + " WHERE A.ARTICULOID = :articuloId", con))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("articuloId", articuloId);
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                    return reader.Read() ? Mapear(reader) : null;
            }
        }

        public void Modificar(Articulo articulo)
        {
            const string sql = @"UPDATE TALLER.ARTICULO SET NOMBRE = :nombre, PRECIO = :precio,
                PORCENTAJEIVA = :iva, CANTIDAD = :cantidad, DESCRIPCION = :descripcion,
                DURACION = :duracion, PROVEEDOR_NIT = :nitProveedor,
                TIPO_ARTICULO_ID = (SELECT TIPO_ARTICULO_ID FROM TALLER.TIPO_ARTICULO WHERE UPPER(NOMBRE) = UPPER(:tipo))
                WHERE ARTICULOID = :articuloId";
            using (var con = ObtenerConexion())
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    try
                    {
                        string tipo;
                        using (var bloquear = new OracleCommand(@"SELECT T.NOMBRE FROM TALLER.ARTICULO A
                            JOIN TALLER.TIPO_ARTICULO T ON T.TIPO_ARTICULO_ID = A.TIPO_ARTICULO_ID
                            WHERE A.ARTICULOID = :id FOR UPDATE OF A.TIPO_ARTICULO_ID", con))
                        {
                            bloquear.Transaction = tx;
                            bloquear.Parameters.Add("id", articulo.ArticuloId);
                            object valor = bloquear.ExecuteScalar();
                            if (valor == null) throw new InvalidOperationException("El artículo ya no existe.");
                            tipo = Convert.ToString(valor);
                        }
                        bool mismoTipo = string.Equals(tipo, articulo is Repuesto ? "Repuesto" : "Servicio", StringComparison.OrdinalIgnoreCase);
                        using (var movimientos = new OracleCommand("SELECT COUNT(*) FROM TALLER.DETALLE_FACTURA WHERE ARTICULO_ARTICULOID = :id", con))
                        {
                            movimientos.Transaction = tx;
                            movimientos.Parameters.Add("id", articulo.ArticuloId);
                            if (!mismoTipo && Convert.ToInt32(movimientos.ExecuteScalar()) > 0)
                                throw new InvalidOperationException("No se puede cambiar el tipo de un artículo con movimientos de facturación.");
                        }
                        using (var cmd = new OracleCommand(sql, con))
                        {
                            cmd.Transaction = tx;
                            AgregarParametros(cmd, articulo);
                            cmd.Parameters.Add("articuloId", articulo.ArticuloId);
                            if (cmd.ExecuteNonQuery() != 1) throw new InvalidOperationException("El artículo ya no existe.");
                        }
                        tx.Commit();
                    }
                    catch { tx.Rollback(); throw; }
                }
            }
        }

        public bool TieneMovimientos(int articuloId)
        {
            using (var con = ObtenerConexion())
            using (var cmd = new OracleCommand("SELECT COUNT(*) FROM TALLER.DETALLE_FACTURA WHERE ARTICULO_ARTICULOID = :id", con))
            {
                cmd.Parameters.Add("id", articuloId);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public void Eliminar(int articuloId)
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("DELETE FROM TALLER.ARTICULO WHERE ARTICULOID = :articuloId", con))
            {
                cmd.Parameters.Add("articuloId", articuloId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private static void AgregarParametros(OracleCommand cmd, Articulo articulo)
        {
            cmd.BindByName = true;
            cmd.Parameters.Add("nombre", articulo.Nombre);
            cmd.Parameters.Add("precio", articulo.PrecioVenta);
            cmd.Parameters.Add("iva", articulo.PorcentajeIVA);
            if (articulo is Repuesto repuesto)
            {
                if (repuesto.Proveedor == null || string.IsNullOrWhiteSpace(repuesto.Proveedor.Nit))
                    throw new ArgumentException("El repuesto debe estar asociado a un proveedor.");
                cmd.Parameters.Add("cantidad", repuesto.Cantidad);
                cmd.Parameters.Add("descripcion", DBNull.Value);
                cmd.Parameters.Add("duracion", DBNull.Value);
                cmd.Parameters.Add("nitProveedor", repuesto.Proveedor.Nit);
                cmd.Parameters.Add("tipo", "Repuesto");
            }
            else if (articulo is Servicio servicio)
            {
                cmd.Parameters.Add("cantidad", DBNull.Value);
                cmd.Parameters.Add("descripcion", (object)servicio.Descripcion ?? DBNull.Value);
                cmd.Parameters.Add("duracion", servicio.Duracion);
                cmd.Parameters.Add("nitProveedor", DBNull.Value);
                cmd.Parameters.Add("tipo", "Servicio");
            }
            else
            {
                throw new ArgumentException("El artículo debe ser un Repuesto o un Servicio.");
            }
        }

        private static Articulo Mapear(OracleDataReader reader)
        {
            string tipo = reader["TIPO_ARTICULO"].ToString();
            int id = Convert.ToInt32(reader["ARTICULOID"]);
            string nombre = reader["NOMBRE"].ToString();
            decimal precio = Convert.ToDecimal(reader["PRECIO"]);
            decimal iva = Convert.ToDecimal(reader["PORCENTAJEIVA"]);
            if (string.Equals(tipo, "Repuesto", StringComparison.OrdinalIgnoreCase))
            {
                return new Repuesto
                {
                    ArticuloId = id, Nombre = nombre, PrecioVenta = precio, PorcentajeIVA = iva,
                    Cantidad = reader["CANTIDAD"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CANTIDAD"]),
                    Proveedor = reader["PROVEEDOR_NIT"] == DBNull.Value ? null : new Proveedor
                    {
                        Nit = reader["PROVEEDOR_NIT"].ToString(),
                        RazonSocial = reader["PROVEEDOR_NOMBRE"] == DBNull.Value ? null : reader["PROVEEDOR_NOMBRE"].ToString()
                    }
                };
            }
            if (string.Equals(tipo, "Servicio", StringComparison.OrdinalIgnoreCase))
            {
                return new Servicio
                {
                    ArticuloId = id, Nombre = nombre, PrecioVenta = precio, PorcentajeIVA = iva,
                    Duracion = reader["DURACION"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DURACION"]),
                    Descripcion = reader["DESCRIPCION"] == DBNull.Value ? null : reader["DESCRIPCION"].ToString()
                };
            }
            throw new InvalidOperationException("El tipo de artículo guardado en Oracle no es Repuesto ni Servicio: " + tipo);
        }
    }
}
