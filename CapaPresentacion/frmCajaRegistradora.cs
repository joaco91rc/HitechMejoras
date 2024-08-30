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
    public partial class frmCajaRegistradora : Form
    {
        private Image defaultImage = Properties.Resources.CHECK;
        public class TotalesCaja
        {
            public decimal Total { get; set; }
            public decimal TotalMP { get; set; }
            public decimal TotalUSS { get; set; }
            public decimal TotalGalicia { get; set; }

        }

        private int idCompraGlobal = 0;
        private int idVentaGlobal = 0;
        private TotalesCaja totalesCaja = new TotalesCaja();
        public frmCajaRegistradora()
        {
            InitializeComponent();
        }

        private void btnCerrarCaja_Click(object sender, EventArgs e)
        {
            List<CajaRegistradora> lista = new CN_CajaRegistradora().Listar(GlobalSettings.SucursalId);

            CajaRegistradora cajaAbierta = lista.Where(c => c.estado == true).FirstOrDefault();

            if (cajaAbierta != null)
            {
                int idCajaAbierta = cajaAbierta.idCajaRegistradora;

                string mensaje = string.Empty;
                CajaRegistradora objCajaRegistradora = new CajaRegistradora()
                {
                    idCajaRegistradora = idCajaAbierta,
                    fechaCierre = DateTime.Now.ToString(),
                    saldo = Convert.ToDecimal(txtSaldo.Text) ,
                    saldoMP = Convert.ToDecimal(txtSaldoMP.Text) ,
                    saldoUSS = Convert.ToDecimal(txtSaldoUSS.Text) ,
                    saldoGalicia = Convert.ToDecimal(txtSaldoGalicia.Text)
                };
                bool resultado = new CN_CajaRegistradora().CerrarCaja(objCajaRegistradora, out mensaje, GlobalSettings.SucursalId);
                MessageBox.Show("Caja Registradora de la fecha: " + cajaAbierta.fechaApertura + "Cerrada ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("No hay ninguna Caja para Cerrar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }

        //private void CargarComboBoxFormaPago()
        //{
        //    // Crear una instancia de la capa de negocio
        //    CN_FormaPago objCN_FormaPago = new CN_FormaPago();

        //    // Obtener la lista de formas de pago desde la base de datos
        //    List<FormaPago> listaFormaPago = objCN_FormaPago.ListarFormasDePago();

        //    // Limpiar los items actuales del ComboBox
        //    cboFormaPago.Items.Clear();
            

        //    // Llenar el ComboBox con los datos obtenidos
        //    foreach (FormaPago formaPago in listaFormaPago)
        //    {
        //        cboFormaPago.Items.Add(new OpcionCombo() { Valor = formaPago.idFormaPago, Texto = formaPago.descripcion });
              
        //    }

        //    // Establecer DisplayMember y ValueMember
        //    cboFormaPago.DisplayMember = "Texto";
        //    cboFormaPago.ValueMember = "Valor";
           

        //    // Seleccionar el primer item por defecto si hay elementos en el ComboBox
        //    if (cboFormaPago.Items.Count > 0)
        //    {
        //        cboFormaPago.SelectedIndex = -1;
               
        //    }
        //}

        private void frmCajaRegistradora_Load(object sender, EventArgs e)
        {
            
            cboTipoMovimiento.Items.Add(new OpcionCombo() { Valor = 1, Texto = "ENTRADA" });
            cboTipoMovimiento.Items.Add(new OpcionCombo() { Valor = 0, Texto = "SALIDA" });
            cboTipoMovimiento.DisplayMember = "Texto";
            cboTipoMovimiento.ValueMember = "Valor";
            cboTipoMovimiento.SelectedIndex = -1;

         

            //CargarComboBoxFormaPago();

            foreach (DataGridViewColumn columna in dgvData.Columns)
            {

                if (columna.Visible == true )
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });

                }


            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 4;

            CajaRegistradora cajaAbierta = new CN_CajaRegistradora().Listar(GlobalSettings.SucursalId).Where(x => x.estado == true).FirstOrDefault();

            if (cajaAbierta != null)
            {
                txtSaldoInicial.Text= cajaAbierta.saldo.ToString("0.00");
                txtSaldoInicialMP.Text = cajaAbierta.saldoMP.ToString("0.00");
                txtSaldoInicialUSS.Text = cajaAbierta.saldoUSS.ToString("0.00");
                txtSaldoInicialGalicia.Text = cajaAbierta.saldoGalicia.ToString("0.00");
                txtSaldo.Text = cajaAbierta.saldo.ToString("0.00");
                txtSaldoMP.Text = cajaAbierta.saldoMP.ToString("0.00");
                txtSaldoUSS.Text = cajaAbierta.saldoUSS.ToString("0.00");
                txtSaldoGalicia.Text = cajaAbierta.saldoGalicia.ToString("0.00");
                List<TransaccionCaja> listaTransacciones = new CN_Transaccion().Listar(cajaAbierta.idCajaRegistradora, GlobalSettings.SucursalId);

                foreach (TransaccionCaja item in listaTransacciones)
                {
                    dgvData.Rows.Add(new object[] {
                        defaultImage,
                        item.idCajaRegistradora,
                        item.idTransaccion,
                        item.hora,
                        item.concepto,
                        item.tipoTransaccion,
                        item.monto,
                        item.formaPago,
                        item.cajaAsociada,
                        item.docAsociado,
                        item.usuarioTransaccion,
                        item.idCompra,
                        item.idVenta,
                        item.idNegocio
                             });

                }
                TotalesCaja totalesCaja = CalcularTotales(cajaAbierta);
                txtSaldo.Text = totalesCaja.Total.ToString();
                txtSaldoMP.Text = totalesCaja.TotalMP.ToString();
                txtSaldoUSS.Text = totalesCaja.TotalUSS.ToString();
                txtSaldoGalicia.Text = totalesCaja.TotalGalicia.ToString();
            }

            
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
            txtHora.Text = DateTime.Now.ToString(); 

            string mensaje = string.Empty;

            List<CajaRegistradora> lista = new CN_CajaRegistradora().Listar(GlobalSettings.SucursalId);

            CajaRegistradora cajaAbierta = lista.Where(c => c.estado == true).FirstOrDefault();

            if (cajaAbierta != null)

            {
                decimal montoCalculado = Convert.ToDecimal(txtMonto.Text);
                if (cboTipoMovimiento.Text == "SALIDA")
                {
                    montoCalculado = montoCalculado * -1;
                    
                }

                TransaccionCaja objTransaccion = new TransaccionCaja()
                {
                    idCajaRegistradora = cajaAbierta.idCajaRegistradora,
                    idTransaccion = Convert.ToInt32(txtIdTransaccion.Text),
                    hora = txtHora.Text,
                    tipoTransaccion = cboTipoMovimiento.Text,
                    monto = montoCalculado,
                    concepto= cboConcepto.Text,
                    formaPago = "",
                    cajaAsociada = cboCajaAsociada.Text,
                    docAsociado = txtDocAsociado.Text,
                    usuarioTransaccion = Environment.GetEnvironmentVariable("usuario"),
                    idCompra= idCompraGlobal,
                    idVenta = idVentaGlobal,
                    idNegocio = GlobalSettings.SucursalId
                    
                };


                if (objTransaccion.idTransaccion == 0)
                {





                    int idTransaccionGenerado = new CN_Transaccion().RegistrarMovimiento(objTransaccion, out mensaje);




                    if (idTransaccionGenerado != 0)
                    {
                        txtIdTransaccion.Text = idTransaccionGenerado.ToString();
                        objTransaccion.idTransaccion = idTransaccionGenerado;
                        dgvData.Rows.Add(new object[] { defaultImage,

                        objTransaccion.idCajaRegistradora,
                        idTransaccionGenerado,
                        txtHora.Text,
                        cboConcepto.Text,
                        cboTipoMovimiento.Text,
                        montoCalculado,
                        "",
                        cboCajaAsociada.Text,
                        txtDocAsociado.Text,
                        objTransaccion.usuarioTransaccion,
                        0,
                        0,
                        GlobalSettings.SucursalId


            });
                        totalesCaja = CalcularTotales(cajaAbierta);

                        Limpiar();
                        txtMonto.Select();
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                } else
                {
                    bool resultado = new CN_Transaccion().EditarMovimiento(objTransaccion, out mensaje);
                    if (resultado)
                    {
                        // Obtén la fila correspondiente en el DataGridView
                        DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];

                        // Actualiza las celdas con los nuevos valores
                        row.Cells["idTransaccion"].Value = objTransaccion.idTransaccion;
                        row.Cells["tipoTransaccion"].Value = objTransaccion.tipoTransaccion;
                        row.Cells["monto"].Value = objTransaccion.monto;
                        row.Cells["docAsociado"].Value = objTransaccion.docAsociado;
                        row.Cells["fecha"].Value = objTransaccion.hora;
                        row.Cells["usuarioTransaccion"].Value = objTransaccion.usuarioTransaccion;
                        row.Cells["formaPago"].Value = objTransaccion.formaPago;
                        row.Cells["cajaAsociada"].Value = objTransaccion.cajaAsociada;
                        row.Cells["concepto"].Value = objTransaccion.concepto;

                        // Opcional: si la transacción tiene asociada una venta o compra
                        row.Cells["idVenta"].Value = objTransaccion.idVenta.HasValue ? objTransaccion.idVenta.Value.ToString() : "0";
                        row.Cells["idCompra"].Value = objTransaccion.idCompra.HasValue ? objTransaccion.idCompra.Value.ToString() : "0";

                        // Opcional: si quieres agregar alguna lógica específica según los valores
                        // Por ejemplo, si la transacción está asociada a un negocio:
                        if (objTransaccion.idNegocio > 0)
                        {
                            row.Cells["idNegocio"].Value = objTransaccion.idNegocio;
                        }
                        totalesCaja = CalcularTotales(cajaAbierta);
                        // Limpiar controles si es necesario
                        Limpiar();
                    }
                    
                    else
                    {
                        MessageBox.Show(mensaje);
                    }
                }
                txtSaldo.Text = totalesCaja.Total.ToString();
                txtSaldoMP.Text = totalesCaja.TotalMP.ToString();
                txtSaldoUSS.Text = totalesCaja.TotalUSS.ToString();
                txtSaldoGalicia.Text = totalesCaja.TotalGalicia.ToString();

            }
            


        }


        private TotalesCaja CalcularTotales(CajaRegistradora objCajaRegistradora)
        {
            decimal total = 0;
            decimal totalMP = 0;
            decimal totalUSS = 0;
            decimal totalGalicia = 0;

            if (dgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Visible)
                    {
                        string cajaAsociada = row.Cells["cajaAsociada"].Value.ToString();
                        decimal monto = Convert.ToDecimal(row.Cells["monto"].Value.ToString());

                        if (cajaAsociada == "EFECTIVO")
                        {
                            total += monto;
                        }
                        else if (cajaAsociada == "DOLARES" )
                        {
                            totalUSS += monto;
                        }
                        else if (cajaAsociada == "MERCADO PAGO" )
                        {
                            totalMP += monto;
                        }
                        else if (cajaAsociada == "GALICIA")
                        {
                            totalGalicia += monto;
                        }
                    }
                }
            }

            TotalesCaja totales = new TotalesCaja
            {
                Total = total + Convert.ToDecimal(txtSaldoInicial.Text), 
                TotalMP = totalMP + Convert.ToDecimal(txtSaldoInicialMP.Text), 
                TotalUSS = totalUSS + Convert.ToDecimal(txtSaldoInicialUSS.Text), 
                TotalGalicia = totalGalicia + Convert.ToDecimal(txtSaldoInicialGalicia.Text)
            };

            return totales;
        }

        private void Limpiar()
        {

           
            cboTipoMovimiento.SelectedItem = 1;
            txtMonto.Text = "";
            txtDocAsociado.Text = "";
            cboCajaAsociada.SelectedIndex = -1;
            txtMonto.Select();
            txtIdTransaccion.Text = "0";
            txtIndice.Text = "-1";
            txtIdCompra.Text = "-1";
            txtIdCompra.Text = "-1";

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();
            List<CajaRegistradora> lista = new CN_CajaRegistradora().Listar(GlobalSettings.SucursalId);

            CajaRegistradora cajaAbierta = lista.Where(c => c.estado == true).FirstOrDefault();
            if (dgvData.Rows.Count > 0)
            {

                foreach (DataGridViewRow row in dgvData.Rows)
                {

                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Text.Trim().ToUpper()))
                    {
                        row.Visible = true;
                        
                    }
                    else
                        row.Visible = false;


                }
                
                totalesCaja = CalcularTotales(cajaAbierta);
                
            }
        }

        


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            List<CajaRegistradora> lista = new CN_CajaRegistradora().Listar(GlobalSettings.SucursalId);

            CajaRegistradora cajaAbierta = lista.Where(c => c.estado == true).FirstOrDefault();
            txtBusqueda.Clear();
            foreach (DataGridViewRow row in dgvData.Rows)
                row.Visible = true;
            totalesCaja = CalcularTotales(cajaAbierta);
            decimal saldoFiltrado = Convert.ToDecimal(txtSaldo.Text) - cajaAbierta.saldo;
            txtSaldo.Text = saldoFiltrado.ToString();
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

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtIdTransaccion.Text = dgvData.Rows[indice].Cells["idTransaccion"].Value.ToString();
                    cboTipoMovimiento.Text = dgvData.Rows[indice].Cells["tipoTransaccion"].Value.ToString();
                    
                    cboCajaAsociada.Text = dgvData.Rows[indice].Cells["cajaAsociada"].Value.ToString();
                    if (dgvData.Rows[indice].Cells["tipoTransaccion"].Value.ToString() == "SALIDA")
                    {
                        txtMonto.Text = (Convert.ToDecimal(dgvData.Rows[indice].Cells["monto"].Value)*-1).ToString();
                    }else
                    {
                        txtMonto.Text = dgvData.Rows[indice].Cells["monto"].Value.ToString();
                    }

                    
                    cboCajaAsociada.Text = dgvData.Rows[indice].Cells["cajaAsociada"].Value.ToString();
                    txtDocAsociado.Text = dgvData.Rows[indice].Cells["docAsociado"].Value.ToString();
                    
                    // Verificar si idCompra es NULL
                    txtIdCompra.Text = dgvData.Rows[indice].Cells["idCompra"].Value != DBNull.Value
                                        ? dgvData.Rows[indice].Cells["idCompra"].Value.ToString()
                                        : "0";

                    // Verificar si idVenta es NULL
                    txtIdVenta.Text = dgvData.Rows[indice].Cells["idVenta"].Value != DBNull.Value
                                      ? dgvData.Rows[indice].Cells["idVenta"].Value.ToString()
                                      : "0";

                    if(txtIdCompra.Text != "0")
                    {
                        this.idCompraGlobal = Convert.ToInt32(txtIdCompra.Text);
                    }
                    if (txtIdVenta.Text != "0")
                    {
                        this.idVentaGlobal = Convert.ToInt32(txtIdVenta.Text);
                    }



                }

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            List<CajaRegistradora> lista = new CN_CajaRegistradora().Listar(GlobalSettings.SucursalId);
            CajaRegistradora cajaAbierta = lista.Where(c => c.estado == true).FirstOrDefault();
            if (Convert.ToInt32(txtIdTransaccion.Text) != 0)
            {

                if (MessageBox.Show("Desea eliminar el Movimiento?", "Confirmar Eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;

                    if (GlobalSettings.RolUsuario == 1)
                    {
                        
                        bool respuesta = new CN_Transaccion().EliminarMovimiento(Convert.ToInt32(txtIdTransaccion.Text), out mensaje);
                        if (respuesta)
                        {

                            dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                            totalesCaja = CalcularTotales(cajaAbierta);
                            txtSaldo.Text = totalesCaja.Total.ToString();
                            txtSaldoMP.Text = totalesCaja.TotalMP.ToString();
                            txtSaldoUSS.Text = totalesCaja.TotalUSS.ToString();
                            txtSaldoGalicia.Text = totalesCaja.TotalGalicia.ToString();
                            Limpiar();
                        }

                        else
                        {

                            MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                        }
                    } else
                    {
                        MessageBox.Show("No posee permisos para Eliminar un Movimiento de Caja", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }



                }
            }
        }

        

        private void btnLimpiarDatos_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void cboTipoMovimiento_SelectedIndexChanged(object sender, EventArgs e)
        { 
            var listaConceptos = new CN_Concepto().Listar();
            
            if(cboTipoMovimiento.Text == "ENTRADA")
            {
                var listaConcpetoFiltrada = listaConceptos.Where(x => x.tipo == "ENTRADA").ToList();
                cboConcepto.DataSource = listaConcpetoFiltrada;
                cboConcepto.DisplayMember = "descripcion"; // Propiedad que se mostrará en el ComboBox
                cboConcepto.ValueMember = "idConcepto";

            }
            else
            {
                var listaConcpetoFiltrada = listaConceptos.Where(x => x.tipo == "SALIDA").ToList();
                cboConcepto.DataSource = listaConcpetoFiltrada;
                cboConcepto.DisplayMember = "descripcion"; // Propiedad que se mostrará en el ComboBox
                cboConcepto.ValueMember = "idConcepto";

            }
        }
    }
}
