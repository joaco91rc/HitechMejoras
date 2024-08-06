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
                    ""});
            }

        }
        private void frmOrdenesDeTraspaso_Load(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
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
                    new CN_ProductoNegocio().CargarOActualizarStockProducto(idProducto, GlobalSettings.SucursalId, cantidad);

                    MessageBox.Show("Producto Ingresado");
                    CargarGrilla();
                }
                else
                {

                    MessageBox.Show("No se Pudo Ingresar el Producto");
                }
            
            }
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            int traspasarColumnIndex = dgvData.Columns["btnConfirmarRecepcion"].Index;

            if (e.ColumnIndex == traspasarColumnIndex)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.check20.Width;
                var h = Properties.Resources.check20.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;
                e.Graphics.DrawImage(Properties.Resources.check20, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }
    }
}
