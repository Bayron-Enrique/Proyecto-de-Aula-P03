using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class ArticuloRepository : ManejadorConexion, IRepository<Articulo>
    {
        public void Guardar(Articulo articulo)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = @"INSERT INTO TALLER.ARTICULO
                                 (ARTICULOID, NOMBRE, PRECIO, PORCENTAJEIVA, CANTIDAD, DESCRIPCION, DURACION, PROVEEDOR_NIT, TIPO_ARTICULO_ID)
                                 VALUES (TALLER.SEQ_ARTICULOID.NEXTVAL, :nombre, :precioventa, :porcentajeiva, :cantidad, :descripcion, :duracion, :proveedorNit,
                                         (SELECT TIPO_ARTICULO_ID FROM TALLER.TIPO_ARTICULO WHERE UPPER(NOMBRE) = UPPER(:tipoArticulo)))";

                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    AgregarParams(cmd, articulo);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void AgregarParams(OracleCommand cmd, Articulo articulo)
        {
            cmd.BindByName = true;

            cmd.Parameters.Add(new OracleParameter("nombre", articulo.Nombre));
            cmd.Parameters.Add(new OracleParameter("precioventa", articulo.PrecioVenta));
            cmd.Parameters.Add(new OracleParameter("porcentajeiva", articulo.PorcentajeIVA));

            if (articulo is Repuesto repuesto)
            {
                cmd.Parameters.Add(new OracleParameter("cantidad", repuesto.Cantidad));
                cmd.Parameters.Add(new OracleParameter("proveedorNit", repuesto.Proveedor != null ? (object)repuesto.Proveedor.Nit : DBNull.Value));
                cmd.Parameters.Add(new OracleParameter("descripcion", DBNull.Value));
                cmd.Parameters.Add(new OracleParameter("duracion", 0));
            }
            else if (articulo is Servicio servicio)
            {
                cmd.Parameters.Add(new OracleParameter("duracion", servicio.Duracion));
                cmd.Parameters.Add(new OracleParameter("descripcion", string.IsNullOrEmpty(servicio.Descripcion) ? (object)DBNull.Value : servicio.Descripcion));
                cmd.Parameters.Add(new OracleParameter("cantidad", 0));
                cmd.Parameters.Add(new OracleParameter("proveedorNit", DBNull.Value));
            }
            else
            {
                cmd.Parameters.Add(new OracleParameter("cantidad", 0));
                cmd.Parameters.Add(new OracleParameter("descripcion", DBNull.Value));
                cmd.Parameters.Add(new OracleParameter("duracion", 0));
                cmd.Parameters.Add(new OracleParameter("proveedorNit", DBNull.Value));
            }

            cmd.Parameters.Add(new OracleParameter("tipoArticulo", articulo is Repuesto ? "Repuesto" : "Servicio"));
        }

        public List<Articulo> Consultar()
        {
            List<Articulo> articulos = new List<Articulo>();
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = @"SELECT A.ARTICULOID, A.NOMBRE, A.PRECIO, A.PORCENTAJEIVA, A.CANTIDAD,
                                      A.DESCRIPCION, A.DURACION, A.PROVEEDOR_NIT, A.TIPO_ARTICULO_ID,
                                      T.NOMBRE AS TIPO_ARTICULO
                               FROM TALLER.ARTICULO A
                               JOIN TALLER.TIPO_ARTICULO T ON T.TIPO_ARTICULO_ID = A.TIPO_ARTICULO_ID";

                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    con.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            articulos.Add(Mapear(reader));
                        }
                    }
                }
            }
            return articulos;
        }

        private Articulo Mapear(OracleDataReader reader)
        {
            string tipoArticulo = reader["TIPO_ARTICULO"].ToString();
            int id = Convert.ToInt32(reader["ARTICULOID"]);
            string nombre = reader["NOMBRE"].ToString();
            decimal precio = Convert.ToDecimal(reader["PRECIO"]);
            decimal iva = Convert.ToDecimal(reader["PORCENTAJEIVA"]);

            if (string.Equals(tipoArticulo, "Repuesto", StringComparison.OrdinalIgnoreCase))
            {
                return new Repuesto
                {
                    ArticuloId = id,
                    Nombre = nombre,
                    PrecioVenta = precio,
                    PorcentajeIVA = iva,
                    Cantidad = Convert.ToInt32(reader["CANTIDAD"]),
                    Proveedor = reader["PROVEEDOR_NIT"] != DBNull.Value ? new Proveedor { Nit = reader["PROVEEDOR_NIT"].ToString() } : null
                };
            }
            if (string.Equals(tipoArticulo, "Servicio", StringComparison.OrdinalIgnoreCase))
            {
                return new Servicio
                {
                    ArticuloId = id,
                    Nombre = nombre,
                    PrecioVenta = precio,
                    PorcentajeIVA = iva,
                    Duracion = Convert.ToInt32(reader["DURACION"]),
                    Descripcion = reader["DESCRIPCION"] != DBNull.Value ? reader["DESCRIPCION"].ToString() : string.Empty
                };
            }
            throw new InvalidOperationException("El tipo de artículo guardado en Oracle no es Repuesto ni Servicio: " + tipoArticulo);
        }

        public Articulo BuscarId(int articuloId)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = @"SELECT A.ARTICULOID, A.NOMBRE, A.PRECIO, A.PORCENTAJEIVA,
                                      A.CANTIDAD, A.DESCRIPCION, A.DURACION, A.PROVEEDOR_NIT,
                                      A.TIPO_ARTICULO_ID, T.NOMBRE AS TIPO_ARTICULO
                               FROM TALLER.ARTICULO A
                               JOIN TALLER.TIPO_ARTICULO T ON T.TIPO_ARTICULO_ID = A.TIPO_ARTICULO_ID
                               WHERE A.ARTICULOID = :articuloId";

                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    cmd.Parameters.Add(new OracleParameter("articuloId", articuloId));
                    con.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return Mapear(reader);
                        }
                    }
                }
            }

            return null;
        }

        public void Eliminar(int articuloId)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = @"DELETE FROM TALLER.ARTICULO WHERE ARTICULOID = :articuloId";

                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    cmd.Parameters.Add(new OracleParameter("articuloId", articuloId));
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Modificar(Articulo articulo)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = @"UPDATE TALLER.ARTICULO SET
                 NOMBRE = :nombre, 
                 PRECIO = :precioventa, 
                 PORCENTAJEIVA = :porcentajeiva, 
                 CANTIDAD = :cantidad, 
                 DESCRIPCION = :descripcion, 
                 DURACION = :duracion, 
                 PROVEEDOR_NIT = :proveedorNit, 
                 TIPO_ARTICULO_ID = (SELECT TIPO_ARTICULO_ID FROM TALLER.TIPO_ARTICULO WHERE UPPER(NOMBRE) = UPPER(:tipoArticulo))
               WHERE ARTICULOID = :articuloId";

                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    AgregarParams(cmd, articulo);
                    cmd.Parameters.Add(new OracleParameter("articuloId", articulo.ArticuloId));

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
