
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
                string sql = "SELECT Obtener_FacturaID() FROM DUAL";
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
                string sql = @"INSERT INTO Factura
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
                            "FROM Factura";
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
                             " FROM Factura" +
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
                string sql = "DELETE FROM Factura WHERE FACTURAID = :facturaId";
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
                string sql = @"UPDATE Factura SET 
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
                string sql = @"INSERT INTO DETALLE_VEHICULO (DETALLEVEHICULOID, FACTURA_FACTURAID, VEHICULO_PLACA)
                               VALUES (SEQ_DETALLEVEHICULOID.NEXTVAL, :facturaId, :placa)";

                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    cmd.BindByName = true;
                    cmd.Parameters.Add(new OracleParameter("facturaId", detalle.FacturaID));
                    cmd.Parameters.Add(new OracleParameter("placa", detalle.Vehiculo.Placa));
                    con.Open();
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
                               FROM DETALLE_VEHICULO DV
                               JOIN VEHICULO V ON DV.VEHICULO_PLACA = V.PLACA
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
