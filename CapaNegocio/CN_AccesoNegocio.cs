using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_AccesoNegocio
    {
        private CD_AccesoNegocio objcd_AccesoNegocio = new CD_AccesoNegocio();

        public bool TienePermiso(int idUsuario, int idNegocio)
        {
            return objcd_AccesoNegocio.TienePermiso(idUsuario, idNegocio);
        }

        public bool AsignarPermiso(int idUsuario, int idNegocio)
        {
            return objcd_AccesoNegocio.Asignarpermiso(idUsuario, idNegocio);
        }

        public List<int> ListarNegociosPermitidos(int idUsuario)
        {
            return objcd_AccesoNegocio.ListarNegociosPermitidos(idUsuario);
        }


        public bool ModificarPermisos(int idUsuario, List<int> listaNegocios)
        {
            return objcd_AccesoNegocio.ModificarPermisos(idUsuario, listaNegocios);
        }

        public bool EliminarPermisos(int idUsuario)
        {

            return objcd_AccesoNegocio.EliminarPermisos(idUsuario);
        }


    }       
}
