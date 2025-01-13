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
        private decimal cotizacionDolarModificada;
        private int _idVentaGenerada;
        private bool _pagoParcialAplicado = false;
        private int contadorFormasPago = 0;
        private bool recargoAplicado = false;
        private string mensajeSerialActualizado = string.Empty;

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
                cotizacionDolarModificada = cotizacionOriginal;
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
                    
                }
                else
                {
                    txtDocumentoCliente.Select();
                }
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
                    txtProductoDolar.Text = modal._Producto.productoDolar?"SI":"NO";
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
                    txtProductoDolar.Text = oProducto.productoDolar ? "SI" : "NO";
                }
                else
                {
                    txtCodigoProducto.BackColor = Color.IndianRed;
                    txtIdProducto.Text = "0";
                    txtProducto.Text = "";
                    txtPrecio.Text = "";
                    txtStock.Text = "";
                    txtCantidad.Value = 1;
                    txtProductoDolar.Text = "";



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
                MessageBox.Show("Precio - Formato Moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrecio.Select();
                return;
            }

            if (Convert.ToInt32(txtStock.Text) < Convert.ToInt32(txtCantidad.Value.ToString()))
            {
                MessageBox.Show("La cantidad ingresada debe ser menor al Stock Físico", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if(cotizacionOriginal != txtCotizacion.Value)
            {
                cotizacionDolarModificada = txtCotizacion.Value;
                cotizacionCambio = true;
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

            if (!producto_existe)
            {
                if (esSerializable)
                {
                    using (var modal = new mdProductoSerializable(Convert.ToInt32(txtIdProducto.Text)))
                    {
                        if (modal.ShowDialog() == DialogResult.OK)
                        {
                            if (modal.ListaProductoDetalles != null && modal.ListaProductoDetalles.Count > 0)
                            {
                                foreach (var productoDetalle in modal.ListaProductoDetalles)
                                {
                                    dgvData.Rows.Add(new object[]{
                                txtIdProducto.Text,
                                txtProducto.Text,
                                productoDetalle.marca,        // Marca del producto
                                productoDetalle.modelo,       // Modelo del producto
                                productoDetalle.color,
                                productoDetalle.numeroSerie,
                                precio.ToString("0.00"),
                                string.Format("{0 } {1}", txtProductoDolar.Text=="SI"?"USD":"ARS", txtPrecioLista.Text),
                                txtCantidad.Value.ToString(),
                                txtCotizacion.Value.ToString(),
                                string.Format("{0 } {1}",txtProductoDolar.Text=="SI"?"USD":"ARS",txtProductoDolar.Text=="SI"?(txtCantidad.Value * precio).ToString("0.00"):(txtCantidad.Value * Convert.ToDecimal(txtPrecioLista.Text)).ToString("0.00")),
                                txtSerializable.Text,

                                productoDetalle.idProductoDetalle,
                                txtProductoDolar.Text,
                                defaultImage
                            }); ;
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
                else
                {
                    dgvData.Rows.Add(new object[]{
                txtIdProducto.Text,
                txtProducto.Text,
                string.Empty,  // Marca (vacío para productos no serializables)
                string.Empty,  // Modelo
                string.Empty,  // Color
                string.Empty,  // Serial number
                precio.ToString("0.00"),
                string.Format("{0 } {1}", txtProductoDolar.Text=="SI"?"USD":"ARS", txtPrecioLista.Text),
                txtCantidad.Value.ToString(),
                txtCotizacion.Value.ToString(),
                string.Format("{0 } {1}",txtProductoDolar.Text=="SI"?"USD":"ARS",txtProductoDolar.Text=="SI"?(txtCantidad.Value * precio).ToString("0.00"):(txtCantidad.Value * Convert.ToDecimal(txtPrecioLista.Text)).ToString("0.00")),
                txtSerializable.Text,
                null,
                txtProductoDolar.Text,
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


        private string RemoverSimboloMoneda(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return "0";

            // Remueve los primeros caracteres (ej. "ARS " o "$ ") y devuelve el resto
            return valor.Trim().Substring(4); // Ajusta según el formato de tu dato
        }


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
                    // Validamos que las celdas no sean nulas antes de procesarlas
                    if (row.Cells["subTotal"].Value != null && row.Cells["cotizacionDolar"].Value != null)
                    {
                        string subTotal = row.Cells["subTotal"].Value.ToString();
                        string cotizacionStr = row.Cells["cotizacionDolar"].Value.ToString();

                        // Inicializamos la cotización en 1 para los casos donde no sea aplicable
                        
                            decimal cotizacionDolar = Convert.ToDecimal(cotizacionStr);
                        

                        // Verificamos el prefijo y realizamos las operaciones necesarias
                        if (subTotal.StartsWith("ARS"))
                        {
                            string valorLimpio = RemoverSimboloMoneda(subTotal);
                            decimal valor = Convert.ToDecimal(valorLimpio);

                            // Suma directa a pesos
                            totalPesos += valor;

                            // Conversión a dólares y suma (si la cotización es válida)
                            if (cotizacionDolar > 0)
                            {
                                totalDolares += Math.Round(valor / cotizacionDolar, 2);
                            }
                        }
                        else if (subTotal.StartsWith("USD"))
                        {
                            string valorLimpio = RemoverSimboloMoneda(subTotal);
                            decimal valor = Convert.ToDecimal(valorLimpio);

                            // Suma directa a dólares
                            totalDolares += valor;

                            // Conversión a pesos y suma
                            totalPesos += Math.Round(valor * cotizacionDolar, 2);
                        }
                    }
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
            // Obtiene el total a pagar desde el campo correspondiente
            decimal totalAPagar = txtTotalAPagar.Value;

            // Inicializa la suma total de los pagos
            decimal sumaPagos = 0;

            // Recorre las filas del DataGridView para sumar los pagos
            foreach (DataGridViewRow fila in dgvDataFormasPago.Rows)
            {
                if (fila.Cells["importeFP"].Value != null)
                {
                    // Verifica si la forma de pago es un recargo, en ese caso no sumarlo
                    string formaPago = fila.Cells["formaPago"].Value.ToString();
                    if (formaPago != "RECARGO") // Solo sumamos si no es un recargo
                    {
                        sumaPagos += Convert.ToDecimal(fila.Cells["importeFP"].Value);
                    }else
                    {
                        totalAPagar += Convert.ToDecimal(fila.Cells["importeFP"].Value);
                    }

                    if (formaPago == "DOLAR EFECTIVO")
                    {
                        sumaPagos = Convert.ToDecimal(fila.Cells["importeFP"].Value) * txtCotizacion.Value;
                    }
                }
            }

            

            // Calcula el cambio
            decimal cambio = sumaPagos - totalAPagar;

            // Si el cambio es negativo, no hay cambio, solo resta por pagar
            if (cambio < 0)
            {
                txtRestaPagar.Value = Math.Abs(cambio); // Muestra cuánto falta pagar
                txtCambioCliente.Text = "0.00";        // No hay cambio
            }
            else
            {
                txtRestaPagar.Value = 0;               // No falta nada por pagar
                txtCambioCliente.Text = cambio.ToString("0.00"); // Muestra el cambio
            }
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
                string tipo = formaPagoADescontarRetencion.tipo;


                // Agregar al DataGridView con idFormaPago, formaPago y montoPago
                dgvDataFormasPago.Rows.Add(idFormaPago, formaPago, montoPago, montoMenosRetencion,tipo, defaultImage);

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
            contadorFormasPago = 0;
            txtIdCliente.Text = string.Empty;
            
            dgvDataFormasPago.Rows.Clear();
            
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

        private void btnRegistrarVenta_Click(object sender, EventArgs e)
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
                        bool darBajaPagoParcial = new CN_PagoParcial().DarDeBajaPagoParcial(Convert.ToInt32(txtIdPagoParcial.Text),idVentaGenerado);
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
                    MessageBox.Show($"Venta registrada correctamente Numero : {oVenta.nroDocumento}. Stock actualizado y transacción registrada en la caja. {mensajeSerialActualizado}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar los campos de la venta
                    LimpiarVenta();
                    oVenta = null;
                    mensajeSerialActualizado = string.Empty;
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
             
            foreach (DataRow row in detalleVenta.Rows)
            {
                int idProducto = Convert.ToInt32(row["idProducto"]);
                int cantidad = Convert.ToInt32(row["cantidad"]);
                new CN_ProductoNegocio().CargarOActualizarStockProducto(idProducto, GlobalSettings.SucursalId, -cantidad);
            }


            
                foreach (DataGridViewRow row in dgvData.Rows)
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
               if(txtProductoDolar.Text == "NO")
                {
                    
                    txtCotizacion.Value = cotizacionOriginal;
                    return;
                }
                

                if (cboFormaPago.SelectedIndex != -1)
                {
                    MessageBox.Show("Para modificar la cotización no debe de haber Formas de pago Seleccionadas.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCotizacion.Value = cotizacionOriginal;
                    txtCotizacion.ReadOnly = false;

                    return; // Salir del método si hay filas en el DataGridView
                }
                // Validar si hay filas en el DataGridView
                if (dgvDataFormasPago.Rows.Count > 0)
                {
                    MessageBox.Show("Para modificar la cotización no deben haber pagos ingresados.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCotizacion.Value = cotizacionOriginal;
                    txtCotizacion.ReadOnly = false;
                    
                    return; // Salir del método si hay filas en el DataGridView
                }

                // Continuar con la lógica si no hay pagos ingresados
                txtTotalAPagar.Value = (txtTotalAPagarDolares.Value * txtCotizacion.Value);
                
                cotizacionCambio = true;
                CalcularRestaAPagar();
            }
        }





        private void CalcularRestaAPagar()
        {
            decimal cotizacionDolar;
            decimal totalAPagar = txtTotalAPagar.Value;
            decimal totalAPagarDolares = txtTotalAPagarDolares.Value;
            decimal pagoTotal = 0;
            decimal pagoTotalDolares = 0;

            // Variables para acumular el recargo
            decimal recargoPesos = 0;
            decimal recargoDolares = 0;

            // Lista de formas de pago y montos extraídos del DataGridView
            foreach (DataGridViewRow fila in dgvDataFormasPago.Rows)
            {
                string formaPago = fila.Cells["formaPago"].Value.ToString();
                decimal monto = Convert.ToDecimal(fila.Cells["importeFP"].Value);

                if (formaPago.StartsWith("PAGO PARCIAL") || formaPago == "RECARGO" || formaPago == "DESCUENTO")
                {
                    cotizacionDolar = cotizacionOriginal;
                }
                else
                {
                    cotizacionDolar = cotizacionDolarModificada;
                }

                if (formaPago == "DOLAR" || formaPago == "DOLAR EFECTIVO" || formaPago == "DOLAR - PAGO PARCIAL" || formaPago == "DOLAR EFECTIVO - PAGO PARCIAL")
                {
                    pagoTotalDolares += monto;
                    pagoTotal += Math.Round(monto * cotizacionDolar, 2);
                }
                else if (formaPago == "RECARGO")
                {
                    if (checkMonedaDolar.Checked)
                    {
                        // Recargo en dólares
                        recargoDolares += monto;
                        recargoPesos += Math.Round(monto * cotizacionDolar, 2);
                    }
                    else
                    {
                        // Recargo en pesos
                        recargoPesos += monto;
                        recargoDolares += Math.Round(monto / cotizacionDolar, 2);
                    }
                }
                else
                {
                    pagoTotal += monto;
                    pagoTotalDolares += Math.Round(monto / cotizacionDolar, 2);
                }
            }

            // Ajustar el total a pagar con los recargos
            totalAPagar += recargoPesos;
            totalAPagarDolares += recargoDolares;

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
                // Intentamos convertir el texto del monto a un valor decimal
                decimal montoDescuentoRecargo = 0;
                if (!decimal.TryParse(txtMontoDescuento.Text, out montoDescuentoRecargo) || montoDescuentoRecargo <= 0)
                {
                    // Si la conversión falla o el valor no es válido, mostramos el mensaje de error
                    MessageBox.Show("Ingrese un monto válido para el descuento o recargo.",
                                    "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return; // Salir si no es válido
                }

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
                txtMontoDescuento.Enabled = false;
                txtMontoDescuento.Text = string.Empty;
                txtMontoDescuento.Visible = false;

                lblFormaPago.Visible = true;
                lblImporte.Visible = true;
                cboFormaPago.SelectedIndex = -1;
                cboFormaPago.Visible = true;
                txtPagaCon.ResetText();
                txtPagaCon.Visible = true;
                btnAgregarPago.Visible = true;

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

        // Variable para almacenar el total original de la venta
        private decimal totalVentaOriginalPesos;
        private decimal totalVentaOriginalDolares;
        private bool formaPagoAplicada = false;
        private void cboFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCotizacion.ReadOnly = true;

            if (cboFormaPago.SelectedIndex != -1)
            {
                string formaPagoDescripcion = cboFormaPago.Text;
                if (formaPagoDescripcion.Equals("EFECTIVO", StringComparison.OrdinalIgnoreCase) ||
            formaPagoDescripcion.Equals("DOLAR EFECTIVO", StringComparison.OrdinalIgnoreCase))
                {
                    // Si es "Efectivo" o "Dólar Efectivo", no realizar ninguna acción
                    return;
                }
                CN_FormaPago cnFormaPago = new CN_FormaPago();
                FormaPago formaPago = cnFormaPago.ObtenerFPPorDescripcion(formaPagoDescripcion);

                if (formaPago != null)
                {
                    decimal porcentajeRecargo = formaPago.porcentajeRecargo;
                    decimal porcentajeDescuento = formaPago.porcentajeDescuento;
                    decimal porcentajeRecargoDolar = formaPago.porcentajeRecargoDolar;
                    decimal porcentajeDescuentoDolar = formaPago.porcentajeDescuentoDolar;

                    decimal cotizacionActual;
                    if (!decimal.TryParse(txtCotizacion.Text, out cotizacionActual) || cotizacionActual <= 0)
                    {
                        MessageBox.Show("Cotización inválida. Verifique el valor ingresado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (!formaPagoAplicada)
                    {

                        decimal totalPesos = 0;
                        decimal totalDolares = 0;

                        decimal recargoPesosProductoPesos = 0;
                        decimal descuentoPesosProductoPesos = 0;
                        decimal recargoDolaresProductoPesos = 0;
                        decimal descuentoDolaresProductoPesos = 0;

                        decimal recargoPesosProductoDolares = 0;
                        decimal descuentoPesosProductoDolares = 0;
                        decimal recargoDolaresProductoDolares = 0;
                        decimal descuentoDolaresProductoDolares = 0;

                        // Recorremos la grilla para calcular los subtotales y aplicar recargos/descuentos
                        foreach (DataGridViewRow row in dgvData.Rows)
                        {

                            if (row.Cells["idProducto"].Value != null && row.Cells["productoDolar"].Value != null &&
                                row.Cells["cantidad"].Value != null && row.Cells["precio"].Value != null)
                            {
                                int idProducto = Convert.ToInt32(row.Cells["idProducto"].Value);
                                string productoDolar = row.Cells["productoDolar"].Value.ToString();
                                decimal cantidad = Convert.ToDecimal(row.Cells["cantidad"].Value);
                                decimal precio = Convert.ToDecimal(row.Cells["precio"].Value);
                                decimal precioLista = Convert.ToDecimal(RemoverSimboloMoneda(row.Cells["precioLista"].Value.ToString()));
                                cotizacionActual = Convert.ToDecimal(row.Cells["cotizacionDolar"].Value);
                                decimal subtotal = cantidad * precio;
                                decimal subTotalPrecioLista = cantidad * precioLista;




                                if (productoDolar == "SI")
                                {


                                    // Producto en dólares
                                    totalDolares += subtotal;
                                    totalPesos += Math.Round(subtotal * cotizacionActual, 2);

                                    recargoPesosProductoDolares += Math.Round((subtotal * cotizacionActual) * porcentajeRecargo, 2);
                                    descuentoPesosProductoDolares += Math.Round((subtotal * cotizacionActual) * porcentajeDescuento, 2);

                                    recargoDolaresProductoDolares += Math.Round(subtotal * porcentajeRecargo, 2);
                                    descuentoDolaresProductoDolares += Math.Round(subtotal * porcentajeDescuento, 2);

                                }
                                else
                                {
                                    // Producto en pesos
                                    totalPesos += subTotalPrecioLista;
                                    totalDolares += Math.Round(subTotalPrecioLista / cotizacionActual, 2);

                                    //recargoPesosProductoPesos += Math.Round(subtotal * porcentajeRecargo, 2);
                                    descuentoPesosProductoPesos += Math.Round(subTotalPrecioLista * porcentajeDescuento, 2);
                                    recargoPesosProductoPesos = 0;

                                    //recargoDolaresProductoPesos += Math.Round((subtotal / cotizacionActual) * porcentajeRecargo, 2);
                                    descuentoDolaresProductoPesos += Math.Round((subTotalPrecioLista / cotizacionActual) * porcentajeDescuento, 2);
                                    recargoDolaresProductoPesos = 0;
                                }
                            }
                        }

                        // Sumar recargos y descuentos a los totales
                        decimal nuevoTotalPesos = totalPesos
                            + recargoPesosProductoPesos - descuentoPesosProductoPesos
                            + recargoPesosProductoDolares - descuentoPesosProductoDolares;

                        decimal nuevoTotalDolares = totalDolares
                            + recargoDolaresProductoPesos - descuentoDolaresProductoPesos
                            + recargoDolaresProductoDolares - descuentoDolaresProductoDolares;

                        // Actualizar los TextBox
                        txtTotalAPagar.Value = Math.Round(nuevoTotalPesos, 2);
                        txtTotalAPagarDolares.Value = Math.Round(nuevoTotalDolares, 2);

                        txtRestaPagar.Value = Math.Round(nuevoTotalPesos, 2);
                        txtRestaPagarDolares.Value = Math.Round(nuevoTotalDolares, 2);
                        formaPagoAplicada = true;
                    } else
                    {
                        var listaTiposFP = ObtenerTiposFormaPagoEnGrilla();

                        // Verificar si la forma de pago ya existe en la lista
                        if (listaTiposFP.Contains(formaPagoDescripcion, StringComparer.OrdinalIgnoreCase))
                        {
                            // Si la forma de pago ya existe, salir del método
                            return;
                        }
                        else
                        {
                            // Verificar si el tipo de forma de pago ya existe en la grilla
                            bool tipoFormaPagoExisteEnGrilla = false;
                            decimal restoPesos = txtRestaPagar.Value;
                            decimal restoDolares = txtRestaPagarDolares.Value;
                            decimal nuevoRestoPesos = restoPesos + Math.Round(restoPesos * porcentajeRecargo, 2) - Math.Round(restoPesos * porcentajeDescuento, 2);
                            decimal nuevoRestoDolares = restoDolares + Math.Round(restoDolares * porcentajeRecargo, 2) - Math.Round(restoDolares * porcentajeDescuento, 2);

                            foreach (DataGridViewRow row in dgvDataFormasPago.Rows)
                            {
                                if (row.Cells["tipo"].Value != null && row.Cells["tipo"].Value.ToString().Equals(formaPago.tipo, StringComparison.OrdinalIgnoreCase))
                                {
                                    tipoFormaPagoExisteEnGrilla = true;
                                    break;
                                }
                            }

                            if (tipoFormaPagoExisteEnGrilla)
                            {
                                nuevoRestoPesos = restoPesos - Math.Round(restoPesos * porcentajeDescuento, 2);
                                nuevoRestoDolares = restoDolares  - Math.Round(restoDolares * porcentajeDescuento, 2);
                            }
                            else
                            {
                                // Si no existe, se realiza el cálculo del resto a pagar
                               

                                 nuevoRestoPesos = restoPesos + Math.Round(restoDolares * porcentajeRecargo, 2) - Math.Round(restoPesos * porcentajeDescuento, 2);
                                 nuevoRestoDolares = restoDolares + Math.Round(restoDolares * porcentajeRecargo, 2) - Math.Round(restoDolares * porcentajeDescuento, 2);
                                if (listaTiposFP.Contains("CREDITO")){
                                    nuevoRestoPesos = restoPesos  - Math.Round(restoPesos * porcentajeDescuento, 2);
                                    nuevoRestoDolares = restoDolares  - Math.Round(restoDolares * porcentajeDescuento, 2);
                                }



                            }
                            txtRestaPagar.Value = Math.Round(nuevoRestoPesos, 2);
                            txtRestaPagarDolares.Value = Math.Round(nuevoRestoDolares, 2);
                        }


                    }
                        
                }
            }
        }



        private List<string> ObtenerTiposFormaPagoEnGrilla()
        {
            List<string> tiposFormaPago = new List<string>();

            foreach (DataGridViewRow row in dgvDataFormasPago.Rows)
            {
                if (row.Cells["tipo"].Value != null)
                {
                    string tipoFormaPago = row.Cells["tipo"].Value.ToString();
                    tiposFormaPago.Add(tipoFormaPago);
                }
            }

            return tiposFormaPago;
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

                        
                        txtIdPagoParcial.Text = _pagoParcialGlobal.idPagoParcial.ToString();


                        FormaPago formaPagoADescontarRetencion = new CN_FormaPago().ObtenerFPPorDescripcion(_pagoParcialGlobal.formaPago);
                        decimal montoMenosRetencion = Math.Round(_pagoParcialGlobal.monto - (_pagoParcialGlobal.monto * formaPagoADescontarRetencion.porcentajeRetencion) / 100, 2);

                        // Agregar el pago parcial al DataGridView de formas de pago
                        dgvDataFormasPago.Rows.Add(
                            txtIdPagoParcial.Text,        // Columna ID Pago Parcial
                            "PAGO PARCIAL - " + _pagoParcialGlobal.formaPago,                        // Columna Forma de Pago
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





        //private void txtMontoPagoParcial_ValueChanged(object sender, EventArgs e)
        //{
        //    // Verifica si la moneda del pago parcial es pesos antes de llamar a CalcularRestaAPagar
        //    if (_pagoParcialGlobal != null && _pagoParcialGlobal.moneda == "PESOS")
        //    {
        //        CalcularRestaAPagar();
        //    }
        //    else if (_pagoParcialGlobal != null && _pagoParcialGlobal.moneda == "DOLARES" && !pagoParcialDolaresContado)
        //    {
        //        // Solo suma a totalAPagarDolares la primera vez si es en dólares
        //        txtRestaPagarDolares.Value -= _pagoParcialGlobal.monto;
        //        txtRestaPagar.Value -= Math.Round(_pagoParcialGlobal.monto * cotizacionOriginal, 2);
        //        pagoParcialDolaresContado = true;
        //        lblTotalAPagarDolares.Visible = true;
        //        lblRestaPagarDolares.Visible = true;
        //        txtTotalAPagarDolares.Visible = true;
        //        txtRestaPagarDolares.Visible = true;
        //    }
        //}


        

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
            string tipo = formaPagoADescontarRetencion.tipo;


            // Agregar al DataGridView con idFormaPago, formaPago y montoPago
            dgvDataFormasPago.Rows.Add(idFormaPago, formaPago, montoPago, montoMenosRetencion, tipo,defaultImage);

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
            txtCotizacion.ReadOnly = true;
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

        

        private void dgvDataFormasPago_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if(dgvDataFormasPago.Rows.Count == 0)
            {
                txtCotizacion.ReadOnly = false;
            }
        }

       
    }
}
