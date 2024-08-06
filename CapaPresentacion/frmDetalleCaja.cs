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
            string fecha = dtpFecha.Value.Year.ToString() + "-" + dtpFecha.Value.Month.ToString() + "-" + dtpFecha.Value.Day.ToString();
            CajaRegistradora cajaPorFecha = new CN_CajaRegistradora().ObtenerCajaPorFecha(fecha,GlobalSettings.SucursalId);
            
            if(cajaPorFecha.fechaCierre != "")
            {
                List<DetalleCaja> listaCaja = new CN_DetalleCaja().Listar(fecha);

                foreach (DetalleCaja item in listaCaja)
                {
                    dgvData.Rows.Add(new object[] {

                        item.fechaApertura,

                        item.hora,
                        item.tipoTransaccion,
                        item.monto,
                        item.formaPago,
                        item.docAsociado,
                        item.usuarioTransaccion
                             });

                }
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

        




    }
}
