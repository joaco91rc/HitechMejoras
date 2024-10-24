using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Venta
    {

        private CD_Venta objcd_venta = new CD_Venta();

        public bool RestarStock(int idProducto, int cantidad)
        {
            return objcd_venta.RestarStock(idProducto, cantidad);
        }

        public bool SumarStock(int idProducto, int cantidad)
        {
            return objcd_venta.SumarStock(idProducto, cantidad);
        }

        public int ObtenerCorrelativo()
        {
            return objcd_venta.ObtenerCorrelativo();
        }

        public bool Registrar(Venta objVenta, DataTable detalleVenta, out string mensaje, out int idVentaGenerado)
        {


            return objcd_venta.Registrar(objVenta, detalleVenta, out mensaje, out idVentaGenerado);

        }

        public bool EliminarVentaConDetalle(int idVenta, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                // Llamar al método de la capa de datos para eliminar la venta y sus detalles
                objcd_venta.EliminarVentaConDetalle(idVenta);
                resultado = true;
                mensaje = "La venta y sus detalles fueron eliminados correctamente.";
            }
            catch (Exception ex)
            {
                mensaje = "Ocurrió un error al eliminar la venta: " + ex.Message;
            }

            return resultado;
        }


        public bool EditarVenta(Venta objventa, DataTable detalleVenta, out string mensaje, out int idVentaGenerado)
        {
            return objcd_venta.EditarVenta(objventa, detalleVenta, out mensaje, out idVentaGenerado);
        }

        public Venta ObtenerVenta(string numero, int idNegocio)
        {
            Venta oVenta = objcd_venta.ObtenerVenta(numero,idNegocio);

            if (oVenta.idVenta != 0)
            {
                List<DetalleVenta> oDetalleVenta = objcd_venta.ObtenerDetalleVenta(oVenta.idVenta);
                oVenta.oDetalleVenta = oDetalleVenta;
            }

            return oVenta;
        }
        public List<Venta> ObtenerVentasConDetalle(int idNegocio)
        {
            // Simplemente llamamos al método del DAL que ya trae todo junto
            return objcd_venta.ObtenerVentasConDetalle(idNegocio);
        }

        public List<Venta> ObtenerVentasConDetalleEntreFechas(int idNegocio, DateTime fechaInicio, DateTime fechaFin)
        {
            // Simplemente llamamos al método del DAL que ya trae todo junto
            return objcd_venta.ObtenerVentasConDetalleEntreFechas(idNegocio,fechaInicio,fechaFin);
        }
        public List<Venta> ObtenerVentasConDetalle()
        {
            List<Venta> listaVentas = objcd_venta.ObtenerVentasConDetalle();

            foreach (Venta venta in listaVentas)
            {
                venta.oDetalleVenta = objcd_venta.ObtenerDetalleVenta(venta.idVenta);
            }

            return listaVentas;
        }
    }
}
