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
    public partial class frmDetalleVenta : Form
    {
        private Venta oVenta;
        public frmDetalleVenta(Venta venta)
        {
            InitializeComponent();
            oVenta = venta;
        }

        private void frmDetalleVenta_Load(object sender, EventArgs e)
        {
            
            lblIdVenta.Text = "0";
            if (oVenta != null)
            {
                lblIdVenta.Text = oVenta.idVenta.ToString();
                lblNumeroVenta.Text = oVenta.nroDocumento;
                txtnroDocumento.Text = oVenta.nroDocumento;
                dtpFecha.Text = oVenta.fechaRegistro.ToString();
                cboTipoDocumento.Text = oVenta.tipoDocumento;
                txtUsuario.Text = oVenta.oUsuario.nombreCompleto;
                txtDNI.Text = oVenta.documentoCliente;
                txtNombreCliente.Text = oVenta.nombreCliente;
                txtDescuento.Text = oVenta.descuento.ToString();
                txtMontoDescuento.Text = oVenta.montoDescuento.ToString();
                txtFormaPago1.Text = oVenta.formaPago.ToString();
                txtFormaPago2.Text = oVenta.formaPago2.ToString();
                txtFormaPago3.Text = oVenta.formaPago3.ToString();
                txtFormaPago4.Text = oVenta.formaPago4.ToString();
                txtMontoFP1.Text = oVenta.montoFP1.ToString();
                txtMontoFP2.Text = oVenta.montoFP2.ToString();
                txtMontoFP3.Text = oVenta.montoFP3.ToString();
                txtMontoFP4.Text = oVenta.montoFP4.ToString();

                dgvData.Rows.Clear();
                foreach (DetalleVenta dv in oVenta.oDetalleVenta)
                {
                    dgvData.Rows.Add(new object[] { dv.oProducto.nombre, dv.precioVenta, dv.cantidad, dv.subTotal });
                }

                txtTotalAPagar.Text = oVenta.montoTotal.ToString("0.00");
                txtPagaCon.Text = oVenta.montoPago.ToString("0.00");
                txtCambio.Text = oVenta.montoCambio.ToString("0.00");
            }
        }

        private void btnBuscarVenta_Click(object sender, EventArgs e)
        {
        }

        private void Limpiar()
        {
            dtpFecha.Value = DateTime.Now;
            cboTipoDocumento.SelectedItem = 0;
            txtUsuario.Text = "";
            txtDNI.Text = "";
            txtNombreCliente.Text = "";
            dgvData.Rows.Clear();
            txtTotalAPagar.Text = "0.00";
            txtPagaCon.Text = "0.00";
            txtCambio.Text = "0.00";
            lblIdVenta.Text = "0";
            txtMontoDescuento.Text = string.Empty;
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
            if (txtDNI.Text == "")
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string textoHtml = Properties.Resources.PlantillaVenta.ToString();
            int idNegocio = GlobalSettings.SucursalId;
            Negocio odatos = new CN_Negocio().ObtenerDatos(idNegocio);

            textoHtml = textoHtml.Replace("@nombrenegocio", odatos.nombre.ToUpper());
            textoHtml = textoHtml.Replace("@docnegocio", odatos.CUIT.ToUpper());
            textoHtml = textoHtml.Replace("@direcnegocio", odatos.direccion.ToUpper());

            textoHtml = textoHtml.Replace("@tipodocumento", cboTipoDocumento.Text.ToString().ToUpper());
            textoHtml = textoHtml.Replace("@numerodocumento", txtnroDocumento.Text.ToUpper());

            textoHtml = textoHtml.Replace("@doccliente", txtDNI.Text);
            textoHtml = textoHtml.Replace("@nombrecliente", txtNombreCliente.Text);
            textoHtml = textoHtml.Replace("@fecharegistro", dtpFecha.Text);
            textoHtml = textoHtml.Replace("@usuarioregistro", txtUsuario.Text);


            string filas = string.Empty;

            foreach (DataGridViewRow row in dgvData.Rows)
            {

                filas += "<tr>";
                filas += "<td>" + row.Cells["producto"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["precioVenta"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["subTotal"].Value.ToString() + "</td>";
                filas += "</tr>";

            }

            textoHtml = textoHtml.Replace("@filas", filas);
            textoHtml = textoHtml.Replace("@descuento", txtDescuento.Text);
            textoHtml = textoHtml.Replace("@montodescuento", txtMontoDescuento.Text);
            textoHtml = textoHtml.Replace("@montototal", txtTotalAPagar.Text);
            textoHtml = textoHtml.Replace("@pagocon", txtPagaCon.Text);
            textoHtml = textoHtml.Replace("@cambio", txtCambio.Text);
            textoHtml = textoHtml.Replace("@formaPago", txtFormaPago1.Text);
            textoHtml = textoHtml.Replace("@montoFP1", txtMontoFP1.Text);
            textoHtml = textoHtml.Replace("@formaPago2", txtFormaPago2.Text);
            textoHtml = textoHtml.Replace("@montoFP2", txtMontoFP2.Text);
            textoHtml = textoHtml.Replace("@formaPago3", txtFormaPago3.Text);
            textoHtml = textoHtml.Replace("@montoFP3", txtMontoFP3.Text);
            textoHtml = textoHtml.Replace("@formaPago4", txtFormaPago4.Text);
            textoHtml = textoHtml.Replace("@montoFP4", txtMontoFP4.Text);


            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = string.Format("Orden de Venta nro {0}.pdf", txtnroDocumento.Text);
            saveFile.Filter = "Pdf Files | *.pdf";


            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFile.FileName, FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A4, 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();

                    bool obtenido = true;
                    byte[] byteImage = new CN_Negocio().ObtenerLogo(out obtenido);
                    if (obtenido)
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteImage);
                        img.ScaleToFit(110, 110);
                        img.Alignment = iTextSharp.text.Image.UNDERLYING;
                        img.SetAbsolutePosition(pdfdoc.Left, pdfdoc.GetTop(70));
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
            if (GlobalSettings.RolUsuario == 1)
            {
                // Mostrar un cuadro de diálogo de confirmación
                DialogResult result = MessageBox.Show("¿Está seguro de que desea eliminar esta venta?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Verificar si el usuario hizo clic en "Sí"
                if (result == DialogResult.Yes)
                {
                    bool resultado = new CN_Venta().EliminarVentaConDetalle(Convert.ToInt32(lblIdVenta.Text), out mensaje);
                    if (resultado)
                    {
                        MessageBox.Show("Venta Eliminada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("No se ha podido Eliminar la Venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No posee permisos para Eliminar una Venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
    }

