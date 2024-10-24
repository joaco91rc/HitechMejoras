using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaPresentacion.Utilidades;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class frmUsuarios : Form
    {
        private Image defaultImage = Properties.Resources.CHECK;
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {

            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;

            List<Rol> listaRol = new CN_ROL().Listar();

            foreach (Rol item in listaRol)
            {
                cboRol.Items.Add(new OpcionCombo() { Valor = item.idRol, Texto = item.descripcion });
            }
            cboRol.DisplayMember = "Texto";
            cboRol.ValueMember = "Valor";
            cboRol.SelectedIndex = 0;

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

            //Mostrar todos los usuarios
            List<Usuario> listaUsuario = new CN_Usuario().Listar();

            foreach (Usuario item in listaUsuario)
            {
                dgvData.Rows.Add(new object[] { defaultImage,item.idUsuario,item.documento,item.nombreCompleto,item.correo,item.clave,item.oRol.idRol,item.oRol.descripcion,

                    item.estado==true?1:0,
                    item.estado==true? "Activo": "No Activo"
                    });
            }




        }

        public void AgregarPermisosNegocio(List<int> permisosNegocio)
        {
            // Limpiar la lista antes de agregar nuevos permisos
            permisosNegocio.Clear();

            // Agregar los permisos basados en los checkboxes seleccionados
            if (checkHitech1.Checked)
            {
                permisosNegocio.Add(1); // ID del negocio para el primer local
            }
            if (checkHitech2.Checked)
            {
                permisosNegocio.Add(2); // ID del negocio para el segundo local
            }
            if (checkApple.Checked)
            {
                permisosNegocio.Add(3); // ID del negocio para el tercer local
            }
            if (checkAppleCafe.Checked)
            {
                permisosNegocio.Add(4); // ID del negocio para el cuarto local
            }

            // Verificar si la lista está vacía y mostrar mensaje
            if (permisosNegocio.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos un Permiso de Acceso a una Sucursal.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            List<int> permisosNegocio = new List<int>();
            AgregarPermisosNegocio(permisosNegocio);
            string mensaje = string.Empty;
            Usuario objUsuario = new Usuario()
            {
                idUsuario = Convert.ToInt32(txtIdUsuario.Text),
                documento = txtDocumento.Text,
                nombreCompleto = txtNombreCompleto.Text,
                correo = txtEmail.Text,
                clave =txtClave.Text,
                oRol = new Rol { idRol = Convert.ToInt32(((OpcionCombo)cboRol.SelectedItem).Valor)},
                estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1? true : false
            };

            if (objUsuario.idUsuario == 0)
            {

                int idUsuarioGenerado = new CN_Usuario().Registrar(objUsuario, out mensaje);


                if (idUsuarioGenerado != 0)
                {
                    dgvData.Rows.Add(new object[] { defaultImage,idUsuarioGenerado,txtDocumento.Text,txtNombreCompleto.Text,txtEmail.Text,txtClave.Text,

                ((OpcionCombo)cboRol.SelectedItem).Valor.ToString(),
                ((OpcionCombo)cboRol.SelectedItem).Texto.ToString(),
                ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString(),
                ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString()
            });
                    foreach (var item in permisosNegocio)
                    {
                        bool asignarPermisos = new CN_AccesoNegocio().AsignarPermiso(idUsuarioGenerado, item);
                    }

                    Limpiar();
                }
                else
                {

                    MessageBox.Show(mensaje);
                }


            }
            else { 
            
                bool resultado = new CN_Usuario().Editar(objUsuario, out mensaje);
                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["idusuario"].Value = txtIdUsuario.Text;
                    row.Cells["documento"].Value = txtDocumento.Text;
                    row.Cells["nombreCompleto"].Value = txtNombreCompleto.Text;
                    row.Cells["correo"].Value = txtEmail.Text;
                    row.Cells["clave"].Value = txtClave.Text;
                    row.Cells["idRol"].Value = ((OpcionCombo)cboRol.SelectedItem).Valor.ToString();
                    row.Cells["rol"].Value = ((OpcionCombo)cboRol.SelectedItem).Texto.ToString();
                    row.Cells["estadoValor"].Value = ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString();
                    row.Cells["estado"].Value = ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString();
                   
                    bool modificarPermisos = new CN_AccesoNegocio().ModificarPermisos(Convert.ToInt32(txtIdUsuario.Text), permisosNegocio);
                    


                    Limpiar();
                    MessageBox.Show("Datos Modificados.", "Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    

                }
                else {

                    MessageBox.Show(mensaje);
                }

            }

            

            
        }

        private void Limpiar() {
            txtIndice.Text = "-1";
            txtIdUsuario.Text = "0";
            txtDocumento.Text = "";
            txtClave.Text = "";
            txtConfirmarClave.Text = "";
            txtEmail.Text = "";
            txtNombreCompleto.Text = "";
            cboRol.SelectedIndex = 0;
            cboEstado.SelectedIndex = 0;
            txtDocumento.Select();
            DesmarcarTodosLosChecks();
        }

       

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar") {
                int indice = e.RowIndex;
                DesmarcarTodosLosChecks();
                if (indice >= 0) {
                    txtIndice.Text = indice.ToString();
                    txtUsuarioSeleccionado.Text = dgvData.Rows[indice].Cells["nombreCompleto"].Value.ToString();
                    txtIdUsuario.Text = dgvData.Rows[indice].Cells["idUsuario"].Value.ToString();
                    txtDocumento.Text = dgvData.Rows[indice].Cells["documento"].Value.ToString();
                    txtNombreCompleto.Text = dgvData.Rows[indice].Cells["nombreCompleto"].Value.ToString();
                    txtEmail.Text = dgvData.Rows[indice].Cells["correo"].Value.ToString();
                    txtClave.Text = dgvData.Rows[indice].Cells["clave"].Value.ToString();
                    txtConfirmarClave.Text = dgvData.Rows[indice].Cells["clave"].Value.ToString();

                    foreach (OpcionCombo oc in cboRol.Items) {

                        if (Convert.ToInt32(oc.Valor) == (Convert.ToInt32(dgvData.Rows[indice].Cells["idRol"].Value)))
                        {
                            int indiceCombo = cboRol.Items.IndexOf(oc);
                            cboRol.SelectedIndex = indiceCombo;
                            break;

                        }

                    }

                    foreach (OpcionCombo oc in cboEstado.Items)
                    {

                        if (Convert.ToInt32(oc.Valor) == (Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value)))
                        {
                            int indiceCombo = cboEstado.Items.IndexOf(oc);
                            cboEstado.SelectedIndex = indiceCombo;
                            break;

                        }

                    }

                    var listaPermisosUsuario = new CN_AccesoNegocio().ListarNegociosPermitidos(Convert.ToInt32(txtIdUsuario.Text));
                    foreach(var item in listaPermisosUsuario)
                    {
                        switch (item)
                        {
                            case 1:
                                checkHitech1.Checked = true;
                                break;
                            case 2:
                                checkHitech2.Checked = true;
                                break;
                            case 3:
                                checkApple.Checked = true;
                                break;
                            case 4:
                                checkAppleCafe.Checked = true;
                                break;
                            default:
                                // Si hay algún caso que no está cubierto, opcionalmente lo puedes manejar aquí
                                break;
                        }
                    }
                }
            
            }
        }

        public void DesmarcarTodosLosChecks()
        {
            checkHitech1.Checked = false;
            checkHitech2.Checked = false;
            checkApple.Checked = false;
            checkAppleCafe.Checked = false;
        }


        private void btnLimpiarDatos_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtIdUsuario.Text) != 0) {

                if (MessageBox.Show("Desea eliminar el usuario?", "Confirmar Eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    string mensaje = string.Empty;
                    Usuario objUsuario = new Usuario()
                    {
                        idUsuario = Convert.ToInt32(txtIdUsuario.Text),
                        
                    };
                    bool eliminarPermisos = new CN_AccesoNegocio().EliminarPermisos(objUsuario.idUsuario);
                    if (eliminarPermisos)
                    {
                        bool respuesta = new CN_Usuario().Eliminar(objUsuario, out mensaje);

                        if (respuesta)
                        {

                            dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                            txtIndice.Text = "-1";
                            txtIdUsuario.Text = "0";
                            Limpiar();
                        }

                        else
                        {

                            MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                        }
                    }
                    
                    
                    
                    
                    
                    }
                }
            
            
            }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();

            if(dgvData.Rows.Count > 0)
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
            foreach(DataGridViewRow row in dgvData.Rows)
              row.Visible = true;
            
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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
        }
    }
    }

