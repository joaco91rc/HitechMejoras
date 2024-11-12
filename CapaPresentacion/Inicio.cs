using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad;
using FontAwesome.Sharp;
using CapaNegocio;
using CapaPresentacion.Modales;
using CapaNegocio.Services;
using CapaPresentacion.Utilidades;
using ClosedXML.Excel;

namespace CapaPresentacion
{
    public partial class Inicio : Form
    {
        private int sucursalId;
        private SucursalService sucursalService;
        private static Usuario usuarioActual;
        private static IconMenuItem menuActivo = null;
        private static Form formularioActivo = null;
        public Inicio(Usuario objUsuario)
        {
            Environment.SetEnvironmentVariable("usuario", objUsuario.nombreCompleto);
            Environment.SetEnvironmentVariable("documentoUsuario", objUsuario.documento);
            Environment.SetEnvironmentVariable("rol", objUsuario.oRol.descripcion);
            usuarioActual = objUsuario;
            InitializeComponent();
        }

       

        private void iconPictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            
        }

       

        private void Inicio_Load(object sender, EventArgs e)
        {
            //sucursalService = new SucursalService();


            GlobalSettings.RolUsuario = usuarioActual.oRol.idRol;

           
            List<Permiso> listaPermisos = new CN_Permiso().Listar(usuarioActual.idUsuario);

            foreach(IconMenuItem iconMenu in menu.Items)
            {
                bool encontrado = listaPermisos.Any(m => m.nombreMenu == iconMenu.Name);
                if(encontrado==false)
                {
                    iconMenu.Visible = false;
                }
            }

            lblUsuario.Text = Environment.GetEnvironmentVariable("usuario");
            lblDocumento.Text = Environment.GetEnvironmentVariable("documentoUsuario");
            lblRol.Text = Environment.GetEnvironmentVariable("rol");
            lblSucursal.Text = GlobalSettings.NombreSucursal;
            CargarComboBoxSucursal();
            if(GlobalSettings.RolUsuario == 1)
            {
                cboSucursal.Visible = true;
                lblCambioSucursal.Visible = true;
            }
        }

        //private void AbrirFormulario(IconMenuItem menu, Form formulario)
        //{
        //    if (menuActivo != null)
        //    {
        //        menuActivo.BackColor = Color.ForestGreen;
        //        menuActivo.ForeColor = Color.FromArgb(224, 224, 224);
        //    }
        //    menu.BackColor = Color.FromArgb(178, 214, 243);
        //    menu.ForeColor = Color.ForestGreen;
        //    menuActivo = menu;

        //    if (formularioActivo != null)
        //    {
        //        formularioActivo.Close();
        //    }
        //    formularioActivo = formulario;
        //    formulario.TopLevel = false;
        //    formulario.FormBorderStyle = FormBorderStyle.None;
        //    formulario.Dock = DockStyle.Fill;
        //    formulario.BackColor = Color.FromArgb(63, 61, 64);
        //    contenedor.Controls.Add(formulario);
        //    formulario.Show();

        //}
        private void AbrirFormulario(IconMenuItem menu, Form formulario, Inicio formularioInicio)
        {
            if (menuActivo != null)
            {
                menuActivo.BackColor = Color.ForestGreen;
                menuActivo.ForeColor = Color.FromArgb(224, 224, 224);
            }
            menu.BackColor = Color.FromArgb(178, 214, 243);
            menu.ForeColor = Color.ForestGreen;
            menuActivo = menu;

            if (formularioActivo != null)
            {
                formularioActivo.Close();
            }

            // Aquí, pasa el formularioInicio al formulario que estás abriendo
            if (formulario is frmCajaRegistradora)
            {
                (formulario as frmCajaRegistradora).SetInicio(formularioInicio);
            }

            formularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.FromArgb(63, 61, 64);
            contenedor.Controls.Add(formulario);
            formulario.Show();
        }



        private void subMenuCategoria_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmCategoria(),this);
        }

        private void subMenuProducto_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmProducto(usuarioActual),this);
        }

        private void subMenuRegistrarVenta_Click(object sender, EventArgs e)
        {
            List<CajaRegistradora> lista = new CN_CajaRegistradora().Listar(GlobalSettings.SucursalId);

            CajaRegistradora cajaAbierta = lista.Where(c => c.estado == true).FirstOrDefault();

            if (cajaAbierta == null)
            {
                MessageBox.Show("No hay cajas Abiertas. Por favor abra una caja para poder Registrar una Venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                AbrirFormulario(menuVentas, new frmVentas(usuarioActual),this);
            }


        }

        private void subMenuDetalleVenta_Click(object sender, EventArgs e)
        {
            //AbrirFormulario(menuVentas, new frmDetalleVenta());
            AbrirFormulario(menuVentas, new frmListadoVentas(usuarioActual),this);
        }

        private void subMenuRegistrarCompra_Click(object sender, EventArgs e)
        {
            List<CajaRegistradora> lista = new CN_CajaRegistradora().Listar(GlobalSettings.SucursalId);

            CajaRegistradora cajaAbierta = lista.Where(c => c.estado == true).FirstOrDefault();

            if (cajaAbierta == null)
            {
                MessageBox.Show("No hay cajas Abiertas. Por favor abra una caja para poder Registrar una Compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                AbrirFormulario(menuCompras, new frmCompras(usuarioActual),this);
            }



        }

        private void subMenuDetalleCompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuCompras, new frmListadoCompras(),this);
        }

        private void menuClientes_Click(object sender, EventArgs e)
        {
            
        }

        private void menuProveedores_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmProveedores(),this);
        }

        

        private void menuUsuarios_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmUsuarios(),this);
        }

        

        private void subMenuNegocio_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmNegocio(),this);
        }

        private void subMenuReporteCompras_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuReportes, new frmReporteCompras(),this);
        }

        private void subMenuReporteVentas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuReportes, new frmReporteVentas(),this);
        }

      

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro que desea cerrar Sesión?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void subMenuCajaDiaria_Click(object sender, EventArgs e)
        {
            List<CajaRegistradora> lista = new CN_CajaRegistradora().Listar(GlobalSettings.SucursalId);

            CajaRegistradora cajaAbierta = lista.Where(c => c.estado == true).FirstOrDefault();

            if (cajaAbierta == null)
            {
                MessageBox.Show("No hay cajas Abiertas. Por favor abra una caja para poder ver la Caja Diaria", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                AbrirFormulario(menuCajaRegistradora, new frmCajaRegistradora(),this);
            }

           
        }

        private void subMenuAperturaCaja_Click(object sender, EventArgs e)
        {

            List<CajaRegistradora> lista = new CN_CajaRegistradora().Listar(GlobalSettings.SucursalId);

            CajaRegistradora cajaAbierta = lista.Where(c => c.estado == true).FirstOrDefault();

            if (cajaAbierta != null)
            {
                MessageBox.Show("Ya hay una Caja Abierta en curso. Por favor cierre la Caja en curso antes de Abrir otra caja", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                mdAperturacaja mdAperturacaja = new mdAperturacaja();
                mdAperturacaja.ShowDialog();
            }


           
        }

        private void subMenuConsultaCaja_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuCajaRegistradora, new frmDetalleCaja(),this);
        }

        private void cotizacionDolarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmCotizacion(),this);
        }

        private void menuRMA_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmRMA(),this);
        }

        private void menuConsultaStock_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmConsultaStock(),this);
        }

        private void subMenuFormaPago_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmFormaPago(),this);
        }

        private void subMenuStock_Click(object sender, EventArgs e)
        {
            mdCargaStock mdCargaStock = new mdCargaStock();
            mdCargaStock.ShowDialog();
        }

        private void subMenuTraspasoMercaderia_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmOrdenesDeTraspaso(),this);
        }

        private void iconPictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconPictureBox2_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void subMenuConceptos_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuCajaRegistradora, new frmConceptos(),this);
        }

        private void subMenuIngresoService_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuServicioTecnico, new frmServicioTecnico(),this);
        }

        private void subMenuCobroService_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuServicioTecnico, new frmCobrarServicio(),this);
        }

        

        private void subMenuVendedores_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmVendedor(),this);
        }

        private void CargarComboBoxSucursal()
        {
            // Crear una instancia de la capa de negocio
            CN_Negocio objCN_Negocio = new CN_Negocio();

            // Obtener la lista de formas de pago desde la base de datos
            List<Negocio> listaNegocio = objCN_Negocio.ListarNegocios();

            // Limpiar los items actuales del ComboBox
            cboSucursal.Items.Clear();


            // Llenar el ComboBox con los datos obtenidos
            foreach (Negocio negocio in listaNegocio)
            {
                cboSucursal.Items.Add(new OpcionCombo() { Valor = negocio.idNegocio, Texto = negocio.nombre });
            }

            // Establecer DisplayMember y ValueMember
            cboSucursal.DisplayMember = "Texto";
            cboSucursal.ValueMember = "Valor";


            // Seleccionar el primer item por defecto si hay elementos en el ComboBox
            if (cboSucursal.Items.Count > 0)
            {
                cboSucursal.SelectedIndex = -1;

            }

            cboSucursal.SelectedIndexChanged += cboSucursal_SelectedIndexChanged;
        }

        public void ActualizarSucursal(string nombreSucursal)
        {
            lblSucursal.Text = nombreSucursal;
        }
        private void cboSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSucursal.SelectedIndex != -1)
            {
                // Cerrar el formulario activo si existe
                if (formularioActivo != null)
                {
                    formularioActivo.Close();
                    formularioActivo = null; // Restablecer la referencia del formulario activo
                }

                // Establecer la sucursal seleccionada en los ajustes globales
                GlobalSettings.SucursalId = Convert.ToInt32(((OpcionCombo)cboSucursal.SelectedItem).Valor);
                GlobalSettings.NombreSucursal = ((OpcionCombo)cboSucursal.SelectedItem).Texto;
                lblSucursal.Text = GlobalSettings.NombreSucursal;
            }
        }

        private void subMenuStock2_Click(object sender, EventArgs e)
        {
            mdCargaStock mdCargaStock = new mdCargaStock();
            mdCargaStock.ShowDialog();
        }

        private void subMenuTraspasoStock_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmOrdenesDeTraspaso(), this);
        }

        private void subMenuDeudas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmDeuda(), this);
        }

        private void subMenuProductosSerializados_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario(menuSerializacion, new frmProductosSerializados(), this);
        }

        private void subMenuSerializarProducto_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario(menuSerializacion, new frmDetalleProducto(), this);
        }

        private void subMenuSerialVendidos_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuSerializacion, new frmSerialesVendidos(), this);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuSerializacion, new frmProductosSerializados(), this);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuSerializacion, new frmSerialesVendidos(), this);
        }

        private void subMenuClientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuClientes, new frmClientes(), this);
        }

        private void subMenuPagosParciales_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuClientes, new frmPagoParcial(), this);
        }


        private void ExportarAExcel(List<ReporteCantidadVentas> lista)
        {
            if (lista == null || lista.Count < 1)
            {
                MessageBox.Show("No hay registros para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            using (var workbook = new XLWorkbook())
            {
                // Crear una nueva hoja de trabajo
                var worksheet = workbook.Worksheets.Add("Reporte de Ventas por Local");

                // Establecer encabezados de columna
                worksheet.Cell(1, 1).Value = "Local";
                worksheet.Cell(1, 2).Value = "Producto Vendido";
                worksheet.Cell(1, 3).Value = "Cantidad";

                // Formato para el encabezado (solo A a C)
                worksheet.Range("A1:C1").Style.Fill.BackgroundColor = XLColor.FromArgb(81, 129, 191); // Color de fondo del encabezado
                worksheet.Range("A1:C1").Style.Font.FontColor = XLColor.White; // Color del texto en el encabezado
                worksheet.Range("A1:C1").Style.Font.Bold = true; // Negrita
                worksheet.Range("A1:C1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; // Alinear al centro

                // Añadir bordes al encabezado
                worksheet.Range("A1:C1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:C1").Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                // Llenar datos y aplicar formato alternante
                for (int row = 2; row <= lista.Count + 1; row++)
                {
                    var item = lista[row - 2];
                    worksheet.Cell(row, 1).Value = item.nombreLocal;
                    worksheet.Cell(row, 2).Value = item.nombreProducto;
                    worksheet.Cell(row, 3).Value = item.cantidadVendida;

                    // Aplicar formato alternante solo en A, B, C
                    if (row % 2 == 0)
                    {
                        worksheet.Cell(row, 1).Style.Fill.BackgroundColor = XLColor.FromArgb(221, 230, 241); // Color para fila par en columna A
                        worksheet.Cell(row, 2).Style.Fill.BackgroundColor = XLColor.FromArgb(221, 230, 241); // Color para fila par en columna B
                        worksheet.Cell(row, 3).Style.Fill.BackgroundColor = XLColor.FromArgb(221, 230, 241); // Color para fila par en columna C
                    }
                    else
                    {
                        worksheet.Cell(row, 1).Style.Fill.BackgroundColor = XLColor.FromArgb(253, 254, 255); // Color para fila impar en columna A
                        worksheet.Cell(row, 2).Style.Fill.BackgroundColor = XLColor.FromArgb(253, 254, 255); // Color para fila impar en columna B
                        worksheet.Cell(row, 3).Style.Fill.BackgroundColor = XLColor.FromArgb(253, 254, 255); // Color para fila impar en columna C
                    }

                    // Asegurar que no se aplique formato a columnas D en adelante
                    for (int col = 4; col <= worksheet.LastColumnUsed().ColumnNumber(); col++)
                    {
                        worksheet.Cell(row, col).Style.Fill.BackgroundColor = XLColor.Transparent; // Quitar formato
                    }
                }

                // Ajustar alineación de las columnas
                worksheet.Column(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; // Alinear Local al centro
                worksheet.Column(2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left; // Alinear Producto Vendido a la izquierda
                worksheet.Column(3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right; // Alinear Cantidad a la derecha

                // Aplicar bordes a todo el rango de datos (A1:C última fila)
                worksheet.Range("A1:C" + (lista.Count + 1)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:C" + (lista.Count + 1)).Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                // Ajustar el tamaño de las columnas
                worksheet.Columns("A:C").AdjustToContents(); // Ajustar solo las columnas A a C

                // Aplicar filtros en los encabezados
                worksheet.Range("A1:C" + (lista.Count + 1)).SetAutoFilter(); // Crear autofiltro en el rango de datos

                // Guardar el archivo
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";

                    // Formatear el nombre del archivo con fecha y hora
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    sfd.FileName = $"Reporte_Cantidad_Productos_Vendidos_por_Local_{timestamp}.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        workbook.SaveAs(sfd.FileName);
                        MessageBox.Show("El reporte se ha exportado exitosamente.", "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }



        private void subMenuReporteCantidadVentasPorLocal_Click(object sender, EventArgs e)
        {
            using (mdSeleccionFechas formFechas = new mdSeleccionFechas())
            {
                if (formFechas.ShowDialog() == DialogResult.OK)
                {
                    // Obtener las fechas seleccionadas
                    DateTime fechaDesde = formFechas.FechaDesde;
                    DateTime fechaHasta = formFechas.FechaHasta;

                    // Llamar al método de reporte con las fechas seleccionadas
                    List<ReporteCantidadVentas> lista = new CN_Reporte().CantidadVendidaPorLocal(fechaDesde, fechaHasta);

                    // Exportar el resultado a Excel
                    ExportarAExcel(lista);
                }
            }
            
        }
    }
}
