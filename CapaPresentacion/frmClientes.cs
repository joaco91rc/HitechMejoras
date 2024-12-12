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

namespace CapaPresentacion
{
    public partial class frmClientes : Form
    {
        private Image defaultImage = Properties.Resources.CHECK;
        public frmClientes()
        {
            InitializeComponent();
        }

        
        private void CargarClientes()
        {
            dgvData.Rows.Clear();
            // Mostrar todos los Clientes
            List<Cliente> listaCliente = new CN_Cliente().ListarClientes();

            foreach (Cliente item in listaCliente)
            {
                dgvData.Rows.Add(new object[] {
            defaultImage,
            item.idCliente,
            item.documento,
            item.nombreCompleto,
            item.correo,
            item.direccion,
            item.ciudad,
            item.telefono,
            // Convertir los valores "Si" a true y "No" a false para los CheckBoxColumns
            item.hitech1 == "Si",   // CheckBoxColumn para hitech1
            item.hitech2 == "Si",   // CheckBoxColumn para hitech2
            item.appleStore == "Si",// CheckBoxColumn para appleStore
            item.appleCafe == "Si", // CheckBoxColumn para appleCafe
            item.estado == true ? 1 : 0,
            item.estado == true ? "Activo" : "No Activo"
        });
            }
        }

        private void CargarClientesConcatenados()
        {
            dgvData.Rows.Clear();
            // Mostrar todos los Clientes
            List<Cliente> listaCliente = new CN_Cliente().ListarClientes();

            foreach (Cliente item in listaCliente)
            {
                // Concatenar nombres de locales si el valor es "Si", separados por un guion
                List<string> localesList = new List<string>();
                if (item.hitech1 == "Si") localesList.Add("Hitech1");
                if (item.hitech2 == "Si") localesList.Add("Hitech2");
                if (item.appleStore == "Si") localesList.Add("Apple Store");
                if (item.appleCafe == "Si") localesList.Add("Apple Cafe");

                // Unir la lista de locales con un guion
                string locales = string.Join(" - ", localesList);

                dgvData.Rows.Add(new object[] {
            defaultImage,
            item.idCliente,
            item.documento,
            item.nombreCompleto,
            item.correo,
            item.direccion,
            item.ciudad,
            item.telefono,
            // Agregar la concatenación de los locales a la nueva columna de tipo TextBox
            locales,
            item.estado == true ? 1 : 0,
            item.estado == true ? "Activo" : "No Activo"
        });
            }
        }


        private void CargarClientesIconos()
        {
            dgvData.Rows.Clear();
            // Cargar las imágenes de los íconos (asegúrate de que las imágenes existan en los recursos o en una ruta válida)
            Image greenIcon = Properties.Resources.greenIcon;
            Image redIcon = Properties.Resources.redIcon;

            // Mostrar todos los Clientes
            List<Cliente> listaCliente = new CN_Cliente().ListarClientes();

            foreach (Cliente item in listaCliente)
            {
                // Determinar qué icono asignar para cada local
                Image iconoHitech1 = item.hitech1 == "Si" ? greenIcon : redIcon;
                Image iconoHitech2 = item.hitech2 == "Si" ? greenIcon : redIcon;
                Image iconoAppleStore = item.appleStore == "Si" ? greenIcon : redIcon;
                Image iconoAppleCafe = item.appleCafe == "Si" ? greenIcon : redIcon;

                // Agregar las filas al DataGridView con los iconos correspondientes
                dgvData.Rows.Add(new object[] {
            defaultImage,
            item.idCliente,
            item.documento,
            item.nombreCompleto,
            item.correo,
            item.telefono,
            // Agregar los íconos de pertenencia a los locales en las columnas de tipo imagen
            iconoHitech1,
            iconoHitech2,
            iconoAppleStore,
            iconoAppleCafe,
            item.estado == true ? 1 : 0,
            item.estado == true ? "Activo" : "No Activo"
        });
            }
        }


        private void frmClientes_Load(object sender, EventArgs e)
        {

            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;

            List<Rol> listaRol = new CN_ROL().Listar();

          

            foreach (DataGridViewColumn columna in dgvData.Columns)
            {

                if (columna.Visible == true && columna.Name != "btnSeleccionar")
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });

                }


            }

            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;

            CargarClientesConcatenados();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            List<int> clientesNegocio = new List<int>();
            //AgregarClientesNegocio(clientesNegocio);
            string mensaje = string.Empty;
            Cliente objCliente = new Cliente()
            {
                idCliente = Convert.ToInt32(txtIdCliente.Text),
                documento = txtDocumento.Text,
                nombreCompleto = txtNombreCompleto.Text,
                correo = txtEmail.Text,
                telefono = txtTelefono.Text,
                direccion = txtDireccion.Text,
                ciudad = txtCiudad.Text,
                
                estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false
            };

            if (objCliente.idCliente == 0)
            {

                int idClienteGenerado = new CN_Cliente().Registrar(objCliente, out mensaje);


                if (idClienteGenerado != 0)
                {
                    dgvData.Rows.Add(new object[] { defaultImage,idClienteGenerado,txtDocumento.Text,txtNombreCompleto.Text,txtEmail.Text,txtDireccion.Text,txtCiudad.Text,txtTelefono.Text,

                
                ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString(),
                ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString()
            });
                    foreach (var item in clientesNegocio)
                    {
                        bool asignarCientesASucursal = new CN_ClienteNegocio().AsignarClienteANegocio(idClienteGenerado, item);
                    }

                    Limpiar();
                }
                else
                {

                    MessageBox.Show(mensaje);
                }


            }
            else
            {

                bool resultado = new CN_Cliente().Editar(objCliente, out mensaje);
                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["idCliente"].Value = txtIdCliente.Text;
                    row.Cells["documento"].Value = txtDocumento.Text;
                    row.Cells["nombreCompleto"].Value = txtNombreCompleto.Text;
                    row.Cells["correo"].Value = txtEmail.Text;
                    row.Cells["direccion"].Value = txtDireccion.Text;
                    row.Cells["ciudad"].Value = txtCiudad.Text;
                    row.Cells["telefono"].Value = txtTelefono.Text;
                    
                    row.Cells["estadoValor"].Value = ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString();
                    row.Cells["estado"].Value = ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString();
                    bool modificarClienteASucursal = new CN_ClienteNegocio().ModificarAsignacionNegocios(Convert.ToInt32(txtIdCliente.Text), clientesNegocio);

                    Limpiar();
                    CargarClientes();
                }
                else
                {

                    MessageBox.Show(mensaje);
                }

            }
        }

        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtIdCliente.Text = "0";
            txtDocumento.Text = "";
            txtTelefono.Text = "";
           
            txtEmail.Text = "";
            txtNombreCompleto.Text = "";
            
            cboEstado.SelectedIndex = 0;
            txtDocumento.Select();
            //DesmarcarTodosLosChecks();
        }

       

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;
                //DesmarcarTodosLosChecks();

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtUsuarioSeleccionado.Text = dgvData.Rows[indice].Cells["nombreCompleto"].Value.ToString();
                    txtIdCliente.Text = dgvData.Rows[indice].Cells["idCliente"].Value.ToString();
                    txtDocumento.Text = dgvData.Rows[indice].Cells["documento"].Value.ToString();
                    txtNombreCompleto.Text = dgvData.Rows[indice].Cells["nombreCompleto"].Value.ToString();
                    txtEmail.Text = dgvData.Rows[indice].Cells["correo"].Value.ToString();
                    txtDireccion.Text = dgvData.Rows[indice].Cells["direccion"].Value.ToString();
                    txtCiudad.Text = dgvData.Rows[indice].Cells["ciudad"].Value.ToString();
                    txtTelefono.Text = dgvData.Rows[indice].Cells["telefono"].Value.ToString();
                    

                   

                    foreach (OpcionCombo oc in cboEstado.Items)
                    {

                        if (Convert.ToInt32(oc.Valor) == (Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value)))
                        {
                            int indiceCombo = cboEstado.Items.IndexOf(oc);
                            cboEstado.SelectedIndex = indiceCombo;
                            break;

                        }

                    }

                    //var listaClientesSucursales = new CN_ClienteNegocio().ListarNegociosDeCliente(Convert.ToInt32(txtIdCliente.Text));
                    //foreach (var item in listaClientesSucursales)
                    //{
                    //    switch (item)
                    //    {
                    //        case 1:
                    //            checkHitech1.Checked = true;
                    //            break;
                    //        case 2:
                    //            checkHitech2.Checked = true;
                    //            break;
                    //        case 3:
                    //            checkApple.Checked = true;
                    //            break;
                    //        case 4:
                    //            checkAppleCafe.Checked = true;
                    //            break;
                    //        default:
                    //            // Si hay algún caso que no está cubierto, opcionalmente lo puedes manejar aquí
                    //            break;
                    //    }
                    //}

                }

            }
        }

        //public void DesmarcarTodosLosChecks()
        //{
        //    checkHitech1.Checked = false;
        //    checkHitech2.Checked = false;
        //    checkApple.Checked = false;
        //    checkAppleCafe.Checked = false;
        //}
        private void btnLimpiarDatos_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtIdCliente.Text) != 0)
            {

                if (MessageBox.Show("Desea eliminar el Cliente?", "Confirmar Eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Cliente objCliente = new Cliente()
                    {
                        idCliente = Convert.ToInt32(txtIdCliente.Text),

                    };

                    bool respuesta = new CN_Cliente().Eliminar(objCliente, out mensaje);
                    if (respuesta)
                    {

                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                        Limpiar();
                    }

                    else
                    {

                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                    }




                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();

            if (dgvData.Rows.Count > 0)
            {

                foreach (DataGridViewRow row in dgvData.Rows)
                {

                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;


                }

            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBusqueda.Clear();
            foreach (DataGridViewRow row in dgvData.Rows)
                row.Visible = true;

        }
    }
}
