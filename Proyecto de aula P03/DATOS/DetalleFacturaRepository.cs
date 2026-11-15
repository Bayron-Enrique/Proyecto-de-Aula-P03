using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class DetalleFacturaRepository : ManejadorConexion, IRepository<DetalleFactura>
    {
        public List<DetalleFactura> Consultar()
        {
            List<DetalleFactura> lista = new List<DetalleFactura>();

            using (OracleConnection con = ObtenerConexion())
            {
                string sql = @"SELECT D.DETALLEFACTURAID, D.CANTIDAD, D.VALORUNITARIO, D.VALORIVA, 
                                      D.FACTURA_FACTURAID, D.ARTICULO_ARTICULOID, D.PORCENTAJEIVA, 
                                      D.DESCRIPCION, D.SUBTOTAL, D.TOTAL, A.TIPODEARTICULO 
                               FROM DETALLEFACTURA D
                               JOIN ARTICULO A ON D.ARTICULO_ARTICULOID = A.ARTICULOID";
                               

                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    con.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(Mapear(reader));
                        }
                    }
                }
            }
            return lista;
        }
    
        public void Guardar(DetalleFactura detalle)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = @"INSERT INTO DETALLEFACTURA 
                            (DETALLEFACTURAID, CANTIDAD, VALORUNITARIO, VALORIVA, FACTURA_FACTURAID, 
                            ARTICULO_ARTICULOID, PORCENTAJEIVA, DESCRIPCION, SUBTOTAL, TOTAL) 
                            VALUES (Seq_DetalleFacturaID.NEXTVAL, :cantidad, :valorUnitario, :valorIva, :facturaId,
                           :articuloId, :porcentajeIva, :descripcion, :subtotal, :total)";

                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    // Llamamos al método auxiliar que empaqueta todos los parámetros
                    AgregarParams(cmd, detalle);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void AgregarParams(OracleCommand cmd, DetalleFactura detalle)
        {
            cmd.Parameters.Add(new OracleParameter("cantidad", detalle.Cantidad));
            cmd.Parameters.Add(new OracleParameter("valorUnitario", detalle.ValorUnitario));
            cmd.Parameters.Add(new OracleParameter("valorIva", detalle.ValorIVA));
            cmd.Parameters.Add(new OracleParameter("facturaId", detalle.FacturaID));

            // Extraemos el ID del objeto Articulo polimórfico (sea Repuesto o Servicio)
            int articuloId = detalle.Articulo != null ? detalle.Articulo.ArticuloId : 0;
            cmd.Parameters.Add(new OracleParameter("articuloId", articuloId));

            cmd.Parameters.Add(new OracleParameter("porcentajeIva", detalle.PorcentajeIVA));
            cmd.Parameters.Add(new OracleParameter("descripcion", string.IsNullOrEmpty(detalle.Descripcion) ? (object)DBNull.Value : detalle.Descripcion));
            cmd.Parameters.Add(new OracleParameter("subtotal", detalle.Subtotal));
            cmd.Parameters.Add(new OracleParameter("total", detalle.Total));
        }

        public void Modificar(DetalleFactura entidad)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = "DELETE FROM DETALLEFACTURA WHERE DETALLEFACTURAID = :detalleId";
                using (OracleCommand cmd = new OracleCommand(sql,con))
                {
                    cmd.Parameters.Add(new OracleParameter("detalleId", id));
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<DetalleFactura> ConsultarPorFactura(int facturaId)
        {
            List<DetalleFactura> detalles = new List<DetalleFactura>();
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = @"SELECT DETALLEFACTURAID, CANTIDAD, VALORUNITARIO, VALORIVA, FACTURA_FACTURAID, 
                               ARTICULO_ARTICULOID, PORCENTAJEIVA, DESCRIPCION, SUBTOTAL, TOTAL
                               FROM DETALLEFACTURA
                               WHERE FACTURA_FACTURAID = :facturaId";
                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    cmd.Parameters.Add(new OracleParameter("facturaId", facturaId));
                    con.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            detalles.Add(Mapear(reader));
                        }
                    }

                }
            }
            return detalles;
        }

        
     
        private DetalleFactura Mapear(OracleDataReader reader)
        {
            int articuloId = Convert.ToInt32(reader["ARTICULO_ARTICULOID"]);
            string nombre = reader["DESCRIPCION"].ToString();
            decimal precioVenta = Convert.ToDecimal(reader["VALORUNITARIO"]);
            decimal porcentajeIva = Convert.ToDecimal(reader["PORCENTAJEIVA"]);
            string tipoArticulo = reader["TIPODEARTICULO"]?.ToString();

            
            Articulo articulo;
            if (tipoArticulo == "Repuesto" || tipoArticulo == "1")
            {
                articulo = new Repuesto { ArticuloId = articuloId, Nombre = nombre, PrecioVenta = precioVenta, PorcentajeIVA = porcentajeIva };
            }
            else
            {
                articulo = new Servicio { ArticuloId = articuloId, Nombre = nombre, PrecioVenta = precioVenta, PorcentajeIVA = porcentajeIva };
            }

            int cantidad = Convert.ToInt32(reader["CANTIDAD"]);

            DetalleFactura detalle = new DetalleFactura(cantidad, articulo)
            {
                DetalleFacturaID = reader["DETALLEFACTURAID"] != DBNull.Value ? Convert.ToInt32(reader["DETALLEFACTURAID"]) : 0,
                FacturaID = reader["FACTURA_FACTURAID"] != DBNull.Value ? Convert.ToInt32(reader["FACTURA_FACTURAID"]) : 0
            };

            return detalle;
        }
    }
}
