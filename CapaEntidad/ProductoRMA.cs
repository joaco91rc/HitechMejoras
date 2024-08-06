using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
   public  class ProductoRMA
    {
        public int idProductoRMA { get; set; }
        public string estado { get; set; }
        public int cantidad { get; set; }
        public string descripcionProductoRMA { get; set; }
        public int idProducto { get; set; }

        public DateTime fechaIngreso { get; set; }
        public DateTime fechaEgreso { get; set; }



    }
}
