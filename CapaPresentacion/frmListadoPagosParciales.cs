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
    public partial class frmListadoPagosParciales : Form
    {
        public frmListadoPagosParciales()
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
        private void CargarPagosParcialesPorLocal()
        {
            dgvData.Rows.Clear();
            // Mostrar todos los Clientes
            List<PagoParcial> listaPagoParciales = new CN_PagoParcial().ListarPagosParcialesPorLocal(GlobalSettings.SucursalId);

            foreach (PagoParcial item in listaPagoParciales)
            {



                dgvData.Rows.Add(new object[] {
            
            item.idPagoParcial,
            item.fechaRegistro,
            item.idCliente,
            item.nombreCliente,
            item.productoReservado,
            item.formaPago,
            item.moneda=="PESOS"?"ARS " + item.monto:"USD " + item.monto,
            item.moneda,
            item.idVenta,
            item.numeroVenta,
            item.vendedor,
            item.estado == true ? 1 : 0,
            item.estado == true ? "ACTIVO" : "UTILIZADO",
            item.nombreLocal,
            item.idNegocio
        });
            }



        }

        private void CargarPagosParcialesActivos()
        {
            dgvData.Rows.Clear();
            // Obtener la lista de pagos activos
            List<PagoParcial> listaPagosActivos = new CN_PagoParcial().ListarPagosParcialesActivos(GlobalSettings.SucursalId);

            foreach (PagoParcial item in listaPagosActivos)
            {
                dgvData.Rows.Add(new object[] {
            item.idPagoParcial,
            item.fechaRegistro,
            item.idCliente,
            item.nombreCliente,
            item.productoReservado,
            item.formaPago,
            item.moneda=="PESOS"?"ARS " + item.monto:"USD " + item.monto,
            item.moneda,
            item.idVenta,
            item.numeroVenta,
            item.vendedor,
            1, // Estado activo
            "ACTIVO",
            item.nombreLocal,
            item.idNegocio
        });
            }
        }

        private void CargarPagosParcialesInactivos()
        {
            dgvData.Rows.Clear();
            // Obtener la lista de pagos inactivos
            List<PagoParcial> listaPagosInactivos = new CN_PagoParcial().ListarPagosParcialesInactivos(GlobalSettings.SucursalId);

            foreach (PagoParcial item in listaPagosInactivos)
            {
                dgvData.Rows.Add(new object[] {
            item.idPagoParcial,
            item.fechaRegistro,
            item.idCliente,
            item.nombreCliente,
            item.productoReservado,
            item.formaPago,
            item.moneda=="PESOS"?"ARS " + item.monto:"USD " + item.monto,
            item.moneda,
            item.idVenta,
            item.numeroVenta,
            item.vendedor,
            0, // Estado inactivo
            "UTILIZADO",
            item.nombreLocal,
            item.idNegocio
        });
            }
        }


        private void CargarPagosParciales()
        {
            dgvData.Rows.Clear();
            // Mostrar todos los Clientes
            List<PagoParcial> listaPagoParciales = new CN_PagoParcial().Listar();

            foreach (PagoParcial item in listaPagoParciales)
            {



                dgvData.Rows.Add(new object[] {
            
            item.idPagoParcial,
            item.fechaRegistro,
            item.idCliente,
            item.nombreCliente,
            item.productoReservado,
            item.formaPago,
            item.moneda=="PESOS"?"ARS " + item.monto:"USD " + item.monto,
            item.moneda,
            item.idVenta,
            item.numeroVenta,
            item.vendedor,
            item.estado == true ? 1 : 0,
            item.estado == true ? "ACTIVO" : "UTILIZADO",
            item.nombreLocal,
            item.idNegocio
        });
            }



        }
        private void frmListadoPagosParciales_Load(object sender, EventArgs e)
        {
            CargarComboBusqueda();
            CargarPagosParcialesPorLocal();
        }

        private void checkMostrarTodosPagosParciales_CheckedChanged(object sender, EventArgs e)
        {
            if (checkMostrarTodosPagosParciales.Checked) { CargarPagosParciales(); } else { CargarPagosParcialesPorLocal(); }
        }

        private void checkpagoParcialesActivos_CheckedChanged(object sender, EventArgs e)
        {
            CargarPagosParcialesActivos();
        }

        private void checkPagosParcialesUtilizados_CheckedChanged(object sender, EventArgs e)
        {
            CargarPagosParcialesInactivos();
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

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            // Verifica si la tecla presionada es Enter
            if (e.KeyCode == Keys.Enter)
            {
                string columnaFiltro = ((OpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();

                if (dgvData.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvData.Rows)
                    {
                        // Aplica el filtro de búsqueda
                        if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Text.Trim().ToUpper()))
                            row.Visible = true;
                        else
                            row.Visible = false;
                    }
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
