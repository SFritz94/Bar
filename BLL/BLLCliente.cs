using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using Entidades;
using Entidades.CarpetaCliente;
using Entidades.ClasesGenerales;
using Entidades.Consumibles;
using Entidades.Interfaces;
using Entidades.Lugares;

namespace BLL
{
    public sealed class BLLCliente: IBLLCliente
    {
        public delegate void TipoDelegadoPedir(Lugar lugar, Consumible consumido, int cantidad);
        public event TipoDelegadoPedir realizarPedido;
        public delegate void TipoDelegadoSentarse(Lugar lugar);
        public event TipoDelegadoSentarse sentarseEnMesa;
        public delegate void TipoDelegadoPagar(Lugar lugar);
        public event TipoDelegadoPagar realizarPago;

        public Cliente BuscarCliente(int id){
            var dalCliente=new DALCliente();
            return dalCliente.BuscarClientePorId(id);
        }
        public List<Cliente> ListarTodosLosClientes(){
            var dalCliente=new DALCliente();
            return dalCliente.MostrarTodosLosClientes();
        }
        
        public void ModificarCliente(Cliente cliente){
            var dalCliente=new DALCliente();
            dalCliente.ActualizarCliente(cliente);
        }
        public void DesSuscribirseAlBar(int id){
            var dalCliente=new DALCliente();
            Bar.Clientes.Remove(this.BuscarCliente(id));
            dalCliente.EliminarCliente(id);
        }

        public void AgregarMensaje(Mensaje mensaje, Cliente cliente){
            cliente.Mensajes.Add(mensaje);
        }

        public void MostrarMensaje(Cliente cliente){
            Console.WriteLine(cliente.Mensajes.Last().Contenido);
        }

        public void SuscribirseAlBar(Cliente cliente){
            var dalCliente=new DALCliente();
            dalCliente.InsertarCliente(cliente);
            BLLBar.enviarMensaje += MostrarMensaje;
        }

        public void RealizarPedido(Lugar lugar, Consumible consumible, Cliente cliente, int cantidad){
            try{
                var bllLugar = new BLLLugar();
                if(lugar.GetType() == typeof(Barra)){
                    bllLugar.AsignarClienteALaBarra(lugar, cliente);
                    realizarPedido(lugar, consumible, cantidad);
                    realizarPago(lugar);
                }else if(((Mesa)lugar).Cliente == cliente){
                    realizarPedido(lugar, consumible, cantidad);
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void SentarseEnMesa(Lugar lugar, Cliente cliente){
            try{
                var bllLugar = new BLLLugar();
                if(((Mesa)lugar).EstaOcupada != true){
                    bllLugar.OcuparMesa(lugar, cliente);
                    sentarseEnMesa(lugar);
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void Pagar(Lugar lugar, Cliente cliente){
            try{
                if(cliente == ((Mesa)lugar).Cliente){
                    realizarPago(lugar);
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }
    }
}