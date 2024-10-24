using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_ClienteNegocio
    {
        private CD_ClienteNegocio objcd_ClientesNegocio = new CD_ClienteNegocio();

        // Verifica si un cliente está asignado a un negocio específico
        public bool ClienteAsignadoANegocio(int idCliente, int idNegocio)
        {
            return objcd_ClientesNegocio.ClienteAsignadoANegocio(idCliente, idNegocio);
        }

        // Lista los negocios a los que pertenece un cliente
        public List<int> ListarNegociosDeCliente(int idCliente)
        {
            return objcd_ClientesNegocio.ListarNegociosDeCliente(idCliente);
        }

        // Asigna un cliente a un negocio
        public bool AsignarClienteANegocio(int idCliente, int idNegocio)
        {
            return objcd_ClientesNegocio.AsignarClienteANegocio(idCliente, idNegocio);
        }

        // Modifica las asignaciones de negocios para un cliente
        public bool ModificarAsignacionNegocios(int idCliente, List<int> listaNegocios)
        {
            return objcd_ClientesNegocio.ModificarClientesEnNegocios(idCliente, listaNegocios);
        }

        // Elimina todas las asignaciones de negocios para un cliente
        public bool EliminarAsignacionesCliente(int idCliente)
        {
            return objcd_ClientesNegocio.EliminarClientesDeNegocio(idCliente);
        }
    }
}
