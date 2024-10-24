using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class AccesoNegocio
    {
        public int idPermiso { get; set; }       // Identificador único del permiso
        public int idUsuario { get; set; }       // Identificador del usuario
        public int idNegocio { get; set; }       // Identificador del negocio
        public DateTime fechaAsignacion { get; set; }
    }
}
