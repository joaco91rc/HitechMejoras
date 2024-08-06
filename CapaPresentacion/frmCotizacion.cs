using CapaEntidad;
using CapaNegocio;
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
    public partial class frmCotizacion : Form
    {
        public frmCotizacion()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            var cotizacionActiva = new CN_Cotizacion().CotizacionActiva();

            if(cotizacionActiva.estado == true)
            {

                bool resultado = new CN_Cotizacion().Editar(cotizacionActiva, out mensaje);

                if (resultado)
                {
                    Cotizacion objCotizacion = new Cotizacion()
                    {
                        fecha = dtpFecha.Value,
                        importe = txtImporte.Value,

                    };

                    int idCotizacionGenerado = new CN_Cotizacion().Registrar(objCotizacion, out mensaje);

                    if (idCotizacionGenerado != 0)
                    {
                        dgvData.Rows.Add(new object[] { idCotizacionGenerado ,dtpFecha.Value.ToString(), (txtImporte.Value).ToString()
            });
                        Limpiar();
                    }
                    else
                    {

                        MessageBox.Show(mensaje);
                    }

                }

            }

            
            
        }


        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtIdCotizacion.Text = "0";
            txtImporte.Value = 1;
            dtpFecha.Value = DateTime.Now;
            txtImporte.Select();
        }

        private void frmCotizacion_Load(object sender, EventArgs e)
        {
            List<Cotizacion> listaCotizaciones = new CN_Cotizacion().HistoricoCotizaciones();

            foreach (Cotizacion item in listaCotizaciones)
            {
                dgvData.Rows.Add(new object[] {item.idCotizacion,
                    item.fecha.ToString(),
                    item.importe
                    
                    });
            }

        }
    }
    }

