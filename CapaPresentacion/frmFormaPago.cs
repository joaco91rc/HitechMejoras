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

namespace CapaPresentacion
{
    public partial class frmFormaPago : Form
    {
        public frmFormaPago()
        {
            InitializeComponent();
        }

        

        private void frmFormaPago_Load(object sender, EventArgs e)
        {
            List<FormaPago> listaFormaDePagos = new CN_FormaPago().ListarFormasDePago();

            foreach (FormaPago item in listaFormaDePagos)
            {
                dgvData.Rows.Add(new object[] { "",item.idFormaPago,
                    item.descripcion,
                    item.porcentajeRetencion,
                    item.cajaAsociada

                    });
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            FormaPago objFormaPago = new FormaPago()
            {
                idFormaPago = Convert.ToInt32(txtIdFormaPago.Text),
                descripcion = txtFormaPago.Text.Trim(),
                porcentajeRetencion = txtPorcentajeRetencion.Value,
                cajaAsociada = cboCajaAsociada.Text
            };

            if (objFormaPago.idFormaPago == 0)
            {
                // Registrar nueva forma de pago
                int idFormaPagoGenerado = new CN_FormaPago().RegistrarFormaPago(objFormaPago, out mensaje);

                if (idFormaPagoGenerado != 0)
                {
                    dgvData.Rows.Add(new object[]
                    {
                "", // Placeholder for an auto-increment column if needed
                idFormaPagoGenerado,
                objFormaPago.descripcion,
                objFormaPago.porcentajeRetencion.ToString(),
                objFormaPago.cajaAsociada.ToString()
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
                // Editar forma de pago existente
                bool resultado = new CN_FormaPago().EditarFormaPago(objFormaPago, out mensaje);
                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["idFormaPago"].Value = objFormaPago.idFormaPago;
                    row.Cells["descripcion"].Value = objFormaPago.descripcion;
                    row.Cells["porcentajeRetencion"].Value = objFormaPago.porcentajeRetencion.ToString();
                    row.Cells["cajaAsociada"].Value = objFormaPago.cajaAsociada.ToString();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
        }


        private void Limpiar()
        {
            txtIdFormaPago.Text = "0";
            txtFormaPago.Text = string.Empty;
            txtPorcentajeRetencion.Value = 0;
            txtIndice.Text = "-1"; // Resetear el índice
        }

        private void btnLimpiarDatos_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
                if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
                {
                    int indice = e.RowIndex;

                    if (indice >= 0)
                    {
                        txtIndice.Text = indice.ToString();
                        txtFormaPago.Text = dgvData.Rows[indice].Cells["descripcion"].Value.ToString();
                        txtIdFormaPago.Text = dgvData.Rows[indice].Cells["idFormaPago"].Value.ToString();
                        cboCajaAsociada.Text = dgvData.Rows[indice].Cells["cajaAsociada"].Value.ToString();
                        txtPorcentajeRetencion.Text = dgvData.Rows[indice].Cells["porcentajeRetencion"].Value.ToString();






                }

                }
            
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
           
                if (e.RowIndex < 0)
                    return;
                if (e.ColumnIndex == 0)
                {

                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                    var w = Properties.Resources.check20.Width;
                    var h = Properties.Resources.check20.Height;
                    var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Width - h) / 2;
                    e.Graphics.DrawImage(Properties.Resources.check20, new Rectangle(x, y, w, h));
                    e.Handled = true;
                }
            
        }
    }
}
