using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Cotizacion
    {

        private CD_Cotizacion objcd_Cotizacion = new CD_Cotizacion();
        public Cotizacion CotizacionActiva()
        {
            return objcd_Cotizacion.CotizacionActiva();
        }


        public List<Cotizacion> HistoricoCotizaciones()
        {
            return objcd_Cotizacion.HistoricoCotizaciones();
        }

        public int Registrar(Cotizacion objCotizacion, out string mensaje)
        {
            mensaje = string.Empty;
            if (objCotizacion.importe <= 0)
            {
                mensaje = mensaje + "El importe debe ser mayor o igual a 0\n";
            }

            if (mensaje != string.Empty)
            {
                return 0;
            }
            else
            {

                return objcd_Cotizacion.Registrar(objCotizacion, out mensaje);
            }
        }

        public bool Editar(Cotizacion objCotizacion, out string mensaje)
        {
            mensaje = string.Empty;
            

               return objcd_Cotizacion.Editar(objCotizacion, out mensaje);
            

        }

    }
}
