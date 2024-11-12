using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using ClosedXML.Excel;
using OfficeOpenXml;
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
    public partial class frmConsultaStock : Form
    {
        public decimal cotizacionActiva { get; set; } = new CN_Cotizacion().CotizacionActiva().importe;
        public frmConsultaStock()
        {
            InitializeComponent();
        }

        private void frmConsultaStock_Load(object sender, EventArgs e)
        {
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

            List<Producto> listaProducto = new CN_Producto().Listar();

            foreach (Producto item in listaProducto)
            {
                decimal precioVentaCotizado = Math.Round((item.precioVenta * cotizacionActiva) / 1000, 0) * 1000 - 100;

                decimal precioConIncremento = Math.Round((precioVentaCotizado * 1.30m) / 1000, 0) * 1000 - 100;


                dgvData.Rows.Add(new object[] {
        item.idProducto,
        item.codigo,
        item.nombre,
        item.oCategoria.idCategoria,
        item.oCategoria.descripcion,
        item.stockTotal,
        item.stockH1,
        item.stockH2,
        item.stockAS,
        item.stockAC,
        item.precioCompra,
        item.precioVenta,
        precioVentaCotizado.ToString("0.00"),
        precioConIncremento.ToString("0.00"),
        (precioConIncremento/3).ToString("0.00"),
        (precioConIncremento/6).ToString("0.00"),
        item.estado == true ? 1 : 0,
        item.estado == true ? "Activo" : "No Activo",
        item.fechaUltimaVenta,
        item.diasSinVenta
    });
            }
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBusqueda.Clear();
            foreach (DataGridViewRow row in dgvData.Rows)
                row.Visible = true;
        }

        private void ExportarExcel()
        {
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("No hay registros para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable dt = new DataTable();

            // Crear las columnas en el DataTable
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible) // Solo agregar las columnas visibles
                {
                    dt.Columns.Add(columna.HeaderText, typeof(string));
                }
            }

            // Agregar las filas al DataTable
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                if (row.Visible)
                {
                    DataRow fila = dt.NewRow();

                    // Iterar solo sobre las columnas visibles
                    foreach (DataGridViewColumn columna in dgvData.Columns)
                    {
                        if (columna.Visible)
                        {
                            DataGridViewCell cell = row.Cells[columna.Index];
                            fila[columna.HeaderText] = cell.Value != null ? cell.Value.ToString() : "";
                        }
                    }

                    dt.Rows.Add(fila);
                }
            }

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = string.Format("Exportacion_Productos_{0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmss"));
            saveFile.Filter = "Archivos Excel (*.xlsx)|*.xlsx";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var hoja = wb.Worksheets.Add(dt, "Productos");

                        // Ajustar el ancho de las columnas
                        hoja.ColumnsUsed().AdjustToContents();

                        // Guardar el archivo Excel
                        wb.SaveAs(saveFile.FileName);
                        MessageBox.Show("Archivo Excel exportado correctamente.", "Exportación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar a Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExportarExcel(int idLocal)
        {
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("No hay registros para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable dt = new DataTable();

            // Agregar columnas específicas al DataTable
            dt.Columns.Add("Codigo", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Categoria", typeof(string));
            dt.Columns.Add("Stock", typeof(int)); // Solo una columna de stock

            // Determinar la columna de stock según el idLocal
            string stockColumn;
            if (idLocal == 1)
                stockColumn = "StockH1";   // Hitech 1
            else if (idLocal == 2)
                stockColumn = "StockH2";   // Hitech 2
            else if (idLocal == 3)
                stockColumn = "StockAS";   // Apple 49
            else if (idLocal == 4)
                stockColumn = "StockAC";   // Apple Cafe
            else
                throw new Exception("ID de local no válido");

            // Agregar las filas filtradas al DataTable
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                if (row.Visible && row.Cells[stockColumn].Value != null && Convert.ToInt32(row.Cells[stockColumn].Value) > 0)
                {
                    DataRow fila = dt.NewRow();
                    fila["Codigo"] = row.Cells["Codigo"].Value?.ToString();
                    fila["Nombre"] = row.Cells["Nombre"].Value?.ToString();
                    fila["Categoria"] = row.Cells["Categoria"].Value?.ToString();
                    fila["Stock"] = Convert.ToInt32(row.Cells[stockColumn].Value); // Solo la columna de stock correspondiente al local

                    dt.Rows.Add(fila);
                }
            }

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = string.Format("Exportacion_Productos_{0}_Local_{1}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmss"),stockColumn);
            saveFile.Filter = "Archivos Excel (*.xlsx)|*.xlsx";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var hoja = wb.Worksheets.Add(dt, "Productos");

                        // Ajustar el ancho de las columnas
                        hoja.ColumnsUsed().AdjustToContents();

                        // Guardar el archivo Excel
                        wb.SaveAs(saveFile.FileName);
                        MessageBox.Show("Archivo Excel exportado correctamente.", "Exportación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar a Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            ExportarExcel();
        }

        private void btnBajarReporteStock_Click(object sender, EventArgs e)
        {
            int idLocal = 0;
            if (GlobalSettings.RolUsuario == 1) { 
            

            switch (cboStockLocal.Text)
            {
                case "HITECH 1":
                    idLocal = 1;
                    break;
                case "HITECH 2":
                    idLocal = 2;
                    break;
                case "APPLE 49":
                    idLocal = 3;
                    break;
                case "APPLE CAFE":
                    idLocal = 4;
                    break;
                default:
                    idLocal = GlobalSettings.SucursalId; // Valor predeterminado si no coincide con ninguna opción
                    break;
            }
            
            } else
            {
                idLocal = GlobalSettings.SucursalId;
                MessageBox.Show("Sin permisos para consultar Stock de otro local al cual no pertenecce. EN su lugar se descargara el stock de su Local");
            }

            ExportarExcel(idLocal);
        }

    }
}
