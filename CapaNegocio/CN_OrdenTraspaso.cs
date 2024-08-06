using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_OrdenTraspaso
    {

        private CD_OrdenTraspaso objcd_OrdenTraspaso = new CD_OrdenTraspaso();

        public bool InsertarOrdenTraspaso(OrdenTraspaso ordenTraspaso)
        {

            return objcd_OrdenTraspaso.InsertarOrdenTraspaso(ordenTraspaso);

        }

        public List<OrdenTraspaso> ListarOrdenesTraspaso()
        {
            return objcd_OrdenTraspaso.ListarOrdenesTraspaso();
        }

        public OrdenTraspaso ObtenerOrdenTraspasoPorId(int id)
        {
            return objcd_OrdenTraspaso.ObtenerOrdenTraspasoPorId(id);
        }


        public bool ConfirmarOrdenTraspaso(int id)
        {
            return objcd_OrdenTraspaso.ConfirmarOrdenTraspaso(id);


        }



        }


}
