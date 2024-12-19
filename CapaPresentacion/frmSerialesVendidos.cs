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
    public partial class frmSerialesVendidos : Form
    {
        public frmSerialesVendidos()
        {
            InitializeComponent();
        }
        private void CargarSerialVendidos()
        {
            if (GlobalSettings.RolUsuario != 1)
            {
                dgvData.Columns["idProveedor"].Visible = false; // Oculta la columna idProveedor
                dgvData.Columns["proveedor"].Visible = false; // Oculta la columna NombreProveedor
            }
            dgvData.Rows.Clear(); // Limpiar el DataGridView antes de cargar nuevos datos
            var listaProductosSerializadosVendidos = new CN_Producto().ListarProductosVendidos(GlobalSettings.SucursalId);
            foreach (ProductoDetalle item in listaProductosSerializadosVendidos)
            {
                dgvData.Rows.Add(new object[] {
            // Asignar la imagen predeterminada
            item.idProductoDetalle,
            item.idProducto, // Id del producto
            item.fecha,
            item.fechaEgreso,
            item.codigo,
            item.nombre,
            item.idProveedor,
            item.NombreProveedor,
            item.marca, // Marca
            item.modelo, // Modelo
            item.color, // Color
            item.numeroSerie, // Número de serie
                     
            
            item.idNegocio,// Id del negocio,
            item.idVenta,
            item.numeroVenta,
            item.nombreLocal
            

        });

            }
        }

        private void CargarSerialVendidosTodosLocales()
        {
            if (GlobalSettings.RolUsuario != 1)
            {
                dgvData.Columns["idProveedor"].Visible = false; // Oculta la columna idProveedor
                dgvData.Columns["proveedor"].Visible = false; // Oculta la columna NombreProveedor
            }
            dgvData.Rows.Clear(); // Limpiar el DataGridView antes de cargar nuevos datos
            var listaProductosSerializadosVendidos = new CN_Producto().ListarProductosVendidosTodosLocales();
            foreach (ProductoDetalle item in listaProductosSerializadosVendidos)
            {
                dgvData.Rows.Add(new object[] {
            // Asignar la imagen predeterminada
            item.idProductoDetalle,
            item.idProducto, // Id del producto
            item.fecha,
            item.fechaEgreso,
            item.codigo,
            item.nombre,
            item.idProveedor,
            item.NombreProveedor,
            item.marca, // Marca
            item.modelo, // Modelo
            item.color, // Color
            item.numeroSerie, // Número de serie
                     
            
            item.idNegocio,// Id del negocio,
            item.idVenta,
            item.numeroVenta,
            item.nombreLocal


        });

            }
        }

        private void CargarComboBusqueda()
        {
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {

                if (columna.Visible == true && columna.Name != "btnSeleccionar")
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });

                }


            }

            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 1;
        }
        private void frmSerialesVendidos_Load(object sender, EventArgs e)
        {
            CargarSerialVendidos();
            CargarComboBusqueda();
        }

        private void checkProductosTodosLocales_CheckedChanged(object sender, EventArgs e)
        {
            if (checkProductosTodosLocales.Checked)
            {
                CargarSerialVendidosTodosLocales();
            } else
            {
                CargarSerialVendidos();
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

        private void btnFiltrarFechas_Click(object sender, EventArgs e)
        {
            // Validar que el rango de fechas sea correcto
            if (dtpInicio.Value > dtpFin.Value)
            {
                MessageBox.Show("La fecha de inicio no puede ser mayor a la fecha de fin.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime fechaInicio = dtpInicio.Value.Date;
            DateTime fechaFin = dtpFin.Value.Date;

            // Verificar si el DataGridView tiene filas
            if (dgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    // Validar si la columna fechaEgreso contiene un valor válido
                    if (row.Cells["fechaEgreso"].Value != null && DateTime.TryParse(row.Cells["fechaEgreso"].Value.ToString(), out DateTime fechaEgreso))
                    {
                        // Filtrar las filas que están dentro del rango de fechas
                        row.Visible = fechaEgreso.Date >= fechaInicio && fechaEgreso.Date <= fechaFin;
                    }
                    else
                    {
                        // Ocultar filas si la fecha es inválida o nula
                        row.Visible = false;
                    }
                }
            }
        }

        private void btnLimpiarRangoFechas_Click(object sender, EventArgs e)
        {
            // Restablecer los valores de los DateTimePicker
            dtpInicio.Value = DateTime.Now.Date;
            dtpFin.Value = DateTime.Now.Date;

            // Verificar si el DataGridView tiene filas
            if (dgvData.Rows.Count > 0)
            {
                // Hacer visibles todas las filas
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    row.Visible = true;
                }
            }

            // Opcional: Restablecer el foco en algún control o mostrar un mensaje
            txtBusqueda.Clear();
            txtBusqueda.Focus();
        }

    }
}
