﻿using CapaEntidad;
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
    public partial class frmReporteCompras : Form
    {
        public frmReporteCompras()
        {
            InitializeComponent();
        }

        private void frmReporteCompras_Load(object sender, EventArgs e)
        {
            List<Proveedor> lista = new CN_Proveedor().Listar();
            cboProveedor.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Todos" });

            foreach  (Proveedor item in lista)
            {
                cboProveedor.Items.Add(new OpcionCombo() { Valor = item.idProveedor, Texto = item.razonSocial });
            }

            cboProveedor.DisplayMember = "Texto";
            cboProveedor.ValueMember = "Valor";
            cboProveedor.SelectedIndex = 0;

            foreach (DataGridViewColumn columna  in dgvData.Columns)
            {
                cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
            }

            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;

        }

        private void btnBuscarReporte_Click(object sender, EventArgs e)
        {
            int idProveedor = Convert.ToInt32(((OpcionCombo)cboProveedor.SelectedItem).Valor.ToString());
            List<ReporteCompra> lista = new List<ReporteCompra>();

            lista = new CN_Reporte().Compra(dtpFechaDesde.Value.ToString(), dtpFechaHasta.Value.ToString(), idProveedor, GlobalSettings.SucursalId);

            dgvData.Rows.Clear();
            foreach (ReporteCompra rc  in lista)
            {

                dgvData.Rows.Add(new object[]
                {
                    rc.fechaRegistro,
                    rc.tipoDocumento,
                    rc.nroDocumento,
                    rc.montoTotal,
                    rc.usuarioRegistro,
                    rc.documentoProveedor,
                    rc.razonSocial,
                    rc.codigoProducto,
                    rc.nombreProducto,
                    rc.categoria,
                    rc.precioCompra,
                    rc.precioVenta,
                    rc.cantidad,
                    rc.subTotal
                });
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if(dgvData.Rows.Count < 1)
            {
                MessageBox.Show("No hay registros para exportar","Mensaje", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            else
            {
                {
                    DataTable dt = new DataTable();

                    foreach (DataGridViewColumn columna in dgvData.Columns)
                    {
                        


                            dt.Columns.Add(columna.HeaderText, typeof(string));

                        
                    }

                    foreach (DataGridViewRow row in dgvData.Rows)
                    {
                        if (row.Visible)
                        {
                            dt.Rows.Add(new object[]
                            {
                            row.Cells[0].Value.ToString(),
                            row.Cells[1].Value.ToString(),
                            row.Cells[2].Value.ToString(),
                            row.Cells[3].Value.ToString(),
                            row.Cells[4].Value.ToString(),
                            row.Cells[5].Value.ToString(),
                            row.Cells[6].Value.ToString(),
                            row.Cells[7].Value.ToString(),
                            row.Cells[8].Value.ToString(),
                            row.Cells[9].Value.ToString(),
                            row.Cells[10].Value.ToString(),
                            row.Cells[11].Value.ToString(),
                            row.Cells[12].Value.ToString(),
                            row.Cells[13].Value.ToString(),


                            });
                        }



                    }
                    SaveFileDialog saveFile = new SaveFileDialog();
                    saveFile.FileName = string.Format("ReporteCompras_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
                    saveFile.Filter = "Excel Files | *.xlsx";

                    if (saveFile.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            XLWorkbook wb = new XLWorkbook();
                            var hoja = wb.Worksheets.Add(dt, "Informe de Compras");
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
