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
using CapaPresentacion;
using System.Globalization;

namespace CapaPresentacion.Modales
{
    public partial class mdAperturacaja : Form
    {
        private Usuario _Usuario;
        public mdAperturacaja(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }

        private void iconPictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            if (txtSaldoInicial.Text != "" && txtSaldoInicialMP.Text != "" && txtSaldoInicialUSS.Text != "" && txtSaldoInicialGalicia.Text != "")
            {
                try
                {

                    // Crear el objeto CajaRegistradora con los valores convertidos
                    CajaRegistradora objCaja = new CajaRegistradora()
                    {
                        usuarioAperturaCaja = Environment.GetEnvironmentVariable("usuario"),
                        saldoInicio = txtSaldoInicial.Value,
                        saldoInicioMP = txtSaldoInicialMP.Value,
                        saldoInicioUSS = txtSaldoInicialUSS.Value,
                        saldoInicioGalicia = txtSaldoInicialGalicia.Value
                    };

                    // Consultar si existe una caja abierta
                    List<CajaRegistradora> lista = new CN_CajaRegistradora().Listar(GlobalSettings.SucursalId);
                    CajaRegistradora cajaAbierta = lista.Where(c => c.estado == true).FirstOrDefault();

                    // Si no hay caja abierta, realizar la apertura
                    if (cajaAbierta == null)
                    {
                        int idCajaGenerado = new CN_CajaRegistradora().AperturaCaja(objCaja, out mensaje, GlobalSettings.SucursalId);
                        this.Close();
                        MessageBox.Show("Caja Abierta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Ya existe una Caja Abierta. Cierrela e intente nuevamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (FormatException ex)
                {
                    // Manejo de errores si el formato del número no es correcto
                    MessageBox.Show(ex.Message, "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Todos los Saldos Iniciales deben ser informados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void mdAperturacaja_Load(object sender, EventArgs e)
        {
            txtFechaApertura.Text = DateTime.Now.ToString();
            txtUsuario.Text = Environment.GetEnvironmentVariable("usuario");

            var ultimaCaja = new CN_CajaRegistradora().ObtenerUltimaCajaCerrada(GlobalSettings.SucursalId);

            // Verificar si ultimaCaja es null
            if (ultimaCaja != null)
            {
                txtFechaAperturaUC.Text = ultimaCaja.fechaApertura;
                txtFechaCierreUC.Text = ultimaCaja.fechaCierre;
                txtUsuarioUC.Text = ultimaCaja.usuarioAperturaCaja;
                txtSaldoEfectivoUC.Value = ultimaCaja.saldo;
                txtSaldoMPUC.Value = ultimaCaja.saldoMP;
                txtSaldoUSSUC.Value = ultimaCaja.saldoUSS;
                txtSaldoGaliciaUC.Value = ultimaCaja.saldoGalicia;
            }
            else
            {
                // Si ultimaCaja es null, limpiar los campos
                txtFechaAperturaUC.Text = string.Empty;
                txtFechaCierreUC.Text = string.Empty;
                txtUsuarioUC.Text = string.Empty;
                txtSaldoEfectivoUC.Value = 0; // o cualquier valor predeterminado
                txtSaldoMPUC.Value = 0; // o cualquier valor predeterminado
                txtSaldoUSSUC.Value = 0; // o cualquier valor predeterminado
                txtSaldoGaliciaUC.Value = 0; // o cualquier valor predeterminado
            }








        }









    }
}
