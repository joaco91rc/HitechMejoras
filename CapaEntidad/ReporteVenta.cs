using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ReporteVenta
    {

        public string fechaRegistro { get; set; }
        public string tipoDocumento { get; set; }
        public string nroDocumento { get; set; }
        public string montoTotal { get; set; }
        public string cotizacionDolar { get; set; }
        public string documentoCliente { get; set; }
        public string nombreCliente { get; set; }

        public string nombreProducto { get; set; }
        public string costoTotalProductos { get; set; }


        public string margenGananciaEnDolares {get;set;}
        public string porcentajeMargenGanancia { get; set; }
        public string vendedor { get; set; }
        public string nombreLocal { get; set; }
    }

}
