using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
   public  class CN_Moneda
    {
        private CD_Moneda objcd_Moneda = new CD_Moneda();

        public List<Moneda> ListarMonedas()
        {
            return objcd_Moneda.ListarMonedas();
        }

        public int RegistrarMoneda(Moneda objMoneda, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(objMoneda.Nombre) || string.IsNullOrWhiteSpace(objMoneda.Simbolo))
            {
                mensaje = "El nombre y símbolo de la moneda son obligatorios.";
                return 0;
            }

            return objcd_Moneda.RegistrarMoneda(objMoneda, out mensaje);
        }

        public Moneda ObtenerMonedaPorId(int idMoneda)
        {
            return objcd_Moneda.ObtenerMonedaPorId(idMoneda);
        
        }
            public bool EditarMoneda(Moneda objMoneda, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(objMoneda.Nombre) || string.IsNullOrWhiteSpace(objMoneda.Simbolo))
            {
                mensaje = "El nombre y símbolo de la moneda son obligatorios.";
                return false;
            }

            return objcd_Moneda.EditarMoneda(objMoneda, out mensaje);
        }
        public bool EliminarMoneda(int idMoneda, out string mensaje)
        {
            mensaje = string.Empty;

            if (idMoneda <= 0)
            {
                mensaje = "El identificador de la moneda debe ser válido.";
                return false;
            }

            return objcd_Moneda.EliminarMoneda(idMoneda, out mensaje);
        }

    }
}
