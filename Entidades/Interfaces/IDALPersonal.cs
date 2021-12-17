using System.Collections.Generic;
using Entidades.CarpetaPersonal;

namespace Entidades.Interfaces
{
    public interface IDALPersonal
    {
        Personal BuscarPersonalPorId(int id);
        List<Personal> MostrarTodoElPersonal();
        void InsertarPersonal(Personal personal);
        void ActualizarPersonal(Personal personal);
        void EliminarPersonal(int id);
    }
}