using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_FormaPago
    {
        private CD_FormaPago objcd_FormaPago = new CD_FormaPago();

        // Listar formas de pago
        public List<FormaPago> ListarFormasDePago()
        {
            return objcd_FormaPago.ListarFormasDePago();
        }

        // Registrar una nueva forma de pago
        public int RegistrarFormaPago(FormaPago objFormaPago, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(objFormaPago.descripcion))
            {
                mensaje += "Es necesario la descripción de la forma de pago\n";
            }

            if (string.IsNullOrWhiteSpace(objFormaPago.cajaAsociada))
            {
                mensaje += "Es necesario informar la Caja Asociada de la forma de pago\n";
            }

            if (objFormaPago.porcentajeRetencion < 0)
            {
                mensaje += "El porcentaje de retención debe ser mayor o igual a 0\n";
            }

            if (mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_FormaPago.RegistrarFormaPago(objFormaPago, out mensaje);
            }
        }

        // Editar una forma de pago existente
        public bool EditarFormaPago(FormaPago objFormaPago, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(objFormaPago.descripcion))
            {
                mensaje += "Es necesario la descripción de la forma de pago\n";
            }
            if(string.IsNullOrWhiteSpace(objFormaPago.cajaAsociada))
            {
                mensaje += "Es necesario informar la Caja Asociada de la forma de pago\n";
            }

            if (objFormaPago.porcentajeRetencion <= 0)
            {
                mensaje += "El porcentaje de retención debe ser mayor a 0\n";
            }

            if (mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_FormaPago.EditarFormaPago(objFormaPago, out mensaje);
            }



          

        

    }

        public FormaPago ObtenerFPPorDescripcion(string descripcion)
        {
            try
            {
                return objcd_FormaPago.ObtenerFPPorDescripcion(descripcion);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones si es necesario
                throw new Exception("Error al obtener la forma de pago por descripción", ex);
            }
        }


    }
}
