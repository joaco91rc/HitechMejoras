﻿using CapaEntidad;
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
    public partial class frmProductosSerializados : Form
    {

        private Image defaultImage = Properties.Resources.CHECK;
        private Image defaultImage2 = Properties.Resources.trash;
        public frmProductosSerializados()
        {
            InitializeComponent();
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

        private void CargarGrillaEnStock()
        {
            dgvData.Rows.Clear(); // Limpiar el DataGridView antes de cargar nuevos datos
            var listaProductosSerializados = new CN_Producto().ListarProductosConSerialNumberPorLocalDisponibles(GlobalSettings.SucursalId);
            foreach (ProductoDetalle item in listaProductosSerializados)
            {
                dgvData.Rows.Add(new object[] {
            // Asignar la imagen predeterminada
            item.idProductoDetalle,
            item.idProducto, // Id del producto
            item.codigo,
            item.nombre,
            item.marca, // Marca
            item.modelo, // Modelo
            item.color, // Color
            item.numeroSerie, // Número de serie
            item.nombreLocal,
            item.estadoVendido,

            item.idNegocio, // Id del negocio,
            defaultImage,
            defaultImage2

        });
            }
        }

        private void CargarGrillaEnStockPorLocal(int idNegocio)
        {
            dgvData.Rows.Clear(); // Limpiar el DataGridView antes de cargar nuevos datos
            var listaProductosSerializados = new CN_Producto().ListarProductosConSerialNumberPorLocalDisponibles(idNegocio);
            foreach (ProductoDetalle item in listaProductosSerializados)
            {
                dgvData.Rows.Add(new object[] {
            // Asignar la imagen predeterminada
            item.idProductoDetalle,
            item.idProducto, // Id del producto
            item.codigo,
            item.nombre,
            item.marca, // Marca
            item.modelo, // Modelo
            item.color, // Color
            item.numeroSerie, // Número de serie
            item.nombreLocal,
            item.estadoVendido,

            item.idNegocio, // Id del negocio,
            defaultImage,
            defaultImage2

        });
            }
        }

        private void CargarGrilla()
        {
            dgvData.Rows.Clear(); // Limpiar el DataGridView antes de cargar nuevos datos
            var listaProductosSerializados = new CN_Producto().ListarProductosConSerialNumberEnStockTodosLocales();
            foreach (ProductoDetalle item in listaProductosSerializados)
            {
                dgvData.Rows.Add(new object[] {
            // Asignar la imagen predeterminada
            item.idProductoDetalle,
            item.idProducto, // Id del producto
            item.codigo,
            item.nombre,
            item.marca, // Marca
            item.modelo, // Modelo
            item.color, // Color
            item.numeroSerie,
            item.nombreLocal,// Número de serie
            item.estadoVendido,         
            
            item.idNegocio, // Id del negocio,
            defaultImage,
            defaultImage2
            
        });
            }
        }


        private void frmProductosSerializados_Load(object sender, EventArgs e)
        {

            CargarGrillaEnStock();
            CargarComboBusqueda();
        }


        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string mensaje = string.Empty;
            int indice = e.RowIndex;
            var productoDetalle = new ProductoDetalle
            {
                idProductoDetalle = Convert.ToInt32(dgvData.Rows[indice].Cells["idProductoDetalle"].Value),
                idProducto = Convert.ToInt32(dgvData.Rows[indice].Cells["idProducto"].Value),
                numeroSerie = dgvData.Rows[indice].Cells["serialNumber"].Value.ToString(),
                color = dgvData.Rows[indice].Cells["color"].Value.ToString(),
                modelo = dgvData.Rows[indice].Cells["modelo"].Value.ToString(),
                marca = dgvData.Rows[indice].Cells["marca"].Value.ToString(),
                idNegocio = Convert.ToInt32(dgvData.Rows[indice].Cells["idNegocio"].Value) // Asegúrate de tener esta celda
            };
            if (dgvData.Columns[e.ColumnIndex].Name == "btnEditar")
            {
                

                if (indice >= 0) // Asegurarse de que el índice sea válido
                {
                    // Obtener los datos de la fila seleccionada
                    

                    if (dgvData.Rows[indice].Cells["estado"].Value.ToString() == "EN STOCK")
                    {
                        // Llamar al método para editar el serial number
                        if (GlobalSettings.RolUsuario == 1)
                        {
                            var editar = new CN_Producto().EditarSerialNumber(productoDetalle, out mensaje);
                            if (editar)
                            {
                                // Actualizar la fila en el DataGridView con los nuevos datos
                                dgvData.Rows[indice].Cells["serialNumber"].Value = productoDetalle.numeroSerie;
                                dgvData.Rows[indice].Cells["color"].Value = productoDetalle.color;
                                dgvData.Rows[indice].Cells["modelo"].Value = productoDetalle.modelo;
                                dgvData.Rows[indice].Cells["marca"].Value = productoDetalle.marca;

                                // Cambiar el color de fondo de la fila a verde
                                foreach (DataGridViewCell cell in dgvData.Rows[indice].Cells)
                                {
                                    cell.Style.BackColor = Color.ForestGreen; // Cambia a verde claro o el color que prefieras
                                }

                                MessageBox.Show("Producto Serializado actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show($"Error al actualizar: {mensaje}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        } else
                        {
                            MessageBox.Show("No tiene permisos para editar el Producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar:No se puede actualizar un Producto Vendido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            if (dgvData.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                if (GlobalSettings.RolUsuario == 1)
                {
                    var eliminar = new CN_Producto().EliminarSerialNumber(productoDetalle, out mensaje);

                    if (eliminar)
                    {
                        MessageBox.Show("Productos Serializado Eliminado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }else
                {
                    MessageBox.Show("No tiene permisos para eliminar el Producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

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

        private bool loading = false; // Variable para evitar recursión en la carga

        private void checkProductosEnStock_CheckedChanged(object sender, EventArgs e)
        {
            if (loading) return; // Si estamos cargando, salimos del método

            loading = true; // Iniciamos la carga

            // Desmarcar los otros checkboxes
            checkHitech1.Checked = false;
            checkAppleCafe.Checked = false;
            checkHitech2.Checked = false;
            checkApple49.Checked = false;

            // Cargar grilla en función del estado
            if (checkProductosTodosLocales.Checked)
            {
                CargarGrilla();
            }
            else
            {
                CargarGrillaEnStock();
            }

            loading = false; // Terminamos la carga
        }

        private void checkHitech1_CheckedChanged(object sender, EventArgs e)
        {
            if (loading) return; // Si estamos cargando, salimos del método

            loading = true; // Iniciamos la carga

            // Desmarcar los otros checkboxes
            checkProductosTodosLocales.Checked = false;
            checkAppleCafe.Checked = false;
            checkHitech2.Checked = false;
            checkApple49.Checked = false;

            // Cargar grilla en función del estado
            if (checkHitech1.Checked)
            {
                CargarGrillaEnStockPorLocal(1);
            }
            else
            {
                CargarGrillaEnStock();
            }

            loading = false; // Terminamos la carga
        }

        private void checkHitech2_CheckedChanged(object sender, EventArgs e)
        {
            if (loading) return; // Si estamos cargando, salimos del método

            loading = true; // Iniciamos la carga

            // Desmarcar los otros checkboxes
            checkProductosTodosLocales.Checked = false;
            checkHitech1.Checked = false;
            checkAppleCafe.Checked = false;
            checkApple49.Checked = false;

            // Cargar grilla en función del estado
            if (checkHitech2.Checked)
            {
                CargarGrillaEnStockPorLocal(2);
            }
            else
            {
                CargarGrillaEnStock();
            }

            loading = false; // Terminamos la carga
        }

        private void checkApple49_CheckedChanged(object sender, EventArgs e)
        {
            if (loading) return; // Si estamos cargando, salimos del método

            loading = true; // Iniciamos la carga

            // Desmarcar los otros checkboxes
            checkProductosTodosLocales.Checked = false;
            checkHitech1.Checked = false;
            checkHitech2.Checked = false;
            checkAppleCafe.Checked = false;

            // Cargar grilla en función del estado
            if (checkApple49.Checked)
            {
                CargarGrillaEnStockPorLocal(3);
            }
            else
            {
                CargarGrillaEnStock();
            }

            loading = false; // Terminamos la carga
        }

        private void checkAppleCafe_CheckedChanged(object sender, EventArgs e)
        {
            if (loading) return; // Si estamos cargando, salimos del método

            loading = true; // Iniciamos la carga

            // Desmarcar los otros checkboxes
            checkProductosTodosLocales.Checked = false;
            checkHitech1.Checked = false;
            checkHitech2.Checked = false;
            checkApple49.Checked = false;

            // Cargar grilla en función del estado
            if (checkAppleCafe.Checked)
            {
                CargarGrillaEnStockPorLocal(4);
            }
            else
            {
                CargarGrillaEnStock();
            }

            loading = false; // Terminamos la carga
        }

    }
}
