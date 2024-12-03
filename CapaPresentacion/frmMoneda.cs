using CapaEntidad;
using CapaNegocio;
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
    public partial class frmMoneda : Form
    {
        private Image defaultImage = Properties.Resources.CHECK;
        public frmMoneda()
        {
            InitializeComponent();
        }

        private Moneda CrearMoneda()
        {
            return new Moneda()
            {
                IdMoneda = Convert.ToInt32(txtIdMoneda.Text),
                Nombre = txtNombreMoneda.Text,
                Simbolo = txtSimbolo.Text
            };
        }

        private void frmMoneda_Load(object sender, EventArgs e)
        {
            List<Moneda> listaMonedas = new CN_Moneda().ListarMonedas();

            foreach (Moneda item in listaMonedas)
            {
                dgvData.Rows.Add(new object[] {defaultImage, item.IdMoneda,
                    item.Nombre,
                    item.Simbolo

                    });
            }
        }
        private void Limpiar() {
            txtIndice.Text = "-1";
            txtIdMoneda.Text = "0";
            txtNombreMoneda.Text = string.Empty;
            txtSimbolo.Text = string.Empty;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        { string mensaje = string.Empty;
            if(txtNombreMoneda.Text == string.Empty || txtSimbolo.Text == string.Empty) {
                MessageBox.Show("Debe ingresar el nombre de la moneda y el Simbolo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 
            
                
            Moneda objMoneda = CrearMoneda();





            try
            {
                if (objMoneda.IdMoneda == 0)
                {
                    int idMonedaGenerado = new CN_Moneda().RegistrarMoneda(objMoneda, out mensaje);
                    if (idMonedaGenerado != 0)
                    {
                        dgvData.Rows.Add(new object[] {defaultImage, idMonedaGenerado ,txtNombreMoneda.Text, txtSimbolo.Text
            });

                        Limpiar();
                    }

                }
                else
                {
                    bool editarNoneda = new CN_Moneda().EditarMoneda(objMoneda, out mensaje);
                    if (editarNoneda)
                    {
                        DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                        row.Cells["idMoneda"].Value = txtIdMoneda.Text;
                        row.Cells["nombreMoneda"].Value = txtNombreMoneda.Text;
                        row.Cells["simbolo"].Value = txtSimbolo.Text;
                        Limpiar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        txtMonedaSeleccionada.Text = dgvData.Rows[indice].Cells["nombreMoneda"].Value.ToString();
                        txtIdMoneda.Text = dgvData.Rows[indice].Cells["idMoneda"].Value.ToString();
                        txtNombreMoneda.Text = dgvData.Rows[indice].Cells["nombreMoneda"].Value.ToString();
                        txtSimbolo.Text = dgvData.Rows[indice].Cells["simbolo"].Value.ToString();
                       
                    }
                }
            
        }
    }
}
