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
    public partial class frmVendedor : Form
    {
        private Image defaultImage = Properties.Resources.CHECK;
        public frmVendedor()
        {
            InitializeComponent();
        }

        private void CargarVendedores() {

            //Mostrar todos los Venddores
            List<Vendedor> listaVendedores = new CN_Vendedor().ListarVendedores();

            foreach (Vendedor item in listaVendedores)
            {
                dgvData.Rows.Add(new object[] { defaultImage,item.idVendedor,item.DNI,item.nombre,item.apellido,item.sueldoBase,item.sueldoComision,

                    item.estado==true?1:0,
                    item.estado==true? "Activo": "No Activo"
                    });
            }
        }

        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtIdVendedor.Text = "0";
            txtDocumento.Text = "";
            txtSueldoBase.Value = 0;

            txtApellido.Text = "";
            txtNombre.Text = "";
            txtSueldoComision.Value = 0;

            cboEstado.SelectedIndex = 0;
            txtDocumento.Select();
        }
        private void frmVendedor_Load(object sender, EventArgs e)
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
            CargarVendedores();
           
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            Vendedor objVendedor = new Vendedor()
            {
                idVendedor = Convert.ToInt32(txtIdVendedor.Text),
                DNI = txtDocumento.Text,
                nombre = txtNombre.Text,
                apellido = txtApellido.Text,
                sueldoBase = txtSueldoBase.Value,
                sueldoComision = txtSueldoComision.Value,

                estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false
            };

            if (objVendedor.idVendedor == 0)
            {

                int idVendedorGenerado = new CN_Vendedor().RegistrarVendedor(objVendedor, out mensaje);


                if (idVendedorGenerado != 0)
                {
                    dgvData.Rows.Add(new object[] { defaultImage,idVendedor,txtDocumento.Text,txtNombre.Text,txtApellido.Text,txtSueldoBase.Value,txtSueldoComision.Value,


                ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString(),
                ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString()
            });
                    Limpiar();
                }
                else
                {

                    MessageBox.Show(mensaje);
                }


            }
            else
            {

                bool resultado = new CN_Vendedor().EditarVendedor(objVendedor, out mensaje);
                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["idVendedor"].Value = txtIdVendedor.Text;
                    row.Cells["DNI"].Value = txtDocumento.Text;
                    row.Cells["nombre"].Value = txtNombre.Text;
                    row.Cells["apellido"].Value = txtApellido.Text;
                    row.Cells["sueldoBase"].Value = txtSueldoBase.Text;
                    row.Cells["sueldoComision"].Value = txtSueldoComision.Text;

                    row.Cells["estadoValor"].Value = ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString();
                    row.Cells["estado"].Value = ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString();
                    Limpiar();

                }
                else
                {

                    MessageBox.Show(mensaje);
                }

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtIdVendedor.Text) != 0)
            {

                if (MessageBox.Show("Desea eliminar el Vendedor?", "Confirmar Eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Vendedor objVendedor = new Vendedor()
                    {
                        idVendedor = Convert.ToInt32(txtIdVendedor.Text),

                    };

                    bool respuesta = new CN_Vendedor().EliminarVendedor(objVendedor, out mensaje);
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

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtVendedorSeleccionado.Text = dgvData.Rows[indice].Cells["nombre"].Value.ToString() + dgvData.Rows[indice].Cells["apellido"].Value.ToString();
                    txtIdVendedor.Text = dgvData.Rows[indice].Cells["idVendedor"].Value.ToString();
                    txtDocumento.Text = dgvData.Rows[indice].Cells["DNI"].Value.ToString();
                    txtNombre.Text = dgvData.Rows[indice].Cells["nombre"].Value.ToString();
                    txtApellido.Text = dgvData.Rows[indice].Cells["apellido"].Value.ToString();
                    txtSueldoBase.Text = dgvData.Rows[indice].Cells["sueldoBase"].Value.ToString();
                    txtSueldoComision.Text = dgvData.Rows[indice].Cells["sueldoComision"].Value.ToString();




                    foreach (OpcionCombo oc in cboEstado.Items)
                    {

                        if (Convert.ToInt32(oc.Valor) == (Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value)))
                        {
                            int indiceCombo = cboEstado.Items.IndexOf(oc);
                            cboEstado.SelectedIndex = indiceCombo;
                            break;

                        }

                    }

                }

            }
        }
    }
}
