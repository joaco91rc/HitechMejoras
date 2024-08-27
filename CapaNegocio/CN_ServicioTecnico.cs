using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_ServicioTecnico
    {
        private CD_ServicioTecnico objcd_ServicioTecnico = new CD_ServicioTecnico();

        public List<ServicioTecnico> ListarServiciosPendientes(int idSucursal)
        {
            return objcd_ServicioTecnico.Listar(idSucursal);
        }

        public bool InsertarServicioTecnico(ServicioTecnico servicioTecnico, out string mensaje)
        {
            mensaje = string.Empty;

            // Validaciones previas
            if (servicioTecnico.IdCliente <= 0)
            {
                mensaje += "El Id del cliente debe ser válido.\n";
            }

            if (string.IsNullOrWhiteSpace(servicioTecnico.Producto))
            {
                mensaje += "El producto es obligatorio.\n";
            }

            if (servicioTecnico.FechaRecepcion == DateTime.MinValue)
            {
                mensaje += "La fecha de recepción es obligatoria.\n";
            }

            if (!string.IsNullOrEmpty(mensaje))
            {
                return false;
            }

            return objcd_ServicioTecnico.InsertarServicioTecnico(servicioTecnico, out mensaje);
        }

        public List<ServicioTecnico> ListarServiciosCompletados(int idSucursal)
        {
            return objcd_ServicioTecnico.ListarServiciosCompletados(idSucursal);
        }

        public bool CambiarEstadoIngresadoAPendiente(int idServicio, out string mensaje)
        {
            mensaje = string.Empty;
            return objcd_ServicioTecnico.CambiarEstadoIngresadoAPendiente(idServicio,out mensaje);
        }

        public bool CambiarEstadoPendienteACompletado(int idServicio, string descripcionReparacion, string Observaciones,  out string mensaje)
        {
            mensaje = string.Empty;

            

            return objcd_ServicioTecnico.CambiarEstadoPendienteACompletado(idServicio, descripcionReparacion,Observaciones, out mensaje );
        }

        public bool CobrarServicioTecnico(int idServicio, decimal precioReal, DateTime fechaEntregaReal, out string mensaje)
        {
            mensaje = string.Empty;

            // Llamada al método de la capa de datos que realiza la actualización en la base de datos.
            return objcd_ServicioTecnico.CobrarServicioTecnico(idServicio, precioReal, fechaEntregaReal, out mensaje);
        }

    }
}
