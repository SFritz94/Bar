using System;
using System.Data;
using Entidades.Facturacion;
using Entidades.CarpetaCliente;
using Entidades.CarpetaPersonal;
using Entidades.Lugares;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Entidades.Consumibles;
using Entidades.Interfaces;

namespace DAL
{
    public sealed class DALRegistro: IDALRegistro
    {
        Operaciones objetoOperaciones;
        public DALRegistro(){
            objetoOperaciones = new Operaciones();
        }

        public Registro BuscarRegistroPorId(int id){
            var idRegistro = 0;
            var dalCliente = new DALCliente();
            var dalPersonal = new DALPersonal();
            var dalLugar = new DALLugar();
            var DALRegistroConsumible = new DALRegistroConsumible();
            Cliente cliente = null;
            Personal personal = null;
            Lugar lugar = null;
            List<Consumible> consumido = new List<Consumible>();
            DateTime horario = DateTime.Now;
            float total = 0;

            try{
                var consulta = "select * from Registros where IdRegistro = @pId";
                var parametroId= new SqlParameter("@pId", id);
                var parametros = new SqlParameter[]{parametroId}; 
                DataTable dataTable = objetoOperaciones.MostrarDataTable(consulta,parametros);
                foreach(DataRow dataRow in dataTable.Rows){
                        idRegistro = Int32.Parse(dataRow["IdRegistro"].ToString());
                        cliente = dalCliente.BuscarClientePorId(Int32.Parse(dataRow["IdCliente"].ToString()));
                        personal = dalPersonal.BuscarPersonalPorId(Int32.Parse(dataRow["IdPersonal"].ToString()));
                        lugar = dalLugar.BuscarLugarPorId(Int32.Parse(dataRow["IdLugar"].ToString()));
                        lugar.Consumido.AddRange(DALRegistroConsumible.MostrarConsumiblesPorRegistro(idRegistro));
                        lugar.Cantidades.AddRange(DALRegistroConsumible.MostrarCantidadesConsumidas(idRegistro));
                        horario = DateTime.Parse(dataRow["Horario"].ToString());
                        total = float.Parse(dataRow["Total"].ToString());
                }
                var registro = new Registro(idRegistro, cliente, personal, lugar, horario, total);
                if(registro.IdRegistro == 0){
                    throw new Exception("No existe el registro especificado.");
                }else{
                    return registro;
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Registro> MostrarTodosLosRegistros(){
            var dalCliente = new DALCliente();
            var dalPersonal = new DALPersonal();
            var dalLugar = new DALLugar();
            var DALRegistroConsumible = new DALRegistroConsumible();
            var listaRegistros = new List<Registro>();

            try{
                var consulta = "select * from Registros where Activo = 1";
                DataTable dataTable = objetoOperaciones.MostrarDataTable(consulta,null);
                foreach(DataRow dataRow in dataTable.Rows){
                    var registro = new Registro(
                        Int32.Parse(dataRow["IdRegistro"].ToString()),
                        dalCliente.BuscarClientePorId(Int32.Parse(dataRow["IdCliente"].ToString())),
                        dalPersonal.BuscarPersonalPorId(Int32.Parse(dataRow["IdPersonal"].ToString())),
                        dalLugar.BuscarLugarPorId(Int32.Parse(dataRow["IdLugar"].ToString())),
                        DateTime.Parse(dataRow["Horario"].ToString()),
                        float.Parse(dataRow["Total"].ToString())
                    );
                    registro.Lugar.Consumido.AddRange(DALRegistroConsumible.MostrarConsumiblesPorRegistro(Int32.Parse(dataRow["IdRegistro"].ToString())));
                    registro.Lugar.Cantidades.AddRange(DALRegistroConsumible.MostrarCantidadesConsumidas(Int32.Parse(dataRow["IdRegistro"].ToString())));
                    listaRegistros.Add(registro);
                }
                if(listaRegistros.Count == 0){
                    throw new Exception("No existen registros.");
                }else{
                    return listaRegistros;
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void InsertarRegistro(Registro registro){
            var dalRegistroConsumible = new DALRegistroConsumible();
            try{
                var consulta ="insert into Registros(IdCliente, IdPersonal, IdLugar, Horario, Total, Activo) values(@pIdCliente, @pIdPersonal, @pIdLugar, @pHorario, @pTotal, @pActivo)";
                var parametroIdCliente = new SqlParameter("@pIdCliente", registro.Cliente.Id);
                var parametroIdPersonal = new SqlParameter("@pIdPersonal", registro.Personal.Id);
                var parametroIdLugar = new SqlParameter("@pIdLugar", registro.Lugar.IdLugar);
                var parametroHorario = new SqlParameter("@pHorario", DateTime.Now);
                var parametroTotal = new SqlParameter("@pTotal", registro.TotalConsumido);
                var parametroActivo = new SqlParameter("@pActivo", 1);
                var vecparam = new SqlParameter[]{parametroIdCliente, parametroIdPersonal, parametroIdLugar, parametroHorario, parametroTotal, parametroActivo};
                var resultadoOperacion = objetoOperaciones.EjecutarSinResultado(consulta, vecparam);
                if(resultadoOperacion == 0){
                    throw new Exception("No se pudo ingresar el registro.");
                }else{
                    dalRegistroConsumible.InsertarRegistrosConsumibles(registro, MostrarIdUltimoRegistro());
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void DarDeBajaRegistro(int id){
            var dalRegistroConsumible = new DALRegistroConsumible();
            try{
                var consulta ="update Registros set Activo = 0 where IdRegistro = @pId";
                var parametroId = new SqlParameter("@pId", id);
                var vecparam = new SqlParameter[]{parametroId};
                var resultadoOperacion = objetoOperaciones.EjecutarSinResultado(consulta, vecparam);
                if(resultadoOperacion == 0){
                    throw new Exception("No se pudo dar de baja el registro especificado.");
                }else{
                    dalRegistroConsumible.DarDeBajaConsumiblesDelRegistro(id);
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void DarDeAltaRegistro(int id){
            var dalRegistroConsumible = new DALRegistroConsumible();
            try{
                var consulta ="update Registros set Activo = 1 where IdRegistro = @pId";
                var parametroId = new SqlParameter("@pId", id);
                var vecparam = new SqlParameter[]{parametroId};
                var resultadoOperacion = objetoOperaciones.EjecutarSinResultado(consulta, vecparam);
                if(resultadoOperacion == 0){
                    throw new Exception("No se pudo dar de alta el registro especificado.");
                }else{
                    dalRegistroConsumible.DarDeAltaConsumiblesDelRegistro(id);
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public double CalcularTotalRecaudado(){
            try{
                var consulta="select sum(Total) from Registros where Activo = 1";
                var resultado = objetoOperaciones.EjecutarScalar(consulta, null);
                if(resultado.ToString() == ""){
                    throw new Exception("No se recaudo nada.");
                }else{
                    return double.Parse(resultado.ToString());
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        
        public int MostrarIdUltimoRegistro(){
            try{
                var consulta="select IdRegistro from Registros order by IdRegistro desc";
                var resultado = objetoOperaciones.EjecutarScalar(consulta, null);
                Console.WriteLine(resultado);
                if(Int32.Parse(resultado.ToString()) == 0){
                    throw new Exception("No existen registros.");
                }else{
                    return Int32.Parse(resultado.ToString());
                }
            }catch(Exception ex){
                Console.WriteLine(ex);
                return 0;
            }
        }
    }
}