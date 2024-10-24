using CapaEntidad;
using CapaNegocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmDetalleCompra : Form
    {
        private Compra oCompra;
        public frmDetalleCompra(Compra compra)
        {
            InitializeComponent();
            oCompra = compra;
        }

        //private void btnBuscarProducto_Click(object sender, EventArgs e)
        //{
        //    Compra oCompra = new CN_Compra().ObtenerCompra(txtBuscarCompra.Text,GlobalSettings.SucursalId);

        //    if(oCompra.idCompra != 0)
        //    {
        //        lblIdCompra.Text = oCompra.idCompra.ToString();
        //        txtnroDocumento.Text = oCompra.nroDocumento;
        //        dtpFecha.Text = oCompra.fechaRegistro;
        //        cboTipoDocumento.Text = oCompra.tipoDocumento;
        //        txtUsuario.Text = oCompra.oUsuario.nombreCompleto;
        //        txtCUIT.Text = oCompra.oProveedor.documento;
        //        txtRazonSocial.Text = oCompra.oProveedor.razonSocial;

        //        dgvData.Rows.Clear();

        //        foreach ( DetalleCompra dc in oCompra.oDetalleCompra)
        //        {

        //            dgvData.Rows.Add(new object[] { dc.oProducto.nombre, dc.precioCompra, dc.cantidad, dc.montoTotal });
        //        }
        //        txtTotalAPagar.Text = oCompra.montoTotal.ToString("0.00");
        //    }
        //}
        private void Limpiar()
        {
            dtpFecha.Value = DateTime.Now;
            cboTipoDocumento.SelectedItem = 0;
            txtUsuario.Text = "";
            txtCUIT.Text = "";
            txtRazonSocial.Text = "";
            dgvData.Rows.Clear();
            txtTotalAPagar.Text = "0.00";
            txtnroDocumento.Text = "";
            lblIdCompra.Text = "0";
            txtFormaPago1.Text = string.Empty;
            txtFormaPago2.Text = string.Empty;
            txtFormaPago3.Text = string.Empty;
            txtFormaPago4.Text = string.Empty;
            txtMontoFP1.Text = string.Empty;
            txtMontoFP2.Text = string.Empty;
            txtMontoFP3.Text = string.Empty;
            txtMontoFP4.Text = string.Empty;
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnDescargarPDF_Click(object sender, EventArgs e)
        {
            if (txtCUIT.Text == "")
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string textoHtml = Properties.Resources.PlantillaCompra.ToString();
            int idNegocio = GlobalSettings.SucursalId;
            Negocio odatos = new CN_Negocio().ObtenerDatos(idNegocio);

            textoHtml = textoHtml.Replace("@nombrenegocio", odatos.nombre.ToUpper());
            textoHtml = textoHtml.Replace("@docnegocio", odatos.CUIT.ToUpper());
            textoHtml = textoHtml.Replace("@direcnegocio", odatos.direccion.ToUpper());

            textoHtml = textoHtml.Replace("@tipodocumento", cboTipoDocumento.Text.ToString().ToUpper());
            textoHtml = textoHtml.Replace("@numerodocumento", txtnroDocumento.Text.ToUpper());

            textoHtml = textoHtml.Replace("@docproveedor", txtCUIT.Text);
            textoHtml = textoHtml.Replace("@nombreproveedor", txtRazonSocial.Text);
            textoHtml = textoHtml.Replace("@fecharegistro", dtpFecha.Text);
            textoHtml = textoHtml.Replace("@usuarioregistro", txtUsuario.Text);


            string filas = string.Empty;

            foreach (DataGridViewRow row in dgvData.Rows)
            {

                filas += "<tr>";
                filas += "<td>" + row.Cells["producto"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["precioCompra"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["subTotal"].Value.ToString() + "</td>";
                filas += "</tr>";

            }

            textoHtml = textoHtml.Replace("@filas", filas);
            textoHtml = textoHtml.Replace("@montototal", txtTotalAPagar.Text);


            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = string.Format("Orden de Compra nro {0}.pdf", txtnroDocumento.Text);
            saveFile.Filter = "Pdf Files | *.pdf";
        

            if(saveFile.ShowDialog()== DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFile.FileName, FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A4, 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();

                    bool obtenido = true;
                    byte[] byteImage = new CN_Negocio().ObtenerLogo( out obtenido);
                    if (obtenido)
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteImage);
                        img.ScaleToFit(110,110);
                        img.Alignment = iTextSharp.text.Image.UNDERLYING;
                        img.SetAbsolutePosition(pdfdoc.Left,pdfdoc.GetTop(70));
                        pdfdoc.Add(img);
                    }

                    using (StringReader sr = new StringReader(textoHtml))
                    {

                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfdoc, sr);

                    }

                    pdfdoc.Close();
                    stream.Close();

                    MessageBox.Show("Documento Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            string eliminacionCompra = string.Empty;
            string eliminacionMovimientos = string.Empty;
            string actualizacionStock = string.Empty;
            if (GlobalSettings.RolUsuario == 1)
            {
                DialogResult result = MessageBox.Show("¿Está seguro de que desea eliminar esta Compra?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    bool resultado = new CN_Compra().EliminarCompraConDetalle(Convert.ToInt32(lblIdCompra.Text), out mensaje);
                    
                    if (resultado)
                    {
                        MessageBox.Show("Compra y  Movimientos en Caja Eliminados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        foreach (DataGridViewRow row in dgvData.Rows)
                        {
                            // Asegúrate de que la fila no sea una fila nueva (la fila de edición en blanco al final)
                            if (!row.IsNewRow)
                            {
                                // Obtener el valor de la columna "IdProducto"
                                int idProducto = Convert.ToInt32(row.Cells["idProducto"].Value);
                                int cantidad = Convert.ToInt32(row.Cells["cantidad"].Value);

                                actualizacionStock =new CN_ProductoNegocio().CargarOActualizarStockProducto(idProducto, GlobalSettings.SucursalId, -cantidad);
                            }
                        }
                        eliminacionCompra = "Se ha Eliminado la Compra. ";
                        Limpiar();
                    }
                    else
                    {
                        eliminacionCompra = "No se pudo eliminar la Compra";
                    }
                    bool eliminar = new CN_Transaccion().EliminarMovimientoCajaYCompra(Convert.ToInt32(lblIdCompra.Text), out mensaje);
                    if (eliminar)
                    {

                        eliminacionMovimientos = " Moviemientos en Caja Eliminados";



                    }
                    else
                    {
                        eliminacionMovimientos = " No se pudo Eliminar los Moviemientos en la Caja";
                    }

                    MessageBox.Show(eliminacionCompra + actualizacionStock +eliminacionMovimientos, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("No posee permisos para Eliminar una Compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void frmDetalleCompra_Load(object sender, EventArgs e)
        {
            lblIdCompra.Text = "0";
            lblIdCompra.Text = "0";
            if (oCompra != null)
            {
                lblIdCompra.Text = oCompra.idCompra.ToString();
                lblNumeroCompra.Text = oCompra.nroDocumento;
                txtnroDocumento.Text = oCompra.nroDocumento;
                dtpFecha.Text = oCompra.fechaRegistro.ToString();
                cboTipoDocumento.Text = oCompra.tipoDocumento;
                txtUsuario.Text = oCompra.oUsuario.nombreCompleto;
                txtCUIT.Text = oCompra.oProveedor.documento;
                txtRazonSocial.Text = oCompra.oProveedor.razonSocial;
                
                txtFormaPago1.Text = oCompra.formaPago.ToString();
                txtFormaPago2.Text = oCompra.formaPago2.ToString();
                txtFormaPago3.Text = oCompra.formaPago3.ToString();
                txtFormaPago4.Text = oCompra.formaPago4.ToString();
                txtMontoFP1.Text = oCompra.montoFP1.ToString();
                txtMontoFP2.Text = oCompra.montoFP2.ToString();
                txtMontoFP3.Text = oCompra.montoFP3.ToString();
                txtMontoFP4.Text = oCompra.montoFP4.ToString();

                dgvData.Rows.Clear();
                foreach (DetalleCompra dc in oCompra.oDetalleCompra)
                {
                    dgvData.Rows.Add(new object[] {dc.oProducto.idProducto, dc.oProducto.nombre, dc.precioCompra, dc.cantidad, dc.montoTotal });
                }

                txtTotalAPagar.Text = oCompra.montoTotal.ToString("0.00");
                txtPagaCon.Text = oCompra.montoPago.ToString("0.00");
                //txtCambio.Text = oCompra.montoCambio.ToString("0.00");
            }
        }
    }
}

