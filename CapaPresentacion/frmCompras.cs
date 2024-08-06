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
        public frmCompras(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
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
            
            dtpFecha.Text = DateTime.Now.ToString();
            txtIdProducto.Text = "0";
            txtIdProducto.Text = "0";

            if(_Usuario.oRol.idRol == 1)
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
                if(result== DialogResult.OK)
                {
                    txtIdProveedor.Text = modal._Proveedor.idProveedor.ToString() ;
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

            using (var modal = new mdProducto())
            {
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
            if(e.KeyData== Keys.Enter)
            {
                Producto oProducto = new CN_Producto().Listar(GlobalSettings.SucursalId).Where(p => p.codigo == txtCodigoProducto.Text && p.estado == true).FirstOrDefault();
                if (oProducto != null)
                {
                    txtCodigoProducto.BackColor = Color.ForestGreen;
                    txtIdProducto.Text = oProducto.idProducto.ToString();
                    txtProducto.Text = oProducto.nombre;
                    txtPrecioCompra.Select();
                }
                else {
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

            if(!decimal.TryParse(txtPrecioCompra.Text, out precioCompra))
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
                if(fila.Cells["idProducto"].Value.ToString()== txtIdProducto.Text)
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
            if(dgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    total += Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString());

                }
                txtTotalAPagar.Text = total.ToString("0.00");
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
            if (Char.IsDigit(e.KeyChar)) {

                e.Handled = false;
            }
            else
            {
                if(txtPrecioCompra.Text.Trim().Length ==0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if(Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
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

        private void btnRegistrarCompra_Click(object sender, EventArgs e)
        {
            if(Convert.ToInt32(txtIdProveedor.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un proveedor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if(dgvData.Rows.Count < 1)
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

            int idCorrelativo = new CN_Compra().ObtenerCorrelativo();
            string numeroDocumento = string.Format("{0:00000}", idCorrelativo);

            Compra oCompra = new Compra()
            {
                oUsuario = new Usuario() {idUsuario = _Usuario.idUsuario },
                idNegocio = GlobalSettings.SucursalId,
                oProveedor = new Proveedor() { idProveedor = Convert.ToInt32(txtIdProveedor.Text)},
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

            };

            string mensaje = string.Empty;
            bool respuesta = new CN_Compra().Registrar(oCompra,detalle_compra,out mensaje);
            if (respuesta)
            {
                string nombreProveedor = txtRazonSocial.Text;
                var result = MessageBox.Show("Numero de Compra Generado:\n" + numeroDocumento + "\n\n¿Desea copiar al portapapeles?","Mensaje",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                

                txtIdProveedor.Text = "0";
                txtCUIT.Text = "";
                txtRazonSocial.Text = "";
                dgvData.Rows.Clear();
                calcularTotal();

                if (checkCaja.Checked)
                {
                    List<CajaRegistradora> lista = new CN_CajaRegistradora().Listar(GlobalSettings.SucursalId);

                    CajaRegistradora cajaAbierta = lista.Where(c => c.estado == true).FirstOrDefault();

                    if (cajaAbierta != null)

                    {
                        decimal montoCalculado = Convert.ToDecimal(txtTotalAPagar.Text) * -1;





                        TransaccionCaja objTransaccion = new TransaccionCaja()
                        {
                            idCajaRegistradora = cajaAbierta.idCajaRegistradora,

                            hora = dtpFecha.Value.Hour.ToString(),
                            tipoTransaccion = "SALIDA",
                            monto = montoCalculado,
                            docAsociado = "Compra Numero:" + " " + numeroDocumento + " Proveedor:" + " " + nombreProveedor,
                            usuarioTransaccion = Environment.GetEnvironmentVariable("usuario")
                        };




                        int idTransaccionGenerado = new CN_Transaccion().RegistrarMovimiento(objTransaccion, out mensaje);

                    }
                }
            }


            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}



