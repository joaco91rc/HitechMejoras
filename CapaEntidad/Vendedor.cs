using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Vendedor
    {
        public int idVendedor { get; set; }        // idVendedor
        public string nombre { get; set; }          // nombre
        public string apellido { get; set; }        // apellido
        public string DNI { get; set; }
        public decimal sueldoBase { get; set; }     // sueldoBase
        public decimal sueldoComision { get; set; } // sueldoComision
        public bool estado { get; set; }
    }
}
