using System.Collections.Generic;
using Entidades.Consumibles;
using Entidades.Facturacion;

namespace Entidades.Interfaces
{
    public interface IDALRegistroConsumible
    {
        List<Consumible> MostrarConsumiblesPorRegistro(int id);
        List<int> MostrarCantidadesConsumidas(int id);
        void InsertarRegistrosConsumibles(Registro registro, int idRegistro);
        void DarDeBajaConsumiblesDelRegistro(int id);
        void DarDeAltaConsumiblesDelRegistro(int id);
    }
}