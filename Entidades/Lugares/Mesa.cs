namespace Entidades.Lugares
{
    public sealed class Mesa:Lugar
    {
        #region Propiedades
        private bool estaOcupada;

        public bool EstaOcupada{
            get{
                return estaOcupada;
            }
            set{
                estaOcupada = value;
            }
        }
        #endregion

        #region Metodos
        public Mesa(string descripcion):base(descripcion){
            this.estaOcupada = false;
        }
        public Mesa(int idMesa, string descripcion):base(idMesa, descripcion){
            this.estaOcupada = false;
        }
        #endregion
    }
}