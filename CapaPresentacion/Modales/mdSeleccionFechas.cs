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
    public partial class mdSeleccionFechas : Form
    {
        public DateTime FechaDesde { get; private set; }
        public DateTime FechaHasta { get; private set; }
        public mdSeleccionFechas()
        {
            InitializeComponent();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            // Establecer los valores de fecha desde y hasta con las horas adecuadas
            FechaDesde = dtpFechaDesde.Value.Date; // Ajusta a 00:00
            FechaHasta = dtpFechaHasta.Value.Date.AddDays(1).AddTicks(-1); // Ajusta a 23:59:59.999

            this.DialogResult = DialogResult.OK; // Indica que se aceptaron las fechas
            this.Close();
        }
    }
}
