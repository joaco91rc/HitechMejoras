using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class PagoParcial
    {
        public int idPagoParcial { get; set; }
        public int idCliente { get; set; }
        public string nombreCliente { get; set; } // Nuevo campo para el nombre del cliente
        public decimal monto { get; set; }
        public int? idVenta { get; set; } // Es opcional porque no siempre puede estar asociado a una venta
        public string numeroVenta { get; set; } // Nuevo campo para el número de venta (opcional)
        public DateTime fechaRegistro { get; set; }
        public bool estado { get; set; }
        public string vendedor { get; set; }
        public string productoReservado { get; set; }
        public string formaPago { get; set; }
    }


}
