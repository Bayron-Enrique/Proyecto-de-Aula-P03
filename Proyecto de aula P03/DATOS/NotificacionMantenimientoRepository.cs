using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace DATOS
{
    public class NotificacionMantenimientoRepository : ManejadorConexion, IRepository<NotificacionMantenimiento>
    {
        public void Guardar(NotificacionMantenimiento notificacion)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_NOTIFICACION_MANTENIMIENTO.GUARDAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                AgregarParams(comando, notificacion);
                OracleParameter parametroId = comando.Parameters.Add("p_id", OracleDbType.Int32);
                parametroId.Direction = ParameterDirection.Output;
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); notificacion.NotificacionID = Convert.ToInt32(parametroId.Value.ToString()); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }
        public void Modificar(NotificacionMantenimiento notificacion)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_NOTIFICACION_MANTENIMIENTO.MODIFICAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                AgregarParams(comando, notificacion);
                comando.Parameters.Add("p_id", OracleDbType.Int32).Value = notificacion.NotificacionID;
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }
        public List<NotificacionMantenimiento> Consultar()
        {
            List<NotificacionMantenimiento> notificaciones = new List<NotificacionMantenimiento>();
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_NOTIFICACION_MANTENIMIENTO.CONSULTAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                conexion.Open(); using (OracleDataReader reader = comando.ExecuteReader())
                    while (reader.Read()) notificaciones.Add(Mapear(reader));
            }
            return notificaciones;
        }
        public NotificacionMantenimiento BuscarId(int id)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_NOTIFICACION_MANTENIMIENTO.BUSCAR_ID", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_id", OracleDbType.Int32).Value = id;
                comando.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                conexion.Open(); using (OracleDataReader reader = comando.ExecuteReader()) return reader.Read() ? Mapear(reader) : null;
            }
        }
        public void Eliminar(NotificacionMantenimiento notificacion)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_NOTIFICACION_MANTENIMIENTO.ELIMINAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_id", OracleDbType.Int32).Value = notificacion.NotificacionID;
                conexion.Open(); comando.ExecuteNonQuery();
            }
        }
        private static void AgregarParams(OracleCommand comando, NotificacionMantenimiento notificacion)
        {
            comando.BindByName = true;
            comando.Parameters.Add("p_fecha", OracleDbType.Date).Value = notificacion.Fecha;
            comando.Parameters.Add("p_comentario", OracleDbType.Varchar2).Value = (object)notificacion.Comentario ?? DBNull.Value;
            comando.Parameters.Add("p_estado", OracleDbType.Varchar2).Value = notificacion.Estado;
            comando.Parameters.Add("p_placa", OracleDbType.Char, 7).Value = notificacion.Vehiculo.Placa;
            comando.Parameters.Add("p_articulo_id", OracleDbType.Int32).Value = notificacion.Articulo.ArticuloId;
        }
        private static NotificacionMantenimiento Mapear(OracleDataReader reader)
        {
            Articulo articulo;
            if (string.Equals(reader["TIPO_ARTICULO"].ToString(), "Repuesto", StringComparison.OrdinalIgnoreCase))
                articulo = new Repuesto { Cantidad = reader["CANTIDAD"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CANTIDAD"]),
                    Proveedor = reader["PROVEEDOR_NIT"] == DBNull.Value ? null : new Proveedor { Nit = reader["PROVEEDOR_NIT"].ToString() } };
            else articulo = new Servicio { Duracion = reader["DURACION"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DURACION"]),
                Descripcion = reader["DESCRIPCION"] == DBNull.Value ? string.Empty : reader["DESCRIPCION"].ToString() };
            articulo.ArticuloId = Convert.ToInt32(reader["ARTICULO_ARTICULOID"]);
            articulo.Nombre = reader["ARTICULO_NOMBRE"].ToString();
            return new NotificacionMantenimiento
            {
                NotificacionID = Convert.ToInt32(reader["NOTIFICACIONID"]),
                Fecha = reader["FECHA"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FECHA"]),
                Comentario = reader["COMENTARIO"] == DBNull.Value ? string.Empty : reader["COMENTARIO"].ToString(), Estado = reader["ESTADO"].ToString(),
                Vehiculo = new Vehiculo { Placa = reader["VEHICULO_PLACA"].ToString().Trim(), Marca = reader["VEHICULO_MARCA"].ToString(),
                    Linea = reader["VEHICULO_LINEA"].ToString(), Cliente = new Cliente { Cedula = reader["CLIENTE_CEDULA"].ToString().Trim(),
                        PrimerNombre = reader["PRIMER_NOMBRE"].ToString(), SegundoNombre = reader["SEGUNDO_NOMBRE"] == DBNull.Value ? null : reader["SEGUNDO_NOMBRE"].ToString(),
                        PrimerApellido = reader["PRIMER_APELLIDO"].ToString(), SegundoApellido = reader["SEGUNDO_APELLIDO"] == DBNull.Value ? null : reader["SEGUNDO_APELLIDO"].ToString(),
                        CorreoElectronico = reader["CORREOELECTRONICO"] == DBNull.Value ? null : reader["CORREOELECTRONICO"].ToString() } }, Articulo = articulo
            };
        }
    }
}
