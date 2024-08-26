using CapaEntidad;
using CapaNegocio;
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
    public partial class frmDetalleCaja : Form
    {
        private Image defaultImage = Properties.Resources.trash;
        public class TotalesCaja
        {
            public decimal Total { get; set; }
            public decimal TotalMP { get; set; }
            public decimal TotalUSS { get; set; }
            public decimal TotalGalicia { get; set; }
        }

        private TotalesCaja totalesCaja = new TotalesCaja();

        public frmDetalleCaja()
        {
            InitializeComponent();
        }

        private void frmDetalleCaja_Load(object sender, EventArgs e)
        {
            
        }


        private TotalesCaja CalcularTotales()
        {
            decimal total = 0;
            decimal totalMP = 0;
            decimal totalUSS = 0;
            decimal totalGalicia = 0;

            if (dgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Visible)
                    {
                        string formaPago = row.Cells["formaPago"].Value.ToString();
                        decimal monto = Convert.ToDecimal(row.Cells["monto"].Value.ToString());

                        if (formaPago == "EFECTIVO")
                        {
                            total += monto;
                        }
                        else if (formaPago == "DOLARES" || formaPago == "CRYPTO")
                        {
                            totalUSS += monto;
                        }
                        else if (formaPago == "CREDITO" || formaPago == "DEBITO" || formaPago == "MERCADO PAGO" || formaPago == "TRANSFERENCIA")
                        {
                            totalMP += monto;
                        }
                        else if (formaPago == "GALICIA")
                        {
                            totalGalicia += monto;
                        }
                    }
                }
            }

            TotalesCaja totales = new TotalesCaja
            {
                Total = total,
                TotalMP = totalMP,
                TotalUSS = totalUSS,
                TotalGalicia = totalGalicia
            };

            return totales;
        }

        private void calcularTotal()
        {
            decimal total = 0;
            if (dgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Visible == true)
                        total += Convert.ToDecimal(row.Cells["monto"].Value.ToString());

                }
                txtSaldo.Text = total.ToString("0.00");
            }
        }

        

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            dgvData.Rows.Clear();
            GlobalSettings.fechaBusquedaDetalleCaja = dtpFecha.Value.Year.ToString() + "-" + dtpFecha.Value.Month.ToString() + "-" + dtpFecha.Value.Day.ToString();
            CajaRegistradora cajaPorFecha = new CN_CajaRegistradora().ObtenerCajaPorFecha(GlobalSettings.fechaBusquedaDetalleCaja,GlobalSettings.SucursalId);
            
            if(cajaPorFecha.fechaCierre != "")
            {

                CargarDatosEnDataGridView(GlobalSettings.fechaBusquedaDetalleCaja);
                
                //calcularTotal();

                //TotalesCaja totalesCaja = CalcularTotales();
                txtSaldo.Text = cajaPorFecha.saldo.ToString();
                txtSaldoMP.Text = cajaPorFecha.saldoMP.ToString();
                txtSaldoUSS.Text = cajaPorFecha.saldoUSS.ToString();
                txtSaldoGalicia.Text = cajaPorFecha.saldoGalicia.ToString();



            }
            else
            {
                MessageBox.Show("No se encontraron caja cerradas para la fecha seleccionada. Chequee que la caja seleccionada no este Abierta, es decir que se haa cerrado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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


                        dt.Columns.Add(columna.HeaderText, typeof(string));

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
                            row.Cells[4].Value.ToString(),
                            row.Cells[5].Value.ToString(),
                            row.Cells[6].Value.ToString(),


                        });
                    }



                }
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.FileName = string.Format("ReporteCajaDiaria_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
                saveFile.Filter = "Excel Files | *.xlsx";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Caja Diaria Exportada");
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

        

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string mensaje = string.Empty;
            if (dgvData.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                // Obtener la fila seleccionada
                DataGridViewRow selectedRow = dgvData.Rows[e.RowIndex];

                // Obtener los valores de las celdas de la fila seleccionada
                int idCompra = Convert.ToInt32(selectedRow.Cells["idCompra"].Value);
                int idVenta = Convert.ToInt32(selectedRow.Cells["idVenta"].Value);
                int idTransaccion = Convert.ToInt32(selectedRow.Cells["idTransaccion"].Value);

                string movimiento = selectedRow.Cells["tipoTransaccion"].Value.ToString();

                if (GlobalSettings.RolUsuario == 1)
                {
                    DialogResult result = MessageBox.Show("¿Está seguro de que desea eliminar esta Compra?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (movimiento == "ENTRADA")
                        {
                            if (idVenta != 0)
                            {
                                bool respuesta = new CN_Transaccion().EliminarMovimientoCajaYVenta(idVenta, out mensaje);
                                if (respuesta)
                                {
                                    bool resultado = new CN_Venta().EliminarVentaConDetalle(idVenta, out mensaje);
                                    if (resultado)
                                    {
                                        MessageBox.Show("Movimiento y Venta Asociada Eliminada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        CargarDatosEnDataGridView(GlobalSettings.fechaBusquedaDetalleCaja);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error no se pudo eliminar el Movimiento y la Venta Asociada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }

                                }

                            }
                            else
                            {
                                bool eliminarmov = new CN_Transaccion().EliminarMovimiento(idTransaccion, out mensaje);
                                if (eliminarmov)
                                {
                                    MessageBox.Show("Movimiento  Eliminado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    CargarDatosEnDataGridView(GlobalSettings.fechaBusquedaDetalleCaja);
                                }
                                else
                                {
                                    MessageBox.Show("Error no se pudo eliminar el Movimiento", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }


                        }
                        if (movimiento == "SALIDA")
                        {
                            if (idCompra != 0)
                            {
                                bool respuesta = new CN_Transaccion().EliminarMovimientoCajaYCompra(idCompra, out mensaje);
                                if (respuesta)
                                {
                                    bool resultado = new CN_Compra().EliminarCompraConDetalle(idCompra, out mensaje);
                                    if (resultado)
                                    {
                                        MessageBox.Show("Movimiento y Compra Asociada Eliminada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        
                                        CargarDatosEnDataGridView(GlobalSettings.fechaBusquedaDetalleCaja);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error no se pudo eliminar el Movimiento y la Compra Asociada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            else
                            {
                                bool eliminarmov2 = new CN_Transaccion().EliminarMovimiento(idTransaccion, out mensaje);
                                if (eliminarmov2)
                                {
                                    MessageBox.Show("Movimiento  Eliminado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    CargarDatosEnDataGridView(GlobalSettings.fechaBusquedaDetalleCaja);
                                }
                                else
                                {
                                    MessageBox.Show("Error no se pudo eliminar el Movimiento", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }



                    }
                }
                else
                {
                    MessageBox.Show("No posee permisos para Eliminar un Movimiento  de Caja", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }

        private void CargarDatosEnDataGridView(string fecha)
        {
            dgvData.Rows.Clear(); // Limpiar el DataGridView antes de cargar los datos

            List<DetalleCaja> listaCaja = new CN_DetalleCaja().Listar(fecha); // Obteniendo la lista según la fecha

            foreach (DetalleCaja item in listaCaja)
            {
                dgvData.Rows.Add(new object[] {
            item.idTransaccion,
            item.fechaApertura,
            item.hora,
            item.tipoTransaccion,
            item.monto,
            item.formaPago,
            item.docAsociado,
            item.usuarioTransaccion,            
            item.idCompra,
            item.idVenta,
            defaultImage
        });
            }
        }
    }
}
