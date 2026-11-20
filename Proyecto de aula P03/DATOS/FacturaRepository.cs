
using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DATOS
{
    public class FacturaRepository : ManejadorConexion, IRepository<Factura>
    {
        public FacturaRepository()
        {

        }

        public int ObtenerSiguienteFacturaId()
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_FACTURA.SIGUIENTE_ID", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_id", OracleDbType.Int32).Direction = ParameterDirection.Output;
                conexion.Open(); comando.ExecuteNonQuery();
                return Convert.ToInt32(comando.Parameters["p_id"].Value.ToString());
            }
        }
        public void Guardar(Factura factura)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_FACTURA.GUARDAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                AgregarParamsFactura(comando, factura);
                OracleParameter parametroId = comando.Parameters.Add("p_id", OracleDbType.Int32);
                parametroId.Direction = ParameterDirection.Output;
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); factura.FacturaID = Convert.ToInt32(parametroId.Value.ToString()); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }

        public void GuardarCompleto(Factura factura)
        {
            if (factura == null) throw new ArgumentNullException("factura");
            if (factura.Detalles == null || factura.Detalles.Count == 0) throw new ArgumentException("El documento debe tener detalles.");
            if (string.Equals(factura.EstadoPago, "Anulada", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Los documentos nuevos no pueden estar anulados.");
            if (factura.AgendaID.HasValue && (factura.TipoDocumento != "VENTA" || factura.Vehiculo == null || factura.Cliente == null))
                throw new ArgumentException("Una cita solo se puede asociar a una venta con cliente y vehículo.");
            factura.CalcularTotales();
            using (OracleConnection con = ObtenerConexion())
            {
                con.Open();
                using (OracleTransaction tx = con.BeginTransaction())
                {
                    try
                    {
                        if (factura.AgendaID.HasValue)
                        {
                            using (OracleCommand cita = new OracleCommand("TALLER.PKG_FACTURA.VALIDAR_CITA", con))
                            {
                                cita.CommandType = CommandType.StoredProcedure;
                                cita.Transaction = tx;
                                cita.BindByName = true;
                                cita.Parameters.Add("p_agenda_id", OracleDbType.Int32).Value = factura.AgendaID.Value;
                                cita.Parameters.Add("p_placa", OracleDbType.Char, 7).Value = factura.Vehiculo.Placa;
                                cita.Parameters.Add("p_cliente", OracleDbType.Char, 10).Value = factura.Cliente.Cedula;
                                cita.ExecuteNonQuery();
                            }
                        }
                        using (OracleCommand cmd = new OracleCommand("TALLER.PKG_FACTURA.GUARDAR", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Transaction = tx;
                            AgregarParamsFactura(cmd, factura);
                            OracleParameter parametroFacturaId = cmd.Parameters.Add("p_id", OracleDbType.Int32);
                            parametroFacturaId.Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            factura.FacturaID = Convert.ToInt32(parametroFacturaId.Value.ToString());
                        }

                        bool venta = string.Equals(factura.TipoDocumento, "VENTA", StringComparison.OrdinalIgnoreCase);
                        foreach (DetalleFactura detalle in factura.Detalles.OrderBy(d => d.Articulo.ArticuloId))
                        {
                            detalle.FacturaID = factura.FacturaID;
                            using (OracleCommand articulo = new OracleCommand("TALLER.PKG_ARTICULO.OBTENER_TIPO", con))
                            {
                                articulo.CommandType = CommandType.StoredProcedure;
                                articulo.Transaction = tx;
                                articulo.BindByName = true;
                                articulo.Parameters.Add("p_id", OracleDbType.Int32).Value = detalle.Articulo.ArticuloId;
                                articulo.Parameters.Add("p_tipo", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                                articulo.ExecuteNonQuery();
                                string tipo = Convert.ToString(articulo.Parameters["p_tipo"].Value);
                                if (!string.Equals(tipo, detalle.Articulo is Repuesto ? "Repuesto" : "Servicio", StringComparison.OrdinalIgnoreCase))
                                    throw new InvalidOperationException("El tipo del artículo cambió. Recarga el documento antes de guardar.");
                            }
                            if (venta && detalle.Articulo is Repuesto)
                            {
                                using (OracleCommand stock = new OracleCommand("TALLER.PKG_ARTICULO.DESCONTAR_STOCK", con))
                                {
                                    stock.CommandType = CommandType.StoredProcedure;
                                    stock.Transaction = tx;
                                    stock.Parameters.Add("p_id", OracleDbType.Int32).Value = detalle.Articulo.ArticuloId;
                                    stock.Parameters.Add("p_cantidad", OracleDbType.Int32).Value = detalle.Cantidad;
                                    stock.ExecuteNonQuery();
                                }
                            }

                            using (OracleCommand cmd = new OracleCommand("TALLER.PKG_DETALLE_FACTURA.GUARDAR", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Transaction = tx;
                                AgregarParamsDetalleFactura(cmd, detalle);
                                OracleParameter parametroDetalleId = cmd.Parameters.Add("p_id", OracleDbType.Int32);
                                parametroDetalleId.Direction = ParameterDirection.Output;
                                cmd.ExecuteNonQuery();
                                detalle.DetalleFacturaID = Convert.ToInt32(parametroDetalleId.Value.ToString());
                            }
                        }

                        if (factura.Vehiculo != null)
                        {
                            using (OracleCommand cmd = new OracleCommand("TALLER.PKG_DETALLE_VEHICULO.GUARDAR", con))
                            {
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Transaction = tx;
                                OracleParameter parametroId = cmd.Parameters.Add("p_id", OracleDbType.Int32);
                                parametroId.Direction = System.Data.ParameterDirection.InputOutput;
                                parametroId.Value = 0;
                                AgregarParamsDetalleVehiculo(cmd, new DetalleVehiculo
                                {
                                    FacturaID = factura.FacturaID,
                                    Vehiculo = factura.Vehiculo
                                });
                                cmd.ExecuteNonQuery();
                            }
                        }
                        if (factura.AgendaID.HasValue)
                        {
                            using (OracleCommand completarCita = new OracleCommand("TALLER.PKG_FACTURA.COMPLETAR_CITA", con))
                            {
                                completarCita.CommandType = CommandType.StoredProcedure;
                                completarCita.Transaction = tx;
                                completarCita.Parameters.Add("p_agenda_id", OracleDbType.Int32).Value = factura.AgendaID.Value;
                                completarCita.ExecuteNonQuery();
                            }
                        }
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public decimal ObtenerTotalVentas()
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_FACTURA.TOTAL_VENTAS", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_total", OracleDbType.Decimal).Direction = ParameterDirection.Output;
                conexion.Open(); comando.ExecuteNonQuery();
                return Convert.ToDecimal(comando.Parameters["p_total"].Value.ToString());
            }
        }

        public void ConvertirCotizacionEnVenta(int facturaId)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_FACTURA.CONVERTIR_COTIZACION", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_id", OracleDbType.Int32).Value = facturaId;
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }

        public void AnularDocumento(int facturaId)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_FACTURA.ANULAR_DOCUMENTO", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_id", OracleDbType.Int32).Value = facturaId;
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }

        private void AgregarParamsFactura(OracleCommand cmd, Factura factura)
        {
            cmd.BindByName = true;
            cmd.Parameters.Add("p_fecha", OracleDbType.Date).Value = factura.Fecha;
            cmd.Parameters.Add("p_descuento", OracleDbType.Decimal).Value = factura.Descuento;
            cmd.Parameters.Add("p_subtotal", OracleDbType.Decimal).Value = factura.SubTotal;
            cmd.Parameters.Add("p_iva", OracleDbType.Decimal).Value = factura.Iva;
            cmd.Parameters.Add("p_total", OracleDbType.Decimal).Value = factura.Total;
            cmd.Parameters.Add("p_observaciones", OracleDbType.Varchar2).Value = (object)factura.Observaciones ?? DBNull.Value;
            cmd.Parameters.Add("p_estado_pago", OracleDbType.Varchar2).Value = factura.EstadoPago;
            cmd.Parameters.Add("p_cliente", OracleDbType.Char, 10).Value = factura.Cliente != null ? (object)factura.Cliente.Cedula : DBNull.Value;
            cmd.Parameters.Add("p_empleado", OracleDbType.Char, 10).Value = factura.Empleado != null ? (object)factura.Empleado.Cedula : DBNull.Value;
            cmd.Parameters.Add("p_tipo_documento", OracleDbType.Varchar2).Value = string.IsNullOrEmpty(factura.TipoDocumento) ? "VENTA" : factura.TipoDocumento;
            cmd.Parameters.Add("p_domicilio", OracleDbType.Char, 1).Value = factura.Domicilio ? "S" : "N";
            cmd.Parameters.Add("p_direccion_domicilio", OracleDbType.Varchar2).Value = factura.Domicilio ? (object)factura.DireccionDomicilio : DBNull.Value;
            cmd.Parameters.Add("p_agenda_id", OracleDbType.Int32).Value = (object)factura.AgendaID ?? DBNull.Value;
        }

        private static void AgregarParamsDetalleFactura(OracleCommand cmd, DetalleFactura detalle)
        {
            cmd.BindByName = true;
            cmd.Parameters.Add("p_cantidad", OracleDbType.Int32).Value = detalle.Cantidad;
            cmd.Parameters.Add("p_valor_unitario", OracleDbType.Decimal).Value = detalle.ValorUnitario;
            cmd.Parameters.Add("p_valor_iva", OracleDbType.Decimal).Value = detalle.ValorIVA;
            cmd.Parameters.Add("p_factura_id", OracleDbType.Int32).Value = detalle.FacturaID;
            cmd.Parameters.Add("p_articulo_id", OracleDbType.Int32).Value = detalle.Articulo.ArticuloId;
            cmd.Parameters.Add("p_porcentaje_iva", OracleDbType.Decimal).Value = detalle.PorcentajeIVA;
            cmd.Parameters.Add("p_descripcion", OracleDbType.Varchar2).Value = (object)detalle.Descripcion ?? DBNull.Value;
            cmd.Parameters.Add("p_subtotal", OracleDbType.Decimal).Value = detalle.Subtotal;
            cmd.Parameters.Add("p_total", OracleDbType.Decimal).Value = detalle.Total;
            cmd.Parameters.Add("p_descuento", OracleDbType.Decimal).Value = detalle.Descuento;
        }

        public List<Factura> Consultar()
        {
            List<Factura> facturas = new List<Factura>();
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_FACTURA.CONSULTAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                conexion.Open();
                using (OracleDataReader reader = comando.ExecuteReader())
                    while (reader.Read()) facturas.Add(Mapear(reader));
            }
            return facturas;
        }

        private Factura Mapear(OracleDataReader reader)
        {
            Factura factura = new Factura
            {
                FacturaID = Convert.ToInt32(reader["FACTURAID"]),
                Fecha = reader["FECHA"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FECHA"]),
                Descuento = Convert.ToDecimal(reader["DESCUENTO"]),
                SubTotal = Convert.ToDecimal(reader["SUBTOTAL"]),
                Iva = Convert.ToDecimal(reader["IVA"]),
                Total = reader["TOTAL"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["TOTAL"]),
                Observaciones = reader["OBSERVACIONES"] != DBNull.Value ? reader["OBSERVACIONES"].ToString() : string.Empty,
                EstadoPago = reader["ESTADOPAGO"].ToString(),
                TipoDocumento = reader["TIPO_DOCUMENTO"].ToString(),
                Domicilio = reader["DOMICILIO"].ToString().Trim() == "S",
                DireccionDomicilio = reader["DIRECCION_DOMICILIO"] != DBNull.Value ? reader["DIRECCION_DOMICILIO"].ToString() : null
            };

            if (reader["CLIENTE_CEDULA"] != DBNull.Value)
            {
                factura.Cliente = new Cliente
                {
                    Cedula = reader["CLIENTE_CEDULA"].ToString().Trim(),
                    PrimerNombre = ValorTexto(reader, "CLIENTE_PRIMER_NOMBRE"), SegundoNombre = ValorTexto(reader, "CLIENTE_SEGUNDO_NOMBRE"),
                    PrimerApellido = ValorTexto(reader, "CLIENTE_PRIMER_APELLIDO"), SegundoApellido = ValorTexto(reader, "CLIENTE_SEGUNDO_APELLIDO")
                };
            }

            if (reader["EMPLEADO_CEDULA"] != DBNull.Value)
            {
                factura.Empleado = new Empleado
                {
                    Cedula = reader["EMPLEADO_CEDULA"].ToString().Trim(),
                    PrimerNombre = ValorTexto(reader, "EMPLEADO_PRIMER_NOMBRE"), SegundoNombre = ValorTexto(reader, "EMPLEADO_SEGUNDO_NOMBRE"),
                    PrimerApellido = ValorTexto(reader, "EMPLEADO_PRIMER_APELLIDO"), SegundoApellido = ValorTexto(reader, "EMPLEADO_SEGUNDO_APELLIDO")
                };
            }

            if (reader["VEHICULO_PLACA"] != DBNull.Value)
                factura.Vehiculo = new Vehiculo
                {
                    Placa = reader["VEHICULO_PLACA"].ToString().Trim(),
                    Modelo = reader["VEHICULO_MODELO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["VEHICULO_MODELO"]),
                    Marca = ValorTexto(reader, "VEHICULO_MARCA"), TipoVehiculo = ValorTexto(reader, "VEHICULO_TIPO"),
                    Color = ValorTexto(reader, "VEHICULO_COLOR"), Linea = ValorTexto(reader, "VEHICULO_LINEA")
                };

            factura.AgendaID = reader["AGENDA_AGENDAID"] != DBNull.Value
                ? (int?)Convert.ToInt32(reader["AGENDA_AGENDAID"])
                : null;

            return factura;
        }

        private static string ValorTexto(OracleDataReader reader, string campo)
        {
            return reader[campo] == DBNull.Value ? null : reader[campo].ToString();
        }

        public Factura BuscarId(int facturaId)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_FACTURA.BUSCAR_ID", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_id", OracleDbType.Int32).Value = facturaId;
                comando.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                conexion.Open();
                using (OracleDataReader reader = comando.ExecuteReader()) return reader.Read() ? Mapear(reader) : null;
            }
        }

        public void Eliminar(int facturaId)
        {
            // Compatibilidad con consumidores anteriores: cancelar siempre conserva el documento.
            AnularDocumento(facturaId);
        }

        public void Modificar(Factura factura)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_FACTURA.MODIFICAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                AgregarParamsFactura(comando, factura);
                comando.Parameters.Add("p_id", OracleDbType.Int32).Value = factura.FacturaID;
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }

        public void GuardarDetalleVehiculo(DetalleVehiculo detalle)
        {
            if (detalle == null || detalle.Vehiculo == null)
            {
                throw new ArgumentException("EL VEHICULO DEL DETALLE NO PUEDE SER NULO");
            }

            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("TALLER.PKG_DETALLE_VEHICULO.GUARDAR", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter parametroId = cmd.Parameters.Add("p_id", OracleDbType.Int32);
                parametroId.Direction = System.Data.ParameterDirection.InputOutput;
                parametroId.Value = detalle.ID;
                AgregarParamsDetalleVehiculo(cmd, detalle);
                con.Open();
                using (OracleTransaction tx = con.BeginTransaction())
                {
                    cmd.Transaction = tx;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        detalle.ID = Convert.ToInt32(parametroId.Value.ToString());
                        tx.Commit();
                    }
                    catch { tx.Rollback(); throw; }
                }
            }
        }

        private static void AgregarParamsDetalleVehiculo(OracleCommand cmd, DetalleVehiculo detalle)
        {
            cmd.BindByName = true;
            cmd.Parameters.Add("p_factura_id", OracleDbType.Int32).Value = detalle.FacturaID;
            cmd.Parameters.Add("p_placa", OracleDbType.Char, 7).Value = detalle.Vehiculo.Placa;
        }

        public void ModificarDetalleVehiculo(DetalleVehiculo detalle)
        {
            if (detalle == null || detalle.Vehiculo == null)
                throw new ArgumentException("El detalle debe tener un vehículo asociado.");

            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("TALLER.PKG_DETALLE_VEHICULO.MODIFICAR", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = detalle.ID;
                AgregarParamsDetalleVehiculo(cmd, detalle);
                con.Open();
                using (OracleTransaction tx = con.BeginTransaction())
                {
                    cmd.Transaction = tx;
                    try { cmd.ExecuteNonQuery(); tx.Commit(); }
                    catch { tx.Rollback(); throw; }
                }
            }
        }

        public DetalleVehiculo BuscarDetalleVehiculo(int id)
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("TALLER.PKG_DETALLE_VEHICULO.BUSCAR_ID", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = id;
                cmd.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = System.Data.ParameterDirection.Output;
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                    return reader.Read() ? MapearDetalleVehiculo(reader) : null;
            }
        }

        public void EliminarDetalleVehiculo(int id)
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("TALLER.PKG_DETALLE_VEHICULO.ELIMINAR", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = id;
                con.Open();
                using (OracleTransaction tx = con.BeginTransaction())
                {
                    cmd.Transaction = tx;
                    try { cmd.ExecuteNonQuery(); tx.Commit(); }
                    catch { tx.Rollback(); throw; }
                }
            }
        }

        public List<DetalleVehiculo> ConsultarDetallesVehiculo(int facturaId)
        {
            List<DetalleVehiculo> detalles = new List<DetalleVehiculo>();

            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("TALLER.PKG_DETALLE_VEHICULO.CONSULTAR_POR_FACTURA", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("p_factura_id", OracleDbType.Int32).Value = facturaId;
                cmd.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = System.Data.ParameterDirection.Output;
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        detalles.Add(MapearDetalleVehiculo(reader));
                    }
            }
            return detalles;
        }

        private static DetalleVehiculo MapearDetalleVehiculo(OracleDataReader reader)
        {
            return new DetalleVehiculo
            {
                ID = Convert.ToInt32(reader["DETALLEVEHICULOID"]),
                FacturaID = Convert.ToInt32(reader["FACTURA_FACTURAID"]),
                Vehiculo = new Vehiculo
                {
                    Placa = reader["PLACA"].ToString().Trim(),
                    Modelo = reader["MODELO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MODELO"]),
                    Marca = reader["MARCA"] == DBNull.Value ? null : reader["MARCA"].ToString(),
                    TipoVehiculo = reader["TIPO_VEHICULO"].ToString(),
                    Color = reader["COLOR"] == DBNull.Value ? null : reader["COLOR"].ToString(),
                    Linea = reader["LINEA"] == DBNull.Value ? null : reader["LINEA"].ToString(),
                    Cliente = new Cliente { Cedula = reader["CLIENTE_CEDULA"].ToString().Trim() }
                }
            };
        }
    }
}
