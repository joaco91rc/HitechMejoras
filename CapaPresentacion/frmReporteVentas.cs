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
            rv.nombreProducto,
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
                    if (columna.Index == 9 || columna.Index == 10) // Omite columnas 9 y 10
                        continue;

                    if (columna.Index >= 4 && columna.Index <= 7) // Columnas E a H como decimal
                    {
                        dt.Columns.Add(columna.HeaderText, typeof(decimal));
                    }
                    else if (columna.Index == 8) // Columna I como porcentaje
                    {
                        dt.Columns.Add(columna.HeaderText, typeof(decimal));
                    }
                    else // Resto de columnas como texto
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
                            if (i == 9 || i == 10) // Omite columnas 9 y 10
                                continue;

                            if (i >= 4 && i <= 7) // Columnas E a H como decimal
                            {
                                values.Add(row.Cells[i].Value != null && !string.IsNullOrEmpty(row.Cells[i].Value.ToString())
                                    ? Convert.ToDecimal(row.Cells[i].Value)
                                    : (object)0);
                            }
                            else if (i == 8) // Columna I como porcentaje
                            {
                                values.Add(row.Cells[i].Value != null && !string.IsNullOrEmpty(row.Cells[i].Value.ToString())
                                    ? Convert.ToDecimal(row.Cells[i].Value) / 100 // Dividir para aplicar como porcentaje
                                    : (object)0);
                            }
                            else // Resto de columnas como texto
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
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            var hoja = wb.Worksheets.Add(dt, "Informe de Ventas");

                            // Cambiar nombres de los encabezados
                            hoja.Cell(1, 3).Value = "NUMERO VENTA"; // Columna C
                            hoja.Cell(1, 4).Value = "PRODUCTOS VENDIDOS"; // Columna D
                            hoja.Cell(1, 5).Value = "MONTO RECIBIDO VENTA"; // Columna E

                            // Aplicar formato sin decimales para la columna C
                            hoja.Column(3).Style.NumberFormat.Format = "@"; // Texto en columna C

                            // Aplicar formato como texto para la columna D
                            hoja.Column(4).Style.NumberFormat.Format = "@"; // Texto en columna D

                            // Formato con dos decimales para columnas E a H
                            for (int col = 5; col <= 8; col++)
                            {
                                hoja.Column(col).Style.NumberFormat.Format = "#,##0.00"; // Formato decimal con dos decimales
                            }

                            // Aplicar formato de porcentaje a la columna I
                            hoja.Column(9).Style.NumberFormat.Format = "0.00%";

                            hoja.ColumnsUsed().AdjustToContents(); // Ajustar ancho de las columnas
                            wb.SaveAs(saveFile.FileName);
                        }

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
