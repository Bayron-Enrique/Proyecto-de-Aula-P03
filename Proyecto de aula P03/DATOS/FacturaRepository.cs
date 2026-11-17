
using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace DATOS
{
    public class FacturaRepository : ManejadorConexion, IRepository<Factura>
    {
        public FacturaRepository()
        {

        }

        public int ObtenerSiguienteFacturaId()
        {
            int siguienteID = 0;
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = "SELECT TALLER.SEQ_FACTURAID.NEXTVAL FROM DUAL";
                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    con.Open();
                    siguienteID = Convert.ToInt32(cmd.ExecuteScalar());
                    return siguienteID;
                }
            }
        }
        public void Guardar(Factura factura)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = @"INSERT INTO TALLER.FACTURA
                         (FACTURAID, FECHA, DESCUENTO, SUBTOTAL, IVA, TOTAL, OBSERVACIONES, ESTADOPAGO, CLIENTE_CEDULA, EMPLEADO_CEDULA, TIPO_DOCUMENTO, DOMICILIO, DIRECCION_DOMICILIO, AGENDA_AGENDAID)
                         VALUES (:facturaId, :fecha, :descuento, :subtotal, :iva, :total, :observaciones, :estadoPago, :clienteCedula, :empleadoCedula, :tipoDocumento, :domicilio, :direccionDomicilio, :agendaId)";
                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    AgregarPararms(cmd, factura);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void GuardarCompleto(Factura factura)
        {
            if (factura == null) throw new ArgumentNullException("factura");
            factura.CalcularTotales();
            using (OracleConnection con = ObtenerConexion())
            {
                con.Open();
                using (OracleTransaction tx = con.BeginTransaction())
                {
                    try
                    {
                        using (OracleCommand secuencia = new OracleCommand("SELECT TALLER.SEQ_FACTURAID.NEXTVAL FROM DUAL", con))
                        {
                            secuencia.Transaction = tx;
                            factura.FacturaID = Convert.ToInt32(secuencia.ExecuteScalar());
                        }

                        const string sqlCabecera = @"INSERT INTO TALLER.FACTURA
                            (FACTURAID, FECHA, DESCUENTO, SUBTOTAL, IVA, TOTAL, OBSERVACIONES, ESTADOPAGO,
                             CLIENTE_CEDULA, EMPLEADO_CEDULA, TIPO_DOCUMENTO, DOMICILIO, DIRECCION_DOMICILIO, AGENDA_AGENDAID)
                            VALUES (:facturaId, :fecha, :descuento, :subtotal, :iva, :total, :observaciones, :estadoPago,
                                    :clienteCedula, :empleadoCedula, :tipoDocumento, :domicilio, :direccionDomicilio, :agendaId)";
                        using (OracleCommand cmd = new OracleCommand(sqlCabecera, con))
                        {
                            cmd.Transaction = tx;
                            AgregarPararms(cmd, factura);
                            cmd.ExecuteNonQuery();
                        }

                        bool venta = string.Equals(factura.TipoDocumento, "VENTA", StringComparison.OrdinalIgnoreCase);
                        foreach (DetalleFactura detalle in factura.Detalles)
                        {
                            detalle.FacturaID = factura.FacturaID;
                            if (venta && detalle.Articulo is Repuesto)
                            {
                                const string sqlStock = @"UPDATE TALLER.ARTICULO SET CANTIDAD = CANTIDAD - :cantidad
                                                          WHERE ARTICULOID = :articuloId AND CANTIDAD >= :cantidad";
                                using (OracleCommand stock = new OracleCommand(sqlStock, con))
                                {
                                    stock.Transaction = tx;
                                    stock.BindByName = true;
                                    stock.Parameters.Add("cantidad", detalle.Cantidad);
                                    stock.Parameters.Add("articuloId", detalle.Articulo.ArticuloId);
                                    if (stock.ExecuteNonQuery() != 1)
                                        throw new InvalidOperationException("El stock cambió o no alcanza para el artículo " + detalle.Articulo.Nombre + ".");
                                }
                            }

                            const string sqlDetalle = @"INSERT INTO TALLER.DETALLE_FACTURA
                                (DETALLEFACTURAID, CANTIDAD, VALORUNITARIO, VALORIVA, FACTURA_FACTURAID,
                                 ARTICULO_ARTICULOID, PORCENTAJEIVA, DESCRIPCION, SUBTOTAL, TOTAL, DESCUENTO)
                                VALUES (TALLER.SEQ_DETALLEFACTURAID.NEXTVAL, :cantidad, :valorUnitario, :valorIva,
                                        :facturaId, :articuloId, :porcentajeIva, :descripcion, :subtotal, :total, :descuento)";
                            using (OracleCommand cmd = new OracleCommand(sqlDetalle, con))
                            {
                                cmd.Transaction = tx;
                                cmd.BindByName = true;
                                cmd.Parameters.Add("cantidad", detalle.Cantidad);
                                cmd.Parameters.Add("valorUnitario", detalle.ValorUnitario);
                                cmd.Parameters.Add("valorIva", detalle.ValorIVA);
                                cmd.Parameters.Add("facturaId", factura.FacturaID);
                                cmd.Parameters.Add("articuloId", detalle.Articulo.ArticuloId);
                                cmd.Parameters.Add("porcentajeIva", detalle.PorcentajeIVA);
                                cmd.Parameters.Add("descripcion", (object)detalle.Descripcion ?? DBNull.Value);
                                cmd.Parameters.Add("subtotal", detalle.Subtotal);
                                cmd.Parameters.Add("total", detalle.Total);
                                cmd.Parameters.Add("descuento", detalle.Descuento);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        if (factura.Vehiculo != null)
                        {
                            using (OracleCommand bloquear = new OracleCommand("LOCK TABLE TALLER.DETALLE_VEHICULO IN EXCLUSIVE MODE", con))
                            {
                                bloquear.Transaction = tx;
                                bloquear.ExecuteNonQuery();
                            }
                            int detalleVehiculoId;
                            using (OracleCommand siguiente = new OracleCommand("SELECT NVL(MAX(DETALLEVEHICULOID), 0) + 1 FROM TALLER.DETALLE_VEHICULO", con))
                            {
                                siguiente.Transaction = tx;
                                detalleVehiculoId = Convert.ToInt32(siguiente.ExecuteScalar());
                            }
                            const string sqlVehiculo = @"INSERT INTO TALLER.DETALLE_VEHICULO
                                (DETALLEVEHICULOID, FACTURA_FACTURAID, VEHICULO_PLACA) VALUES (:id, :facturaId, :placa)";
                            using (OracleCommand cmd = new OracleCommand(sqlVehiculo, con))
                            {
                                cmd.Transaction = tx;
                                cmd.BindByName = true;
                                cmd.Parameters.Add("id", detalleVehiculoId);
                                cmd.Parameters.Add("facturaId", factura.FacturaID);
                                cmd.Parameters.Add("placa", factura.Vehiculo.Placa);
                                cmd.ExecuteNonQuery();
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
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(@"SELECT NVL(SUM(TOTAL), 0) FROM TALLER.FACTURA
                WHERE TIPO_DOCUMENTO = 'VENTA' AND UPPER(ESTADOPAGO) <> 'ANULADA'", con))
            {
                con.Open();
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }

        public void ConvertirCotizacionEnVenta(int facturaId)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                con.Open();
                using (OracleTransaction tx = con.BeginTransaction())
                {
                    try
                    {
                        string tipo;
                        string estado;
                        using (OracleCommand cabecera = new OracleCommand("SELECT TIPO_DOCUMENTO, ESTADOPAGO FROM TALLER.FACTURA WHERE FACTURAID = :id FOR UPDATE", con))
                        {
                            cabecera.Transaction = tx;
                            cabecera.Parameters.Add("id", facturaId);
                            using (OracleDataReader reader = cabecera.ExecuteReader())
                            {
                                if (!reader.Read()) throw new InvalidOperationException("No se encontró el documento.");
                                tipo = reader["TIPO_DOCUMENTO"].ToString();
                                estado = reader["ESTADOPAGO"].ToString();
                            }
                        }
                        if (!string.Equals(tipo, "COTIZACION", StringComparison.OrdinalIgnoreCase))
                            throw new InvalidOperationException("Solo se pueden convertir cotizaciones pendientes.");
                        if (string.Equals(estado, "Anulada", StringComparison.OrdinalIgnoreCase))
                            throw new InvalidOperationException("No se puede convertir una cotización anulada.");

                        const string sqlDetalles = @"SELECT D.CANTIDAD, D.ARTICULO_ARTICULOID, T.NOMBRE AS TIPO
                            FROM TALLER.DETALLE_FACTURA D JOIN TALLER.ARTICULO A ON A.ARTICULOID = D.ARTICULO_ARTICULOID
                            JOIN TALLER.TIPO_ARTICULO T ON T.TIPO_ARTICULO_ID = A.TIPO_ARTICULO_ID
                            WHERE D.FACTURA_FACTURAID = :id";
                        var repuestos = new List<KeyValuePair<int, int>>();
                        using (OracleCommand detalles = new OracleCommand(sqlDetalles, con))
                        {
                            detalles.Transaction = tx;
                            detalles.Parameters.Add("id", facturaId);
                            using (OracleDataReader reader = detalles.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (!string.Equals(reader["TIPO"].ToString(), "Repuesto", StringComparison.OrdinalIgnoreCase)) continue;
                                    repuestos.Add(new KeyValuePair<int, int>(Convert.ToInt32(reader["ARTICULO_ARTICULOID"]), Convert.ToInt32(reader["CANTIDAD"])));
                                }
                            }
                        }
                        foreach (KeyValuePair<int, int> item in repuestos)
                        {
                            using (OracleCommand stock = new OracleCommand(@"UPDATE TALLER.ARTICULO SET CANTIDAD = CANTIDAD - :cantidad
                                WHERE ARTICULOID = :articuloId AND CANTIDAD >= :cantidad", con))
                            {
                                stock.Transaction = tx;
                                stock.BindByName = true;
                                stock.Parameters.Add("cantidad", item.Value);
                                stock.Parameters.Add("articuloId", item.Key);
                                if (stock.ExecuteNonQuery() != 1)
                                    throw new InvalidOperationException("No hay stock suficiente para convertir esta cotización.");
                            }
                        }
                        using (OracleCommand actualizar = new OracleCommand("UPDATE TALLER.FACTURA SET TIPO_DOCUMENTO = 'VENTA', ESTADOPAGO = 'Pendiente' WHERE FACTURAID = :id", con))
                        {
                            actualizar.Transaction = tx;
                            actualizar.Parameters.Add("id", facturaId);
                            actualizar.ExecuteNonQuery();
                        }
                        tx.Commit();
                    }
                    catch { tx.Rollback(); throw; }
                }
            }
        }

        private void AgregarPararms(OracleCommand cmd, Factura factura)
        {
            // Enlazar por nombre: en Modificar el orden del SQL no coincide con el orden de los parametros
            cmd.BindByName = true;

            cmd.Parameters.Add(new OracleParameter("facturaId", factura.FacturaID));
            cmd.Parameters.Add(new OracleParameter("fecha", factura.Fecha));
            cmd.Parameters.Add(new OracleParameter("descuento", factura.Descuento));
            cmd.Parameters.Add(new OracleParameter("subtotal", factura.SubTotal));
            cmd.Parameters.Add(new OracleParameter("iva", factura.Iva));
            cmd.Parameters.Add(new OracleParameter("total", factura.Total));
            cmd.Parameters.Add(new OracleParameter("observaciones", string.IsNullOrEmpty(factura.Observaciones) ? (object)DBNull.Value : factura.Observaciones));
            cmd.Parameters.Add(new OracleParameter("estadoPago", factura.EstadoPago));
            cmd.Parameters.Add(new OracleParameter("clienteCedula", factura.Cliente != null ? (object)factura.Cliente.Cedula : DBNull.Value));
            cmd.Parameters.Add(new OracleParameter("empleadoCedula", factura.Empleado != null ? (object)factura.Empleado.Cedula : DBNull.Value));
            cmd.Parameters.Add(new OracleParameter("tipoDocumento", string.IsNullOrEmpty(factura.TipoDocumento) ? "VENTA" : factura.TipoDocumento));

            // En la BD DOMICILIO es CHAR(1): 'S' o 'N'
            cmd.Parameters.Add(new OracleParameter("domicilio", factura.Domicilio ? "S" : "N"));
            cmd.Parameters.Add(new OracleParameter("direccionDomicilio", factura.Domicilio && !string.IsNullOrEmpty(factura.DireccionDomicilio) ? (object)factura.DireccionDomicilio : DBNull.Value));
            cmd.Parameters.Add(new OracleParameter("agendaId", (object)factura.AgendaID ?? DBNull.Value));
        }

        public List<Factura> Consultar()
        {
            List<Factura> facturas = new List<Factura>();

            using (OracleConnection con = ObtenerConexion())
            {
                string sql = "SELECT FACTURAID, FECHA," +
                            " DESCUENTO, SUBTOTAL, IVA, TOTAL," +
                            " OBSERVACIONES, ESTADOPAGO, CLIENTE_CEDULA," +
                            " EMPLEADO_CEDULA, TIPO_DOCUMENTO," +
                            " DOMICILIO, DIRECCION_DOMICILIO, AGENDA_AGENDAID " +
                            "FROM TALLER.FACTURA";
                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    con.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            facturas.Add(Mapear(reader));
                        }
                    }
                }

            }
            return facturas;
        }

        private Factura Mapear(OracleDataReader reader)
        {
            Factura factura = new Factura
            {
                FacturaID = Convert.ToInt32(reader["FACTURAID"]),
                Fecha = Convert.ToDateTime(reader["FECHA"]),
                Descuento = Convert.ToDecimal(reader["DESCUENTO"]),
                SubTotal = Convert.ToDecimal(reader["SUBTOTAL"]),
                Iva = Convert.ToDecimal(reader["IVA"]),
                Total = Convert.ToDecimal(reader["TOTAL"]),
                Observaciones = reader["OBSERVACIONES"] != DBNull.Value ? reader["OBSERVACIONES"].ToString() : string.Empty,
                EstadoPago = reader["ESTADOPAGO"].ToString(),
                TipoDocumento = reader["TIPO_DOCUMENTO"].ToString(),
                Domicilio = reader["DOMICILIO"].ToString().Trim() == "S",
                DireccionDomicilio = reader["DIRECCION_DOMICILIO"] != DBNull.Value ? reader["DIRECCION_DOMICILIO"].ToString() : null
            };

            if (reader["CLIENTE_CEDULA"] != DBNull.Value)
            {
                factura.Cliente = new Cliente { Cedula = reader["CLIENTE_CEDULA"].ToString().Trim() };
            }

            if (reader["EMPLEADO_CEDULA"] != DBNull.Value)
            {
                factura.Empleado = new Empleado { Cedula = reader["EMPLEADO_CEDULA"].ToString().Trim() };
            }

            factura.AgendaID = reader["AGENDA_AGENDAID"] != DBNull.Value
                ? (int?)Convert.ToInt32(reader["AGENDA_AGENDAID"])
                : null;

            return factura;
        }

        public Factura BuscarId(int facturaId)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = "SELECT FACTURAID, FECHA, DESCUENTO, SUBTOTAL," +
                             " IVA, TOTAL, OBSERVACIONES, ESTADOPAGO, CLIENTE_CEDULA," +
                             " EMPLEADO_CEDULA, TIPO_DOCUMENTO," +
                             " DOMICILIO, DIRECCION_DOMICILIO, AGENDA_AGENDAID" +
                             " FROM TALLER.FACTURA" +
                             " WHERE FACTURAID = :facturaId";

                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    cmd.Parameters.Add(new OracleParameter("facturaId", facturaId));
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

        public void Eliminar(int facturaId)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                // DETALLE_FACTURA y DETALLE_VEHICULO se borran solos (ON DELETE CASCADE)
                string sql = "DELETE FROM TALLER.FACTURA WHERE FACTURAID = :facturaId";
                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    cmd.Parameters.Add(new OracleParameter("facturaId", facturaId));
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Modificar(Factura factura)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = @"UPDATE TALLER.FACTURA SET 
                                 FECHA = :fecha, 
                                 DESCUENTO = :descuento, 
                                 SUBTOTAL = :subtotal, 
                                 IVA = :iva, 
                                 TOTAL = :total, 
                                 OBSERVACIONES = :observaciones, 
                                 ESTADOPAGO = :estadoPago, 
                                 CLIENTE_CEDULA = :clienteCedula, 
                                 EMPLEADO_CEDULA = :empleadoCedula, 
                                 TIPO_DOCUMENTO = :tipoDocumento,
                                 DOMICILIO = :domicilio,
                                 DIRECCION_DOMICILIO = :direccionDomicilio,
                                 AGENDA_AGENDAID = :agendaId
                               WHERE FACTURAID = :facturaId";

                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    AgregarPararms(cmd, factura);
                    con.Open();
                    cmd.ExecuteNonQuery();
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
            {
                con.Open();
                using (OracleCommand bloquear = new OracleCommand("LOCK TABLE TALLER.DETALLE_VEHICULO IN EXCLUSIVE MODE", con)) bloquear.ExecuteNonQuery();
                int detalleVehiculoId;
                using (OracleCommand siguiente = new OracleCommand("SELECT NVL(MAX(DETALLEVEHICULOID), 0) + 1 FROM TALLER.DETALLE_VEHICULO", con))
                    detalleVehiculoId = Convert.ToInt32(siguiente.ExecuteScalar());
                string sql = @"INSERT INTO TALLER.DETALLE_VEHICULO (DETALLEVEHICULOID, FACTURA_FACTURAID, VEHICULO_PLACA)
                               VALUES (:id, :facturaId, :placa)";

                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    cmd.BindByName = true;
                    cmd.Parameters.Add(new OracleParameter("id", detalleVehiculoId));
                    cmd.Parameters.Add(new OracleParameter("facturaId", detalle.FacturaID));
                    cmd.Parameters.Add(new OracleParameter("placa", detalle.Vehiculo.Placa));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<DetalleVehiculo> ConsultarDetallesVehiculo(int facturaId)
        {
            List<DetalleVehiculo> detalles = new List<DetalleVehiculo>();

            using (OracleConnection con = ObtenerConexion())
            {
                string sql = @"SELECT DV.DETALLEVEHICULOID, DV.FACTURA_FACTURAID,
                               V.PLACA, V.MODELO, V.MARCA, V.TIPO_VEHICULO, V.COLOR, V.LINEA, V.CLIENTE_CEDULA
                               FROM TALLER.DETALLE_VEHICULO DV
                               JOIN TALLER.VEHICULO V ON DV.VEHICULO_PLACA = V.PLACA
                               WHERE DV.FACTURA_FACTURAID = :facturaId";

                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    cmd.Parameters.Add(new OracleParameter("facturaId", facturaId));
                    con.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Vehiculo vehiculo = new Vehiculo
                            {
                                Placa = reader["PLACA"].ToString().Trim(),
                                Modelo = reader["MODELO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MODELO"]),
                                Marca = reader["MARCA"] == DBNull.Value ? null : reader["MARCA"].ToString(),
                                TipoVehiculo = reader["TIPO_VEHICULO"].ToString(),
                                Color = reader["COLOR"] == DBNull.Value ? null : reader["COLOR"].ToString(),
                                Linea = reader["LINEA"] == DBNull.Value ? null : reader["LINEA"].ToString(),
                                Cliente = new Cliente { Cedula = reader["CLIENTE_CEDULA"].ToString().Trim() }
                            };

                            detalles.Add(new DetalleVehiculo
                            {
                                ID = Convert.ToInt32(reader["DETALLEVEHICULOID"]),
                                FacturaID = Convert.ToInt32(reader["FACTURA_FACTURAID"]),
                                Vehiculo = vehiculo
                            });
                        }
                    }
                }
            }
            return detalles;
        }
    }
}
