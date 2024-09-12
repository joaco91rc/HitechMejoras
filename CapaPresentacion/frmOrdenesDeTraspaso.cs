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
    public partial class frmOrdenesDeTraspaso : Form
    {
        private Image defaultImage = Properties.Resources.CHECK;
        public frmOrdenesDeTraspaso()
        {
            InitializeComponent();
        }

        private void CargarGrilla() {
            dgvData.Rows.Clear();
            List<OrdenTraspaso> listaOrdenes = new CN_OrdenTraspaso().ListarOrdenesTraspaso().Where(ot => ot.Confirmada == "0" && ot.IdSucursalDestino == GlobalSettings.SucursalId).ToList();

            foreach (OrdenTraspaso item in listaOrdenes)
            {

                string nombreProducto = new CN_Producto().ObtenerProductoPorId(item.IdProducto).nombre;

                dgvData.Rows.Add(new object[] {item.FechaCreacion,
                    item.IdProducto,
                    nombreProducto,
                    item.Cantidad,
                    item.Confirmada,
                    item.IdOrdenTraspaso,
                    item.IdSucursalOrigen,
                    item.IdSucursalDestino,
                    item.FechaConfirmacion,
                    defaultImage
                    });
            }

        }
        private void frmOrdenesDeTraspaso_Load(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string actualizacionStock = string.Empty;
            if (dgvData.Columns[e.ColumnIndex].Name == "btnConfirmarRecepcion")
            {
                // Obtener la fila seleccionada
                DataGridViewRow selectedRow = dgvData.Rows[e.RowIndex];

                // Obtener los valores de las celdas de la fila seleccionada
                int idOrdenTraspaso = Convert.ToInt32(selectedRow.Cells["IdOrdenTraspaso"].Value);

                int idProducto = Convert.ToInt32(selectedRow.Cells["idProducto"].Value);

                int cantidad = Convert.ToInt32(selectedRow.Cells["Cantidad"].Value);

                var ConfirmarOrden = new CN_OrdenTraspaso().ConfirmarOrdenTraspaso(idOrdenTraspaso);

                if (ConfirmarOrden)
                {
                    actualizacionStock = new CN_ProductoNegocio().CargarOActualizarStockProducto(idProducto, GlobalSettings.SucursalId, cantidad);

                    MessageBox.Show("Producto Ingresado. " + actualizacionStock);
                    CargarGrilla();
                }
                else
                {

                    MessageBox.Show("No se Pudo Ingresar el Producto");
                }
            
            }
        }

        
    }
}
