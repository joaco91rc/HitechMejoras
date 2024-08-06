using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class FormaPago
    {
        public int idFormaPago { get; set; }
        public string descripcion { get; set; }
        public decimal porcentajeRetencion { get; set; }
        public string cajaAsociada { get; set; }

    }
}
