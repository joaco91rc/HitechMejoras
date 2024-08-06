using CapaEntidad;
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

namespace CapaPresentacion
{
    public partial class frmListadoVentas : Form
    {
        public frmListadoVentas()
        {
            InitializeComponent();
        }

        private void frmListadoVentas_Load(object sender, EventArgs e)
        {
            List<Venta> listaVentas = new CN_Venta().ObtenerVentasConDetalle();
            foreach (Venta item in listaVentas)
            {
                if (item.idNegocio == GlobalSettings.SucursalId)
                {

                    dgvData.Rows.Add(new object[] {item.idVenta,
                    item.fechaRegistro.Date,
                    item.tipoDocumento,
                    item.nroDocumento,
                    item.montoTotal,
                    item.nombreCliente,
                    ""

                    });
                }
            }

        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnDetalle")
            {

                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    string nroVenta = dgvData.Rows[indice].Cells["nroDocumento"].Value.ToString();
                    Venta oVenta = new CN_Venta().ObtenerVenta(nroVenta, GlobalSettings.SucursalId);
                    // Pasar el objeto Venta al formulario frmDetalleVenta
                    frmDetalleVenta detalleVentaForm = new frmDetalleVenta(oVenta);
                    detalleVentaForm.ShowDialog();
                }


                }
            }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            int detalleColumnIndex = dgvData.Columns["btnDetalle"].Index;

            if (e.ColumnIndex == detalleColumnIndex)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.viewBtn.Width;
                var h = Properties.Resources.viewBtn.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;
                e.Graphics.DrawImage(Properties.Resources.viewBtn, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }
    }
}
