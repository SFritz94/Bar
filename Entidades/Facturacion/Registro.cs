using System;
using Entidades.CarpetaCliente;
using Entidades.CarpetaPersonal;
using Entidades.Lugares;

namespace Entidades.Facturacion
{
    public sealed class Registro
    {
        #region Propiedades
        private int idRegistro;
        public int IdRegistro {
            get{
                return idRegistro;
            }
        }
        private DateTime horarioRegistro;
        public DateTime HorarioRegistro {
            get{
                return horarioRegistro;
            }
        }
        private Cliente cliente;

        public Cliente Cliente{
            get{
                return cliente;
            }
        }
        private Personal personal;
        public Personal Personal {
            get{
                return personal;
            }
        }
        private Lugar lugar;
        public Lugar Lugar {
            get{
                return lugar;
            }
        }
        private double totalConsumido;
        public double TotalConsumido {
            get{
                return totalConsumido;
            }
        }
        #endregion

        #region Metodos
        public Registro(Cliente cliente,Personal personal, Lugar lugar, DateTime horario, /*List<Consumible> consumido,*/ double totalConsumido){
            this.cliente = cliente;
            this.personal = personal;
            this.lugar = lugar;
            this.horarioRegistro = horario;
            this.totalConsumido = totalConsumido;
        }
        public Registro(int id, Cliente cliente,Personal personal, Lugar lugar, DateTime horario, /*List<Consumible> consumido,*/ double totalConsumido){
            this.idRegistro = id;
            this.cliente = cliente;
            this.personal = personal;
            this.lugar = lugar;
            this.horarioRegistro = horario;
            this.totalConsumido = totalConsumido;
        }
        #endregion
    }
}