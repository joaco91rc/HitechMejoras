using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad;
using FontAwesome.Sharp;
using CapaNegocio;
using CapaPresentacion.Modales;
using CapaNegocio.Services;
using CapaPresentacion.Utilidades;

namespace CapaPresentacion
{
    public partial class Inicio : Form
    {
        private int sucursalId;
        private SucursalService sucursalService;
        private static Usuario usuarioActual;
        private static IconMenuItem menuActivo = null;
        private static Form formularioActivo = null;
        public Inicio(Usuario objUsuario)
        {
            Environment.SetEnvironmentVariable("usuario", objUsuario.nombreCompleto);
            Environment.SetEnvironmentVariable("documentoUsuario", objUsuario.documento);
            Environment.SetEnvironmentVariable("rol", objUsuario.oRol.descripcion);
            usuarioActual = objUsuario;
            InitializeComponent();
        }

       

        private void iconPictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            
        }

       

        private void Inicio_Load(object sender, EventArgs e)
        {
            //sucursalService = new SucursalService();


            GlobalSettings.RolUsuario = usuarioActual.oRol.idRol;

           
            List<Permiso> listaPermisos = new CN_Permiso().Listar(usuarioActual.idUsuario);

            foreach(IconMenuItem iconMenu in menu.Items)
            {
                bool encontrado = listaPermisos.Any(m => m.nombreMenu == iconMenu.Name);
                if(encontrado==false)
                {
                    iconMenu.Visible = false;
                }
            }

            lblUsuario.Text = Environment.GetEnvironmentVariable("usuario");
            lblDocumento.Text = Environment.GetEnvironmentVariable("documentoUsuario");
            lblRol.Text = Environment.GetEnvironmentVariable("rol");
            lblSucursal.Text = GlobalSettings.NombreSucursal;
            CargarComboBoxSucursal();
            if(GlobalSettings.RolUsuario == 1)
            {
                cboSucursal.Visible = true;
                lblCambioSucursal.Visible = true;
            }
        }

        //private void AbrirFormulario(IconMenuItem menu, Form formulario)
        //{
        //    if (menuActivo != null)
        //    {
        //        menuActivo.BackColor = Color.ForestGreen;
        //        menuActivo.ForeColor = Color.FromArgb(224, 224, 224);
        //    }
        //    menu.BackColor = Color.FromArgb(178, 214, 243);
        //    menu.ForeColor = Color.ForestGreen;
        //    menuActivo = menu;

        //    if (formularioActivo != null)
        //    {
        //        formularioActivo.Close();
        //    }
        //    formularioActivo = formulario;
        //    formulario.TopLevel = false;
        //    formulario.FormBorderStyle = FormBorderStyle.None;
        //    formulario.Dock = DockStyle.Fill;
        //    formulario.BackColor = Color.FromArgb(63, 61, 64);
        //    contenedor.Controls.Add(formulario);
        //    formulario.Show();

        //}
        private void AbrirFormulario(IconMenuItem menu, Form formulario, Inicio formularioInicio)
        {
            if (menuActivo != null)
            {
                menuActivo.BackColor = Color.ForestGreen;
                menuActivo.ForeColor = Color.FromArgb(224, 224, 224);
            }
            menu.BackColor = Color.FromArgb(178, 214, 243);
            menu.ForeColor = Color.ForestGreen;
            menuActivo = menu;

            if (formularioActivo != null)
            {
                formularioActivo.Close();
            }

            // Aquí, pasa el formularioInicio al formulario que estás abriendo
            if (formulario is frmCajaRegistradora)
            {
                (formulario as frmCajaRegistradora).SetInicio(formularioInicio);
            }

            formularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.FromArgb(63, 61, 64);
            contenedor.Controls.Add(formulario);
            formulario.Show();
        }



        private void subMenuCategoria_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmCategoria(),this);
        }

        private void subMenuProducto_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmProducto(usuarioActual),this);
        }

        private void subMenuRegistrarVenta_Click(object sender, EventArgs e)
        {
            List<CajaRegistradora> lista = new CN_CajaRegistradora().Listar(GlobalSettings.SucursalId);

            CajaRegistradora cajaAbierta = lista.Where(c => c.estado == true).FirstOrDefault();

            if (cajaAbierta == null)
            {
                MessageBox.Show("No hay cajas Abiertas. Por favor abra una caja para poder Registrar una Venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                AbrirFormulario(menuVentas, new frmVentas(usuarioActual),this);
            }


        }

        private void subMenuDetalleVenta_Click(object sender, EventArgs e)
        {
            //AbrirFormulario(menuVentas, new frmDetalleVenta());
            AbrirFormulario(menuVentas, new frmListadoVentas(usuarioActual),this);
        }

        private void subMenuRegistrarCompra_Click(object sender, EventArgs e)
        {
            List<CajaRegistradora> lista = new CN_CajaRegistradora().Listar(GlobalSettings.SucursalId);

            CajaRegistradora cajaAbierta = lista.Where(c => c.estado == true).FirstOrDefault();

            if (cajaAbierta == null)
            {
                MessageBox.Show("No hay cajas Abiertas. Por favor abra una caja para poder Registrar una Compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                AbrirFormulario(menuCompras, new frmCompras(usuarioActual),this);
            }



        }

        private void subMenuDetalleCompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuCompras, new frmListadoCompras(),this);
        }

        private void menuClientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmClientes(),this);
        }

        private void menuProveedores_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmProveedores(),this);
        }

        

        private void menuUsuarios_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmUsuarios(),this);
        }

        

        private void subMenuNegocio_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmNegocio(),this);
        }

        private void subMenuReporteCompras_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuReportes, new frmReporteCompras(),this);
        }

        private void subMenuReporteVentas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuReportes, new frmReporteVentas(),this);
        }

      

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro que desea cerrar Sesión?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void subMenuCajaDiaria_Click(object sender, EventArgs e)
        {
            List<CajaRegistradora> lista = new CN_CajaRegistradora().Listar(GlobalSettings.SucursalId);

            CajaRegistradora cajaAbierta = lista.Where(c => c.estado == true).FirstOrDefault();

            if (cajaAbierta == null)
            {
                MessageBox.Show("No hay cajas Abiertas. Por favor abra una caja para poder ver la Caja Diaria", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                AbrirFormulario(menuCajaRegistradora, new frmCajaRegistradora(),this);
            }

           
        }

        private void subMenuAperturaCaja_Click(object sender, EventArgs e)
        {

            List<CajaRegistradora> lista = new CN_CajaRegistradora().Listar(GlobalSettings.SucursalId);

            CajaRegistradora cajaAbierta = lista.Where(c => c.estado == true).FirstOrDefault();

            if (cajaAbierta != null)
            {
                MessageBox.Show("Ya hay una Caja Abierta en curso. Por favor cierre la Caja en curso antes de Abrir otra caja", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                mdAperturacaja mdAperturacaja = new mdAperturacaja();
                mdAperturacaja.ShowDialog();
            }


           
        }

        private void subMenuConsultaCaja_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuCajaRegistradora, new frmDetalleCaja(),this);
        }

        private void cotizacionDolarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmCotizacion(),this);
        }

        private void menuRMA_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmRMA(),this);
        }

        private void menuConsultaStock_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmConsultaStock(),this);
        }

        private void subMenuFormaPago_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmFormaPago(),this);
        }

        private void subMenuStock_Click(object sender, EventArgs e)
        {
            mdCargaStock mdCargaStock = new mdCargaStock();
            mdCargaStock.ShowDialog();
        }

        private void subMenuTraspasoMercaderia_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmOrdenesDeTraspaso(),this);
        }

        private void iconPictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconPictureBox2_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void subMenuConceptos_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuCajaRegistradora, new frmConceptos(),this);
        }

        private void subMenuIngresoService_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuServicioTecnico, new frmServicioTecnico(),this);
        }

        private void subMenuCobroService_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuServicioTecnico, new frmCobrarServicio(),this);
        }

        

        private void subMenuVendedores_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmVendedor(),this);
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

        public void ActualizarSucursal(string nombreSucursal)
        {
            lblSucursal.Text = nombreSucursal;
        }
        private void cboSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSucursal.SelectedIndex != -1)
            {
                // Cerrar el formulario activo si existe
                if (formularioActivo != null)
                {
                    formularioActivo.Close();
                    formularioActivo = null; // Restablecer la referencia del formulario activo
                }

                // Establecer la sucursal seleccionada en los ajustes globales
                GlobalSettings.SucursalId = Convert.ToInt32(((OpcionCombo)cboSucursal.SelectedItem).Valor);
                GlobalSettings.NombreSucursal = ((OpcionCombo)cboSucursal.SelectedItem).Texto;
                lblSucursal.Text = GlobalSettings.NombreSucursal;
            }
        }

        private void subMenuStock2_Click(object sender, EventArgs e)
        {
            mdCargaStock mdCargaStock = new mdCargaStock();
            mdCargaStock.ShowDialog();
        }

        private void subMenuTraspasoStock_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmOrdenesDeTraspaso(), this);
        }

        private void subMenuDeudas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmDeuda(), this);
        }

        private void subMenuProductosSerializados_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario(menuSerializacion, new frmProductosSerializados(), this);
        }

        private void subMenuSerializarProducto_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario(menuSerializacion, new frmDetalleProducto(), this);
        }

        private void subMenuSerialVendidos_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuSerializacion, new frmSerialesVendidos(), this);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuSerializacion, new frmProductosSerializados(), this);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuSerializacion, new frmSerialesVendidos(), this);
        }
    }
}
