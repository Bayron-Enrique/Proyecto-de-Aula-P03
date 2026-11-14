using System.Collections.Generic;

namespace DATOS
{
    public interface IRepository<T>
    {
        void Guardar(T entidad);
        List<T> Consultar();
        void Modificar(T entidad);
    }
}
