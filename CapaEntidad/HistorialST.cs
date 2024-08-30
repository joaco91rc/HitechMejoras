using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class HistorialST
    {

        public int idHistorialST { get; set; }
        public int idEquipoST { get; set; }
        public DateTime fecha { get; set; }
        public string descripcion { get; set; }
        public string estado { get; set; }
        public string tecnico { get; set; }

    }
}
