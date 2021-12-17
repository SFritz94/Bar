using System.Collections.Generic;
using Entidades.Consumibles;
using Entidades.CarpetaPersonal;
using Entidades.CarpetaCliente;

namespace Entidades.Lugares
{
    public abstract class Lugar
    {
        #region Propiedades
        private int idLugar;
        public int IdLugar{
            get{
                return idLugar;
            }
        }

        private string descripcionLugar;
        public string DescripcionLugar{
            get{
                return descripcionLugar;
            }
            set{
                descripcionLugar=value;
            }
        }
        private Personal personalAtendiendola;
        public Personal PersonalAtendiendola{
            get{
                return personalAtendiendola;
            }
            set{
                personalAtendiendola = value;
            }
        }
        private List<Consumible> consumido;
        public List<Consumible> Consumido {
            get{
                return consumido;
            }
        }
        private List<int> cantidades;
        public List<int> Cantidades {
            get{
                return cantidades;
            }
        }
        protected Cliente cliente;

        public Cliente Cliente{
            get{
                return cliente;
            }
            set{
                cliente = value;
            }
        }
        #endregion

        #region Metodos
        public Lugar(string descripcionLugar){
            this.descripcionLugar=descripcionLugar;
            this.personalAtendiendola = null;
            this.consumido = new List<Consumible>();
            this.cantidades = new List<int>();
        }
        public Lugar(int idLugar, string descripcionLugar){
            this.idLugar=idLugar;
            this.descripcionLugar=descripcionLugar;
            this.personalAtendiendola = null;
            this.consumido = new List<Consumible>();
            this.cantidades = new List<int>();
        }
        #endregion
    }
}