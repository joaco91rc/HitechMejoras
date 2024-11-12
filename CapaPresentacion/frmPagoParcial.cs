using CapaEntidad;
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
    public partial class frmPagoParcial : Form
    {
        public frmPagoParcial()
        {
            InitializeComponent();
        }
        private Image defaultImage = Properties.Resources.CHECK;

        private void CargarComboBoxVendedores()
        {
            // Crear una instancia de la capa de negocio para vendedores
            CN_Vendedor objCN_Vendedor = new CN_Vendedor();

            // Obtener la lista de vendedores desde la base de datos
            List<Vendedor> listaVendedores = objCN_Vendedor.ListarVendedores();

            // Limpiar los items actuales del ComboBox
            cboVendedores.Items.Clear();

            // Llenar el ComboBox con los datos obtenidos
            foreach (Vendedor vendedor in listaVendedores)
            {
                cboVendedores.Items.Add(new OpcionCombo() { Valor = vendedor.idVendedor, Texto = $"{vendedor.nombre} {vendedor.apellido}" });
            }

            // Establecer DisplayMember y ValueMember
            cboVendedores.DisplayMember = "Texto";
            cboVendedores.ValueMember = "Valor";

            // Seleccionar el primer item por defecto si hay elementos en el ComboBox
            if (cboVendedores.Items.Count > 0)
            {
                cboVendedores.SelectedIndex = -1; // O puedes poner `0` si deseas seleccionar el primer item
            }
        }
        private void CargarComboBoxFormaPago()
        {
            // Crear una instancia de la capa de negocio
            CN_FormaPago objCN_FormaPago = new CN_FormaPago();

            // Obtener la lista de formas de pago desde la base de datos
            List<FormaPago> listaFormaPago = objCN_FormaPago.ListarFormasDePago();

            // Limpiar los items actuales del ComboBox
            cboFormaPago.Items.Clear();
            

            // Llenar el ComboBox con los datos obtenidos
            foreach (FormaPago formaPago in listaFormaPago)
            {
                cboFormaPago.Items.Add(new OpcionCombo() { Valor = formaPago.idFormaPago, Texto = formaPago.descripcion });
                
            }

            // Establecer DisplayMember y ValueMember
            cboFormaPago.DisplayMember = "Texto";
            cboFormaPago.ValueMember = "Valor";
            

            // Seleccionar el primer item por defecto si hay elementos en el ComboBox
            if (cboFormaPago.Items.Count > 0)
            {
                cboFormaPago.SelectedIndex = -1;
                
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
            defaultImage,
            item.idPagoParcial,
            item.fechaRegistro,
            item.idCliente,
            item.nombreCliente,
            item.productoReservado,
            item.formaPago,
            item.monto,
            item.moneda,
            item.idVenta,
            item.numeroVenta,
            item.vendedor,
            item.estado == true ? 1 : 0,
            item.estado == true ? "Pendiente" : "Usada",
            item.nombreLocal,
            item.idNegocio
        });
            }



        }

        private void CargarPagosParcialesPorLocal()
        {
            dgvData.Rows.Clear();
            // Mostrar todos los Clientes
            List<PagoParcial> listaPagoParciales = new CN_PagoParcial().ListarPagosParcialesPorLocal(GlobalSettings.SucursalId);

            foreach (PagoParcial item in listaPagoParciales)
            {



                dgvData.Rows.Add(new object[] {
            defaultImage,
            item.idPagoParcial,
            item.fechaRegistro,
            item.idCliente,
            item.nombreCliente,
            item.productoReservado,
            item.formaPago,
            item.monto,
            item.moneda,
            item.idVenta,
            item.numeroVenta,
            item.vendedor,
            item.estado == true ? 1 : 0,
            item.estado == true ? "Pendiente" : "Usada",
            item.nombreLocal,
            item.idNegocio
        });
            }



        }
        private void frmPagoParcial_Load(object sender, EventArgs e)
        {

            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Pendiente" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Usada" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;
            CargarPagosParcialesPorLocal();
            CargarComboBoxFormaPago();
            CargarComboBoxVendedores();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            using (var modal = new mdCliente())
            {
                var result = modal.ShowDialog();
                if (result == DialogResult.OK)
                {

                    txtCliente.Text = modal._Cliente.nombreCompleto;
                    txtIdCliente.Text = modal._Cliente.idCliente.ToString();


                }
                else
                {
                    txtCliente.Select();
                }
            }
        }

        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtIdCliente.Text = "0";
            txtIdPagoParcial.Text = "0";
            txtCliente.Text = string.Empty;
            txtMonto.Value = 0;
            cboFormaPago.SelectedIndex = -1;
            dtpFecha.Value = DateTime.Now;
            cboEstado.SelectedIndex = 0;
            txtCliente.Select();
            cboVendedores.SelectedIndex = -1;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            
            PagoParcial objPagoParcial = CrearPagoParcial();

            decimal montoPagado = CalcularMontoPagado(objPagoParcial);

            if (objPagoParcial.idPagoParcial == 0)
            {
                // Registro de nuevo PagoParcial
                GuardarNuevoPagoParcial(objPagoParcial, montoPagado, out mensaje);
            }
            else
            {
                // Edición de PagoParcial existente
                ModificarPagoParcial(objPagoParcial, out mensaje);
            }
        }

        private PagoParcial CrearPagoParcial()
        {
            return new PagoParcial()
            {
                idPagoParcial = Convert.ToInt32(txtIdPagoParcial.Text),
                idCliente = Convert.ToInt32(txtIdCliente.Text),
                monto = txtMonto.Value,
                formaPago = cboFormaPago.Text,
                estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1,
                idVenta = null,
                productoReservado = txtProductoReservado.Text,
                fechaRegistro = dtpFecha.Value,
                vendedor = cboVendedores.Text,
                idNegocio = GlobalSettings.SucursalId,
                moneda =checkDolares.Checked?"DOLARES":"PESOS" 
            };
        }

        private decimal CalcularMontoPagado(PagoParcial objPagoParcial)
        {
            if (cboFormaPago.SelectedItem == null || txtMonto.Value == 0) return 0;

            FormaPago fp1 = new CN_FormaPago().ObtenerFPPorDescripcion(((OpcionCombo)cboFormaPago.SelectedItem).Texto);
            return Convert.ToDecimal(txtMonto.Text) - (Convert.ToDecimal(txtMonto.Text) * fp1.porcentajeRetencion) / 100;
        }

        private void GuardarNuevoPagoParcial(PagoParcial objPagoParcial, decimal montoPagado, out string mensaje)
        {
            int idPagoParcialGenerado = new CN_PagoParcial().RegistrarPagoParcial(objPagoParcial, out mensaje);
            if (idPagoParcialGenerado != 0)
            {
                AgregarFilaDataGrid(idPagoParcialGenerado);
                RealizarTransaccionCaja(montoPagado, idPagoParcialGenerado);
                Limpiar();
            }
            else
            {
                MessageBox.Show(mensaje);
            }
        }

        private void ModificarPagoParcial(PagoParcial objPagoParcial, out string mensaje)
        {
            bool resultado = new CN_PagoParcial().ModificarPagoParcial(objPagoParcial, out mensaje);
            if (resultado)
            {
                ActualizarFilaDataGrid();

                // Crea el objeto TransaccionCaja para la edición
                TransaccionCaja objTransaccion = CrearTransaccionCaja(objPagoParcial);

                // Pasa el objeto TransaccionCaja al método EditarMovimiento
                bool editarMovimientoCaja = new CN_Transaccion().EditarMovimiento(objTransaccion, out mensaje);
                if (editarMovimientoCaja)
                {
                    MessageBox.Show("Movimiento en Caja Modificado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo Modificar el movimiento en la Caja Registradora", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Limpiar();
                CargarPagosParciales();
            }
            else
            {
                MessageBox.Show(mensaje);
            }
        }

        private TransaccionCaja CrearTransaccionCaja(PagoParcial objPagoParcial)
        {
            List<CajaRegistradora> lista = new CN_CajaRegistradora().Listar(GlobalSettings.SucursalId);
            CajaRegistradora cajaAbierta = lista.FirstOrDefault(c => c.estado == true);
            var cajaAsociada = new CN_FormaPago().ObtenerFPPorDescripcion(cboFormaPago.Text).cajaAsociada;
            int? idTransaccion = new CN_Transaccion().ObtenerIdTransaccionPorIdPagoParcial(Convert.ToInt32(txtIdPagoParcial.Text));
            // Aquí asumo que tienes la lógica para construir el objeto TransaccionCaja
            // según los datos de objPagoParcial o de otras variables necesarias.
            return new TransaccionCaja()
            {
                // Asigna los valores necesarios a la transacción aquí
                // Por ejemplo:
                idTransaccion = idTransaccion.HasValue ? idTransaccion.Value : 0,
                idCajaRegistradora = cajaAbierta.idCajaRegistradora,
                hora = dtpFecha.Value.Hour.ToString(),
                tipoTransaccion = "ENTRADA",
                monto = CalcularMontoPagado(objPagoParcial), // Usa el método para calcular el monto
                docAsociado = "Pago Parcial Numero: " + objPagoParcial.idPagoParcial + " Cliente: " + txtCliente.Text,
                usuarioTransaccion = cboVendedores.Text,
                formaPago = cboFormaPago.Text,
                idVenta = null,
                idCompra = null,
                idPagoParcial = objPagoParcial.idPagoParcial,
                idNegocio = GlobalSettings.SucursalId,
                concepto = "PAGO PARCIAL",
                cajaAsociada = cajaAsociada
            };
        }


        private void AgregarFilaDataGrid(int idPagoParcialGenerado)
        {
            int idNegocio = GlobalSettings.SucursalId;
            string nombreLocal;

            // Asigna el nombre del local en función de idNegocio
            if (idNegocio == 1)
                nombreLocal = "HITECH 1";
            else if (idNegocio == 2)
                nombreLocal = "HITECH 2";
            else if (idNegocio == 3)
                nombreLocal = "APPLE 49";
            else if (idNegocio == 4)
                nombreLocal = "APPLE CAFE";
            else
                nombreLocal = "";

            dgvData.Rows.Add(new object[] {
        defaultImage,
        idPagoParcialGenerado,
        dtpFecha.Value,
        txtIdCliente.Text,
        txtCliente.Text,
        txtProductoReservado.Text,
        cboFormaPago.Text,
        txtMonto.Value,
        checkDolares.Checked?"DOLARES":"PESOS",
        "",
        "",
        cboVendedores.Text,
        ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString(),
        ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString(),
         nombreLocal,
        idNegocio        
       
    });
        }


        private void ActualizarFilaDataGrid()
        {
            int idNegocio = GlobalSettings.SucursalId;
            string nombreLocal;

            // Asigna el nombre del local en función de idNegocio
            if (idNegocio == 1)
                nombreLocal = "HITECH 1";
            else if (idNegocio == 2)
                nombreLocal = "HITECH 2";
            else if (idNegocio == 3)
                nombreLocal = "APPLE 49";
            else if (idNegocio == 4)
                nombreLocal = "APPLE CAFE";
            else
                nombreLocal = "";

            DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
            row.Cells["idPagoParcial"].Value = txtIdPagoParcial.Text;
            row.Cells["idCliente"].Value = txtIdCliente.Text;
            row.Cells["nombreCompleto"].Value = txtCliente.Text;
            row.Cells["formaPago"].Value = cboFormaPago.Text;
            row.Cells["monto"].Value = txtMonto.Value;
            row.Cells["productoReservado"].Value = txtProductoReservado.Text;
            row.Cells["idVenta"].Value = "";
            row.Cells["numeroVenta"].Value = "";
            row.Cells["vendedor"].Value = cboVendedores.Text;
            row.Cells["estadoValor"].Value = ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString();
            row.Cells["estado"].Value = ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString();
            row.Cells["nombreLocal"].Value = nombreLocal;
            row.Cells["idNegocio"].Value = idNegocio;
            row.Cells["moneda"].Value = checkDolares.Checked?"DOLARES":"PESOS";
        }

        private void RealizarTransaccionCaja(decimal montoPagado, int idPagoParcialGenerado)
        {
            List<CajaRegistradora> lista = new CN_CajaRegistradora().Listar(GlobalSettings.SucursalId);
            CajaRegistradora cajaAbierta = lista.FirstOrDefault(c => c.estado == true);

            if (cajaAbierta != null && montoPagado > 0)
            {
                var cajaAsociadaFP1 = new CN_FormaPago().ObtenerFPPorDescripcion(cboFormaPago.Text).cajaAsociada;
                TransaccionCaja objTransaccion = new TransaccionCaja()
                {
                    idCajaRegistradora = cajaAbierta.idCajaRegistradora,
                    hora = dtpFecha.Value.Hour.ToString(),
                    tipoTransaccion = "ENTRADA",
                    monto = montoPagado,
                    docAsociado = "Pago Parcial Numero: " + idPagoParcialGenerado + " Cliente: " + txtCliente.Text,
                    usuarioTransaccion = cboVendedores.Text,
                    formaPago = cboFormaPago.Text,
                    cajaAsociada = cajaAsociadaFP1,
                    idVenta = null,
                    idCompra = null,
                    idPagoParcial = idPagoParcialGenerado,
                    idNegocio = GlobalSettings.SucursalId,
                    concepto = "PAGO PARCIAL"
                };

                int idTransaccionGenerado = new CN_Transaccion().RegistrarMovimiento(objTransaccion, out string mensaje);
            }
        }


        private void btnLimpiarDatos_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            if (Convert.ToInt32(txtIdPagoParcial.Text) != 0)
            {

                if (MessageBox.Show("Desea eliminar el Pago Parcial?", "Confirmar Eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    

                    int idPagoParcial = Convert.ToInt32(txtIdPagoParcial.Text);
                    


                    bool respuesta = new CN_PagoParcial().Eliminar(idPagoParcial, out mensaje);
                    if (respuesta)
                    {

                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                        bool eliminarTransaccion = new CN_Transaccion().EliminarMovimientoCajaYPagoParcial(idPagoParcial, out mensaje);
                        if (eliminarTransaccion)
                        {
                            MessageBox.Show("Pago Parcial Eliminado y Movimiento en Caja Eliminado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        } else
                        {
                            MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
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
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtSeñaSeleccionada.Text = dgvData.Rows[indice].Cells["nombreCompleto"].Value.ToString();
                    txtIdCliente.Text = dgvData.Rows[indice].Cells["idCliente"].Value.ToString();
                    txtIdPagoParcial.Text = dgvData.Rows[indice].Cells["idPagoParcial"].Value.ToString();
                    dtpFecha.Text = dgvData.Rows[indice].Cells["fecha"].Value.ToString();
                    txtCliente.Text = dgvData.Rows[indice].Cells["nombreCompleto"].Value.ToString();
                    cboFormaPago.Text = dgvData.Rows[indice].Cells["formaPago"].Value.ToString();
                    txtMonto.Value = Convert.ToDecimal(dgvData.Rows[indice].Cells["monto"].Value);
                    txtProductoReservado.Text = dgvData.Rows[indice].Cells["productoReservado"].Value.ToString();
                    cboVendedores.Text = dgvData.Rows[indice].Cells["vendedor"].Value.ToString();




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

        private void checkMostrarTodosPagosParciales_CheckedChanged(object sender, EventArgs e)
        {
            if (checkMostrarTodosPagosParciales.Checked) { CargarPagosParciales();  } else { CargarPagosParcialesPorLocal(); }
        }

        private void checkPesos_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPesos.Checked)
            {
                checkDolares.Checked = false;
            }
        }

        private void checkDolares_CheckedChanged(object sender, EventArgs e)
        {
            if (checkDolares.Checked)
            {
                checkPesos.Checked = false;
            }
        }

        private void cboFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboFormaPago.Text=="DOLAR EFECTIVO")
            {
                checkDolares.Checked = true;
            }
        }
    }
}
