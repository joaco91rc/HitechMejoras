using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
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

namespace CapaPresentacion
{
    public partial class frmProductosSerializados : Form
    {

        private Image defaultImage = Properties.Resources.CHECK;
        private Image defaultImage2 = Properties.Resources.trash;
        public frmProductosSerializados()
        {
            InitializeComponent();
        }

        private void CargarComboBusqueda()
        {
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {

                if (columna.Visible == true && columna.Name != "btnSeleccionar")
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });

                }


            }

            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 1;
        }


        private void CargarComboLocales()
        {
            var listaLocales = new CN_Negocio().ListarNegocios();
            
            
            foreach (var item in listaLocales)
            {

               
                    cboLocales.Items.Add(new OpcionCombo() { Valor = item.idNegocio, Texto =item.nombre });

                


            }

            cboLocales.DisplayMember = "Texto";
            cboLocales.ValueMember = "Valor";
            cboLocales.SelectedIndex = -1;
        }


        private void CargarGrillaEnStockPorLocal(int idLocal)
        {
            if (GlobalSettings.RolUsuario != 1)
            {
                dgvData.Columns["idProveedor"].Visible = false; // Oculta la columna idProveedor
                dgvData.Columns["proveedor"].Visible = false; // Oculta la columna NombreProveedor
            }

            dgvData.Rows.Clear(); // Limpiar el DataGridView antes de cargar nuevos datos
            var listaProductosSerializados = new CN_Producto().ListarProductosConSerialNumberPorLocalDisponibles(idLocal);
            foreach (ProductoDetalle item in listaProductosSerializados)
            {
                dgvData.Rows.Add(new object[] {
            // Asignar la imagen predeterminada
            item.idProductoDetalle,
            item.idProducto, // Id del producto
            item.fecha,
            item.codigo,
            item.nombre,
            item.idProveedor,
            item.NombreProveedor,
            item.marca, // Marca
            item.modelo, // Modelo
            item.color, // Color
            item.numeroSerie, // Número de serie
            item.nombreLocal,
            item.estadoVendido,

            item.idNegocio, // Id del negocio,
            defaultImage,
            defaultImage2

        });
            }
        }

        

        private void CargarGrilla()
        {
            if (GlobalSettings.RolUsuario != 1)
            {
                dgvData.Columns["idProveedor"].Visible = false; // Oculta la columna idProveedor
                dgvData.Columns["proveedor"].Visible = false; // Oculta la columna NombreProveedor
            }
            dgvData.Rows.Clear(); // Limpiar el DataGridView antes de cargar nuevos datos
            var listaProductosSerializados = new CN_Producto().ListarProductosConSerialNumberEnStockTodosLocales();
            foreach (ProductoDetalle item in listaProductosSerializados)
            {
                dgvData.Rows.Add(new object[] {
            // Asignar la imagen predeterminada
            item.idProductoDetalle,
            item.idProducto,
            item.fecha,// Id del producto
            item.codigo,
            item.nombre,
            item.marca, // Marca
            item.modelo, // Modelo
            item.color, // Color
            item.numeroSerie,
            item.nombreLocal,// Número de serie
            item.estadoVendido,         
            
            item.idNegocio, // Id del negocio,
            defaultImage,
            defaultImage2
            
        });
            }
        }


        private void frmProductosSerializados_Load(object sender, EventArgs e)
        {
            CargarComboLocales();
            
            CargarGrillaEnStockPorLocal(GlobalSettings.SucursalId);
            CargarComboBusqueda();
        }


        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar que el clic no sea en el encabezado
            if (e.RowIndex < 0)
                return;

            string mensaje = string.Empty;
            int indice = e.RowIndex;

            // Obtener los datos de la fila seleccionada
            var productoDetalle = new ProductoDetalle
            {
                idProductoDetalle = Convert.ToInt32(dgvData.Rows[indice].Cells["idProductoDetalle"].Value),
                idProducto = Convert.ToInt32(dgvData.Rows[indice].Cells["idProducto"].Value),
                numeroSerie = dgvData.Rows[indice].Cells["serialNumber"].Value.ToString(),
                color = dgvData.Rows[indice].Cells["color"].Value.ToString(),
                modelo = dgvData.Rows[indice].Cells["modelo"].Value.ToString(),
                marca = dgvData.Rows[indice].Cells["marca"].Value.ToString(),
                idNegocio = Convert.ToInt32(dgvData.Rows[indice].Cells["idNegocio"].Value),
                fecha = Convert.ToDateTime(dgvData.Rows[indice].Cells["fecha"].Value),
                idProveedor = Convert.ToInt32(dgvData.Rows[indice].Cells["idProveedor"].Value),
                NombreProveedor = dgvData.Rows[indice].Cells["proveedor"].Value.ToString(),
            };

            // Acción si se hace clic en el botón "Editar"
            if (dgvData.Columns[e.ColumnIndex].Name == "btnEditar")
            {
                if (dgvData.Rows[indice].Cells["estado"].Value.ToString() == "EN STOCK")
                {
                    if (GlobalSettings.RolUsuario == 1)
                    {
                        var editar = new CN_Producto().EditarSerialNumber(productoDetalle, out mensaje);
                        if (editar)
                        {
                            dgvData.Rows[indice].Cells["serialNumber"].Value = productoDetalle.numeroSerie;
                            dgvData.Rows[indice].Cells["color"].Value = productoDetalle.color;
                            dgvData.Rows[indice].Cells["modelo"].Value = productoDetalle.modelo;
                            dgvData.Rows[indice].Cells["marca"].Value = productoDetalle.marca;
                            dgvData.Rows[indice].Cells["idProveedor"].Value = productoDetalle.idProveedor;
                            dgvData.Rows[indice].Cells["proveedor"].Value = productoDetalle.NombreProveedor;

                            foreach (DataGridViewCell cell in dgvData.Rows[indice].Cells)
                            {
                                cell.Style.BackColor = Color.ForestGreen;
                            }

                            MessageBox.Show("Producto Serializado actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"Error al actualizar: {mensaje}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No tiene permisos para editar el Producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error al actualizar: No se puede actualizar un Producto Vendido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Acción si se hace clic en el botón "Eliminar"
            if (dgvData.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                if (GlobalSettings.RolUsuario == 1)
                {
                    var eliminar = new CN_Producto().EliminarSerialNumber(productoDetalle, out mensaje);
                    if (eliminar)
                    {
                        dgvData.Rows.RemoveAt(indice);
                        MessageBox.Show("Producto Serializado Eliminado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No tiene permisos para eliminar el Producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }




        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
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




            }
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

        private bool loading = false; // Variable para evitar recursión en la carga

        private void checkProductosEnStock_CheckedChanged(object sender, EventArgs e)
        {
            if (loading) return; // Si estamos cargando, salimos del método

            loading = true; // Iniciamos la carga

           

            // Cargar grilla en función del estado
            if (checkProductosTodosLocales.Checked)
            {
                CargarGrilla();
            }
            else
            {
                CargarGrillaEnStockPorLocal(GlobalSettings.SucursalId);
            }

            loading = false; // Terminamos la carga
        }

       

        private void iconButton1_Click(object sender, EventArgs e)
        {
            CargarGrillaEnStockPorLocal(GlobalSettings.SucursalId);
        }

        private void cboLocales_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loading) return; // Salir si estamos en un estado de carga

            loading = true; // Indicar que la carga ha iniciado

            if (cboLocales.SelectedIndex >= 0) // Verificar que hay un ítem seleccionado
            {
                // Obtener el valor del item seleccionado
                var opcionSeleccionada = (OpcionCombo)cboLocales.SelectedItem;
                int idLocalSeleccionado = Convert.ToInt32(opcionSeleccionada.Valor);

                // Ejecutar la acción correspondiente al idLocal
                switch (idLocalSeleccionado)
                {
                    case 1: // Hitech 1
                        CargarGrillaEnStockPorLocal(1);
                        break;

                    case 2: // Hitech 2
                        CargarGrillaEnStockPorLocal(2);
                        break;

                    case 3: // Apple 49
                        CargarGrillaEnStockPorLocal(3);
                        break;

                    case 4: // Apple Café
                        CargarGrillaEnStockPorLocal(4);
                        break;

                    default: // Mostrar todos los productos
                        CargarGrillaEnStockPorLocal(GlobalSettings.SucursalId);
                        break;
                }
            }
            else
            {
                // Si no hay selección, cargar todos los productos
                CargarGrillaEnStockPorLocal(GlobalSettings.SucursalId);
            }

            loading = false; // Indicar que la carga ha finalizado
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si se hizo doble clic en la columna "proveedor"
            if (e.RowIndex >= 0 && dgvData.Columns[e.ColumnIndex].Name == "proveedor")
            {
                // Finalizar el modo de edición antes de abrir el modal
                dgvData.EndEdit();

                // Abrir el modal mdProveedor
                using (mdProveedor modalProveedor = new mdProveedor())
                {
                    var result = modalProveedor.ShowDialog(); // Mostrar el modal y esperar el resultado

                    // Si se seleccionó un proveedor y se cerró el modal con OK
                    if (result == DialogResult.OK && modalProveedor._Proveedor != null)
                    {
                        // Finalizar edición y actualizar valores
                        dgvData.Rows[e.RowIndex].Cells["proveedor"].Value = modalProveedor._Proveedor.razonSocial;
                        dgvData.Rows[e.RowIndex].Cells["idProveedor"].Value = modalProveedor._Proveedor.idProveedor;

                        // Actualizar visualmente el DataGridView
                        dgvData.RefreshEdit();
                    }
                }
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {

        }
    }
}
