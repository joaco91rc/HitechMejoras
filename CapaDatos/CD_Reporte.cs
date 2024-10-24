using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Reporte
    {

        public List<ReporteCompra> Compra(string fechaInicio, string fechaFin, int idProveedor, int idNegocio)
        {
            List<ReporteCompra> lista = new List<ReporteCompra>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    StringBuilder query = new StringBuilder();

                    SqlCommand cmd = new SqlCommand("SP_REPORTECOMPRAS", oconexion);
                    cmd.Parameters.AddWithValue("fechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("fechaFin", fechaFin);
                    cmd.Parameters.AddWithValue("idProveedor", idProveedor);
                    cmd.Parameters.AddWithValue("idNegocio", idNegocio);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReporteCompra()
                            {
                                fechaRegistro = dr["fechaRegistro"].ToString(),
                                tipoDocumento = dr["tipoDocumento"].ToString(),
                                nroDocumento = dr["nroDocumento"].ToString(),
                                montoTotal = dr["montoTotal"].ToString(),
                                usuarioRegistro = dr["usuarioRegistro"].ToString(),
                                documentoProveedor = dr["documentoProveedor"].ToString(),
                                razonSocial = dr["razonSocial"].ToString(),
                                codigoProducto = dr["codigoProducto"].ToString(),
                                nombreProducto = dr["nombreProducto"].ToString(),
                                categoria = dr["categoria"].ToString(),
                                precioCompra = dr["precioCompra"].ToString(),
                                precioVenta = dr["precioVenta"].ToString(),
                                cantidad = dr["cantidad"].ToString(),
                                subTotal = dr["subTotal"].ToString(),

                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<ReporteCompra>();
                }

            }
            return lista;


        }


        //public List<ReporteVenta> Venta(string fechaInicio, string fechaFin, int idNegocio)
        //{
        //    List<ReporteVenta> lista = new List<ReporteVenta>();
        //    using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
        //    {
        //        try
        //        {

        //            StringBuilder query = new StringBuilder();

        //            SqlCommand cmd = new SqlCommand("SP_REPORTEVENTAS", oconexion);
        //            cmd.Parameters.AddWithValue("fechaInicio", fechaInicio);
        //            cmd.Parameters.AddWithValue("fechaFin", fechaFin);
        //            cmd.Parameters.AddWithValue("idNegocio", idNegocio);

        //            cmd.CommandType = CommandType.StoredProcedure;
        //            oconexion.Open();

        //            using (SqlDataReader dr = cmd.ExecuteReader())
        //            {
        //                while (dr.Read())
        //                {
        //                    lista.Add(new ReporteVenta()
        //                    {
        //                        fechaRegistro = dr["fechaRegistro"].ToString(),
        //                        tipoDocumento = dr["tipoDocumento"].ToString(),
        //                        nroDocumento = dr["nroDocumento"].ToString(),
        //                        montoTotal = dr["montoTotal"].ToString(),
        //                        usuarioRegistro = dr["usuarioRegistro"].ToString(),
        //                        documentoCliente = dr["documentoCliente"].ToString(),
        //                        nombreCliente = dr["nombreCliente"].ToString(),
        //                        codigoProducto = dr["codigoProducto"].ToString(),
        //                        nombreProducto = dr["nombreProducto"].ToString(),
        //                        categoria = dr["categoria"].ToString(),
        //                        precioVenta = dr["precioVenta"].ToString(),
        //                        cantidad = dr["cantidad"].ToString(),
        //                        subTotal = dr["subTotal"].ToString(),
        //                        cotizacionDolar = dr["cotizacionDolar"].ToString(),
        //                        formaPago = dr["formaPago"].ToString(),
        //                        descuento = dr["descuento"].ToString(),
        //                        montoDescuento = dr["montoDescuento"].ToString(),

        //                    });
        //                }
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            lista = new List<ReporteVenta>();
        //        }

        //    }
        //    return lista;


        //}

        public List<ReporteVenta> GananciaPorVentas(DateTime fechaInicio, DateTime fechaFin, int idNegocio)
        {
            List<ReporteVenta> lista = new List<ReporteVenta>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_REPORTEVENTAS_MARGEN_GANANCIA", oconexion);
                    cmd.Parameters.AddWithValue("fechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("fechaFin", fechaFin);
                    cmd.Parameters.AddWithValue("idNegocio", idNegocio);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReporteVenta()
                            {
                                nombreLocal = ObtenerNombreLocal(idNegocio),
                                fechaRegistro = dr["FechaRegistro"]?.ToString() ?? string.Empty,
                                tipoDocumento = dr["tipoDocumento"]?.ToString() ?? string.Empty,
                                nroDocumento = dr["nroDocumento"]?.ToString() ?? string.Empty,
                                montoTotal = dr["MontoPagoTotalEnPesos"] != DBNull.Value ? Convert.ToDecimal(dr["MontoPagoTotalEnPesos"]).ToString("0.00") : string.Empty,
                                costoTotalProductos = dr["CostoTotalProductos"] != DBNull.Value ? Convert.ToDecimal(dr["CostoTotalProductos"]).ToString("0.00") : string.Empty,
                                margenGananciaEnDolares = dr["MargenGananciaEnDolares"] != DBNull.Value ? Convert.ToDecimal(dr["MargenGananciaEnDolares"]).ToString("0.00") : string.Empty,
                                porcentajeMargenGanancia = dr["PorcentajeMargenGanancia"] != DBNull.Value  ? Convert.ToDecimal(dr["PorcentajeMargenGanancia"]).ToString("0.00"): string.Empty,
                                vendedor = dr["Vendedor"]?.ToString() ?? string.Empty,
                                documentoCliente = dr["documentoCliente"]?.ToString() ?? string.Empty,
                                nombreCliente = dr["nombreCliente"]?.ToString() ?? string.Empty,
                                cotizacionDolar = dr["cotizacionDolar"]?.ToString() ?? string.Empty,
                            });


                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<ReporteVenta>();
                    // Agregar logging aquí si es necesario para registrar errores
                }
            }
            return lista;
        }

        // Método para obtener el nombre del local según el idNegocio
        private string ObtenerNombreLocal(int idNegocio)
        {
            switch (idNegocio)
            {
                case 1:
                    return "HITECH 1";
                case 2:
                    return "HITECH 2";
                case 3:
                    return "APPLE STORE";
                case 4:
                    return "APPLE CAFÉ";
                default:
                    return ""; // Valor por defecto en caso de que no coincida
            }
        }




    }
}
