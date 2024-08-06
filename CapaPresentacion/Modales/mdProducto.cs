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
    public partial class mdProducto : Form
    {
        public Producto _Producto { get; set; }

        public decimal cotizacionActiva { get; set; } = new CN_Cotizacion().CotizacionActiva().importe;
        public mdProducto()
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

        private void CargarListadoProductos()
        {
            List<Producto> listaProducto = new CN_Producto().Listar(GlobalSettings.SucursalId);

            foreach (Producto item in listaProducto)
            {
                
                
                    dgvData.Rows.Add(new object[] {
                item.idProducto,
                item.codigo,
                item.nombre,
                item.oCategoria.descripcion,
                item.stock,
                item.precioCompra,
                item.precioVenta,
            });
                
            }
        }
        private void ActualizarListadoProductos()
        {
            dgvData.Rows.Clear(); // Limpiar la grilla
            CargarListadoProductos(); // Volver a cargar los productos
        }

        private void mdProducto_Load(object sender, EventArgs e)
        {
            txtBusqueda.Select();
            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {

                if (columna.Visible == true )
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });

                }


            }

            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 1;

            List<Categoria> listaCategoria = new CN_Categoria().Listar();

            foreach (Categoria item in listaCategoria)
            {
                cboCategoria.Items.Add(new OpcionCombo() { Valor = item.idCategoria, Texto = item.descripcion });
            }
            cboCategoria.DisplayMember = "Texto";
            cboCategoria.ValueMember = "Valor";
            cboCategoria.SelectedIndex = 0;
            CargarListadoProductos();

        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            int iRow = e.RowIndex;
            int iColumn = e.ColumnIndex;
            if (iRow >= 0 && iColumn > 0)
            {
                _Producto = new Producto()
                {
                    idProducto = Convert.ToInt32(dgvData.Rows[iRow].Cells["idProducto"].Value.ToString()),
                    codigo = dgvData.Rows[iRow].Cells["codigo"].Value.ToString(),
                    nombre = dgvData.Rows[iRow].Cells["nombre"].Value.ToString(),
                    
                    //stock = Convert.ToInt32(dgvData.Rows[iRow].Cells["stock"].Value.ToString()),
                    precioCompra = Convert.ToDecimal(dgvData.Rows[iRow].Cells["precioCompra"].Value.ToString()),
                    precioVenta = Convert.ToDecimal(dgvData.Rows[iRow].Cells["precioVenta"].Value.ToString())
                };
                
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void  MostrarControles()
        {
            lblCodigo.Visible = true;
            txtCodigo.Visible = true;
            lblNombre.Visible = true;
            txtNombre.Visible = true;
            lblDescripcion.Visible = true;
            txtDescripcion.Visible = true;
            lblCategoria.Visible = true;
            cboCategoria.Visible = true;
            lblEstado.Visible = true;
            cboEstado.Visible = true;
            lblPrecioCompra.Visible = true;
            txtPrecioCompra.Visible = true;
            lblPrecioVenta.Visible = true;
            txtPrecioVenta.Visible = true;
            checkCostoPesos.Visible = true;
            checkCategoria.Visible = true;
            
        }
        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            MostrarControles();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            int idCategoriaProducto = 0;

            if (checkCategoria.Checked)
            {
                Categoria objCategoria = new Categoria()
                {
                    idCategoria = Convert.ToInt32(txtIdCategoria.Text),
                    descripcion = txtCategoria.Text,
                    estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false
                };

                if (objCategoria.idCategoria == 0)
                {
                    int idCategoriaGenerado = new CN_Categoria().Registrar(objCategoria, out mensaje);
                    if (idCategoriaGenerado != 0)
                    {
                        txtIdCategoria.Text = idCategoriaGenerado.ToString();
                        idCategoriaProducto = idCategoriaGenerado;
                    }
                }
                else
                {
                    idCategoriaProducto = objCategoria.idCategoria;
                }
            }
            else
            {
                idCategoriaProducto = Convert.ToInt32(((OpcionCombo)cboCategoria.SelectedItem).Valor);
            }

            Producto objProducto = new Producto()
            {
                idProducto = Convert.ToInt32(txtIdProducto.Text),
                codigo = txtCodigo.Text,
                nombre = txtNombre.Text,
                descripcion = txtDescripcion.Text,
                oCategoria = new Categoria { idCategoria = idCategoriaProducto },
                estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false,
                precioCompra = string.IsNullOrWhiteSpace(txtPrecioCompra.Text) ? 0 : Convert.ToDecimal(txtPrecioCompra.Text),
                precioVenta = string.IsNullOrWhiteSpace(txtPrecioVenta.Text) ? 0 : Convert.ToDecimal(txtPrecioVenta.Text),
            };

            if (checkCostoPesos.Checked)
            {
                objProducto.costoPesos = Convert.ToDecimal(txtCostoPesos.Text);
                objProducto.precioCompra = Convert.ToDecimal(txtCostoPesos.Text) / cotizacionActiva;


            }
            else
            {
                objProducto.costoPesos = 0;
                txtCostoPesos.Text = "0";
            }
            if (objProducto.idProducto == 0)
            {

                int idProductoGenerado = new CN_Producto().Registrar(objProducto, out mensaje);

                decimal precioCompra = objProducto.precioCompra;
                if (idProductoGenerado != 0)
                {
                    int stockProducto = new CN_ProductoNegocio().ObtenerStockProductoEnSucursal(idProductoGenerado, GlobalSettings.SucursalId);

            //        dgvData.Rows.Add(new object[] { "",
            //            idProductoGenerado,
            //            txtCodigo.Text,
            //            txtNombre.Text,
            //            txtDescripcion.Text,

            //    ((OpcionCombo)cboCategoria.SelectedItem).Valor.ToString(),
            //    ((OpcionCombo)cboCategoria.SelectedItem).Texto.ToString(),
            //    stockProducto,
            //     precioCompra,
            //     Convert.ToDecimal(txtCostoPesos.Text),
            //     Convert.ToDecimal(txtPrecioVenta.Text),
            //     Convert.ToDecimal(txtPrecioVenta.Text)*cotizacionActiva,
            //    ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString(),
            //    ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString()

            //});
                    Limpiar();
                    ActualizarListadoProductos();
                }
                else
                {

                    MessageBox.Show(mensaje);
                }


            }
            else
            {

                bool resultado = new CN_Producto().Editar(objProducto, out mensaje);
                int stockProducto = new CN_ProductoNegocio().ObtenerStockProductoEnSucursal(objProducto.idProducto, GlobalSettings.SucursalId);
                if (resultado)
                {
                    //DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    //row.Cells["idProducto"].Value = txtIdProducto.Text;
                    //row.Cells["codigo"].Value = txtCodigo.Text;
                    //row.Cells["nombre"].Value = txtNombre.Text;
                    //row.Cells["descripcion"].Value = txtDescripcion.Text;

                    //row.Cells["idCategoria"].Value = ((OpcionCombo)cboCategoria.SelectedItem).Valor.ToString();
                    //row.Cells["categoria"].Value = ((OpcionCombo)cboCategoria.SelectedItem).Texto.ToString();
                    //row.Cells["estadoValor"].Value = ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString();
                    //row.Cells["estado"].Value = ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString();
                    //row.Cells["stock"].Value = stockProducto;

                    //row.Cells["precioCompra"].Value = objProducto.precioCompra;
                    //row.Cells["costoPesos"].Value = txtCostoPesos.Text;
                    //row.Cells["precioVenta"].Value = txtPrecioVenta.Text;
                    //row.Cells["precioPesos"].Value = (Convert.ToDecimal(row.Cells["precioVenta"].Value) * cotizacionActiva).ToString();

                    Limpiar();
                    ActualizarListadoProductos();
                }
                else
                {

                    MessageBox.Show(mensaje);
                }

            }
        }

        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtIdProducto.Text = "0";
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtCostoPesos.Visible = false;
            checkCostoPesos.Checked = false;
            txtPrecioCompra.Visible = true;
            txtPrecioCompra.Text = "";
            txtPrecioVenta.Text = "";

            cboCategoria.SelectedIndex = 0;
            cboEstado.SelectedIndex = 0;
            txtCodigo.Select();
            
            txtCostoPesos.Text = string.Empty;
        }

        private void checkCategoria_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCategoria.Checked)
            {
                cboCategoria.Visible = false;
                txtCategoria.Visible = true;
                cboCategoria.SelectedIndex = -1;
            }
            else
            {
                cboCategoria.Visible = true;
                txtCategoria.Visible = false;
                cboCategoria.SelectedIndex = 0;
            }
        }

        private void checkCostoPesos_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCostoPesos.Checked)
            {
                lblCostoPesos.Visible = true;
                txtCostoPesos.Visible = true;
                lblPrecioCompra.Visible = false;
                txtPrecioCompra.Visible = false;
                if (txtIdProducto.Text == "0")
                {
                    txtPrecioCompra.Text = "0";
                }
                txtCostoPesos.Select();
            }
            else
            {
                lblCostoPesos.Visible = false;
                txtCostoPesos.Visible = false;
                lblPrecioCompra.Visible = true;
                txtPrecioCompra.Visible = true;
                txtPrecioCompra.Select();

            }
        }
    }
}
