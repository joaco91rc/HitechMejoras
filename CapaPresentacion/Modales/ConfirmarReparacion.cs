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
            var completarReparacion = new CN_ServicioTecnico().CambiarEstadoPendienteACompletado(_idServicioTecnico, txtDescripcionReparacion.Text,txtObservaciones.Text, out mensaje);
            if (completarReparacion)
            {
                MessageBox.Show("Se ha cambiado el Estado de la reparacion a Completado", "Servicio Tecnico Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            } else
            {
                MessageBox.Show("No se ha podido cambiar el Estado de la reparacion", "Servicio Tecnico Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
