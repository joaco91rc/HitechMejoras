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
    public partial class frmConceptos : Form
    {
        public frmConceptos()
        {
            InitializeComponent();
        }

        private void frmConceptos_Load(object sender, EventArgs e)
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

            //Mostrar todos los usuarios
            List<Concepto> listaConceptos = new CN_Concepto().Listar();

            foreach (Concepto item in listaConceptos)
            {
                dgvData.Rows.Add(new object[] { "",item.idConcepto,
                    item.descripcion,
                    item.estado==true?1:0,
                    item.estado==true? "Activo": "No Activo"
                    });
            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            Concepto objConcepto = new Concepto()
            {
                idConcepto = Convert.ToInt32(txtIdConcepto.Text),
                descripcion = txtDescripcion.Text,
                estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false,
                fechaRegistro = DateTime.Now.Date
            };

            if (objConcepto.idConcepto == 0)
            {

                int idConceptoGenerado = new CN_Concepto().Registrar(objConcepto, out mensaje);


                if (idConceptoGenerado != 0)
                {
                    dgvData.Rows.Add(new object[] { "",idConceptoGenerado,txtDescripcion.Text,
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

                bool resultado = new CN_Concepto().Editar(objConcepto, out mensaje);
                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["idConcepto"].Value = txtIdConcepto.Text;
                    row.Cells["descripcion"].Value = txtDescripcion.Text;
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

        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtIdConcepto.Text = "0";
            txtDescripcion.Text = "";
            cboEstado.SelectedIndex = 0;
            txtDescripcion.Select();
        }

        private void btnLimpiarDatos_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtIdConcepto.Text) != 0)
            {

                if (MessageBox.Show("Desea eliminar el Concepto?", "Confirmar Eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Concepto objConcepto = new Concepto()
                    {
                        idConcepto = Convert.ToInt32(txtIdConcepto.Text),

                    };

                    bool respuesta = new CN_Concepto().Eliminar(objConcepto, out mensaje);
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
                    txtCategoriaSeleccionado.Text = dgvData.Rows[indice].Cells["descripcion"].Value.ToString();
                    txtIdConcepto.Text = dgvData.Rows[indice].Cells["idConcepto"].Value.ToString();
                    txtDescripcion.Text = dgvData.Rows[indice].Cells["descripcion"].Value.ToString();




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

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex == 0)
            {

                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.check20.Width;
                var h = Properties.Resources.check20.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Width - h) / 2;
                e.Graphics.DrawImage(Properties.Resources.check20, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }
    }
}
