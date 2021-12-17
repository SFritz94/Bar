using System;
using System.Linq;
using BLL;
using Entidades;

namespace Programa
{
    class Program
    {
        static void Main(string[] args)
        {
            var bllPersonal = new BLLPersonal();
            var bllCliente = new BLLCliente();
            var bllLugar = new BLLLugar();
            var bllConsumible = new BLLConsumible();
            var bllRegistro = new BLLRegistro();
            bllCliente.realizarPedido += bllPersonal.CargarConsumido;
            bllCliente.sentarseEnMesa += bllPersonal.AtenderSector;
            bllCliente.realizarPago += bllPersonal.GenerarRegistro;
            BLLBar.enviarMensaje += bllCliente.MostrarMensaje;
            BLLBar.AgregarListaPersonal(bllPersonal.ListarTodoElPersonal());
            BLLBar.AgregarClientes(bllCliente.ListarTodosLosClientes());
            BLLBar.AgregarLugares(bllLugar.ListarTodosLosLugares());
            foreach(var empleado in Bar.Personal){
                bllPersonal.CargarHorarioEntrada(empleado);
            }
            BLLBar.EnviarMensajeAbrimos();
            BLLBar.MostrarMenu();

            bllCliente.SentarseEnMesa(Bar.Lugares[1], Bar.Clientes[3]);
            bllCliente.SentarseEnMesa(Bar.Lugares[2], Bar.Clientes[2]);

            bllPersonal.AtenderBarra(Bar.Lugares[0],Bar.Personal.Where(emp => emp.Cargo.Equals("Barman")).ToList().First());

            bllCliente.RealizarPedido(Bar.Lugares[1], bllConsumible.BuscarConsumible(5), Bar.Clientes[3], 1);
            bllCliente.RealizarPedido(Bar.Lugares[1], bllConsumible.BuscarConsumible(12), Bar.Clientes[3], 1);
            bllCliente.RealizarPedido(Bar.Lugares[1], bllConsumible.BuscarConsumible(17), Bar.Clientes[3], 1);
            bllCliente.RealizarPedido(Bar.Lugares[2], bllConsumible.BuscarConsumible(9), Bar.Clientes[2], 1);
            bllCliente.RealizarPedido(Bar.Lugares[2], bllConsumible.BuscarConsumible(11), Bar.Clientes[2], 1);
            bllCliente.RealizarPedido(Bar.Lugares[2], bllConsumible.BuscarConsumible(16), Bar.Clientes[2], 1);
            bllCliente.RealizarPedido(Bar.Lugares[0], bllConsumible.BuscarConsumible(3), Bar.Clientes[1], 2);

            bllCliente.Pagar(Bar.Lugares[1], Bar.Clientes[3]);
            bllCliente.Pagar(Bar.Lugares[2], Bar.Clientes[2]);

            BLLBar.MostrarInfoRegistros();
            Bar.TotalRecaudado = BLLBar.TotalRecaudado();
            Console.WriteLine($"Total recaudado: ${Bar.TotalRecaudado}");

            foreach(var empleado in Bar.Personal){
                bllPersonal.CargarHorarioSalida(empleado);
            }
        }
    }
}
