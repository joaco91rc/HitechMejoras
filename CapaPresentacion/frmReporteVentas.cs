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
    public partial class frmReporteVentas : Form
    {
        public frmReporteVentas()
        {
            InitializeComponent();
        }

        private void frmReporteVentas_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
            }

            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;
            dtpFechaDesde.Value = DateTime.Now.Date;
            dtpFechaHasta.Value = DateTime.Now.AddDays(30).Date.AddHours(23).AddMinutes(59);
        }

        

        private void CargarReporteGananciaVentas()
        {
            // Obtener el rango de fechas y el id de negocio
            DateTime fechaDesde = dtpFechaDesde.Value.Date; // Esto da 00:00:00
            DateTime fechaHasta = dtpFechaHasta.Value.Date.AddDays(1).AddTicks(-1);
            int idNegocio = GlobalSettings.SucursalId;

            // Obtener los datos del reporte
            List<ReporteVenta> lista = new CN_Reporte().GananciaPorVentas(fechaDesde, fechaHasta, idNegocio);

            // Limpiar el DataGridView antes de cargar nuevos datos
            dgvData.Rows.Clear();

            // Agregar cada ReporteVenta al DataGridView
            foreach (ReporteVenta rv in lista)
            {
                dgvData.Rows.Add(new object[]
                {
                                // Nombre del local
            rv.fechaRegistro,                  // Fecha de registro
            rv.tipoDocumento,                  // Tipo de documento
            rv.nroDocumento,                   // Número de documento
            rv.montoTotal, 
            rv.cotizacionDolar,// Monto total de la venta
            rv.costoTotalProductos,            // Costo total de los productos
            rv.margenGananciaEnDolares,       // Margen de ganancia en dólares
            rv.porcentajeMargenGanancia,      // Porcentaje del margen de ganancia
            rv.vendedor,                       // Nombre del vendedor
            rv.documentoCliente,               // Documento del cliente
            rv.nombreCliente ,                   // Nombre del cliente
            rv.nombreLocal,
                                                // Agregar o eliminar columnas según sea necesario
                });
            }

            // Opcional: ajustar el ancho de las columnas o agregar configuraciones
            // dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        private void btnBuscarReporte_Click(object sender, EventArgs e)
        {
            CargarReporteGananciaVentas();
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

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("No hay registros para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable();

                foreach (DataGridViewColumn columna in dgvData.Columns)
                {
                    // Omite las columnas 8 y 9 (índices 8 y 9)
                    if (columna.Index == 9 || columna.Index == 10)
                        continue;

                    // Especifica el tipo de datos de las columnas C a F como decimal y G como porcentaje
                    if (columna.Index >= 2 && columna.Index <= 5)
                    {
                        dt.Columns.Add(columna.HeaderText, typeof(decimal));
                    }
                    else if (columna.Index == 6) // Columna G para porcentaje
                    {
                        dt.Columns.Add(columna.HeaderText, typeof(decimal));
                    }
                    else
                    {
                        dt.Columns.Add(columna.HeaderText, typeof(string));
                    }
                }

                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Visible)
                    {
                        List<object> values = new List<object>();

                        for (int i = 0; i < dgvData.Columns.Count; i++)
                        {
                            // Omite las columnas 8 y 9
                            if (i == 8 || i == 9)
                                continue;

                            // Formato para columnas C a F como decimal y G como porcentaje
                            if (i >= 2 && i <= 6)
                            {
                                values.Add(row.Cells[i].Value != null && !string.IsNullOrEmpty(row.Cells[i].Value.ToString())
                                    ? Convert.ToDecimal(row.Cells[i].Value)
                                    : (object)0);
                            }
                            else if (i == 7) // Columna G como porcentaje
                            {
                                values.Add(row.Cells[i].Value != null && !string.IsNullOrEmpty(row.Cells[i].Value.ToString())
                                    ? Convert.ToDecimal(row.Cells[i].Value) / 100
                                    : (object)0);
                            }
                            else
                            {
                                values.Add(row.Cells[i].Value?.ToString() ?? string.Empty);
                            }
                        }

                        dt.Rows.Add(values.ToArray());
                    }
                }

                SaveFileDialog saveFile = new SaveFileDialog
                {
                    FileName = string.Format("ReporteMargenVentas_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss")),
                    Filter = "Excel Files | *.xlsx"
                };

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Informe de Ventas");

                        // Formato de porcentaje para columna G
                        hoja.Column(7).Style.NumberFormat.Format = "0.00%";

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




    }
}
