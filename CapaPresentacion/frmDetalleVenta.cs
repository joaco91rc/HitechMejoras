using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
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
        private void frmDetalleVenta_Load(object sender, EventArgs e)
        {
            CargarComboBoxVendedores();
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
                txtCotizacionDolar.Text = oVenta.cotizacionDolar.ToString();
                txtObservaciones.Text = oVenta.observaciones;
                dgvData.Rows.Clear();
                foreach (DetalleVenta dv in oVenta.oDetalleVenta)
                {
                    dgvData.Rows.Add(new object[] {dv.oProducto.idProducto, dv.oProducto.nombre, dv.precioVenta, dv.cantidad, dv.subTotal, dv.oProducto.prodSerializable });
                }

                txtTotalAPagar.Text = oVenta.montoTotal.ToString("0.00");
                txtPagaCon.Text = oVenta.montoPago.ToString("0.00");
                txtCambio.Text = oVenta.montoCambio.ToString("0.00");

                // Establecer el item seleccionado en el ComboBox cboVendedores
                if (cboVendedores.Items.Count > 0)
                {
                    foreach (var item in cboVendedores.Items)
                    {
                        var opcionCombo = item as OpcionCombo; // Cast al tipo OpcionCombo
                        if (opcionCombo != null && Convert.ToInt32(opcionCombo.Valor) == oVenta.idVendedor) // Comparar con idVendedor
                        {
                            cboVendedores.SelectedItem = opcionCombo; // Selecciona el item correspondiente
                            //cboVendedores.Text = opcionCombo.Texto;
                            break;
                        }
                    }
                }

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
            Cliente oCliente = new CN_Cliente().ObtenerClientePorDocumentoYNombre(txtDNI.Text, txtNombreCliente.Text);
            // Reemplazar valores del negocio
            textoHtml = textoHtml.Replace("@nombrenegocio", odatos.nombre?.ToUpper() ?? "");
            textoHtml = textoHtml.Replace("@docnegocio", odatos.CUIT?.ToUpper() ?? "");
            textoHtml = textoHtml.Replace("@direcnegocio", odatos.direccion?.ToUpper() ?? "");

            // Reemplazar valores del documento y cliente
            textoHtml = textoHtml.Replace("@numerodocumento", txtnroDocumento.Text.ToUpper());
            textoHtml = textoHtml.Replace("@doccliente", txtDNI.Text);
            textoHtml = textoHtml.Replace("@nombrecliente", txtNombreCliente.Text);
            textoHtml = textoHtml.Replace("@direccion", oCliente.direccion);
            textoHtml = textoHtml.Replace("@ciudad", oCliente.ciudad);
            textoHtml = textoHtml.Replace("@telefono", oCliente.telefono);
            textoHtml = textoHtml.Replace("@mail", oCliente.correo);

            // Crear las filas de la tabla
            string filas = string.Empty;

            foreach (DataGridViewRow row in dgvData.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + (row.Cells["producto"].Value?.ToString() ?? "") + "</td>";
                filas += "<td>" + (row.Cells["precioVenta"].Value?.ToString() ?? "") + "</td>";
                filas += "<td>" + (row.Cells["cantidad"].Value?.ToString() ?? "") + "</td>";
                filas += "<td>" + (row.Cells["subTotal"].Value?.ToString() ?? "") + "</td>";
                filas += "</tr>";
            }

            textoHtml = textoHtml.Replace("@filas", filas);

            // Reemplazar los demás valores con verificación de nulos o vacíos
            textoHtml = textoHtml.Replace("@descuento", string.IsNullOrEmpty(txtDescuento.Text) ? "" : txtDescuento.Text);
            textoHtml = textoHtml.Replace("@montodescuento", string.IsNullOrEmpty(txtMontoDescuento.Text) ? "" : txtMontoDescuento.Text);
            textoHtml = textoHtml.Replace("@montototal", string.IsNullOrEmpty(txtTotalAPagar.Text) ? "" : txtTotalAPagar.Text);
            textoHtml = textoHtml.Replace("@cambio", string.IsNullOrEmpty(txtCambio.Text) ? "" : txtCambio.Text);
            textoHtml = textoHtml.Replace("@cotizacionDolar", string.IsNullOrEmpty(txtCotizacionDolar.Text) ? "" : txtCotizacionDolar.Text);

            string formaPago1 = string.IsNullOrEmpty(txtFormaPago1.Text) ? "" : txtFormaPago1.Text;
            string montoFP1 = string.IsNullOrEmpty(txtMontoFP1.Text) || txtMontoFP1.Text == "0.00" ? "" : txtMontoFP1.Text;

            string formaPago2 = string.IsNullOrEmpty(txtFormaPago2.Text) ? "" : txtFormaPago2.Text;
            string montoFP2 = string.IsNullOrEmpty(txtMontoFP2.Text) || txtMontoFP2.Text == "0.00" ? "" : txtMontoFP2.Text;

            string formaPago3 = string.IsNullOrEmpty(txtFormaPago3.Text) ? "" : txtFormaPago3.Text;
            string montoFP3 = string.IsNullOrEmpty(txtMontoFP3.Text) || txtMontoFP3.Text == "0.00" ? "" : txtMontoFP3.Text;

            string formaPago4 = string.IsNullOrEmpty(txtFormaPago4.Text) ? "" : txtFormaPago4.Text;
            string montoFP4 = string.IsNullOrEmpty(txtMontoFP4.Text) || txtMontoFP4.Text == "0.00" ? "" : txtMontoFP4.Text;


            // Reemplazar en el texto HTML
            textoHtml = textoHtml.Replace("@formaPago1", formaPago1);
            textoHtml = textoHtml.Replace("@montoFP1", montoFP1);
            textoHtml = textoHtml.Replace("@formaPago2", formaPago2);
            textoHtml = textoHtml.Replace("@montoFP2", montoFP2);
            textoHtml = textoHtml.Replace("@formaPago3", formaPago3);
            textoHtml = textoHtml.Replace("@montoFP3", montoFP3);
            textoHtml = textoHtml.Replace("@formaPago4", formaPago4);
            textoHtml = textoHtml.Replace("@montoFP4", montoFP4);

            // Reemplazar valores de contacto
            textoHtml = textoHtml.Replace("@telefono", odatos.telefono ?? "");
            textoHtml = textoHtml.Replace("@instagram", odatos.instagram ?? "");
            textoHtml = textoHtml.Replace("@mail", odatos.mail ?? "");



            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = string.Format("Orden de Venta nro {0}.pdf", txtnroDocumento.Text);
            saveFile.Filter = "Pdf Files | *.pdf";


            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFile.FileName, FileMode.Create))
                {
                    // Crear documento PDF con márgenes
                    Document pdfdoc = new Document(PageSize.A4, 25, 25, 25, 100);
                    PdfWriter writer = PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();

                    // Obtener logo
                    bool obtenido = true;
                    byte[] byteImage = new CN_Negocio().ObtenerLogo(out obtenido);
                    float positionAfterLogo = pdfdoc.GetTop(70); // Posición inicial para calcular la línea después del logo
                    if (obtenido)
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteImage);
                        img.ScaleToFit(110, 110);
                        img.SetAbsolutePosition(pdfdoc.Left, pdfdoc.GetTop(70)); // Sin la alineación
                        pdfdoc.Add(img);
                    }


                    // Dibujar una línea con el color hexadecimal #028635
                    PdfContentByte cb = writer.DirectContent;
                    BaseColor greenColor = new BaseColor(2, 134, 53); // Color hexadecimal #028635
                    cb.SetColorStroke(greenColor); // Establecer el color
                    cb.SetLineWidth(2f); // Ancho de la línea
                    cb.MoveTo(25, positionAfterLogo); // Posición inicial de la línea (x, y)
                    cb.LineTo(pdfdoc.PageSize.Width - 25, positionAfterLogo); // Posición final de la línea (x, y)
                    cb.Stroke(); // Dibujar la línea

                    // Añadir el contenido HTML
                    using (StringReader sr = new StringReader(textoHtml))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfdoc, sr);
                    }

                    // Cerrar el documento y el stream
                    pdfdoc.Close();
                    stream.Close();

                    MessageBox.Show("Documento Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)

        {
            string elimnacionVenta = string.Empty;
            string eliminacionMovimientos = string.Empty;
            string actualizacionStock = string.Empty;
            string mensaje = string.Empty;
            string mensajeActivarSerial = string.Empty;
            if (GlobalSettings.RolUsuario == 1 || GlobalSettings.RolUsuario == 2)
            {
                // Mostrar un cuadro de diálogo de confirmación
                DialogResult result = MessageBox.Show("¿Está seguro de que desea eliminar esta venta?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Verificar si el usuario hizo clic en "Sí"
                if (result == DialogResult.Yes)
                {
                    bool resultado = new CN_Venta().EliminarVentaConDetalle(Convert.ToInt32(lblIdVenta.Text), out mensaje);
                    if (resultado)
                    {
                        foreach (DataGridViewRow row in dgvData.Rows)
                        {
                            // Asegúrate de que la fila no sea una fila nueva (la fila de edición en blanco al final)
                            if (!row.IsNewRow)
                            {
                                // Obtener el valor de la columna "IdProducto"
                                int idProducto = Convert.ToInt32(row.Cells["idProducto"].Value);
                                int cantidad = Convert.ToInt32(row.Cells["cantidad"].Value);

                                actualizacionStock = new CN_ProductoNegocio().CargarOActualizarStockProducto(idProducto, GlobalSettings.SucursalId, cantidad);
                                var listaSeriales = new CN_Producto().ListarProductosSerialesPorVenta(Convert.ToInt32(lblIdVenta.Text));
                                if(listaSeriales.Count > 0)
                                {
                                    foreach(var item in listaSeriales)
                                    {
                                        bool activarSerial = new CN_Producto().ActivarProductoDetalle(item.idProductoDetalle, out mensajeActivarSerial);
                                        if (activarSerial)
                                        {
                                            mensajeActivarSerial = "Productos con Numero de Serie Devueltos al Stock";
                                        }
                                    }
                                }
                            }
                        }
                        elimnacionVenta = "Se ha Eliminado la Venta. ";
                        

                    } else
                    {
                        elimnacionVenta = "No se pudo eliminar la Venta";
                    }
                    bool eliminar = new CN_Transaccion().EliminarMovimientoCajaYVenta(Convert.ToInt32(lblIdVenta.Text), out mensaje);
                    if (eliminar)
                    {
                                         
                            eliminacionMovimientos = " Moviemientos en Caja Eliminados";
                        

                        
                    }
                    else
                    {
                        eliminacionMovimientos = mensaje;
                    }
                    
                   MessageBox.Show(elimnacionVenta + actualizacionStock + eliminacionMovimientos + mensajeActivarSerial, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("No posee permisos para Eliminar una Venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
    }

