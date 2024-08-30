using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EquipoST
    {
        public int idEquipoST { get; set; }
        public int idNegocio { get; set; }
        public string nombre { get; set; }
        public string tipoEquipo { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string serialNumber { get; set; }
        public int idCliente { get; set; }
    }
}
