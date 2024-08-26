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
        private Image defaultImage = Properties.Resources.detail;
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
                    defaultImage

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

        
    }
}
