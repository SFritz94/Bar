using System;
using System.Collections.Generic;
using System.Data;
using Entidades.Interfaces;
using Entidades.Lugares;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public sealed class DALLugar: IDALLugar
    {
        Operaciones objetoOperaciones;
        public DALLugar(){
            objetoOperaciones = new Operaciones();
        }

        public Lugar BuscarLugarPorId(int id){
            var idLugar = 0;
            var descripcion = "";

            try{
                var consulta = "select * from Lugares L inner join TipoLugar TL on L.IdTipoLugar = TL.IdTipoLugar where IdLugar = @pId";
                var parametroId= new SqlParameter("@pId", id);
                var parametros = new SqlParameter[]{parametroId}; 
                DataTable dataTable = objetoOperaciones.MostrarDataTable(consulta,parametros);
                foreach(DataRow dataRow in dataTable.Rows){
                        idLugar = Int32.Parse(dataRow["IdLugar"].ToString());
                        descripcion = dataRow["Descripcion"].ToString();
                }
                if(descripcion.Equals("Barra")){
                    return new Barra(idLugar, descripcion);
                }
                if(descripcion.Equals("Mesa")){
                    return new Mesa(idLugar, descripcion);
                }
                throw new Exception("No existe el lugar especificado.");
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Lugar> MostrarTodosLosLugares(){
            var listaLugares = new List<Lugar>();

            try{
                var consulta = "select * from Lugares L inner join TipoLugar TL on L.IdTipoLugar = TL.IdTipoLugar";
                DataTable dataTable = objetoOperaciones.MostrarDataTable(consulta,null);
                foreach(DataRow dataRow in dataTable.Rows){
                    if(dataRow["Descripcion"].ToString().Equals("Barra")){
                        var barra = new Barra(
                            Int32.Parse(dataRow["IdLugar"].ToString()),
                            dataRow["Descripcion"].ToString()
                        );
                        listaLugares.Add(barra);
                    }
                    if(dataRow["Descripcion"].ToString().Equals("Mesa")){
                        var mesa = new Mesa(
                            Int32.Parse(dataRow["IdLugar"].ToString()),
                            dataRow["Descripcion"].ToString()
                        );
                        listaLugares.Add(mesa);
                    }
                }
                if(listaLugares.Count == 0){
                    throw new Exception("No existen lugares.");
                }
                return listaLugares;
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void InsertarLugar(Lugar lugar){
            try{
                var consulta ="insert into Lugares(IdTipoLugar) values(@pIdTipoLugar)";
                var parametroIdTipoLugar = new SqlParameter();
                if(lugar.GetType()==typeof(Barra)){
                    parametroIdTipoLugar = new SqlParameter("@pIdTipoLugar", 1);
                }
                if(lugar.GetType()==typeof(Mesa)){
                    parametroIdTipoLugar = new SqlParameter("@pIdTipoLugar", 2);
                }
                var vecparam = new SqlParameter[]{parametroIdTipoLugar}; 
                var resultadoOperacion = objetoOperaciones.EjecutarSinResultado(consulta, vecparam);
                if(resultadoOperacion == 0){
                    throw new Exception("No se pudo ingresar el lugar especificado.");
                }
                }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void ActualizarLugar(Lugar lugar){
            try{
                var consulta ="update Lugares set IdTipoLugar = @pIdTipoLugar where IdLugar=@pId";
                var parametroIdTipoLugar = new SqlParameter();
                if(lugar.GetType()==typeof(Barra)){
                    parametroIdTipoLugar = new SqlParameter("@pIdTipoLugar", 1);
                }
                if(lugar.GetType()==typeof(Mesa)){
                    parametroIdTipoLugar = new SqlParameter("@pIdTipoLugar", 2);
                }
                var parametroId = new SqlParameter("@pId", lugar.IdLugar);
                var vecparam = new SqlParameter[]{parametroIdTipoLugar, parametroId}; 
                var resultadoOperacion = objetoOperaciones.EjecutarSinResultado(consulta, vecparam);
                if(resultadoOperacion == 0){
                    throw new Exception("No se pudo actualizar el lugar especificado.");
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void EliminarLugar(int id){
            try{
                var consulta ="delete from Lugares where IdLugar=@pId";
                var parametroId = new SqlParameter("@pId", id);
                var vecparam = new SqlParameter[]{parametroId}; 
                var resultadoOperacion = objetoOperaciones.EjecutarSinResultado(consulta, vecparam);
                if(resultadoOperacion == 0){
                    throw new Exception("No se pudo elimiar el lugar especificado.");
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }
    }
}