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
    public class CN_Compra
    {

        private CD_Compra objcd_compra = new CD_Compra();

        public int ObtenerCorrelativo()
        {
            return objcd_compra.ObtenerCorrelativo();
        }

        public bool Registrar(Compra objCompra, DataTable detalleCompra, out string mensaje, out int idCompraSalida)
        {
          

                return objcd_compra.Registrar(objCompra, detalleCompra, out mensaje,out idCompraSalida);
            
        }

        public List<Compra> ObtenerComprasConDetalleEntreFechas(int idNegocio, DateTime fechaInicio, DateTime fechaFin)
        {
            // Simplemente llamamos al método del DAL que ya trae todo junto
            return objcd_compra.ObtenerComprasConDetalleEntreFechas(idNegocio, fechaInicio, fechaFin);
        }
        public List<Compra> ObtenerComprasConDetalle(int idNegocio)
        {
            List<Compra> listaCompras = objcd_compra.ObtenerComprasConDetalle(idNegocio);

            foreach (Compra compra in listaCompras)
            {
                compra.oDetalleCompra = objcd_compra.ObtenerDetalleCompra(compra.idCompra);
            }

            return listaCompras;
        }


        public Compra ObtenerCompra(string numero, int idNegocio)
        {
            Compra oCompra = objcd_compra.ObtenerCompra(numero,idNegocio);

            if(oCompra.idCompra != 0)
            {
                List<DetalleCompra> oDetalleCompra = objcd_compra.ObtenerDetalleCompra(oCompra.idCompra);
                oCompra.oDetalleCompra = oDetalleCompra;
            }

            return oCompra;
        }

        public bool EliminarCompraConDetalle(int idCompra, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                // Llamar al método de la capa de datos para eliminar la compra y sus detalles
                objcd_compra.EliminarCompraConDetalle(idCompra);
                resultado = true;
                mensaje = "La compra y sus detalles fueron eliminados correctamente.";
            }
            catch (Exception ex)
            {
                mensaje = "Ocurrió un error al eliminar la compra: " + ex.Message;
            }

            return resultado;
        }

    }
}
