using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Modales
{
    public partial class ConfirmarReparacion : Form
    {
        private int _idServicioTecnico;
        public ConfirmarReparacion(int idServicioTecnico)
        {
            InitializeComponent();
            _idServicioTecnico = idServicioTecnico;
        }

        private void btnCompletarReparacion_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            var completarreparacion = new CN_ServicioTecnico().CambiarEstadoPendienteACompletado(_idServicioTecnico, mensaje);
        }
    }
}
