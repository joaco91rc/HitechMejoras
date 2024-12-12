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
    public partial class mdSeleccionProductosSerializables : Form
    {
        public Producto _Producto { get; set; }
        private Form _parentForm;  // Variable para almacenar el formulario que llama (frmVentas o frmDetalleProducto)
        public mdSeleccionProductosSerializables(Form parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
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
        private void CargarListadoProductosSerializables()
        {
            dgvData.Rows.Clear();
            List<Producto> listaProducto = new CN_Producto().ListarSerializables(GlobalSettings.SucursalId);

            foreach (Producto item in listaProducto)
            {


                dgvData.Rows.Add(new object[] {
                item.idProducto,
                item.codigo,
                item.nombre,
                item.oCategoria.descripcion,
                item.stock,


                item.prodSerializable,
                item.precioCompra,
                item.precioVenta,
                item.nombreLocal

            });

            }
        }

        private void mdSeleccionProductosSerializables_Load(object sender, EventArgs e)
        {
            CargarListadoProductosSerializables();
            CargarComboBusqueda();
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

        private void dgvData_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var negocioNombres = new Dictionary<string, int>
    {
        { "HITECH 1", 1 },
        { "HITECH 2", 2 },
        { "APPLE 49", 3 },
        { "APPLE CAFÉ", 4 }
    };

            int iRow = e.RowIndex;
            int iColumn = e.ColumnIndex;

            if (iRow >= 0 && iColumn > 0)
            {
                // Obtener el nombre del local desde el DataGridView
                string nombreLocalProducto = dgvData.Rows[iRow].Cells["nombreLocal"].Value.ToString();

                // Obtener el idNegocio del nombre del local
                if (!negocioNombres.TryGetValue(nombreLocalProducto, out int idNegocioProducto))
                {
                    MessageBox.Show("El local del producto no está registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Salir del método si el local no está registrado
                }

                // Validar si el local del producto es diferente al local logueado
                if (idNegocioProducto != GlobalSettings.SucursalId)
                {
                    MessageBox.Show($"El producto que desea serializar se encuentra en un local diferente del que está logueado.",
                                    "Advertencia",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return; // Salir del método si los locales son diferentes
                }

                // Leer el stock directamente desde el DataGridView
                int stockProducto = Convert.ToInt32(dgvData.Rows[iRow].Cells["stock"].Value.ToString());

                // Validar si el stock es 0
                if (stockProducto <= 0)
                {
                    MessageBox.Show("No se puede serializar el producto porque el stock es 0. Por favor, cargue el stock primero.",
                                    "Advertencia",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return; // Salir del método si el stock es cero
                }

                _Producto = new Producto()
                {
                    idProducto = Convert.ToInt32(dgvData.Rows[iRow].Cells["idProducto"].Value.ToString()),
                    codigo = dgvData.Rows[iRow].Cells["codigo"].Value.ToString(),
                    nombre = dgvData.Rows[iRow].Cells["nombre"].Value.ToString(),
                    precioCompra = Convert.ToDecimal(dgvData.Rows[iRow].Cells["precioCompra"].Value.ToString()),
                    precioVenta = Convert.ToDecimal(dgvData.Rows[iRow].Cells["precioVenta"].Value.ToString()),
                    prodSerializable = Convert.ToBoolean(dgvData.Rows[iRow].Cells["prodSerializable"].Value.ToString()),
                    nombreLocal = nombreLocalProducto
                };

                // Actualizar stock en el formulario padre
                if (_parentForm is frmVentas frmVentas)
                {
                    frmVentas.StockProducto = stockProducto;
                    frmVentas.ActualizarStock();
                }
                else if (_parentForm is frmDetalleProducto frmDetalleProducto)
                {
                    frmDetalleProducto.StockProducto = stockProducto;
                    frmDetalleProducto.ActualizarStock();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }


        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
