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
        private Usuario _Usuario;
        public frmVentas(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
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
            

            

            //cboFormaPago4.Items.Add(new OpcionCombo() { Valor = "EFECTIVO", Texto = "EFECTIVO" });
            //cboFormaPago4.Items.Add(new OpcionCombo() { Valor = "DOLARES", Texto = "DOLARES" });
            //cboFormaPago4.Items.Add(new OpcionCombo() { Valor = "DEBITO", Texto = "DEBITO" });
            //cboFormaPago4.Items.Add(new OpcionCombo() { Valor = "CREDITO", Texto = "CREDITO" });
            //cboFormaPago4.Items.Add(new OpcionCombo() { Valor = "TRANSFERENCIA", Texto = "TRANSFERENCIA" });
            //cboFormaPago4.Items.Add(new OpcionCombo() { Valor = "MERCADOPAGO", Texto = "MERCADO PAGO" });
            //cboFormaPago4.Items.Add(new OpcionCombo() { Valor = "CUENTA DNI", Texto = "CUENTA DNI" });
            //cboFormaPago4.Items.Add(new OpcionCombo() { Valor = "MODO", Texto = "MODO" });
            //cboFormaPago4.Items.Add(new OpcionCombo() { Valor = "CRYPTO", Texto = "CRYPTO" });

            //cboFormaPago4.DisplayMember = "Texto";
            //cboFormaPago4.ValueMember = "Valor";
            //cboFormaPago4.SelectedIndex = 0;


            dtpFecha.Text = DateTime.Now.ToString();
            txtIdProducto.Text = "0";
            txtIdProducto.Text = "0";
            txtPagaCon.Text = "";
            txtTotalAPagar.Text = "0";

            var cotizacionDolar = new CN_Cotizacion().CotizacionActiva();
            txtCotizacion.Value = cotizacionDolar.importe;
            txtCotizacion.ReadOnly = true;
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

            using (var modal = new mdProducto())
            {
                var result = modal.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtIdProducto.Text = modal._Producto.idProducto.ToString();
                    txtCodigoProducto.Text = modal._Producto.codigo;
                    txtProducto.Text = modal._Producto.nombre;
                    txtPrecio.Text = modal._Producto.precioVenta.ToString("0.00");
                    
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
                    txtPrecio.Text = oProducto.precioVenta.ToString("0.00");
                    txtStock.Text = oProducto.ToString();
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

                bool respuesta = new CN_Venta().RestarStock(Convert.ToInt32(txtIdProducto.Text), Convert.ToInt32(txtCantidad.Value.ToString()));


                if (respuesta)
                {
                    dgvData.Rows.Add(new object[]{
                    txtIdProducto.Text,
                    txtProducto.Text,
                    precio.ToString("0.00"),
                    txtCantidad.Value.ToString(),
                    (txtCantidad.Value * precio).ToString("0.00")
                });
                    calcularTotal();
                    limpiarProducto();
                    txtCodigoProducto.Select();

                }

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
                txtTotalAPagar.Text = (total*txtCotizacion.Value).ToString("0.00");
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

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex == 5)
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
                    bool respuesta = new CN_Venta().SumarStock(Convert.ToInt32(dgvData.Rows[indice].Cells["idProducto"].Value.ToString()), Convert.ToInt32(dgvData.Rows[indice].Cells["cantidad"].Value.ToString()));


                    if (respuesta)
                    {
                        dgvData.Rows.RemoveAt(indice);
                        
                        txtTotalAPagar.Text = "";
                        calcularTotal();
                    }



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
            pagacon = Convert.ToInt32(txtPagaCon.Text);
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
                if (txtPagaCon.Text != string.Empty) {

                    txtRestaPagar.Text = (Convert.ToDecimal(txtTotalAPagar.Text) - Convert.ToDecimal(txtPagaCon.Text)).ToString();


                    CalcularCambio();

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

            
            

            DataTable detalle_venta = new DataTable();

            detalle_venta.Columns.Add("idProducto", typeof(int));
            detalle_venta.Columns.Add("precioVenta", typeof(decimal));
            detalle_venta.Columns.Add("cantidad", typeof(int));
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
        fechaRegistro = dtpFecha.Value.Date,
        tipoDocumento = ((OpcionCombo)cboTipoDocumento.SelectedItem).Texto,
        nroDocumento = numeroDocumento,
        documentoCliente = txtDocumentoCliente.Text,
        nombreCliente = txtNombreCliente.Text,
       
        montoCambio = Convert.ToDecimal(txtCambioCliente.Text),
        montoTotal = Convert.ToDecimal(txtTotalAPagar.Text),
        formaPago = (OpcionCombo)cboFormaPago.SelectedItem != null ? ((OpcionCombo)cboFormaPago.SelectedItem).Texto : "",
        formaPago2 = (OpcionCombo)cboFormaPago2.SelectedItem != null ? ((OpcionCombo)cboFormaPago2.SelectedItem).Texto : "",
        formaPago3 = (OpcionCombo)cboFormaPago3.SelectedItem != null ? ((OpcionCombo)cboFormaPago3.SelectedItem).Texto : "",
        formaPago4 = (OpcionCombo)cboFormaPago4.SelectedItem != null? ((OpcionCombo)cboFormaPago4.SelectedItem).Texto :"" ,
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


    };

    string mensaje = string.Empty;
    bool respuesta = new CN_Venta().Registrar(oVenta, detalle_venta, out mensaje);
    if (respuesta)
    {
                    new CN_ProductoNegocio().SobrescribirStock(Convert.ToInt32(txtIdProducto.Text), GlobalSettings.SucursalId, -Convert.ToInt32(txtCantidad.Text));
                    txtIdProducto.Text = string.Empty;
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
                            usuarioTransaccion = Environment.GetEnvironmentVariable("usuario"),
                            formaPago = cboFormaPago.Text,
                            cajaAsociada = cajaAsociadaFP1
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
                                usuarioTransaccion = Environment.GetEnvironmentVariable("usuario"),
                                formaPago = cboFormaPago2.Text,
                                cajaAsociada = cajaAsociadaFP2
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
                                usuarioTransaccion = Environment.GetEnvironmentVariable("usuario"),
                                formaPago = cboFormaPago3.Text,
                                cajaAsociada = cajaAsociadaFP3
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
                                usuarioTransaccion = Environment.GetEnvironmentVariable("usuario"),
                                formaPago = cboFormaPago4.Text,
                                cajaAsociada= cajaAsociadaFP4
                            };




                            int idTransaccionGenerado = new CN_Transaccion().RegistrarMovimiento(objTransaccion4, out mensaje);
                        }

                    }
                    

                    var result = MessageBox.Show("Numero de Venta Generado:\n" + numeroDocumento, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        if (result == DialogResult.OK)
            Clipboard.SetText(numeroDocumento);
        
        txtDocumentoCliente.Text = "";
        txtNombreCliente.Text = "";
        dgvData.Rows.Clear();
        calcularTotal();
        txtPagaCon.Text = "";
        txtCambioCliente.Text = "";
        cboFormaPago.SelectedItem = -1;
        txtCotizacion.Value = 1;
        cboFormaPago2.SelectedItem = -1;
        cboFormaPago3.SelectedItem = -1;
        cboFormaPago4.SelectedItem = -1;
                    txtTotalAPagar.Text = string.Empty;
                    txtPagaCon.Text = string.Empty;
                    txtPagaCon2.Text = string.Empty;
                    txtPagaCon3.Text = string.Empty;
                    txtPagaCon4.Text = string.Empty;
                    txtRestaPagar.Text = string.Empty;


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
                    if(checkDescuento.Checked == true)
                    {
                        txtTotalAPagar.Text = (Convert.ToDecimal(txtTotalAPagar.Text) - montoDescuentoRecargo).ToString("0.00");
                    }
                    if (checkRecargo.Checked == true)
                    {
                        txtTotalAPagar.Text = (Convert.ToDecimal(txtTotalAPagar.Text) + montoDescuentoRecargo).ToString("0.00");
                    }
                    txtDescuento.Enabled = false;
                    lblDescuento.Visible = true;
                    
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
                calcularTotal();
            }
        }

        private void txtCotizacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtTotalAPagar.Text = (Convert.ToDecimal(txtTotalAPagar.Text) * txtCotizacion.Value).ToString();
            }
        }

        private void txtMontoDescuento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (checkDescuento.Checked == true)
                {
                    txtTotalAPagar.Text = (Convert.ToDecimal(txtTotalAPagar.Text) - Convert.ToDecimal(txtMontoDescuento.Text)).ToString("0.00");
                }
                if (checkRecargo.Checked == true)
                {
                    txtTotalAPagar.Text = (Convert.ToDecimal(txtTotalAPagar.Text) + Convert.ToDecimal(txtMontoDescuento.Text)).ToString("0.00");
                }
                
            }
        }

        private void txtPagaCon2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (txtPagaCon2.Text != string.Empty) {
                    txtRestaPagar.Text = (Convert.ToDecimal(txtRestaPagar.Text) - Convert.ToDecimal(txtPagaCon2.Text)).ToString("0.00");


                    CalcularCambio();

                }
                
            }

            
        }

        private void txtPagaCon3_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Enter)
            {
                if (txtPagaCon3.Text != string.Empty)
                {
                    txtRestaPagar.Text = (Convert.ToDecimal(txtRestaPagar.Text) - Convert.ToDecimal(txtPagaCon3.Text)).ToString("0.00");


                    CalcularCambio();

                }

            }

            
        }

        private void txtPagaCon4_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Enter)
            {
                if (txtPagaCon4.Text != string.Empty)
                {
                    txtRestaPagar.Text = (Convert.ToDecimal(txtRestaPagar.Text) - Convert.ToDecimal(txtPagaCon4.Text)).ToString("0.00");


                    CalcularCambio();

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

        private void txtIdProducto_TextChanged(object sender, EventArgs e)
        {
            
            if (txtIdProducto.Text != string.Empty)
            {
                int stockProducto = new CN_ProductoNegocio().ObtenerStockProductoEnSucursal(Convert.ToInt32(txtIdProducto.Text), GlobalSettings.SucursalId);
                txtStock.Text = stockProducto.ToString();
            }
            else {
                txtStock.Text = string.Empty;
            }
        }
    }
    }
