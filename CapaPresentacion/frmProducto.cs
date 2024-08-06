using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using ClosedXML.Excel;
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
    public partial class frmProducto : Form
    {
        private Usuario usuarioActual;

        
        public decimal cotizacionActiva { get; set; } = new CN_Cotizacion().CotizacionActiva().importe;
        public frmProducto(Usuario usuario)
        {
            usuarioActual = usuario;
            InitializeComponent();
        }




        private void frmProducto_Load(object sender, EventArgs e)
        {
            if(usuarioActual.oRol.idRol == 1 || usuarioActual.oRol.idRol == 3)
            {

                txtPrecioCompra.Visible = true;
                txtPrecioVenta.Visible = true;
                lblPrecioCompra.Visible = true;
                lblPrecioVenta.Visible = true;
            }
            else
            {
                txtPrecioCompra.Visible = true;
                txtPrecioVenta.Visible = true;
                lblPrecioCompra.Visible = true;
                lblPrecioVenta.Visible = true;
                txtPrecioCompra.Text = "0";
                txtPrecioVenta.Text = "0";

            }
            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;

            List<Categoria> listaCategoria = new CN_Categoria().Listar();

            foreach (Categoria item in listaCategoria)
            {
                cboCategoria.Items.Add(new OpcionCombo() { Valor = item.idCategoria, Texto = item.descripcion });
            }
            cboCategoria.DisplayMember = "Texto";
            cboCategoria.ValueMember = "Valor";
            cboCategoria.SelectedIndex = 0;

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

            //Mostrar todos los Productos
            List<Producto> listaProducto = new CN_Producto().Listar(GlobalSettings.SucursalId);
            
            foreach (Producto item in listaProducto)
            {
                //int stockProducto = new CN_ProductoNegocio().ObtenerStockProductoEnSucursal(item.idProducto, GlobalSettings.SucursalId);
                
                dgvData.Rows.Add(new object[] { "",item.idProducto,
                    item.codigo,
                    item.nombre,
                    item.descripcion,
                    item.oCategoria.idCategoria,
                    item.oCategoria.descripcion,
                    item.stock,
                    item.precioCompra,
                    item.costoPesos,
                    item.precioVenta,
                    (item.precioVenta*cotizacionActiva).ToString("0.00"),
                    item.estado==true?1:0,
                    item.estado==true? "Activo": "No Activo"
                    });
            }




        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            Producto objProducto = new Producto()
            {
                idProducto = Convert.ToInt32(txtIdProducto.Text),
                codigo = txtCodigo.Text,
                nombre = txtNombre.Text,
                descripcion = txtDescripcion.Text,
                
                oCategoria = new Categoria { idCategoria = Convert.ToInt32(((OpcionCombo)cboCategoria.SelectedItem).Valor) },
                estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false,
               
                precioCompra = Convert.ToDecimal(txtPrecioCompra.Text),
                precioVenta = Convert.ToDecimal(txtPrecioVenta.Text),
            };
           
            if (checkCostoPesos.Checked)
            {
                objProducto.costoPesos = Convert.ToDecimal(txtCostoPesos.Text);
                objProducto.precioCompra = Convert.ToDecimal(txtCostoPesos.Text)/ cotizacionActiva;
                

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

                    dgvData.Rows.Add(new object[] { "",
                        idProductoGenerado,
                        txtCodigo.Text,
                        txtNombre.Text,
                        txtDescripcion.Text,

                ((OpcionCombo)cboCategoria.SelectedItem).Valor.ToString(),
                ((OpcionCombo)cboCategoria.SelectedItem).Texto.ToString(),
                stockProducto,
                 precioCompra,
                 Convert.ToDecimal(txtCostoPesos.Text),
                 Convert.ToDecimal(txtPrecioVenta.Text),
                 Convert.ToDecimal(txtPrecioVenta.Text)*cotizacionActiva,
                ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString(),
                ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString()
                
            });
                    Limpiar();
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
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["idProducto"].Value = txtIdProducto.Text;
                    row.Cells["codigo"].Value = txtCodigo.Text;
                    row.Cells["nombre"].Value = txtNombre.Text;
                    row.Cells["descripcion"].Value = txtDescripcion.Text;
                    
                    row.Cells["idCategoria"].Value = ((OpcionCombo)cboCategoria.SelectedItem).Valor.ToString();
                    row.Cells["categoria"].Value = ((OpcionCombo)cboCategoria.SelectedItem).Texto.ToString();
                    row.Cells["estadoValor"].Value = ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString();
                    row.Cells["estado"].Value = ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString();
                    row.Cells["stock"].Value = stockProducto;
                    
                    row.Cells["precioCompra"].Value = objProducto.precioCompra;
                    row.Cells["costoPesos"].Value = txtCostoPesos.Text;
                    row.Cells["precioVenta"].Value = txtPrecioVenta.Text;
                    row.Cells["precioPesos"].Value = (Convert.ToDecimal(row.Cells["precioVenta"].Value) * cotizacionActiva).ToString();

                    Limpiar();

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
            txtProductoSeleccionado.Text = "Ninguno";
            txtCostoPesos.Text = string.Empty;
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex == 0)
            {

                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.check20.Width;
                var h = Properties.Resources.check20.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Width - h) / 2;
                e.Graphics.DrawImage(Properties.Resources.check20, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtProductoSeleccionado.Text = dgvData.Rows[indice].Cells["nombre"].Value.ToString();
                    txtIdProducto.Text = dgvData.Rows[indice].Cells["idProducto"].Value.ToString();
                    txtCodigo.Text = dgvData.Rows[indice].Cells["codigo"].Value.ToString();
                    txtNombre.Text = dgvData.Rows[indice].Cells["nombre"].Value.ToString();
                    txtDescripcion.Text = dgvData.Rows[indice].Cells["descripcion"].Value.ToString();
                    
                    txtPrecioCompra.Text = dgvData.Rows[indice].Cells["precioCompra"].Value.ToString();
                    txtPrecioVenta.Text = dgvData.Rows[indice].Cells["precioVenta"].Value.ToString();
                    txtCostoPesos.Text = dgvData.Rows[indice].Cells["costoPesos"].Value.ToString();

                    foreach (OpcionCombo oc in cboCategoria.Items)
                    {

                        if (Convert.ToInt32(oc.Valor) == (Convert.ToInt32(dgvData.Rows[indice].Cells["idCategoria"].Value)))
                        {
                            int indiceCombo = cboCategoria.Items.IndexOf(oc);
                            cboCategoria.SelectedIndex = indiceCombo;
                            break;

                        }

                    }

                    foreach (OpcionCombo oc in cboEstado.Items)
                    {

                        if (Convert.ToInt32(oc.Valor) == (Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value)))
                        {
                            int indiceCombo = cboEstado.Items.IndexOf(oc);
                            cboEstado.SelectedIndex = indiceCombo;
                            break;

                        }

                    }

                }

            }
        }

        private void btnLimpiarDatos_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtIdProducto.Text) != 0)
            {

                if (MessageBox.Show("Desea eliminar el Producto?", "Confirmar Eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Producto objProducto = new Producto()
                    {
                        idProducto = Convert.ToInt32(txtIdProducto.Text),

                    };

                    bool respuesta = new CN_Producto().Eliminar(objProducto, out mensaje);
                    if (respuesta)
                    {

                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                    }

                    else
                    {

                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


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

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para Exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else {
                DataTable dt = new DataTable();

                foreach (DataGridViewColumn columna in dgvData.Columns) { 
                    if(columna.HeaderText!= "" && columna.Visible)
                    {


                        dt.Columns.Add(columna.HeaderText, typeof(string));

                    }
                }

                foreach (DataGridViewRow row in dgvData.Rows) {
                    if (row.Visible) {
                        dt.Rows.Add(new object[]
                        {
                            row.Cells[2].Value.ToString(),
                            row.Cells[3].Value.ToString(),
                            row.Cells[4].Value.ToString(),
                            row.Cells[6].Value.ToString(),
                            row.Cells[7].Value.ToString(),
                            row.Cells[8].Value.ToString(),
                            row.Cells[9].Value.ToString(),
                            row.Cells[11].Value.ToString(),
                            

                        }); 
                    }
                    


                }
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.FileName = string.Format("ReporteProducto_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
                saveFile.Filter = "Excel Files | *.xlsx";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Informe Productos");
                        hoja.ColumnsUsed().AdjustToContents();
                        wb.SaveAs(saveFile.FileName);
                        MessageBox.Show("Planilla Exportada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch
                    {
                        MessageBox.Show("Error al generar la Planilla de Excel", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

            }
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
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
