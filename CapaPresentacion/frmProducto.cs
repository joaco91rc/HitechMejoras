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
        private Image defaultImage = Properties.Resources.CHECK;

        public decimal cotizacionActiva { get; set; } = new CN_Cotizacion().CotizacionActiva().importe;
        public frmProducto(Usuario usuario)
        {
            usuarioActual = usuario;
            InitializeComponent();
        }




        private void frmProducto_Load(object sender, EventArgs e)
        {
            if (usuarioActual.oRol.idRol == 1 || usuarioActual.oRol.idRol == 3)
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

            CargarGrilla();




        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            // Validación de campos obligatorios
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("El campo 'Código' es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El campo 'Nombre' es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboCategoria.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una categoría.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Producto objProducto = new Producto()
            {
                idProducto = Convert.ToInt32(txtIdProducto.Text),
                codigo = txtCodigo.Text,
                nombre = txtNombre.Text,
                descripcion = txtDescripcion.Text,

                oCategoria = new Categoria { idCategoria = Convert.ToInt32(((OpcionCombo)cboCategoria.SelectedItem).Valor) },
                estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false,

                
                
            };

            // Variables para almacenar los valores de los NumericUpDown
            decimal precioCompra = txtPrecioCompra.Value;
            decimal precioVenta = txtPrecioVenta.Value;
            decimal costoPesos = txtCostoPesos.Value;
            decimal ventaPesos = txtVentaPesos.Value;

            // Caso 1: Si el usuario ingresó precioCompra y precioVenta
            if (precioCompra > 0 && precioVenta > 0)
            {
                txtCostoPesos.Value = (Math.Round(precioCompra * cotizacionActiva,2)/500)*500;
                txtVentaPesos.Value = (Math.Round(precioVenta * cotizacionActiva,2)/500)*500;
                txtPrecioCompra.Value = precioCompra;
                txtPrecioVenta.Value = precioVenta;
            }
            // Caso 2: Si el usuario ingresó costoPesos y precioVenta
            else if (costoPesos > 0 && precioVenta > 0)
            {
                txtPrecioCompra.Value = costoPesos / cotizacionActiva;
                txtVentaPesos.Value = (Math.Round(precioVenta * cotizacionActiva,2)/500)*500;
                txtCostoPesos.Value = costoPesos;
                txtPrecioVenta.Value = precioVenta;
            }
            // Caso 3: Si el usuario ingresó costoPesos y ventaPesos
            else if (costoPesos > 0 && ventaPesos > 0)
            {
                txtPrecioCompra.Value = costoPesos / cotizacionActiva;
                txtPrecioVenta.Value = ventaPesos / cotizacionActiva;
                txtCostoPesos.Value = costoPesos;
                txtVentaPesos.Value = ventaPesos;
            }
            else
            {
                // Si no se cumplen las condiciones, mostrar un mensaje de error
                MessageBox.Show("Por favor, complete los campos requeridos correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            objProducto.precioCompra = txtPrecioCompra.Value;
            objProducto.precioVenta = txtPrecioVenta.Value;
            objProducto.costoPesos = txtCostoPesos.Value;
            objProducto.ventaPesos = txtVentaPesos.Value;

            // Aquí podrías agregar cualquier lógica adicional que necesites para guardar los datos en la base de datos o procesarlos.




            //if (checkCostoPesos.Checked)
            //{
            //    objProducto.costoPesos = Convert.ToDecimal(txtCostoPesos.Text);
            //    objProducto.precioCompra = (Math.Round(Convert.ToDecimal(txtCostoPesos.Text) / cotizacionActiva, 2)/ 500)*500; 
            //    objProducto.ventaPesos = Convert.ToDecimal(txtVentaPesos.Text);
            //    objProducto.precioVenta = (Math.Round(Convert.ToDecimal(txtVentaPesos.Text) / cotizacionActiva, 2)/500)*500;

            //}
            //else
            //{
            //    objProducto.costoPesos = Convert.ToDecimal(txtPrecioCompra.Text)*cotizacionActiva; 
            //    objProducto.ventaPesos = Convert.ToDecimal(txtPrecioVenta.Text) * cotizacionActiva;
            //    objProducto.precioCompra = Convert.ToDecimal(txtPrecioCompra.Text);
            //    objProducto.precioVenta = Convert.ToDecimal(txtPrecioVenta.Text);
            //}

            if (checkSerializable.Checked)
            {
                objProducto.prodSerializable = true;
            } else
            {
                objProducto.prodSerializable = false;
            }
            bool esSerializable = objProducto.prodSerializable;
            if (objProducto.idProducto == 0)
            {

                int idProductoGenerado = new CN_Producto().Registrar(objProducto, out mensaje);

                decimal precioCompra2 = objProducto.precioCompra;
                if (idProductoGenerado != 0)
                {
                    int stockProducto = new CN_ProductoNegocio().ObtenerStockProductoEnSucursal(idProductoGenerado, GlobalSettings.SucursalId);
                    
                    dgvData.Rows.Add(new object[] { defaultImage,
                        idProductoGenerado,
                        txtCodigo.Text,
                        txtNombre.Text,
                        txtDescripcion.Text,

                ((OpcionCombo)cboCategoria.SelectedItem).Valor.ToString(),
                ((OpcionCombo)cboCategoria.SelectedItem).Texto.ToString(),
                stockProducto,
                 precioCompra2.ToString("0.00"),
                 Convert.ToDecimal(txtCostoPesos.Text).ToString("0.00"),
                 Convert.ToDecimal(txtVentaPesos.Text).ToString("0.00"),
                 Convert.ToDecimal(txtPrecioVenta.Text).ToString("0.00"),
                 (Convert.ToDecimal(txtPrecioVenta.Text)*cotizacionActiva).ToString("0.00"),
                 esSerializable == true?"SI":"NO",
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
                    row.Cells["idProducto"].Value = objProducto.idProducto;
                    row.Cells["codigo"].Value = objProducto.codigo;
                    row.Cells["nombre"].Value = objProducto.nombre;
                    row.Cells["descripcion"].Value = objProducto.descripcion;

                    row.Cells["idCategoria"].Value = ((OpcionCombo)cboCategoria.SelectedItem).Valor.ToString();
                    row.Cells["categoria"].Value = ((OpcionCombo)cboCategoria.SelectedItem).Texto.ToString();
                    row.Cells["estadoValor"].Value = ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString();
                    row.Cells["estado"].Value = ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString();
                    row.Cells["stock"].Value = stockProducto;

                    row.Cells["precioCompra"].Value = objProducto.precioCompra.ToString("0.00"); // Formato 0.00
                    row.Cells["costoPesos"].Value = objProducto.costoPesos.ToString("0.00"); // Asegura formato 0.00
                    row.Cells["ventaPesos"].Value = objProducto.ventaPesos.ToString("0.00"); // Asegura formato 0.00
                    row.Cells["precioVenta"].Value = objProducto.precioVenta.ToString("0.00"); // Asegura formato 0.00
                    row.Cells["precioPesos"].Value = objProducto.ventaPesos.ToString("0.00"); // Asegura formato 0.00
                    row.Cells["prodSerializable"].Value = esSerializable == true ? "SI" : "NO";

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
            txtVentaPesos.Text = string.Empty;
            cboCategoria.SelectedIndex = 0;
            cboEstado.SelectedIndex = 0;
            txtCodigo.Select();
            txtProductoSeleccionado.Text = "Ninguno";
            txtCostoPesos.Text = string.Empty;
            checkSerializable.Checked = false;
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
                    txtVentaPesos.Text= dgvData.Rows[indice].Cells["ventaPesos"].Value.ToString();
                    txtPrecioCompra.Text = dgvData.Rows[indice].Cells["precioCompra"].Value.ToString();
                    txtPrecioVenta.Text = dgvData.Rows[indice].Cells["precioVenta"].Value.ToString();
                    txtCostoPesos.Text = dgvData.Rows[indice].Cells["costoPesos"].Value.ToString();
                    if(dgvData.Rows[indice].Cells["prodSerializable"].Value.ToString() == "SI")
                    {
                        checkSerializable.Checked = true;
                    }
                    else
                    {
                        checkSerializable.Checked= false;
                    }

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
                if (MessageBox.Show("Desea eliminar el Producto?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    int idProducto = Convert.ToInt32(txtIdProducto.Text);

                    bool respuesta = new CN_Producto().DarBajaLogica(idProducto, out mensaje);
                    if (respuesta)
                    {
                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));  // Eliminar la fila de la grilla
                        MessageBox.Show("Producto Eliminado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            else
            {
                DataTable dt = new DataTable();

                foreach (DataGridViewColumn columna in dgvData.Columns)
                {
                    if (columna.HeaderText != "" && columna.Visible)
                    {


                        dt.Columns.Add(columna.HeaderText, typeof(string));

                    }
                }

                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Visible)
                    {
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

        private void checkCostoPesos_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCostoPesos.Checked)
            {
                lblVentaPesos.Visible = true;
                lblCostoPesos.Visible = true;
                txtCostoPesos.Visible = true;
                txtVentaPesos.Visible = true;
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
                lblVentaPesos.Visible = false;
                txtVentaPesos.Visible = false;
                lblCostoPesos.Visible = false;
                txtCostoPesos.Visible = false;
                lblPrecioCompra.Visible = true;
                txtPrecioCompra.Visible = true;
                txtPrecioCompra.Select();

            }
        }

        private void CargarGrilla()
        {
            dgvData.Rows.Clear();
            //Mostrar todos los Productos
            List<Producto> listaProducto = new CN_Producto().Listar(GlobalSettings.SucursalId);

            foreach (Producto item in listaProducto)
            {
                dgvData.Rows.Add(new object[] {
                defaultImage, // Asignar la imagen predeterminada
                item.idProducto,
                item.codigo,
                item.nombre,
                item.descripcion,
                item.oCategoria.idCategoria,
                item.oCategoria.descripcion,
                item.stock,
                item.precioCompra,
                item.costoPesos,
                item.ventaPesos,
                item.precioVenta,
                (Math.Ceiling((item.precioVenta * cotizacionActiva) / 500) * 500).ToString("0.00"),
                item.prodSerializable == true? "SI":"NO",
                item.estado == true ? 1 : 0,
                item.estado == true ? "Activo" : "No Activo"
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
                dgvData.Rows.Add(new object[] {
                defaultImage, // Asignar la imagen predeterminada
                item.idProducto,
                item.codigo,
                item.nombre,
                item.descripcion,
                item.oCategoria.idCategoria,
                item.oCategoria.descripcion,
                item.stock,
                item.precioCompra,
                item.costoPesos,
                item.ventaPesos,
                item.precioVenta,
                (Math.Ceiling((item.precioVenta * cotizacionActiva) / 500) * 500).ToString("0.00"),
                item.prodSerializable == true? "SI":"NO",
                item.estado == true ? 1 : 0,
                item.estado == true ? "Activo" : "No Activo"
                });
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

        private void btnSetearPrecios_Click(object sender, EventArgs e)
        {
            txtPrecioCompra.Value = 0;
            txtPrecioVenta.Value = 0;
            txtCostoPesos.Value = 0;
            txtVentaPesos.Value = 0;

        }
    }
}