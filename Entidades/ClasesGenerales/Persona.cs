namespace Entidades.ClasesGenerales
{
    public abstract class Persona
    {
        #region Propiedades
        private int id;
        public int Id{
            get{
                return id;
            }
        }
        private string nombre;
        public string Nombre{
            get{
                return nombre;
            }
        }
        private string apellido;
        public string Apellido{
            get{
                return apellido;
            }
        }
        private int dni;
        public int Dni{
            get{
                return dni;
            }
        }
        private string direccion;
        public string Direccion{
            get{
                return direccion;
            }
        }
        private string email;
        public string Email{
            get{
                return email;
            }
        }
        #endregion

        #region Metodos
        public Persona(string nombre, string apellido, int dni, string direccion, string email){
            this.nombre=nombre;
            this.apellido=apellido;
            this.dni=dni;
            this.direccion=direccion;
            this.email=email;
        }
        public Persona(int id, string nombre, string apellido, int dni, string direccion, string email){
            this.id=id;
            this.nombre=nombre;
            this.apellido=apellido;
            this.dni=dni;
            this.direccion=direccion;
            this.email=email;
        }
        #endregion
    }
}