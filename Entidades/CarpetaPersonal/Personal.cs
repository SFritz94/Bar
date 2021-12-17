using System;
using Entidades.ClasesGenerales;

namespace Entidades.CarpetaPersonal
{
    public abstract class Personal: Persona
    {
        #region Propiedades
        private DateTime horarioEntrada;
        public DateTime HorarioEntrada{
            get{
                return horarioEntrada;
            }
            set{
                horarioEntrada = value;
            }
        }
        private DateTime horarioSalida;
        public DateTime HorarioSalida{
            get{
                return horarioSalida;
            }
            set{
                horarioSalida = value;
            }
        }

        private string cargo;

        public string Cargo{
            get{
                return cargo;
            }
            set{
                cargo=value;
            }
        }
        #endregion

        #region Metodos
        public Personal(string nombre, string apellido, int dni, string direccion, string email, string cargo): base(nombre, apellido,dni,direccion,email){
            this.cargo=cargo;
        }
        public Personal(int id, string nombre, string apellido, int dni, string direccion, string email, string cargo): base(id, nombre, apellido,dni,direccion,email){
            this.cargo=cargo;
        }
        #endregion
    }
}