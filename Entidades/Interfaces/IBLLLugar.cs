using System.Collections.Generic;
using Entidades.CarpetaCliente;
using Entidades.CarpetaPersonal;
using Entidades.Consumibles;
using Entidades.Lugares;

namespace Entidades.Interfaces
{
    public interface IBLLLugar
    {
        Lugar BuscarLugar(int id);
        List<Lugar> ListarTodosLosLugares();
        void AgregarLugar(Lugar lugar);
        void ModificarLugar(Lugar lugar);
        void BorrarLugar(int id);
        void AgregarConsumible(Lugar lugar, Consumible consumible, int cantidad);
        double CalcularTotalConsumido(Lugar lugar);
        void LimpiarListaConsumibles(Lugar lugar);
        void AtenderLugar(Lugar lugar, Personal personal);
        void DesAtenderLugar(Lugar lugar);
        void AsignarClienteALaBarra(Lugar lugar, Cliente cliente);
        void DesAsignarClienteALaBarra(Lugar lugar);
        void OcuparMesa(Lugar lugar,Cliente cliente);
        void DesOcuparMesa(Lugar lugar);
    }
}