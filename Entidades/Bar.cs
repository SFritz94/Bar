using System.Collections.Generic;
using Entidades.CarpetaCliente;
using Entidades.CarpetaPersonal;
using Entidades.Consumibles;
using Entidades.Lugares;

namespace Entidades
{
    public static class Bar
    {
        #region Propiedades
        private static double totalRecaudado;
        public static double TotalRecaudado{
            get{
                return totalRecaudado;
            }
            set{
                totalRecaudado = value;
            }
        }
        private static List<Personal> personal;
        public static List<Personal> Personal{
            get{
                return personal;
            }
            set{
                personal = value;
            }
        }
        private static List<Lugar> lugares;
        public static List<Lugar> Lugares{
            get{
                return lugares;
            }
            set{
                lugares = value;
            }
        }
        private static List<Consumible> consumibles;
        public static List<Consumible> Consumibles{
            get{
                return consumibles;
            }
            set{
                consumibles = value;
            }
        }
        private static List<Cliente> clientes;

        public static List<Cliente> Clientes{
            get{
                return clientes;
            }
            set{
                clientes = value;
            }
        }
        #endregion
    }
}