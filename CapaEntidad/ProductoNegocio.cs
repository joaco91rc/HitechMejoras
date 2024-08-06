using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ProductoNegocio
    {

        public int idProductoNegocio { get; set; }
        public int idProducto { get; set; }
        public int idNegocio { get; set; }
        public int stock { get; set; }
        

        public Producto oProducto { get; set; }
        public Negocio oNegocio { get; set; }
    }
}
