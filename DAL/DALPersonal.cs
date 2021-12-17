using System;
using System.Collections.Generic;
using System.Data;
using Entidades.CarpetaPersonal;
using Entidades.Interfaces;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public sealed class DALPersonal: IDALPersonal
    {
        Operaciones objetoOperaciones;
        public DALPersonal(){
            objetoOperaciones = new Operaciones();
        }

        public Personal BuscarPersonalPorId(int id){
            var idPersonal = 0;
            var nombre = "";
            var apellido = "";
            var dni = 0;
            var direccion = "";
            var email = "";
            var cargo ="";

            try{
                var consulta = "select * from Personal P inner join Cargos C on P.IdCargo = C.IdCargo where IdPersonal = @pId";
                var parametroId= new SqlParameter("@pId", id);
                var parametros = new SqlParameter[]{parametroId}; 
                DataTable dataTable = objetoOperaciones.MostrarDataTable(consulta,parametros);
                foreach(DataRow dataRow in dataTable.Rows){
                        idPersonal = Int32.Parse(dataRow["IdPersonal"].ToString());
                        nombre = dataRow["Nombre"].ToString();
                        apellido = dataRow["Apellido"].ToString();
                        dni = Int32.Parse(dataRow["DNI"].ToString());
                        direccion = dataRow["Direccion"].ToString();
                        email = dataRow["Email"].ToString();
                        cargo = dataRow["Descripcion"].ToString();
                }
                if(cargo.Equals("Mozo")){
                    return new Mozo(idPersonal, nombre, apellido, dni, direccion, email, cargo);
                }
                if(cargo.Equals("Barman")){
                    return new Mozo(idPersonal, nombre, apellido, dni, direccion, email, cargo);
                }
                if(cargo.Equals("Encargado")){
                    return new Encargado(idPersonal, nombre, apellido, dni, direccion, email, cargo);
                }
                throw new Exception("No existe el personal especificado.");
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Personal> MostrarTodoElPersonal(){
            var listaPersonal = new List<Personal>();
            try{
                var consulta = "select * from Personal P inner join Cargos C on P.IdCargo = C.IdCargo";
                DataTable dataTable = objetoOperaciones.MostrarDataTable(consulta,null);
                foreach(DataRow dataRow in dataTable.Rows){
                    if(dataRow["Descripcion"].ToString().Equals("Mozo")){
                        var personal = new Mozo(
                        Int32.Parse(dataRow["IdPersonal"].ToString()),
                        dataRow["Nombre"].ToString(),
                        dataRow["Apellido"].ToString(),
                        Int32.Parse(dataRow["DNI"].ToString()),
                        dataRow["Direccion"].ToString(),
                        dataRow["Email"].ToString(),
                        dataRow["Descripcion"].ToString());
                        listaPersonal.Add(personal);
                    }
                    if(dataRow["Descripcion"].ToString().Equals("Barman")){
                        var personal = new Barman(
                        Int32.Parse(dataRow["IdPersonal"].ToString()),
                        dataRow["Nombre"].ToString(),
                        dataRow["Apellido"].ToString(),
                        Int32.Parse(dataRow["DNI"].ToString()),
                        dataRow["Direccion"].ToString(),
                        dataRow["Email"].ToString(),
                        dataRow["Descripcion"].ToString());
                        listaPersonal.Add(personal);
                    }
                    if(dataRow["Descripcion"].ToString().Equals("Encargado")){
                        var personal = new Encargado(
                        Int32.Parse(dataRow["IdPersonal"].ToString()),
                        dataRow["Nombre"].ToString(),
                        dataRow["Apellido"].ToString(),
                        Int32.Parse(dataRow["DNI"].ToString()),
                        dataRow["Direccion"].ToString(),
                        dataRow["Email"].ToString(),
                        dataRow["Descripcion"].ToString());
                        listaPersonal.Add(personal);
                    }
                }
                if(listaPersonal.Count == 0){
                    throw new Exception("No existe personal.");
                }
                return listaPersonal;
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void InsertarPersonal(Personal personal){
            try{
                var consulta ="insert into Personal(IdCargo, Nombre, Apellido, DNI, Direccion, Email) values (@pIdCargo, @pNombre, @pApellido, @pDni, @pDireccion, @pEmail)";
                var parametroIdCargo = new SqlParameter();
                if(personal.GetType()==typeof(Encargado)){
                    parametroIdCargo = new SqlParameter("@pIdCargo", 1);
                }
                if(personal.GetType()==typeof(Barman)){
                    parametroIdCargo = new SqlParameter("@pIdCargo", 2);
                }
                if(personal.GetType()==typeof(Mozo)){
                    parametroIdCargo = new SqlParameter("@pIdCargo", 3);
                }
                var parametroNombre = new SqlParameter("@pNombre", personal.Nombre);
                var parametroApellido = new SqlParameter("@pApellido", personal.Apellido);
                var parametroDNI = new SqlParameter("@pDni", personal.Dni);
                var parametroDireccion = new SqlParameter("@pDireccion", personal.Direccion);
                var parametroEmail = new SqlParameter("@pEmail", personal.Email);
                var vecparam = new SqlParameter[]{parametroIdCargo, parametroNombre, parametroApellido, parametroDNI, parametroDireccion, parametroEmail}; 
                var resultadoOperacion = objetoOperaciones.EjecutarSinResultado(consulta, vecparam);
                if(resultadoOperacion == 0){
                    throw new Exception("No se pudo ingresar el personal especificado.");
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void ActualizarPersonal(Personal personal){
            try{
                var consulta ="update Personal set IdCargo = @pIdCargo, Nombre = @pNombre, Apellido = @pApellido, DNI = @pDni, Direccion = @pDireccion, Email = @pEmail where IdPersonal=@pId";
                var parametroIdCargo = new SqlParameter();
                if(personal.Cargo.Equals("Encargado")){
                    parametroIdCargo = new SqlParameter("@pIdCargo", 1);
                }
                if(personal.Cargo.Equals("Barman")){
                    parametroIdCargo = new SqlParameter("@pIdCargo", 2);
                }
                if(personal.Cargo.Equals("Mozo")){
                    parametroIdCargo = new SqlParameter("@pIdCargo", 3);
                }
                var parametroNombre = new SqlParameter("@pNombre", personal.Nombre);
                var parametroApellido = new SqlParameter("@pApellido", personal.Apellido);
                var parametroDNI = new SqlParameter("@pDni", personal.Dni);
                var parametroDireccion = new SqlParameter("@pDireccion", personal.Direccion);
                var parametroEmail = new SqlParameter("@pEmail", personal.Email);
                var parametroId = new SqlParameter("@pId", personal.Id);
                var vecparam = new SqlParameter[]{parametroId, parametroIdCargo, parametroNombre, parametroApellido, parametroDNI, parametroDireccion, parametroEmail};
                var resultadoOperacion = objetoOperaciones.EjecutarSinResultado(consulta, vecparam);
                if(resultadoOperacion == 0){
                    throw new Exception("No se pudo actualizar el personal especificado.");
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void EliminarPersonal(int id){
            try{
                var consulta ="delete from Personal where IdPersonal=@pId";
                var parametroId = new SqlParameter("@pId", id);
                var vecparam = new SqlParameter[]{parametroId};
                var resultadoOperacion = objetoOperaciones.EjecutarSinResultado(consulta, vecparam);
                if(resultadoOperacion == 0){
                    throw new Exception("No se pudo eliminar el personal especificado.");
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }
    }
}