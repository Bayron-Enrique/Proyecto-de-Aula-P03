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
        List<Articulo> articulos;

        public void Guardar(Articulo articulo)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = @"INSERT INTO Articulo 
                                 (ARTICULOID, NOMBRE, PRECIO, PORCENTAJEIVA, CANTIDAD, DESCRIPCION, DURACION, PROVEEDOR_NIT, TIPO_ARTICULO_ID) 
                                 VALUES (Seq_ArticuloID.NEXTVAL, :nombre, :precioventa, :porcentajeiva, :cantidad, :descripcion, :duracion, :proveedorNit, :tipoArticuloId)";

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
            
            cmd.Parameters.Add(new OracleParameter("articuloId", articulo.ArticuloId));
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

            
            int tipoArticuloId = articulo is Repuesto ? 1 : 2;
            cmd.Parameters.Add(new OracleParameter("tipoArticuloId", tipoArticuloId));
        }

        public List<Articulo> Consultar()
        {
            List<Articulo> articulos = new List<Articulo>();
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = "SELECT ARTICULOID, NOMBRE, PRECIO, PORCENTAJEIVA, CANTIDAD, DESCRIPCION, DURACION, PROVEEDOR_NIT, TIPO_ARTICULO_ID FROM Articulo";

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
            int tipoArticuloId = Convert.ToInt32(reader["TIPO_ARTICULO_ID"]);
            int id = Convert.ToInt32(reader["ARTICULOID"]);
            string nombre = reader["NOMBRE"].ToString();
            decimal precio = Convert.ToDecimal(reader["PRECIO"]);
            decimal iva = Convert.ToDecimal(reader["PORCENTAJEIVA"]);

            // Si el tipo es 1 (Repuesto), devolvemos un Repuesto
            if (tipoArticuloId == 1)
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
            else // Si es 2, devolvemos un Servicio
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
        }

        public Articulo BuscarId(int articuloId)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = @"SELECT ARTICULOID, NOMBRE, PRECIO, PORCENTAJEIVA, 
                               CANTIDAD, DESCRIPCION, DURACION, PROVEEDOR_NIT,
                               TIPO_ARTICULO_ID 
                               FROM Articulo 
                               WHERE ARTICULOID = :articuloId"; ;

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
                string sql = @"DELETE FROM Articulo WHERE ARTICULOID = :agendaId";

                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    cmd.Parameters.Add(new OracleParameter("agendaId", articuloId));
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Modificar(Articulo articulo)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = @"UPDATE Articulo SET 
                         NOMBRE = :nombre, 
                         PRECIOVENTA = :precioVenta, 
                         PORCENTAJEIVA = :porcentajeIva, 
                         STOCK = :stock, 
                         TIPODEARTICULO = :tipoArticulo 
                       WHERE ARTICULOID = :articuloId";

                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    // Llamamos al método que carga todos los parámetros de un solo golpe
                    AgregarParams(cmd, articulo);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
