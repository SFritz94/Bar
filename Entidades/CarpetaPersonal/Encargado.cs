namespace Entidades.CarpetaPersonal
{
    public sealed class Encargado:Personal
    {
        public Encargado(string nombre, string apellido, int dni, string direccion, string email, string cargo):base(nombre, apellido, dni, direccion, email, cargo){
            
        }
        public Encargado(int id, string nombre, string apellido, int dni, string direccion, string email, string cargo):base(id, nombre, apellido, dni, direccion, email, cargo){
            
        }
    }
}