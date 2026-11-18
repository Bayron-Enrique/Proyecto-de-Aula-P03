
using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
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
                            using (var cita = new OracleCommand(@"SELECT A.VEHICULO_PLACA, V.CLIENTE_CEDULA, A.ESTADO
                                FROM TALLER.AGENDA A JOIN TALLER.VEHICULO V ON V.PLACA = A.VEHICULO_PLACA
                                WHERE A.AGENDAID = :id FOR UPDATE OF A.ESTADO, V.CLIENTE_CEDULA", con))
                            {
                                cita.Transaction = tx;
                                cita.Parameters.Add("id", factura.AgendaID.Value);
                                using (var reader = cita.ExecuteReader())
                                {
                                    if (!reader.Read() || reader["VEHICULO_PLACA"].ToString().Trim() != factura.Vehiculo.Placa.Trim() ||
                                        reader["CLIENTE_CEDULA"].ToString().Trim() != factura.Cliente.Cedula.Trim() ||
                                        string.Equals(reader["ESTADO"].ToString().Trim(), "CANCELADA", StringComparison.OrdinalIgnoreCase))
                                        throw new InvalidOperationException("La cita no corresponde al cliente/vehículo o fue cancelada.");
                                }
                            }
                        }
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
                        foreach (DetalleFactura detalle in factura.Detalles.OrderBy(d => d.Articulo.ArticuloId))
                        {
                            detalle.FacturaID = factura.FacturaID;
                            using (var articulo = new OracleCommand(@"SELECT T.NOMBRE FROM TALLER.ARTICULO A
                                JOIN TALLER.TIPO_ARTICULO T ON T.TIPO_ARTICULO_ID = A.TIPO_ARTICULO_ID
                                WHERE A.ARTICULOID = :id FOR UPDATE OF A.CANTIDAD", con))
                            {
                                articulo.Transaction = tx;
                                articulo.Parameters.Add("id", detalle.Articulo.ArticuloId);
                                string tipo = Convert.ToString(articulo.ExecuteScalar());
                                if (!string.Equals(tipo, detalle.Articulo is Repuesto ? "Repuesto" : "Servicio", StringComparison.OrdinalIgnoreCase))
                                    throw new InvalidOperationException("El tipo del artículo cambió. Recarga el documento antes de guardar.");
                            }
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
                                cmd.Parameters.Add("placa", OracleDbType.Char, 7).Value = factura.Vehiculo.Placa;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        if (factura.AgendaID.HasValue)
                        {
                            using (OracleCommand completarCita = new OracleCommand(
                                "UPDATE TALLER.AGENDA SET ESTADO = 'COMPLETADA' WHERE AGENDAID = :agendaId", con))
                            {
                                completarCita.Transaction = tx;
                                completarCita.Parameters.Add("agendaId", factura.AgendaID.Value);
                                if (completarCita.ExecuteNonQuery() != 1)
                                    throw new InvalidOperationException("La cita asociada ya no existe; no se guardó el documento.");
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

        public void AnularDocumento(int facturaId)
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
                        using (OracleCommand cmd = new OracleCommand(
                            "SELECT TIPO_DOCUMENTO, ESTADOPAGO FROM TALLER.FACTURA WHERE FACTURAID = :id FOR UPDATE", con))
                        {
                            cmd.Transaction = tx;
                            cmd.Parameters.Add("id", facturaId);
                            using (OracleDataReader reader = cmd.ExecuteReader())
                            {
                                if (!reader.Read()) throw new InvalidOperationException("No se encontró el documento.");
                                tipo = reader["TIPO_DOCUMENTO"].ToString();
                                estado = reader["ESTADOPAGO"].ToString();
                            }
                        }
                        if (string.Equals(estado, "Anulada", StringComparison.OrdinalIgnoreCase))
                            throw new InvalidOperationException("El documento ya está anulado.");

                        if (string.Equals(tipo, "VENTA", StringComparison.OrdinalIgnoreCase))
                        {
                            const string sqlDetalles = @"SELECT D.CANTIDAD, D.ARTICULO_ARTICULOID
                                FROM TALLER.DETALLE_FACTURA D
                                JOIN TALLER.ARTICULO A ON A.ARTICULOID = D.ARTICULO_ARTICULOID
                                JOIN TALLER.TIPO_ARTICULO T ON T.TIPO_ARTICULO_ID = A.TIPO_ARTICULO_ID
                                WHERE D.FACTURA_FACTURAID = :id AND UPPER(T.NOMBRE) = 'REPUESTO'";
                            var lineas = new List<KeyValuePair<int, int>>();
                            using (OracleCommand cmd = new OracleCommand(sqlDetalles, con))
                            {
                                cmd.Transaction = tx;
                                cmd.Parameters.Add("id", facturaId);
                                using (OracleDataReader reader = cmd.ExecuteReader())
                                    while (reader.Read()) lineas.Add(new KeyValuePair<int, int>(
                                        Convert.ToInt32(reader["ARTICULO_ARTICULOID"]), Convert.ToInt32(reader["CANTIDAD"])));
                            }
                            foreach (KeyValuePair<int, int> linea in lineas)
                            {
                                using (OracleCommand stock = new OracleCommand(
                                    "UPDATE TALLER.ARTICULO SET CANTIDAD = NVL(CANTIDAD, 0) + :cantidad WHERE ARTICULOID = :articuloId", con))
                                {
                                    stock.Transaction = tx;
                                    stock.BindByName = true;
                                    stock.Parameters.Add("cantidad", linea.Value);
                                    stock.Parameters.Add("articuloId", linea.Key);
                                    if (stock.ExecuteNonQuery() != 1) throw new InvalidOperationException("No se pudo devolver un repuesto al inventario.");
                                }
                            }
                        }
                        using (OracleCommand actualizar = new OracleCommand(
                            "UPDATE TALLER.FACTURA SET ESTADOPAGO = 'Anulada' WHERE FACTURAID = :id", con))
                        {
                            actualizar.Transaction = tx;
                            actualizar.Parameters.Add("id", facturaId);
                            if (actualizar.ExecuteNonQuery() != 1) throw new InvalidOperationException("No se pudo anular el documento.");
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
            cmd.Parameters.Add("clienteCedula", OracleDbType.Char, 10).Value = factura.Cliente != null ? (object)factura.Cliente.Cedula : DBNull.Value;
            cmd.Parameters.Add("empleadoCedula", OracleDbType.Char, 10).Value = factura.Empleado != null ? (object)factura.Empleado.Cedula : DBNull.Value;
            cmd.Parameters.Add(new OracleParameter("tipoDocumento", string.IsNullOrEmpty(factura.TipoDocumento) ? "VENTA" : factura.TipoDocumento));

            // En la BD DOMICILIO es CHAR(1): 'S' o 'N'
            cmd.Parameters.Add(new OracleParameter("domicilio", factura.Domicilio ? "S" : "N"));
            cmd.Parameters.Add(new OracleParameter("direccionDomicilio", factura.Domicilio && !string.IsNullOrEmpty(factura.DireccionDomicilio) ? (object)factura.DireccionDomicilio : DBNull.Value));
            cmd.Parameters.Add(new OracleParameter("agendaId", (object)factura.AgendaID ?? DBNull.Value));
        }

        private const string SelectFactura = @"SELECT F.FACTURAID, F.FECHA, F.DESCUENTO, F.SUBTOTAL, F.IVA, F.TOTAL,
                    F.OBSERVACIONES, F.ESTADOPAGO, F.CLIENTE_CEDULA, F.EMPLEADO_CEDULA, F.TIPO_DOCUMENTO,
                    F.DOMICILIO, F.DIRECCION_DOMICILIO, F.AGENDA_AGENDAID,
                    CP.PRIMER_NOMBRE AS CLIENTE_PRIMER_NOMBRE, CP.SEGUNDO_NOMBRE AS CLIENTE_SEGUNDO_NOMBRE,
                    CP.PRIMER_APELLIDO AS CLIENTE_PRIMER_APELLIDO, CP.SEGUNDO_APELLIDO AS CLIENTE_SEGUNDO_APELLIDO,
                    EP.PRIMER_NOMBRE AS EMPLEADO_PRIMER_NOMBRE, EP.SEGUNDO_NOMBRE AS EMPLEADO_SEGUNDO_NOMBRE,
                    EP.PRIMER_APELLIDO AS EMPLEADO_PRIMER_APELLIDO, EP.SEGUNDO_APELLIDO AS EMPLEADO_SEGUNDO_APELLIDO,
                    V.PLACA AS VEHICULO_PLACA, V.MODELO AS VEHICULO_MODELO, V.MARCA AS VEHICULO_MARCA,
                    V.TIPO_VEHICULO AS VEHICULO_TIPO, V.COLOR AS VEHICULO_COLOR, V.LINEA AS VEHICULO_LINEA
                FROM TALLER.FACTURA F
                LEFT JOIN TALLER.CLIENTE C ON C.CEDULA = F.CLIENTE_CEDULA
                LEFT JOIN TALLER.PERSONA CP ON CP.CEDULA = C.CEDULA
                LEFT JOIN TALLER.EMPLEADO E ON E.CEDULA = F.EMPLEADO_CEDULA
                LEFT JOIN TALLER.PERSONA EP ON EP.CEDULA = E.CEDULA
                LEFT JOIN TALLER.DETALLE_VEHICULO DV ON DV.FACTURA_FACTURAID = F.FACTURAID
                LEFT JOIN TALLER.VEHICULO V ON V.PLACA = DV.VEHICULO_PLACA";

        public List<Factura> Consultar()
        {
            List<Factura> facturas = new List<Factura>();

            using (OracleConnection con = ObtenerConexion())
            {
                using (OracleCommand cmd = new OracleCommand(SelectFactura + " ORDER BY F.FECHA DESC, F.FACTURAID DESC", con))
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
            using (OracleConnection con = ObtenerConexion())
            {
                using (OracleCommand cmd = new OracleCommand(SelectFactura + " WHERE F.FACTURAID = :facturaId", con))
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
            // Compatibilidad con consumidores anteriores: cancelar siempre conserva el documento.
            AnularDocumento(facturaId);
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
                    cmd.Parameters.Add("placa", OracleDbType.Char, 7).Value = detalle.Vehiculo.Placa;
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
