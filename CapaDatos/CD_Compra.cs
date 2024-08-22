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
    public class CD_Compra
    {

        public int ObtenerCorrelativo()
        {
            int idCorrelativo = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count (*) +1 from COMPRA");

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

        public bool Registrar(Compra objCompra, DataTable detalleCompra, out string mensaje, out int idCompraSalida)
        {
            bool respuesta = false;
            mensaje = string.Empty;
            idCompraSalida = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARCOMPRA", oconexion);
                    cmd.Parameters.AddWithValue("idUsuario", objCompra.oUsuario.idUsuario);
                    cmd.Parameters.AddWithValue("idNegocio", objCompra.idNegocio);
                    cmd.Parameters.AddWithValue("idProveedor", objCompra.oProveedor.idProveedor);
                    cmd.Parameters.AddWithValue("tipoDocumento", objCompra.tipoDocumento);
                    cmd.Parameters.AddWithValue("nroDocumento", objCompra.nroDocumento);
                    cmd.Parameters.AddWithValue("montoTotal", objCompra.montoTotal);
                    cmd.Parameters.AddWithValue("detalleCompra", detalleCompra);

                    // Nuevos campos
                    cmd.Parameters.AddWithValue("formaPago", objCompra.formaPago);
                    cmd.Parameters.AddWithValue("formaPago2", objCompra.formaPago2);
                    cmd.Parameters.AddWithValue("formaPago3", objCompra.formaPago3);
                    cmd.Parameters.AddWithValue("formaPago4", objCompra.formaPago4);
                    cmd.Parameters.AddWithValue("montoFP1", objCompra.montoFP1);
                    cmd.Parameters.AddWithValue("montoFP2", objCompra.montoFP2);
                    cmd.Parameters.AddWithValue("montoFP3", objCompra.montoFP3);
                    cmd.Parameters.AddWithValue("montoFP4", objCompra.montoFP4);
                    cmd.Parameters.AddWithValue("montoPago", objCompra.montoPago);
                    cmd.Parameters.AddWithValue("montoPagoFP2", objCompra.montoPagoFP2);
                    cmd.Parameters.AddWithValue("montoPagoFP3", objCompra.montoPagoFP3);
                    cmd.Parameters.AddWithValue("montoPagoFP4", objCompra.montoPagoFP4);

                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("idCompraSalida", SqlDbType.Int).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                    idCompraSalida = Convert.ToInt32(cmd.Parameters["idCompraSalida"].Value);
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    mensaje = ex.Message;
                }
            }

            return respuesta;
        }


        public List<Compra> ObtenerComprasConDetalle()
        {
            List<Compra> listaCompras = new List<Compra>();

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT C.idCompra,");
                    query.AppendLine("U.nombreCompleto,");
                    query.AppendLine("PR.documento, PR.razonSocial,");
                    query.AppendLine("C.tipoDocumento, C.idNegocio, C.nroDocumento, C.montoTotal, CONVERT(CHAR(10), C.fechaRegistro, 103) [fechaRegistro],");
                    query.AppendLine("C.formaPago, C.formaPago2, C.formaPago3, C.formaPago4,");
                    query.AppendLine("C.montoFP1, C.montoFP2, C.montoFP3, C.montoFP4,");
                    query.AppendLine("C.montoPago, C.montoPagoFP2, C.montoPagoFP3, C.montoPagoFP4");
                    query.AppendLine("FROM COMPRA C");
                    query.AppendLine("INNER JOIN USUARIO U ON U.idUsuario = C.idUsuario");
                    query.AppendLine("INNER JOIN PROVEEDOR PR ON PR.idProveedor = C.idProveedor");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Compra objCompra = new Compra()
                            {
                                idCompra = Convert.ToInt32(dr["idCompra"]),
                                idNegocio = Convert.ToInt32(dr["idNegocio"]),
                                oUsuario = new Usuario() { nombreCompleto = dr["nombreCompleto"].ToString() },
                                oProveedor = new Proveedor() { documento = dr["documento"].ToString(), razonSocial = dr["razonSocial"].ToString() },
                                tipoDocumento = dr["tipoDocumento"].ToString(),
                                nroDocumento = dr["nroDocumento"].ToString(),
                                montoTotal = Convert.ToDecimal(dr["montoTotal"].ToString()),
                                fechaRegistro = dr["fechaRegistro"].ToString(),
                                formaPago = dr["formaPago"].ToString(),
                                formaPago2 = dr["formaPago2"].ToString(),
                                formaPago3 = dr["formaPago3"].ToString(),
                                formaPago4 = dr["formaPago4"].ToString(),
                                montoFP1 = Convert.ToDecimal(dr["montoFP1"].ToString()),
                                montoFP2 = Convert.ToDecimal(dr["montoFP2"].ToString()),
                                montoFP3 = Convert.ToDecimal(dr["montoFP3"].ToString()),
                                montoFP4 = Convert.ToDecimal(dr["montoFP4"].ToString()),
                                montoPago = Convert.ToDecimal(dr["montoPago"].ToString()),
                                montoPagoFP2 = Convert.ToDecimal(dr["montoPagoFP2"].ToString()),
                                montoPagoFP3 = Convert.ToDecimal(dr["montoPagoFP3"].ToString()),
                                montoPagoFP4 = Convert.ToDecimal(dr["montoPagoFP4"].ToString())
                            };

                            // Obtener los detalles de compra
                            objCompra.oDetalleCompra = ObtenerDetalleCompra(objCompra.idCompra);

                            listaCompras.Add(objCompra);
                        }
                    }
                }
                catch (Exception ex)
                {
                    listaCompras = new List<Compra>();
                }
            }

            return listaCompras;
        }



        public Compra ObtenerCompra(string numero, int idNegocio)
        {
            Compra objCompra = new Compra();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

     

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT C.idCompra,");
                    query.AppendLine("U.nombreCompleto,");
                    query.AppendLine("PR.documento, PR.razonSocial,");
                    query.AppendLine("C.tipoDocumento, C.idNegocio,C.nroDocumento,C.montoTotal,convert(char(10), C.fechaRegistro, 103)[FechaRegistro],C.formaPago,C.formaPago2,C.formaPago3,C.formaPago4, C.montoFP1,C.montoFP2,C.montoFP3,C.montoFP4,C.montoPago,C.montoPagoFP2,C.montoPagoFP3,C.montoPagoFP4 ");
                    query.AppendLine("FROM COMPRA C");
                    query.AppendLine("INNER JOIN USUARIO U ON U.idUsuario = C.idUsuario");
                    query.AppendLine("INNER JOIN PROVEEDOR PR ON PR.idProveedor = C.idProveedor");
                    query.AppendLine("WHERE C.nroDocumento = @numero  AND c.idNegocio = @idNegocio");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.Parameters.AddWithValue("@idNegocio", idNegocio);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            objCompra = new Compra()
                            {
                                idCompra = Convert.ToInt32(dr["idCompra"]),
                                idNegocio = Convert.ToInt32(dr["idNegocio"]),
                                oUsuario = new Usuario() { nombreCompleto=dr["nombreCompleto"].ToString()},
                                oProveedor = new Proveedor() { documento = dr["documento"].ToString(),razonSocial= dr["razonSocial"].ToString()},
                                tipoDocumento = dr["tipoDocumento"].ToString(),
                                nroDocumento = dr["nroDocumento"].ToString(),
                                montoTotal = Convert.ToDecimal(dr["montoTotal"].ToString()),
                                fechaRegistro = dr["fechaRegistro"].ToString(),
                                formaPago = dr["formaPago"].ToString(),
                                formaPago2 = dr["formaPago2"].ToString(),
                                formaPago3 = dr["formaPago3"].ToString(),
                                formaPago4 = dr["formaPago4"].ToString(),
                                montoFP1 = Convert.ToDecimal(dr["montoFP1"].ToString()),
                                montoFP2 = Convert.ToDecimal(dr["montoFP2"].ToString()),
                                montoFP3 = Convert.ToDecimal(dr["montoFP3"].ToString()),
                                montoFP4 = Convert.ToDecimal(dr["montoFP4"].ToString()),
                                montoPago = Convert.ToDecimal(dr["montoPago"].ToString()),
                                montoPagoFP2 = Convert.ToDecimal(dr["montoPagoFP2"].ToString()),
                                montoPagoFP3 = Convert.ToDecimal(dr["montoPagoFP3"].ToString()),
                                montoPagoFP4 = Convert.ToDecimal(dr["montoPagoFP4"].ToString()),
                            };

                          
                        }
                    }

                }
                catch (Exception ex)
                {
                    objCompra = new Compra();
                }

            }



            return objCompra;
        }


        public List<DetalleCompra> ObtenerDetalleCompra(int idCompra)
        {
            List<DetalleCompra> oLista = new List<DetalleCompra>();
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("select P.nombre, DC.precioCompra,DC.cantidad,DC.montoTotal from DETALLE_COMPRA DC");
                    query.AppendLine("inner join PRODUCTO P ON P.idProducto=DC.idProducto");
                    query.AppendLine("WHERE DC.idCompra=@idCompra");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@idCompra", idCompra);
                    cmd.CommandType = CommandType.Text;


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            oLista.Add(new DetalleCompra()
                            {
                                
                                oProducto = new Producto() { nombre = dr["nombre"].ToString() },
                                precioCompra = Convert.ToDecimal(dr["precioCompra"].ToString()),
                                cantidad = Convert.ToInt32(dr["cantidad"].ToString()),
                                montoTotal = Convert.ToDecimal(dr["montoTotal"].ToString())
                            });


                        }
                    }

                }
            }
            catch (Exception)
            {

                oLista = new List<DetalleCompra>();
            }
            return oLista;
        }

        public void EliminarCompraConDetalle(int idCompra)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                conexion.Open();
                SqlTransaction transaction = conexion.BeginTransaction();

                try
                {
                    // Eliminar los detalles de la compra
                    SqlCommand deleteDetalleCmd = new SqlCommand(@"
                DELETE FROM DETALLE_COMPRA 
                WHERE idCompra = @idCompra", conexion, transaction);
                    deleteDetalleCmd.Parameters.AddWithValue("@idCompra", idCompra);
                    deleteDetalleCmd.ExecuteNonQuery();

                    // Eliminar la compra
                    SqlCommand deleteCompraCmd = new SqlCommand(@"
                DELETE FROM COMPRA 
                WHERE idCompra = @idCompra", conexion, transaction);
                    deleteCompraCmd.Parameters.AddWithValue("@idCompra", idCompra);
                    deleteCompraCmd.ExecuteNonQuery();

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

    

