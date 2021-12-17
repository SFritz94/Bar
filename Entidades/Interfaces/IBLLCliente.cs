using System.Collections.Generic;
using Entidades.CarpetaCliente;
using Entidades.ClasesGenerales;
using Entidades.Consumibles;
using Entidades.Lugares;

namespace Entidades.Interfaces
{
    public interface IBLLCliente
    {
        #region Metodos
        Cliente BuscarCliente(int id);
        List<Cliente> ListarTodosLosClientes();
        void ModificarCliente(Cliente cliente);
        void DesSuscribirseAlBar(int id);
        void AgregarMensaje(Mensaje mensaje, Cliente cliente);
        void MostrarMensaje(Cliente cliente);
        void SuscribirseAlBar(Cliente cliente);
        void RealizarPedido(Lugar lugar, Consumible consumible, Cliente cliente, int cantidad);
        void SentarseEnMesa(Lugar lugar, Cliente cliente);
        void Pagar(Lugar lugar, Cliente cliente);
        #endregion
    }
}