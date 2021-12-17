using System.Collections.Generic;
using Entidades.Lugares;

namespace Entidades.Interfaces
{
    public interface IDALLugar
    {
        Lugar BuscarLugarPorId(int id);
        List<Lugar> MostrarTodosLosLugares();
        void InsertarLugar(Lugar lugar);
        void ActualizarLugar(Lugar lugar);
        void EliminarLugar(int id);
    }
}