using ENTIDADES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class AgendaRepository : ManejadorConexion, IRepository<Agenda>
    {
        public void Guardar(Agenda agenda)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = @"INSERT INTO Agenda 
                                 (AGENDAID, FECHA, COMENTARIO, VEHICULO_PLACA, EMPLEADO_CEDULA, ESTADO, ARTICULO_ARTICULOID) 
                                 VALUES (Seq_AgendaID.NEXTVAL, :fecha, :comentario, :vehiculoPlaca, :empleadoCedula, :estado, :articuloId)";
            
                using ( OracleCommand cmd = new OracleCommand(sql, con))
                {
                    AgregarParams(cmd, agenda);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private void AgregarParams(OracleCommand cmd, Agenda agenda)
        {
            cmd.Parameters.Add(new OracleParameter("fecha", agenda.Fecha));
            cmd.Parameters.Add(new OracleParameter("comentario", string.IsNullOrEmpty(agenda.Comentario) ? (object)DBNull.Value : agenda.Comentario));
            cmd.Parameters.Add(new OracleParameter("vehiculoPlaca", agenda.Vehiculo.Placa));
            cmd.Parameters.Add(new OracleParameter("empleadoCedula", agenda.Empleado.Cedula));
            cmd.Parameters.Add(new OracleParameter("estado", agenda.Estado));

            if (agenda.ArticuloServicio != null)
            {
                cmd.Parameters.Add(new OracleParameter("articuloId", agenda.ArticuloServicio.ArticuloId));
            }
            else
            {
                cmd.Parameters.Add(new OracleParameter("articuloId", DBNull.Value));
            }
        }

        public List<Agenda> Consultar()
        {
            List<Agenda> agendas = new List<Agenda>();
            using(OracleConnection con = ObtenerConexion())
            {
                string sql = @"SELECT AGENDAID,
                            FECHA, COMENTARIO, VEHICULO_PLACA, 
                            EMPLEADO_CEDULA, ESTADO, ARTICULO_ARTICULOID 
                            FROM Agenda";
                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    con.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            agendas.Add(Mapear(reader));
                        }
                    }
                }
                
            }
            return agendas;
        }

        private Agenda Mapear(OracleDataReader reader)
        {
            
            return new Agenda
            {
                AgendaID = Convert.ToInt32(reader["AGENDAID"]),
                Fecha = Convert.ToDateTime(reader["FECHA"]),
                Comentario = reader["COMENTARIO"] != DBNull.Value ? reader["COMENTARIO"].ToString() : string.Empty,
                Estado = reader["ESTADO"].ToString(),

                // Instanciamos los objetos solo con su llave primaria para mantener la estructura orientada a objetos
                Vehiculo = new Vehiculo { Placa = reader["VEHICULO_PLACA"].ToString() },
                Empleado = new Empleado { Cedula = reader["EMPLEADO_CEDULA"].ToString() },
                ArticuloServicio = reader["ARTICULO_ARTICULOID"] != DBNull.Value
                    ? new Servicio { ArticuloId = Convert.ToInt32(reader["ARTICULO_ARTICULOID"]) }
                    : null
            };
        }

        public void Modificar(Agenda entidad)
        {
            throw new NotImplementedException();
        }

        public Agenda BuscarId(int agendaId)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = @"SELECT AGENDAID, FECHA, COMENTARIO, VEHICULO_PLACA, 
                               EMPLEADO_CEDULA, ESTADO, 
                               ARTICULO_ARTICULOID 
                               FROM Agenda 
                               WHERE AGENDAID = :agendaId";

                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    cmd.Parameters.Add(new OracleParameter("agendaId", agendaId));
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

        public void Eliminar(Agenda agenda)
        {
            using (OracleConnection con = ObtenerConexion())
            {
                string sql = @"DELETE FROM Agenda WHERE AGENDAID = :agendaId";

                using (OracleCommand cmd = new OracleCommand(sql,con))
                {
                    cmd.Parameters.Add(new OracleParameter("agendaId", agenda.AgendaID));
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
