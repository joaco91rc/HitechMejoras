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
    public partial class mdTraspasoStock : Form
    {
        public mdTraspasoStock()
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

        private void mdTraspasoStock_Load(object sender, EventArgs e)
        {
         
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
                    item.precioCompra,
                    item.precioVenta,

                    });
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgvData.Columns[e.ColumnIndex].Name == "btnTraspasar")
            {
                // Obtener la fila seleccionada
                DataGridViewRow selectedRow = dgvData.Rows[e.RowIndex];

                // Obtener los valores de las celdas de la fila seleccionada
                int idProducto = Convert.ToInt32(selectedRow.Cells["idProducto"].Value);
               
                string nombre = selectedRow.Cells["nombre"].Value.ToString();
              

                // Crear una instancia del formulario modal y pasar los datos
                mdTraspasoASucursal md = new mdTraspasoASucursal(idProducto, nombre);
                md.ShowDialog();
                this.Close();
            }
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (e.RowIndex < 0)
                return;

            int traspasarColumnIndex = dgvData.Columns["btnTraspasar"].Index;

            if (e.ColumnIndex == traspasarColumnIndex)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.traspasar.Width;
                var h = Properties.Resources.traspasar.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;
                e.Graphics.DrawImage(Properties.Resources.traspasar, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }
    }
}
