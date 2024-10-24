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
    public partial class frmDeuda : Form
    {
        private Image defaultImage = Properties.Resources.CHECK;
        private Image defaultImage2 = Properties.Resources.trash;
        public frmDeuda()
        {
            InitializeComponent();
        }
        private void CargarDeudas()
        {
            dgvData.Rows.Clear();
            List<Deuda> listaDeudas = new CN_Deuda().ListarDeudas().Where(deuda => deuda.idSucursalDestino == GlobalSettings.SucursalId).ToList();

            foreach (Deuda item in listaDeudas)
            {



                dgvData.Rows.Add(new object[] {item.fecha,
                    item.idDeuda,
                    item.nombreProducto,
                    item.nombreSucursalOrigen,
                    item.nombreSucursalDestino,
                    item.costo,
                    item.idTraspasoMercaderia,
                    item.idSucursalOrigen,
                    item.idSucursalDestino,
                    item.estado,

                    defaultImage,
                    defaultImage2
                    });
            }


        }

        private void ImprimirDeudaLocales()
        {
            var deudasPorSucursal = new CN_Deuda().CalcularDeudaTotalPorSucursal();

            // Asignar la deuda a cada TextBox según el ID de la sucursal
            if (deudasPorSucursal.ContainsKey(1))
            {
                txtDeudaH1.Text = deudasPorSucursal[1].ToString("0.00"); // Formato de moneda
            }
            else
            {
                txtDeudaH1.Text = "0"; // Si no hay deuda, asignar cero
            }

            if (deudasPorSucursal.ContainsKey(2))
            {
                txtDeudaH2.Text = deudasPorSucursal[2].ToString("0.00"); // Formato de moneda
            }
            else
            {
                txtDeudaH2.Text = "0"; // Si no hay deuda, asignar cero
            }

            if (deudasPorSucursal.ContainsKey(3))
            {
                txtDeudaAS.Text = deudasPorSucursal[3].ToString("0.00"); // Formato de moneda
            }
            else
            {
                txtDeudaAS.Text = "0"; // Si no hay deuda, asignar cero
            }

            if (deudasPorSucursal.ContainsKey(4))
            {
                txtAC.Text = deudasPorSucursal[4].ToString("0.00"); // Formato de moneda
            }
            else
            {
                txtAC.Text = "0"; // Si no hay deuda, asignar cero
            }
        }

        private void frmDeuda_Load(object sender, EventArgs e)
        {
            CargarDeudas();
            ImprimirDeudaLocales();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnCancelarDeuda")
            {
                if(GlobalSettings.RolUsuario == 1)
                {
                    DataGridViewRow selectedRow = dgvData.Rows[e.RowIndex];
                    int idDeuda = Convert.ToInt32(selectedRow.Cells["idDeuda"].Value);
                    var cancelarDeuda = new CN_Deuda().ActualizarEstadoDeuda(idDeuda);
                    if (cancelarDeuda)
                    {
                        MessageBox.Show("Deuda Pagada.");
                        CargarDeudas();
                        ImprimirDeudaLocales();

                    } else
                    {
                        MessageBox.Show("No se pudo pagar la Deuda Seleccionada.");
                    }

                } else
                {
                    MessageBox.Show("No posee permisos para Cancelar la Deuda Seleccionada. Contactese con un Administrador");
                }
            }
        }
    }
}
