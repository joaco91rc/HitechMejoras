using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class OrdenTraspaso
    {
        public int IdOrdenTraspaso { get; set; }
        public int IdSucursalOrigen { get; set; }
        public int IdSucursalDestino { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public string Confirmada { get; set; }
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaConfirmacion { get; set; }

    }
}
