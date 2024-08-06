using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidad;
using CapaPresentacion.Utilidades;

namespace CapaPresentacion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
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
            cboSucursal.SelectedIndexChanged += cboSucursal_SelectedIndexChanged;
        }
        private void Login_Load(object sender, EventArgs e)
        {cboSucursal.Enabled = true;
            CargarComboBoxSucursal();
        }
        //Boton Ingresar
        private void iconButton1_Click(object sender, EventArgs e)

        {
            if(cboSucursal.SelectedIndex != -1) { 
            LoginUsuario();
                
            } else
            { MessageBox.Show("La Sucursal es Obligatoria", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

        }

        private void LoginUsuario()
        {
            List<Usuario> test = new CN_Usuario().Listar();

            Usuario oUsuario = new CN_Usuario().Listar().Where(u => u.documento == txtUsuario.Text && u.clave == txtContrasena.Text).FirstOrDefault();

            if (oUsuario != null)
            {
                Form form1 = new Inicio(oUsuario);
                form1.Show();
                this.Hide();
                form1.FormClosing += form_closing;
            }



            else
            {
                MessageBox.Show("Usuario o Contraseña Invalidos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void form_closing(object sender, FormClosingEventArgs e)
        {
            this.Show();
            txtContrasena.Text = "";
            txtUsuario.Text = "";
        }

        private void txtContrasena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cboSucursal.SelectedIndex != -1)
                {
                    LoginUsuario();

                }
                else
                { MessageBox.Show("La Sucursal es Obligatoria", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                
            }
        }

        private void cboSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSucursal.SelectedIndex != -1)
            {

                GlobalSettings.SucursalId = Convert.ToInt32(((OpcionCombo)cboSucursal.SelectedItem).Valor);
                GlobalSettings.NombreSucursal = ((OpcionCombo)cboSucursal.SelectedItem).Texto;
                
            }
        }

        private void cboSucursal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cboSucursal.SelectedIndex != -1)
                {
                    LoginUsuario();

                }
                else
                { MessageBox.Show("La Sucursal es Obligatoria", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

            }
        }
    }
}

