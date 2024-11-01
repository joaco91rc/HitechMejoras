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
    public partial class frmVentas : Form
    {
        private bool cotizacionCambio = false;
        private Image defaultImage = Properties.Resources.trash;
        private bool isUpdated = false;
        private Usuario _Usuario;
        private Venta _Venta;
        private decimal montoPagoParcialAnterior = 0;
        public List<ProductoDetalle> ListaProductoDetalles { get; set; } = new List<ProductoDetalle>();
        public int StockProducto { get; set; }
        public bool modoEdicion { get; set; }
        public frmVentas(Usuario oUsuario = null, Venta oVenta = null)
        {
            _Usuario = oUsuario;
            _Venta = oVenta;
            modoEdicion = false;
            InitializeComponent();

            if (_Venta != null)
            {
                CargarDatosVenta();
                modoEdicion = true;
            }
        }
        private void CargarComboBoxVendedores()
        {
            // Crear una instancia de la capa de negocio para vendedores
            CN_Vendedor objCN_Vendedor = new CN_Vendedor();

            // Obtener la lista de vendedores desde la base de datos
            List<Vendedor> listaVendedores = objCN_Vendedor.ListarVendedores();

            // Limpiar los items actuales del ComboBox
            cboVendedores.Items.Clear();

            // Llenar el ComboBox con los datos obtenidos
            foreach (Vendedor vendedor in listaVendedores)
            {
                cboVendedores.Items.Add(new OpcionCombo() { Valor = vendedor.idVendedor, Texto = $"{vendedor.nombre} {vendedor.apellido}" });
            }

            // Establecer DisplayMember y ValueMember
            cboVendedores.DisplayMember = "Texto";
            cboVendedores.ValueMember = "Valor";

            // Seleccionar el primer item por defecto si hay elementos en el ComboBox
            if (cboVendedores.Items.Count > 0)
            {
                cboVendedores.SelectedIndex = -1; // O puedes poner `0` si deseas seleccionar el primer item
            }
        }
        public void ActualizarStock()
        {
            txtStock.Text = StockProducto.ToString();
        }
        private void CargarDatosVenta()
           
        {
            int idCliente = new CN_Cliente().ObtenerIdClientePorDocumentoYNombre(_Venta.documentoCliente, _Venta.nombreCliente);
            CargarComboBoxVendedores();
            lblTitulo.Text = String.Format("EDITAR VENTA NUMERO {0}", _Venta.nroDocumento);
            // Aquí puedes cargar los datos de la venta en los controles del formulario
            cboTipoDocumento.Text = _Venta.tipoDocumento;
            txtNombreCliente.Text = _Venta.nombreCliente;           
            txtDocumentoCliente.Text = _Venta.documentoCliente;
            txtIdCliente.Text = idCliente.ToString();
             txtObservaciones.Text = _Venta.observaciones;
            foreach(var item in _Venta.oDetalleVenta)
            {
                dgvData.Rows.Add(item.oProducto.idProducto,item.oProducto.nombre, item.precioVenta,item.precioVenta*1.30m, item.cantidad, item.subTotal,item.oProducto.prodSerializable);
            }
            cboFormaPago.Text = _Venta.formaPago;
            cboFormaPago2.Text = _Venta.formaPago2;
            cboFormaPago3.Text = _Venta.formaPago3;
            cboFormaPago4.Text = _Venta.formaPago4;
            txtPagaCon.Text = _Venta.montoFP1.ToString("0.00");
            txtPagaCon2.Text = _Venta.montoFP2.ToString();
            txtPagaCon3.Text = _Venta.montoFP3.ToString();
            txtPagaCon4.Text = _Venta.montoFP4.ToString();
            txtTotalAPagar.Text = _Venta.montoTotal.ToString();

            OpcionCombo vendedorSeleccionado = cboVendedores.Items.Cast<OpcionCombo>()
                                    .FirstOrDefault(x => Convert.ToInt32(x.Valor) == _Venta.idVendedor);
            if (vendedorSeleccionado != null)
            {
                cboVendedores.SelectedItem = vendedorSeleccionado; // Establecer el item seleccionado
            }
        }

        private void CargarComboBoxFormaPago()
        {
            // Crear una instancia de la capa de negocio
            CN_FormaPago objCN_FormaPago = new CN_FormaPago();

            // Obtener la lista de formas de pago desde la base de datos
            List<FormaPago> listaFormaPago = objCN_FormaPago.ListarFormasDePago();

            // Limpiar los items actuales del ComboBox
            cboFormaPago.Items.Clear();
            cboFormaPago2.Items.Clear();
            cboFormaPago3.Items.Clear();
            cboFormaPago4.Items.Clear();

            // Llenar el ComboBox con los datos obtenidos
            foreach (FormaPago formaPago in listaFormaPago)
            {
                cboFormaPago.Items.Add(new OpcionCombo() { Valor = formaPago.idFormaPago, Texto = formaPago.descripcion });
                cboFormaPago2.Items.Add(new OpcionCombo() { Valor = formaPago.idFormaPago, Texto = formaPago.descripcion });
                cboFormaPago3.Items.Add(new OpcionCombo() { Valor = formaPago.idFormaPago, Texto = formaPago.descripcion });
                cboFormaPago4.Items.Add(new OpcionCombo() { Valor = formaPago.idFormaPago, Texto = formaPago.descripcion });
            }

            // Establecer DisplayMember y ValueMember
            cboFormaPago.DisplayMember = "Texto";
            cboFormaPago.ValueMember = "Valor";
            cboFormaPago2.DisplayMember = "Texto";
            cboFormaPago2.ValueMember = "Valor";
            cboFormaPago3.DisplayMember = "Texto";
            cboFormaPago3.ValueMember = "Valor";
            cboFormaPago4.DisplayMember = "Texto";
            cboFormaPago4.ValueMember = "Valor";

            // Seleccionar el primer item por defecto si hay elementos en el ComboBox
            if (cboFormaPago.Items.Count > 0)
            {
                cboFormaPago.SelectedIndex = -1;
                cboFormaPago2.SelectedIndex = -1;
                cboFormaPago3.SelectedIndex = -1;
                cboFormaPago4.SelectedIndex = -1;
            }
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Factura A", Texto = "Factura A" });
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Factura B", Texto = "Factura B" });
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Factura C", Texto = "Factura C" });
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Remito R", Texto = "Remito R" });
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Presupuesto", Texto = "Presupuesto" });
            cboTipoDocumento.DisplayMember = "Texto";
            cboTipoDocumento.ValueMember = "Valor";
            cboTipoDocumento.SelectedIndex = 3;

            CargarComboBoxFormaPago();
            CargarComboBoxVendedores();

            

            


            dtpFecha.Text = DateTime.Now.ToString();
            txtIdProducto.Text = "0";
            txtIdProducto.Text = "0";
            
            

            var cotizacionDolar = new CN_Cotizacion().CotizacionActiva();
            txtCotizacion.Value = cotizacionDolar.importe;
            
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            using (var modal = new mdCliente())
            {
                var result = modal.ShowDialog();
                if (result == DialogResult.OK)
                {

                    txtDocumentoCliente.Text = modal._Cliente.documento;
                    txtNombreCliente.Text = modal._Cliente.nombreCompleto;
                    txtIdCliente.Text = modal._Cliente.idCliente.ToString();
                    txtCodigoProducto.Select();
                    txtMontoPagoParcial.Value = 0;
                }
                else
                {
                    txtDocumentoCliente.Select();
                }
            }
        }

        public void AgregarDetalleProductoADataGridView(ProductoDetalle productoDetalle)
        {
            string mensaje = string.Empty;
            bool serialExistente = false;

            // Verifica si ya existe el número de serie en el DataGridView
            foreach (DataGridViewRow row in dgvSeriales.Rows)
            {
                if (row.Cells["serialNumber"].Value != null &&
                    row.Cells["serialNumber"].Value.ToString() == productoDetalle.numeroSerie)
                {
                    serialExistente = true;
                    break;
                }
            }

            // Si el serial ya existe, no agregues el producto y muestra un mensaje
            if (serialExistente)
            {
                MessageBox.Show("El número de serie ya existe en el listado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Agrega el producto al DataGridView si el número de serie no existe
                dgvSeriales.Rows.Add(
                    productoDetalle.idProductoDetalle,
                    productoDetalle.idProducto,
                    productoDetalle.nombre,
                    productoDetalle.marca,
                    productoDetalle.modelo,
                    productoDetalle.color,
                    productoDetalle.numeroSerie
                );
                dgvSeriales.Visible = true;

                // Llama al método para desactivar el producto si es necesario
                //bool desactivarProductoSerial = new CN_Producto().DesactivarProductoDetalle(productoDetalle.idProductoDetalle, 0, out mensaje);
            }
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {

            using (var modal = new mdProducto(this))
            {
                modal.Owner = this;
                var result = modal.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtIdProducto.Text = modal._Producto.idProducto.ToString();
                    txtCodigoProducto.Text = modal._Producto.codigo;
                    txtProducto.Text = modal._Producto.nombre;
                    txtPrecio.Text = modal._Producto.precioVenta.ToString("0.00");
                    txtSerializable.Text = modal._Producto.prodSerializable.ToString();
                    txtCantidad.Select();
                    
                }
                else
                {
                    txtCodigoProducto.Select();
                }
            }
        }

        private void txtCodigoProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Producto oProducto = new CN_Producto().Listar(GlobalSettings.SucursalId).Where(p => p.codigo == txtCodigoProducto.Text && p.estado == true).FirstOrDefault();
                int stockProducto = new CN_ProductoNegocio().ObtenerStockProductoEnSucursal(oProducto.idProducto, GlobalSettings.SucursalId);
                if (oProducto != null)
                {
                    txtCodigoProducto.BackColor = Color.ForestGreen;
                    txtIdProducto.Text = oProducto.idProducto.ToString();
                    txtProducto.Text = oProducto.nombre;
                    txtPrecio.Text = oProducto.precioVenta.ToString("0.00");
                    txtStock.Text = stockProducto.ToString();
                    txtCantidad.Select();
                }
                else
                {
                    txtCodigoProducto.BackColor = Color.IndianRed;
                    txtIdProducto.Text = "0";
                    txtProducto.Text = "";
                    txtPrecio.Text = "";
                    txtStock.Text = "";
                    txtCantidad.Value = 1;



                }
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            decimal precio = 0;
            bool producto_existe = false;
            bool esSerializable = Convert.ToBoolean(txtSerializable.Text);

            if (int.Parse(txtIdProducto.Text) == 0)
            {
                MessageBox.Show("Debe Seleccionar un Producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("Precio  - Formato Moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrecio.Select();
                return;
            }

            if (Convert.ToInt32(txtStock.Text) < Convert.ToInt32(txtCantidad.Value.ToString()))
            {
                MessageBox.Show("La cantidad ingresada deber ser menor al Stock Fisico", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Verificar si el producto ya existe en dgvData
            foreach (DataGridViewRow fila in dgvData.Rows)
            {
                if (fila.Cells["idProducto"].Value.ToString() == txtIdProducto.Text)
                {
                    if (!esSerializable) // Si el producto no es serializable, verifica si ya existe
                    {
                        producto_existe = true;
                        break;
                    }
                }
            }

            // Si el producto no existe, proceder a agregarlo
            if (!producto_existe)
            {
                // Si es un producto serializable
                if (esSerializable)
                {
                    // Abrir el modal para capturar los seriales
                    using (var modal = new mdProductoSerializable(Convert.ToInt32(txtIdProducto.Text)))
                    {
                        if (modal.ShowDialog() == DialogResult.OK)
                        {
                            // Solo si hay seriales válidos, proceder a agregar el producto a dgvData
                            if (modal.ListaProductoDetalles != null && modal.ListaProductoDetalles.Count > 0)
                            {
                                decimal precioLista30 = Math.Round((precio * txtCotizacion.Value) / 500) * 500 * 1.30m;

                                // Agregar el producto a dgvData
                                dgvData.Rows.Add(new object[]{
                            txtIdProducto.Text,
                            txtProducto.Text,
                            precio.ToString("0.00"),
                            precioLista30,
                            txtCantidad.Value.ToString(),
                            (txtCantidad.Value * precio).ToString("0.00"),
                            txtSerializable.Text,
                            defaultImage
                        });

                                // Agregar los seriales al dgvSeriales
                                foreach (var productoDetalle in modal.ListaProductoDetalles)
                                {
                                    AgregarDetalleProductoADataGridView(productoDetalle);
                                }

                                calcularTotal();
                                limpiarProducto();
                                txtCodigoProducto.Select();
                            }
                            else
                            {
                                MessageBox.Show("No se han agregado seriales. No se puede añadir el producto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                else // Si es un producto no serializable
                {
                    decimal precioLista30 = Math.Round((precio * txtCotizacion.Value) / 500) * 500 * 1.30m;

                    // Agregar el producto a dgvData directamente
                    dgvData.Rows.Add(new object[]{
                txtIdProducto.Text,
                txtProducto.Text,
                precio.ToString("0.00"),
                precioLista30,
                txtCantidad.Value.ToString(),
                (txtCantidad.Value * precio).ToString("0.00"),
                txtSerializable.Text,
                defaultImage
            });

                    calcularTotal();
                    limpiarProducto();
                    txtCodigoProducto.Select();
                }
            }
            else
            {
                MessageBox.Show("El producto ya existe en la lista y no es serializable.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void calcularTotal()
        {
            decimal total = 0;
            if (dgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    total += Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString());
                }
                decimal totalCotizado = total * txtCotizacion.Value;
                decimal totalProductos = 0;

                // Verifica el estado de la variable cotizacionCambio
                if (cotizacionCambio)
                {
                    totalProductos = totalCotizado; // Usa el valor exacto si la cotización cambió
                }
                else
                {
                    totalProductos = Math.Round(totalCotizado / 500) * 500; // Redondea si no cambió
                }

                txtTotalAPagar.Value = totalProductos;
                txtTotalAPagarDolares.Value = total;
                txtRestaPagar.Value = txtTotalAPagar.Value;
                txtRestaPagarDolares.Value = txtTotalAPagarDolares.Value;
            }
        }

        

        private void limpiarProducto()
        {
           
            txtProducto.Text = "";
            txtCodigoProducto.BackColor = Color.White;
            txtCodigoProducto.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            txtCantidad.Value = 1;
            
        }



        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    int idProducto = Convert.ToInt32(dgvData.Rows[indice].Cells["idProducto"].Value.ToString());
                    

                    
                        // Eliminar el producto de dgvData
                        dgvData.Rows.RemoveAt(indice);

                        // Eliminar también el producto correspondiente de dgvSeriales
                        EliminarProductoDeSeriales(idProducto);
                        if(dgvSeriales.Rows.Count == 0)
                        {
                            dgvSeriales.Visible = false;
                        }

                        // Reiniciar campos y recalcular totales
                        txtTotalAPagar.Value = 0;
                        txtTotalAPagarDolares.Value = 0;
                        txtRestaPagar.Value = 0;
                        txtRestaPagarDolares.Value = 0;
                        cboFormaPago.SelectedIndex = -1;
                        cboFormaPago2.SelectedIndex = -1;
                        cboFormaPago3.SelectedIndex = -1;
                        cboFormaPago4.SelectedIndex = -1;
                        txtPagaCon.Value = 0;
                        txtPagaCon2.Value = 0;
                        txtPagaCon3.Value = 0;
                        txtPagaCon4.Value = 0;

                        calcularTotal();
                    
                }
            }
        }


        private void EliminarProductoDeSeriales(int idProducto)
        {
            string mensaje = string.Empty;
            for (int i = dgvSeriales.Rows.Count - 1; i >= 0; i--)
            {
                int idProductoDetalle = Convert.ToInt32(dgvSeriales.Rows[i].Cells["idProductoDetalle"].Value);
                // Asumiendo que hay una columna en dgvSeriales que contiene el idProducto
                if (Convert.ToInt32(dgvSeriales.Rows[i].Cells["idProdSerial"].Value) == idProducto)
                {
                    dgvSeriales.Rows.RemoveAt(i);
                    bool activarSerial = new CN_Producto().ActivarProductoDetalle(idProductoDetalle, out mensaje);
                }
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {

                e.Handled = false;
            }
            else
            {
                if (txtPrecio.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void txtPagaCon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {

                e.Handled = false;
            }
            else
            {
                if (txtPagaCon.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }


        private void CalcularCambio()
        {
            if (txtTotalAPagar.Text.Trim() == "")
            {
                MessageBox.Show("No existen productos en la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            decimal pagacon;
            if(cboFormaPago.Text == "DOLAR" || cboFormaPago.Text == "DOLAR EFECTIVO")
            {

                pagacon = 0;
            }
            else
            {
                pagacon = Convert.ToDecimal(txtPagaCon.Text);
            }
            
           if( txtPagaCon2.Text != string.Empty)
            {
                pagacon +=  Convert.ToDecimal(txtPagaCon2.Text);

            }else
            {
                pagacon +=  0;
            }

            if (txtPagaCon3.Text != string.Empty)
            {
                pagacon +=  Convert.ToDecimal(txtPagaCon3.Text);

            }
            else
            {
                pagacon += 0;
            }

            if (txtPagaCon4.Text != string.Empty)
            {
                pagacon += Convert.ToDecimal(txtPagaCon4.Text);

            }
            else
            {
                pagacon = pagacon + 0;
            }
            

            decimal total = Convert.ToDecimal(txtTotalAPagar.Text);
            

            if (txtPagaCon.Text.Trim() == "")
            {
                txtPagaCon.Text = "0";
            }

                if (pagacon < total)
                {
                    txtCambioCliente.Text = "0.00";

                }
                else
                {
                    decimal cambio = pagacon - total;
                    txtCambioCliente.Text = cambio.ToString("0.00");
                }
            
        }

       

        private void txtPagaCon_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyData == Keys.Enter)
            //{
            //    CalcularCambio();
            //}

            if (e.KeyData == Keys.Enter)
            {
                //if (txtPagaCon.Text != string.Empty) {
                //    if(cboFormaPago.Text =="DOLAR" || cboFormaPago.Text == "DOLAR EFECTIVO")
                //    {


                //       calcularTotalConDolares();
                //        txtRestaPagarDolares.Value = (txtTotalAPagarDolares.Value -txtPagaCon.Value);
                //    }
                //    else { 
                //    txtRestaPagar.Text = (Convert.ToDecimal(txtTotalAPagar.Text) - Convert.ToDecimal(txtPagaCon.Text)).ToString();


                //    CalcularCambio();
                //    }
                //}
                CalcularRestaAPagar();
                CalcularCambio();
                if(cboFormaPago.Text == "DOLAR" || cboFormaPago.Text == "DOLAR EFECTIVO")
                {
                    txtTotalAPagar.Value = txtRestaPagarDolares.Value * txtCotizacion.Value;
                    CalcularRestaAPagar();
                }

                

            }
        }

        private void btnRegistrarCompra_Click(object sender, EventArgs e)
        {
            if (txtDocumentoCliente.Text == "")
            {
                MessageBox.Show("Debe ingresar el documento del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtNombreCliente.Text == "")
            {
                MessageBox.Show("Debe ingresar el nombre del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar productos en la Venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (checkDescuento.Checked && txtDescuento.Text == "")
            {
                MessageBox.Show("Debe ingresar un porcentaje de descuento", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if ((txtPagaCon.Value == 0)  && (txtPagaCon2.Value == 0) && (txtPagaCon3.Value == 0) && (txtPagaCon4.Value == 0))
            {
                MessageBox.Show("No ha establecido ningun monto para la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (cboFormaPago.SelectedIndex == -1 &&  cboFormaPago2.SelectedIndex == -1 && cboFormaPago3.SelectedIndex == -1 && cboFormaPago4.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar al menos una forma de pago", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //if(cboVendedores.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Debe Seleccionar un Vendedor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}
            DataTable detalle_venta = new DataTable();

            detalle_venta.Columns.Add("idProducto", typeof(int));
            detalle_venta.Columns.Add("precioVenta", typeof(decimal));
            detalle_venta.Columns.Add("cantidad", typeof(decimal));
            detalle_venta.Columns.Add("subTotal", typeof(decimal));


            foreach (DataGridViewRow row in dgvData.Rows)
            {

                detalle_venta.Rows.Add(
                    new object[]
                    {
                        Convert.ToInt32(row.Cells["idProducto"].Value.ToString()),

                        row.Cells["precio"].Value.ToString(),
                        row.Cells["cantidad"].Value.ToString(),
                        row.Cells["subTotal"].Value.ToString()
                    });
            }

            int idCorrelativo = new CN_Venta().ObtenerCorrelativo();
            string numeroDocumento = string.Format("{0:00000}", idCorrelativo);
            CalcularCambio();
            decimal montoPagado = 0;
            decimal montoPagadoFP2 = 0;
            decimal montoPagadoFP3 = 0;
            decimal montoPagadoFP4 = 0;
            if (cboFormaPago.SelectedItem != null)
            {
                FormaPago fp1 = new CN_FormaPago().ObtenerFPPorDescripcion(((OpcionCombo)cboFormaPago.SelectedItem).Texto);
                if (txtPagaCon.Text != string.Empty)
                {
                    montoPagado = montoPagado + Convert.ToDecimal(txtPagaCon.Text) - (Convert.ToDecimal(txtPagaCon.Text) * fp1.porcentajeRetencion) / 100;
                }
            }

            if (cboFormaPago2.SelectedItem != null)
            {
                FormaPago fp2 = new CN_FormaPago().ObtenerFPPorDescripcion(((OpcionCombo)cboFormaPago2.SelectedItem).Texto);
                if (txtPagaCon2.Text != string.Empty)
                {
                    montoPagadoFP2 = montoPagadoFP2 + Convert.ToDecimal(txtPagaCon2.Text) - (Convert.ToDecimal(txtPagaCon2.Text) * fp2.porcentajeRetencion) / 100;
                }
            }
            if (cboFormaPago3.SelectedItem != null)
            {
                FormaPago fp3 = new CN_FormaPago().ObtenerFPPorDescripcion(((OpcionCombo)cboFormaPago3.SelectedItem).Texto);
                if (txtPagaCon3.Text != string.Empty)
                {
                    montoPagadoFP3 = montoPagadoFP3 + Convert.ToDecimal(txtPagaCon3.Text) - (Convert.ToDecimal(txtPagaCon3.Text) * fp3.porcentajeRetencion) / 100;
                }
            }
            if (cboFormaPago4.SelectedItem != null)
            {
                FormaPago fp4 = new CN_FormaPago().ObtenerFPPorDescripcion(((OpcionCombo)cboFormaPago4.SelectedItem).Texto);
                if (txtPagaCon4.Text != string.Empty)
                {
                    montoPagadoFP4 = montoPagadoFP4 + Convert.ToDecimal(txtPagaCon4.Text) - (Convert.ToDecimal(txtPagaCon4.Text) * fp4.porcentajeRetencion) / 100;
                }
            }

            
            
            

            
            

            

           

            

            if (txtCotizacion.Value != 0)
{
    Venta oVenta = new Venta()
    {
        oUsuario = new Usuario() { idUsuario = _Usuario.idUsuario },
        idNegocio = GlobalSettings.SucursalId,
        fechaRegistro = Convert.ToDateTime(dtpFecha.Value),
        tipoDocumento = ((OpcionCombo)cboTipoDocumento.SelectedItem).Texto,
        nroDocumento = numeroDocumento,
        documentoCliente = txtDocumentoCliente.Text,
        nombreCliente = txtNombreCliente.Text,
        idVendedor = (OpcionCombo)cboVendedores.SelectedItem==null?_Venta.idVendedor: Convert.ToInt32(((OpcionCombo)cboVendedores.SelectedItem).Valor),
        montoCambio = Convert.ToDecimal(txtCambioCliente.Text),
        montoTotal = Convert.ToDecimal(txtTotalAPagar.Text)==0? Convert.ToDecimal(txtTotalAPagarDolares.Text): Convert.ToDecimal(txtTotalAPagar.Text),
        formaPago = cboFormaPago.SelectedItem != null ? ((OpcionCombo)cboFormaPago.SelectedItem).Texto : "",
        formaPago2 = cboFormaPago2.SelectedItem != null ? ((OpcionCombo)cboFormaPago2.SelectedItem).Texto : "",
        formaPago3 = cboFormaPago3.SelectedItem != null ? ((OpcionCombo)cboFormaPago3.SelectedItem).Texto : "",
        formaPago4 = cboFormaPago4.SelectedItem != null ? ((OpcionCombo)cboFormaPago4.SelectedItem).Texto : "",

                descuento = Convert.ToDecimal(txtDescuento.Text),
        montoDescuento = Convert.ToDecimal(txtMontoDescuento.Text),
        cotizacionDolar = txtCotizacion.Value,
        
        
        montoFP1 = txtPagaCon.Text != string.Empty ?  Convert.ToDecimal(txtPagaCon.Text): 0,
        montoFP2 = txtPagaCon2.Text != string.Empty ? Convert.ToDecimal(txtPagaCon2.Text) : 0,
        montoFP3 = txtPagaCon3.Text != string.Empty ? Convert.ToDecimal(txtPagaCon3.Text) : 0,
        montoFP4 = txtPagaCon4.Text != string.Empty ? Convert.ToDecimal(txtPagaCon4.Text) : 0,

        montoPago = montoPagado,
        montoPagoFP2 = montoPagadoFP2,
        montoPagoFP3 = montoPagadoFP3,
        montoPagoFP4 = montoPagadoFP4,
        observaciones = txtObservaciones.Text


    };
                string actualizacionStock = string.Empty;
                bool actualizarSerial = false;
                string mensaje = string.Empty;
                string mensajeSerialActualizado = string.Empty;
                int idVentaGenerado = 0;
                bool respuesta = false;
                string tipo = string.Empty;
                if (oVenta.idVenta == 0)
                {
                     respuesta = new CN_Venta().Registrar(oVenta, detalle_venta, out mensaje, out idVentaGenerado);
                    tipo = "Generado";
                }
                else
                {
                    oVenta.idVenta = _Venta.idVenta;
                    respuesta = new CN_Venta().EditarVenta(oVenta, detalle_venta, out mensaje, out idVentaGenerado);
                    tipo = "Modificado";
                }
                if (respuesta)
                    
    {           
                    if(txtIdPagoParcial.Text != "0")
                    {
                        bool darBajaPagoParcial = new CN_PagoParcial().DarDeBajaPagoParcial(Convert.ToInt32(txtIdPagoParcial.Text));
                        if (darBajaPagoParcial == false) {
                            MessageBox.Show("no se pudo dar de baja el pago parcial", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    var perteneceANegocio = new CN_ClienteNegocio().ClienteAsignadoANegocio(Convert.ToInt32(txtIdCliente.Text), GlobalSettings.SucursalId);
                    if (!perteneceANegocio)
                    {
                        var asignarCliente = new CN_ClienteNegocio().AsignarClienteANegocio(Convert.ToInt32(txtIdCliente.Text), GlobalSettings.SucursalId);
                    }
                    
                    foreach (DataGridViewRow row in dgvData.Rows)
                    {
                        if (row.Cells["idProducto"].Value != null && row.Cells["cantidad"].Value != null)
                        {
                            int idProducto = Convert.ToInt32(row.Cells["idProducto"].Value);
                            int cantidad = Convert.ToInt32(row.Cells["cantidad"].Value);

                            // Actualizar el stock del producto
                            actualizacionStock= new CN_ProductoNegocio().CargarOActualizarStockProducto(idProducto, GlobalSettings.SucursalId, -cantidad);
                        }
                    }
                    
                    if (dgvSeriales.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow row in dgvSeriales.Rows)
                        {
                            if (row.Cells["idProductoDetalle"].Value != null) // Verificar que la celda no esté vacía
                            {
                                int idProductoDetalle = Convert.ToInt32(row.Cells["idProductoDetalle"].Value);
                                actualizarSerial = new CN_Producto().DesactivarProductoDetalle(idProductoDetalle, idVentaGenerado, out  mensaje);
                                if (actualizarSerial)
                                {
                                    mensajeSerialActualizado = "Se han dado de Baja el o los Numero de Serie";
                                } else
                                {
                                    MessageBox.Show($"Error al dar de Baja el Numero de Serie: {mensaje}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break; // Detener el loop si ocurre un error
                                }
                                
                                   
                                
                            }
                        }
                    }
                   

                    txtIdProducto.Text = "0";
                    List<string> formasPago = new List<string>();
                    formasPago.Add(cboFormaPago.Text);
                    if (cboFormaPago2.SelectedIndex >= 0) formasPago.Add(cboFormaPago2.Text);
                    if (cboFormaPago3.SelectedIndex >= 0) formasPago.Add(cboFormaPago3.Text);
                    if (cboFormaPago4.SelectedIndex >= 0) formasPago.Add(cboFormaPago4.Text);
                    string nombreCliente = txtNombreCliente.Text;
                    List<CajaRegistradora> lista = new CN_CajaRegistradora().Listar(GlobalSettings.SucursalId);

                    CajaRegistradora cajaAbierta = lista.Where(c => c.estado == true).FirstOrDefault();
                   
                    if (cajaAbierta != null)

                    {
                        
                        if(oVenta.montoPago > 0) {
                            var cajaAsociadaFP1 = new CN_FormaPago().ObtenerFPPorDescripcion(oVenta.formaPago).cajaAsociada;
                            TransaccionCaja objTransaccion = new TransaccionCaja()
                        {
                            idCajaRegistradora = cajaAbierta.idCajaRegistradora,

                            hora = dtpFecha.Value.Hour.ToString(),
                            tipoTransaccion = "ENTRADA",
                            monto = oVenta.montoPago,
                            docAsociado = "Venta Numero:" + " " + numeroDocumento + " Cliente:" + " " + nombreCliente,
                            usuarioTransaccion = cboVendedores.Text,
                            formaPago = cboFormaPago.Text,
                            cajaAsociada = cajaAsociadaFP1,
                            idVenta = idVentaGenerado,
                            idCompra = null,
                            idNegocio = GlobalSettings.SucursalId,
                            concepto = "VENTA"

                            };




                        int idTransaccionGenerado = new CN_Transaccion().RegistrarMovimiento(objTransaccion, out mensaje);

                        }

                        if (oVenta.montoPagoFP2 > 0)
                        {
                            var cajaAsociadaFP2 = new CN_FormaPago().ObtenerFPPorDescripcion(oVenta.formaPago2).cajaAsociada;
                            TransaccionCaja objTransaccion2 = new TransaccionCaja()
                            {
                                idCajaRegistradora = cajaAbierta.idCajaRegistradora,

                                hora = dtpFecha.Value.Hour.ToString(),
                                tipoTransaccion = "ENTRADA",
                                monto = oVenta.montoPagoFP2,
                                docAsociado = "Venta Numero:" + " " + numeroDocumento + " Cliente:" + " " + nombreCliente,
                                usuarioTransaccion = cboVendedores.Text,
                                formaPago = cboFormaPago2.Text,
                                cajaAsociada = cajaAsociadaFP2,
                                idVenta = idVentaGenerado,
                                idCompra = null,
                                idNegocio = GlobalSettings.SucursalId,
                                concepto = "VENTA"
                            };




                            int idTransaccionGenerado = new CN_Transaccion().RegistrarMovimiento(objTransaccion2, out mensaje);
                        }

                        if (oVenta.montoPagoFP3 > 0)
                        {
                            var cajaAsociadaFP3 = new CN_FormaPago().ObtenerFPPorDescripcion(oVenta.formaPago3).cajaAsociada;
                            TransaccionCaja objTransaccion3 = new TransaccionCaja()
                            {
                                idCajaRegistradora = cajaAbierta.idCajaRegistradora,

                                hora = dtpFecha.Value.Hour.ToString(),
                                tipoTransaccion = "ENTRADA",
                                monto = oVenta.montoPagoFP3,
                                docAsociado = "Venta Numero:" + " " + numeroDocumento + " Cliente:" + " " + nombreCliente,
                                usuarioTransaccion = cboVendedores.Text,
                                formaPago = cboFormaPago3.Text,
                                cajaAsociada = cajaAsociadaFP3,
                                idVenta = idVentaGenerado,
                                idCompra = null,
                                idNegocio = GlobalSettings.SucursalId,
                                concepto = "VENTA"
                            };




                            int idTransaccionGenerado = new CN_Transaccion().RegistrarMovimiento(objTransaccion3, out mensaje);
                        }

                        if (oVenta.montoPagoFP4 > 0)
                        {
                            var cajaAsociadaFP4 = new CN_FormaPago().ObtenerFPPorDescripcion(oVenta.formaPago4).cajaAsociada;
                            TransaccionCaja objTransaccion4 = new TransaccionCaja()
                            {
                                idCajaRegistradora = cajaAbierta.idCajaRegistradora,

                                hora = dtpFecha.Value.Hour.ToString(),
                                tipoTransaccion = "ENTRADA",
                                monto = oVenta.montoPagoFP4,
                                docAsociado = "Venta Numero:" + " " + numeroDocumento + " Cliente:" + " " + nombreCliente,
                                usuarioTransaccion = cboVendedores.Text,
                                formaPago = cboFormaPago4.Text,
                                cajaAsociada= cajaAsociadaFP4,
                                idVenta = idVentaGenerado,
                                idCompra = null,
                                idNegocio = GlobalSettings.SucursalId,
                                concepto = "VENTA"
                            };




                            int idTransaccionGenerado = new CN_Transaccion().RegistrarMovimiento(objTransaccion4, out mensaje);
                        }

                    }
                    

                    var result = MessageBox.Show("Numero de Venta:\n"+ tipo + numeroDocumento +". " + mensajeSerialActualizado, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        if (result == DialogResult.OK)
                        
            Clipboard.SetText(numeroDocumento);
                    txtIdCliente.Text = string.Empty;
                    dgvSeriales.Rows.Clear();
                    dgvSeriales.Visible = false;
                    txtObservaciones.Text = string.Empty;
        txtDocumentoCliente.Text = "";
        txtNombreCliente.Text = "";
        dgvData.Rows.Clear();
                    txtIdPagoParcial.Text = "0";
        calcularTotal();
        txtPagaCon.Text = "";
        txtCambioCliente.Text = "";
        cboFormaPago.SelectedIndex = -1;
        txtTotalVentaDolares.Text = string.Empty;
        cboVendedores.SelectedIndex = -1;
        cboFormaPago2.SelectedIndex = -1;
        cboFormaPago3.SelectedIndex = -1;
        cboFormaPago4.SelectedIndex = -1;
                    txtTotalAPagar.Text = string.Empty;
                    txtPagaCon.Text = string.Empty;
                    txtPagaCon2.Text = string.Empty;
                    txtPagaCon3.Text = string.Empty;
                    txtPagaCon4.Text = string.Empty;
                    txtRestaPagar.Text = string.Empty;
                    isUpdated = false;
                    lblTotalAPagarDolares.Visible = false;
                    lblRestaPagarDolares.Visible = false;
                    txtTotalAPagarDolares.Visible = false;
                    txtRestaPagarDolares.Visible = false;
                    checkDescuento.Checked = false;
                    

       


    }
    else
    {
        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
}else
{
                MessageBox.Show("Debe ingresar la cotizacion del dolar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            
        }
        }

        private void checkDescuento_CheckedChanged(object sender, EventArgs e)
        {
            if (checkDescuento.Checked)
            {
                txtDescuento.Visible = true;
                txtDescuento.Enabled = true;
                txtMontoDescuento.Visible = true;
                txtMontoDescuento.Enabled = true;
                txtMontoDescuento.Text = "0";
                txtDescuento.Text = "0";
                lblPorcentaje.Visible = true;
                txtMontoDescuento.Text = "0";
                checkRecargo.Visible = false;
                checkMonedaDolar.Visible = true;

            }


            else
            {
                txtDescuento.Visible = false;
                txtMontoDescuento.Text = "0";
                txtMontoDescuento.Visible = false;
                txtDescuento.Text = "0";
                lblPorcentaje.Visible = false;
                lblDescuento.Visible = false;
                checkRecargo.Visible = true;
                cboFormaPago.SelectedIndex = -1;
                cboFormaPago2.SelectedIndex = -1;
                cboFormaPago3.SelectedIndex = -1;
                cboFormaPago4.SelectedIndex = -1;
                checkMonedaDolar.Visible = false;
                calcularTotal();
            }

        }

        private void txtDescuento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                if (Convert.ToDecimal(txtDescuento.Text) > 0 && Convert.ToDecimal(txtDescuento.Text) <= 100 && (txtDescuento.Text != ""))
                {
                    txtMontoDescuento.Visible = true;
                    txtMontoDescuento.Enabled = false;
                    decimal montoDescuentoRecargo = (txtRestaPagar.Value * Convert.ToDecimal(txtDescuento.Text)) / 100;
                    txtMontoDescuento.Text = montoDescuentoRecargo.ToString("0.00");
                    if(checkDescuento.Checked == true)
                    {
                        txtTotalAPagar.Text = (Convert.ToDecimal(txtTotalAPagar.Text) - montoDescuentoRecargo).ToString("0.00");
                        
                        
                        if (cboFormaPago.Text =="DOLAR" || cboFormaPago.Text=="DOLAR EFECTIVO")
                        {
                            txtRestaPagar.Value -=   montoDescuentoRecargo;
                        }
                        else
                        {
                            CalcularRestaAPagar();
                        }
                    }
                    if (checkRecargo.Checked == true)
                    {
                        txtTotalAPagar.Text = (Convert.ToDecimal(txtTotalAPagar.Text) + montoDescuentoRecargo).ToString("0.00");
                        if (cboFormaPago.Text == "DOLAR" || cboFormaPago.Text == "DOLAR EFECTIVO")
                        {
                            txtRestaPagar.Value +=  montoDescuentoRecargo;
                        } else
                        {
                            CalcularRestaAPagar();
                        }
                    }
                    txtDescuento.Enabled = false;
                    lblDescuento.Visible = true;
                    CalcularCambio();
                }
                else
                {
                    txtMontoDescuento.Visible = false;
                    MessageBox.Show("Ingrese un valor entre 1 y 100 para el porcentaje de descuento o recargo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
        }

        private void checkRecargo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkRecargo.Checked)
            {
                txtMontoDescuento.Visible = true;
                txtMontoDescuento.Enabled= true;
                txtDescuento.Visible = true;
                txtDescuento.Enabled = true;
                txtDescuento.Text = "0";
                lblPorcentaje.Visible = true;
                txtMontoDescuento.Text = "0";
                checkDescuento.Visible = false;
                checkMonedaDolar.Visible = true;


            }


            else
            {
                txtDescuento.Visible = false;
                txtMontoDescuento.Text = "0";
                txtMontoDescuento.Visible = false;
                txtDescuento.Text = "0";
                lblPorcentaje.Visible = false;
                lblDescuento.Visible = false;
                checkDescuento.Visible = true;
                cboFormaPago.SelectedIndex = -1;
                cboFormaPago2.SelectedIndex = -1;
                cboFormaPago3.SelectedIndex = -1;
                cboFormaPago4.SelectedIndex = -1;
                checkMonedaDolar.Visible = false;
                calcularTotal();
            }
        }

        private void txtCotizacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtTotalAPagar.Text = (Convert.ToDecimal(txtTotalAPagarDolares.Text) * txtCotizacion.Value).ToString();
                cotizacionCambio = true;
            }
        }

        

        private void CalcularRestaAPagar()
        {
            decimal cotizacionDolar = txtCotizacion.Value;
            decimal totalAPagar = txtTotalAPagar.Value;
            decimal totalAPagarDolares = txtTotalAPagarDolares.Value;
            decimal pagoTotal = 0;
            decimal pagoTotalDolares = 0;

            // Calcula la diferencia entre el valor actual y el valor anterior del NumericUpDown
            decimal diferenciaPagoParcial = txtMontoPagoParcial.Value - montoPagoParcialAnterior;

            // Resta solo la diferencia calculada
            totalAPagar -= diferenciaPagoParcial;

            // Actualiza el valor del monto anterior al valor actual del NumericUpDown
            montoPagoParcialAnterior = txtMontoPagoParcial.Value;

            // Continua con el cálculo de formas de pago y acumulación de montos como antes
            var formasDePago = new[]
            {
        new { FormaPago = cboFormaPago.Text, Monto = txtPagaCon.Value },
        new { FormaPago = cboFormaPago2.Text, Monto = txtPagaCon2.Value },
        new { FormaPago = cboFormaPago3.Text, Monto = txtPagaCon3.Value },
        new { FormaPago = cboFormaPago4.Text, Monto = txtPagaCon4.Value }
    };

            foreach (var pago in formasDePago)
            {
                if (pago.FormaPago == "DOLAR" || pago.FormaPago == "DOLAR EFECTIVO")
                {
                    pagoTotalDolares += pago.Monto;
                }
                else
                {
                    pagoTotal += pago.Monto;
                }
            }

            // Manejo de descuentos
            if (checkDescuento.Checked)
            {
                decimal montoDescuento = Convert.ToDecimal(txtMontoDescuento.Text);

                if (checkMonedaDolar.Checked)
                {
                    totalAPagarDolares -= montoDescuento;
                }
                else
                {
                    totalAPagar -= montoDescuento;
                }
            }

            // Manejo de recargos
            if (checkRecargo.Checked)
            {
                decimal montoRecargo = Convert.ToDecimal(txtMontoDescuento.Text);

                if (checkMonedaDolar.Checked)
                {
                    totalAPagarDolares += montoRecargo;
                }
                else
                {
                    totalAPagar += montoRecargo;
                }
            }

            // Calcula el resto a pagar en cada moneda
            decimal restoAPagar = totalAPagar - pagoTotal;
            decimal restoAPagarDolares = totalAPagarDolares - pagoTotalDolares;

            if (restoAPagar < 0) restoAPagar = 0;
            if (restoAPagarDolares < 0) restoAPagarDolares = 0;

            // Actualiza los valores en los controles
            txtTotalAPagar.Value = totalAPagar;
            txtTotalAPagarDolares.Value = totalAPagarDolares;
            txtRestaPagar.Value = restoAPagar;
            txtRestaPagarDolares.Value = restoAPagarDolares;

            // Si una de las "restas a pagar" es 0, ambas deben ser 0
            if (txtRestaPagar.Value == 0)
            {
                txtRestaPagarDolares.Value = 0;
            }
            if (txtRestaPagarDolares.Value == 0)
            {
                txtRestaPagar.Value = 0;
            }
        }


        private void txtMontoDescuento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (checkDescuento.Checked == true)
                {
                    if (checkMonedaDolar.Checked)
                    {
                        
                        txtRestaPagarDolares.Value = txtRestaPagarDolares.Value - Convert.ToDecimal(txtMontoDescuento.Text);
                        txtRestaPagar.Value = txtTotalAPagarDolares.Value * txtCotizacion.Value - Convert.ToDecimal(txtMontoDescuento.Text)*txtCotizacion.Value;
                        
                    }
                    else
                    {
                        txtRestaPagar.Text = (Convert.ToDecimal(txtTotalAPagar.Text) - Convert.ToDecimal(txtMontoDescuento.Text)).ToString("0.00");
                        
                    }
                    CalcularRestaAPagar();
                }
                if (checkRecargo.Checked == true)

                {
                    if (checkMonedaDolar.Checked)
                    {
                        
                        txtRestaPagarDolares.Value = txtRestaPagarDolares.Value + Convert.ToDecimal(txtMontoDescuento.Text);
                        txtRestaPagar.Value = txtTotalAPagarDolares.Value * txtCotizacion.Value + Convert.ToDecimal(txtMontoDescuento.Text) * txtCotizacion.Value;
                    } else
                    {
                        txtRestaPagar.Text = (Convert.ToDecimal(txtTotalAPagar.Text) + Convert.ToDecimal(txtMontoDescuento.Text)).ToString("0.00");
                    }
                    CalcularRestaAPagar();
                }
                
            }
        }

        private void txtPagaCon2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (txtPagaCon2.Text != string.Empty) {

                    //if (cboFormaPago2.Text == "DOLAR" || cboFormaPago2.Text == "DOLAR EFECTIVO")
                    //{


                    //    calcularTotalConDolares();
                    //    txtRestaPagarDolares.Value = (txtRestaPagarDolares.Value - txtPagaCon2.Value);
                    //}
                    //else
                    //{
                    //    txtRestaPagar.Text = (Convert.ToDecimal(txtTotalAPagar.Text) - Convert.ToDecimal(txtPagaCon.Text)).ToString();


                    //    CalcularCambio();
                    //}
                    CalcularRestaAPagar();
                    CalcularCambio();
                    if(txtRestaPagar.Value == 0)
                    {
                        txtRestaPagarDolares.Value = 0;
                    }
                }
               
                
            }

            


        }

        private void txtPagaCon3_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Enter)
            {
                //if (txtPagaCon3.Text != string.Empty)
                //{
                //    txtRestaPagar.Text = (Convert.ToDecimal(txtRestaPagar.Text) - txtPagaCon3.Value).ToString("0.00");


                //    CalcularCambio();

                //}
                CalcularRestaAPagar();
                CalcularCambio();
                if (txtRestaPagar.Value == 0)
                {
                    txtRestaPagarDolares.Value = 0;
                }
            }

            
        }

        private void txtPagaCon4_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Enter)
            {
                //if (txtPagaCon4.Text != string.Empty)
                //{
                //    txtRestaPagar.Text = (Convert.ToDecimal(txtRestaPagar.Text) - txtPagaCon4.Value).ToString("0.00");


                //    CalcularCambio();

                //}
                CalcularRestaAPagar();
                CalcularCambio();
                if (txtRestaPagar.Value == 0)
                {
                    txtRestaPagarDolares.Value = 0;
                }
            }

            
        }

        private void txtRestaPagar_TextChanged(object sender, EventArgs e)
        {
            if (txtRestaPagar.Text != string.Empty)
            {
                if (Convert.ToDecimal(txtRestaPagar.Text) < 0)
                {
                    txtCambioCliente.Text = (Convert.ToDecimal(txtRestaPagar.Text) * -1).ToString("0.00");
                }
                else
                {
                    txtCambioCliente.Text = "0.00";
                }
            }
        }

        
        
        private void cboFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Recalcula el total original sin recargos ni descuentos
            calcularTotal();
            if(cboFormaPago.Text == "DOLAR" || cboFormaPago.Text == "DOLAR EFECTIVO")
            {
                lblTotalAPagarDolares.Visible = true;
                lblRestaPagarDolares.Visible = true;
                txtTotalAPagarDolares.Visible = true;
                txtRestaPagarDolares.Visible = true;
            }
            // Obtén el recargo y descuento según la forma de pago seleccionada
            if(cboFormaPago.SelectedIndex != -1) { 
            decimal porcentajeRecargo = new CN_FormaPago().ObtenerFPPorDescripcion(cboFormaPago.Text).porcentajeRecargo;
            decimal porcentajeDescuento = new CN_FormaPago().ObtenerFPPorDescripcion(cboFormaPago.Text).porcentajeDescuento;
            
            // Convierte el total original a un valor decimal
            decimal totalOriginal = Convert.ToDecimal(txtTotalAPagar.Text);

            // Aplica el recargo y descuento al total original
            decimal totalConRecargo = (totalOriginal + (totalOriginal * porcentajeRecargo)) ;
            decimal totalConRecargoYDescuento = totalConRecargo - (totalConRecargo * porcentajeDescuento);
                
                // Actualiza el TextBox con el nuevo total
                txtTotalAPagar.Text = totalConRecargoYDescuento.ToString("0.00");
            }
            checkDescuento.Checked = false;
            checkRecargo.Checked = false;
            txtMontoDescuento.Text = "0";
            txtDescuento.Text = "0";
            txtMontoDescuento.Visible = false;
            lblDescuento.Visible = false;
            lblMontoDescuento.Visible = false;
            lblPorcentaje.Visible = false;
            checkDescuento.Visible = true;
            checkRecargo.Visible = true;
            txtPagaCon.Value = 0;
            CalcularRestaAPagar();

        }

        private void cboFormaPago2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFormaPago2.SelectedIndex != -1)
            {
                decimal porcentajeRecargo = new CN_FormaPago().ObtenerFPPorDescripcion(cboFormaPago2.Text).porcentajeRecargo;
                decimal porcentajeDescuento = new CN_FormaPago().ObtenerFPPorDescripcion(cboFormaPago2.Text).porcentajeDescuento;
                decimal totalRestoAPagar = txtRestaPagarDolares.Value * txtCotizacion.Value;

                if (cboFormaPago.Text == "DOLAR" || cboFormaPago.Text == "DOLAR EFECTIVO")
                {



                    decimal totalConRecargoYDescuento = (totalRestoAPagar + (totalRestoAPagar * porcentajeRecargo)) - (totalRestoAPagar * porcentajeDescuento);

                    // Actualiza el TextBox con el nuevo total
                    txtTotalAPagar.Text = totalConRecargoYDescuento.ToString("0.00");
                    CalcularRestaAPagar();
                }
                if (checkDescuento.Checked)
                {
                    totalRestoAPagar = txtRestaPagar.Value - Convert.ToDecimal(txtMontoDescuento.Text);
                    txtTotalAPagar.Text = totalRestoAPagar.ToString("0.00");
                    CalcularRestaAPagar();

                }

                if (checkRecargo.Checked)
                {
                    totalRestoAPagar = txtRestaPagar.Value + Convert.ToDecimal(txtMontoDescuento.Text);
                    txtTotalAPagar.Text = totalRestoAPagar.ToString("0.00");
                    CalcularRestaAPagar();

                }
            }
        }

        private void btnAgregarPagoParcial_Click(object sender, EventArgs e)
        {
            // Verificar que se haya seleccionado un cliente y que haya al menos una fila en el DataGridView
            if (txtIdCliente.Text != "0" && dgvData.Rows.Count > 0)
            {
                using (var modal = new mdAgregarPagoParcial())
                {
                    modal.IdCliente = int.Parse(txtIdCliente.Text); // Asigna el id del cliente al modal
                    var result = modal.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        txtMontoPagoParcial.Value = modal._PagoParcial.monto;
                        txtIdPagoParcial.Text = modal._PagoParcial.idPagoParcial.ToString();
                        txtCodigoProducto.Select();
                    }
                    else
                    {
                        txtDocumentoCliente.Select();
                    }
                }
            }
            else
            {
                // Mostrar mensajes de error según la condición que no se cumple
                if (txtIdCliente.Text == "0")
                {
                    MessageBox.Show("Debe seleccionar un Cliente para registrar un pago parcial.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (dgvData.Rows.Count == 0)
                {
                    MessageBox.Show("Debe tener al menos un producto en la lista para registrar un pago parcial.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void txtMontoPagoParcial_ValueChanged(object sender, EventArgs e)
        {
            CalcularRestaAPagar();
        }

        private void btnEliminarPagoParcial_Click(object sender, EventArgs e)
        {
            txtMontoPagoParcial.Value = 0;
            txtIdPagoParcial.Text = "0";
        }
    }
    }
