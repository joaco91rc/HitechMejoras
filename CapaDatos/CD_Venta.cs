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


        public bool Registrar(Venta objventa, DataTable detalleVenta, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

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

                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();


                    respuesta = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();



                }
                catch (Exception ex)
                {
                    respuesta = false;
                    mensaje = ex.Message;
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
                    query.AppendLine("SELECT v.idVenta,v.idNegocio, u.nombreCompleto,v.documentoCliente,v.nombreCliente,v.tipoDocumento,v.nroDocumento,v.montoPago,v.montoCambio,v.montoTotal, convert(char(10), v.fechaRegistro, 103)[FechaRegistro],v.formaPago,v.descuento,v.montoDescuento, v.montoFP1,v.montoFP2,v.montoFP3,v.montoFP4,v.formaPago2,v.formaPago3,v.formaPago4");
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


        public List<DetalleVenta> ObtenerDetalleVenta(int idVenta)
        {
            List<DetalleVenta> oLista = new List<DetalleVenta>();
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("SELECT P.nombre,DV.precioVenta,DV.cantidad,DV.subTotal FROM DETALLE_VENTA DV");
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

                                oProducto = new Producto() { nombre = dr["nombre"].ToString() },
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


        public List<Venta> ObtenerVentasConDetalle()
        {
            List<Venta> listaVentas = new List<Venta>();

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT v.idVenta,v.idNegocio, u.nombreCompleto,v.documentoCliente,v.nombreCliente,v.tipoDocumento,v.nroDocumento,v.montoPago,v.montoCambio,v.montoTotal, convert(char(10), v.fechaRegistro, 103)[FechaRegistro],v.formaPago,v.descuento,v.montoDescuento, v.montoFP1,v.montoFP2,v.montoFP3,v.montoFP4,v.formaPago2,v.formaPago3,v.formaPago4");
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
