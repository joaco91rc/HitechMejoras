using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
   public  class CN_PrecioProducto
    {
        private CD_PrecioProducto objcd_PrecioProducto = new CD_PrecioProducto();

        public List<PrecioProducto> ListarPreciosProducto()
        {
            return objcd_PrecioProducto.ListarPreciosProducto();
        }

        public PrecioProducto ObtenerPreciosPorProductoYMoneda(int idProducto, int idMoneda)
        {
            return objcd_PrecioProducto.ObtenerPreciosPorProductoYMoneda(idProducto,idMoneda);
        }

            public int RegistrarPrecioProducto(PrecioProducto objPrecioProducto, out string mensaje)
        {
            mensaje = string.Empty;

            if (objPrecioProducto.PrecioCompra <= 0 || objPrecioProducto.PrecioVenta <= 0)
            {
                mensaje = "El precio de compra y el precio de venta deben ser mayores a 0.";
                return 0;
            }

            return objcd_PrecioProducto.RegistrarPrecioProducto(objPrecioProducto, out mensaje);
        }
        public bool EditarPrecioProducto(PrecioProducto objPrecioProducto, out string mensaje)
        {
            mensaje = string.Empty;

            if (objPrecioProducto.PrecioCompra <= 0 || objPrecioProducto.PrecioVenta <= 0)
            {
                mensaje = "El precio de compra y el precio de venta deben ser mayores a 0.";
                return false;
            }

            return objcd_PrecioProducto.EditarPrecioProducto(objPrecioProducto, out mensaje);
        }
        public bool EliminarPrecioProducto(int idPrecioProducto, out string mensaje)
        {
            mensaje = string.Empty;

            if (idPrecioProducto <= 0)
            {
                mensaje = "El identificador del precio debe ser válido.";
                return false;
            }

            return objcd_PrecioProducto.EliminarPrecioProducto(idPrecioProducto, out mensaje);
        }

    }
}
