using System.Collections.Generic;
using Entidades.ClasesGenerales;

namespace Entidades.CarpetaCliente
{
    public sealed class Cliente: Persona
    {
        #region Propiedades
        private List<Mensaje> mensajes;

        public List<Mensaje> Mensajes{
            get{
                return mensajes;
            }
            set{
                mensajes = value;
            }
        }
        #endregion

        #region Metodos
        public Cliente(string nombre, string apellido, int dni, string direccion, string email): base(nombre, apellido,dni,direccion,email){
            this.mensajes=new List<Mensaje>();
        }
        public Cliente(int id, string nombre, string apellido, int dni, string direccion, string email): base(id, nombre, apellido,dni,direccion,email){
            this.mensajes=new List<Mensaje>();
        }
        #endregion
    }
}