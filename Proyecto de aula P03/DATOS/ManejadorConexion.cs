using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class ManejadorConexion
    {
        private readonly string cadenaConexion;

        public ManejadorConexion()
        {
            ConnectionStringSettings configuracion = ConfigurationManager.ConnectionStrings["NombreAPPConfig"];
            if (configuracion == null || string.IsNullOrWhiteSpace(configuracion.ConnectionString))
                throw new ConfigurationErrorsException("Falta la cadena de conexión NombreAPPConfig en App.config. Configura la conexión local de Oracle antes de iniciar la aplicación.");
            cadenaConexion = configuracion.ConnectionString;

        }

        protected OracleConnection ObtenerConexion() => new OracleConnection(cadenaConexion);

        protected const string SqlGuardarPersona = @"MERGE INTO TALLER.PERSONA P
            USING (SELECT :cedula CEDULA FROM DUAL) N ON (P.CEDULA = N.CEDULA)
            WHEN MATCHED THEN UPDATE SET P.PRIMER_NOMBRE = :primerNombre, P.SEGUNDO_NOMBRE = :segundoNombre,
                P.PRIMER_APELLIDO = :primerApellido, P.SEGUNDO_APELLIDO = :segundoApellido,
                P.SEXO = :sexo, P.TELEFONO = :telefono, P.DIRECCION = :direccion, P.CORREOELECTRONICO = :correo
            WHEN NOT MATCHED THEN INSERT (CEDULA, PRIMER_NOMBRE, SEGUNDO_NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO,
                SEXO, TELEFONO, DIRECCION, CORREOELECTRONICO)
            VALUES (:cedula, :primerNombre, :segundoNombre, :primerApellido, :segundoApellido, :sexo, :telefono, :direccion, :correo)";

        // Serializa altas/bajas de roles para no perder ni duplicar la identidad compartida.
        protected static void BloquearIdentidades(OracleConnection con, OracleTransaction tx)
        {
            using (var cmd = new OracleCommand("LOCK TABLE TALLER.PERSONA IN SHARE ROW EXCLUSIVE MODE", con))
            {
                cmd.Transaction = tx;
                cmd.ExecuteNonQuery();
            }
        }

    }
}
