using System;
using System.Collections.Generic;
using System.Data;
using Entidades.Consumibles;
using Entidades.Facturacion;
using Entidades.Interfaces;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public sealed class DALRegistroConsumible: IDALRegistroConsumible
    {
        Operaciones objetoOperaciones;
        public DALRegistroConsumible(){
            objetoOperaciones = new Operaciones();
        }
        public List<Consumible> MostrarConsumiblesPorRegistro(int id){
            var dalConsumible = new DALConsumible();
            var listaConsumible = new List<Consumible>();

            try{
                var consulta = "select * from RegistrosConsumibles where IdRegistro = @pId and Activo = 1";
                var parametroId= new SqlParameter("@pId", id);
                var parametros = new SqlParameter[]{parametroId}; 
                DataTable dataTable = objetoOperaciones.MostrarDataTable(consulta,parametros);
                foreach(DataRow dataRow in dataTable.Rows){
                    var consumible = dalConsumible.BuscarConsumiblePorId(Int32.Parse(dataRow["IdConsumible"].ToString()));
                    listaConsumible.Add(consumible);
                }
                if(listaConsumible.Count == 0){
                    throw new Exception("No existen consumibles para este registro.");
                }else{
                    return listaConsumible; 
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<int> MostrarCantidadesConsumidas(int id){
            var dalConsumible = new DALConsumible();
            var listaCantidades = new List<int>();

            try{
                var consulta = "select * from RegistrosConsumibles where IdRegistro = @pId and Activo = 1";
                var parametroId= new SqlParameter("@pId", id);
                var parametros = new SqlParameter[]{parametroId}; 
                DataTable dataTable = objetoOperaciones.MostrarDataTable(consulta,parametros);
                foreach(DataRow dataRow in dataTable.Rows){
                    var cantidad = Int32.Parse(dataRow["Cantidad"].ToString());
                    listaCantidades.Add(cantidad);
                }
                if(listaCantidades.Count == 0){
                    throw new Exception("No existen cantidades para los consumibles de este registro.");
                }else{
                    return listaCantidades;
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void InsertarRegistrosConsumibles(Registro registro, int idRegistro){
            try{
                var consulta ="insert into RegistrosConsumibles(IdRegistro, IdConsumible, Cantidad, SubTotal, Activo) values(@pIdRegistro, @pIdConsumible, @pCantidad, @pSubtotal, @pActivo)";
                for(int i=0; i<registro.Lugar.Consumido.Count; i++){
                    var parametroIdRegistro = new SqlParameter("@pIdRegistro", idRegistro);
                    var parametroIdConsumible = new SqlParameter("@pIdConsumible", registro.Lugar.Consumido[i].Codigo);
                    var parametroCantidad = new SqlParameter("@pCantidad", registro.Lugar.Cantidades[i]);
                    var parametroSubTotal = new SqlParameter("@pSubtotal", registro.Lugar.Consumido[i].Precio*registro.Lugar.Cantidades[i]);
                    var parametroActivo = new SqlParameter("@pActivo", 1);
                    var vecparam = new SqlParameter[]{parametroIdRegistro, parametroIdConsumible, parametroCantidad, parametroSubTotal, parametroActivo};
                    var resultadoOperacion = objetoOperaciones.EjecutarSinResultado(consulta, vecparam);
                    if(resultadoOperacion == 0){
                        throw new Exception("No se pudo ingresar el comsumible del registro.");
                    }
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void DarDeBajaConsumiblesDelRegistro(int id){
            try{
                var consulta ="update RegistrosConsumibles set Activo = 0 where IdRegistro = @pId";
                var parametroId = new SqlParameter("@pId", id);
                var vecparam = new SqlParameter[]{parametroId};
                var resultadoOperacion = objetoOperaciones.EjecutarSinResultado(consulta, vecparam);
                if(resultadoOperacion == 0){
                    throw new Exception("No se pudo dar de baja los comsumibles del registro.");
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void DarDeAltaConsumiblesDelRegistro(int id){
            try{
                var consulta ="update RegistrosConsumibles set Activo = 1 where IdRegistro = @pId";
                var parametroId = new SqlParameter("@pId", id);
                var vecparam = new SqlParameter[]{parametroId};
                var resultadoOperacion = objetoOperaciones.EjecutarSinResultado(consulta, vecparam);
                if(resultadoOperacion == 0){
                    throw new Exception("No se pudo dar de alta los comsumibles del registro.");
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }
    }
}