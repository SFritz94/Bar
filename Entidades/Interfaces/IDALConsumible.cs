using System.Collections.Generic;
using Entidades.Consumibles;

namespace Entidades.Interfaces
{
    public interface IDALConsumible
    {
        Consumible BuscarConsumiblePorId(int id);
        List<Consumible> MostrarTodosLosConsumibles();
        void InsertarConsumible(Consumible consumible);
        void ActualizarConsumible(Consumible consumible);
        void EliminarConsumible(int id);
        int ConsultarStockConsumible(Consumible consumible);
        void RestarStock(Consumible consumible, int cantidad);
        void SumarStock(Consumible consumible, int cantidad);
    }
}