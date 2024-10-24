using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Deuda
    {
        public int idDeuda { get; set; }
        public DateTime fecha { get; set; }
        public decimal costo { get; set; }
        public int idSucursalOrigen { get; set; }
        public int idSucursalDestino { get; set; }
        public string estado { get; set; }
        public int? idTraspasoMercaderia { get; set; }

        // Nuevas propiedades para los nombres de las sucursales
        public string nombreSucursalOrigen { get; set; }
        public string nombreSucursalDestino { get; set; }
        public string nombreProducto { get; set; }


    }
}
