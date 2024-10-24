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
        private Image defaultImage2 = Properties.Resources.trash;
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
                    item.SerialNumber,
                    item.Cantidad,
                    item.Confirmada,
                    item.IdOrdenTraspaso,
                    item.IdSucursalOrigen,
                    item.IdSucursalDestino,
                    item.FechaConfirmacion,
                    item.CostoProducto,
                    defaultImage,
                    defaultImage2
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
            string mensaje = string.Empty;
            if (dgvData.Columns[e.ColumnIndex].Name == "btnConfirmarRecepcion")
            {
                if(GlobalSettings.RolUsuario == 1) { 
                // Obtener la fila seleccionada
                DataGridViewRow selectedRow = dgvData.Rows[e.RowIndex];

                // Obtener los valores de las celdas de la fila seleccionada
                int idOrdenTraspaso = Convert.ToInt32(selectedRow.Cells["IdOrdenTraspaso"].Value);

                int idProducto = Convert.ToInt32(selectedRow.Cells["idProducto"].Value);

                int cantidad = Convert.ToInt32(selectedRow.Cells["Cantidad"].Value);

                decimal costoProducto = Convert.ToDecimal(selectedRow.Cells["CostoProducto"].Value);
                    int idSucursalOrigen = Convert.ToInt32(selectedRow.Cells["IdSucursalOrigen"].Value);
                    int idSucursalDestino = Convert.ToInt32(selectedRow.Cells["IdSucursalDestino"].Value);
                    int idTraspasoMercaderia = Convert.ToInt32(selectedRow.Cells["IdOrdenTraspaso"].Value);
                    string serialNumber = selectedRow.Cells["SerialNumber"].Value.ToString();

                    var ConfirmarOrden = new CN_OrdenTraspaso().ConfirmarOrdenTraspaso(idOrdenTraspaso);

                if (ConfirmarOrden)
                {
                    actualizacionStock = new CN_ProductoNegocio().CargarOActualizarStockProducto(idProducto, GlobalSettings.SucursalId, cantidad);
                        ProductoDetalle productoDetalle = new ProductoDetalle();
                        productoDetalle.idNegocio = idSucursalDestino;
                        productoDetalle.numeroSerie = serialNumber;

                        var traspasarSN = new CN_Producto().TraspasarSerialNumber(productoDetalle, out mensaje);

                        Deuda deuda = new Deuda();
                        deuda.idSucursalOrigen = idSucursalOrigen;
                        deuda.idSucursalDestino = idSucursalDestino;
                        deuda.costo = costoProducto;
                        deuda.fecha = DateTime.Now.Date;
                        deuda.idTraspasoMercaderia = idTraspasoMercaderia;
                        deuda.estado = "NO PAGO";

                        var insertarDeuda = new CN_Deuda().InsertarDeuda(deuda);
                        string mensajeDeuda = string.Empty;
                        if (insertarDeuda)
                        {
                            mensajeDeuda = "Se ha insertado la Deuda";
                        } 

                    MessageBox.Show("Producto Ingresado. " + actualizacionStock +" " + mensajeDeuda);
                    CargarGrilla();
                }
                else
                {

                    MessageBox.Show("No se Pudo Ingresar el Producto");
                }
                } else
                {
                    MessageBox.Show("No posee permisos para Confirmar la Recepcion de Mercaderia. Contactese con un Administrador");
                }
            }

            if (dgvData.Columns[e.ColumnIndex].Name == "btnRechazarRecepcion")
            {
                if (GlobalSettings.RolUsuario == 1)
                {
                    // Obtener la fila seleccionada
                    DataGridViewRow selectedRow = dgvData.Rows[e.RowIndex];

                    // Obtener los valores de las celdas de la fila seleccionada
                    int idOrdenTraspaso = Convert.ToInt32(selectedRow.Cells["IdOrdenTraspaso"].Value);

                    int idProducto = Convert.ToInt32(selectedRow.Cells["idProducto"].Value);

                    int cantidad = Convert.ToInt32(selectedRow.Cells["Cantidad"].Value);

                    int idSucursalOrigen = Convert.ToInt32(selectedRow.Cells["idSucursalOrigen"].Value);

                    var Rechazar = new CN_OrdenTraspaso().RechazarOrdenTraspaso(idOrdenTraspaso);

                    if (Rechazar)
                    {
                        actualizacionStock = new CN_ProductoNegocio().CargarOActualizarStockProducto(idProducto, idSucursalOrigen, cantidad);

                        MessageBox.Show("Producto Devuelto a Sucursal de Origen. " + actualizacionStock);
                        CargarGrilla();
                    }
                    else
                    {

                        MessageBox.Show("No se Pudo Ingresar el Producto");
                    }
                } else
                {
                    MessageBox.Show("No posee permisos para Rechazar el Traspaso de Mercaderia. Contactese con un Administrador");
                }
            }
        }

        
    }
}
