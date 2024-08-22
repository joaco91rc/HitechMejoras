using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Concepto
    {
        public int idConcepto { get; set; }
        public string descripcion { get; set; }
        public string tipo { get; set; }
        public bool estado { get; set; }
        public DateTime fechaRegistro { get; set; }
    }
}
