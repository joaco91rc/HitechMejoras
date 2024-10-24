using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Deuda
    {
        private CD_Deuda obj_cdDeuda = new CD_Deuda();

        public bool InsertarDeuda(Deuda deuda)
        {
            return obj_cdDeuda.InsertarDeuda(deuda);
        }

        public List<Deuda> ListarDeudas()
        {
            return obj_cdDeuda.ListarDeudas();
        }

        public Dictionary<int, decimal> CalcularDeudaTotalPorSucursal()
        {

            return obj_cdDeuda.CalcularDeudaTotalPorSucursal();
        }

        public bool ActualizarEstadoDeuda(int idDeuda)
        {
            return obj_cdDeuda.ActualizarEstadoDeuda(idDeuda);
        }

        }
}
