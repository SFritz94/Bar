namespace Entidades.Consumibles
{
    public abstract class Consumible
    {
        #region Propiedades
        private int codigo;
        public int Codigo{
            get{
                return codigo;
            }
        }
        private string nombre;
        public string Nombre{
            get{
                return nombre;
            }
        }
        private string tipoConsumible;

        public string TipoConsumible{
            get{
                return tipoConsumible;
            }
        }
        protected double precio;
        public double Precio{
            get{
                return precio;
            }
            set{
                precio = value;
            }
        }

        protected int stock;
        public double Stock{
            get{
                return stock;
            }
        }
        #endregion

        #region Metodos
        public Consumible(string nombre, double precio, int stock, string tipoConsumible){
            this.nombre=nombre;
            this.precio=precio;
            this.stock=stock;
            this.tipoConsumible=tipoConsumible;
        }
        public Consumible(int codigo, string nombre, double precio, int stock, string tipoConsumible){
            this.codigo=codigo;
            this.nombre=nombre;
            this.precio=precio;
            this.stock=stock;
            this.tipoConsumible=tipoConsumible;
        }
        #endregion
    }
}