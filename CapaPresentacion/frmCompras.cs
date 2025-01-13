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
    public partial class frmCompras : Form
    {
        private decimal cotizacionOriginal;
        private bool cotizacionCambio = false;
        private Usuario _Usuario;
        private Compra _Compra;
        private int contadorFormasPago = 0;
        private Image defaultImage = Properties.Resources.trash;
        private int _idCompraGenerada;
        public decimal cotizacionActiva { get; set; } = new CN_Cotizacion().CotizacionActiva().importe;
        public frmCompras(Usuario oUsuario = null, Compra oCompra = null)
        {
            _Usuario = oUsuario;
            _Compra = oCompra;
            InitializeComponent();
            if(_Compra != null)
            {
                CargarDatosCompra();
            }
        }
        private void CargarDatosCompra()

        {
            //CargarComboBoxVendedores();
            lblTitulo.Text = String.Format("EDITAR COMPRA NUMERO {0}", _Compra.nroDocumento);
            // Aquí puedes cargar los datos de la venta en los controles del formulario
            cboTipoDocumento.Text = _Compra.tipoDocumento;
            txtRazonSocial.Text = _Compra.oProveedor.razonSocial;
            txtCUIT.Text = _Compra.oProveedor.documento;
            txtObservaciones.Text = _Compra.observaciones;
            foreach (var item in _Compra.oDetalleCompra)
            {
                dgvData.Rows.Add(item.oProducto.idProducto, item.oProducto.nombre, item.precioCompra, item.precioVenta, item.cantidad, item.montoTotal, "");
            }
            cboFormaPago.Text = _Compra.formaPago;
            
            txtPagaCon.Text = _Compra.montoFP1.ToString("0.00");
            
            txtTotalAPagar.Text = _Compra.montoTotal.ToString();

            // Establecer el item seleccionado en el ComboBox cboVendedores
            //if (cboVendedores.Items.Count > 0)
            //{
            //    foreach (var item in cboVendedores.Items)
            //    {
            //        var opcionCombo = item as OpcionCombo; // Cast al tipo OpcionCombo
            //        if (opcionCombo != null && Convert.ToInt32(opcionCombo.Valor) == _Compra.idVendedor) // Comparar con idVendedor
            //        {
            //            cboVendedores.SelectedItem = opcionCombo; // Selecciona el item correspondiente
            //            //cboVendedores.Text = opcionCombo.Texto;
            //            break;
            //        }
            //    }
            //}
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {

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
        private void frmCompras_Load(object sender, EventArgs e)
        {
            CargarComboBoxFormaPago();
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Factura A", Texto = "Factura A" });
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Factura B", Texto = "Factura B" });
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Factura C", Texto = "Factura C" });
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Remito R", Texto = "Remito R" });
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Presupuesto", Texto = "Presupuesto" });
            cboTipoDocumento.DisplayMember = "Texto";
            cboTipoDocumento.ValueMember = "Valor";
            cboTipoDocumento.SelectedIndex = 0;

            var cotizacionDolar = new CN_Cotizacion().CotizacionActiva();
            txtCotizacion.Value = cotizacionDolar.importe;
            txtCotizacion.ReadOnly = true;


            dtpFecha.Text = DateTime.Now.ToString();
            txtIdProducto.Text = "0";
            txtIdProducto.Text = "0";

            if (_Usuario.oRol.idRol == 1)
            {
                txtPrecioVenta.Visible = true;
                txtPrecioCompra.Visible = true;
                lblPrecioCompra.Visible = true;
                lblPrecioVenta.Visible = true;
            }
            else
            {
                txtPrecioVenta.Visible = false;
                txtPrecioCompra.Visible = false;
                lblPrecioCompra.Visible = false;
                lblPrecioVenta.Visible = false;
            }

            if (decimal.TryParse(txtCotizacion.Text, out decimal valorCotizacion))
            {
                cotizacionOriginal = valorCotizacion;
            }
            else
            {
                cotizacionOriginal = 0; // Si no es un valor válido, considerarlo como 0
            }
        }

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProveedor())
            {
                var result = modal.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtIdProveedor.Text = modal._Proveedor.idProveedor.ToString();
                    txtCUIT.Text = modal._Proveedor.documento;
                    txtRazonSocial.Text = modal._Proveedor.razonSocial;
                }
                else
                {
                    txtCUIT.Select();
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
                    txtPrecioCompra.Text = modal._Producto.precioCompra.ToString();
                    txtPrecioVenta.Text = modal._Producto.precioVenta.ToString();
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
                if (oProducto != null)
                {
                    txtCodigoProducto.BackColor = Color.ForestGreen;
                    txtIdProducto.Text = oProducto.idProducto.ToString();
                    txtProducto.Text = oProducto.nombre;
                    txtPrecioCompra.Select();
                }
                else
                {
                    txtCodigoProducto.BackColor = Color.IndianRed;
                    txtIdProducto.Text = "0";
                    txtProducto.Text = "";



                }
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            decimal precioCompra = 0;
            decimal precioVenta = 0;
            bool producto_existe = false;

            if (int.Parse(txtIdProducto.Text) == 0)
            {
                MessageBox.Show("Debe Seleccionar un Producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!decimal.TryParse(txtPrecioCompra.Text, out precioCompra))
            {
                MessageBox.Show("Precio Compra - Formato Moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrecioCompra.Select();
                return;
            }

            if (!decimal.TryParse(txtPrecioVenta.Text, out precioVenta))
            {
                MessageBox.Show("Precio Venta - Formato Moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrecioCompra.Select();
                return;
            }

            foreach (DataGridViewRow fila in dgvData.Rows)
            {
                if (fila.Cells["idProducto"].Value.ToString() == txtIdProducto.Text)
                {
                    producto_existe = true;
                    break;
                }

            }
            if (!producto_existe)
            {

                dgvData.Rows.Add(new object[]{
                    txtIdProducto.Text,
                    txtProducto.Text,
                    precioCompra.ToString("0.00"),
                    precioVenta.ToString("0.00"),
                    txtCantidad.Value.ToString(),
                    (txtCantidad.Value * precioCompra).ToString("0.00")
                });
                calcularTotal();
                limpiarProducto();
                txtCodigoProducto.Select();
            }



        }

        private void limpiarProducto()
        {
            txtIdProducto.Text = "0";
            txtProducto.Text = "";
            txtCodigoProducto.BackColor = Color.White;
            txtCodigoProducto.Text = "";
            txtPrecioCompra.Text = "";
            txtPrecioVenta.Text = "";
            txtCantidad.Value = 1;
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
                    // Validamos que las celdas no sean nulas antes de convertir
                    if ( row.Cells["subTotal"].Value != null)
                    {
                        totalPesos += Convert.ToDecimal(row.Cells["subTotal"].Value);
                        totalDolares += Convert.ToDecimal(row.Cells["subTotal"].Value);
                    }
                }

                // Ajustamos el total si la cotización ha cambiado
                if (cotizacionCambio)
                {
                    totalPesos = Math.Round(totalDolares * txtCotizacion.Value, 2);
                }

                // Actualizamos los TextBox correspondientes
                txtTotalAPagar.Value = totalPesos*txtCotizacion.Value;
                txtTotalAPagarDolares.Value = totalDolares;

                // Solo asignamos RestaPagar si es la primera vez
                if (txtRestaPagar.Value == 0 && txtRestaPagarDolares.Value == 0)
                {
                    txtRestaPagar.Value = totalPesos*txtCotizacion.Value;
                    txtRestaPagarDolares.Value = totalDolares;
                }
            }

            // Retornamos el total en pesos
            return txtTotalAPagar.Value;
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex == 6)
            {

                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.trash25.Width;
                var h = Properties.Resources.trash25.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Width - h) / 2;
                e.Graphics.DrawImage(Properties.Resources.trash25, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {

                    dgvData.Rows.RemoveAt(indice);
                    calcularTotal();




                }

            }
        }

        private void txtPrecioCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {

                e.Handled = false;
            }
            else
            {
                if (txtPrecioCompra.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
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

        private void txtPrecioventa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {

                e.Handled = false;
            }
            else
            {
                if (txtPrecioVenta.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
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

                if (formaPago == "DOLAR" || formaPago == "DOLAR EFECTIVO" || formaPago == "DOLAR - PAGO PARCIAL" || formaPago == "DOLAR EFECTIVO - PAGO PARCIAL")
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

        private DataTable CrearDetalleCompra()
        {
            DataTable detalleCompra = new DataTable();

            detalleCompra.Columns.Add("idProducto", typeof(int));
            detalleCompra.Columns.Add("precioCompra", typeof(decimal));
            detalleCompra.Columns.Add("precioVenta", typeof(decimal));
            detalleCompra.Columns.Add("cantidad", typeof(int));
            detalleCompra.Columns.Add("montoTotal", typeof(decimal));

            foreach (DataGridViewRow row in dgvData.Rows)
            {

                detalleCompra.Rows.Add(
                    new object[]
                    {
                        Convert.ToInt32(row.Cells["idProducto"].Value.ToString()),
                        row.Cells["precioCompra"].Value.ToString(),
                        row.Cells["precioVenta"].Value.ToString(),
                        row.Cells["cantidad"].Value.ToString(),
                        row.Cells["subTotal"].Value.ToString()
                    });
            }
            return detalleCompra;

           
        }

        private Compra CrearCompra(DataTable detalleCompra)
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

            return new Compra()            
            {
                oUsuario = new Usuario() { idUsuario = _Usuario.idUsuario },
                idNegocio = GlobalSettings.SucursalId,
                oProveedor = new Proveedor() { idProveedor = Convert.ToInt32(txtIdProveedor.Text) },
                tipoDocumento = ((OpcionCombo)cboTipoDocumento.SelectedItem).Texto,
                nroDocumento = numeroDocumento,
                montoTotal = montoPagoFP1 + montoPagoFP2 + montoPagoFP3 + montoPagoFP4,
                formaPago = formaPagoFP1,
                formaPago2 = formaPagoFP2,
                formaPago3 = formaPagoFP3,
                formaPago4 = formaPagoFP4,
                montoFP1 = montoPagoFP1,
                montoFP2 = montoPagoFP2,
                montoFP3 = montoPagoFP3,
                montoFP4 = montoPagoFP4,
                montoPago = montoRecibidoPagoFP1, // Sumar los montos de pago
                montoPagoFP2 = montoRecibidoPagoFP2,
                montoPagoFP3 = montoRecibidoPagoFP3,
                montoPagoFP4 = montoRecibidoPagoFP4,
                observaciones = txtObservaciones.Text

            };

           
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtIdProveedor.Text) || txtIdProveedor.Text =="0")
                return MostrarMensajeError("Debe Seleccionar un Proveedor");

           

            if (dgvData.Rows.Count < 1)
                return MostrarMensajeError("Debe ingresar productos en la Compra");

            if (checkDescuento.Checked && string.IsNullOrWhiteSpace(txtDescuento.Text))
                return MostrarMensajeError("Debe ingresar un porcentaje de descuento");

            // Verificar si el DataGridView de formas de pago tiene filas
            if (dgvDataFormasPago.Rows.Count < 1)
                return MostrarMensajeError("Debe agregar al menos una forma de pago al detalle de la Compra");

            

            return true;
        }
        private bool MostrarMensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return false;
        }

        private bool ProcesarCompra(Compra oCompra, DataTable detalleCompra, out string mensaje, out int idCompraGenerada)
        {
            // Llamamos al método Registrar y asignamos el resultado a la variable global
            bool resultado = new CN_Compra().Registrar(oCompra, detalleCompra, out mensaje, out idCompraGenerada);

            if (resultado)
            {
                _idCompraGenerada = idCompraGenerada; // Guardamos el ID en la variable global
            }

            return resultado;
        }

        private void RegistrarTransaccionesCaja(Compra oCompra, int idCompraGenerada)
        {
            // Obtener la caja abierta
            List<CajaRegistradora> lista = new CN_CajaRegistradora().Listar(GlobalSettings.SucursalId);
            CajaRegistradora cajaAbierta = lista.FirstOrDefault(c => c.estado == true);

            if (cajaAbierta != null)
            {
                // Crear un diccionario de formas de pago con sus montos asociados
                var formasDePago = new Dictionary<string, decimal>();

                // Verificar si la forma de pago ya está en el diccionario antes de agregarla
                if (!formasDePago.ContainsKey(oCompra.formaPago) && oCompra.montoPago > 0)
                {
                    formasDePago.Add(oCompra.formaPago, oCompra.montoPago);
                }

                if (!formasDePago.ContainsKey(oCompra.formaPago2) && oCompra.montoPagoFP2 > 0)
                {
                    formasDePago.Add(oCompra.formaPago2, oCompra.montoPagoFP2);
                }

                if (!formasDePago.ContainsKey(oCompra.formaPago3) && oCompra.montoPagoFP3 > 0)
                {
                    formasDePago.Add(oCompra.formaPago3, oCompra.montoPagoFP3);
                }

                if (!formasDePago.ContainsKey(oCompra.formaPago4) && oCompra.montoPagoFP4 > 0)
                {
                    formasDePago.Add(oCompra.formaPago4, oCompra.montoPagoFP4);
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
                            tipoTransaccion = "SALIDA", // Tipo de transacción
                            monto = pago.Value,
                            docAsociado = $"Compra Numero: {oCompra.nroDocumento} Proveedor: {oCompra.oProveedor.razonSocial}",
                            usuarioTransaccion = _Usuario.nombreCompleto,
                            formaPago = pago.Key,
                            cajaAsociada = cajaAsociada,
                            idVenta = null,
                            idCompra = idCompraGenerada,
                            idNegocio = GlobalSettings.SucursalId,
                            concepto = "COMPRA",
                            idPagoParcial = null
                        };

                        // Registrar la transacción en la base de datos
                        string mensaje;
                        int idTransaccionGenerado = new CN_Transaccion().RegistrarMovimiento(objTransaccion, out mensaje);
                    }
                }
            }
        }

        private void ActualizarStockYPrecios(int idProducto, int cantidad, decimal precioCompra, decimal precioVenta)
        {
            // Obtener el producto
            Producto producto = new CN_Producto().ObtenerProductoPorId(idProducto);

            // Actualizar el stock del producto
            string actualizacionStock = new CN_ProductoNegocio().CargarOActualizarStockProducto(idProducto, GlobalSettings.SucursalId, cantidad);

            // Configuración de precios

            string mensaje;

            PrecioProducto precioActualPesos = new CN_PrecioProducto().ObtenerPreciosPorProductoYMoneda(idProducto, 1); // Moneda: Pesos
            PrecioProducto precioActualDolar = new CN_PrecioProducto().ObtenerPreciosPorProductoYMoneda(idProducto, 2); // Moneda: Dólares




            // Si el producto es en dólares
            if (producto.productoDolar)
            {
                if (precioActualDolar == null ||
        precioActualDolar.PrecioCompra != txtPrecioCompra.Value ||
        precioActualDolar.PrecioVenta != txtPrecioVenta.Value)
                {


                    PrecioProducto objPrecioProductoPesos = new PrecioProducto()
                    {
                        IdProducto = producto.idProducto,
                        IdMoneda = 1,
                        PrecioCompra = Math.Round((txtPrecioCompra.Value * cotizacionActiva), 2),
                        PrecioVenta = Math.Round((txtPrecioVenta.Value * cotizacionActiva), 2),
                        PrecioLista = Math.Round(txtPrecioVenta.Value * cotizacionActiva * 1.40m, 2),
                        PrecioEfectivo = Math.Round((txtPrecioVenta.Value * cotizacionActiva * 1.40m) * 0.85m, 2),
                        FechaRegistro = DateTime.Now
                    };
                    int idPrecioPesos = new CN_PrecioProducto().RegistrarPrecioProducto(objPrecioProductoPesos, out mensaje);
                    PrecioProducto objPrecioProductoDolar = new PrecioProducto()
                    {
                        IdProducto = producto.idProducto,
                        IdMoneda = 2,
                        PrecioCompra = Math.Round(txtPrecioCompra.Value, 2),
                        PrecioVenta = Math.Round(txtPrecioVenta.Value, 2),
                        FechaRegistro = DateTime.Now,
                        PrecioEfectivo = Math.Round(txtPrecioVenta.Value, 2),
                        PrecioLista = Math.Round(txtPrecioVenta.Value * 1.40m, 2)
                    };
                    int idPrecioDolar = new CN_PrecioProducto().RegistrarPrecioProducto(objPrecioProductoDolar, out mensaje);

                }
            }
            else
            {
                if (precioActualPesos == null ||
        precioActualPesos.PrecioCompra != txtPrecioCompra.Value ||
        precioActualPesos.PrecioVenta != txtPrecioVenta.Value)
                {

                    PrecioProducto objPrecioProductoPesos = new PrecioProducto()
                    {
                        IdProducto = producto.idProducto,
                        IdMoneda = 1,
                        PrecioCompra = Math.Round(txtPrecioCompra.Value, 2),
                        PrecioVenta = Math.Round(txtPrecioVenta.Value, 2),
                        PrecioLista = Math.Round(txtPrecioVenta.Value * 1.40m, 2),
                        PrecioEfectivo = Math.Round(txtPrecioVenta.Value * 1.40m * 0.85m, 2),
                        FechaRegistro = DateTime.Now
                    };
                    int idPrecioPesos = new CN_PrecioProducto().RegistrarPrecioProducto(objPrecioProductoPesos, out mensaje);
                    PrecioProducto objPrecioProductoDolar = new PrecioProducto()
                    {
                        IdProducto = producto.idProducto,
                        IdMoneda = 2,
                        PrecioCompra = Math.Round((txtPrecioCompra.Value / cotizacionActiva), 2),
                        PrecioVenta = Math.Round((txtPrecioVenta.Value / cotizacionActiva), 2),
                        FechaRegistro = DateTime.Now,
                        PrecioEfectivo = Math.Round(txtPrecioVenta.Value, 2),
                        PrecioLista = Math.Round((txtPrecioVenta.Value / cotizacionActiva) * 1.40m, 2)
                    };
                    int idPrecioDolar = new CN_PrecioProducto().RegistrarPrecioProducto(objPrecioProductoDolar, out mensaje);

                }
            }
            
        }

        

        private void btnRegistrarCompra_Click(object sender, EventArgs e)
        {
            

            if (!ValidarFormulario()) return;

            DataTable detalleCompra = CrearDetalleCompra();

            // Generar la venta
            Compra oCompra = CrearCompra(detalleCompra);

            // Procesar registro de venta
            string mensaje = string.Empty;
            int idCompraGenerado;
            bool respuesta = ProcesarCompra(oCompra, detalleCompra, out mensaje, out idCompraGenerado);

            decimal montoPagado = 0;
            decimal montoPagadoFP2 = 0;
            decimal montoPagadoFP3 = 0;
            decimal montoPagadoFP4 = 0;
            if (cboFormaPago.SelectedItem != null)
            {
                FormaPago fp1 = new CN_FormaPago().ObtenerFPPorDescripcion(((OpcionCombo)cboFormaPago.SelectedItem).Texto);
                if (txtPagaCon.Text != string.Empty)
                {
                    montoPagado = montoPagado + Convert.ToDecimal(txtPagaCon.Text);
                }
            }


            int idCorrelativo = new CN_Compra().ObtenerCorrelativo();
            string numeroDocumento = string.Format("{0:00000}", idCorrelativo);
            

            
            string actualizacionStock = string.Empty;
            string actualizacionPrecios = string.Empty;
            int idCompragenerado = 0;
            
            if (respuesta)
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Cells["idProducto"].Value != null && row.Cells["cantidad"].Value != null)
                    {
                        int idProducto = Convert.ToInt32(row.Cells["idProducto"].Value);
                        int cantidad = Convert.ToInt32(row.Cells["cantidad"].Value);
                        decimal precioCompra = Convert.ToDecimal(row.Cells["precioCompra"].Value);
                        decimal precioVenta = Convert.ToDecimal(row.Cells["precioVenta"].Value);

                        Producto producto = new CN_Producto().ObtenerProductoPorId(idProducto);
                        
                       


                        ActualizarStockYPrecios(idProducto, cantidad, precioCompra, precioVenta);
                        


                       
                        
                    }
                }
                txtIdProducto.Text = string.Empty;
                string nombreProveedor = txtRazonSocial.Text;




                calcularTotal();

                if (!checkCaja.Checked)
                {

                    RegistrarTransaccionesCaja(oCompra, idCompraGenerado);
                }
                MessageBox.Show("Numero de Compra Generado:\n" + numeroDocumento, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
            }


            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Limpiar()
        {
            txtIdProveedor.Text = "0";
            txtCUIT.Text = "";
            txtRazonSocial.Text = "";
            cboFormaPago.SelectedIndex = -1;
           
            txtPagaCon.Text = string.Empty;
            dgvDataFormasPago.Rows.Clear();
            txtTotalAPagar.Text = string.Empty;
            dgvData.Rows.Clear();
            txtObservaciones.Text = string.Empty;

        }
        private void CalcularCambio()
        {
            if (txtTotalAPagar.Text.Trim() == "")
            {
                MessageBox.Show("No existen productos en la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            decimal pagacon;
            if (cboFormaPago.Text == "DOLAR" || cboFormaPago.Text == "DOLAR EFECTIVO")
            {

                pagacon = 0;
            }
            else
            {
                pagacon = Convert.ToDecimal(txtPagaCon.Text);
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

        private void calcularTotalConDolares()
        {
            decimal total = Convert.ToDecimal(txtTotalVentaDolares.Text);




            decimal totalCotizado = total * txtCotizacion.Value;
            decimal totalRedondeado = Math.Ceiling(totalCotizado / 500) * 500;
            txtTotalAPagar.Text = totalRedondeado.ToString("0.00");
            txtRestaPagar.Text = txtTotalAPagar.Text;

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
                decimal montoMenosRetencion = Math.Round(montoPago - (montoPago * formaPagoADescontarRetencion.porcentajeRetencion) / 100, 2);



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


        


        private void checkDescuento_CheckedChanged(object sender, EventArgs e)
        {

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
            decimal montoMenosRetencion = Math.Round(montoPago - (montoPago * formaPagoADescontarRetencion.porcentajeRetencion) / 100, 2);



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

        private void dgvDataFormasPago_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            txtCotizacion.ReadOnly = true;
            CalcularRestaAPagar();
        }

        private void dgvDataFormasPago_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (dgvDataFormasPago.Rows.Count == 0)
            {
                txtCotizacion.ReadOnly = false;
            }
        }

        private void txtCotizacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (dgvData.Rows.Count <= 0)
                {
                    MessageBox.Show("Para modificar la cotización tiene que haber productos en la Venta.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCotizacion.Value = cotizacionOriginal;
                    txtCotizacion.ReadOnly = false;

                    return; // Salir del método si hay filas en el DataGridView
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
    }
    }




