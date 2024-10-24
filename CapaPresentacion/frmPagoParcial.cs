using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
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

namespace CapaPresentacion
{
    public partial class frmPagoParcial : Form
    {
        public frmPagoParcial()
        {
            InitializeComponent();
        }
        private Image defaultImage = Properties.Resources.CHECK;

        private void CargarPagosParciales()
        {
            dgvData.Rows.Clear();
            // Mostrar todos los Clientes
            List<PagoParcial> listaPagoParciales = new CN_PagoParcial().Listar();

            foreach (PagoParcial item in listaPagoParciales)
            {



                dgvData.Rows.Add(new object[] {
            defaultImage,
            item.idPagoParcial,
            item.fechaRegistro,
            item.idCliente,
            item.nombreCliente,
            item.formaPago,
            item.monto,
            item.idVenta,
            item.numeroVenta,
            item.estado == true ? 1 : 0,
            item.estado == true ? "Pendiente" : "Usada"
        });
            }



        }
        private void frmPagoParcial_Load(object sender, EventArgs e)
        {

            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Pendiente" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Usada" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;
            CargarPagosParciales();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            using (var modal = new mdCliente())
            {
                var result = modal.ShowDialog();
                if (result == DialogResult.OK)
                {

                    txtCliente.Text = modal._Cliente.nombreCompleto;
                    txtIdCliente.Text = modal._Cliente.idCliente.ToString();


                }
                else
                {
                    txtCliente.Select();
                }
            }
        }

        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtIdCliente.Text = "0";
            txtIdPagoParcial.Text = "0";
            txtCliente.Text = string.Empty;
            txtMonto.Value = 0;
            cboFormaPago.SelectedIndex = -1;
            dtpFecha.Value = DateTime.Now;
            cboEstado.SelectedIndex = 0;
            txtCliente.Select();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            PagoParcial objPagoParcial = new PagoParcial()
            {
                idPagoParcial = Convert.ToInt32(txtIdPagoParcial.Text),
                idCliente = Convert.ToInt32(txtIdCliente.Text),
                monto = txtMonto.Value,
                formaPago = cboFormaPago.Text,
                estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false,
                idVenta = null
            };

            if (objPagoParcial.idPagoParcial == 0)
            {

                int idPagoParcialGenerado = new CN_PagoParcial().RegistrarPagoParcial(objPagoParcial, out mensaje);


                if (idPagoParcialGenerado != 0)
                {
                    dgvData.Rows.Add(new object[] {
                        defaultImage,
                        idPagoParcialGenerado,
                        dtpFecha.Value,
                        txtIdCliente.Text,
                        txtCliente.Text,
                        cboFormaPago.Text,
                        txtMonto.Value,
                        null,
                        "",
                        ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString(),
                        ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString()
            });


                    Limpiar();
                }
                else
                {

                    MessageBox.Show(mensaje);
                }


            }
            else
            {

                bool resultado = new CN_PagoParcial().ModificarPagoParcial(objPagoParcial, out mensaje);
                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["idPagoParcial"].Value = txtIdPagoParcial.Text;
                    row.Cells["idCliente"].Value = txtIdCliente.Text;
                    row.Cells["nombreCompleto"].Value = txtCliente.Text;
                    row.Cells["formaPago"].Value = cboFormaPago.Text;
                    row.Cells["monto"].Value = txtMonto.Value;
                    row.Cells["idVenta"].Value = objPagoParcial.idVenta;
                    row.Cells["numeroVenta"].Value = objPagoParcial.numeroVenta;


                    row.Cells["estadoValor"].Value = ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString();
                    row.Cells["estado"].Value = ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString();


                    Limpiar();
                    CargarPagosParciales();
                }
                else
                {

                    MessageBox.Show(mensaje);
                }

            }
        }

        private void btnLimpiarDatos_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtIdPagoParcial.Text) != 0)
            {

                if (MessageBox.Show("Desea eliminar el Pago Parcial?", "Confirmar Eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;

                    int idPagoParcial = Convert.ToInt32(txtIdPagoParcial.Text);



                    bool respuesta = new CN_PagoParcial().Eliminar(idPagoParcial, out mensaje);
                    if (respuesta)
                    {

                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                        Limpiar();
                    }

                    else
                    {

                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                    }
                }
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtSeñaSeleccionada.Text = dgvData.Rows[indice].Cells["nombreCompleto"].Value.ToString();
                    txtIdCliente.Text = dgvData.Rows[indice].Cells["idCliente"].Value.ToString();
                    txtIdPagoParcial.Text = dgvData.Rows[indice].Cells["idPagoParcial"].Value.ToString();
                    dtpFecha.Text = dgvData.Rows[indice].Cells["fecha"].Value.ToString();
                    txtCliente.Text = dgvData.Rows[indice].Cells["nombreCompleto"].Value.ToString();
                    cboFormaPago.Text = dgvData.Rows[indice].Cells["formaPago"].Value.ToString();
                    txtMonto.Value = Convert.ToDecimal(dgvData.Rows[indice].Cells["monto"].Value);
                    




                    foreach (OpcionCombo oc in cboEstado.Items)
                    {

                        if (Convert.ToInt32(oc.Valor) == (Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value)))
                        {
                            int indiceCombo = cboEstado.Items.IndexOf(oc);
                            cboEstado.SelectedIndex = indiceCombo;
                            break;

                        }

                    }
                }
            }
        }
    }
}
