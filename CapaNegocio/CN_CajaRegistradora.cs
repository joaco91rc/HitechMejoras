using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_CajaRegistradora
    {
        private CD_CajaRegistradora objcd_CajaRegistradora = new CD_CajaRegistradora();

        public List<CajaRegistradora> Listar(int idNegocio)
        {
            return objcd_CajaRegistradora.Listar(idNegocio);
        }

        

        public CajaRegistradora ObtenerCajaPorFecha(string fecha, int idNegocio)
        {
            return objcd_CajaRegistradora.ObtenerCajaPorFecha(fecha, GlobalSettings.SucursalId);
        }

        public int AperturaCaja(CajaRegistradora objCajaRegistradora, out string mensaje, int idNegocio)
        {
            mensaje = string.Empty;

            if (mensaje != string.Empty)
            {
                return 0;
            }
            else

                return objcd_CajaRegistradora.AperturaCaja(objCajaRegistradora, out mensaje, idNegocio);
            
        }

        public bool CerrarCaja(CajaRegistradora objCajaRegistradora, out string mensaje, int idNegocio)
        {
            mensaje = string.Empty;

            return objcd_CajaRegistradora.CerrarCaja(objCajaRegistradora, out mensaje, idNegocio);
        }

    }
}
