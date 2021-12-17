using System.Collections.Generic;
using Entidades.CarpetaPersonal;
using Entidades.Consumibles;
using Entidades.Lugares;

namespace Entidades.Interfaces
{
    public interface IBLLPersonal
    {
        #region Metodos
        Personal BuscarPersonal(int id);
        List<Personal> ListarTodoElPersonal();
        void AgregarPersonal(Personal empleado, Personal encargado);
        void ModificarPersonal(Personal empleado, Personal encargado);
        void EliminarPersonal(int id, Personal encargado);
        void AltaConsumible(Personal encargado, Consumible consumible);
        void ModificarConsumible(Personal encargado, Consumible consumible);
        void BajaConsumible(int id, Personal encargado);
        void AltaLugar(Personal encargado, Lugar lugar);
        void ModificarLugar(Personal encargado, Lugar lugar);
        void BajaLugar(int id, Personal encargado);
        void DarDeBajaRegistro(int id, Personal encargado);
        void DarDeAltaRegistro(int id, Personal encargado);
        void CargarHorarioEntrada(Personal personal);
        void CargarHorarioSalida(Personal personal);
        void AtenderSector(Lugar lugar);
        void CargarConsumido(Lugar lugar, Consumible consumido, int cantidad);
        void GenerarRegistro(Lugar lugar);
        void AtenderBarra(Lugar lugar, Personal personal);
        void DejarBarra(Lugar lugar, Personal personal);
        #endregion
    }
}