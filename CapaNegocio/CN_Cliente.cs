using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Cliente
    {

        private CD_Cliente objcd_Cliente = new CD_Cliente();

        public List<Cliente> Listar()
        {
            return objcd_Cliente.Listar();
        }
        public List<Cliente> ListarClientes()
        {
            return objcd_Cliente.ListarClientes();
        }

        public int ObtenerIdClientePorDocumentoYNombre(string documentoCliente, string nombreCompleto)
        {
            return objcd_Cliente.ObtenerIdClientePorDocumentoYNombre(documentoCliente,nombreCompleto);
        }
        public Cliente ObtenerClientePorDocumentoYNombre(string documentoCliente, string nombreCompleto)
        {
            return objcd_Cliente.ObtenerClientePorDocumentoYNombre(documentoCliente, nombreCompleto);
        }
            public List<Cliente> ListarClientesPorNegocio(int idNegocio)
        {
            return objcd_Cliente.ListarClientesPorNegocio(idNegocio);
        }
        public int Registrar(Cliente objCliente, out string mensaje)
        {
            mensaje = string.Empty;
            if (objCliente.nombreCompleto == "")
            {
                mensaje = mensaje + "Es necesario el nombre del Cliente\n";
            }

            if (objCliente.documento == "")
            {
                mensaje = mensaje + "Es necesario el documento del Cliente\n";
            }


            if (objCliente.correo == "")
            {
                mensaje = mensaje + "Es necesario el correo del Cliente\n";
            }

            if (mensaje != string.Empty)
            {
                return 0;
            }
            else
            {

                return objcd_Cliente.Registrar(objCliente, out mensaje);
            }
        }

        public bool Editar(Cliente objCliente, out string mensaje)
        {
            mensaje = string.Empty;
            if (objCliente.nombreCompleto == "")
            {
                mensaje = mensaje + "Es necesario el nombre del Cliente\n";
            }

            if (objCliente.documento == "")
            {
                mensaje = mensaje + "Es necesario el documento del Cliente\n";
            }


            if (objCliente.correo == "")
            {
                mensaje = mensaje + "Es necesario el correo del Cliente\n";
            }

            if (mensaje != string.Empty)
            {
                return false;
            }
            else
            {

                return objcd_Cliente.Editar(objCliente, out mensaje);
            }

        }


        public bool Eliminar(Cliente objCliente, out string mensaje)
        {
            return objcd_Cliente.Eliminar(objCliente, out mensaje);
        }


    }

}
