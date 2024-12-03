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
                    item.simboloMoneda,
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
            var deudasPorSucursal = new CN_Deuda().CalcularDeudaTotalPorSucursalYMoneda(GlobalSettings.SucursalId);

            // Asignar la deuda en ARS a cada TextBox correspondiente para ARS
            if (deudasPorSucursal.ContainsKey(1))
            {
                txtDeudaH1ARS.Text = deudasPorSucursal[1].ContainsKey("ARS") ? deudasPorSucursal[1]["ARS"].ToString("0.00") : "0"; // Deuda en ARS para Sucursal 1
            }
            else
            {
                txtDeudaH1ARS.Text = "0"; // Si no hay deuda, asignar cero
            }

            if (deudasPorSucursal.ContainsKey(2))
            {
                txtDeudaH2ARS.Text = deudasPorSucursal[2].ContainsKey("ARS") ? deudasPorSucursal[2]["ARS"].ToString("0.00") : "0"; // Deuda en ARS para Sucursal 2
            }
            else
            {
                txtDeudaH2ARS.Text = "0"; // Si no hay deuda, asignar cero
            }

            if (deudasPorSucursal.ContainsKey(3))
            {
                txtDeudaASARS.Text = deudasPorSucursal[3].ContainsKey("ARS") ? deudasPorSucursal[3]["ARS"].ToString("0.00") : "0"; // Deuda en ARS para Sucursal 3
            }
            else
            {
                txtDeudaASARS.Text = "0"; // Si no hay deuda, asignar cero
            }

            if (deudasPorSucursal.ContainsKey(4))
            {
                txtDeudaACARS.Text = deudasPorSucursal[4].ContainsKey("ARS") ? deudasPorSucursal[4]["ARS"].ToString("0.00") : "0"; // Deuda en ARS para Sucursal 4
            }
            else
            {
                txtDeudaACARS.Text = "0"; // Si no hay deuda, asignar cero
            }

            // Asignar la deuda en USD a cada TextBox correspondiente para USD
            if (deudasPorSucursal.ContainsKey(1))
            {
                txtDeudaH1USD.Text = deudasPorSucursal[1].ContainsKey("USD") ? deudasPorSucursal[1]["USD"].ToString("0.00") : "0"; // Deuda en USD para Sucursal 1
            }
            else
            {
                txtDeudaH1USD.Text = "0"; // Si no hay deuda, asignar cero
            }

            if (deudasPorSucursal.ContainsKey(2))
            {
                txtDeudaH2USD.Text = deudasPorSucursal[2].ContainsKey("USD") ? deudasPorSucursal[2]["USD"].ToString("0.00") : "0"; // Deuda en USD para Sucursal 2
            }
            else
            {
                txtDeudaH2USD.Text = "0"; // Si no hay deuda, asignar cero
            }

            if (deudasPorSucursal.ContainsKey(3))
            {
                txtDeudaASUSD.Text = deudasPorSucursal[3].ContainsKey("USD") ? deudasPorSucursal[3]["USD"].ToString("0.00") : "0"; // Deuda en USD para Sucursal 3
            }
            else
            {
                txtDeudaASUSD.Text = "0"; // Si no hay deuda, asignar cero
            }

            if (deudasPorSucursal.ContainsKey(4))
            {
                txtDeudaACUSD.Text = deudasPorSucursal[4].ContainsKey("USD") ? deudasPorSucursal[4]["USD"].ToString("0.00") : "0"; // Deuda en USD para Sucursal 4
            }
            else
            {
                txtDeudaACUSD.Text = "0"; // Si no hay deuda, asignar cero
            }
        }

        private void ImprimirDeudaSucursalDeudoraALocales(int idSucursalDeudora)
        {
            // Obtener las deudas de la sucursal deudora a las otras sucursales
            var deudasPorSucursal = new CN_Deuda().CalcularDeudaPorSucursalRestante(idSucursalDeudora);

            // Asignar la deuda en ARS y USD a cada sucursal deudora
            foreach (var sucursal in new[] { 1, 2, 3, 4 })
            {
                if (sucursal != idSucursalDeudora) // Solo asignar deudas a otras sucursales
                {
                    // Inicializamos las deudas específicas para ARS y USD en cero
                    decimal deudaARS = 0;
                    decimal deudaUSD = 0;

                    // Si hay deuda de ARS hacia esta sucursal
                    if (deudasPorSucursal.ContainsKey(sucursal) && deudasPorSucursal[sucursal].ContainsKey("ARS"))
                    {
                        deudaARS = deudasPorSucursal[sucursal]["ARS"];
                    }

                    // Si hay deuda de USD hacia esta sucursal
                    if (deudasPorSucursal.ContainsKey(sucursal) && deudasPorSucursal[sucursal].ContainsKey("USD"))
                    {
                        deudaUSD = deudasPorSucursal[sucursal]["USD"];
                    }

                    // Asignamos la deuda a los TextBox correspondientes, con los valores correctos
                    AsignarDeudaTextBox(sucursal, "ARS", deudaARS);
                    AsignarDeudaTextBox(sucursal, "USD", deudaUSD);
                }
            }
        }

        // Método para asignar la deuda a los TextBox
        private void AsignarDeudaTextBox(int sucursal, string moneda, decimal deuda)
        {
            string deudaFormateada = deuda.ToString("0.00");

            // Asignar la deuda según la moneda y la sucursal
            switch (sucursal)
            {
                case 1:
                    if (moneda == "ARS")
                        txtDeudaH1ARS.Text = deudaFormateada;
                    else if (moneda == "USD")
                        txtDeudaH1USD.Text = deudaFormateada;
                    break;
                case 2:
                    if (moneda == "ARS")
                        txtDeudaH2ARS.Text = deudaFormateada;
                    else if (moneda == "USD")
                        txtDeudaH2USD.Text = deudaFormateada;
                    break;
                case 3:
                    if (moneda == "ARS")
                        txtDeudaASARS.Text = deudaFormateada;
                    else if (moneda == "USD")
                        txtDeudaASUSD.Text = deudaFormateada;
                    break;
                case 4:
                    if (moneda == "ARS")
                        txtDeudaACARS.Text = deudaFormateada;
                    else if (moneda == "USD")
                        txtDeudaACUSD.Text = deudaFormateada;
                    break;
            }
        }





        private void frmDeuda_Load(object sender, EventArgs e)
        {
            CargarDeudas();
            ImprimirDeudaSucursalDeudoraALocales(GlobalSettings.SucursalId);

            int sucursalId = GlobalSettings.SucursalId;

            switch (sucursalId)
            {
                case 1:
                    lblInfoDeuda.Text = "Deuda de Sucursal Hitech 1 al resto de las Sucursales";
                    break;
                case 2:
                    lblInfoDeuda.Text = "Deuda de Sucursal Hitech 2 al resto de las Sucursales";
                    break;
                case 3:
                    lblInfoDeuda.Text = "Deuda de Sucursal Apple Store 49 al resto de las Sucursales";
                    break;
                case 4:
                    lblInfoDeuda.Text = "Deuda de Sucursal Apple Cafe al resto de las Sucursales";
                    break;
                default:
                    lblInfoDeuda.Text = "Deuda desconocida";
                    break;
            }
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
