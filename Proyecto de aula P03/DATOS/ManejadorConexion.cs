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
            cadenaConexion = ConfigurationManager.ConnectionStrings["NombreAPPConfig"].ConnectionString;

        }

        protected OracleConnection ObtenerConexion() => new OracleConnection(cadenaConexion);

    }
}
