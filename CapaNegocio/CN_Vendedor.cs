using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Vendedor
    {
        private CD_Vendedor objcd_Vendedor = new CD_Vendedor();

        public List<Vendedor> ListarVendedores()
        {
            return objcd_Vendedor.ListarVendedores();
        }

        public int RegistrarVendedor(Vendedor objVendedor, out string mensaje)
        {
            mensaje = string.Empty;

            // Validaciones
            if (string.IsNullOrWhiteSpace(objVendedor.DNI))
            {
                mensaje += "Es necesario el DNI del Vendedor\n";
            }

            if (string.IsNullOrWhiteSpace(objVendedor.nombre))
            {
                mensaje += "Es necesario el nombre del Vendedor\n";
            }

            if (string.IsNullOrWhiteSpace(objVendedor.apellido))
            {
                mensaje += "Es necesario el apellido del Vendedor\n";
            }

            if (objVendedor.sueldoBase < 0)
            {
                mensaje += "El sueldo base no puede ser negativo\n";
            }

            if (mensaje != string.Empty)
            {
                return 0; // Si hay mensajes de error, no se realiza el registro.
            }
            else
            {
                return objcd_Vendedor.RegistrarVendedor(objVendedor, out mensaje);
            }
        }

        public bool EditarVendedor(Vendedor objVendedor, out string mensaje)
        {
            mensaje = string.Empty;

            // Validaciones
            if (string.IsNullOrWhiteSpace(objVendedor.DNI))
            {
                mensaje += "Es necesario el DNI del Vendedor\n";
            }

            if (string.IsNullOrWhiteSpace(objVendedor.nombre))
            {
                mensaje += "Es necesario el nombre del Vendedor\n";
            }

            if (string.IsNullOrWhiteSpace(objVendedor.apellido))
            {
                mensaje += "Es necesario el apellido del Vendedor\n";
            }

            if (objVendedor.sueldoBase < 0)
            {
                mensaje += "El sueldo base no puede ser negativo\n";
            }

            if (mensaje != string.Empty)
            {
                return false; // Si hay mensajes de error, no se realiza la edición.
            }
            else
            {
                return objcd_Vendedor.EditarVendedor(objVendedor, out mensaje);
            }
        }

        public bool EliminarVendedor(Vendedor objVendedor, out string mensaje)
        {
            return objcd_Vendedor.EliminarVendedor(objVendedor, out mensaje);
        }
    }
}
