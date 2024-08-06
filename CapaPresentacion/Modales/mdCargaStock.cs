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
                    item.costoPesos,
                    item.precioCompra,
                    item.precioVenta,

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
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizarStock_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            bool editarPrecios = false;

            // Iterar sobre las filas del DataGridView
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                // Verificar si la fila está marcada como modificada
                if (row.Tag != null && (bool)row.Tag == true)
                {
                    // Obtener el idProducto, idNegocio y el nuevo valor de stock
                    int idProducto = Convert.ToInt32(row.Cells["idProducto"].Value); // Ajusta el nombre de la columna según tu DataGridView
                    int idNegocio = GlobalSettings.SucursalId; // Ajusta el nombre de la columna según tu DataGridView
                    int nuevoStock = Convert.ToInt32(row.Cells["stock"].Value);      // Ajusta el nombre de la columna según tu DataGridView
                    decimal precioCompra = Convert.ToDecimal(row.Cells["precioCompra"].Value);
                    decimal precioVenta = Convert.ToDecimal(row.Cells["precioVenta"].Value);
                    decimal costoPesos = Convert.ToDecimal(row.Cells["costoPesos"].Value);
                    // Llamar al método para cargar o actualizar el stock del producto
                    new CN_ProductoNegocio().SobrescribirStock(idProducto, idNegocio, nuevoStock);
                    var producto = new CN_Producto().ObtenerProductoPorId(idProducto);
                    producto.precioCompra = precioCompra;
                    producto.precioVenta = precioVenta;
                    if (costoPesos != 0)
                    {
                        producto.costoPesos = costoPesos;
                        producto.precioCompra = costoPesos / cotizacionActiva; 
                    }
                    
                    editarPrecios = new CN_Producto().Editar(producto, out mensaje);

                    // Resetear la marca de modificación de la fila
                    row.Tag = null;
                }
            }

            if (editarPrecios)
            {
                MessageBox.Show("Se ha actualizado el Stock y los precios.");
            }
            else
            {
                MessageBox.Show("El stock ha sido actualizado correctamente.");
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

    }
}
