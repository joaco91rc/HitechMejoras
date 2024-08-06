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
    public partial class frmCajaRegistradora : Form
    {
        public class TotalesCaja
        {
            public decimal Total { get; set; }
            public decimal TotalMP { get; set; }
            public decimal TotalUSS { get; set; }
            public decimal TotalGalicia { get; set; }
        }

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

        private void frmCajaRegistradora_Load(object sender, EventArgs e)
        {
            
            cboTipoMovimiento.Items.Add(new OpcionCombo() { Valor = 1, Texto = "ENTRADA" });
            cboTipoMovimiento.Items.Add(new OpcionCombo() { Valor = 0, Texto = "SALIDA" });
            cboTipoMovimiento.DisplayMember = "Texto";
            cboTipoMovimiento.ValueMember = "Valor";
            cboTipoMovimiento.SelectedIndex = 0;

            CargarComboBoxFormaPago();

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
                List<TransaccionCaja> listaTransacciones = new CN_Transaccion().Listar(cajaAbierta.idCajaRegistradora);

                foreach (TransaccionCaja item in listaTransacciones)
                {
                    dgvData.Rows.Add(new object[] {
                        "",
                        item.idCajaRegistradora,
                        item.idTransaccion,
                        item.hora,
                        item.tipoTransaccion,
                        item.monto,
                        item.formaPago,
                        item.cajaAsociada,
                        item.docAsociado,
                        item.usuarioTransaccion
                             });

                }
                TotalesCaja totalesCaja = CalcularTotales(cajaAbierta);
                txtSaldo.Text = (totalesCaja.Total + Convert.ToDecimal(txtSaldoInicial.Text)).ToString();
                txtSaldoMP.Text = (totalesCaja.TotalMP + Convert.ToDecimal(txtSaldoInicialMP.Text)).ToString();
                txtSaldoUSS.Text = (totalesCaja.TotalUSS + Convert.ToDecimal(txtSaldoInicialUSS.Text)).ToString();
                txtSaldoGalicia.Text = (totalesCaja.TotalGalicia + Convert.ToDecimal(txtSaldoInicialGalicia.Text)).ToString();
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
                    
                    hora = txtHora.Text,
                    tipoTransaccion = cboTipoMovimiento.Text,
                    monto = montoCalculado,
                    
                    formaPago = cboFormaPago.Text,
                    cajaAsociada = cboCajaAsociada.Text,
                    docAsociado = txtDocAsociado.Text,
                    usuarioTransaccion = Environment.GetEnvironmentVariable("usuario"),
                    
                };




                int idTransaccionGenerado = new CN_Transaccion().RegistrarMovimiento(objTransaccion, out mensaje);

                


                if (idTransaccionGenerado != 0)
                {
                    objTransaccion.idTransaccion = idTransaccionGenerado;
                    dgvData.Rows.Add(new object[] { "",
                        idTransaccionGenerado,
                        idCajaRegistradora,
                        txtHora.Text,
                        cboTipoMovimiento.Text,
                        montoCalculado,
                        cboFormaPago.Text,
                        cboCajaAsociada.Text,
                        txtDocAsociado.Text,
                        objTransaccion.usuarioTransaccion


            });
                    totalesCaja = CalcularTotales(cajaAbierta);

                    Limpiar();
                    txtMonto.Select();
                }
                else
                {
                    MessageBox.Show(mensaje,"Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }


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
                Total = total, 
                TotalMP = totalMP, 
                TotalUSS = totalUSS, 
                TotalGalicia = totalGalicia 
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
                    cboFormaPago.Text = dgvData.Rows[indice].Cells["formaPago"].Value.ToString();
                    cboCajaAsociada.Text = dgvData.Rows[indice].Cells["cajaAsociada"].Value.ToString();
                    txtMonto.Text = dgvData.Rows[indice].Cells["monto"].Value.ToString();
                    cboCajaAsociada.Text = dgvData.Rows[indice].Cells["cajaAsociada"].Value.ToString();
                    txtDocAsociado.Text = dgvData.Rows[indice].Cells["docAsociado"].Value.ToString();


                   

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
                            txtSaldo.Text = (totalesCaja.Total + Convert.ToDecimal(txtSaldoInicial.Text)).ToString();
                            txtSaldoMP.Text = (totalesCaja.TotalMP + Convert.ToDecimal(txtSaldoInicialMP.Text)).ToString();
                            txtSaldoUSS.Text = (totalesCaja.TotalUSS + Convert.ToDecimal(txtSaldoInicialUSS.Text)).ToString();
                            txtSaldoGalicia.Text = (totalesCaja.TotalGalicia + Convert.ToDecimal(txtSaldoInicialGalicia.Text)).ToString();
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
