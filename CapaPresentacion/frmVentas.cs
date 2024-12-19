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
        private PagoParcial _pagoParcialGlobal;
        private bool pagoParcialDolaresContado = false;
        private bool pagoParcialDolaresDescontado = false;
        private decimal cotizacionOriginal;
        private int _idVentaGenerada;
        private bool _pagoParcialAplicado = false;
        private int contadorFormasPago = 0;
        private bool recargoAplicado = false;

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
                CargarComboBoxVendedores();
                CargarComboBoxFormaPago();
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
            txtCotizacion.Value = _Venta.cotizacionDolar;
            // Cargar los productos en el DataGridView
            foreach (var item in _Venta.oDetalleVenta)
            {
                dgvData.Rows.Add(item.oProducto.idProducto, item.oProducto.nombre, item.precioVenta, item.precioVenta * 1.30m, item.cantidad, item.subTotal, item.oProducto.prodSerializable);
            }
            // Obtener la lista de formas de pago cargadas en el ComboBox
            List<OpcionCombo> listaFormasPago = cboFormaPago.Items.Cast<OpcionCombo>().ToList();

            
            // Cargar las formas de pago en el DataGridView dgvDataFormasPago
            // Suponiendo que la forma de pago y los montos están definidos en las propiedades de _Venta
            if (!string.IsNullOrEmpty(_Venta.formaPago))
            {
                int idFormaPago = ObtenerIdFormaPago(_Venta.formaPago);
                dgvDataFormasPago.Rows.Add(idFormaPago, _Venta.formaPago, _Venta.montoFP1, _Venta.montoPago);
            }

            if (!string.IsNullOrEmpty(_Venta.formaPago2))
            {
                int idFormaPago2 = ObtenerIdFormaPago(_Venta.formaPago2);
                dgvDataFormasPago.Rows.Add(idFormaPago2, _Venta.formaPago2, _Venta.montoFP2, _Venta.montoPagoFP2);
            }

            if (!string.IsNullOrEmpty(_Venta.formaPago3))
            {
                int idFormaPago3 = ObtenerIdFormaPago(_Venta.formaPago3);
                dgvDataFormasPago.Rows.Add(idFormaPago3, _Venta.formaPago3, _Venta.montoFP3, _Venta.montoPagoFP3);
            }

            if (!string.IsNullOrEmpty(_Venta.formaPago4))
            {
                int idFormaPago4 = ObtenerIdFormaPago(_Venta.formaPago4);
                dgvDataFormasPago.Rows.Add(idFormaPago4, _Venta.formaPago4, _Venta.montoFP4, _Venta.montoPagoFP4);
            }

            // Mostrar el total a pagar en el TextBox correspondiente
            txtTotalAPagar.Text = _Venta.montoTotal.ToString("0.00");

            // Cargar el vendedor seleccionado
            OpcionCombo vendedorSeleccionado = cboVendedores.Items.Cast<OpcionCombo>()
                                            .FirstOrDefault(x => Convert.ToInt32(x.Valor) == _Venta.idVendedor);
            if (vendedorSeleccionado != null)
            {
                cboVendedores.SelectedItem = vendedorSeleccionado; // Establecer el item seleccionado
            }
        }


        private int ObtenerIdFormaPago(string nombreFormaPago)
        {
            // Buscar en los ítems del ComboBox donde el texto coincide con el nombre de la forma de pago
            var formaPago = cboFormaPago.Items
                                        .Cast<OpcionCombo>()
                                        .FirstOrDefault(x => x.Texto == nombreFormaPago);

            // Si encuentra la forma de pago, devuelve su id; si no, devuelve 0
            return formaPago != null ? Convert.ToInt32(formaPago.Valor) : 0;
        }

        private void CargarComboBoxFormaPago()
        {
            // Crear una instancia de la capa de negocio
            CN_FormaPago objCN_FormaPago = new CN_FormaPago();

            // Obtener la lista de formas de pago desde la base de datos
            List<FormaPago> listaFormaPago = objCN_FormaPago.ListarFormasDePago();

            // Limpiar los items actuales del ComboBox
            cboFormaPago.Items.Clear();
            

            // Llenar el ComboBox con los datos obtenidos
            foreach (FormaPago formaPago in listaFormaPago)
            {
                cboFormaPago.Items.Add(new OpcionCombo() { Valor = formaPago.idFormaPago, Texto = formaPago.descripcion });
                
            }

            // Establecer DisplayMember y ValueMember
            cboFormaPago.DisplayMember = "Texto";
            cboFormaPago.ValueMember = "Valor";
            

            // Seleccionar el primer item por defecto si hay elementos en el ComboBox
            if (cboFormaPago.Items.Count > 0)
            {
                cboFormaPago.SelectedIndex = -1;
               
            }
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            var cotizacionDolar = new CN_Cotizacion().CotizacionActiva();
            txtCotizacion.Value = cotizacionDolar.importe;
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


            if (decimal.TryParse(txtCotizacion.Text, out decimal valorCotizacion))
            {
                cotizacionOriginal = valorCotizacion;
            }
            else
            {
                cotizacionOriginal = 0; // Si no es un valor válido, considerarlo como 0
            }




            dtpFecha.Text = DateTime.Now.ToString();
            txtIdProducto.Text = "0";
            txtIdProducto.Text = "0";
            
            

            
            
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
                    txtPrecioLista.Text = modal._Producto.precioLista.ToString();
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
                    txtSerializable.Text = oProducto.prodSerializable.ToString();
                    txtPrecioLista.Text = oProducto.precioLista.ToString();
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

            if (esSerializable && txtCantidad.Value > 1)
            {
                MessageBox.Show("Solo se puede agregar un producto serializable a la vez.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                //decimal precioLista30 = Math.Round(((Math.Round((precio * txtCotizacion.Value) / 1000) * 1000 - 100) * 1.30m) / 1000, 0) * 1000 - 100;





                                // Agregar el producto a dgvData
                                 dgvData.Rows.Add(new object[]{
                            txtIdProducto.Text,
                            txtProducto.Text,
                            precio.ToString("0.00"),
                            txtPrecioLista.Text=="0.00"? (precio * cotizacionOriginal).ToString("0.00"):txtPrecioLista.Text,
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
                                CalcularRestaAPagar();
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
                    //decimal precioLista30 = Math.Round(((Math.Round((precio * txtCotizacion.Value) / 1000) * 1000 - 100) * 1.30m) / 1000, 0) * 1000 - 100;


                    // Agregar el producto a dgvData directamente
                    dgvData.Rows.Add(new object[]{
                txtIdProducto.Text,
                txtProducto.Text,
                precio.ToString("0.00"),
                txtPrecioLista.Text=="0.00"? (precio * cotizacionOriginal).ToString("0.00"):txtPrecioLista.Text,
                txtCantidad.Value.ToString(),
                (txtCantidad.Value * precio).ToString("0.00"),
                txtSerializable.Text,
                defaultImage
            });

                    calcularTotal();
                    CalcularRestaAPagar();
                    limpiarProducto();
                    txtCodigoProducto.Select();
                    
                }
            }
            else
            {
                MessageBox.Show("El producto ya existe en la lista y no es serializable.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        //private void calcularTotal()
        //{
        //    decimal total = 0;
        //    if (dgvData.Rows.Count > 0)
        //    {
        //        foreach (DataGridViewRow row in dgvData.Rows)
        //        {
        //            total += Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString());
        //        }

        //        decimal totalCotizado = total * txtCotizacion.Value;
        //        decimal totalProductos;

        //        // Verifica el estado de la variable cotizacionCambio
        //        if (cotizacionCambio)
        //        {
        //            totalProductos = totalCotizado; // Usa el valor exacto si la cotización cambió
        //        }
        //        else
        //        {
        //            totalProductos = Math.Round(totalCotizado / 1000, 0) * 1000 - 100;
        //        }

        //        txtTotalAPagar.Value = totalProductos;
        //        txtTotalAPagarDolares.Value = total;

        //        // Solo asigna RestaPagar si es la primera vez
        //        if (txtRestaPagar.Value == 0 && txtRestaPagarDolares.Value == 0)
        //        {
        //            txtRestaPagar.Value = txtTotalAPagar.Value;
        //            txtRestaPagarDolares.Value = total;
        //        }
        //    }
        //}

        private decimal calcularTotal()
        {
            decimal totalPesos = 0;
            decimal totalDolares = 0;

            // Verificamos si el DataGridView tiene filas
            if (dgvData.Rows.Count > 0)
            {
                // Recorremos las filas y sumamos los totales
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    // Validamos que las celdas no sean nulas antes de convertir
                    if (row.Cells["precioLista"].Value != null && row.Cells["subTotal"].Value != null)
                    {
                        totalPesos += Convert.ToDecimal(row.Cells["precioLista"].Value);
                        totalDolares += Convert.ToDecimal(row.Cells["subTotal"].Value);
                    }
                }

                // Ajustamos el total si la cotización ha cambiado
                if (cotizacionCambio)
                {
                    totalPesos = Math.Round(totalDolares * txtCotizacion.Value, 2);
                }

                // Actualizamos los TextBox correspondientes
                txtTotalAPagar.Value = totalPesos;
                txtTotalAPagarDolares.Value = totalDolares;

                // Solo asignamos RestaPagar si es la primera vez
                if (txtRestaPagar.Value == 0 && txtRestaPagarDolares.Value == 0)
                {
                    txtRestaPagar.Value = totalPesos;
                    txtRestaPagarDolares.Value = totalDolares;
                }
            }

            // Retornamos el total en pesos
            return txtTotalAPagar.Value;
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


        //private void CalcularCambio()
        //{
        //    if (txtTotalAPagar.Text.Trim() == "")
        //    {
        //        MessageBox.Show("No existen productos en la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        return;
        //    }

        //    decimal pagacon;
        //    if(cboFormaPago.Text == "DOLAR" || cboFormaPago.Text == "DOLAR EFECTIVO")
        //    {

        //        pagacon = 0;
        //    }
        //    else
        //    {
        //        pagacon = Convert.ToDecimal(txtPagaCon.Text);
        //    }

        //   if( txtPagaCon2.Text != string.Empty)
        //    {
        //        pagacon +=  Convert.ToDecimal(txtPagaCon2.Text);

        //    }else
        //    {
        //        pagacon +=  0;
        //    }

        //    if (txtPagaCon3.Text != string.Empty)
        //    {
        //        pagacon +=  Convert.ToDecimal(txtPagaCon3.Text);

        //    }
        //    else
        //    {
        //        pagacon += 0;
        //    }

        //    if (txtPagaCon4.Text != string.Empty)
        //    {
        //        pagacon += Convert.ToDecimal(txtPagaCon4.Text);

        //    }
        //    else
        //    {
        //        pagacon = pagacon + 0;
        //    }


        //    decimal total = Convert.ToDecimal(txtTotalAPagar.Text);


        //    if (txtPagaCon.Text.Trim() == "")
        //    {
        //        txtPagaCon.Text = "0";
        //    }

        //        if (pagacon < total)
        //        {
        //            txtCambioCliente.Text = "0.00";

        //        }
        //        else
        //        {
        //            decimal cambio = pagacon - total;
        //            txtCambioCliente.Text = cambio.ToString("0.00");
        //        }

        //}

        private void CalcularCambio()
        {
            // Verifica que haya valores en los campos de resta antes de calcular el cambio
            decimal restoAPagar = txtRestaPagar.Value;
            decimal restoAPagarDolares = txtRestaPagarDolares.Value;

            // Asegura que haya valores válidos en los campos de resta
            if (restoAPagar == 0 && restoAPagarDolares == 0)
            {
                //MessageBox.Show("El monto restante a pagar es cero", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            decimal pagoTotal = 0;
            decimal pagoTotalDolares = 0;

            // Recorre las filas del DataGridView para sumar los pagos
            foreach (DataGridViewRow fila in dgvDataFormasPago.Rows)
            {
                string formaPago = fila.Cells["FormaPago"].Value.ToString();
                decimal montoPago = Convert.ToDecimal(fila.Cells["importeFP"].Value);

                if (formaPago == "DOLAR" || formaPago == "DOLAR EFECTIVO")
                {
                    pagoTotalDolares += montoPago;
                }
                else
                {
                    pagoTotal += montoPago;
                }
            }

            // Si el total de pago en pesos cubre el total a pagar en pesos
            decimal cambioPesos = pagoTotal >= restoAPagar ? (pagoTotal - restoAPagar) : 0;
            // Si el total de pago en dólares cubre el total a pagar en dólares
            decimal cambioDolares = pagoTotalDolares >= restoAPagarDolares ? (pagoTotalDolares - restoAPagarDolares) : 0;

            // Mostrar el cambio en pesos y dólares
            txtCambioCliente.Text = (cambioPesos + cambioDolares).ToString("0.00");
        }






        private void txtPagaCon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (dgvData.Rows.Count <= 0)
                {
                    MessageBox.Show("No hay Productos para aplicar le Pago", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (contadorFormasPago >= 4)
                {
                    MessageBox.Show("Solo se pueden agregar hasta 4 formas de pago.", "Límite alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboFormaPago.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtPagaCon.Text))
                {
                    MessageBox.Show("Seleccione una forma de pago e ingrese un monto válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!decimal.TryParse(txtPagaCon.Text, out decimal montoPago) || montoPago <= 0)
                {
                    MessageBox.Show("Ingrese un monto válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener el elemento seleccionado como objeto OpcionCombo
                var formaPagoSeleccionada = (OpcionCombo)cboFormaPago.SelectedItem;

                if (formaPagoSeleccionada == null)
                {
                    MessageBox.Show("Forma de pago seleccionada no válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int idFormaPago = (int)formaPagoSeleccionada.Valor;
                string formaPago = formaPagoSeleccionada.Texto;




                FormaPago formaPagoADescontarRetencion = new CN_FormaPago().ObtenerFPPorDescripcion(((OpcionCombo)cboFormaPago.SelectedItem).Texto);
                decimal montoMenosRetencion = Math.Round(montoPago - (montoPago * formaPagoADescontarRetencion.porcentajeRetencion) / 100,2);



                // Agregar al DataGridView con idFormaPago, formaPago y montoPago
                dgvDataFormasPago.Rows.Add(idFormaPago, formaPago, montoPago, montoMenosRetencion, defaultImage);

                // Incrementar el contador
                contadorFormasPago++;

                

                // Limpiar campos
                cboFormaPago.SelectedIndex = -1;
                
                txtPagaCon.ResetText();

                // Mensaje si se alcanzó el límite
                if (contadorFormasPago == 4)
                {
                    MessageBox.Show("Se han agregado las 4 formas de pago permitidas.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        //        private void btnRegistrarCompra_Click(object sender, EventArgs e)
        //        {
        //            if (txtDocumentoCliente.Text == "")
        //            {
        //                MessageBox.Show("Debe ingresar el documento del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                return;
        //            }

        //            if (txtNombreCliente.Text == "")
        //            {
        //                MessageBox.Show("Debe ingresar el nombre del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                return;
        //            }

        //            if (dgvData.Rows.Count < 1)
        //            {
        //                MessageBox.Show("Debe ingresar productos en la Venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                return;
        //            }
        //            if (checkDescuento.Checked && txtDescuento.Text == "")
        //            {
        //                MessageBox.Show("Debe ingresar un porcentaje de descuento", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                return;
        //            }

        //            if ((txtPagaCon.Value == 0)  && (txtPagaCon2.Value == 0) && (txtPagaCon3.Value == 0) && (txtPagaCon4.Value == 0))
        //            {
        //                MessageBox.Show("No ha establecido ningun monto para la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                return;
        //            }

        //            if (cboFormaPago.SelectedIndex == -1 &&  cboFormaPago2.SelectedIndex == -1 && cboFormaPago3.SelectedIndex == -1 && cboFormaPago4.SelectedIndex == -1)
        //            {
        //                MessageBox.Show("Debe seleccionar al menos una forma de pago", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                return;
        //            }

        //            //if(cboVendedores.SelectedIndex == -1)
        //            //{
        //            //    MessageBox.Show("Debe Seleccionar un Vendedor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //            //    return;
        //            //}
        //            DataTable detalle_venta = new DataTable();

        //            detalle_venta.Columns.Add("idProducto", typeof(int));
        //            detalle_venta.Columns.Add("precioVenta", typeof(decimal));
        //            detalle_venta.Columns.Add("cantidad", typeof(decimal));
        //            detalle_venta.Columns.Add("subTotal", typeof(decimal));


        //            foreach (DataGridViewRow row in dgvData.Rows)
        //            {

        //                detalle_venta.Rows.Add(
        //                    new object[]
        //                    {
        //                        Convert.ToInt32(row.Cells["idProducto"].Value.ToString()),

        //                        row.Cells["precio"].Value.ToString(),
        //                        row.Cells["cantidad"].Value.ToString(),
        //                        row.Cells["subTotal"].Value.ToString()
        //                    });
        //            }

        //            int idCorrelativo = new CN_Venta().ObtenerCorrelativo();
        //            string numeroDocumento = string.Format("{0:00000}", idCorrelativo);
        //            CalcularCambio();
        //            decimal montoPagado = 0;
        //            decimal montoPagadoFP2 = 0;
        //            decimal montoPagadoFP3 = 0;
        //            decimal montoPagadoFP4 = 0;
        //            if (cboFormaPago.SelectedItem != null)
        //            {
        //                FormaPago fp1 = new CN_FormaPago().ObtenerFPPorDescripcion(((OpcionCombo)cboFormaPago.SelectedItem).Texto);
        //                if (txtPagaCon.Text != string.Empty)
        //                {
        //                    montoPagado = montoPagado + Convert.ToDecimal(txtPagaCon.Text) - (Convert.ToDecimal(txtPagaCon.Text) * fp1.porcentajeRetencion) / 100;
        //                }
        //            }

        //            if (cboFormaPago2.SelectedItem != null)
        //            {
        //                FormaPago fp2 = new CN_FormaPago().ObtenerFPPorDescripcion(((OpcionCombo)cboFormaPago2.SelectedItem).Texto);
        //                if (txtPagaCon2.Text != string.Empty)
        //                {
        //                    montoPagadoFP2 = montoPagadoFP2 + Convert.ToDecimal(txtPagaCon2.Text) - (Convert.ToDecimal(txtPagaCon2.Text) * fp2.porcentajeRetencion) / 100;
        //                }
        //            }
        //            if (cboFormaPago3.SelectedItem != null)
        //            {
        //                FormaPago fp3 = new CN_FormaPago().ObtenerFPPorDescripcion(((OpcionCombo)cboFormaPago3.SelectedItem).Texto);
        //                if (txtPagaCon3.Text != string.Empty)
        //                {
        //                    montoPagadoFP3 = montoPagadoFP3 + Convert.ToDecimal(txtPagaCon3.Text) - (Convert.ToDecimal(txtPagaCon3.Text) * fp3.porcentajeRetencion) / 100;
        //                }
        //            }
        //            if (cboFormaPago4.SelectedItem != null)
        //            {
        //                FormaPago fp4 = new CN_FormaPago().ObtenerFPPorDescripcion(((OpcionCombo)cboFormaPago4.SelectedItem).Texto);
        //                if (txtPagaCon4.Text != string.Empty)
        //                {
        //                    montoPagadoFP4 = montoPagadoFP4 + Convert.ToDecimal(txtPagaCon4.Text) - (Convert.ToDecimal(txtPagaCon4.Text) * fp4.porcentajeRetencion) / 100;
        //                }
        //            }














        //            if (txtCotizacion.Value != 0)
        //{
        //    Venta oVenta = new Venta()
        //    {
        //        oUsuario = new Usuario() { idUsuario = _Usuario.idUsuario },
        //        idNegocio = GlobalSettings.SucursalId,
        //        fechaRegistro = Convert.ToDateTime(dtpFecha.Value),
        //        tipoDocumento = ((OpcionCombo)cboTipoDocumento.SelectedItem).Texto,
        //        nroDocumento = numeroDocumento,
        //        documentoCliente = txtDocumentoCliente.Text,
        //        nombreCliente = txtNombreCliente.Text,
        //        idVendedor = (OpcionCombo)cboVendedores.SelectedItem==null?_Venta.idVendedor: Convert.ToInt32(((OpcionCombo)cboVendedores.SelectedItem).Valor),
        //        montoCambio = Convert.ToDecimal(txtCambioCliente.Text),
        //        montoTotal = Convert.ToDecimal(txtTotalAPagar.Text)==0? Convert.ToDecimal(txtTotalAPagarDolares.Text): Convert.ToDecimal(txtTotalAPagar.Text),
        //        formaPago = cboFormaPago.SelectedItem != null ? ((OpcionCombo)cboFormaPago.SelectedItem).Texto : "",
        //        formaPago2 = cboFormaPago2.SelectedItem != null ? ((OpcionCombo)cboFormaPago2.SelectedItem).Texto : "",
        //        formaPago3 = cboFormaPago3.SelectedItem != null ? ((OpcionCombo)cboFormaPago3.SelectedItem).Texto : "",
        //        formaPago4 = cboFormaPago4.SelectedItem != null ? ((OpcionCombo)cboFormaPago4.SelectedItem).Texto : "",

        //                descuento = Convert.ToDecimal(txtDescuento.Text),
        //        montoDescuento = Convert.ToDecimal(txtMontoDescuento.Text),
        //        cotizacionDolar = txtCotizacion.Value,


        //        montoFP1 = txtPagaCon.Text != string.Empty ?  Convert.ToDecimal(txtPagaCon.Text): 0,
        //        montoFP2 = txtPagaCon2.Text != string.Empty ? Convert.ToDecimal(txtPagaCon2.Text) : 0,
        //        montoFP3 = txtPagaCon3.Text != string.Empty ? Convert.ToDecimal(txtPagaCon3.Text) : 0,
        //        montoFP4 = txtPagaCon4.Text != string.Empty ? Convert.ToDecimal(txtPagaCon4.Text) : 0,

        //        montoPago = montoPagado,
        //        montoPagoFP2 = montoPagadoFP2,
        //        montoPagoFP3 = montoPagadoFP3,
        //        montoPagoFP4 = montoPagadoFP4,
        //        observaciones = txtObservaciones.Text


        //    };
        //                string actualizacionStock = string.Empty;
        //                bool actualizarSerial = false;
        //                string mensaje = string.Empty;
        //                string mensajeSerialActualizado = string.Empty;
        //                int idVentaGenerado = 0;
        //                bool respuesta = false;
        //                string tipo = string.Empty;
        //                if (oVenta.idVenta == 0)
        //                {
        //                     respuesta = new CN_Venta().Registrar(oVenta, detalle_venta, out mensaje, out idVentaGenerado);
        //                    tipo = "Generado";
        //                }
        //                else
        //                {
        //                    oVenta.idVenta = _Venta.idVenta;
        //                    respuesta = new CN_Venta().EditarVenta(oVenta, detalle_venta, out mensaje, out idVentaGenerado);
        //                    tipo = "Modificado";
        //                }
        //                if (respuesta)

        //    {           
        //                    if(txtIdPagoParcial.Text != "0")
        //                    {
        //                        bool darBajaPagoParcial = new CN_PagoParcial().DarDeBajaPagoParcial(Convert.ToInt32(txtIdPagoParcial.Text));
        //                        if (darBajaPagoParcial == false) {
        //                            MessageBox.Show("no se pudo dar de baja el pago parcial", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                        }
        //                    }
        //                    var perteneceANegocio = new CN_ClienteNegocio().ClienteAsignadoANegocio(Convert.ToInt32(txtIdCliente.Text), GlobalSettings.SucursalId);
        //                    if (!perteneceANegocio)
        //                    {
        //                        var asignarCliente = new CN_ClienteNegocio().AsignarClienteANegocio(Convert.ToInt32(txtIdCliente.Text), GlobalSettings.SucursalId);
        //                    }

        //                    foreach (DataGridViewRow row in dgvData.Rows)
        //                    {
        //                        if (row.Cells["idProducto"].Value != null && row.Cells["cantidad"].Value != null)
        //                        {
        //                            int idProducto = Convert.ToInt32(row.Cells["idProducto"].Value);
        //                            int cantidad = Convert.ToInt32(row.Cells["cantidad"].Value);

        //                            // Actualizar el stock del producto
        //                            actualizacionStock= new CN_ProductoNegocio().CargarOActualizarStockProducto(idProducto, GlobalSettings.SucursalId, -cantidad);
        //                        }
        //                    }

        //                    if (dgvSeriales.Rows.Count > 0)
        //                    {
        //                        foreach (DataGridViewRow row in dgvSeriales.Rows)
        //                        {
        //                            if (row.Cells["idProductoDetalle"].Value != null) // Verificar que la celda no esté vacía
        //                            {
        //                                int idProductoDetalle = Convert.ToInt32(row.Cells["idProductoDetalle"].Value);
        //                                actualizarSerial = new CN_Producto().DesactivarProductoDetalle(idProductoDetalle, idVentaGenerado, out  mensaje);
        //                                if (actualizarSerial)
        //                                {
        //                                    mensajeSerialActualizado = "Se han dado de Baja el o los Numero de Serie";
        //                                } else
        //                                {
        //                                    MessageBox.Show($"Error al dar de Baja el Numero de Serie: {mensaje}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                                    break; // Detener el loop si ocurre un error
        //                                }



        //                            }
        //                        }
        //                    }


        //                    txtIdProducto.Text = "0";
        //                    List<string> formasPago = new List<string>();
        //                    formasPago.Add(cboFormaPago.Text);
        //                    if (cboFormaPago2.SelectedIndex >= 0) formasPago.Add(cboFormaPago2.Text);
        //                    if (cboFormaPago3.SelectedIndex >= 0) formasPago.Add(cboFormaPago3.Text);
        //                    if (cboFormaPago4.SelectedIndex >= 0) formasPago.Add(cboFormaPago4.Text);
        //                    string nombreCliente = txtNombreCliente.Text;
        //                    List<CajaRegistradora> lista = new CN_CajaRegistradora().Listar(GlobalSettings.SucursalId);

        //                    CajaRegistradora cajaAbierta = lista.Where(c => c.estado == true).FirstOrDefault();

        //                    if (cajaAbierta != null)

        //                    {

        //                        if(oVenta.montoPago > 0) {
        //                            var cajaAsociadaFP1 = new CN_FormaPago().ObtenerFPPorDescripcion(oVenta.formaPago).cajaAsociada;
        //                            TransaccionCaja objTransaccion = new TransaccionCaja()
        //                        {
        //                            idCajaRegistradora = cajaAbierta.idCajaRegistradora,

        //                            hora = dtpFecha.Value.Hour.ToString(),
        //                            tipoTransaccion = "ENTRADA",
        //                            monto = oVenta.montoPago,
        //                            docAsociado = "Venta Numero:" + " " + numeroDocumento + " Cliente:" + " " + nombreCliente,
        //                            usuarioTransaccion = cboVendedores.Text,
        //                            formaPago = cboFormaPago.Text,
        //                            cajaAsociada = cajaAsociadaFP1,
        //                            idVenta = idVentaGenerado,
        //                            idCompra = null,
        //                            idNegocio = GlobalSettings.SucursalId,
        //                            concepto = "VENTA",
        //                            idPagoParcial = null

        //                            };




        //                        int idTransaccionGenerado = new CN_Transaccion().RegistrarMovimiento(objTransaccion, out mensaje);

        //                        }

        //                        if (oVenta.montoPagoFP2 > 0)
        //                        {
        //                            var cajaAsociadaFP2 = new CN_FormaPago().ObtenerFPPorDescripcion(oVenta.formaPago2).cajaAsociada;
        //                            TransaccionCaja objTransaccion2 = new TransaccionCaja()
        //                            {
        //                                idCajaRegistradora = cajaAbierta.idCajaRegistradora,

        //                                hora = dtpFecha.Value.Hour.ToString(),
        //                                tipoTransaccion = "ENTRADA",
        //                                monto = oVenta.montoPagoFP2,
        //                                docAsociado = "Venta Numero:" + " " + numeroDocumento + " Cliente:" + " " + nombreCliente,
        //                                usuarioTransaccion = cboVendedores.Text,
        //                                formaPago = cboFormaPago2.Text,
        //                                cajaAsociada = cajaAsociadaFP2,
        //                                idVenta = idVentaGenerado,
        //                                idCompra = null,
        //                                idNegocio = GlobalSettings.SucursalId,
        //                                concepto = "VENTA",
        //                                idPagoParcial = null
        //                            };




        //                            int idTransaccionGenerado = new CN_Transaccion().RegistrarMovimiento(objTransaccion2, out mensaje);
        //                        }

        //                        if (oVenta.montoPagoFP3 > 0)
        //                        {
        //                            var cajaAsociadaFP3 = new CN_FormaPago().ObtenerFPPorDescripcion(oVenta.formaPago3).cajaAsociada;
        //                            TransaccionCaja objTransaccion3 = new TransaccionCaja()
        //                            {
        //                                idCajaRegistradora = cajaAbierta.idCajaRegistradora,

        //                                hora = dtpFecha.Value.Hour.ToString(),
        //                                tipoTransaccion = "ENTRADA",
        //                                monto = oVenta.montoPagoFP3,
        //                                docAsociado = "Venta Numero:" + " " + numeroDocumento + " Cliente:" + " " + nombreCliente,
        //                                usuarioTransaccion = cboVendedores.Text,
        //                                formaPago = cboFormaPago3.Text,
        //                                cajaAsociada = cajaAsociadaFP3,
        //                                idVenta = idVentaGenerado,
        //                                idCompra = null,
        //                                idNegocio = GlobalSettings.SucursalId,
        //                                concepto = "VENTA",
        //                                idPagoParcial = null
        //                            };




        //                            int idTransaccionGenerado = new CN_Transaccion().RegistrarMovimiento(objTransaccion3, out mensaje);
        //                        }

        //                        if (oVenta.montoPagoFP4 > 0)
        //                        {
        //                            var cajaAsociadaFP4 = new CN_FormaPago().ObtenerFPPorDescripcion(oVenta.formaPago4).cajaAsociada;
        //                            TransaccionCaja objTransaccion4 = new TransaccionCaja()
        //                            {
        //                                idCajaRegistradora = cajaAbierta.idCajaRegistradora,

        //                                hora = dtpFecha.Value.Hour.ToString(),
        //                                tipoTransaccion = "ENTRADA",
        //                                monto = oVenta.montoPagoFP4,
        //                                docAsociado = "Venta Numero:" + " " + numeroDocumento + " Cliente:" + " " + nombreCliente,
        //                                usuarioTransaccion = cboVendedores.Text,
        //                                formaPago = cboFormaPago4.Text,
        //                                cajaAsociada= cajaAsociadaFP4,
        //                                idVenta = idVentaGenerado,
        //                                idCompra = null,
        //                                idNegocio = GlobalSettings.SucursalId,
        //                                concepto = "VENTA",
        //                                idPagoParcial = null
        //                            };




        //                            int idTransaccionGenerado = new CN_Transaccion().RegistrarMovimiento(objTransaccion4, out mensaje);
        //                        }

        //                    }


        //                    var result = MessageBox.Show("Numero de Venta:\n"+ tipo + numeroDocumento +". " + mensajeSerialActualizado, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        if (result == DialogResult.OK)

        //            Clipboard.SetText(numeroDocumento);
        //                    txtIdCliente.Text = string.Empty;
        //                    dgvSeriales.Rows.Clear();
        //                    dgvSeriales.Visible = false;
        //                    txtObservaciones.Text = string.Empty;
        //        txtDocumentoCliente.Text = "";
        //        txtNombreCliente.Text = "";
        //        dgvData.Rows.Clear();
        //                    txtIdPagoParcial.Text = "0";
        //        calcularTotal();
        //        txtPagaCon.Text = "";
        //        txtCambioCliente.Text = "";
        //        cboFormaPago.SelectedIndex = -1;
        //        txtTotalVentaDolares.Text = string.Empty;
        //        cboVendedores.SelectedIndex = -1;
        //        cboFormaPago2.SelectedIndex = -1;
        //        cboFormaPago3.SelectedIndex = -1;
        //        cboFormaPago4.SelectedIndex = -1;
        //                    txtTotalAPagar.Text = string.Empty;
        //                    txtPagaCon.Text = string.Empty;
        //                    txtPagaCon2.Text = string.Empty;
        //                    txtPagaCon3.Text = string.Empty;
        //                    txtPagaCon4.Text = string.Empty;
        //                    txtRestaPagar.Text = string.Empty;
        //                    isUpdated = false;
        //                    lblTotalAPagarDolares.Visible = false;
        //                    lblRestaPagarDolares.Visible = false;
        //                    txtTotalAPagarDolares.Visible = false;
        //                    txtRestaPagarDolares.Visible = false;
        //                    checkDescuento.Checked = false;





        //    }
        //    else
        //    {
        //        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //}else
        //{
        //                MessageBox.Show("Debe ingresar la cotizacion del dolar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                return;

        //        }
        //        }

        private void checkDescuento_CheckedChanged(object sender, EventArgs e)
        {
            if (checkDescuento.Checked)
            {
                // Mostrar y habilitar controles relacionados con el descuento
                txtDescuento.Visible = true;
                txtDescuento.Enabled = true;
                txtMontoDescuento.Visible = true;
                txtMontoDescuento.Enabled = true;
                lblPorcentaje.Visible = true;
                txtMontoDescuento.Text = "0";
                txtDescuento.Text = "0";
                lblFormaPago.Visible = false;
                lblImporte.Visible = false;
                cboFormaPago.SelectedIndex = -1;
                cboFormaPago.Visible = false;
                txtPagaCon.ResetText();
                txtPagaCon.Visible = false;
                btnAgregarPago.Visible = false;

                // Ocultar controles relacionados con el recargo
                checkRecargo.Visible = false;
                checkMonedaDolar.Visible = true;
            }
            else
            {
                // Ocultar controles relacionados con el descuento
                txtDescuento.Visible = false;
                txtMontoDescuento.Visible = false;
                txtMontoDescuento.Text = "0";
                txtDescuento.Text = "0";
                lblPorcentaje.Visible = false;
                lblDescuento.Visible = false;

                // Mostrar controles relacionados con el recargo
                checkRecargo.Visible = true;

                // Limpiar opciones de forma de pago
                cboFormaPago.SelectedIndex = -1;
               

                // Ocultar moneda en dólares
                checkMonedaDolar.Visible = false;
                lblFormaPago.Visible = true;
                lblImporte.Visible = true;
                cboFormaPago.SelectedIndex = -1;
                cboFormaPago.Visible = true;
                txtPagaCon.ResetText();
                txtPagaCon.Visible = true;
                btnAgregarPago.Visible = true;

                // Recalcular el total después de desmarcar el descuento
                calcularTotal();
            }
        }

        private void RegistrarTransaccionesCaja(Venta oVenta, int idVentaGenerado)
        {
            // Obtener la caja abierta
            List<CajaRegistradora> lista = new CN_CajaRegistradora().Listar(GlobalSettings.SucursalId);
            CajaRegistradora cajaAbierta = lista.FirstOrDefault(c => c.estado == true);

            if (cajaAbierta != null)
            {
                // Crear un diccionario de formas de pago con sus montos asociados
                var formasDePago = new Dictionary<string, decimal>();

                // Verificar si la forma de pago ya está en el diccionario antes de agregarla
                if (!formasDePago.ContainsKey(oVenta.formaPago) && oVenta.montoPago > 0)
                {
                    formasDePago.Add(oVenta.formaPago, oVenta.montoPago);
                }

                if (!formasDePago.ContainsKey(oVenta.formaPago2) && oVenta.montoPagoFP2 > 0)
                {
                    formasDePago.Add(oVenta.formaPago2, oVenta.montoPagoFP2);
                }

                if (!formasDePago.ContainsKey(oVenta.formaPago3) && oVenta.montoPagoFP3 > 0)
                {
                    formasDePago.Add(oVenta.formaPago3, oVenta.montoPagoFP3);
                }

                if (!formasDePago.ContainsKey(oVenta.formaPago4) && oVenta.montoPagoFP4 > 0)
                {
                    formasDePago.Add(oVenta.formaPago4, oVenta.montoPagoFP4);
                }

                // Registrar las transacciones correspondientes a las formas de pago
                foreach (var pago in formasDePago)
                {
                    // Solo registrar transacción si el monto es mayor que 0
                    if (pago.Value > 0)
                    {
                        // Obtener la caja asociada a la forma de pago
                        var cajaAsociada = new CN_FormaPago().ObtenerFPPorDescripcion(pago.Key).cajaAsociada;

                        // Crear el objeto de transacción
                        TransaccionCaja objTransaccion = new TransaccionCaja()
                        {
                            idCajaRegistradora = cajaAbierta.idCajaRegistradora,
                            hora = dtpFecha.Value.Hour.ToString(),
                            tipoTransaccion = "ENTRADA", // Tipo de transacción
                            monto = pago.Value,
                            docAsociado = $"Venta Numero: {oVenta.nroDocumento} Cliente: {oVenta.nombreCliente}",
                            usuarioTransaccion = cboVendedores.Text,
                            formaPago = pago.Key,
                            cajaAsociada = cajaAsociada,
                            idVenta = idVentaGenerado,
                            idCompra = null,
                            idNegocio = GlobalSettings.SucursalId,
                            concepto = "VENTA",
                            idPagoParcial = null
                        };

                        // Registrar la transacción en la base de datos
                        string mensaje;
                        int idTransaccionGenerado = new CN_Transaccion().RegistrarMovimiento(objTransaccion, out mensaje);
                    }
                }
            }
        }

        private void LimpiarVenta()
        {
            
            txtIdCliente.Text = string.Empty;
            dgvSeriales.Rows.Clear();
            dgvDataFormasPago.Rows.Clear();
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
           
            txtTotalAPagar.Text = string.Empty;
            txtPagaCon.Text = string.Empty;
           
            txtRestaPagar.Text = string.Empty;
            isUpdated = false;
            lblTotalAPagarDolares.Visible = false;
            lblRestaPagarDolares.Visible = false;
            txtTotalAPagarDolares.Visible = false;
            txtRestaPagarDolares.Visible = false;
            checkDescuento.Checked = false;
        }

        private void btnRegistrarCompra_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (!ValidarFormulario()) return;

            // Crear tabla de detalle de venta
            DataTable detalleVenta = CrearDetalleVenta();

            // Generar la venta
            Venta oVenta = CrearVenta(detalleVenta);

            // Procesar registro de venta
            string mensaje = string.Empty;
            int idVentaGenerado;
            bool respuesta = ProcesarVenta(oVenta, detalleVenta, out mensaje, out idVentaGenerado);

            // Verificar si la venta fue procesada correctamente
            if (respuesta)
            {
                bool stockActualizado = false;
                bool transaccionRegistrada = false;
                
                
                
                string tipo = string.Empty;
                try
                {
                    if (txtIdPagoParcial.Text != "0")
                    {
                        bool darBajaPagoParcial = new CN_PagoParcial().DarDeBajaPagoParcial(Convert.ToInt32(txtIdPagoParcial.Text));
                        if (darBajaPagoParcial == false)
                        {
                            MessageBox.Show("No se pudo dar de baja el pago parcial", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    var perteneceANegocio = new CN_ClienteNegocio().ClienteAsignadoANegocio(Convert.ToInt32(txtIdCliente.Text), GlobalSettings.SucursalId);
                    if (!perteneceANegocio)
                    {
                        var asignarCliente = new CN_ClienteNegocio().AsignarClienteANegocio(Convert.ToInt32(txtIdCliente.Text), GlobalSettings.SucursalId);
                    }

                   

                    // Actualizar el stock
                    ActualizarStock(detalleVenta);
                    stockActualizado = true;

                    // Registrar la transacción en la caja
                    RegistrarTransaccionesCaja(oVenta, idVentaGenerado);
                    transaccionRegistrada = true;

                    // Si todo fue exitoso, mostrar el mensaje final
                    MessageBox.Show($"Venta registrada correctamente Numero : {oVenta.nroDocumento}. Stock actualizado y transacción registrada en la caja.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar los campos de la venta
                    LimpiarVenta();
                    oVenta = null;
                }
                catch (Exception ex)
                {
                    // Si hubo un error en el proceso de actualización de stock o transacción, mostrar el error
                    MessageBox.Show($"Error en el proceso: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Si no se pudo actualizar el stock o registrar la transacción, mostrar un error
                if (!stockActualizado || !transaccionRegistrada)
                {
                    MessageBox.Show("Hubo un error al completar el proceso de venta. Intente nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Si hubo un error en el procesamiento de la venta, mostrar el mensaje de error
                MessageBox.Show($"Error al procesar la venta: {mensaje}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool ValidarFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtDocumentoCliente.Text))
                return MostrarMensajeError("Debe ingresar el documento del cliente");

            if (string.IsNullOrWhiteSpace(txtNombreCliente.Text))
                return MostrarMensajeError("Debe ingresar el nombre del cliente");

            if (dgvData.Rows.Count < 1)
                return MostrarMensajeError("Debe ingresar productos en la venta");

            if (checkDescuento.Checked && string.IsNullOrWhiteSpace(txtDescuento.Text))
                return MostrarMensajeError("Debe ingresar un porcentaje de descuento");

            // Verificar si el DataGridView de formas de pago tiene filas
            if (dgvDataFormasPago.Rows.Count < 1)
                return MostrarMensajeError("Debe agregar al menos una forma de pago al detalle de la venta");

            if (cboVendedores.SelectedIndex == -1)
                return MostrarMensajeError("Debe seleccionar el Vendedor");

            return true;
        }


        private bool MostrarMensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return false;
        }

        private DataTable CrearDetalleVenta()
        {
            DataTable detalleVenta = new DataTable();
            detalleVenta.Columns.Add("idProducto", typeof(int));
            detalleVenta.Columns.Add("precioVenta", typeof(decimal));
            detalleVenta.Columns.Add("cantidad", typeof(decimal));
            detalleVenta.Columns.Add("subTotal", typeof(decimal));

            foreach (DataGridViewRow row in dgvData.Rows)
            {
                detalleVenta.Rows.Add(
                    Convert.ToInt32(row.Cells["idProducto"].Value),
                    Convert.ToDecimal(row.Cells["precio"].Value),
                    Convert.ToDecimal(row.Cells["cantidad"].Value),
                    Convert.ToDecimal(row.Cells["subTotal"].Value)
                );
            }
            return detalleVenta;
        }

        private Venta CrearVenta(DataTable detalleVenta)
        {
            // Inicializar las variables para los montos de pago
            decimal montoPagoFP1 = 0, montoPagoFP2 = 0, montoPagoFP3 = 0, montoPagoFP4 = 0;
            string formaPagoFP1 = "", formaPagoFP2 = "", formaPagoFP3 = "", formaPagoFP4 = "";
            decimal montoRecibidoPagoFP1 = 0, montoRecibidoPagoFP2 = 0, montoRecibidoPagoFP3 = 0, montoRecibidoPagoFP4 = 0;

            // Recorrer el DataGridView de formas de pago para obtener los valores
            for (int i = 0; i < dgvDataFormasPago.Rows.Count; i++)
            {
                var fila = dgvDataFormasPago.Rows[i];
                int idFormaPago = Convert.ToInt32(fila.Cells["idFormaPago"].Value);

                // Saltar filas con idFormaPago igual a 9999
                if (idFormaPago == 9999)
                    continue;

                string formaPago = fila.Cells["formaPago"].Value.ToString();
                decimal montoPago = Convert.ToDecimal(fila.Cells["importeFP"].Value);
                decimal montoRecibido = Convert.ToDecimal(fila.Cells["montoRecibido"].Value);

                // Asignar valores a las formas de pago y los montos correspondientes
                if (i == 0)
                {
                    formaPagoFP1 = formaPago;
                    montoPagoFP1 = montoPago;
                    montoRecibidoPagoFP1 = montoRecibido;
                }
                else if (i == 1)
                {
                    formaPagoFP2 = formaPago;
                    montoPagoFP2 = montoPago;
                    montoRecibidoPagoFP2 = montoRecibido;
                }
                else if (i == 2)
                {
                    formaPagoFP3 = formaPago;
                    montoPagoFP3 = montoPago;
                    montoRecibidoPagoFP3 = montoRecibido;
                }
                else if (i == 3)
                {
                    formaPagoFP4 = formaPago;
                    montoPagoFP4 = montoPago;
                    montoRecibidoPagoFP4 = montoRecibido;
                }
            }


            int idCorrelativo = new CN_Venta().ObtenerCorrelativo();
            string numeroDocumento = string.Format("{0:00000}", idCorrelativo);

            return new Venta()
            {
                oUsuario = new Usuario() { idUsuario = _Usuario.idUsuario },
                idNegocio = GlobalSettings.SucursalId,
                fechaRegistro = Convert.ToDateTime(dtpFecha.Value),
                tipoDocumento = ((OpcionCombo)cboTipoDocumento.SelectedItem).Texto,
                nroDocumento = numeroDocumento,
                documentoCliente = txtDocumentoCliente.Text,
                nombreCliente = txtNombreCliente.Text,
                idVendedor = (OpcionCombo)cboVendedores.SelectedItem == null ? _Venta.idVendedor : Convert.ToInt32(((OpcionCombo)cboVendedores.SelectedItem).Valor),
                montoCambio = Convert.ToDecimal(txtCambioCliente.Text),
                montoTotal = montoPagoFP1+montoPagoFP2+montoPagoFP3+montoPagoFP4,
                formaPago = formaPagoFP1,
                formaPago2 = formaPagoFP2,
                formaPago3 = formaPagoFP3,
                formaPago4 = formaPagoFP4,
                descuento = Convert.ToDecimal(txtDescuento.Text),
                montoDescuento = Convert.ToDecimal(txtMontoDescuento.Text),
                cotizacionDolar = txtCotizacion.Value,
                montoFP1 = montoPagoFP1,
                montoFP2 = montoPagoFP2,
                montoFP3 = montoPagoFP3,
                montoFP4 = montoPagoFP4,
                montoPago = montoRecibidoPagoFP1, // Sumar los montos de pago
                montoPagoFP2 = montoRecibidoPagoFP2,
                montoPagoFP3 = montoRecibidoPagoFP3,
                montoPagoFP4 = montoRecibidoPagoFP4,
                
                observaciones = txtObservaciones.Text,
            };
        }



        

        

        private bool ProcesarVenta(Venta oVenta, DataTable detalleVenta, out string mensaje, out int idVentaGenerado)
        {
            // Llamamos al método Registrar y asignamos el resultado a la variable global
            bool resultado = new CN_Venta().Registrar(oVenta, detalleVenta, out mensaje, out idVentaGenerado);

            if (resultado)
            {
                _idVentaGenerada = idVentaGenerado; // Guardamos el ID en la variable global
            }

            return resultado;
        }

        private void ActualizarStock(DataTable detalleVenta)
        {
            string actualizacionStock = string.Empty;
            bool actualizarSerial = false;
            string mensaje = string.Empty;
            string mensajeSerialActualizado = string.Empty;
            foreach (DataRow row in detalleVenta.Rows)
            {
                int idProducto = Convert.ToInt32(row["idProducto"]);
                int cantidad = Convert.ToInt32(row["cantidad"]);
                new CN_ProductoNegocio().CargarOActualizarStockProducto(idProducto, GlobalSettings.SucursalId, -cantidad);
            }


            if (dgvSeriales.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvSeriales.Rows)
                {
                    if (row.Cells["idProductoDetalle"].Value != null) // Verificar que la celda no esté vacía
                    {
                        int idProductoDetalle = Convert.ToInt32(row.Cells["idProductoDetalle"].Value);
                        actualizarSerial = new CN_Producto().DesactivarProductoDetalle(idProductoDetalle, _idVentaGenerada, out mensaje);
                        if (actualizarSerial)
                        {
                            mensajeSerialActualizado = "Se han dado de Baja el o los Numero de Serie";
                        }
                        else
                        {
                            MessageBox.Show($"Error al dar de Baja el Numero de Serie: {mensaje}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break; // Detener el loop si ocurre un error
                        }



                    }
                }
            }
        }

        //private void txtDescuento_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyData == Keys.Enter)
        //    {

        //        if (Convert.ToDecimal(txtDescuento.Text) > 0 && Convert.ToDecimal(txtDescuento.Text) <= 100 && (txtDescuento.Text != ""))
        //        {
        //            txtMontoDescuento.Visible = true;
        //            txtMontoDescuento.Enabled = false;
        //            decimal montoDescuentoRecargo = (txtRestaPagar.Value * Convert.ToDecimal(txtDescuento.Text)) / 100;
        //            txtMontoDescuento.Text = montoDescuentoRecargo.ToString("0.00");
        //            if(checkDescuento.Checked == true)
        //            {
        //                txtTotalAPagar.Text = (Convert.ToDecimal(txtTotalAPagar.Text) - montoDescuentoRecargo).ToString("0.00");


        //                if (cboFormaPago.Text =="DOLAR" || cboFormaPago.Text=="DOLAR EFECTIVO")
        //                {
        //                    txtRestaPagar.Value -=   montoDescuentoRecargo;
        //                }
        //                else
        //                {
        //                    CalcularRestaAPagar();
        //                }
        //            }
        //            if (checkRecargo.Checked == true)
        //            {
        //                txtTotalAPagar.Text = (Convert.ToDecimal(txtTotalAPagar.Text) + montoDescuentoRecargo).ToString("0.00");
        //                if (cboFormaPago.Text == "DOLAR" || cboFormaPago.Text == "DOLAR EFECTIVO")
        //                {
        //                    txtRestaPagar.Value +=  montoDescuentoRecargo;
        //                } else
        //                {
        //                    CalcularRestaAPagar();
        //                }
        //            }
        //            txtDescuento.Enabled = false;
        //            lblDescuento.Visible = true;
        //            CalcularCambio();
        //        }
        //        else
        //        {
        //            txtMontoDescuento.Visible = false;
        //            MessageBox.Show("Ingrese un valor entre 1 y 100 para el porcentaje de descuento o recargo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        //        }
        //    }
        //}

        private void txtDescuento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                // Intentamos convertir el texto del descuento a un valor decimal
                decimal descuento = 0;
                if (!decimal.TryParse(txtDescuento.Text, out descuento) || descuento <= 0 || descuento > 100)
                {
                    // Si la conversión falla o el valor no es válido, mostramos el mensaje de error
                    txtMontoDescuento.Visible = false;
                    MessageBox.Show("Ingrese un valor entre 1 y 100 para el porcentaje de descuento o recargo",
                                    "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return; // Salir si no es válido
                }

                // Si todo está bien, calculamos el monto de descuento
                txtMontoDescuento.Visible = true;
                txtMontoDescuento.Enabled = false;
                decimal montoDescuentoRecargo = (txtTotalAPagar.Value * descuento) / 100;
                txtMontoDescuento.Text = montoDescuentoRecargo.ToString("0.00");

                // Determinar si es descuento o recargo
                string tipoFormaPago = checkDescuento.Checked ? "DESCUENTO" : checkRecargo.Checked ? "RECARGO" : null;

                if (tipoFormaPago == null)
                {
                    MessageBox.Show("Seleccione Descuento o Recargo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Agregar al DataGridView
                dgvDataFormasPago.Rows.Add(null, tipoFormaPago, montoDescuentoRecargo, montoDescuentoRecargo, defaultImage);

                // Mensaje de confirmación
                //MessageBox.Show($"{tipoFormaPago} agregado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar campos
                txtDescuento.Enabled = false;
                lblDescuento.Visible = true;

                lblFormaPago.Visible = true;
                lblImporte.Visible = true;
                cboFormaPago.SelectedIndex = -1;
                cboFormaPago.Visible = true;
                txtPagaCon.ResetText();
                txtPagaCon.Visible = true;
                btnAgregarPago.Visible = true;
                txtDescuento.Text = string.Empty;
                txtMontoDescuento.Text = string.Empty;
                txtDescuento.Visible = false;
                txtMontoDescuento.Visible = false;
                
                lblPorcentaje.Visible = false;
                lblDescuento.Visible = false;
                checkDescuento.Visible = true;
                checkRecargo.Visible = true;
                checkDescuento.Checked = false;
                checkRecargo.Checked = false;


                // Recalcular el cambio
                CalcularCambio();
            }
        }



        private void checkRecargo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkRecargo.Checked)
            {
                // Mostrar y habilitar controles relacionados con el descuento o recargo
                txtMontoDescuento.Visible = true;
                txtMontoDescuento.Enabled = true;
                txtDescuento.Visible = true;
                txtDescuento.Enabled = true;
                txtDescuento.Text = "0";
                lblPorcentaje.Visible = true;
                txtMontoDescuento.Text = "0";
                checkDescuento.Visible = false;
                checkMonedaDolar.Visible = true;
                lblFormaPago.Visible = false;
                lblImporte.Visible = false;
                cboFormaPago.SelectedIndex = -1;
                cboFormaPago.Visible = false;
                txtPagaCon.ResetText();
                txtPagaCon.Visible = false;
                btnAgregarPago.Visible = false;
            }
            else
            {
                // Ocultar y resetear los controles cuando se desmarca el recargo
                txtDescuento.Visible = false;
                txtMontoDescuento.Visible = false;
                txtMontoDescuento.Text = "0";
                txtDescuento.Text = "0";
                lblPorcentaje.Visible = false;
                lblDescuento.Visible = false;
                checkDescuento.Visible = true;
                lblFormaPago.Visible = true;
                lblImporte.Visible = true;
                cboFormaPago.SelectedIndex = -1;
                cboFormaPago.Visible = true;
                txtPagaCon.ResetText();
                txtPagaCon.Visible = true;
                btnAgregarPago.Visible = true;



                // Ocultar el control de moneda en dólares
                checkMonedaDolar.Visible = false;

                // Recalcular el total después de desmarcar el recargo
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



        //    private void CalcularRestaAPagar()
        //    {
        //        decimal cotizacionDolar = txtCotizacion.Value;
        //        decimal totalAPagar = txtTotalAPagar.Value;
        //        decimal totalAPagarDolares = txtTotalAPagarDolares.Value;  // Mantener el valor original de Dólares
        //        decimal pagoTotal = 0;
        //        decimal pagoTotalDolares = 0;

        //        // Calcula la diferencia entre el valor actual y el valor anterior del NumericUpDown
        //        decimal diferenciaPagoParcial = txtMontoPagoParcial.Value - montoPagoParcialAnterior;

        //        // Resta solo la diferencia calculada
        //        totalAPagar -= diferenciaPagoParcial;

        //        // Actualiza el valor del monto anterior al valor actual del NumericUpDown
        //        montoPagoParcialAnterior = txtMontoPagoParcial.Value;

        //        // Si el pago parcial es en dólares y aún no se ha descontado, descuéntalo y marca como descontado
        //        if (_pagoParcialGlobal != null && _pagoParcialGlobal.moneda == "DOLARES" && !pagoParcialDolaresDescontado)
        //        {
        //            totalAPagarDolares -= _pagoParcialGlobal.monto;
        //            pagoParcialDolaresDescontado = true;  // Marca que el pago parcial en dólares ya fue descontado
        //        }

        //        // Calcula el monto pagado por cada forma de pago
        //        var formasDePago = new[]
        //        {
        //    new { FormaPago = cboFormaPago.Text, Monto = txtPagaCon.Value },
        //    new { FormaPago = cboFormaPago2.Text, Monto = txtPagaCon2.Value },
        //    new { FormaPago = cboFormaPago3.Text, Monto = txtPagaCon3.Value },
        //    new { FormaPago = cboFormaPago4.Text, Monto = txtPagaCon4.Value }
        //};

        //        foreach (var pago in formasDePago)
        //        {
        //            // Verifica si el pago es en "DOLAR" o "DOLAR EFECTIVO"
        //            if (pago.FormaPago == "DOLAR" || pago.FormaPago == "DOLAR EFECTIVO")
        //            {
        //                if (pago.FormaPago != "DOLAR EFECTIVO")  // Solo descuento de totalAPagarDolares cuando no es "DOLAR EFECTIVO"
        //                {
        //                    pagoTotalDolares += pago.Monto;
        //                    totalAPagarDolares -= pago.Monto;  // Descuenta solo en el caso de pago en Dólares
        //                }
        //                else
        //                {
        //                    pagoTotalDolares += pago.Monto; // No afecta el totalAPagarDolares, solo suma al pago
        //                }
        //            }
        //            else
        //            {
        //                pagoTotal += pago.Monto;
        //            }
        //        }

        //        // Manejo de descuentos
        //        if (checkDescuento.Checked)
        //        {
        //            decimal montoDescuento = Convert.ToDecimal(txtMontoDescuento.Text);

        //            if (checkMonedaDolar.Checked)
        //            {
        //                totalAPagarDolares -= montoDescuento;
        //            }
        //            else
        //            {
        //                totalAPagar -= montoDescuento;
        //            }
        //        }

        //        // Manejo de recargos
        //        if (checkRecargo.Checked)
        //        {
        //            decimal montoRecargo = Convert.ToDecimal(txtMontoDescuento.Text);

        //            if (checkMonedaDolar.Checked)
        //            {
        //                totalAPagarDolares += montoRecargo;
        //            }
        //            else
        //            {
        //                totalAPagar += montoRecargo;
        //            }
        //        }

        //        // Calcula el resto a pagar en cada moneda
        //        decimal restoAPagar = totalAPagar - pagoTotal;
        //        decimal restoAPagarDolares = totalAPagarDolares - pagoTotalDolares;


        //        if (restoAPagar < 0) restoAPagar = 0;
        //        if (restoAPagarDolares < 0) restoAPagarDolares = 0;

        //        // Si no hay pago parcial en Dólares, solo actualiza el valor de totalAPagar
        //        if (totalAPagarDolares == txtTotalAPagarDolares.Value)
        //        {
        //            // Solo actualiza si no se ha hecho un pago parcial en dólares
        //            txtTotalAPagarDolares.Value = totalAPagarDolares;
        //        }

        //        // Actualiza el total a pagar en la moneda local
        //        txtTotalAPagar.Value = totalAPagar;

        //        // Actualiza los valores de RestaPagar y RestaPagarDolares
        //        txtRestaPagar.Value = restoAPagar;
        //        txtRestaPagarDolares.Value = restoAPagarDolares;

        //        // Si alguna de las "restas a pagar" es 0, ambas deben ser 0
        //        if (txtRestaPagar.Value == 0)
        //        {
        //            txtRestaPagarDolares.Value = 0;
        //        }
        //        if (txtRestaPagarDolares.Value == 0)
        //        {
        //            txtRestaPagar.Value = 0;
        //        }
        //    }

        private void CalcularRestaAPagar()
        {
            decimal cotizacionDolar = txtCotizacion.Value;
            decimal totalAPagar = txtTotalAPagar.Value;
            decimal totalAPagarDolares = txtTotalAPagarDolares.Value;
            decimal pagoTotal = 0;
            decimal pagoTotalDolares = 0;

            // Lista de formas de pago y montos extraídos del DataGridView
            foreach (DataGridViewRow fila in dgvDataFormasPago.Rows)
            {
                string formaPago = fila.Cells["formaPago"].Value.ToString();
                decimal monto = Convert.ToDecimal(fila.Cells["importeFP"].Value);

                if (formaPago == "DOLAR" || formaPago == "DOLAR EFECTIVO")
                {
                    pagoTotalDolares += monto;
                    totalAPagar -= Math.Round(monto * cotizacionDolar, 2);
                }
                else if (formaPago == "RECARGO")
                {
                    if (checkMonedaDolar.Checked)
                    {
                        // Recargo en dólares
                        totalAPagarDolares += monto;
                        totalAPagar += Math.Round(monto * cotizacionDolar, 2);
                    }
                    else
                    {
                        // Recargo en pesos
                        totalAPagar += monto;
                        totalAPagarDolares += Math.Round(monto / cotizacionDolar, 2);
                    }
                }
                else
                {
                    pagoTotal += monto;
                    pagoTotalDolares += Math.Round(monto / cotizacionDolar, 2);
                }
            }

            // Aplicar descuentos
            //decimal montoAjuste = Convert.ToDecimal(txtMontoDescuento.Text);
            //if (checkDescuento.Checked)
            //{
            //    if (checkMonedaDolar.Checked)
            //    {
            //        totalAPagarDolares -= montoAjuste;
            //    }
            //    else
            //    {
            //        totalAPagar -= montoAjuste;
            //    }
            //}

            // Calcular el monto restante a pagar en cada moneda
            decimal restoAPagar = Math.Max(0, totalAPagar - pagoTotal);
            decimal restoAPagarDolares = Math.Max(0, totalAPagarDolares - pagoTotalDolares);

            // Actualizar los valores en los campos de resta
            txtRestaPagar.Value = restoAPagar;
            txtRestaPagarDolares.Value = restoAPagarDolares;

            // Si uno de los montos restantes es cero, ambos deben ser cero
            if (restoAPagar == 0 || restoAPagarDolares == 0)
            {
                txtRestaPagar.Value = 0;
                txtRestaPagarDolares.Value = 0;
            }

            // Calcular el cambio
            CalcularCambio();
        }






        //private void txtMontoDescuento_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyData == Keys.Enter)
        //    {
        //        if (checkDescuento.Checked == true)
        //        {
        //            if (checkMonedaDolar.Checked)
        //            {

        //                txtRestaPagarDolares.Value = txtRestaPagarDolares.Value - Convert.ToDecimal(txtMontoDescuento.Text);
        //                txtRestaPagar.Value = txtTotalAPagarDolares.Value * txtCotizacion.Value - Convert.ToDecimal(txtMontoDescuento.Text)*txtCotizacion.Value;

        //            }
        //            else
        //            {
        //                txtRestaPagar.Text = (Convert.ToDecimal(txtTotalAPagar.Text) - Convert.ToDecimal(txtMontoDescuento.Text)).ToString("0.00");

        //            }
        //            CalcularRestaAPagar();
        //        }
        //        if (checkRecargo.Checked == true)

        //        {
        //            if (checkMonedaDolar.Checked)
        //            {

        //                txtRestaPagarDolares.Value = txtRestaPagarDolares.Value + Convert.ToDecimal(txtMontoDescuento.Text);
        //                txtRestaPagar.Value = txtTotalAPagarDolares.Value * txtCotizacion.Value + Convert.ToDecimal(txtMontoDescuento.Text) * txtCotizacion.Value;
        //            } else
        //            {
        //                txtRestaPagar.Text = (Convert.ToDecimal(txtTotalAPagar.Text) + Convert.ToDecimal(txtMontoDescuento.Text)).ToString("0.00");
        //            }
        //            CalcularRestaAPagar();
        //        }

        //    }
        //}
        private void txtMontoDescuento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                // Asegurarse de que el monto de descuento es válido
                if (decimal.TryParse(txtMontoDescuento.Text, out decimal montoDescuento) && montoDescuento > 0)
                {
                    // Calcula el valor ajustado según la moneda seleccionada
                    decimal montoAjustado = montoDescuento;
                    if (checkMonedaDolar.Checked)
                    {
                        montoAjustado *= txtCotizacion.Value;
                    }

                    if (checkDescuento.Checked)
                    {
                        // Aplicar descuento
                        if (checkMonedaDolar.Checked)
                        {
                            txtRestaPagarDolares.Value -= montoDescuento;
                            txtRestaPagar.Value = (txtTotalAPagarDolares.Value * txtCotizacion.Value) - montoAjustado;
                        }
                        else
                        {
                            txtRestaPagar.Text = (Convert.ToDecimal(txtTotalAPagar.Text) - montoDescuento).ToString("0.00");
                        }
                    }
                    else if (checkRecargo.Checked)
                    {
                        // Aplicar recargo
                        if (checkMonedaDolar.Checked)
                        {
                            txtRestaPagarDolares.Value += montoDescuento;
                            txtRestaPagar.Value = (txtTotalAPagarDolares.Value * txtCotizacion.Value) + montoAjustado;
                        }
                        else
                        {
                            txtRestaPagar.Text = (Convert.ToDecimal(txtTotalAPagar.Text) + montoDescuento).ToString("0.00");
                        }
                    }

                    // Llamar a la función de cálculo de la resta a pagar
                    CalcularRestaAPagar();
                }
                else
                {
                    MessageBox.Show("El monto ingresado no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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



        // Bandera para controlar si el pago parcial ya fue descontado


        private bool _recargoAplicado = false; // Bandera para controlar si ya se aplicó el recargo

        private void cboFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFormaPago.Text == "DOLAR EFECTIVO")
            {
                txtTotalAPagarDolares.Visible = true;
                lblTotalAPagarDolares.Visible = true;
                lblRestaPagarDolares.Visible = true;
                txtRestaPagarDolares.Visible = true;
            }

            if (cboFormaPago.SelectedIndex != -1 && !_recargoAplicado)
            {
                string formaPago = cboFormaPago.Text;
                decimal porcentajeRecargo = new CN_FormaPago().ObtenerFPPorDescripcion(formaPago).porcentajeRecargo;
                decimal porcentajeDescuento = new CN_FormaPago().ObtenerFPPorDescripcion(formaPago).porcentajeDescuento;

                decimal totalOriginalPesos = Convert.ToDecimal(txtTotalAPagar.Text);
                decimal totalOriginalDolares = Convert.ToDecimal(txtTotalAPagarDolares.Text);

                decimal totalConRecargoPesos;
                decimal totalConRecargoDolares;

                decimal cotizacionActual;
                if (decimal.TryParse(txtCotizacion.Text, out cotizacionActual) && cotizacionActual != cotizacionOriginal)
                {
                    totalConRecargoPesos = totalOriginalPesos + (totalOriginalPesos * porcentajeRecargo);
                    totalConRecargoDolares = totalOriginalDolares + (totalOriginalDolares * porcentajeRecargo);
                }
                else
                {
                    totalConRecargoPesos = Math.Round(totalOriginalPesos + (totalOriginalPesos * porcentajeRecargo), 2);
                    totalConRecargoDolares = totalOriginalDolares + (totalOriginalDolares * porcentajeRecargo);
                }

                // Aplicar el pago parcial si existe y no se ha aplicado previamente
                if (_pagoParcialGlobal != null && _pagoParcialGlobal.monto > 0 && !_pagoParcialAplicado)
                {
                    if(_pagoParcialGlobal.moneda == "PESOS")
                    {
                        totalConRecargoDolares -= Math.Round(_pagoParcialGlobal.monto /cotizacionActual,2);
                        totalConRecargoPesos -= _pagoParcialGlobal.monto;

                    } else if(_pagoParcialGlobal.moneda == "DOLARES")
                    {
                        totalConRecargoDolares -= _pagoParcialGlobal.monto;
                        totalConRecargoPesos -= Math.Round(_pagoParcialGlobal.monto * cotizacionActual, 2);
                    }
                    

                    _pagoParcialAplicado = true;
                }

                decimal totalFinalPesos = totalConRecargoPesos - (totalConRecargoPesos * porcentajeDescuento);
                decimal totalFinalDolares = totalConRecargoDolares;

                // Mostrar los totales finales dependiendo de la forma de pago
                txtRestaPagarDolares.Value = totalFinalDolares;
                txtRestaPagar.Value = totalFinalPesos;
                txtTotalAPagar.Value = totalFinalPesos;
                txtTotalAPagarDolares.Value = totalFinalDolares;

                // Marcar que el recargo ya fue aplicado
                _recargoAplicado = true;
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
                        // Asignar el objeto PagoParcial del modal a la variable global
                        _pagoParcialGlobal = modal._PagoParcial;

                        txtMontoPagoParcial.Value = _pagoParcialGlobal.monto;
                        txtIdPagoParcial.Text = _pagoParcialGlobal.idPagoParcial.ToString();


                        FormaPago formaPagoADescontarRetencion = new CN_FormaPago().ObtenerFPPorDescripcion(_pagoParcialGlobal.formaPago);
                        decimal montoMenosRetencion = Math.Round(_pagoParcialGlobal.monto - (_pagoParcialGlobal.monto * formaPagoADescontarRetencion.porcentajeRetencion) / 100, 2);

                        // Agregar el pago parcial al DataGridView de formas de pago
                        dgvDataFormasPago.Rows.Add(
                            9999,        // Columna ID Pago Parcial
                            _pagoParcialGlobal.formaPago,                         // Columna Forma de Pago
                            _pagoParcialGlobal.monto.ToString("N2"), // Columna Monto
                            montoMenosRetencion               // Columna Moneda
                        );

                        // Opcional: recalcular el monto restante por pagar
                        //CalcularRestaAPagar();

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
            // Verifica si la moneda del pago parcial es pesos antes de llamar a CalcularRestaAPagar
            if (_pagoParcialGlobal != null && _pagoParcialGlobal.moneda == "PESOS")
            {
                CalcularRestaAPagar();
            }
            else if (_pagoParcialGlobal != null && _pagoParcialGlobal.moneda == "DOLARES" && !pagoParcialDolaresContado)
            {
                // Solo suma a totalAPagarDolares la primera vez si es en dólares
                txtRestaPagarDolares.Value -= _pagoParcialGlobal.monto;
                txtRestaPagar.Value -= Math.Round(_pagoParcialGlobal.monto * cotizacionOriginal, 2);
                pagoParcialDolaresContado = true;
                lblTotalAPagarDolares.Visible = true;
                lblRestaPagarDolares.Visible = true;
                txtTotalAPagarDolares.Visible = true;
                txtRestaPagarDolares.Visible = true;
            }
        }


        private void btnEliminarPagoParcial_Click(object sender, EventArgs e)
        {
            txtMontoPagoParcial.Value = 0;
            txtIdPagoParcial.Text = "0";
        }

        private void btnAgregarPago_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count <= 0)
            {
                MessageBox.Show("No hay Productos para aplicar le Pago", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (contadorFormasPago >= 4)
            {
                MessageBox.Show("Solo se pueden agregar hasta 4 formas de pago.", "Límite alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboFormaPago.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtPagaCon.Text))
            {
                MessageBox.Show("Seleccione una forma de pago e ingrese un monto válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(txtPagaCon.Text, out decimal montoPago) || montoPago <= 0)
            {
                MessageBox.Show("Ingrese un monto válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Obtener el elemento seleccionado como objeto OpcionCombo
            var formaPagoSeleccionada = (OpcionCombo)cboFormaPago.SelectedItem;

            if (formaPagoSeleccionada == null)
            {
                MessageBox.Show("Forma de pago seleccionada no válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idFormaPago = (int)formaPagoSeleccionada.Valor;
            string formaPago = formaPagoSeleccionada.Texto;



            
                FormaPago formaPagoADescontarRetencion = new CN_FormaPago().ObtenerFPPorDescripcion(((OpcionCombo)cboFormaPago.SelectedItem).Texto);
                decimal montoMenosRetencion = Math.Round(montoPago- (montoPago* formaPagoADescontarRetencion.porcentajeRetencion)/100,2);
                
            

            // Agregar al DataGridView con idFormaPago, formaPago y montoPago
            dgvDataFormasPago.Rows.Add(idFormaPago, formaPago, montoPago, montoMenosRetencion,defaultImage);

            // Incrementar el contador
            contadorFormasPago++;

            //// Actualizar los montos restantes según la forma de pago
            //if (formaPago == "DOLAR" || formaPago == "DOLAR EFECTIVO")
            //{
            //    // Actualizar txtRestaPagarDolares y txtRestaPagar cuando la forma de pago es "dolar" o "dolar efectivo"
            //    decimal montoRestanteDolares = decimal.Parse(txtRestaPagarDolares.Text) - montoPago;
            //    txtRestaPagarDolares.Text = montoRestanteDolares.ToString("F2");

            //    // Actualizar txtRestaPagar con el valor en moneda local
            //    decimal montoRestanteLocal = decimal.Parse(txtRestaPagar.Text) - (montoPago * decimal.Parse(txtCotizacion.Text));
            //    txtRestaPagar.Text = montoRestanteLocal.ToString("F2");
            //}
            //else
            //{
            //    // Actualizar txtRestaPagar con el valor de txtPagaCon para otras formas de pago
            //    decimal montoRestanteLocal = decimal.Parse(txtRestaPagar.Text) - montoPago;
            //    txtRestaPagar.Text = montoRestanteLocal.ToString("F2");

            //    // Actualizar txtRestaPagarDolares con el valor convertido a dólares
            //    decimal montoRestanteDolares = decimal.Parse(txtRestaPagarDolares.Text) - (montoPago / decimal.Parse(txtCotizacion.Text));
            //    txtRestaPagarDolares.Text = montoRestanteDolares.ToString("F2");
            //}

            // Limpiar campos
            cboFormaPago.SelectedIndex = -1;
            txtPagaCon.ResetText();

            // Mensaje si se alcanzó el límite
            if (contadorFormasPago == 4)
            {
                MessageBox.Show("Se han agregado las 4 formas de pago permitidas.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvDataFormasPago_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CalcularRestaAPagar();
        }

        private void dgvDataFormasPago_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar que se haga clic en la columna del botón de eliminar y que la fila sea válida
            if (dgvDataFormasPago.Columns[e.ColumnIndex].Name == "btnEliminarPago")
            {
                int indice = e.RowIndex;

                // Verificar que el índice de fila sea válido (es decir, no -1)
                if (indice >= 0 && indice < dgvDataFormasPago.Rows.Count)
                {
                    // Eliminar la fila de dgvDataFormasPago
                    dgvDataFormasPago.Rows.RemoveAt(indice);

                    // Actualizar la resta a pagar después de eliminar la fila
                    CalcularRestaAPagar();
                }
                else
                {
                    MessageBox.Show("Índice de fila no válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        
    }
}
