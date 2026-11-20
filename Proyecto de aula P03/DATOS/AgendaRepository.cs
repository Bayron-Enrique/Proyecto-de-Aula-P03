using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace DATOS
{
    public class AgendaRepository : ManejadorConexion, IRepository<Agenda>
    {
        public int ContarPendientes()
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_AGENDA.PRC_CONTAR_PENDIENTES", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_total", OracleDbType.Int32).Direction = ParameterDirection.Output;
                conexion.Open(); comando.ExecuteNonQuery();
                return Convert.ToInt32(comando.Parameters["p_total"].Value.ToString());
            }
        }
        public bool EmpleadoDisponible(string cedulaEmpleado, DateTime fecha)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_AGENDA.PRC_VALIDAR_DISPONIBILIDAD", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_empleado", OracleDbType.Char, 10).Value = cedulaEmpleado;
                comando.Parameters.Add("p_fecha", OracleDbType.Date).Value = fecha;
                comando.Parameters.Add("p_disponible", OracleDbType.Int32).Direction = ParameterDirection.Output;
                conexion.Open(); comando.ExecuteNonQuery();
                return Convert.ToInt32(comando.Parameters["p_disponible"].Value.ToString()) == 1;
            }
        }
        public void Guardar(Agenda agenda)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_AGENDA.PRC_GUARDAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                AgregarParams(comando, agenda);
                OracleParameter parametroId = comando.Parameters.Add("p_id", OracleDbType.Int32);
                parametroId.Direction = ParameterDirection.InputOutput; parametroId.Value = 0;
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); agenda.AgendaID = Convert.ToInt32(parametroId.Value.ToString()); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }
        public void Modificar(Agenda agenda)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_AGENDA.PRC_MODIFICAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                AgregarParams(comando, agenda);
                comando.Parameters.Add("p_id", OracleDbType.Int32).Value = agenda.AgendaID;
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }
        public void CambiarEstado(int agendaId,string estado)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_AGENDA.PRC_CAMBIAR_ESTADO", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_id", OracleDbType.Int32).Value = agendaId;
                comando.Parameters.Add("p_estado", OracleDbType.Varchar2).Value = estado;
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }
        public List<Agenda> Consultar()
        {
            List<Agenda> agendas = new List<Agenda>();
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_AGENDA.PRC_CONSULTAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                conexion.Open();
                using (OracleDataReader reader = comando.ExecuteReader()) while (reader.Read()) agendas.Add(Mapear(reader));
            }
            return agendas;
        }
        public Agenda BuscarId(int agendaId)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_AGENDA.PRC_BUSCAR_ID", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_id", OracleDbType.Int32).Value = agendaId;
                comando.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                conexion.Open();
                using (OracleDataReader reader = comando.ExecuteReader()) return reader.Read() ? Mapear(reader) : null;
            }
        }
        public void Eliminar(Agenda agenda)
        {
            using (OracleConnection conexion = ObtenerConexion())
            using (OracleCommand comando = new OracleCommand("TALLER.PKG_AGENDA.PRC_ELIMINAR", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_id", OracleDbType.Int32).Value = agenda.AgendaID;
                conexion.Open();
                using (OracleTransaction transaccion = conexion.BeginTransaction())
                {
                    comando.Transaction = transaccion;
                    try { comando.ExecuteNonQuery(); transaccion.Commit(); }
                    catch { transaccion.Rollback(); throw; }
                }
            }
        }
        private static void AgregarParams(OracleCommand comando, Agenda agenda)
        {
            if (agenda == null || agenda.Vehiculo == null || agenda.Empleado == null) throw new ArgumentException("La cita requiere vehículo y empleado.");
            comando.BindByName = true;
            comando.Parameters.Add("p_fecha", OracleDbType.Date).Value = agenda.Fecha == DateTime.MinValue ? (object)DBNull.Value : new DateTime(agenda.Fecha.Year, agenda.Fecha.Month, agenda.Fecha.Day, agenda.Fecha.Hour, agenda.Fecha.Minute, 0);
            comando.Parameters.Add("p_comentario", OracleDbType.Varchar2).Value = (object)agenda.Comentario ?? DBNull.Value;
            comando.Parameters.Add("p_placa", OracleDbType.Char, 7).Value = agenda.Vehiculo.Placa;
            comando.Parameters.Add("p_empleado", OracleDbType.Char, 10).Value = agenda.Empleado.Cedula;
            comando.Parameters.Add("p_estado", OracleDbType.Varchar2).Value = agenda.Estado;
            comando.Parameters.Add("p_articulo", OracleDbType.Int32).Value = agenda.ArticuloServicio == null ? (object)DBNull.Value : agenda.ArticuloServicio.ArticuloId;
        }
        private Agenda Mapear(OracleDataReader r)=>new Agenda
        {
            AgendaID=Convert.ToInt32(r["AGENDAID"]),Fecha=r["FECHA"]==DBNull.Value?DateTime.MinValue:Convert.ToDateTime(r["FECHA"]),
            Comentario=r["COMENTARIO"]==DBNull.Value?string.Empty:r["COMENTARIO"].ToString(),Estado=r["ESTADO"].ToString(),
            Vehiculo=new Vehiculo { Placa=r["VEHICULO_PLACA"].ToString().Trim(),Cliente=new Cliente { Cedula=r["CLIENTE_CEDULA"].ToString().Trim(),
                PrimerNombre=r["CLIENTE_PRIMER_NOMBRE"].ToString(),SegundoNombre=r["CLIENTE_SEGUNDO_NOMBRE"]==DBNull.Value?null:r["CLIENTE_SEGUNDO_NOMBRE"].ToString(),
                PrimerApellido=r["CLIENTE_PRIMER_APELLIDO"].ToString(),SegundoApellido=r["CLIENTE_SEGUNDO_APELLIDO"]==DBNull.Value?null:r["CLIENTE_SEGUNDO_APELLIDO"].ToString()} },
            Empleado=new Empleado { Cedula=r["EMPLEADO_CEDULA"].ToString().Trim(),PrimerNombre=r["EMPLEADO_PRIMER_NOMBRE"].ToString(),
                SegundoNombre=r["EMPLEADO_SEGUNDO_NOMBRE"]==DBNull.Value?null:r["EMPLEADO_SEGUNDO_NOMBRE"].ToString(),PrimerApellido=r["EMPLEADO_PRIMER_APELLIDO"].ToString(),
                SegundoApellido=r["EMPLEADO_SEGUNDO_APELLIDO"]==DBNull.Value?null:r["EMPLEADO_SEGUNDO_APELLIDO"].ToString(),
                Cargo=new Cargo { CargoId=Convert.ToInt32(r["CARGOID"]),NombreCargo=r["NOMBRECARGO"].ToString()} },
            ArticuloServicio=r["ARTICULO_ARTICULOID"]==DBNull.Value?null:(Articulo)new Servicio { ArticuloId=Convert.ToInt32(r["ARTICULO_ARTICULOID"]),Nombre=r["NOMBRE_ARTICULO"].ToString(),Duracion=r["DURACION_SERVICIO"]==DBNull.Value?0:Convert.ToInt32(r["DURACION_SERVICIO"]) }
        };
    }
}
