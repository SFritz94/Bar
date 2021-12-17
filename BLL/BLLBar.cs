using System;
using System.Collections.Generic;
using Entidades;
using Entidades.CarpetaCliente;
using Entidades.CarpetaPersonal;
using Entidades.ClasesGenerales;
using Entidades.Consumibles;
using Entidades.Facturacion;
using Entidades.Lugares;

namespace BLL
{
    public static class BLLBar
    {
        public delegate void tipoDelegadoMensaje(Cliente cliente);
        public static event tipoDelegadoMensaje enviarMensaje;

        public static void AgregarPersonal(Personal personal){
            if(Bar.Personal is null){
                Bar.Personal = new List<Personal>();
            }
            Bar.Personal.Add(personal);
        }

        public static void AgregarListaPersonal(List<Personal> listaPersonal){
            if(Bar.Personal is null){
                Bar.Personal = new List<Personal>();
            }
            Bar.Personal.AddRange(listaPersonal);
        }

        public static void AgregarLugar(Lugar lugar){
            if(Bar.Lugares is null){
                Bar.Lugares = new List<Lugar>();
            }
            Bar.Lugares.Add(lugar);
        }

        public static void AgregarLugares(List<Lugar> lugares){
            if(Bar.Lugares is null){
                Bar.Lugares = new List<Lugar>();
            }
            Bar.Lugares.AddRange(lugares);
        }
        
        public static void AgregarConsumible(Consumible consumible){
            if(Bar.Consumibles is null){
                Bar.Consumibles = new List<Consumible>();
            }
            Bar.Consumibles.Add(consumible);
        }

        public static void AgregarConsumibles(List<Consumible> consumibles){
            if(Bar.Consumibles is null){
                Bar.Consumibles = new List<Consumible>();
            }
            Bar.Consumibles.AddRange(consumibles);
        }

        public static void AgregarCliente(Cliente cliente){
            if(Bar.Clientes is null){
                Bar.Clientes = new List<Cliente>();
            }
            Bar.Clientes.Add(cliente);
        }

        public static void AgregarClientes(List<Cliente> clientes){
            if(Bar.Clientes is null){
                Bar.Clientes = new List<Cliente>();
            }
            Bar.Clientes.AddRange(clientes);
        }

        public static void EnviarMensajeAbrimos(){
            var bllCliente = new BLLCliente();
            var mensaje = new Mensaje("Abrimos nuestro Bar. Te esperamos!!");
            foreach(var cliente in Bar.Clientes){
                bllCliente.AgregarMensaje(mensaje, cliente);
                enviarMensaje(cliente);
            }
        }

        public static void MostrarMenu(){
            var bllConsumible = new BLLConsumible();
            var consumibles = bllConsumible.ListarTodosLosConsumibles();
            Console.WriteLine("Nuestros productos: ");
            foreach(var producto in consumibles){
                Console.WriteLine($"{producto.Codigo}- {producto.Nombre} ${producto.Precio}");
            }
        }

        public static double TotalRecaudado(){
            var bllRegistro = new BLLRegistro();
            return bllRegistro.CalcularTotalRecaudado();
        }

        public static void MostrarInfoRegistroEspecifico(int numeroRegistro){
            var bllRegistro = new BLLRegistro();
            var registro = bllRegistro.BuscarRegistroPorId(numeroRegistro);
            BLLBar.MostrarInfoRegistro(registro);
        }

        public static void MostrarInfoRegistros(){
            var bllRegistro = new BLLRegistro();
            var registros = bllRegistro.ListarTodosLosRegistros();
            foreach(var registro in registros){
                BLLBar.MostrarInfoRegistro(registro);
            }
        }

        private static void MostrarInfoRegistro(Registro registro){
                Console.WriteLine($"Numero registro: {registro.IdRegistro}");
                Console.WriteLine($"Horario: {registro.HorarioRegistro}");
                BLLBar.MostrarInfoPersonal(registro.Personal);
                BLLBar.MostrarInfoCliente(registro.Cliente);
                BLLBar.MostrarInfoLugar(registro.Lugar);
                Console.WriteLine($"Total a abonar: ${registro.TotalConsumido}");
                Console.WriteLine("");
        }

        private static void MostrarInfoPersonal(Personal personal){
            if(personal.GetType()==typeof(Mozo)){
                Console.WriteLine($"Mozo - Id: {personal.Id} Nombre: {personal.Nombre} {personal.Apellido}");
            }else{
                Console.WriteLine($"Barman - Id: {personal.Id} Nombre: {personal.Nombre} {personal.Apellido}");
            }
        }

        private static void MostrarInfoCliente(Cliente cliente){
            Console.WriteLine($"Cliente - Id: {cliente.Id} Nombre: {cliente.Nombre} {cliente.Apellido}");
        }

        private static void MostrarInfoLugar(Lugar lugar){
            if(lugar.GetType()==typeof(Mesa)){
                Console.WriteLine($"Lugar - {((Mesa)lugar).DescripcionLugar} número {((Mesa)lugar).IdLugar}");
                for(int i=0; i<lugar.Consumido.Count; i++){
                    Console.WriteLine($"-- {((Mesa)lugar).Consumido[i].Nombre} ${((Mesa)lugar).Consumido[i].Precio} x{((Mesa)lugar).Cantidades[i]} - ${((Mesa)lugar).Consumido[i].Precio*((Mesa)lugar).Cantidades[i]}");
                }
            }else{
                Console.WriteLine($"Lugar - {((Barra)lugar).DescripcionLugar}");
                for(int i=0; i<lugar.Consumido.Count; i++){
                    Console.WriteLine($"-- {((Barra)lugar).Consumido[i].Nombre} ${((Barra)lugar).Consumido[i].Precio} x{((Barra)lugar).Cantidades[i]} - ${((Barra)lugar).Consumido[i].Precio*((Barra)lugar).Cantidades[i]}");
                }
            }
        }
    }
}