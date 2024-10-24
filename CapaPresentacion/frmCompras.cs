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
        private Usuario _Usuario;
        private Compra _Compra;
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
            cboFormaPago2.Text = _Compra.formaPago2;
            cboFormaPago3.Text = _Compra.formaPago3;
            cboFormaPago4.Text = _Compra.formaPago4;
            txtPagaCon.Text = _Compra.montoFP1.ToString("0.00");
            txtPagaCon2.Text = _Compra.montoFP2.ToString();
            txtPagaCon3.Text = _Compra.montoFP3.ToString();
            txtPagaCon4.Text = _Compra.montoFP4.ToString();
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
                txtPrecioventa.Visible = true;
                txtPrecioCompra.Visible = true;
                lblPrecioCompra.Visible = true;
                lblPrecioVenta.Visible = true;
            }
            else
            {
                txtPrecioventa.Visible = false;
                txtPrecioCompra.Visible = false;
                lblPrecioCompra.Visible = false;
                lblPrecioVenta.Visible = false;
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
                    txtPrecioventa.Text = modal._Producto.precioVenta.ToString();
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

            if (!decimal.TryParse(txtPrecioventa.Text, out precioVenta))
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
            txtPrecioventa.Text = "";
            txtCantidad.Value = 1;
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

                txtTotalAPagar.Text = totalCotizado.ToString("0.00");
                txtTotalVentaDolares.Text = total.ToString("0.00");
            }
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
                if (txtPrecioventa.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
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

            // Arreglo de formas de pago y montos pagados
            var formasDePago = new[]
            {
                new { FormaPago = cboFormaPago.Text, Monto = txtPagaCon.Value },
                new { FormaPago = cboFormaPago2.Text, Monto = txtPagaCon2.Value },
                new { FormaPago = cboFormaPago3.Text, Monto = txtPagaCon3.Value },
                new { FormaPago = cboFormaPago4.Text, Monto = txtPagaCon4.Value }
            };

            // Recorre las formas de pago y acumula los montos en dólares o en la moneda local
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
                decimal montoRecargo = Convert.ToDecimal(txtMontoDescuento.Text);  // Usar txtMontoRecargo en lugar de txtMontoDescuento

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

            // Evita restar más de lo que hay que pagar
            if (restoAPagar < 0) restoAPagar = 0;
            if (restoAPagarDolares < 0) restoAPagarDolares = 0;

            // Actualiza los valores en los controles
            txtTotalAPagar.Value = totalAPagar;
            txtTotalAPagarDolares.Value = totalAPagarDolares;
            txtRestaPagar.Value = restoAPagar;
            txtRestaPagarDolares.Value = restoAPagarDolares;

            // Si alguna de las "restas a pagar" es 0, ambas deben ser 0
            if (txtRestaPagar.Value == 0)
            {
                txtRestaPagarDolares.Value = 0;
            }
            if (txtRestaPagarDolares.Value == 0)
            {
                txtRestaPagar.Value = 0;
            }
        }

        private void btnRegistrarCompra_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtIdProveedor.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un proveedor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar Productos en la Compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable detalle_compra = new DataTable();

            detalle_compra.Columns.Add("idProducto", typeof(int));
            detalle_compra.Columns.Add("precioCompra", typeof(decimal));
            detalle_compra.Columns.Add("precioVenta", typeof(decimal));
            detalle_compra.Columns.Add("cantidad", typeof(int));
            detalle_compra.Columns.Add("montoTotal", typeof(decimal));

            foreach (DataGridViewRow row in dgvData.Rows)
            {

                detalle_compra.Rows.Add(
                    new object[]
                    {
                        Convert.ToInt32(row.Cells["idProducto"].Value.ToString()),
                        row.Cells["precioCompra"].Value.ToString(),
                        row.Cells["precioVenta"].Value.ToString(),
                        row.Cells["cantidad"].Value.ToString(),
                        row.Cells["subTotal"].Value.ToString()
                    });
            }

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

            if (cboFormaPago2.SelectedItem != null)
            {
                FormaPago fp2 = new CN_FormaPago().ObtenerFPPorDescripcion(((OpcionCombo)cboFormaPago2.SelectedItem).Texto);
                if (txtPagaCon2.Text != string.Empty)
                {
                    montoPagadoFP2 = montoPagadoFP2 + Convert.ToDecimal(txtPagaCon2.Text);
                }
            }
            if (cboFormaPago3.SelectedItem != null)
            {
                FormaPago fp3 = new CN_FormaPago().ObtenerFPPorDescripcion(((OpcionCombo)cboFormaPago3.SelectedItem).Texto);
                if (txtPagaCon3.Text != string.Empty)
                {
                    montoPagadoFP3 = montoPagadoFP3 + Convert.ToDecimal(txtPagaCon3.Text);
                }
            }
            if (cboFormaPago4.SelectedItem != null)
            {
                FormaPago fp4 = new CN_FormaPago().ObtenerFPPorDescripcion(((OpcionCombo)cboFormaPago4.SelectedItem).Texto);
                if (txtPagaCon4.Text != string.Empty)
                {
                    montoPagadoFP4 = montoPagadoFP4 + Convert.ToDecimal(txtPagaCon4.Text);
                }
            }

            int idCorrelativo = new CN_Compra().ObtenerCorrelativo();
            string numeroDocumento = string.Format("{0:00000}", idCorrelativo);

            Compra oCompra = new Compra()
            {
                oUsuario = new Usuario() { idUsuario = _Usuario.idUsuario },
                idNegocio = GlobalSettings.SucursalId,
                oProveedor = new Proveedor() { idProveedor = Convert.ToInt32(txtIdProveedor.Text) },
                tipoDocumento = ((OpcionCombo)cboTipoDocumento.SelectedItem).Texto,
                nroDocumento = numeroDocumento,
                montoTotal = Convert.ToDecimal(txtTotalAPagar.Text),
                formaPago = (OpcionCombo)cboFormaPago.SelectedItem != null ? ((OpcionCombo)cboFormaPago.SelectedItem).Texto : "",
                formaPago2 = (OpcionCombo)cboFormaPago2.SelectedItem != null ? ((OpcionCombo)cboFormaPago2.SelectedItem).Texto : "",
                formaPago3 = (OpcionCombo)cboFormaPago3.SelectedItem != null ? ((OpcionCombo)cboFormaPago3.SelectedItem).Texto : "",
                formaPago4 = (OpcionCombo)cboFormaPago4.SelectedItem != null ? ((OpcionCombo)cboFormaPago4.SelectedItem).Texto : "",
                montoFP1 = txtPagaCon.Text != string.Empty ? Convert.ToDecimal(txtPagaCon.Text) : 0,
                montoFP2 = txtPagaCon2.Text != string.Empty ? Convert.ToDecimal(txtPagaCon2.Text) : 0,
                montoFP3 = txtPagaCon3.Text != string.Empty ? Convert.ToDecimal(txtPagaCon3.Text) : 0,
                montoFP4 = txtPagaCon4.Text != string.Empty ? Convert.ToDecimal(txtPagaCon4.Text) : 0,
                montoPago = montoPagado,
                montoPagoFP2 = montoPagadoFP2,
                montoPagoFP3 = montoPagadoFP3,
                montoPagoFP4 = montoPagadoFP4,
                observaciones = txtObservaciones.Text

            };

            string mensaje = string.Empty;
            string actualizacionStock = string.Empty;
            string actualizacionPrecios = string.Empty;
            int idCompragenerado = 0;
            bool respuesta = new CN_Compra().Registrar(oCompra, detalle_compra, out mensaje, out idCompragenerado);
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

                        // Actualizar el stock del producto
                        actualizacionStock = new CN_ProductoNegocio().CargarOActualizarStockProducto(idProducto, GlobalSettings.SucursalId, cantidad);
                        Producto actualizarPrecios = new Producto();
                        actualizarPrecios.idProducto = idProducto;
                        if (checkCompraPesos.Checked)
                        {
                            
                            actualizarPrecios.costoPesos = precioCompra;
                            actualizarPrecios.ventaPesos = precioVenta;
                            actualizarPrecios.precioCompra = Math.Round(precioCompra / txtCotizacion.Value,2);
                            actualizarPrecios.precioVenta = Math.Round(precioVenta / txtCotizacion.Value, 2);
                        } else
                        {
                            
                            actualizarPrecios.precioCompra = precioCompra;
                            actualizarPrecios.precioVenta = precioVenta;
                            actualizarPrecios.costoPesos = 0;
                            actualizarPrecios.ventaPesos = 0;

                        }
                        
                        
                        bool editarPrecios = new CN_Producto().EditarPrecios(actualizarPrecios, out mensaje);
                        
                    }
                }
                txtIdProducto.Text = string.Empty;
                string nombreProveedor = txtRazonSocial.Text;




                calcularTotal();

                if (!checkCaja.Checked)
                {

                    List<CajaRegistradora> lista = new CN_CajaRegistradora().Listar(GlobalSettings.SucursalId);

                    CajaRegistradora cajaAbierta = lista.Where(c => c.estado == true).FirstOrDefault();
                    if (cajaAbierta != null)

                    {

                        if (oCompra.montoPago > 0)
                        {
                            var cajaAsociadaFP1 = new CN_FormaPago().ObtenerFPPorDescripcion(oCompra.formaPago).cajaAsociada;
                            TransaccionCaja objTransaccion = new TransaccionCaja()
                            {
                                idCajaRegistradora = cajaAbierta.idCajaRegistradora,

                                hora = dtpFecha.Value.Hour.ToString(),
                                tipoTransaccion = "SALIDA",
                                monto = oCompra.montoPago * -1,
                                docAsociado = "Compra Numero:" + " " + numeroDocumento + " Proveedor:" + " " + nombreProveedor,
                                usuarioTransaccion = Environment.GetEnvironmentVariable("usuario"),
                                formaPago = cboFormaPago.Text,
                                cajaAsociada = cajaAsociadaFP1,
                                idVenta = null,
                                idCompra = idCompragenerado,
                                idNegocio = GlobalSettings.SucursalId,
                                concepto = "COMPRA"
                            };




                            int idTransaccionGenerado = new CN_Transaccion().RegistrarMovimiento(objTransaccion, out mensaje);
                        }

                        if (oCompra.montoPagoFP2 > 0)
                        {
                            var cajaAsociadaFP2 = new CN_FormaPago().ObtenerFPPorDescripcion(oCompra.formaPago2).cajaAsociada;
                            TransaccionCaja objTransaccion2 = new TransaccionCaja()
                            {
                                idCajaRegistradora = cajaAbierta.idCajaRegistradora,

                                hora = dtpFecha.Value.Hour.ToString(),
                                tipoTransaccion = "SALIDA",
                                monto = oCompra.montoPagoFP2 * -1,
                                docAsociado = "Compra Numero:" + " " + numeroDocumento + " Proveedor:" + " " + nombreProveedor,
                                usuarioTransaccion = Environment.GetEnvironmentVariable("usuario"),
                                formaPago = cboFormaPago2.Text,
                                cajaAsociada = cajaAsociadaFP2,
                                idVenta = null,
                                idCompra = idCompragenerado,
                                idNegocio = GlobalSettings.SucursalId,
                                concepto = "COMPRA"
                            };




                            int idTransaccionGenerado = new CN_Transaccion().RegistrarMovimiento(objTransaccion2, out mensaje);
                        }

                        if (oCompra.montoPagoFP3 > 0)
                        {
                            var cajaAsociadaFP3 = new CN_FormaPago().ObtenerFPPorDescripcion(oCompra.formaPago3).cajaAsociada;
                            TransaccionCaja objTransaccion3 = new TransaccionCaja()
                            {
                                idCajaRegistradora = cajaAbierta.idCajaRegistradora,

                                hora = dtpFecha.Value.Hour.ToString(),
                                tipoTransaccion = "SALIDA",
                                monto = oCompra.montoPagoFP3,
                                docAsociado = "Compra Numero:" + " " + numeroDocumento + " Proveedor:" + " " + nombreProveedor,
                                usuarioTransaccion = Environment.GetEnvironmentVariable("usuario"),
                                formaPago = cboFormaPago3.Text,
                                cajaAsociada = cajaAsociadaFP3,
                                idVenta = null,
                                idCompra = idCompragenerado,
                                idNegocio = GlobalSettings.SucursalId,
                                concepto = "COMPRA"
                            };




                            int idTransaccionGenerado = new CN_Transaccion().RegistrarMovimiento(objTransaccion3, out mensaje);
                        }

                        if (oCompra.montoPagoFP4 > 0)
                        {
                            var cajaAsociadaFP4 = new CN_FormaPago().ObtenerFPPorDescripcion(oCompra.formaPago4).cajaAsociada;
                            TransaccionCaja objTransaccion4 = new TransaccionCaja()
                            {
                                idCajaRegistradora = cajaAbierta.idCajaRegistradora,

                                hora = dtpFecha.Value.Hour.ToString(),
                                tipoTransaccion = "SALIDA",
                                monto = oCompra.montoPagoFP4,
                                docAsociado = "Compra Numero:" + " " + numeroDocumento + " Proveedor:" + " " + nombreProveedor,
                                usuarioTransaccion = Environment.GetEnvironmentVariable("usuario"),
                                formaPago = cboFormaPago4.Text,
                                cajaAsociada = cajaAsociadaFP4,
                                idVenta = null,
                                idCompra = idCompragenerado,
                                idNegocio = GlobalSettings.SucursalId,
                                concepto = "COMPRA"
                            };




                            int idTransaccionGenerado = new CN_Transaccion().RegistrarMovimiento(objTransaccion4, out mensaje);
                        }

                    }
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
            cboFormaPago2.SelectedIndex = -1;
            cboFormaPago3.SelectedIndex = -1;
            cboFormaPago4.SelectedIndex = -1;
            txtPagaCon.Text = string.Empty;
            txtPagaCon2.Text = string.Empty;
            txtPagaCon3.Text = string.Empty;
            txtPagaCon4.Text = string.Empty;
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

            if (txtPagaCon2.Text != string.Empty)
            {
                pagacon += Convert.ToDecimal(txtPagaCon2.Text);

            }
            else
            {
                pagacon += 0;
            }

            if (txtPagaCon3.Text != string.Empty)
            {
                pagacon += Convert.ToDecimal(txtPagaCon3.Text);

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
                if (cboFormaPago.Text == "DOLAR" || cboFormaPago.Text == "DOLAR EFECTIVO")
                {
                    txtTotalAPagar.Value = txtRestaPagarDolares.Value * txtCotizacion.Value;
                    CalcularRestaAPagar();
                }



            }




        }


        private void txtPagaCon2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (txtPagaCon2.Text != string.Empty)
                {

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
                    if (txtRestaPagar.Value == 0)
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

        private void checkRecargo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkRecargo.Checked)
            {
                txtMontoDescuento.Visible = true;
                txtMontoDescuento.Enabled = true;
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

        private void txtDescuento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                if (Convert.ToDecimal(txtDescuento.Text) > 0 && Convert.ToDecimal(txtDescuento.Text) <= 100 && (txtDescuento.Text != ""))
                {
                    txtMontoDescuento.Visible = true;
                    txtMontoDescuento.Enabled = false;
                    decimal montoDescuentoRecargo = (Convert.ToDecimal(txtTotalAPagar.Text) * Convert.ToDecimal(txtDescuento.Text)) / 100;
                    txtMontoDescuento.Text = montoDescuentoRecargo.ToString("0.00");
                    if (checkDescuento.Checked == true)
                    {
                        txtTotalAPagar.Text = (Convert.ToDecimal(txtTotalAPagar.Text) - montoDescuentoRecargo).ToString("0.00");


                        if (cboFormaPago.Text == "DOLAR" || cboFormaPago.Text == "DOLAR EFECTIVO")
                        {
                            txtRestaPagar.Text = txtTotalAPagar.Text;
                        }
                        else
                        {
                            txtRestaPagar.Text = (Convert.ToDecimal(txtRestaPagar.Text) - montoDescuentoRecargo).ToString("0.00");
                        }
                    }
                    if (checkRecargo.Checked == true)
                    {
                        txtTotalAPagar.Text = (Convert.ToDecimal(txtTotalAPagar.Text) + montoDescuentoRecargo).ToString("0.00");
                        if (cboFormaPago.Text == "DOLAR" || cboFormaPago.Text == "DOLAR EFECTIVO")
                        {
                            txtRestaPagar.Text = txtTotalAPagar.Text;
                        }
                        else
                        {
                            txtRestaPagar.Text = (Convert.ToDecimal(txtRestaPagar.Text) + montoDescuentoRecargo).ToString("0.00");
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
                if (checkDescuento.Checked == true)
                {
                    if (checkMonedaDolar.Checked)
                    {

                        txtRestaPagarDolares.Value = txtRestaPagarDolares.Value - Convert.ToDecimal(txtMontoDescuento.Text);
                        txtRestaPagar.Value = txtTotalAPagarDolares.Value * txtCotizacion.Value - Convert.ToDecimal(txtMontoDescuento.Text) * txtCotizacion.Value;

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
                    }
                    else
                    {
                        txtRestaPagar.Text = (Convert.ToDecimal(txtTotalAPagar.Text) + Convert.ToDecimal(txtMontoDescuento.Text)).ToString("0.00");
                    }
                    CalcularRestaAPagar();
                }

            }
        }
    }
}



