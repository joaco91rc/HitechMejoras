using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_ProductoRMA
    {

        private CD_ProductoRMA objcd_ProductoRMA = new CD_ProductoRMA();
        public List<ProductoRMA> ListarProductosRMA()
        {
            return objcd_ProductoRMA.ListarProductosRMA();
        }

        public int RegistrarProductoXRMA(ProductoRMA objProductoRMA, out string mensaje)
        {
            mensaje = string.Empty;
            return objcd_ProductoRMA.RegistrarProductoXRMA(objProductoRMA,out mensaje);
        }

        public bool EditarProductoXRMA(int idProductoRMA, string estado, DateTime fechaEgreso, out string mensaje)
        {
            mensaje = string.Empty;
            return objcd_ProductoRMA.EditarProductoRMA(idProductoRMA,estado,fechaEgreso, out mensaje);
        }

        public bool EliminarProductoXRMA(int idProductoRMA,  out string mensaje)
        {
            mensaje = string.Empty;
            return objcd_ProductoRMA.EliminarProductoXRMA(idProductoRMA, out mensaje);
        }
    }
}
