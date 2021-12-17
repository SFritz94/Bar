using System;
using System.Collections.Generic;
using System.Data;
using Entidades.Consumibles;
using Entidades.Interfaces;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public sealed class DALConsumible: IDALConsumible
    {
        Operaciones objetoOperaciones;
        public DALConsumible(){
            objetoOperaciones = new Operaciones();
        }

        public Consumible BuscarConsumiblePorId(int id){
            var idConsumible = 0;
            var descripcion = "";
            var precio = 0;
            var stock = 0;
            var tipoDescripcion = "";

            try{
                var consulta = "select * from Consumibles C inner join TipoConsumible TC on C.IdTipoConsumible=TC.IdTipoConsumible where IdConsumible = @pId";
                var parametroId= new SqlParameter("@pId", id);
                var parametros = new SqlParameter[]{parametroId}; 
                DataTable dataTable = objetoOperaciones.MostrarDataTable(consulta,parametros);
                foreach(DataRow dataRow in dataTable.Rows){
                        idConsumible = Int32.Parse(dataRow["IdConsumible"].ToString());
                        descripcion = dataRow["Descripcion"].ToString();
                        precio = Int32.Parse(dataRow["Precio"].ToString());
                        stock = Int32.Parse(dataRow["Stock"].ToString());
                        tipoDescripcion = dataRow["TipoDescripcion"].ToString();
                }
                var producto = new Producto(idConsumible, descripcion, precio, stock, tipoDescripcion);
                if(producto.Codigo == 0){
                    throw new Exception("No existe el producto especificado.");
                }else{
                    return producto;
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Consumible> MostrarTodosLosConsumibles(){
            var listaConsumibles = new List<Consumible>();
            try{
                var consulta = "select * from Consumibles C inner join TipoConsumible TC on C.IdTipoConsumible=TC.IdTipoConsumible";
                DataTable dataTable = objetoOperaciones.MostrarDataTable(consulta,null);
                foreach(DataRow dataRow in dataTable.Rows){
                    var producto = new Producto(
                        Int32.Parse(dataRow["IdConsumible"].ToString()),
                        dataRow["Descripcion"].ToString(),
                        Int32.Parse(dataRow["Precio"].ToString()),
                        Int32.Parse(dataRow["Stock"].ToString()),
                        dataRow["TipoDescripcion"].ToString());
                    listaConsumibles.Add(producto);
                }
                if(listaConsumibles.Count == 0){
                    throw new Exception("No existen productos.");
                }else{
                    return listaConsumibles;
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void InsertarConsumible(Consumible consumible){
            try{
                var consulta ="insert into Consumibles(IdTipoConsumible, Descripcion, Precio, Stock) values(@pIdTipoConsumible, @pDescripcion, @pPrecio, @pStock)";
                var parametroIdTipoConsumible = new SqlParameter();
                if(consumible.TipoConsumible.Equals("Bebida")){
                    parametroIdTipoConsumible = new SqlParameter("@pIdTipoConsumible", 1);
                }
                if(consumible.TipoConsumible.Equals("Comida")){
                    parametroIdTipoConsumible = new SqlParameter("@pIdTipoConsumible", 2);
                }
                if(consumible.TipoConsumible.Equals("Postre")){
                    parametroIdTipoConsumible = new SqlParameter("@pIdTipoConsumible", 3);
                }
                var parametroDescripcion = new SqlParameter("@pDescripcion", consumible.Nombre);
                var parametroPrecio = new SqlParameter("@pPrecio", consumible.Precio);
                var parametroStock = new SqlParameter("@pStock", consumible.Stock);
                var vecparam = new SqlParameter[]{parametroIdTipoConsumible, parametroDescripcion, parametroPrecio, parametroStock}; 
                var resultadoOperacion = objetoOperaciones.EjecutarSinResultado(consulta, vecparam);
                if(resultadoOperacion == 0){
                    throw new Exception("No se pudo ingresar el producto especificado.");
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void ActualizarConsumible(Consumible consumible){
            try{
                var consulta ="update Consumibles set IdTipoConsumible = @pIdTipoConsumible, Descripcion = @pDescripcion, Precio = @pPrecio, Stock = @pStock where IdConsumible=@pId";
                var parametroIdTipoConsumible = new SqlParameter();
                if(consumible.TipoConsumible.Equals("Bebida")){
                    parametroIdTipoConsumible = new SqlParameter("@pIdTipoConsumible", 1);
                }
                if(consumible.TipoConsumible.Equals("Comida")){
                    parametroIdTipoConsumible = new SqlParameter("@pIdTipoConsumible", 2);
                }
                if(consumible.TipoConsumible.Equals("Postre")){
                    parametroIdTipoConsumible = new SqlParameter("@pIdTipoConsumible", 3);
                }
                var parametroDescripcion = new SqlParameter("@pDescripcion", consumible.Nombre);
                var parametroPrecio = new SqlParameter("@pPrecio", consumible.Precio);
                var parametroStock = new SqlParameter("@pStock", consumible.Stock);
                var parametroId = new SqlParameter("@pId", consumible.Codigo);
                var vecparam = new SqlParameter[]{parametroId, parametroIdTipoConsumible, parametroDescripcion, parametroPrecio, parametroStock}; 
                var resultadoOperacion = objetoOperaciones.EjecutarSinResultado(consulta, vecparam);
                if(resultadoOperacion == 0){
                    throw new Exception("No se pudo actualizar el producto especificado.");
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void EliminarConsumible(int id){
            try{
                var consulta ="delete from Consumibles where IdConsumible=@pId";
                var parametroId = new SqlParameter("@pId", id);
                var vecparam = new SqlParameter[]{parametroId}; 
                var resultadoOperacion = objetoOperaciones.EjecutarSinResultado(consulta, vecparam);
                if(resultadoOperacion == 0){
                    throw new Exception("No se pudo eliminar el producto especificado.");
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public int ConsultarStockConsumible(Consumible consumible){
            var stock = 0;

            try{
                var consulta = "select Stock from Consumibles where IdConsumible = @pId";
                var parametroId= new SqlParameter("@pId", consumible.Codigo);
                var parametros = new SqlParameter[]{parametroId}; 
                DataTable dataTable = objetoOperaciones.MostrarDataTable(consulta,parametros);
                foreach(DataRow dataRow in dataTable.Rows){
                        stock = Int32.Parse(dataRow["Stock"].ToString());
                }
                if(stock == 0){
                    throw new Exception("No existe stock para el producto especificado.");
                }else{
                    return stock;
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public void RestarStock(Consumible consumible, int cantidad){
            try{
                var consulta ="update Consumibles set Stock = Stock - @pStock where IdConsumible = @pId";
                var parametroId = new SqlParameter("@pId", consumible.Codigo);
                var parametroStock = new SqlParameter("@pStock", cantidad);
                var vecparam = new SqlParameter[]{parametroId, parametroStock}; 
                var resultadoOperacion = objetoOperaciones.EjecutarSinResultado(consulta, vecparam);
                if(resultadoOperacion == 0){
                    throw new Exception("No se pudo restar el stock del producto especificado.");
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public void SumarStock(Consumible consumible, int cantidad){
            try{
                var consulta ="update Consumibles set Stock = Stock + @pStock where IdConsumible = @pId";
                var parametroId = new SqlParameter("@pId", consumible.Codigo);
                var parametroStock = new SqlParameter("@pStock", cantidad);
                var vecparam = new SqlParameter[]{parametroId, parametroStock}; 
                var resultadoOperacion = objetoOperaciones.EjecutarSinResultado(consulta, vecparam);
                if(resultadoOperacion == 0){
                    throw new Exception("No se pudo sumar el stock del producto especificado.");
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }
    }
}