using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Transaccion
    {
        private CD_Transaccion objcd_TransaccionCaja = new CD_Transaccion();
        public List<TransaccionCaja> Listar(int idCajaRegistradora, int idNegocio)
        {
            return objcd_TransaccionCaja.Listar(idCajaRegistradora, idNegocio);
        }

        public bool ObtenerSaldosCajas(int idNegocio, int idCajaRegistradora, out Decimal saldoEfectivo, out Decimal saldoMercadoPago, out Decimal saldoDolares, out Decimal saldoGalicia, out string mensaje)
        {
            // Inicialización de los valores de salida
            saldoEfectivo = 0;
            saldoMercadoPago = 0;
            saldoDolares = 0;
            saldoGalicia = 0;
            mensaje = string.Empty;

            bool resultado = false;

            try
            {
                // Llamar a la capa de datos para obtener los saldos de las cajas
                resultado = objcd_TransaccionCaja.ObtenerSaldosCajas(idNegocio, idCajaRegistradora, out saldoEfectivo, out saldoMercadoPago, out saldoDolares, out saldoGalicia, out mensaje);
            }
            catch (Exception ex)
            {
                // En caso de error, capturar el mensaje de la excepción
                mensaje = ex.Message;
                resultado = false;
            }

            return resultado;
        }


        public int RegistrarMovimiento(TransaccionCaja objTransaccion, out string mensaje)
        {
            mensaje = string.Empty;
            if (objTransaccion.tipoTransaccion == "")
            {
                mensaje = mensaje + "Es necesario especificar el tipo de Movimiento\n";
            }

            if (objTransaccion.monto == 0)
            {
                mensaje = mensaje + "Es necesario especificar el Monto del Movimiento\n";
            }


            

            if (mensaje != string.Empty)
            {
                return 0;
            }
            else
            {

                return objcd_TransaccionCaja.RegistrarMovimiento(objTransaccion, out mensaje);
            }
        }

        public bool EditarMovimiento(TransaccionCaja objTransaccion, out string mensaje)
        {
            mensaje = string.Empty;

            // Validaciones previas
            if (string.IsNullOrWhiteSpace(objTransaccion.tipoTransaccion))
            {
                mensaje = "Es necesario especificar el tipo de Movimiento\n";
            }

            if (objTransaccion.monto == 0)
            {
                mensaje += "Es necesario especificar el Monto del Movimiento\n";
            }

            if (!string.IsNullOrEmpty(mensaje))
            {
                return false;
            }
            else
            {
                // Llamada al método de la capa de datos para editar el movimiento
                return objcd_TransaccionCaja.EditarMovimiento(objTransaccion, out mensaje);
            }
        }


        public bool EliminarMovimiento(int idTransaccion, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                // Llamar al método de la capa de datos para eliminar el movimiento
                resultado = objcd_TransaccionCaja.EliminarMovimiento(idTransaccion, out mensaje);
            }
            catch (Exception ex)
            {
                mensaje = "Ocurrió un error al eliminar el movimiento: " + ex.Message;
            }

            return resultado;
        }

        public bool EliminarMovimientoCajaYVenta(int idVenta, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                // Llamar al método de la capa de datos para eliminar el movimiento
                resultado = objcd_TransaccionCaja.EliminarMovimientoCajaYVenta(idVenta,out mensaje);
            }
            catch (Exception ex)
            {
                mensaje = "Ocurrió un error al eliminar el movimiento: " + ex.Message;
            }

            return resultado;
        }

        public bool EliminarMovimientoCajaYCompra(int idCompra, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                // Llamar al método de la capa de datos para eliminar el movimiento
                resultado = objcd_TransaccionCaja.EliminarMovimientoCajaYCompra(idCompra, out mensaje);
            }
            catch (Exception ex)
            {
                mensaje = "Ocurrió un error al eliminar el movimiento: " + ex.Message;
            }

            return resultado;
        }
    }
}
