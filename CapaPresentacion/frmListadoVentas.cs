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
    {   private Venta _Venta;
        private Usuario _Usuario;
        private Image defaultImageView = Properties.Resources.VIEWICON;
        private Image defaultImageEditar = Properties.Resources.detail;
        public frmListadoVentas(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
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
                    defaultImageView,
                    defaultImageEditar

                    });
                }
            }

        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = e.RowIndex;
            string nroVenta = dgvData.Rows[indice].Cells["nroDocumento"].Value.ToString();
            Venta oVenta = new CN_Venta().ObtenerVenta(nroVenta, GlobalSettings.SucursalId);
            if (dgvData.Columns[e.ColumnIndex].Name == "btnDetalle")
            {

                

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    
                    
                    // Pasar el objeto Venta al formulario frmDetalleVenta
                    frmDetalleVenta detalleVentaForm = new frmDetalleVenta(oVenta);
                    detalleVentaForm.ShowDialog();
                }


                }
            if (dgvData.Columns[e.ColumnIndex].Name == "btnEditarVenta")
            {
                frmVentas editarVentaForm = new frmVentas(_Usuario, oVenta);
                editarVentaForm.ShowDialog();

            }
            }

       
    }
}
