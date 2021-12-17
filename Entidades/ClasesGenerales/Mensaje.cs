namespace Entidades.ClasesGenerales
{
    public sealed class Mensaje
    {
        #region Propiedades
        private string contenido;

        public string Contenido{
            get{
                return contenido;
            }
            set{
                contenido = value;
            }
        }
        #endregion

        #region Metodos
        public Mensaje(){

        }
        public Mensaje(string contenido){
            this.contenido = contenido;
        }
        #endregion
    }
}