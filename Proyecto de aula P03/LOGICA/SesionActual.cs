using ENTIDADES;

namespace LOGICA
{
    // Solo EmpleadoService inicia sesiones después de autenticar contra Oracle.
    public static class SesionActual
    {
        public static Empleado Empleado { get; private set; }
        internal static bool EsRespaldo { get; private set; }

        internal static void Iniciar(Empleado empleado, bool respaldo = false)
        {
            Empleado = empleado;
            EsRespaldo = respaldo;
        }

        public static void Cerrar()
        {
            Empleado = null;
            EsRespaldo = false;
        }
    }
}
