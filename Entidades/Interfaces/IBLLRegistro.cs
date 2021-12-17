using System.Collections.Generic;
using Entidades.Facturacion;

namespace Entidades.Interfaces
{
    public interface IBLLRegistro
    {
        Registro BuscarRegistroPorId(int id);
        List<Registro> ListarTodosLosRegistros();
        void GuardarRegistro(Registro registro);
        void DarDeBajaRegistro(int id);
        void DarDeAltaRegistro(int id);
        double CalcularTotalRecaudado();
    }
}