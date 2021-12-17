using System.Collections.Generic;
using Entidades.Facturacion;

namespace Entidades.Interfaces
{
    public interface IDALRegistro
    {
        Registro BuscarRegistroPorId(int id);
        List<Registro> MostrarTodosLosRegistros();
        void InsertarRegistro(Registro registro);
        void DarDeBajaRegistro(int id);
        void DarDeAltaRegistro(int id);
        double CalcularTotalRecaudado();
    }
}