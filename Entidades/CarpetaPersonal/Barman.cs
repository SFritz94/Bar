namespace Entidades.CarpetaPersonal
{
    public sealed class Barman:Personal
    {
        #region Metodos
        public Barman(string nombre, string apellido, int dni, string direccion, string email, string cargo):base(nombre, apellido, dni, direccion, email, cargo){
            
        }
        public Barman(int id, string nombre, string apellido, int dni, string direccion, string email, string cargo):base(id, nombre, apellido, dni, direccion, email, cargo){
            
        }
        #endregion
    } 
}