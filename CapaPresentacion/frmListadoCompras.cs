using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using ClosedXML.Excel;
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
    public partial class frmListadoCompras : Form
    {
        private Image defaultImageEdit = Properties.Resources.detail;
        private Image defaultImageView = Properties.Resources.VIEWICON;
        private Usuario _Usuario;
        public frmListadoCompras()
        {
            InitializeComponent();
        }

        private void CargarCompras()
        {



            dgvData.Rows.Clear();
            List<Compra> listaCompras = new CN_Compra().ObtenerComprasConDetalle(GlobalSettings.SucursalId);
            foreach (Compra item in listaCompras)
            {
                

                    dgvData.Rows.Add(new object[] {item.idCompra,
                    item.fechaRegistro,
                    item.tipoDocumento,
                    item.nroDocumento,
                    item.montoTotal,
                    item.oProveedor.razonSocial,
                    defaultImageView,
                    defaultImageEdit

                    });
                
            }


        }


        private void CargarComprasEntreFechas(DateTime fechaDesde, DateTime fechaHasta)
        {



            dgvData.Rows.Clear();
            List<Compra> listaCompras = new CN_Compra().ObtenerComprasConDetalleEntreFechas(GlobalSettings.SucursalId, fechaDesde, fechaHasta);
            foreach (Compra item in listaCompras)
            {


                dgvData.Rows.Add(new object[] {item.idCompra,
                    item.fechaRegistro,
                    item.tipoDocumento,
                    item.nroDocumento,
                    item.montoTotal,
                    item.oProveedor.razonSocial,
                    defaultImageView,
                    defaultImageEdit

                    });

            }


        }

        private void frmListadoCompras_Load(object sender, EventArgs e)
        {

            dtpFechaDesde.Value = DateTime.Now.Date.AddDays(-7);
            dtpFechaHasta.Value = DateTime.Now.Date.AddDays(+1);

            CargarComprasEntreFechas(dtpFechaDesde.Value.Date, dtpFechaHasta.Value.Date);
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {

                if (columna.Visible == true && columna.Name != "btnSeleccionar")
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });

                }


            }

            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 1;
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar que el índice de la fila sea válido
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int indice = e.RowIndex;

                // Verificar que la celda "nroDocumento" no sea nula antes de acceder a su valor
                if (dgvData.Rows[indice].Cells["nroDocumento"].Value != null)
                {
                    string nroCompra = dgvData.Rows[indice].Cells["nroDocumento"].Value.ToString();
                    Compra oCompra = new CN_Compra().ObtenerCompra(nroCompra, GlobalSettings.SucursalId);

                    // Si se hace clic en el botón "btnDetalle"
                    if (dgvData.Columns[e.ColumnIndex].Name == "btnDetalle")
                    {
                        if (indice >= 0)
                        {
                            txtIndice.Text = indice.ToString();

                            // Pasar el objeto Compra al formulario frmDetalleCompra
                            frmDetalleCompra detalleCompraForm = new frmDetalleCompra(oCompra);
                            detalleCompraForm.ShowDialog();

                            // Recargar las compras después de cerrar el formulario
                            CargarComprasEntreFechas(dtpFechaDesde.Value.Date, dtpFechaHasta.Value.Date.AddDays(+1));
                        }
                    }

                    // Si se hace clic en el botón "btnEditarCompra"
                    if (dgvData.Columns[e.ColumnIndex].Name == "btnEditarCompra")
                    {
                        frmCompras editarCompraForm = new frmCompras(_Usuario, oCompra);
                        editarCompraForm.ShowDialog();
                    }
                }
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para Exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable();

                foreach (DataGridViewColumn columna in dgvData.Columns)
                {
                    if (columna.HeaderText != "" && columna.Visible)
                    {
                        // Cambiar el tipo de la columna 4 a decimal
                        if (columna.Index == 4)
                        {
                            dt.Columns.Add(columna.HeaderText, typeof(decimal)); // O double
                        }
                        else
                        {
                            dt.Columns.Add(columna.HeaderText, typeof(string));
                        }
                    }
                }

                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Visible)
                    {
                        dt.Rows.Add(new object[]
                        {
                    row.Cells[1].Value.ToString(),
                    row.Cells[2].Value.ToString(),
                    row.Cells[3].Value.ToString(),
                    Convert.ToDecimal(row.Cells[4].Value), // Convertir a decimal la columna 4
                    row.Cells[5].Value.ToString(),
                        });
                    }
                }

                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.FileName = string.Format("ReporteListadoCompras_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
                saveFile.Filter = "Excel Files | *.xlsx";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Listado de Compras");

                        // Formatear la columna 4 como numérica con dos decimales
                        var rangoNumerico = hoja.Column(4); // La cuarta columna en Excel (Index 4) es la que queremos formatear
                        rangoNumerico.Style.NumberFormat.Format = "#,##0.00"; // Formato numérico con punto decimal y dos decimales

                        hoja.ColumnsUsed().AdjustToContents();
                        wb.SaveAs(saveFile.FileName);
                        MessageBox.Show("Planilla Exportada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Error al generar la Planilla de Excel", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            string columnaFiltro = ((OpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();

            if (dgvData.Rows.Count > 0)
            {

                foreach (DataGridViewRow row in dgvData.Rows)
                {

                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;


                }

            }
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtBusqueda.Clear();
            foreach (DataGridViewRow row in dgvData.Rows)
                row.Visible = true;
        }

        private void btnBuscarReporte_Click(object sender, EventArgs e)
        {
            CargarComprasEntreFechas(dtpFechaDesde.Value.Date, dtpFechaHasta.Value.Date.AddDays(+1));
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            CargarCompras();
        }
    }
}
