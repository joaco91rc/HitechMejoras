﻿using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
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
    public partial class frmRMA : Form
    {
        private Image defaultImage = Properties.Resources.CHECK;
        public frmRMA()
        {
            InitializeComponent();
        }

        private void frmRMA_Load(object sender, EventArgs e)
        {
            
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "DADO DE ALTA EN RMA" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "PROCESO DE RMA COMPLETADO" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 2, Texto = "EN ESPERA DE RESOLUCION RMA" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 3, Texto = "REPARADO POR RMA" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 4, Texto = "NOTA DE CREDITO POR EL RMA" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 5, Texto = "MERCADERIA NO A LA VENTA" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;

            List<ProductoRMA> listaProductosRMA = new CN_ProductoRMA().ListarProductosRMA();

            foreach (ProductoRMA item in listaProductosRMA)
            {
                dgvData.Rows.Add(new object[] { defaultImage,item.idProductoRMA,item.descripcionProductoRMA,
                    item.cantidad.ToString(),
                    item.estado,item.estado,item.fechaIngreso.ToString(), item.fechaEgreso.ToString(),item.idProducto

                    });
            }
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProducto(this))
            {
                var result = modal.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtIdProducto.Text = modal._Producto.idProducto.ToString();

                    txtProducto.Text = modal._Producto.nombre;

                    txtCantidad.Select();
                }
                else
                {
                    txtProducto.Select();
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ProductoRMA objProductoRMA = new ProductoRMA()
            {
                fechaIngreso = dtpFechaIngreso.Value,
                descripcionProductoRMA = txtProducto.Text,
                estado = cboEstado.Text,
                cantidad = Convert.ToInt32(txtCantidad.Value),
                idProducto =Convert.ToInt32(txtIdProducto.Text)

            };


            string mensaje = string.Empty;
            if(txtIdProductoRMA.Text == "0" && txtIndice.Text =="-1")
            {
                int idProductoRMAGenerado = new CN_ProductoRMA().RegistrarProductoXRMA(objProductoRMA, out mensaje);
                if (idProductoRMAGenerado != 0)
                {
                    bool respuesta = new CN_Producto().RestarStockPorRMA(Convert.ToInt32(txtIdProducto.Text), Convert.ToInt32(txtCantidad.Value), GlobalSettings.SucursalId, out mensaje);
                    if (respuesta)
                    {
                        MessageBox.Show("Se ha descontado el producto que esta en RMA del Stock", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    dgvData.Rows.Add(new object[] { defaultImage,idProductoRMAGenerado,txtProducto.Text,txtCantidad.Text, cboEstado.Text,cboEstado.Text,dtpFechaIngreso.Text,null,
                        txtIdProducto.Text });
                    Limpiar();
                }


            }else
            {
                if (GlobalSettings.RolUsuario == 1)
                {
                    bool respuesta2 = new CN_ProductoRMA().EditarProductoXRMA(Convert.ToInt32(txtIdProductoRMA.Text), cboEstado.Text, dtpFechaEgreso.Value, out mensaje);
                    if (respuesta2)
                    {
                        DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                        row.Cells["idProducto"].Value = txtIdProducto.Text;
                        row.Cells["fechaIngreso"].Value = dtpFechaIngreso.Text;
                        row.Cells["descripcionProductoRMA"].Value = txtProducto.Text;
                        row.Cells["cantidad"].Value = txtCantidad.Text;
                        row.Cells["estado"].Value = cboEstado.Text;
                        row.Cells["fechaEgreso"].Value = dtpFechaEgreso.Text;

                        if (objProductoRMA.estado == "PROCESO DE RMA COMPLETADO" || objProductoRMA.estado == "REPARADO POR RMA")
                        {
                            bool actualizarStock = new CN_Producto().SumarStockPorRMA(Convert.ToInt32(txtIdProducto.Text), Convert.ToInt32(txtCantidad.Value), GlobalSettings.SucursalId, out mensaje);
                            if (actualizarStock)
                            {
                                MessageBox.Show("Se ha sumado el producto que esta en RMA al Stock", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }

                        Limpiar();
                        MessageBox.Show("Datos Modificados.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                } else
                {
                    MessageBox.Show("No posee permisos para modificar el Estado del Producto en RMA. Contactese con un Administrador", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
           
        }


        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtIdProducto.Text = "0";
            txtIdProductoRMA.Text = "0";
            txtProductoRMASeleccionado.Text = "";
            txtProducto.Text = "";
            txtCantidad.Value = 1;
            cboEstado.SelectedIndex = -1;
            
        }
        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvData.Columns["btnSeleccionar"].Index)
            {
                int indice = e.RowIndex;
                DataGridViewRow selectedRow = dgvData.Rows[e.RowIndex];
                txtIndice.Text = indice.ToString();
                txtProductoRMASeleccionado.Text = selectedRow.Cells["descripcionProductoRMA"].Value.ToString();
                txtIdProductoRMA.Text = selectedRow.Cells["idProductoRMA"].Value.ToString();
                txtIdProducto.Text = selectedRow.Cells["idProducto"].Value.ToString();
                txtProducto.Text = selectedRow.Cells["descripcionProductoRMA"].Value.ToString();
                txtCantidad.Text = selectedRow.Cells["cantidad"].Value.ToString();
                dtpFechaIngreso.Value = Convert.ToDateTime(selectedRow.Cells["fechaIngreso"].Value);

                string estadoTexto = selectedRow.Cells["estado"].Value.ToString();
                cboEstado.SelectedIndex = cboEstado.FindStringExact(estadoTexto);
            }
        }

       

        private void btnLimpiarDatos_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtIdProductoRMA.Text) != 0)
            {

                if (MessageBox.Show("Desea eliminar el Producto en RMA?", "Confirmar Eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    

                    bool respuesta = new CN_ProductoRMA().EliminarProductoXRMA(Convert.ToInt32(txtIdProductoRMA.Text), out mensaje);

                    if (respuesta)
                    {

                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                        txtIndice.Text = "-1";
                        txtIdProductoRMA.Text = "0";
                    }

                    else
                    {

                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                    }




                }
            }


        }
    }
    }
    

