using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class TransaccionCaja
    {
        public int idTransaccion { get; set; }
        public int? idVenta { get; set; }
        public int? idCompra { get; set; }
        public int? idNegocio { get; set; }
        public int idCajaRegistradora { get; set; }
        public string hora { get; set; }
        public string tipoTransaccion { get; set; }
        public decimal monto { get; set; }
        public string docAsociado { get; set; }
        public string usuarioTransaccion { get; set; }
        public string formaPago { get; set; }
        public string cajaAsociada { get; set; }
        

    }
}
