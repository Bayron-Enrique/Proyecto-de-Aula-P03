using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace DATOS
{
    public class DetalleFacturaRepository : ManejadorConexion, IRepository<DetalleFactura>
    {
        public List<DetalleFactura> Consultar()
        {
            List<DetalleFactura> detalles = new List<DetalleFactura>();
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_DETALLE_FACTURA.CONSULTAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                conexion.Open();
                using (OracleDataReader reader = comando.ExecuteReader())
                    while (reader.Read()) detalles.Add(Mapear(reader));
            }
            return detalles;
        }

        public void Guardar(DetalleFactura detalle)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_DETALLE_FACTURA.GUARDAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                AgregarParams(comando, detalle, true);
                OracleParameter parametroId = comando.Parameters.Add("p_id", OracleDbType.Int32);
                parametroId.Direction = ParameterDirection.Output;
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try
                    {
                        comando.ExecuteNonQuery();
                        detalle.DetalleFacturaID = Convert.ToInt32(parametroId.Value.ToString());
                        transaccion.Commit();
                    }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }

        public void Modificar(DetalleFactura detalle)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_DETALLE_FACTURA.MODIFICAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                AgregarParams(comando, detalle, false);
                comando.Parameters.Add("p_id", OracleDbType.Int32).Value = detalle.DetalleFacturaID;
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }

        public void Eliminar(int id)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_DETALLE_FACTURA.ELIMINAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_id", OracleDbType.Int32).Value = id;
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }

        public List<DetalleFactura> ConsultarPorFactura(int facturaId)
        {
            List<DetalleFactura> detalles = new List<DetalleFactura>();
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_DETALLE_FACTURA.CONSULTAR_POR_FACTURA", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_factura_id", OracleDbType.Int32).Value = facturaId;
                comando.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                conexion.Open();
                using (OracleDataReader reader = comando.ExecuteReader())
                    while (reader.Read()) detalles.Add(Mapear(reader));
            }

            return detalles;
        }

        private static void AgregarParams(OracleCommand comando, DetalleFactura detalle, bool incluirFacturaId)
        {
            comando.BindByName = true;
            if (incluirFacturaId)
                comando.Parameters.Add("p_factura_id", OracleDbType.Int32).Value = detalle.FacturaID;
            comando.Parameters.Add("p_cantidad", OracleDbType.Int32).Value = detalle.Cantidad;
            comando.Parameters.Add("p_valor_unitario", OracleDbType.Decimal).Value = detalle.ValorUnitario;
            comando.Parameters.Add("p_valor_iva", OracleDbType.Decimal).Value = detalle.ValorIVA;
            comando.Parameters.Add("p_articulo_id", OracleDbType.Int32).Value = detalle.Articulo.ArticuloId;
            comando.Parameters.Add("p_porcentaje_iva", OracleDbType.Decimal).Value = detalle.PorcentajeIVA;
            comando.Parameters.Add("p_descripcion", OracleDbType.Varchar2).Value = (object)detalle.Descripcion ?? DBNull.Value;
            comando.Parameters.Add("p_subtotal", OracleDbType.Decimal).Value = detalle.Subtotal;
            comando.Parameters.Add("p_total", OracleDbType.Decimal).Value = detalle.Total;
            comando.Parameters.Add("p_descuento", OracleDbType.Decimal).Value = detalle.Descuento;
        }

        private DetalleFactura Mapear(OracleDataReader reader)
        {
            int articuloId = Convert.ToInt32(
                reader["ARTICULO_ARTICULOID"]);

            string nombreArticulo = reader["NOMBRE_ARTICULO"].ToString();
            decimal precioVenta = Convert.ToDecimal(reader["VALORUNITARIO"]);
            decimal porcentajeIva = reader["PORCENTAJEIVA"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["PORCENTAJEIVA"]);
            string tipoArticulo = reader["TIPODEARTICULO"].ToString();

            Articulo articulo = string.Equals(tipoArticulo, "Repuesto", StringComparison.OrdinalIgnoreCase) || tipoArticulo == "1"
                ? (Articulo)new Repuesto { ArticuloId = articuloId, Nombre = nombreArticulo, PrecioVenta = precioVenta, PorcentajeIVA = porcentajeIva }
                : new Servicio { ArticuloId = articuloId, Nombre = nombreArticulo, PrecioVenta = precioVenta, PorcentajeIVA = porcentajeIva };

            DetalleFactura detalle = new DetalleFactura(Convert.ToInt32(reader["CANTIDAD"]), articulo)
            {
                DetalleFacturaID = Convert.ToInt32(reader["DETALLEFACTURAID"]),
                FacturaID = Convert.ToInt32(reader["FACTURA_FACTURAID"]),
                Descripcion = reader["DESCRIPCION"] == DBNull.Value ? string.Empty : reader["DESCRIPCION"].ToString(),
                Descuento = reader["DESCUENTO"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["DESCUENTO"])
            };
            return detalle;
        }
    }
}
