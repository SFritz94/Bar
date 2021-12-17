using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public sealed class Operaciones
    {
        SqlConnection objetoConexion = new SqlConnection();
        public Operaciones(){
            objetoConexion.ConnectionString = "server=DESKTOP-KBNN5S3\\DYABD;database=BarSantiagoFritzDB;trusted_connection=true";
        }

        //se utiliza para funciones de agregado, como count, sum, avg, etc.
        public Object EjecutarScalar(string consulta, SqlParameter[] parametros){
            try{
                object resultado = null;

                using (SqlCommand command = new SqlCommand(consulta, objetoConexion)){
                    if(parametros != null && parametros.Length > 0){
                        command.Parameters.AddRange(parametros);
                    }
                    objetoConexion.Open();
                    resultado = command.ExecuteScalar();
                    objetoConexion.Close();
                }

                return resultado;
            }catch(SqlException ex){
                Console.WriteLine(ex.Message);
                return null;
            }finally{
                objetoConexion.Close();
            }
        }

        //se utiliza para mostrar datos de la base de datos, una lista o un solo registro.
        public DataTable MostrarDataTable(string consulta, SqlParameter[] parametros){
            try{
                var dataTable = new DataTable();

                using (SqlCommand command = new SqlCommand(consulta, objetoConexion)){
                    if(parametros != null && parametros.Length > 0){
                        command.Parameters.AddRange(parametros);
                    }

                    var dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = command;

                    objetoConexion.Open();
                    dataAdapter.Fill(dataTable);
                    objetoConexion.Close();
                }
                return dataTable;
            }catch(SqlException ex){
                Console.WriteLine(ex.Message);
                return null;
            }finally{
                objetoConexion.Close();
            }
        }

        //se utiliza para insert, update y delete      
        public int EjecutarSinResultado(string consulta, SqlParameter[] parametros){
            try{
                int cantFilasAfectadas = 0;
            
                using (SqlCommand command = new SqlCommand(consulta, objetoConexion)){
                    if(parametros != null && parametros.Length > 0){
                        command.Parameters.AddRange(parametros);
                    }

                    objetoConexion.Open();
                    cantFilasAfectadas = command.ExecuteNonQuery();
                    objetoConexion.Close();
                }

                return cantFilasAfectadas;
            }catch(SqlException ex){
                Console.WriteLine(ex.Message);
                return 0;
            }finally{
                objetoConexion.Close();
            }
        }

    }
}