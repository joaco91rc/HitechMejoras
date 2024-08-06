using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_ProductoNegocio
    {
        private CD_ProductoNegocio objcd_ProductoNegocio = new CD_ProductoNegocio();

        public int ObtenerStockProductoEnSucursal(int idProducto, int idNegocio)
        {

            return objcd_ProductoNegocio.ObtenerStockProductoEnSucursal(idProducto, idNegocio);

        }

        public void CargarOActualizarStockProducto(int idProducto, int idNegocio, int nuevoStock)
        {

             objcd_ProductoNegocio.CargarOActualizarStockProducto(idProducto, idNegocio, nuevoStock);

        }


        public void SobrescribirStock(int idProducto, int idNegocio, int nuevoStock)
        {

            objcd_ProductoNegocio.SobrescribirStock(idProducto, idNegocio, nuevoStock);

        }

    }
}
