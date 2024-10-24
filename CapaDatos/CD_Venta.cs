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
    public class CD_Venta
    {

        public int ObtenerCorrelativo()
        {
            int idCorrelativo = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count (*) +1 from VENTA");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    idCorrelativo = Convert.ToInt32(cmd.ExecuteScalar());

                }
                catch (Exception ex)
                {
                    idCorrelativo = 0;
                }
            }

            return idCorrelativo;
        }

        public  bool RestarStock(int idProducto, int cantidad)
        {
            bool respuesta = true;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update producto set stock = stock - @cantidad where idProducto = @idproducto");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@idproducto", idProducto);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    respuesta = cmd.ExecuteNonQuery()>0?true:false;

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }

        public bool SumarStock(int idProducto, int cantidad)
        {
            bool respuesta = true;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update producto set stock = stock + @cantidad where idProducto = @idproducto");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@idproducto", idProducto);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }


        public bool Registrar(Venta objventa, DataTable detalleVenta, out string mensaje, out int idVentaGenerado)
        {
            bool respuesta = false;
            mensaje = string.Empty;
            idVentaGenerado = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARVENTA", oconexion);
                    cmd.Parameters.AddWithValue("idUsuario", objventa.oUsuario.idUsuario);
                    cmd.Parameters.AddWithValue("fecha", objventa.fechaRegistro);
                    cmd.Parameters.AddWithValue("tipoDocumento", objventa.tipoDocumento);
                    cmd.Parameters.AddWithValue("nroDocumento", objventa.nroDocumento);
                    cmd.Parameters.AddWithValue("documentoCliente", objventa.documentoCliente);
                    cmd.Parameters.AddWithValue("nombreCliente", objventa.nombreCliente);
                    cmd.Parameters.AddWithValue("montoPago", objventa.montoPago);
                    cmd.Parameters.AddWithValue("montoPagoFP2", objventa.montoPagoFP2);
                    cmd.Parameters.AddWithValue("montoPagoFP3", objventa.montoPagoFP3);
                    cmd.Parameters.AddWithValue("montoPagoFP4", objventa.montoPagoFP4);
                    cmd.Parameters.AddWithValue("montoFP1", objventa.montoFP1);
                    cmd.Parameters.AddWithValue("montoFP2", objventa.montoFP2);
                    cmd.Parameters.AddWithValue("montoFP3", objventa.montoFP3);
                    cmd.Parameters.AddWithValue("montoFP4", objventa.montoFP4);
                    cmd.Parameters.AddWithValue("montoCambio", objventa.montoCambio);
                    cmd.Parameters.AddWithValue("montoTotal", objventa.montoTotal);
                    cmd.Parameters.AddWithValue("cotizacionDolar", objventa.cotizacionDolar);
                    cmd.Parameters.AddWithValue("detalleVenta", detalleVenta);
                    cmd.Parameters.AddWithValue("formaPago", objventa.formaPago);
                    cmd.Parameters.AddWithValue("formaPago2", objventa.formaPago2);
                    cmd.Parameters.AddWithValue("formaPago3", objventa.formaPago3);
                    cmd.Parameters.AddWithValue("formaPago4", objventa.formaPago4);
                    cmd.Parameters.AddWithValue("descuento", objventa.descuento);
                    cmd.Parameters.AddWithValue("montoDescuento", objventa.montoDescuento);
                    cmd.Parameters.AddWithValue("idNegocio", objventa.idNegocio);
                    cmd.Parameters.AddWithValue("idVendedor", objventa.idVendedor);
                    cmd.Parameters.AddWithValue("observaciones", objventa.observaciones);
                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("idVentaSalida", SqlDbType.Int).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                    idVentaGenerado = Convert.ToInt32(cmd.Parameters["idVentaSalida"].Value);
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    mensaje = ex.Message;
                }
            }

            return respuesta;
        }

        public bool EditarVenta(Venta objventa, DataTable detalleVenta, out string mensaje, out int idVentaModificado)
        {
            bool respuesta = false;
            mensaje = string.Empty;
            idVentaModificado = 0; // Inicializar idVentaModificado

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                SqlTransaction transaction = null;

                try
                {
                    oconexion.Open();
                    transaction = oconexion.BeginTransaction();

                    // Actualizar la venta
                    StringBuilder queryVenta = new StringBuilder();
                    queryVenta.AppendLine("UPDATE VENTA SET");
                    queryVenta.AppendLine("idUsuario = @idUsuario,");
                    queryVenta.AppendLine("fechaRegistro = @fechaRegistro,");
                    queryVenta.AppendLine("tipoDocumento = @tipoDocumento,");
                    queryVenta.AppendLine("nroDocumento = @nroDocumento,");
                    queryVenta.AppendLine("documentoCliente = @documentoCliente,");
                    queryVenta.AppendLine("nombreCliente = @nombreCliente,");
                    queryVenta.AppendLine("montoPago = @montoPago,");
                    queryVenta.AppendLine("montoPagoFP2 = @montoPagoFP2,");
                    queryVenta.AppendLine("montoPagoFP3 = @montoPagoFP3,");
                    queryVenta.AppendLine("montoPagoFP4 = @montoPagoFP4,");
                    queryVenta.AppendLine("montoFP1 = @montoFP1,");
                    queryVenta.AppendLine("montoFP2 = @montoFP2,");
                    queryVenta.AppendLine("montoFP3 = @montoFP3,");
                    queryVenta.AppendLine("montoFP4 = @montoFP4,");
                    queryVenta.AppendLine("montoCambio = @montoCambio,");
                    queryVenta.AppendLine("montoTotal = @montoTotal,");
                    queryVenta.AppendLine("cotizacionDolar = @cotizacionDolar,");
                    queryVenta.AppendLine("formaPago = @formaPago,");
                    queryVenta.AppendLine("formaPago2 = @formaPago2,");
                    queryVenta.AppendLine("formaPago3 = @formaPago3,");
                    queryVenta.AppendLine("formaPago4 = @formaPago4,");
                    queryVenta.AppendLine("descuento = @descuento,");
                    queryVenta.AppendLine("idVendedor = @idVendedor,");
                    queryVenta.AppendLine("montoDescuento = @montoDescuento,");
                    queryVenta.AppendLine("observaciones = @observaciones");
                    queryVenta.AppendLine("WHERE idVenta = @idVenta");

                    SqlCommand cmdVenta = new SqlCommand(queryVenta.ToString(), oconexion, transaction);
                    cmdVenta.CommandType = CommandType.Text;

                    cmdVenta.Parameters.AddWithValue("@idUsuario", objventa.oUsuario.idUsuario);
                    cmdVenta.Parameters.AddWithValue("@fechaRegistro", objventa.fechaRegistro);
                    cmdVenta.Parameters.AddWithValue("@tipoDocumento", objventa.tipoDocumento);
                    cmdVenta.Parameters.AddWithValue("@nroDocumento", objventa.nroDocumento);
                    cmdVenta.Parameters.AddWithValue("@documentoCliente", objventa.documentoCliente);
                    cmdVenta.Parameters.AddWithValue("@nombreCliente", objventa.nombreCliente);
                    cmdVenta.Parameters.AddWithValue("@montoPago", objventa.montoPago);
                    cmdVenta.Parameters.AddWithValue("@montoPagoFP2", objventa.montoPagoFP2);
                    cmdVenta.Parameters.AddWithValue("@montoPagoFP3", objventa.montoPagoFP3);
                    cmdVenta.Parameters.AddWithValue("@montoPagoFP4", objventa.montoPagoFP4);
                    cmdVenta.Parameters.AddWithValue("@montoFP1", objventa.montoFP1);
                    cmdVenta.Parameters.AddWithValue("@montoFP2", objventa.montoFP2);
                    cmdVenta.Parameters.AddWithValue("@montoFP3", objventa.montoFP3);
                    cmdVenta.Parameters.AddWithValue("@montoFP4", objventa.montoFP4);
                    cmdVenta.Parameters.AddWithValue("@montoCambio", objventa.montoCambio);
                    cmdVenta.Parameters.AddWithValue("@montoTotal", objventa.montoTotal);
                    cmdVenta.Parameters.AddWithValue("@cotizacionDolar", objventa.cotizacionDolar);
                    cmdVenta.Parameters.AddWithValue("@formaPago", objventa.formaPago);
                    cmdVenta.Parameters.AddWithValue("@formaPago2", objventa.formaPago2);
                    cmdVenta.Parameters.AddWithValue("@formaPago3", objventa.formaPago3);
                    cmdVenta.Parameters.AddWithValue("@formaPago4", objventa.formaPago4);
                    cmdVenta.Parameters.AddWithValue("@descuento", objventa.descuento);
                    cmdVenta.Parameters.AddWithValue("@montoDescuento", objventa.montoDescuento);
                    cmdVenta.Parameters.AddWithValue("@idVenta", objventa.idVenta);
                    cmdVenta.Parameters.AddWithValue("@idVendedor", objventa.idVendedor);
                    cmdVenta.Parameters.AddWithValue("@observaciones", objventa.observaciones);
                    cmdVenta.ExecuteNonQuery();

                    // Guardar el idVenta modificado
                    idVentaModificado = objventa.idVenta;

                    // Eliminar los detalles actuales de la venta
                    SqlCommand cmdEliminarDetalle = new SqlCommand("DELETE FROM DETALLE_VENTA WHERE idVenta = @idVenta", oconexion, transaction);
                    cmdEliminarDetalle.CommandType = CommandType.Text;
                    cmdEliminarDetalle.Parameters.AddWithValue("@idVenta", objventa.idVenta);
                    cmdEliminarDetalle.ExecuteNonQuery();

                    // Insertar los nuevos detalles de la venta
                    foreach (DataRow row in detalleVenta.Rows)
                    {
                        StringBuilder queryDetalle = new StringBuilder();
                        queryDetalle.AppendLine("INSERT INTO DETALLE_VENTA (idVenta, idProducto, cantidad, precioVenta, subTotal)");
                        queryDetalle.AppendLine("VALUES (@idVenta, @idProducto, @cantidad, @precio, @subtotal)");

                        SqlCommand cmdDetalle = new SqlCommand(queryDetalle.ToString(), oconexion, transaction);
                        cmdDetalle.CommandType = CommandType.Text;
                        cmdDetalle.Parameters.AddWithValue("@idVenta", objventa.idVenta);
                        cmdDetalle.Parameters.AddWithValue("@idProducto", Convert.ToInt32(row["idProducto"]));
                        cmdDetalle.Parameters.AddWithValue("@cantidad", Convert.ToInt32(row["cantidad"]));
                        cmdDetalle.Parameters.AddWithValue("@precio", Convert.ToDecimal(row["precioVenta"]));
                        cmdDetalle.Parameters.AddWithValue("@subtotal", Convert.ToDecimal(row["subTotal"]));

                        cmdDetalle.ExecuteNonQuery();
                    }

                    // Si todo salió bien, se confirma la transacción
                    transaction.Commit();
                    respuesta = true;
                }
                catch (Exception ex)
                {
                    // Si hubo un error, se revierte la transacción
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                    mensaje = ex.Message;
                    respuesta = false;
                }
            }

            return respuesta;
        }



        public Venta ObtenerVenta(string numero, int idNegocio)
        {
            Venta objVenta = new Venta();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {


                    oconexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT v.idVenta,v.idNegocio,v.observaciones, v.idVendedor, u.nombreCompleto,v.documentoCliente,v.nombreCliente,v.tipoDocumento,v.nroDocumento,v.cotizacionDolar,v.montoPago,v.montoCambio,v.montoTotal, convert(char(10), v.fechaRegistro, 103)[FechaRegistro],v.formaPago,v.descuento,v.montoDescuento, v.montoFP1,v.montoFP2,v.montoFP3,v.montoFP4,v.formaPago2,v.formaPago3,v.formaPago4,v.montoPagoFP2,v.montoPagoFP3,v.montoPagoFP4");
                    query.AppendLine("FROM VENTA v");
                    query.AppendLine("inner join USUARIO U ON U.idUsuario = V.idUsuario");
                     query.AppendLine("WHERE v.nroDocumento = @numero AND v.idNegocio = @idNegocio");
            
                    
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.Parameters.AddWithValue("@idNegocio", idNegocio);
                    cmd.CommandType = CommandType.Text;
                    

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            objVenta = new Venta()
                            {
                                idVenta = Convert.ToInt32(dr["idVenta"]),
                                idNegocio = Convert.ToInt32(dr["idNegocio"]),
                                oUsuario = new Usuario() { nombreCompleto = dr["nombreCompleto"].ToString() },
                                documentoCliente = dr["documentoCliente"].ToString(),
                                tipoDocumento = dr["tipoDocumento"].ToString(),
                                nombreCliente = dr["nombreCliente"].ToString(),
                                nroDocumento = dr["nroDocumento"].ToString(),
                                montoPago = Convert.ToDecimal(dr["montoPago"].ToString()),
                                montoCambio = Convert.ToDecimal(dr["montoCambio"].ToString()),
                                montoTotal = Convert.ToDecimal(dr["montoTotal"].ToString()),
                                formaPago = dr["formaPago"].ToString(),
                                descuento = Convert.ToInt32(dr["descuento"]),
                                montoDescuento = Convert.ToDecimal(dr["montoDescuento"].ToString()),
                                fechaRegistro = Convert.ToDateTime(dr["fechaRegistro"]),
                                formaPago2 = dr["formaPago2"].ToString(),
                                formaPago3 = dr["formaPago3"].ToString(),
                                formaPago4 = dr["formaPago4"].ToString(),
                                montoFP1 = Convert.ToDecimal(dr["montoFP1"].ToString()),
                                montoFP2 = Convert.ToDecimal(dr["montoFP2"].ToString()),
                                montoFP3 = Convert.ToDecimal(dr["montoFP3"].ToString()),
                                montoFP4 = Convert.ToDecimal(dr["montoFP4"].ToString()),
                                montoPagoFP2 = Convert.ToDecimal(dr["montoPagoFP2"].ToString()),
                                montoPagoFP3 = Convert.ToDecimal(dr["montoPagoFP3"].ToString()),
                                montoPagoFP4 = Convert.ToDecimal(dr["montoPagoFP4"].ToString()),
                                cotizacionDolar = Convert.ToDecimal(dr["cotizacionDolar"].ToString()),
                                idVendedor = Convert.ToInt32(dr["idVendedor"]),
                                observaciones = dr["observaciones"].ToString(),

                            };


                        }
                    }

                }
                catch (Exception ex)
                {
                    objVenta = new Venta();
                }

            }



            return objVenta;
        }

        public List<Venta> ObtenerVentasConDetalle(int idNegocio)
        {
            List<Venta> listaVentas = new List<Venta>();

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT v.idVenta, v.idNegocio, u.nombreCompleto, v.documentoCliente, v.nombreCliente, v.tipoDocumento, v.nroDocumento,");
                    query.AppendLine("v.montoPago, v.montoCambio, v.montoTotal, v.formaPago, v.observaciones,");
                    query.AppendLine("v.descuento, v.montoDescuento, v.montoFP1, v.montoFP2, v.montoFP3, v.montoFP4, v.formaPago2, v.formaPago3, v.formaPago4,");
                    query.AppendLine("v.idVendedor, dv.idProducto, p.nombre AS productoNombre, dv.precioVenta, dv.cantidad, dv.subTotal,");
                    query.AppendLine("vdr.nombre + ' ' + vdr.apellido AS nombreCompletoVendedor");  // Concatenar nombre y apellido del vendedor
                    query.AppendLine("FROM VENTA v");
                    query.AppendLine("INNER JOIN USUARIO u ON u.idUsuario = v.idUsuario");
                    query.AppendLine("LEFT JOIN DETALLE_VENTA dv ON dv.idVenta = v.idVenta");
                    query.AppendLine("LEFT JOIN PRODUCTO p ON p.idProducto = dv.idProducto");
                    query.AppendLine("LEFT JOIN VENDEDOR vdr ON vdr.idVendedor = v.idVendedor");  // Join con la tabla VENDEDOR
                    query.AppendLine("WHERE v.idNegocio = @idNegocio");  // Filtro por idNegocio

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@idNegocio", idNegocio);  // Pasar el parámetro idNegocio
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        int? lastVentaId = null;
                        Venta currentVenta = null;

                        while (dr.Read())
                        {
                            int idVenta = Convert.ToInt32(dr["idVenta"]);

                            // Si es una nueva venta
                            if (lastVentaId == null || lastVentaId != idVenta)
                            {
                                currentVenta = new Venta()
                                {
                                    idVenta = idVenta,
                                    idNegocio = Convert.ToInt32(dr["idNegocio"]),
                                    oUsuario = new Usuario() { nombreCompleto = dr["nombreCompleto"].ToString() },
                                    documentoCliente = dr["documentoCliente"].ToString(),
                                    tipoDocumento = dr["tipoDocumento"].ToString(),
                                    nombreCliente = dr["nombreCliente"].ToString(),
                                    nroDocumento = dr["nroDocumento"].ToString(),
                                    montoPago = Convert.ToDecimal(dr["montoPago"]),
                                    montoCambio = Convert.ToDecimal(dr["montoCambio"]),
                                    montoTotal = Convert.ToDecimal(dr["montoTotal"]),
                                    formaPago = dr["formaPago"].ToString(),
                                    descuento = Convert.ToInt32(dr["descuento"]),
                                    montoDescuento = Convert.ToDecimal(dr["montoDescuento"]),
                                    formaPago2 = dr["formaPago2"].ToString(),
                                    formaPago3 = dr["formaPago3"].ToString(),
                                    formaPago4 = dr["formaPago4"].ToString(),
                                    montoFP1 = Convert.ToDecimal(dr["montoFP1"]),
                                    montoFP2 = Convert.ToDecimal(dr["montoFP2"]),
                                    montoFP3 = Convert.ToDecimal(dr["montoFP3"]),
                                    montoFP4 = Convert.ToDecimal(dr["montoFP4"]),
                                    idVendedor = Convert.ToInt32(dr["idVendedor"]),
                                    oDetalleVenta = new List<DetalleVenta>(),
                                    nombreVendedor = dr["nombreCompletoVendedor"].ToString(),// Asignar el nombre completo del vendedor
                                    observaciones = dr["observaciones"].ToString()
                                };

                                listaVentas.Add(currentVenta);
                                lastVentaId = idVenta;
                            }

                            // Agregar detalles si existen
                            if (!dr.IsDBNull(dr.GetOrdinal("idProducto")))
                            {
                                currentVenta.oDetalleVenta.Add(new DetalleVenta()
                                {
                                    oProducto = new Producto()
                                    {
                                        idProducto = Convert.ToInt32(dr["idProducto"]),
                                        nombre = dr["productoNombre"].ToString()
                                    },
                                    precioVenta = Convert.ToDecimal(dr["precioVenta"]),
                                    cantidad = Convert.ToInt32(dr["cantidad"]),
                                    subTotal = Convert.ToDecimal(dr["subTotal"])
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    listaVentas = new List<Venta>();
                    // Manejo de errores (opcional)
                }
            }

            return listaVentas;
        }




        public List<DetalleVenta> ObtenerDetalleVenta(int idVenta)
        {
            List<DetalleVenta> oLista = new List<DetalleVenta>();
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("SELECT P.idProducto, P.nombre,DV.precioVenta,DV.cantidad,DV.subTotal FROM DETALLE_VENTA DV");
                    query.AppendLine("INNER JOIN  PRODUCTO P ON P.idProducto= DV.idProducto");
                    query.AppendLine("WHERE DV.idVenta=@idVenta");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@idVenta", idVenta);
                    cmd.CommandType = CommandType.Text;


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            oLista.Add(new DetalleVenta()
                            {

                                oProducto = new Producto() { idProducto= Convert.ToInt32(dr["idProducto"]), nombre = dr["nombre"].ToString() },
                                precioVenta = Convert.ToDecimal(dr["precioVenta"].ToString()),
                                cantidad = Convert.ToInt32(dr["cantidad"].ToString()),
                                subTotal = Convert.ToDecimal(dr["subTotal"].ToString())
                            });


                        }
                    }

                }
            }
            catch (Exception)
            {

                oLista = new List<DetalleVenta>();
            }
            return oLista;
        }

        public List<Venta> ObtenerVentasConDetalleEntreFechas(int idNegocio, DateTime fechaInicio, DateTime fechaFin)
        {
            List<Venta> listaVentas = new List<Venta>();

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT v.idVenta, v.idNegocio, u.nombreCompleto, v.documentoCliente, v.nombreCliente, v.tipoDocumento, v.nroDocumento,v.observaciones,");
                    query.AppendLine("v.montoPago, v.montoCambio, v.montoTotal, CONVERT(char(10), v.fechaRegistro, 103) [FechaRegistro], v.formaPago,");
                    query.AppendLine("v.descuento, v.montoDescuento, v.montoFP1, v.montoFP2, v.montoFP3, v.montoFP4, v.formaPago2, v.formaPago3, v.formaPago4,");
                    query.AppendLine("v.idVendedor, dv.idProducto, p.nombre AS productoNombre, dv.precioVenta, dv.cantidad, dv.subTotal,");
                    query.AppendLine("vdr.nombre + ' ' + vdr.apellido AS nombreCompletoVendedor");  // Concatenamos nombre y apellido del vendedor
                    query.AppendLine("FROM VENTA v");
                    query.AppendLine("INNER JOIN USUARIO u ON u.idUsuario = v.idUsuario");
                    query.AppendLine("LEFT JOIN DETALLE_VENTA dv ON dv.idVenta = v.idVenta");
                    query.AppendLine("LEFT JOIN PRODUCTO p ON p.idProducto = dv.idProducto");
                    query.AppendLine("LEFT JOIN VENDEDOR vdr ON vdr.idVendedor = v.idVendedor");  // Join con la tabla vendedores
                    query.AppendLine("WHERE v.idNegocio = @idNegocio");
                    query.AppendLine("AND v.fechaRegistro BETWEEN @fechaInicio AND @fechaFin");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@idNegocio", idNegocio);  // Pasar el parámetro idNegocio
                    cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);  // Pasar la fecha de inicio
                    cmd.Parameters.AddWithValue("@fechaFin", fechaFin);  // Pasar la fecha de fin
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        int? lastVentaId = null;
                        Venta currentVenta = null;

                        while (dr.Read())
                        {
                            int idVenta = Convert.ToInt32(dr["idVenta"]);

                            // Si es una nueva venta
                            if (lastVentaId == null || lastVentaId != idVenta)
                            {
                                currentVenta = new Venta()
                                {
                                    idVenta = idVenta,
                                    idVendedor = Convert.ToInt32(dr["idVendedor"]),
                                    idNegocio = Convert.ToInt32(dr["idNegocio"]),
                                    oUsuario = new Usuario() { nombreCompleto = dr["nombreCompleto"].ToString() },
                                    documentoCliente = dr["documentoCliente"].ToString(),
                                    tipoDocumento = dr["tipoDocumento"].ToString(),
                                    nombreCliente = dr["nombreCliente"].ToString(),
                                    nroDocumento = dr["nroDocumento"].ToString(),
                                    montoPago = Convert.ToDecimal(dr["montoPago"]),
                                    montoCambio = Convert.ToDecimal(dr["montoCambio"]),
                                    montoTotal = Convert.ToDecimal(dr["montoTotal"]),
                                    formaPago = dr["formaPago"].ToString(),
                                    descuento = Convert.ToInt32(dr["descuento"]),
                                    montoDescuento = Convert.ToDecimal(dr["montoDescuento"]),
                                    fechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]),
                                    formaPago2 = dr["formaPago2"].ToString(),
                                    formaPago3 = dr["formaPago3"].ToString(),
                                    formaPago4 = dr["formaPago4"].ToString(),
                                    montoFP1 = Convert.ToDecimal(dr["montoFP1"]),
                                    montoFP2 = Convert.ToDecimal(dr["montoFP2"]),
                                    montoFP3 = Convert.ToDecimal(dr["montoFP3"]),
                                    montoFP4 = Convert.ToDecimal(dr["montoFP4"]),
                                    oDetalleVenta = new List<DetalleVenta>(),
                                    nombreVendedor = dr["nombreCompletoVendedor"].ToString(),  // Asignar el nombre del vendedor
                                    observaciones = dr["observaciones"].ToString()
                                };

                                listaVentas.Add(currentVenta);
                                lastVentaId = idVenta;
                            }

                            // Agregar detalles si existen
                            if (!dr.IsDBNull(dr.GetOrdinal("idProducto")))
                            {
                                currentVenta.oDetalleVenta.Add(new DetalleVenta()
                                {
                                    oProducto = new Producto()
                                    {
                                        idProducto = Convert.ToInt32(dr["idProducto"]),
                                        nombre = dr["productoNombre"].ToString()
                                    },
                                    precioVenta = Convert.ToDecimal(dr["precioVenta"]),
                                    cantidad = Convert.ToInt32(dr["cantidad"]),
                                    subTotal = Convert.ToDecimal(dr["subTotal"])
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    listaVentas = new List<Venta>();
                    // Manejo de errores (opcional)
                }
            }

            return listaVentas;
        }


        public List<Venta> ObtenerVentasConDetalle()
        {
            List<Venta> listaVentas = new List<Venta>();

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT v.idVenta,v.idNegocio,v.observaciones, v.idVendedor, u.nombreCompleto,v.documentoCliente,v.nombreCliente,v.tipoDocumento,v.nroDocumento,v.montoPago,v.montoCambio,v.montoTotal, convert(char(10), v.fechaRegistro, 103)[FechaRegistro],v.formaPago,v.descuento,v.montoDescuento, v.montoFP1,v.montoFP2,v.montoFP3,v.montoFP4,v.formaPago2,v.formaPago3,v.formaPago4");
                    query.AppendLine("FROM VENTA v");
                    query.AppendLine("INNER JOIN USUARIO U ON U.idUsuario = v.idUsuario");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Venta objVenta = new Venta()
                            {
                                idVenta = Convert.ToInt32(dr["idVenta"]),
                                idNegocio = Convert.ToInt32(dr["idNegocio"]),
                                oUsuario = new Usuario() { nombreCompleto = dr["nombreCompleto"].ToString() },
                                documentoCliente = dr["documentoCliente"].ToString(),
                                tipoDocumento = dr["tipoDocumento"].ToString(),
                                nombreCliente = dr["nombreCliente"].ToString(),
                                nroDocumento = dr["nroDocumento"].ToString(),
                                montoPago = Convert.ToDecimal(dr["montoPago"].ToString()),
                                montoCambio = Convert.ToDecimal(dr["montoCambio"].ToString()),
                                montoTotal = Convert.ToDecimal(dr["montoTotal"].ToString()),
                                formaPago = dr["formaPago"].ToString(),
                                descuento = Convert.ToInt32(dr["descuento"]),
                                montoDescuento = Convert.ToDecimal(dr["montoDescuento"].ToString()),
                                fechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]),
                                formaPago2 = dr["formaPago2"].ToString(),
                                formaPago3 = dr["formaPago3"].ToString(),
                                formaPago4 = dr["formaPago4"].ToString(),
                                montoFP1 = Convert.ToDecimal(dr["montoFP1"].ToString()),
                                montoFP2 = Convert.ToDecimal(dr["montoFP2"].ToString()),
                                montoFP3 = Convert.ToDecimal(dr["montoFP3"].ToString()),
                                montoFP4 = Convert.ToDecimal(dr["montoFP4"].ToString()),
                                idVendedor = Convert.ToInt32(dr["idVendedor"]),
                                observaciones = dr["observaciones"].ToString(),
                            };

                            // Obtener los detalles de venta
                            objVenta.oDetalleVenta = ObtenerDetalleVenta(objVenta.idVenta);

                            listaVentas.Add(objVenta);
                        }
                    }
                }
                catch (Exception ex)
                {
                    listaVentas = new List<Venta>();
                }
            }

            return listaVentas;
        }

        public void EliminarVentaConDetalle(int idVenta)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                conexion.Open();
                SqlTransaction transaction = conexion.BeginTransaction();

                try
                {
                    // Eliminar los detalles de la venta
                    SqlCommand deleteDetalleCmd = new SqlCommand(@"
                DELETE FROM DETALLE_VENTA 
                WHERE idVenta = @idVenta", conexion, transaction);
                    deleteDetalleCmd.Parameters.AddWithValue("@idVenta", idVenta);
                    deleteDetalleCmd.ExecuteNonQuery();

                    // Eliminar la venta
                    SqlCommand deleteVentaCmd = new SqlCommand(@"
                DELETE FROM VENTA 
                WHERE idVenta = @idVenta", conexion, transaction);
                    deleteVentaCmd.Parameters.AddWithValue("@idVenta", idVenta);
                    deleteVentaCmd.ExecuteNonQuery();

                    // Confirmar la transacción
                    transaction.Commit();
                }
                catch (Exception)
                {
                    // Revertir la transacción en caso de error
                    transaction.Rollback();
                    throw; // Re-lanzar la excepción para manejarla fuera del método si es necesario
                }
            }
        }

    }
}
