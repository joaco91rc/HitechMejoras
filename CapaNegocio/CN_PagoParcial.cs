using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_PagoParcial
    {
        private CD_PagoParcial objcd_PagoParcial = new CD_PagoParcial();

        public List<PagoParcial> Listar()
        { return objcd_PagoParcial.Listar()}

            // Método para listar todas las señas de pagos parciales
            public List<PagoParcial> ConsultarPagosParcialesPorCliente(int idCliente)
        {
            return objcd_PagoParcial.ConsultarPagosParcialesPorCliente(idCliente);
        }

        // Método para registrar un pago parcial (seña)
        public int RegistrarPagoParcial(PagoParcial objPagoParcial, out string mensaje)
        {
            return objcd_PagoParcial.RegistrarPagoParcial(objPagoParcial, out mensaje);
        }

        // Método para modificar un pago parcial (seña)
        public bool ModificarPagoParcial(PagoParcial objPagoParcial, out string mensaje)
        {
            return objcd_PagoParcial.ModificarPagoParcial(objPagoParcial, out mensaje);
        }

        // Método para eliminar un pago parcial
        public bool Eliminar(int idPagoParcial, out string mensaje)
        {
            return objcd_PagoParcial.EliminarPagoParcial(idPagoParcial, out mensaje);
        }

        
    }
}
