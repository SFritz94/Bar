using System;
using System.Collections.Generic;
using DAL;
using Entidades.CarpetaCliente;
using Entidades.CarpetaPersonal;
using Entidades.Consumibles;
using Entidades.Interfaces;
using Entidades.Lugares;

namespace BLL
{
    public sealed class BLLLugar: IBLLLugar
    {
        public Lugar BuscarLugar(int id){
        var dalLugar=new DALLugar();
        return dalLugar.BuscarLugarPorId(id);
        }
        public List<Lugar> ListarTodosLosLugares(){
            var dalLugar=new DALLugar();
            return dalLugar.MostrarTodosLosLugares();
        }

        public void AgregarLugar(Lugar lugar){
            var dalLugar = new DALLugar();
            dalLugar.InsertarLugar(lugar);
        }

        public void ModificarLugar(Lugar lugar){
            var dalLugar = new DALLugar();
            dalLugar.ActualizarLugar(lugar);
        }

        public void BorrarLugar(int id){
            var dalLugar = new DALLugar();
            dalLugar.EliminarLugar(id);
        }

        public void AgregarConsumible(Lugar lugar, Consumible consumible, int cantidad){
            try{
                if(lugar.GetType() == typeof(Mesa)){
                    lugar.Consumido.Add(consumible);
                    lugar.Cantidades.Add(cantidad);
                }
                if(lugar.GetType() == typeof(Barra)){
                    if(SonSoloBebidas(consumible)){
                        lugar.Consumido.Add(consumible);
                        lugar.Cantidades.Add(cantidad);
                    }else{
                        Console.WriteLine("Solo puedes agregar bebidas a la barra."); 
                    }
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public double CalcularTotalConsumido(Lugar lugar){
            try{
                double total = 0;
                for(int i=0; i<lugar.Consumido.Count; i++){
                    total += lugar.Consumido[i].Precio*lugar.Cantidades[i];
                }
                return total;
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public void LimpiarListaConsumibles(Lugar lugar){
            try{
                lugar.Consumido.Clear();
                lugar.Cantidades.Clear();
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void AtenderLugar(Lugar lugar, Personal personal){
            try{
                lugar.PersonalAtendiendola = personal;
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void DesAtenderLugar(Lugar lugar){
            try{
                lugar.PersonalAtendiendola = null;
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void AsignarClienteALaBarra(Lugar lugar, Cliente cliente){
            try{
                lugar.Cliente=cliente;
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void DesAsignarClienteALaBarra(Lugar lugar){
            try{
                lugar.Cliente=null;
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void OcuparMesa(Lugar lugar,Cliente cliente){
            try{
                if(lugar.GetType() == typeof(Mesa)){
                    ((Mesa)lugar).EstaOcupada = true;
                    lugar.Cliente = cliente;
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void DesOcuparMesa(Lugar lugar){
            try{
                if(lugar.GetType() == typeof(Mesa)){
                    ((Mesa)lugar).EstaOcupada = false;
                    lugar.Cliente = null;
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        private bool SonSoloBebidas(Consumible consumible){
            try{
                if(consumible.TipoConsumible.Equals("Bebida")){
                    return true;
                }
                return false;
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}