using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Producto
    {

        private CD_Producto objcd_Producto = new CD_Producto();

        public DataTable ListarProductos(int idNegocio)
        {
            return objcd_Producto.ListarProductos(idNegocio);
        }

            public List<Producto> Listar(int idNegocio)
        {
            return objcd_Producto.Listar(idNegocio);
        }
        public List<Producto> ListarPorNegocio(int idNegocio)
        {
            return objcd_Producto.ListarPorNegocio(idNegocio);
        }

        public DataTable ListarProductosPorNegocio(int idNegocio)
        {
            return objcd_Producto.ListarProductosPorNegocio(idNegocio);
        }
            public List<Producto> ListarSerializablesPorNegocio(int idNegocio)
        {
            return objcd_Producto.ListarSerializablesPorNegocio(idNegocio);
        }

        public List<Producto> ListarSerializables(int idNegocio)
        { 
            return objcd_Producto.ListarSerializables(idNegocio); 
        }

            public List<ProductoDetalle> ListarProductosConSerialNumberPorLocalDisponibles(int idNegocio)
        {
            return objcd_Producto.ListarProductosConSerialNumberPorLocalDisponibles(idNegocio);
        }

        public int ContarProductosSerializados(int idProducto, int idNegocio)
        {
            return objcd_Producto.ContarProductosSerializados(idProducto,idNegocio);
        }
        public List<Producto> Listar()
        {
            return objcd_Producto.Listar();
        }
        public Producto ObtenerProductoPorId(int idProducto)
        {
            return objcd_Producto.ObtenerProductoPorId(idProducto);

        }

        public int Registrar(Producto objProducto,out string mensaje)
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

        public bool ActualizarProductoDolar(int idProducto, bool productoDolar)
        {
            return objcd_Producto.ActualizarProductoDolar(idProducto, productoDolar);
        }

            public decimal ObtenerCostoProducto(int idProducto)
        {
            return objcd_Producto.ObtenerCostoProducto(idProducto);
        }


        public bool EditarPrecios(Producto objProducto, out string mensaje)
        {
            mensaje = string.Empty;
            if (objProducto.precioCompra == 0)
            {
                mensaje = mensaje + "Es necesario el Precio de Compra del Producto\n";
            }

            if (objProducto.precioVenta == 0)
            {
                mensaje = mensaje + "Es necesario el Precio de Venta del Producto\n";
            }




            if (mensaje != string.Empty)
            {
                return false;
            }
            else
            {

                return objcd_Producto.EditarPrecios(objProducto, out mensaje);
            }

        }
        public bool RestarStockPorRMA(int idProducto, int cantidad, int idNegocio, out string mensaje)
        {
            return objcd_Producto.RestarStockPorRMA(idProducto, cantidad, idNegocio, out mensaje);
        }

        public bool SumarStockPorRMA(int idProducto, int cantidad, int idNegocio, out string mensaje)
        {
            return objcd_Producto.SumarStockPorRMA(idProducto, cantidad, idNegocio, out mensaje);
        }

        public bool Eliminar(Producto objProducto, out string mensaje)
        {
            return objcd_Producto.Eliminar(objProducto, out mensaje);
        }

        public bool DarBajaLogica(int idProducto, out string mensaje)
        {
            return objcd_Producto.DarBajaLogica(idProducto, out mensaje);
        }

        public bool DesactivarProductoDetalle(int idProductoDetalle, int idVenta, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {



                // Llamar al método de la capa de datos y obtener el resultado
                int registrosAfectados = objcd_Producto.DesactivarProductoDetalle(idProductoDetalle, idVenta, out mensaje);

                if (registrosAfectados > 0)
                {
                    resultado = true; // Indica que se desactivó el producto correctamente
                }
                else
                {
                    resultado = false; // No se encontró o desactivó el producto
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error en la capa de negocio: " + ex.Message;
                resultado = false;
            }

            return resultado;
        }


        public bool ActivarProductoDetalle(int idProductoDetalle, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {



                // Llamar al método de la capa de datos y obtener el resultado
                int registrosAfectados = objcd_Producto.ActivarProductoDetalle(idProductoDetalle, out mensaje);

                if (registrosAfectados > 0)
                {
                    resultado = true; // Indica que se desactivó el producto correctamente
                }
                else
                {
                    resultado = false; // No se encontró o desactivó el producto
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error en la capa de negocio: " + ex.Message;
                resultado = false;
            }

            return resultado;
        }
        public List<ProductoDetalle> ListarProductosVendidos(int idNegocio)
        {
            return objcd_Producto.ListarProductosVendidos(idNegocio);
        }

        public List<ProductoDetalle> ListarProductosVendidosPorFecha(int idNegocio, DateTime fechaInicio, DateTime fechaFin)
        {
            return objcd_Producto.ListarProductosVendidosPorFecha(idNegocio, fechaInicio, fechaFin);
        }
            public List<ProductoDetalle> ListarProductosVendidosTodosLocales()
        {
            return objcd_Producto.ListarProductosVendidosTodosLocales();
        }

            public List<ProductoDetalle> ListarProductosConSerialNumber()
        {
            // Llama al método de la capa de datos
            return objcd_Producto.ListarProductosConSerialNumber();
        }
        public List<ProductoDetalle> ListarProductosConSerialNumberEnStockTodosLocales()
        {
            return objcd_Producto.ListarProductosConSerialNumberEnStockTodosLocales();
        }

        public List<ProductoDetalle> ListarProductosConSerialNumberByID(int idProducto)
        {
            // Llama al método de la capa de datos
            return objcd_Producto.ListarProductosConSerialNumberByID(idProducto);
        }

        public List<ProductoDetalle> ListarProductosConSerialNumberByIDNegocio(int idProducto, int idNegocio)
        {
            return objcd_Producto.ListarProductosConSerialNumberByIDNegocio(idProducto, idNegocio);
        }

            public List<ProductoDetalle> ListarProductosSerialesPorVenta(int idVenta)
        {
            // Llama al método de la capa de datos
            return objcd_Producto.ListarProductosSerialesPorVenta(idVenta);
        }

        public int RegistrarSerialNumber(ProductoDetalle objProductoDetalle, out string mensaje)
            {
                // Validaciones o reglas de negocio antes de registrar
                if (string.IsNullOrWhiteSpace(objProductoDetalle.numeroSerie))
                {
                    mensaje = "El número de serie no puede estar vacío.";
                    return 0;
                }

                if (objProductoDetalle.idProducto <= 0)
                {
                    mensaje = "El ID del producto no es válido.";
                    return 0;
                }

                return objcd_Producto.RegistrarSerialNumber(objProductoDetalle,out mensaje);
            }

        public bool EditarSerialNumber(ProductoDetalle objProductoDetalle, out string mensaje)
        {
            // Validaciones o reglas de negocio antes de editar
            if (string.IsNullOrWhiteSpace(objProductoDetalle.numeroSerie))
            {
                mensaje = "El número de serie no puede estar vacío.";
                return false;
            }

            if (objProductoDetalle.idProducto <= 0)
            {
                mensaje = "El ID del producto no es válido.";
                return false;
            }

            return objcd_Producto.EditarSerialNumber(objProductoDetalle, out mensaje);
        }

        public bool EliminarSerialNumber(ProductoDetalle objProductoDetalle, out string mensaje)
        {
            

            if (objProductoDetalle.idProducto <= 0)
            {
                mensaje = "El ID del producto no es válido.";
                return false;
            }

            return objcd_Producto.EliminarSerialNumber(objProductoDetalle, out mensaje);
        }

        public bool TraspasarSerialNumber(ProductoDetalle productoDetalle, out string mensaje)
        {
            return objcd_Producto.TraspasarSerialNumber(productoDetalle, out mensaje);
        }
        }
}
