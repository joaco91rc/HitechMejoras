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
    public partial class mdProductoSerializable : Form
    {
        private int idProducto;
        public List<ProductoDetalle> ListaProductoDetalles { get; set; }
        public string SerialNumber { get; private set; }
        public mdProductoSerializable(int idProducto)
        {
            InitializeComponent();
            this.idProducto = idProducto;
            ListaProductoDetalles = new List<ProductoDetalle>(); // Inicializa la lista

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
        private void CargarGrilla(int idProducto)
        {

            dgvData.Rows.Clear(); // Limpiar el DataGridView antes de cargar nuevos datos
            var listaProductosSerializados = new CN_Producto().ListarProductosConSerialNumberByID(idProducto);
            foreach (ProductoDetalle item in listaProductosSerializados)
            {
                dgvData.Rows.Add(new object[] {
             // Asignar la imagen predeterminada
             item.idProductoDetalle,
            item.idProducto, // Id del producto
            item.codigo,
            item.nombre,
            item.marca, // Marca
            item.modelo, // Modelo
            item.color, // Color
            item.numeroSerie, // Número de serie
                     
            
            item.idNegocio, // Id del negocio
            
        });
            }
            
        }

        private void CargarGrillaPorLocal(int idProducto, int idNegocio)
        {

            dgvData.Rows.Clear(); // Limpiar el DataGridView antes de cargar nuevos datos
            var listaProductosSerializados = new CN_Producto().ListarProductosConSerialNumberByIDNegocio(idProducto, idNegocio);
            foreach (ProductoDetalle item in listaProductosSerializados)
            {
                dgvData.Rows.Add(new object[] {
             // Asignar la imagen predeterminada
             item.idProductoDetalle,
            item.idProducto, // Id del producto
            item.codigo,
            item.nombre,
            item.marca, // Marca
            item.modelo, // Modelo
            item.color, // Color
            item.numeroSerie, // Número de serie
                     
            
            item.idNegocio, // Id del negocio
            
        });
            }

        }

        private void mdProductoSerializable_Load(object sender, EventArgs e)
        {
            CargarGrillaPorLocal(idProducto,GlobalSettings.SucursalId);
            CargarComboBusqueda();
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int iColumn = e.ColumnIndex;

            if (iRow >= 0 && iColumn >= 0) // Asegúrate de que sea una celda válida
            {
                // Validar que las celdas no sean nulas o vacías
                if (dgvData.Rows[iRow].Cells["idProductoDetalle"].Value != null &&
                    dgvData.Rows[iRow].Cells["idProdSerial"].Value != null &&
                    dgvData.Rows[iRow].Cells["serialNumber"].Value != null)
                {
                    // Crear el objeto ProductoDetalle tomando los valores de la fila seleccionada
                    ProductoDetalle productoDetalle = new ProductoDetalle()
                    {
                        idProductoDetalle = Convert.ToInt32(dgvData.Rows[iRow].Cells["idProductoDetalle"].Value.ToString()),
                        idProducto = Convert.ToInt32(dgvData.Rows[iRow].Cells["idProdSerial"].Value.ToString()),
                        numeroSerie = dgvData.Rows[iRow].Cells["serialNumber"].Value.ToString(),
                        color = dgvData.Rows[iRow].Cells["color"]?.Value?.ToString() ?? "", // Maneja posibles nulos
                        modelo = dgvData.Rows[iRow].Cells["modelo"]?.Value?.ToString() ?? "",
                        marca = dgvData.Rows[iRow].Cells["marca"]?.Value?.ToString() ?? "",
                        nombre = dgvData.Rows[iRow].Cells["nombre"]?.Value?.ToString() ?? "",
                        // Puedes agregar más campos si es necesario
                    };

                    // Agregar el objeto ProductoDetalle a la lista si no está ya en ella
                    if (!ListaProductoDetalles.Any(p => p.idProductoDetalle == productoDetalle.idProductoDetalle))
                    {
                        ListaProductoDetalles.Add(productoDetalle);
                    }

                    // Guardar el SerialNumber seleccionado
                    this.SerialNumber = productoDetalle.numeroSerie;
                    // Aquí puedes cerrar el modal o realizar otras acciones
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // Manejar el caso en que alguna de las celdas necesarias sea nula
                    MessageBox.Show("Faltan datos para agregar el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();

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
    }

}
