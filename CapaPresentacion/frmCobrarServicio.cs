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
    public partial class frmCobrarServicio : Form
    {
        private Image defaultImage = Properties.Resources.CHECK;
        private Image defaultImageCashIcon = Properties.Resources.cashIcon;
        public frmCobrarServicio()
        {
            InitializeComponent();
        }

        private void frmCobrarServicio_Load(object sender, EventArgs e)
        {
            List<ServicioTecnico> listaServicio = new CN_ServicioTecnico().ListarServiciosCompletados(GlobalSettings.SucursalId);
            // Cargar la imagen desde los recursos

            foreach (ServicioTecnico item in listaServicio)
            {
                dgvData.Rows.Add(new object[] {
                defaultImage,
                item.IdServicio,// Asignar la imagen predeterminada
                item.IdCliente,
                item.NombreCliente,
                item.Producto,
                item.DescripcionProblema,
                item.CostoEstimado,
                item.Observaciones,
                item.EstadoServicio,                
                defaultImageCashIcon
                });
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    }

