namespace Entidades.CarpetaPersonal
{
    public sealed class Mozo:Personal
    {
        #region Propiedades
        private bool atiendeMesa;

        public bool AtiendeMesa{
            get{
                return atiendeMesa;
            }
            set{
                atiendeMesa = value;
            }
        }
        #endregion

        #region Metodos
        public Mozo(string nombre, string apellido, int dni, string direccion, string email, string cargo):base(nombre, apellido, dni, direccion, email, cargo){
            
        }
        public Mozo(int id, string nombre, string apellido, int dni, string direccion, string email, string cargo):base(id, nombre, apellido, dni, direccion, email, cargo){
            
        }
        #endregion
    }
}