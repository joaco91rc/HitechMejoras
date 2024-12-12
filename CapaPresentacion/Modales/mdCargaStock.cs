using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
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
    public partial class mdCargaStock : Form
    {
        private bool checkProductoDolarActualizado = false;
        public decimal cotizacionActiva { get; set; } = new CN_Cotizacion().CotizacionActiva().importe;
        public mdCargaStock()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();

            if (dgvData.Rows.Count > 0)
            {

                foreach (DataGridViewRow row in dgvData.Rows)
                {

                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;


                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBusqueda.Clear();
            foreach (DataGridViewRow row in dgvData.Rows)
                row.Visible = true;
        }

        private void dgvData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Marca la fila como modificada
            dgvData.Rows[e.RowIndex].Tag = true;
        }

        private void CargarGrilla()
        {
            dgvData.Rows.Clear();
            //Mostrar todos los Productos
            List<Producto> listaProducto = new CN_Producto().Listar(GlobalSettings.SucursalId);

            foreach (Producto item in listaProducto)
            {
                //int stockProducto = new CN_ProductoNegocio().ObtenerStockProductoEnSucursal(item.idProducto, GlobalSettings.SucursalId);
                
                    dgvData.Rows.Add(new object[] { item.idProducto,
                    item.codigo,
                    item.nombre,
                    item.oCategoria.descripcion,
                    item.stock,
                    item.precioLista,
                    item.costoPesos,
                    item.precioCompra,
                    item.precioVenta,
                    item.productoDolar?true:false
                    });
                
            }
        }

        private void CargarGrillaPorNegocio()
        {
            dgvData.Rows.Clear();
            //Mostrar todos los Productos
            List<Producto> listaProducto = new CN_Producto().ListarPorNegocio(GlobalSettings.SucursalId);

            foreach (Producto item in listaProducto)
            {
                //int stockProducto = new CN_ProductoNegocio().ObtenerStockProductoEnSucursal(item.idProducto, GlobalSettings.SucursalId);

                dgvData.Rows.Add(new object[] { item.idProducto,
                    item.codigo,
                    item.nombre,
                    item.oCategoria.descripcion,
                    item.stock,
                    item.precioLista,
                    item.costoPesos,
                    item.precioCompra,
                    item.precioVenta,
                    item.productoDolar?true:false,

                    });

            }
        }

        private void mdCargaStock_Load(object sender, EventArgs e)
        {
            dgvData.CellValueChanged += new DataGridViewCellEventHandler(dgvData_CellValueChanged);
            txtBusqueda.Select();
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {

                if (columna.Visible == true)
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });

                }


            }

            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 1;

            CargarGrilla();
            if(GlobalSettings.RolUsuario == 1)
            {
                btnActualizarStock.Visible = true;
            }
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizarStock_Click(object sender, EventArgs e)
        {
            if (GlobalSettings.RolUsuario != 1)
            {
                MessageBox.Show("No posee permisos para modificar stock", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string mensaje = string.Empty;
            bool actualizarStock = false;
            bool editarPrecioProductosPesos = false;
            bool editarPrecioProductoDolares = false;
            bool checkProductoDolarActualizado = false;
            string actualizacionStock = string.Empty;
            string actualizacionPreciosPesos = string.Empty;
            string actualizacionPreciosDolares = string.Empty;
            string actualizacionProductoDolar = string.Empty;

            foreach (DataGridViewRow row in dgvData.Rows)
            {
                if (row.Tag != null && (bool)row.Tag == true)
                {
                    int idProducto = Convert.ToInt32(row.Cells["idProducto"].Value);
                    int idNegocio = GlobalSettings.SucursalId;
                    int nuevoStock = Convert.ToInt32(row.Cells["stock"].Value);
                    decimal precioCompra = Convert.ToDecimal(row.Cells["precioCompra"].Value);
                    decimal precioVenta = Convert.ToDecimal(row.Cells["precioVenta"].Value);
                    decimal costoPesos = Convert.ToDecimal(row.Cells["costoPesos"].Value);
                    decimal precioLista = Convert.ToDecimal(row.Cells["precioLista"].Value);
                    bool checkProductoDolares = Convert.ToBoolean(row.Cells["checkProductoDolares"].Value);

                    try
                    {
                        // Obtener el stock actual del producto
                        int stockActual = new CN_ProductoNegocio().ObtenerStockProductoEnSucursal(idProducto, idNegocio);

                        // Verificar si el stock ha cambiado
                        if (nuevoStock != stockActual)
                        {
                            new CN_ProductoNegocio().SobrescribirStock(idProducto, idNegocio, nuevoStock);
                            actualizarStock = true;
                        }

                        // Obtener precios actuales
                        PrecioProducto precioPesosActual = new CN_PrecioProducto().ObtenerPreciosPorProductoYMoneda(idProducto, 1);
                        PrecioProducto precioDolarActual = new CN_PrecioProducto().ObtenerPreciosPorProductoYMoneda(idProducto, 2);

                        // Si el producto no está marcado en dólares
                        if (!checkProductoDolares)
                        {
                            // Verificar cambios en precios en pesos
                            if (costoPesos != precioPesosActual.PrecioCompra || precioLista != precioPesosActual.PrecioLista)
                            {
                                // Actualizar precios en pesos
                                PrecioProducto objPrecioProductoPesos = new PrecioProducto
                                {
                                    IdPrecioProducto = precioPesosActual.IdPrecioProducto,
                                    IdProducto = idProducto,
                                    PrecioCompra = costoPesos,
                                    PrecioVenta = precioLista,
                                    PrecioLista = precioLista,
                                    PrecioEfectivo = precioLista * 0.85m,
                                    IdMoneda = 1
                                };
                                editarPrecioProductosPesos = new CN_PrecioProducto().EditarPrecioProducto(objPrecioProductoPesos, out mensaje);

                                // Calcular precios en dólares con la cotización activa
                                decimal precioDolarCompra = Math.Round(costoPesos / cotizacionActiva, 2);
                                decimal precioDolarVenta = Math.Round(precioLista / cotizacionActiva, 2);

                                // Actualizar precios en dólares
                                PrecioProducto objPrecioProductoDolares = new PrecioProducto
                                {
                                    IdPrecioProducto = precioDolarActual.IdPrecioProducto,
                                    IdProducto = idProducto,
                                    PrecioCompra = precioDolarCompra,
                                    PrecioVenta = precioDolarVenta,
                                    PrecioLista = precioDolarVenta * cotizacionActiva * 1.35m,
                                    PrecioEfectivo = precioDolarVenta,
                                    IdMoneda = 2
                                };
                                editarPrecioProductoDolares = new CN_PrecioProducto().EditarPrecioProducto(objPrecioProductoDolares, out mensaje);
                            }
                        }
                        else
                        {
                            // Verificar cambios en precios en dólares
                            if (precioCompra != precioDolarActual.PrecioCompra || precioVenta != precioDolarActual.PrecioVenta)
                            {
                                // Actualizar precios en dólares
                                PrecioProducto objPrecioProductoDolares = new PrecioProducto
                                {
                                    IdPrecioProducto = precioDolarActual.IdPrecioProducto,
                                    IdProducto = idProducto,
                                    PrecioCompra = precioCompra,
                                    PrecioVenta = precioVenta,
                                    PrecioLista = precioVenta * cotizacionActiva * 1.35m,
                                    PrecioEfectivo = precioVenta,
                                    IdMoneda = 2
                                };
                                editarPrecioProductoDolares = new CN_PrecioProducto().EditarPrecioProducto(objPrecioProductoDolares, out mensaje);

                                // Calcular precios en pesos con la cotización activa
                                decimal precioPesosCompra = Math.Round(precioCompra * cotizacionActiva, 2);
                                decimal precioPesosVenta = Math.Round(precioVenta * cotizacionActiva, 2);

                                // Actualizar precios en pesos
                                PrecioProducto objPrecioProductoPesos = new PrecioProducto
                                {
                                    IdPrecioProducto = precioPesosActual.IdPrecioProducto,
                                    IdProducto = idProducto,
                                    PrecioCompra = precioPesosCompra,
                                    PrecioVenta = precioPesosVenta,
                                    PrecioLista = precioPesosVenta * 1.35m,
                                    PrecioEfectivo = precioPesosVenta * 0.85m,
                                    IdMoneda = 1
                                };
                                editarPrecioProductosPesos = new CN_PrecioProducto().EditarPrecioProducto(objPrecioProductoPesos, out mensaje);
                            }
                            // Cambiar el estado de checkProductoDolares
                            checkProductoDolarActualizado = true;
                        }

                        // Registrar mensajes para cada cambio
                        if (actualizarStock || editarPrecioProductosPesos || editarPrecioProductoDolares || checkProductoDolarActualizado)
                        {
                            actualizacionPreciosPesos = editarPrecioProductosPesos ? "Se ha actualizado el precio en pesos." : "";
                            actualizacionPreciosDolares = editarPrecioProductoDolares ? "Se ha actualizado el precio en dólares." : "";
                            actualizacionStock = actualizarStock ? "Stock actualizado." : "";
                            actualizacionProductoDolar = checkProductoDolarActualizado ? "Se ha modificado el Producto." : "";
                        }

                        row.Tag = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al actualizar: {ex.Message}");
                        actualizarStock = false;
                    }
                }
            }

            if (actualizarStock || editarPrecioProductosPesos || editarPrecioProductoDolares || checkProductoDolarActualizado)
            {
                MessageBox.Show($"{actualizacionStock} {actualizacionPreciosPesos} {actualizacionPreciosDolares} {actualizacionProductoDolar}".Trim());
            }

            this.Close();
        }




        private void btnTraspasarStock_Click(object sender, EventArgs e)
        {
            mdTraspasoStock mdtstock = new mdTraspasoStock() ;
            mdtstock.FormClosed += new FormClosedEventHandler(mdtstock_FormClosed);
            mdtstock.Show();
           
        }
        private void mdtstock_FormClosed(object sender, FormClosedEventArgs e)
        {
            CargarGrilla();
            this.Activate();
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            // Verificar si la tecla presionada es Enter
            if (e.KeyCode == Keys.Enter)
            {
                string columnaFiltro = ((OpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();

                if (dgvData.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvData.Rows)
                    {
                        if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Text.Trim().ToUpper()))
                            row.Visible = true;
                        else
                            row.Visible = false;
                    }
                }

                // Evitar que el sonido de beep se produzca cuando se presiona Enter
                e.SuppressKeyPress = true;
            }
        }

        private void checkProductosLocal_CheckedChanged(object sender, EventArgs e)
        {
            if (checkProductosLocal.Checked)
            {
                CargarGrillaPorNegocio();
            } 
            else
            {
                CargarGrilla();
            }
        }

        private void dgvData_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvData.IsCurrentCellDirty)
            {
                dgvData.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvData_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica que sea la columna "checkProductoDolares"
            if (e.RowIndex >= 0 && dgvData.Columns[e.ColumnIndex].Name == "checkProductoDolares")
            {
                int idProducto = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["idProducto"].Value);

                // Obtén el estado actual del CheckBox
                bool productoDolar = Convert.ToBoolean(dgvData.Rows[e.RowIndex].Cells["checkProductoDolares"].Value);

                // Actualiza en la base de datos
                var actualizarProducto = new CN_Producto().ActualizarProductoDolar(idProducto, productoDolar);
                if (actualizarProducto)
                {
                    checkProductoDolarActualizado = true;
                }

                
            }
        }

        
    }
}
