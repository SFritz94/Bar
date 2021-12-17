using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using Entidades;
using Entidades.CarpetaPersonal;
using Entidades.Consumibles;
using Entidades.Interfaces;

namespace BLL
{
    public sealed class BLLConsumible: IBLLConsumible
    {
        const int STOCK_MINIMO = 5;
        public delegate void TipoDelegadoSumarStock(Consumible consumible, Personal personal);
        public event TipoDelegadoSumarStock sumarStock;

        public Consumible BuscarConsumible(int id){
            var dalConsumible=new DALConsumible();
            return dalConsumible.BuscarConsumiblePorId(id);
        }
        public List<Consumible> ListarTodosLosConsumibles(){
            var dalConsumible=new DALConsumible();
            return dalConsumible.MostrarTodosLosConsumibles();
        }

        public int ConsultarStockConsumible(Consumible consumible){
            var dalConsumible = new DALConsumible();
            return dalConsumible.ConsultarStockConsumible(consumible);
        }

        public void AgregarConsumible(Consumible consumible){
            var dalConsumible = new DALConsumible();
            dalConsumible.InsertarConsumible(consumible);
        }

        public void EditarConsumible(Consumible consumible){
            var dalConsumible = new DALConsumible();
            dalConsumible.ActualizarConsumible(consumible);
        }

        public void BorrarConsumible(int id){
            var dalConsumible = new DALConsumible();
            dalConsumible.EliminarConsumible(id);
        }

        public void RestarStock(Consumible consumible, int cantidad){
            try{
                var dalConsumible = new DALConsumible();
                var encargado = Bar.Personal.Where(empleado => empleado.Cargo.Equals("Encargado")).ToList();
                dalConsumible.RestarStock(consumible, cantidad);
                if(dalConsumible.ConsultarStockConsumible(consumible)<STOCK_MINIMO){
                    sumarStock(consumible, encargado.First());
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void SumarStock(Consumible consumible, int cantidad){
            var dalConsumible = new DALConsumible();
            dalConsumible.SumarStock(consumible, cantidad);
        }
    }
}