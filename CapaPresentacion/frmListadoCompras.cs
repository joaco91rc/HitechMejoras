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
    public partial class frmListadoCompras : Form
    {
        public frmListadoCompras()
        {
            InitializeComponent();
        }

        private void frmListadoCompras_Load(object sender, EventArgs e)
        {
            List<Compra> listaCompras = new CN_Compra().ObtenerComprasConDetalle();
            foreach (Compra item in listaCompras)
            {
                if (item.idNegocio == GlobalSettings.SucursalId)
                {

                    dgvData.Rows.Add(new object[] {item.idCompra,
                    item.fechaRegistro,
                    item.tipoDocumento,
                    item.nroDocumento,
                    item.montoTotal,
                    item.oProveedor.razonSocial,
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
                    string nroCompra = dgvData.Rows[indice].Cells["nroDocumento"].Value.ToString();
                    Compra oCompra = new CN_Compra().ObtenerCompra(nroCompra, GlobalSettings.SucursalId);
                    // Pasar el objeto Venta al formulario frmDetalleVenta
                    frmDetalleCompra detalleCompraForm = new frmDetalleCompra(oCompra);
                    detalleCompraForm.ShowDialog();
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
