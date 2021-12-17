using System.Collections.Generic;
using DAL;
using Entidades.Facturacion;
using Entidades.Interfaces;

namespace BLL
{
    public sealed class BLLRegistro: IBLLRegistro
    {
        public Registro BuscarRegistroPorId(int id){
            var dalRegistro=new DALRegistro();
            return dalRegistro.BuscarRegistroPorId(id);
        }
        public List<Registro> ListarTodosLosRegistros(){
            var dalRegistro=new DALRegistro();
            return dalRegistro.MostrarTodosLosRegistros();
        }

        public void GuardarRegistro(Registro registro){
            var dalRegistro=new DALRegistro();
            dalRegistro.InsertarRegistro(registro);
        }

        public void DarDeBajaRegistro(int id){
            var dalRegistro=new DALRegistro();
            dalRegistro.DarDeBajaRegistro(id);
        }

        public void DarDeAltaRegistro(int id){
            var dalRegistro=new DALRegistro();
            dalRegistro.DarDeAltaRegistro(id);
        }

        public double CalcularTotalRecaudado(){
            var dalRegistro = new DALRegistro();
            return dalRegistro.CalcularTotalRecaudado();
        }
    }
}