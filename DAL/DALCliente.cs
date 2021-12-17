using System;
using System.Collections.Generic;
using System.Data;
using Entidades.CarpetaCliente;
using Entidades.Interfaces;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public sealed class DALCliente: IDALCliente
    {
        Operaciones objetoOperaciones;
        public DALCliente(){
            objetoOperaciones = new Operaciones();
        }

        public Cliente BuscarClientePorId(int id){
            var idCliente = 0;
            var nombre = "";
            var apellido = "";
            var dni = 0;
            var direccion = "";
            var email = "";

            try{
                var consulta = "select * from Clientes where IdCliente = @pId";
                var parametroId= new SqlParameter("@pId", id);
                var parametros = new SqlParameter[]{parametroId}; 
                DataTable dataTable = objetoOperaciones.MostrarDataTable(consulta,parametros);
                foreach(DataRow dataRow in dataTable.Rows){
                        idCliente = Int32.Parse(dataRow["IdCliente"].ToString());
                        nombre = dataRow["Nombre"].ToString();
                        apellido = dataRow["Apellido"].ToString();
                        dni = Int32.Parse(dataRow["DNI"].ToString());
                        direccion = dataRow["Direccion"].ToString();
                        email = dataRow["Email"].ToString();
                }
                var cliente = new Cliente(idCliente, nombre, apellido, dni, direccion, email);
                if(cliente.Id == 0){
                    throw new Exception("No existe el cliente especificado.");
                }else{
                    return cliente;
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Cliente> MostrarTodosLosClientes(){
            var listaClientes = new List<Cliente>();
            try{
                var consulta = "select * from Clientes";
                DataTable dataTable = objetoOperaciones.MostrarDataTable(consulta,null);
                foreach(DataRow dataRow in dataTable.Rows){
                    var cliente = new Cliente(
                        Int32.Parse(dataRow["IdCliente"].ToString()),
                        dataRow["Nombre"].ToString(),
                        dataRow["Apellido"].ToString(),
                        Int32.Parse(dataRow["DNI"].ToString()),
                        dataRow["Direccion"].ToString(),
                        dataRow["Email"].ToString());
                    listaClientes.Add(cliente);
                }
                if(listaClientes.Count == 0){
                    throw new Exception("No existen clientes.");
                }else{
                    return listaClientes;
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void InsertarCliente(Cliente cliente){
            try{
                var consulta ="insert into Clientes(Nombre, Apellido, DNI, Direccion, Email) values(@pNombre, @pApellido, @pDNI, @pDireccion, @pEmail)";
                var parametroNombre = new SqlParameter("@pNombre", cliente.Nombre);
                var parametroApellido = new SqlParameter("@pApellido", cliente.Apellido);
                var parametroDNI = new SqlParameter("@pDNI", cliente.Dni);
                var parametroDireccion = new SqlParameter("@pDireccion", cliente.Direccion);
                var parametroEmail = new SqlParameter("@pEmail", cliente.Email);
                var vecparam = new SqlParameter[]{parametroNombre, parametroApellido, parametroDNI, parametroDireccion, parametroEmail}; 
                var resultadoOperacion = objetoOperaciones.EjecutarSinResultado(consulta, vecparam);
                if(resultadoOperacion == 0){
                    throw new Exception("No se pudo ingresar el cliente especificado.");
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void ActualizarCliente(Cliente cliente){
            try{
                var consulta ="update Clientes set Nombre = @pNombre, Apellido = @pApellido, DNI = @pDNI, Direccion = @pDireccion, Email = @pEmail where IdCliente = @pId";
                var parametroId = new SqlParameter("@pId", cliente.Id);
                var parametroNombre = new SqlParameter("@pNombre", cliente.Nombre);
                var parametroApellido = new SqlParameter("@pApellido", cliente.Apellido);
                var parametroDNI = new SqlParameter("@pDNI", cliente.Dni);
                var parametroDireccion = new SqlParameter("@pDireccion", cliente.Direccion);
                var parametroEmail = new SqlParameter("@pEmail", cliente.Email);
                var vecparam = new SqlParameter[]{parametroId, parametroNombre, parametroApellido, parametroDNI, parametroDireccion, parametroEmail}; 
                var resultadoOperacion = objetoOperaciones.EjecutarSinResultado(consulta, vecparam);
                if(resultadoOperacion == 0){
                    throw new Exception("No se pudo actualizar el cliente especificado.");
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void EliminarCliente(int id){
            try{
                var consulta ="delete from Clientes where IdCliente = @pId";
                var parametroId = new SqlParameter("@pId", id);
                var vecparam = new SqlParameter[]{parametroId}; 
                var resultadoOperacion = objetoOperaciones.EjecutarSinResultado(consulta, vecparam);
                if(resultadoOperacion == 0){
                    throw new Exception("No se pudo elimiar el cliente especificado.");
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }
    }
}