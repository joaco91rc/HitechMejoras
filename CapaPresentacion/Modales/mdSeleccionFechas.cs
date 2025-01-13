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
    public partial class mdSeleccionFechas : Form
    {
        public DateTime FechaDesde { get; private set; }
        public DateTime FechaHasta { get; private set; }
        public int IdNegocio { get; private set; }
        public int idVendedor { get; private set; }
        public bool MostrarVendedores { get; set; } = false;
        public bool MostrarLocales { get; set; } = false;
        public mdSeleccionFechas()
        {
            InitializeComponent();
        }

        private void CargarComboBoxVendedores()
        {
            // Crear una instancia de la capa de negocio para vendedores
            CN_Vendedor objCN_Vendedor = new CN_Vendedor();

            // Obtener la lista de vendedores desde la base de datos
            List<Vendedor> listaVendedores = objCN_Vendedor.ListarVendedores();

            // Limpiar los items actuales del ComboBox
            cboVendedores.Items.Clear();

            // Llenar el ComboBox con los datos obtenidos
            foreach (Vendedor vendedor in listaVendedores)
            {
                cboVendedores.Items.Add(new OpcionCombo() { Valor = vendedor.idVendedor, Texto = $"{vendedor.nombre} {vendedor.apellido}" });
            }

            // Establecer DisplayMember y ValueMember
            cboVendedores.DisplayMember = "Texto";
            cboVendedores.ValueMember = "Valor";

            // Seleccionar el primer item por defecto si hay elementos en el ComboBox
            if (cboVendedores.Items.Count > 0)
            {
                cboVendedores.SelectedIndex = -1; // O puedes poner `0` si deseas seleccionar el primer item
            }
        }

        //private void CargarComboBoxLocales()
        //{
        //    // Crear una instancia de la capa de negocio para vendedores
        //    CN_Negocio objCN_Negocio = new CN_Negocio();

        //    // Obtener la lista de vendedores desde la base de datos
        //    List<Negocio> listaNegocios = objCN_Negocio.ListarNegocios();

        //    // Limpiar los items actuales del ComboBox
        //    cboLocal.Items.Clear();

        //    // Llenar el ComboBox con los datos obtenidos
        //    foreach (Negocio negocio in listaNegocios)
        //    {
        //        cboLocal.Items.Add(new OpcionCombo() { Valor = negocio.idNegocio, Texto = negocio.nombre});
        //    }

        //    // Establecer DisplayMember y ValueMember
        //    cboLocal.DisplayMember = "Texto";
        //    cboLocal.ValueMember = "Valor";

        //    // Seleccionar el primer item por defecto si hay elementos en el ComboBox
        //    if (cboLocal.Items.Count > 0)
        //    {
        //        cboLocal.SelectedIndex = -1; // O puedes poner `0` si deseas seleccionar el primer item
        //    }
        //}

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                // Establecer los valores de fecha desde y hasta con las horas adecuadas
                FechaDesde = dtpFechaDesde.Value.Date; // Ajusta a 00:00
                FechaHasta = dtpFechaHasta.Value.Date.AddDays(1).AddTicks(-1); // Ajusta a 23:59:59.999

                // Verificar si se seleccionó un vendedor
                if (cboVendedores.SelectedItem != null)
                {
                    idVendedor = Convert.ToInt32(((OpcionCombo)cboVendedores.SelectedItem).Valor);
                }
                else
                {
                    idVendedor = 0; // O asignar un valor predeterminado, como 0 o -1, si es necesario
                }

                //IdNegocio = Convert.ToInt32(((OpcionCombo)cboLocal.SelectedItem).Valor);

                this.DialogResult = DialogResult.OK; // Indica que se aceptaron las fechas
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void mdSeleccionFechas_Load(object sender, EventArgs e)
        {
            CargarComboBoxVendedores();
            //CargarComboBoxLocales();

            if (MostrarVendedores)
            {
                lblVendedor.Visible = true;
                cboVendedores.Visible = true;
            }
            if (MostrarLocales)
            {
                lblLocal.Visible = true;
                checkH1.Visible = true;
                checkH2.Visible = true;
                checkStore49.Visible = true;
                checkAppleCafe.Visible = true;
            }
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
