using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace DATOS
{
    public class NotificacionMantenimientoRepository : ManejadorConexion, IRepository<NotificacionMantenimiento>
    {
        private const string SelectSql = @"SELECT N.NOTIFICACIONID, N.FECHA, N.COMENTARIO, N.ESTADO,
                                          N.VEHICULO_PLACA, N.ARTICULO_ARTICULOID,
                                          A.NOMBRE AS ARTICULO_NOMBRE, A.CANTIDAD, A.DESCRIPCION, A.DURACION,
                                          A.PROVEEDOR_NIT, T.NOMBRE AS TIPO_ARTICULO
                                          FROM TALLER.NOTIFICACION_MANTENIMIENTO N
                                          JOIN TALLER.VEHICULO V ON V.PLACA = N.VEHICULO_PLACA
                                          JOIN TALLER.ARTICULO A ON A.ARTICULOID = N.ARTICULO_ARTICULOID
                                          JOIN TALLER.TIPO_ARTICULO T ON T.TIPO_ARTICULO_ID = A.TIPO_ARTICULO_ID";

        public void Guardar(NotificacionMantenimiento notificacion)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                con.Open();
                using (OracleCommand secuencia = new OracleCommand("SELECT TALLER.SEQ_NOTIFICACIONID.NEXTVAL FROM DUAL", con))
                    notificacion.NotificacionID = Convert.ToInt32(secuencia.ExecuteScalar());
                const string sql = @"INSERT INTO TALLER.NOTIFICACION_MANTENIMIENTO
                                     (NOTIFICACIONID, FECHA, COMENTARIO, ESTADO, VEHICULO_PLACA, ARTICULO_ARTICULOID)
                                     VALUES (:notificacionId, :fecha, :comentario, :estado, :placa, :articuloId)";
                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    AgregarParametros(cmd, notificacion);
                    cmd.Parameters.Add("notificacionId", notificacion.NotificacionID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<NotificacionMantenimiento> Consultar()
        {
            var lista = new List<NotificacionMantenimiento>();
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(SelectSql, con))
            {
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                    while (reader.Read()) lista.Add(Mapear(reader));
            }
            return lista;
        }

        public NotificacionMantenimiento BuscarId(int notificacionId)
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(SelectSql + " WHERE N.NOTIFICACIONID = :notificacionId", con))
            {
                cmd.Parameters.Add("notificacionId", notificacionId);
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                    return reader.Read() ? Mapear(reader) : null;
            }
        }

        public void Eliminar(NotificacionMantenimiento notificacion)
        {
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand("DELETE FROM TALLER.NOTIFICACION_MANTENIMIENTO WHERE NOTIFICACIONID = :notificacionId", con))
            {
                cmd.Parameters.Add("notificacionId", notificacion.NotificacionID);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Modificar(NotificacionMantenimiento notificacion)
        {
            const string sql = @"UPDATE TALLER.NOTIFICACION_MANTENIMIENTO SET FECHA = :fecha, COMENTARIO = :comentario,
                                 ESTADO = :estado,
                                 VEHICULO_PLACA = :placa, ARTICULO_ARTICULOID = :articuloId
                                 WHERE NOTIFICACIONID = :notificacionId";
            using (OracleConnection con = ObtenerConexion())
            using (OracleCommand cmd = new OracleCommand(sql, con))
            {
                AgregarParametros(cmd, notificacion);
                cmd.Parameters.Add("notificacionId", notificacion.NotificacionID);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private static void AgregarParametros(OracleCommand cmd, NotificacionMantenimiento notificacion)
        {
            cmd.BindByName = true;
            cmd.Parameters.Add("fecha", notificacion.Fecha);
            cmd.Parameters.Add("comentario", string.IsNullOrEmpty(notificacion.Comentario) ? (object)DBNull.Value : notificacion.Comentario);
            cmd.Parameters.Add("estado", notificacion.Estado);
            cmd.Parameters.Add("placa", notificacion.Vehiculo.Placa);
            cmd.Parameters.Add("articuloId", notificacion.Articulo.ArticuloId);
        }

        private static NotificacionMantenimiento Mapear(OracleDataReader reader)
        {
            Articulo articulo;
            if (string.Equals(reader["TIPO_ARTICULO"].ToString(), "Repuesto", StringComparison.OrdinalIgnoreCase))
            {
                articulo = new Repuesto
                {
                    Cantidad = reader["CANTIDAD"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CANTIDAD"]),
                    Proveedor = reader["PROVEEDOR_NIT"] == DBNull.Value ? null : new Proveedor { Nit = reader["PROVEEDOR_NIT"].ToString() }
                };
            }
            else
            {
                articulo = new Servicio
                {
                    Duracion = reader["DURACION"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DURACION"]),
                    Descripcion = reader["DESCRIPCION"] == DBNull.Value ? string.Empty : reader["DESCRIPCION"].ToString()
                };
            }
            articulo.ArticuloId = Convert.ToInt32(reader["ARTICULO_ARTICULOID"]);
            articulo.Nombre = reader["ARTICULO_NOMBRE"].ToString();

            return new NotificacionMantenimiento
            {
                NotificacionID = Convert.ToInt32(reader["NOTIFICACIONID"]),
                Fecha = Convert.ToDateTime(reader["FECHA"]),
                Comentario = reader["COMENTARIO"] == DBNull.Value ? string.Empty : reader["COMENTARIO"].ToString(),
                Estado = reader["ESTADO"].ToString(),
                Vehiculo = new Vehiculo { Placa = reader["VEHICULO_PLACA"].ToString().Trim() },
                Articulo = articulo
            };
        }
    }
}
