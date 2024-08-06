using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Producto
    {

        private CD_Producto objcd_Producto = new CD_Producto();

        public List<Producto> Listar(int idNegocio)
        {
            return objcd_Producto.Listar(idNegocio);
        }


        public List<Producto> Listar()
        {
            return objcd_Producto.Listar();
        }
        public Producto ObtenerProductoPorId(int idProducto)
        {
            return objcd_Producto.ObtenerProductoPorId(idProducto);

        }

        public int Registrar(Producto objProducto, out string mensaje)
        {
            mensaje = string.Empty;
            if (objProducto.nombre == "")
            {
                mensaje = mensaje + "Es necesario el nombre del Producto\n";
            }

            if (objProducto.codigo == "")
            {
                mensaje = mensaje + "Es necesario el codigo del Producto\n";
            }


            

            if (mensaje != string.Empty)
            {
                return 0;
            }
            else
            {

                return objcd_Producto.Registrar(objProducto, out mensaje);
            }
        }

        public bool Editar(Producto objProducto, out string mensaje)
        {
            mensaje = string.Empty;
            if (objProducto.nombre == "")
            {
                mensaje = mensaje + "Es necesario el nombre del Producto\n";
            }

            if (objProducto.codigo == "")
            {
                mensaje = mensaje + "Es necesario el codigo del Producto\n";
            }


           

            if (mensaje != string.Empty)
            {
                return false;
            }
            else
            {

                return objcd_Producto.Editar(objProducto, out mensaje);
            }

        }
        public bool RestarStockPorRMA(int idProducto, int cantidad, out string mensaje)
        {
            return objcd_Producto.RestarStockPorRMA(idProducto, cantidad, out mensaje);
        }

        public bool SumarStockPorRMA(int idProducto, int cantidad, out string mensaje)
        {
            return objcd_Producto.SumarStockPorRMA(idProducto, cantidad, out mensaje);
        }

        public bool Eliminar(Producto objProducto, out string mensaje)
        {
            return objcd_Producto.Eliminar(objProducto, out mensaje);
        }



    }
}
