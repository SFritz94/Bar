using System.Collections.Generic;
using Entidades.CarpetaCliente;

namespace Entidades.Interfaces
{
    public interface IDALCliente
    {
        Cliente BuscarClientePorId(int id);
        List<Cliente> MostrarTodosLosClientes();
        void InsertarCliente(Cliente cliente);
        void ActualizarCliente(Cliente cliente);
        void EliminarCliente(int id);
    }
}