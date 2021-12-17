using System;
using System.Collections.Generic;
using DAL;
using Entidades;
using Entidades.CarpetaPersonal;
using Entidades.Consumibles;
using Entidades.Facturacion;
using Entidades.Interfaces;
using Entidades.Lugares;

namespace BLL
{
    public sealed class BLLPersonal: IBLLPersonal
    {
        public Personal BuscarPersonal(int id){
            var dalPersonal=new DALPersonal();
            return dalPersonal.BuscarPersonalPorId(id);
        }
        public List<Personal> ListarTodoElPersonal(){
            var dalPersonal=new DALPersonal();
            return dalPersonal.MostrarTodoElPersonal();
        }

        public void AgregarPersonal(Personal empleado, Personal encargado){
            try{
                var dalPersonal = new DALPersonal();
                if(encargado.GetType()==typeof(Encargado)){
                    dalPersonal.InsertarPersonal(empleado);
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void ModificarPersonal(Personal empleado, Personal encargado){
            try{
                var dalPersonal = new DALPersonal();
                if(encargado.GetType()==typeof(Encargado)){
                    dalPersonal.ActualizarPersonal(empleado);
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void EliminarPersonal(int id, Personal encargado){
            try{
                var dalPersonal = new DALPersonal();
                if(encargado.GetType()==typeof(Encargado)){
                    dalPersonal.EliminarPersonal(id);
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void AltaConsumible(Personal encargado, Consumible consumible){
            try{
                var bllConsumible = new BLLConsumible();
                if(encargado.GetType()==typeof(Encargado)){
                    bllConsumible.AgregarConsumible(consumible);
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void ModificarConsumible(Personal encargado, Consumible consumible){
            try{
                var bllConsumible = new BLLConsumible();
                if(encargado.GetType()==typeof(Encargado)){
                    bllConsumible.EditarConsumible(consumible);
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void BajaConsumible(int id, Personal encargado){
            try{
                var bllConsumible = new BLLConsumible();
                if(encargado.GetType()==typeof(Encargado)){
                    bllConsumible.BorrarConsumible(id);
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void AltaLugar(Personal encargado, Lugar lugar){
            try{
                var bllLugar = new BLLLugar();
                if(encargado.GetType()==typeof(Encargado)){
                    bllLugar.AgregarLugar(lugar);
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void ModificarLugar(Personal encargado, Lugar lugar){
            try{
                var bllLugar = new BLLLugar();
                if(encargado.GetType()==typeof(Encargado)){
                    bllLugar.ModificarLugar(lugar);
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void BajaLugar(int id, Personal encargado){
            try{
                var bllLugar = new BLLLugar();
                if(encargado.GetType()==typeof(Encargado)){
                    bllLugar.BorrarLugar(id);
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void DarDeBajaRegistro(int id, Personal encargado){
            try{
                var bllRegistro = new BLLRegistro();
                if(encargado.GetType()==typeof(Encargado)){
                    bllRegistro.DarDeBajaRegistro(id);
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void DarDeAltaRegistro(int id, Personal encargado){
            try{
                var bllRegistro = new BLLRegistro();
                if(encargado.GetType()==typeof(Encargado)){
                    bllRegistro.DarDeAltaRegistro(id);
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void CargarHorarioEntrada(Personal personal){
            try{
                personal.HorarioEntrada = DateTime.Now;
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }
        public void CargarHorarioSalida(Personal personal){
            try{
                personal.HorarioSalida = DateTime.Now;
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void AtenderSector(Lugar lugar){
            try{
                var bllLugar = new BLLLugar();
                foreach(var empleado in Bar.Personal){
                    if(empleado.GetType()==typeof(Mozo)){
                        if(lugar.GetType() == typeof(Mesa) && ((Mesa)lugar).PersonalAtendiendola == null && ((Mozo)empleado).AtiendeMesa==false){
                            bllLugar.AtenderLugar(lugar, empleado);
                            ((Mozo)empleado).AtiendeMesa=true;
                        }
                    }
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void CargarConsumido(Lugar lugar, Consumible consumido, int cantidad){
            try{
                var bllLugar = new BLLLugar();
                var bllConsumible = new BLLConsumible();
                bllConsumible.sumarStock+=this.AgregarStock;
                foreach(var empleado in Bar.Personal){
                    if(empleado.GetType()==typeof(Mozo)){
                        if(lugar.GetType() == typeof(Mesa) && lugar.PersonalAtendiendola == ((Mozo)empleado)
                           && bllConsumible.ConsultarStockConsumible(consumido) > cantidad){
                            bllLugar.AgregarConsumible(lugar, consumido, cantidad);
                            ((Mozo)empleado).AtiendeMesa=false;
                            bllConsumible.RestarStock(consumido, cantidad);
                        }
                    }
                    if(empleado.GetType()==typeof(Barman)){
                        if(lugar.GetType() == typeof(Barra) && ((Barra)lugar).PersonalAtendiendola == ((Barman)empleado)
                           && bllConsumible.ConsultarStockConsumible(consumido) > cantidad){
                            bllLugar.AgregarConsumible(lugar, consumido, cantidad);
                            bllConsumible.RestarStock(consumido, cantidad);
                        }
                    }
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void GenerarRegistro(Lugar lugar){
            try{
                var bllLugar = new BLLLugar();
                var bllRegistro = new BLLRegistro();
                foreach(var empleado in Bar.Personal){
                    if(empleado.GetType()==typeof(Mozo)){
                        if(lugar.GetType() == typeof(Mesa) && lugar.Consumido.Count != 0
                            && lugar.PersonalAtendiendola == ((Mozo)empleado)){
                            Registro reg = new Registro(((Mesa)lugar).Cliente, ((Mozo)empleado), (Mesa)lugar, DateTime.Now, bllLugar.CalcularTotalConsumido((Mesa)lugar));
                            bllRegistro.GuardarRegistro(reg);
                            bllLugar.DesOcuparMesa(lugar);
                            bllLugar.DesAtenderLugar(lugar);
                            bllLugar.LimpiarListaConsumibles(lugar);
                        }
                    }
                    if(empleado.GetType()==typeof(Barman)){
                        if(lugar.GetType() == typeof(Barra) && lugar.Consumido.Count != 0 
                            && lugar.PersonalAtendiendola == ((Barman)empleado)){
                            Registro reg = new Registro(((Barra)lugar).Cliente, ((Barman)empleado), (Barra)lugar, DateTime.Now, bllLugar.CalcularTotalConsumido((Barra)lugar));
                            bllRegistro.GuardarRegistro(reg);
                            bllLugar.LimpiarListaConsumibles(lugar);
                            bllLugar.DesAsignarClienteALaBarra(lugar);
                        }
                    }
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void AtenderBarra(Lugar lugar, Personal personal){
            try{
                var bllLugar = new BLLLugar();
                if(personal.GetType()==typeof(Barman)){
                    if(lugar.GetType() == typeof(Barra)){
                        bllLugar.AtenderLugar(lugar, personal);
                    }
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void DejarBarra(Lugar lugar, Personal personal){
            try{
                var bllLugar = new BLLLugar();
                if(lugar.GetType() == typeof(Barra) && personal.GetType() == typeof(Barman)){
                    bllLugar.DesAtenderLugar(lugar);
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        private void AgregarStock(Consumible consumible, Personal personal){
            try{
                var bllConsumible = new BLLConsumible();
                if(personal.GetType()==typeof(Encargado)){
                    bllConsumible.SumarStock(consumible, 15);
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }
    }
}