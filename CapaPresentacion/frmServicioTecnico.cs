using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
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
    public partial class frmServicioTecnico : Form
    {
        private Image defaultImage = Properties.Resources.CHECK;
        private Image defaultImageCambiarEstado = Properties.Resources.traspasar;
        private Image defaultImageCashIcon = Properties.Resources.cashIcon;
        public frmServicioTecnico()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(txtCliente.Text != string.Empty && txtDescripcionProblema.Text != string.Empty && txtproductoAReparar.Text != string.Empty)
            {
                ServicioTecnico servicio = new ServicioTecnico()
                {
                    FechaEntregaEstimada = dtpFechaEntregaEstimada.Value,
                    FechaRecepcion = DateTime.Now.Date,
                    DescripcionProblema = txtDescripcionProblema.Text,
                    CostoEstimado = Convert.ToDecimal(txtCostoEstimado.Text),
                    Observaciones = txtObservaciones.Text,
                    EstadoServicio ="INGRESADO",
                    FechaRegistro = DateTime.Now.Date,
                    IdCliente = Convert.ToInt32(txtIdCliente.Text),
                    Producto = txtproductoAReparar.Text,
                    
                };
                string mensaje = string.Empty;
                var resultado = new CN_ServicioTecnico().InsertarServicioTecnico(servicio, out mensaje);
                if (resultado)
                {
                    MessageBox.Show("Servicio Tecnico Ingresado" , "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvData.Rows.Add(new object[] { defaultImage, txtIdProveedor.Text, txtIdCliente.Text,txtCliente.Text, txtproductoAReparar.Text,txtDescripcionProblema.Text
                    ,txtCostoEstimado.Text,txtObservaciones.Text,"INGRESADO",defaultImageCambiarEstado, defaultImageCashIcon});
                } else
                {
                    MessageBox.Show("No se pudo ingresar el Servicio tecnico", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }


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
                    txtproductoAReparar.Select();
                }
                else
                {
                    txtCliente.Select();
                }
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string mensaje = string.Empty;
            int indice = e.RowIndex;
            txtIndice.Text = indice.ToString();
            int idServicio = Convert.ToInt32(dgvData.Rows[indice].Cells["idServicio"].Value);
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvData.Columns["btnCambiarEstado"].Index)
            {
                
                
                
                bool cambiarEstado = new CN_ServicioTecnico().CambiarEstadoIngresadoAPendiente(idServicio, out mensaje);
                if (cambiarEstado)
                {
                    MessageBox.Show("Se ha cambiado le Estado de la reparacion a pendiente", "Servicio Tecnico Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["estado"].Value = "PENDIENTE";
                }
            }
            else
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == dgvData.Columns["btnCobrar"].Index)
                {
                    
                }
            }
        }

        private void frmServicioTecnico_Load(object sender, EventArgs e)
        {
            List<ServicioTecnico> listaServicio = new CN_ServicioTecnico().ListarServiciosPendientes(GlobalSettings.SucursalId);
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
                defaultImageCambiarEstado,
                defaultImageCashIcon
                });
            }
        }
    }
}
