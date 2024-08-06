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
    public partial class mdTraspasoASucursal : Form
    {
        public mdTraspasoASucursal(int idProducto, string nombre)
        {
            InitializeComponent();

            // Asignar los valores a los controles del formulario modal
            txtIdProducto.Text = idProducto.ToString();
            txtProducto.Text = nombre;
            lblCantStock.Text =new CN_ProductoNegocio().ObtenerStockProductoEnSucursal(idProducto, GlobalSettings.SucursalId).ToString();

        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mdTraspasoASucursal_Load(object sender, EventArgs e)
        {
            txtFechaTraspaso.Text = DateTime.Now.Date.ToString();
            CargarComboBoxSucursal();
           
        }

        private void CargarComboBoxSucursal()
        {
            // Crear una instancia de la capa de negocio
            CN_Negocio objCN_Negocio = new CN_Negocio();

            // Obtener la lista de formas de pago desde la base de datos
            List<Negocio> listaNegocio = objCN_Negocio.ListarNegocios();

            // Limpiar los items actuales del ComboBox
            cboSucursal.Items.Clear();


            // Llenar el ComboBox con los datos obtenidos
            foreach (Negocio negocio in listaNegocio)
            {
                cboSucursal.Items.Add(new OpcionCombo() { Valor = negocio.idNegocio, Texto = negocio.nombre });
            }

            // Establecer DisplayMember y ValueMember
            cboSucursal.DisplayMember = "Texto";
            cboSucursal.ValueMember = "Valor";


            // Seleccionar el primer item por defecto si hay elementos en el ComboBox
            if (cboSucursal.Items.Count > 0)
            {
                cboSucursal.SelectedIndex = -1;

            }
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(txtCantidad.Value > 0 && cboSucursal.SelectedIndex != -1)
            {
                OrdenTraspaso orden = new OrdenTraspaso();
                orden.FechaCreacion = txtFechaTraspaso.Value.Date;
                orden.FechaConfirmacion = null;
                orden.IdProducto = Convert.ToInt32(txtIdProducto.Text);
                orden.IdSucursalDestino = Convert.ToInt32(((OpcionCombo)cboSucursal.SelectedItem).Valor.ToString());
                orden.IdSucursalOrigen = GlobalSettings.SucursalId;
                orden.Cantidad = Convert.ToInt32(txtCantidad.Value);
                orden.Confirmada = "0";

                int stockactualproducto = new CN_ProductoNegocio().ObtenerStockProductoEnSucursal(orden.IdProducto, GlobalSettings.SucursalId);
                if(orden.Cantidad <= stockactualproducto)
                {
                    bool insertar = new CN_OrdenTraspaso().InsertarOrdenTraspaso(orden);
                    if (insertar)
                    {
                        new CN_ProductoNegocio().CargarOActualizarStockProducto(orden.IdProducto, GlobalSettings.SucursalId, -orden.Cantidad);
                        MessageBox.Show("Orden de Traspaso Generada y Stock Descontado de la Sucursal Actual");
                        this.Close();
                    }
                    else
                    {

                        MessageBox.Show(" No se pudo generar la Orden de Traspaso");
                    }
                }
                else{
                    MessageBox.Show("La cantidad a Traspasar debe ser menor igual que la cantidad de stock actual en la Sucursal de Origen");

                }
                
               

               
            }
        }
    }
}
