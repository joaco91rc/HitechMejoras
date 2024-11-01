using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Modales
{
    public partial class mdAgregarPagoParcial : Form
    {
        public int IdCliente { get; set; }
        public PagoParcial _PagoParcial { get; set; }
        public mdAgregarPagoParcial()
        {
            InitializeComponent();
        }

        private void CargarModalagosParcialesPorCliente()
        {
            List<PagoParcial> listaPagosParcialesPorCliente = new CN_PagoParcial().ConsultarPagosParcialesPorCliente(IdCliente);
            foreach (PagoParcial item in listaPagosParcialesPorCliente)
            {
                dgvData.Rows.Add(new object[] {item.idPagoParcial, item.fechaRegistro,item.productoReservado,item.monto,item.idCliente,
                    item.nombreCliente });
            }
        }

        private void mdAgregarPagoParcial_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {

                if (columna.Visible == true)
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });

                }


            }

            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 1;
            CargarModalagosParcialesPorCliente();
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int iColumn = e.ColumnIndex;
            if (iRow >= 0 && iColumn >= 0)
            {
                _PagoParcial = new PagoParcial()
                {
                    idPagoParcial = Convert.ToInt32(dgvData.Rows[iRow].Cells["idPagoParcial"].Value),
                    monto = Convert.ToDecimal(dgvData.Rows[iRow].Cells["montoSeña"].Value),
                    productoReservado = dgvData.Rows[iRow].Cells["productoReservado"].Value.ToString(),
                    idCliente = Convert.ToInt32(dgvData.Rows[iRow].Cells["idCliente"].Value),
                    nombreCliente = dgvData.Rows[iRow].Cells["nombreCliente"].Value.ToString(),
                };
                this.DialogResult = DialogResult.OK;
                this.Close();
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

        private void btnCerrarModal_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
