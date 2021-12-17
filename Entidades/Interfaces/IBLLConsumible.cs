using System.Collections.Generic;
using Entidades.Consumibles;

namespace Entidades.Interfaces
{
    public interface IBLLConsumible
    {
        Consumible BuscarConsumible(int id);
        List<Consumible> ListarTodosLosConsumibles();
        int ConsultarStockConsumible(Consumible consumible);
        void AgregarConsumible(Consumible consumible);
        void EditarConsumible(Consumible consumible);
        void BorrarConsumible(int id);
        void RestarStock(Consumible consumible, int cantidad);
        void SumarStock(Consumible consumible, int cantidad);
    }
}