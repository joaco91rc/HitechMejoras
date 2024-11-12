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
        private DataTable dtProductos;
        private DataTable dtProductosOriginal;
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

            

            

            //CargarGrilla();
            dtProductos = new CN_Producto().ListarProductos(GlobalSettings.SucursalId);
            dtProductosOriginal = dtProductos;
            CargarDataGridView(dtProductos);
            foreach (DataColumn columna in dtProductosOriginal.Columns)
            {
                // Agrega cada columna del DataTable al ComboBox, excepto "btnSeleccionar", "ProductoId" y "idCategoria"
                if (columna.ColumnName != "btnSeleccionar" && columna.ColumnName != "ProductoId" && columna.ColumnName != "idCategoria")
                {
                    cboBusqueda.Items.Add(new OpcionCombo()
                    {
                        Valor = columna.ColumnName,
                        Texto = columna.ColumnName.ToUpper() // Convierte el texto a mayúsculas
                    });
                }
            }

            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 1;





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

            // Crear el objeto Producto
            Producto objProducto = new Producto()
            {
                idProducto = Convert.ToInt32(txtIdProducto.Text),
                codigo = txtCodigo.Text,
                nombre = txtNombre.Text,
                descripcion = txtDescripcion.Text,
                oCategoria = new Categoria { idCategoria = Convert.ToInt32(((OpcionCombo)cboCategoria.SelectedItem).Valor) },
                estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false,
                prodSerializable = checkSerializable.Checked
            };

            // Lógica de precios y costos
            decimal precioCompra = txtPrecioCompra.Value;
            decimal precioVenta = txtPrecioVenta.Value;
            decimal costoPesos = txtCostoPesos.Value;
            decimal ventaPesos = txtVentaPesos.Value;
            decimal cotizacionActiva = 1.0m; // Asumiendo un valor predeterminado

            if (precioCompra > 0 && precioVenta > 0)
            {
                txtCostoPesos.Value = (Math.Round(precioCompra * cotizacionActiva, 2) / 500) * 500;
                txtVentaPesos.Value = (Math.Round(precioVenta * cotizacionActiva, 2) / 500) * 500;
            }
            else if (costoPesos > 0 && precioVenta > 0)
            {
                txtPrecioCompra.Value = costoPesos / cotizacionActiva;
                txtVentaPesos.Value = (Math.Round(precioVenta * cotizacionActiva, 2) / 500) * 500;
            }
            else if (costoPesos > 0 && ventaPesos > 0)
            {
                txtPrecioCompra.Value = costoPesos / cotizacionActiva;
                txtPrecioVenta.Value = ventaPesos / cotizacionActiva;
            }
            else
            {
                MessageBox.Show("Por favor, complete los campos requeridos correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Asignar precios y costos
            objProducto.precioCompra = txtPrecioCompra.Value;
            objProducto.precioVenta = txtPrecioVenta.Value;
            objProducto.costoPesos = txtCostoPesos.Value;
            objProducto.ventaPesos = txtVentaPesos.Value;

            // Guardar o actualizar en base de datos
            if (objProducto.idProducto == 0) // Nuevo producto
            {
                int idProductoGenerado = new CN_Producto().Registrar(objProducto, out mensaje);
                if (idProductoGenerado != 0)
                {
                    objProducto.idProducto = idProductoGenerado;
                    AgregarProductoAlDataTable(objProducto);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            else // Actualizar producto existente
            {
                bool resultado = new CN_Producto().Editar(objProducto, out mensaje);
                if (resultado)
                {
                    ActualizarProductoEnDataTable(objProducto);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
        }

        private void AgregarProductoAlDataTable(Producto producto)
        {
            DataRow newRow = dtProductos.NewRow();
            newRow["ProductoId"] = producto.idProducto;
            newRow["codigo"] = producto.codigo;
            newRow["nombre"] = producto.nombre;
            newRow["descripcion"] = producto.descripcion;
            newRow["idCategoria"] = producto.oCategoria.idCategoria;
            newRow["DescripcionCategoria"] = ((OpcionCombo)cboCategoria.SelectedItem).Texto;
            newRow["stock"] = new CN_ProductoNegocio().ObtenerStockProductoEnSucursal(producto.idProducto, GlobalSettings.SucursalId);
            newRow["precioCompra"] = producto.precioCompra.ToString("0.00");
            newRow["costoPesos"] = producto.costoPesos.ToString("0.00");
            newRow["ventaPesos"] = producto.ventaPesos.ToString("0.00");
            newRow["precioVenta"] = producto.precioVenta.ToString("0.00");

            // Establecer prodSerializable como "SI" o "NO" según el valor booleano
            newRow["prodSerializable"] = producto.prodSerializable ? "SI" : "NO";

            // Establecer estado como "Activo" o "Inactivo" según la selección en el ComboBox
            newRow["estado"] = ((OpcionCombo)cboEstado.SelectedItem).Texto;

            dtProductos.Rows.Add(newRow);
        }

        private void ActualizarProductoEnDataTable(Producto producto)
        {
            foreach (DataRow row in dtProductos.Rows)
            {
                // Convertir ProductoId de la fila usando Convert.ToInt32 para evitar el error de conversión
                if (Convert.ToInt32(row["ProductoId"]) == producto.idProducto)
                {
                    row["codigo"] = producto.codigo;
                    row["nombre"] = producto.nombre;
                    row["descripcion"] = producto.descripcion;
                    row["idCategoria"] = producto.oCategoria.idCategoria;
                    row["DescripcionCategoria"] = ((OpcionCombo)cboCategoria.SelectedItem).Texto;
                    row["stock"] = new CN_ProductoNegocio().ObtenerStockProductoEnSucursal(producto.idProducto, GlobalSettings.SucursalId);
                    row["precioCompra"] = producto.precioCompra.ToString("0.00");
                    row["costoPesos"] = producto.costoPesos.ToString("0.00");
                    row["ventaPesos"] = producto.ventaPesos.ToString("0.00");
                    row["precioVenta"] = producto.precioVenta.ToString("0.00");

                    // Establecer prodSerializable como "SI" o "NO" según el valor booleano
                    row["prodSerializable"] = producto.prodSerializable ? "SI" : "NO";

                    // Establecer estado como "Activo" o "Inactivo" según la selección en el ComboBox
                    row["estado"] = ((OpcionCombo)cboEstado.SelectedItem).Texto;

                    break;
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
            // Verifica si la columna clicada es "btnSeleccionar"
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    // Asignar valores a los campos de texto desde la fila seleccionada
                    txtIndice.Text = indice.ToString();
                    txtProductoSeleccionado.Text = dgvData.Rows[indice].Cells["nombre"].Value.ToString();
                    txtIdProducto.Text = dgvData.Rows[indice].Cells["ProductoId"].Value.ToString();
                    txtCodigo.Text = dgvData.Rows[indice].Cells["codigo"].Value.ToString();
                    txtNombre.Text = dgvData.Rows[indice].Cells["nombre"].Value.ToString();
                    txtDescripcion.Text = dgvData.Rows[indice].Cells["descripcion"].Value.ToString();
                    txtVentaPesos.Text = dgvData.Rows[indice].Cells["ventaPesos"].Value.ToString();
                    txtPrecioCompra.Text = dgvData.Rows[indice].Cells["precioCompra"].Value.ToString();
                    txtPrecioVenta.Text = dgvData.Rows[indice].Cells["precioVenta"].Value.ToString();
                    txtCostoPesos.Text = dgvData.Rows[indice].Cells["costoPesos"].Value.ToString();

                    // Verificar si prodSerializable es "Sí" o "No" y marcar el CheckBox en consecuencia
                    checkSerializable.Checked = dgvData.Rows[indice].Cells["prodSerializable"].Value.ToString() == "SI";

                    // Seleccionar la categoría en el ComboBox
                    foreach (OpcionCombo oc in cboCategoria.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["idCategoria"].Value))
                        {
                            cboCategoria.SelectedIndex = cboCategoria.Items.IndexOf(oc);
                            break;
                        }
                    }

                    // Verificar el estado: si es "Activo" selecciona 1, si es "Inactivo" selecciona 0
                    string estado = dgvData.Rows[indice].Cells["estado"].Value.ToString();
                    foreach (OpcionCombo oc in cboEstado.Items)
                    {
                        if (estado == "Activo" && Convert.ToInt32(oc.Valor) == 1 ||
                            estado == "No Activo" && Convert.ToInt32(oc.Valor) == 0)
                        {
                            cboEstado.SelectedIndex = cboEstado.Items.IndexOf(oc);
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
            string filtroTexto = txtBusqueda.Text.Trim().ToUpper();

            // Verifica que el DataSource sea un DataTable
            if (dgvData.DataSource is DataTable dt)
            {
                // Crea un DataView a partir del DataTable
                DataView dv = dt.DefaultView;

                // Aplica el filtro en la columna seleccionada
                dv.RowFilter = string.Format("{0} LIKE '%{1}%'", columnaFiltro, filtroTexto);

                // Asigna el DataView filtrado al DataGridView
                dgvData.DataSource = dv;
            }
        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBusqueda.Clear(); // Limpia el cuadro de búsqueda

            // Solo vuelve a cargar el DataGridView si hay datos en dtProductosOriginal
            if (dtProductosOriginal != null)
            {
                CargarDataGridView(dtProductosOriginal);
            }
            else
            {
                MessageBox.Show("No se ha cargado ningún producto aún.");
            }
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
                            row.Cells[10].Value.ToString(),
                            row.Cells[11].Value.ToString(),
                            row.Cells[12].Value.ToString(),
                            row.Cells[13].Value.ToString(),
                            row.Cells[14].Value.ToString(),
                            row.Cells[15].Value.ToString()
                            

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
                string filtroTexto = txtBusqueda.Text.Trim().ToUpper();

                // Verifica que el DataSource sea un DataTable
                if (dgvData.DataSource is DataTable dt)
                {
                    // Crea un DataView a partir del DataTable
                    DataView dv = dt.DefaultView;

                    // Aplica el filtro en la columna seleccionada
                    dv.RowFilter = string.Format("{0} LIKE '%{1}%'", columnaFiltro, filtroTexto);

                    // Asigna el DataView filtrado al DataGridView
                    dgvData.DataSource = dv;
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

        private void CargarDataGridView(DataTable dtProductos)
        {
            dgvData.Columns.Clear(); // Limpiar columnas existentes

            // Guardar el DataTable en la variable global
            dtProductosOriginal = dtProductos.Copy();

            // Crear y configurar la columna de imagen
            DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
            imgColumn.Name = "btnSeleccionar";
            imgColumn.HeaderText = "";
            imgColumn.Image = Properties.Resources.CHECK; // Reemplaza con tu icono de selección
            imgColumn.ImageLayout = DataGridViewImageCellLayout.Stretch; // Ajustar la imagen
            dgvData.Columns.Add(imgColumn); // Agregar la columna de imagen al DataGridView

            // Asignar el DataSource al DataGridView
            dgvData.DataSource = dtProductos;

            // Renombrar encabezados de las columnas
            dgvData.Columns["ProductoId"].HeaderText = "Producto ID";
            dgvData.Columns["codigo"].HeaderText = "Código";
            dgvData.Columns["nombre"].HeaderText = "Producto";
            dgvData.Columns["descripcion"].HeaderText = "Descripción";
            dgvData.Columns["DescripcionCategoria"].HeaderText = "Categoría";
            dgvData.Columns["stock"].HeaderText = "Stock";
            dgvData.Columns["costoPesos"].HeaderText = "Costo $";
            dgvData.Columns["ventaPesos"].HeaderText = "Precio $";
            dgvData.Columns["precioCompra"].HeaderText = "Costo US$";
            dgvData.Columns["precioVenta"].HeaderText = "Precio US$";
            dgvData.Columns["prodSerializable"].HeaderText = "Es Serializable?";
            dgvData.Columns["fechaUltimaVenta"].HeaderText = "Fecha Act. Stock";
            dgvData.Columns["diasSinVenta"].HeaderText = "Días Sin Act. Stock";

            // Ocultar columnas que no deseas mostrar
            dgvData.Columns["idCategoria"].Visible = false;
            dgvData.Columns["ProductoId"].Visible = false;

            // Ajustes de tamaño para las columnas
            dgvData.Columns["nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvData.Columns["nombre"].MinimumWidth = 200;
            dgvData.Columns["btnSeleccionar"].Width = 30;
            dgvData.Columns["codigo"].Width = 80;
            dgvData.Columns["descripcion"].Width = 150;
            dgvData.Columns["DescripcionCategoria"].Width = 150;
            dgvData.Columns["stock"].Width = 80;
            dgvData.Columns["costoPesos"].Width = 100;
            dgvData.Columns["ventaPesos"].Width = 130;
            dgvData.Columns["precioCompra"].Width = 130;
            dgvData.Columns["precioVenta"].Width = 130;
            dgvData.Columns["prodSerializable"].Width = 130;
            dgvData.Columns["fechaUltimaVenta"].Width = 150;
            dgvData.Columns["diasSinVenta"].Width = 150;

            // Establecer el orden de las columnas usando DisplayIndex
            dgvData.Columns["btnSeleccionar"].DisplayIndex = 0; // Columna de imagen en primer lugar
            dgvData.Columns["codigo"].DisplayIndex = 1;
            dgvData.Columns["nombre"].DisplayIndex = 2;
            dgvData.Columns["descripcion"].DisplayIndex = 3;
            dgvData.Columns["DescripcionCategoria"].DisplayIndex = 4;
            dgvData.Columns["stock"].DisplayIndex = 5;
            dgvData.Columns["costoPesos"].DisplayIndex = 6;
            dgvData.Columns["ventaPesos"].DisplayIndex = 7;
            dgvData.Columns["precioCompra"].DisplayIndex = 8;
            dgvData.Columns["precioVenta"].DisplayIndex = 9;
            dgvData.Columns["prodSerializable"].DisplayIndex = 10;
            dgvData.Columns["fechaUltimaVenta"].DisplayIndex = 11;
            dgvData.Columns["diasSinVenta"].DisplayIndex = 12;
        }







        private void CargarGrilla()
        {
            dgvData.Rows.Clear();
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
            (Math.Round((item.precioVenta * cotizacionActiva) / 1000, 0) * 1000 - 100).ToString("0.00"),
            item.prodSerializable ? "SI" : "NO",
            item.estado ? 1 : 0,
            item.estado ? "Activo" : "No Activo",
            item.fechaUltimaVenta.HasValue ? item.fechaUltimaVenta.Value.ToString("dd/MM/yyyy") : "Sin ventas",
            item.diasSinVenta
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
                (Math.Round((item.precioVenta * cotizacionActiva) / 1000, 0) * 1000 - 100).ToString("0.00"),
                item.prodSerializable == true? "SI":"NO",
                item.estado == true ? 1 : 0,
                item.estado == true ? "Activo" : "No Activo"
                });
            }
        }

        private void checkProductosLocal_CheckedChanged(object sender, EventArgs e)
        {
            // Verifica que dtProductosOriginal esté inicializado
            if (dtProductosOriginal == null)
            {
                // Asegúrate de que el DataTable original esté cargado antes de usarlo
                dtProductosOriginal = new CN_Producto().ListarProductos(GlobalSettings.SucursalId); // O cualquier otra forma de obtener los productos originales
            }

            if (checkProductosLocal.Checked)
            {
                // Cargar los productos filtrados por negocio
                DataTable dtProductosXNegocio = new CN_Producto().ListarProductosPorNegocio(GlobalSettings.SucursalId);
                CargarDataGridView(dtProductosXNegocio); // Cargar los productos del negocio
            }
            else
            {
                dtProductosOriginal = new CN_Producto().ListarProductos(GlobalSettings.SucursalId);
                // Restaurar los productos originales
                CargarDataGridView(dtProductosOriginal); // Cargar los productos originales
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