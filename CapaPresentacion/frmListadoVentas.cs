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
    public partial class frmListadoVentas : Form
    {   private Venta _Venta;
        private Usuario _Usuario;
        private Image defaultImageView = Properties.Resources.VIEWICON;
        private Image defaultImageEditar = Properties.Resources.detail;
        public frmListadoVentas(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }
        private void CargarVentas()
        {
            
            

            dgvData.Rows.Clear();
            List<Venta> listaVentas = new CN_Venta().ObtenerVentasConDetalle(GlobalSettings.SucursalId);
            foreach (Venta item in listaVentas)
            {
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(dgvData, new object[] {
            item.idVenta,
            item.fechaRegistro.Date,
            item.tipoDocumento,
            item.nroDocumento,
            item.montoTotal,
            item.nombreCliente,
            item.nombreVendedor,
            defaultImageView,
            defaultImageEditar
             // Asigna la imagen de edición
        });

                // Añade la fila al DataGridView
                dgvData.Rows.Add(newRow);
            }

            
        }

        
        private void CargarVentasEntreFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            
            

            dgvData.Rows.Clear();
            List<Venta> listaVentas = new CN_Venta().ObtenerVentasConDetalleEntreFechas(GlobalSettings.SucursalId, fechaDesde, fechaHasta);
            foreach (Venta item in listaVentas)
            {
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(dgvData, new object[] {
            item.idVenta,
            item.fechaRegistro.Date,
            item.tipoDocumento,
            item.nroDocumento,
            item.montoTotal,
            item.nombreCliente,
            item.nombreVendedor,
            defaultImageView,
            defaultImageEditar
           // Asigna la imagen de edición
        });

                // Añade la fila al DataGridView
                dgvData.Rows.Add(newRow);
            }

            
        }


        private void frmListadoVentas_Load(object sender, EventArgs e)
        {
            dtpFechaDesde.Value = DateTime.Now.Date.AddDays(-1);
            dtpFechaHasta.Value = DateTime.Now.Date.AddDays(+1);

            CargarVentasEntreFechas(dtpFechaDesde.Value.Date, dtpFechaHasta.Value.Date);

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
            // Validar que el índice de fila sea válido
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int indice = e.RowIndex;

                // Verificar que la columna nroDocumento exista antes de acceder
                if (dgvData.Columns.Contains("nroDocumento"))
                {
                    string nroVenta = dgvData.Rows[indice].Cells["nroDocumento"].Value.ToString();
                    Venta oVenta = new CN_Venta().ObtenerVenta(nroVenta, GlobalSettings.SucursalId);

                    if (dgvData.Columns[e.ColumnIndex].Name == "btnDetalle")
                    {
                        txtIndice.Text = indice.ToString();

                        // Pasar el objeto Venta al formulario frmDetalleVenta
                        frmDetalleVenta detalleVentaForm = new frmDetalleVenta(oVenta);
                        detalleVentaForm.ShowDialog();
                        CargarVentasEntreFechas(dtpFechaDesde.Value.Date, dtpFechaHasta.Value.Date.AddDays(+1));
                    }

                    if (dgvData.Columns[e.ColumnIndex].Name == "btnEditarVenta")
                    {
                        if (GlobalSettings.RolUsuario == 1)
                        {
                            frmVentas editarVentaForm = new frmVentas(_Usuario, oVenta);
                            editarVentaForm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Solo el Administrador puede Editar una Venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }



                    }
                }
                else
                {
                    MessageBox.Show("La columna 'nroDocumento' no existe.");
                }
            }
        }


        private void btnBuscarReporte_Click(object sender, EventArgs e)
        {
            CargarVentasEntreFechas(dtpFechaDesde.Value.Date, dtpFechaHasta.Value.Date.AddDays(+1));
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            CargarVentas();
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
                    row.Cells[6].Value.ToString(),
                        });
                    }
                }

                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.FileName = string.Format("ReporteListadoVentas_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
                saveFile.Filter = "Excel Files | *.xlsx";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Listado de Ventas");

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
    }
}
