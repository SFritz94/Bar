namespace Entidades.Consumibles
{
    public sealed class Producto: Consumible
    {
        public Producto(string nombre, double precio, int stock, string tipoConsumible):base(nombre, precio, stock, tipoConsumible){

        }
        public Producto(int codigo, string nombre, double precio, int stock, string tipoConsumible):base(codigo, nombre, precio, stock, tipoConsumible){

        }
    }
}