using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ProductoDetalle
    {
        public DateTime fecha { get; set; }
        public DateTime? fechaEgreso { get; set; }
        public int idProductoDetalle { get; set; }
        public int idProducto { get; set; }
        public string numeroSerie { get; set; }
        public string color { get; set; }
        public string modelo { get; set; }
        public string marca { get; set; }
        public int idNegocio { get; set; }
        public int idProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public  string codigo {get;set;}
        public string nombre { get; set; }
        public bool estado { get; set; }
        public int idVenta { get; set; }
        public string numeroVenta { get; set; }
        public string estadoVendido { get; set; }
        public string nombreLocal { get; set; }
    }
}
