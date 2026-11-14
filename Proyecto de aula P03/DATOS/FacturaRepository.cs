using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class FacturaRepository : ManejadorConexion ,IRepository<Factura>
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
                         (FACTURAID, FECHA, DESCUENTO, SUBTOTAL, IVA, TOTAL, OBSERVACIONES, ESTADOPAGO, CLIENTE_CEDULA, EMPLEADO_CEDULA, TIPO_DOCUMENTO) 
                         VALUES (:facturaId, :fecha, :descuento, :subtotal, :iva, :total, :observaciones, :estadoPago, :clienteCedula, :empleadoCedula, :tipoDocumento)";
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
            cmd.Parameters.Add(new OracleParameter("facturaId", factura.FacturaID));
            cmd.Parameters.Add(new OracleParameter("fecha", factura.Fecha));
            cmd.Parameters.Add(new OracleParameter("descuento", factura.Descuento));
            cmd.Parameters.Add(new OracleParameter("subtotal", factura.SubTotal));
            cmd.Parameters.Add(new OracleParameter("iva", factura.Iva));
            cmd.Parameters.Add(new OracleParameter("total", factura.Total));
            cmd.Parameters.Add(new OracleParameter("observaciones", string.IsNullOrEmpty(factura.Observaciones)? (object)DBNull.Value : factura.Observaciones));
            cmd.Parameters.Add(new OracleParameter("estadoPago", factura.EstadoPago));
            cmd.Parameters.Add(new OracleParameter("clienteCedula", factura.Cliente != null? (object)factura.Cliente.Cedula:DBNull.Value));
            cmd.Parameters.Add(new OracleParameter("empleadoCedula", factura.Empleado != null ? (object)factura.Empleado.Cedula : DBNull.Value));
            cmd.Parameters.Add(new OracleParameter("tipoDocumento", string.IsNullOrEmpty(factura.TipoDocumento)? "VENTA" : factura.TipoDocumento));
        }

        public List<Factura> Consultar()
        {
            List<Factura> facturas = new List<Factura>();

            using (OracleConnection con = ObtenerConexion())
            {
                string sql = "SELECT FACTURAID, FECHA," +
                            " DESCUENTO, SUBTOTAL, IVA, TOTAL," +
                            " OBSERVACIONES, ESTADOPAGO, CLIENTE_CEDULA," +
                            " EMPLEADO_CEDULA, TIPO_DOCUMENTO " +
                            "FROM Factura";
                using (OracleCommand cmd = new OracleCommand(sql,con))
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
                TipoDocumento = reader["TIPO_DOCUMENTO"].ToString()
            };

            if (reader["CLIENTE_CEDULA"] != DBNull.Value)
            {
                factura.Cliente = new Cliente { Cedula = reader["CLIENTE_CEDULA"].ToString().Trim() };
            }

            if (reader["EMPLEADO_CEDULA"] != DBNull.Value)
            {
                factura.Empleado = new Empleado { Cedula = reader["EMPLEADO_CEDULA"].ToString().Trim() };
            }

            return factura;
        }

        public Factura BuscarId(int facturaId)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = "SELECT FACTURAID, FECHA, DESCUENTO, SUBTOTAL," +
                             " IVA, TOTAL, OBSERVACIONES, ESTADOPAGO, CLIENTE_CEDULA," +
                             " EMPLEADO_CEDULA, TIPO_DOCUMENTO" +
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
                                 TIPO_DOCUMENTO = :tipoDocumento 
                               WHERE FACTURAID = :facturaId";

                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    AgregarPararms(cmd, factura);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        
    }
}
