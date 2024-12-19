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
    public class CD_PagoParcial
    {
        public List<PagoParcial> Listar()
        {
            List<PagoParcial> lista = new List<PagoParcial>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    string query = @"SELECT p.idPagoParcial, p.idCliente, p.moneda, c.nombreCompleto AS nombreCliente, 
                            p.monto, p.idVenta, p.vendedor, p.idNegocio,
                            ISNULL(v.nroDocumento, '') AS numeroVenta,
                            p.fechaRegistro, p.estado, p.formaPago, p.productoReservado
                     FROM PAGOPARCIAL p
                     INNER JOIN CLIENTE c ON p.idCliente = c.idCliente
                     LEFT JOIN VENTA v ON p.idVenta = v.idVenta";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int idNegocio = Convert.ToInt32(dr["idNegocio"]);
                            string nombreLocal;

                            if (idNegocio == 1)
                                nombreLocal = "HITECH 1";
                            else if (idNegocio == 2)
                                nombreLocal = "HITECH 2";
                            else if (idNegocio == 3)
                                nombreLocal = "APPLE 49";
                            else if (idNegocio == 4)
                                nombreLocal = "APPLE CAFE";
                            else
                                nombreLocal = "";

                            lista.Add(new PagoParcial()
                            {
                                idPagoParcial = Convert.ToInt32(dr["idPagoParcial"]),
                                idCliente = Convert.ToInt32(dr["idCliente"]),
                                nombreCliente = dr["nombreCliente"] != DBNull.Value ? dr["nombreCliente"].ToString() : "",
                                monto = Convert.ToDecimal(dr["monto"]),
                                idVenta = dr["idVenta"] != DBNull.Value ? (int?)Convert.ToInt32(dr["idVenta"]) : null,
                                numeroVenta = dr["numeroVenta"].ToString(),
                                fechaRegistro = Convert.ToDateTime(dr["fechaRegistro"]),
                                estado = Convert.ToBoolean(dr["estado"]),
                                formaPago = dr["formaPago"].ToString(),
                                productoReservado = dr["productoReservado"].ToString(),
                                vendedor = dr["vendedor"].ToString(),
                                idNegocio = idNegocio,
                                nombreLocal = nombreLocal,
                                moneda = dr["moneda"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<PagoParcial>();
                // Opcional: loggear el mensaje de error para revisión
            }

            return lista;
        }



        public List<PagoParcial> ListarPagosParcialesPorLocal(int idNegocio)
        {
            List<PagoParcial> lista = new List<PagoParcial>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    // Agrega el filtro de idNegocio en el WHERE
                    string query = @"SELECT p.idPagoParcial, p.moneda, p.idCliente, c.nombreCompleto AS nombreCliente, 
                            p.monto, p.idVenta, p.vendedor, p.idNegocio,
                            ISNULL(v.nroDocumento, '') AS numeroVenta, 
                            p.fechaRegistro, p.estado, p.formaPago, p.productoReservado
                     FROM PAGOPARCIAL p
                     INNER JOIN CLIENTE c ON p.idCliente = c.idCliente
                     LEFT JOIN VENTA v ON p.idVenta = v.idVenta
                     WHERE p.idNegocio = @idNegocio";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@idNegocio", idNegocio); // Parámetro para filtrar por idNegocio

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string nombreLocal;

                            // Asignación de nombre de local basado en idNegocio
                            if (idNegocio == 1)
                                nombreLocal = "HITECH 1";
                            else if (idNegocio == 2)
                                nombreLocal = "HITECH 2";
                            else if (idNegocio == 3)
                                nombreLocal = "APPLE 49";
                            else if (idNegocio == 4)
                                nombreLocal = "APPLE CAFE";
                            else
                                nombreLocal = "Desconocido";

                            lista.Add(new PagoParcial()
                            {
                                idPagoParcial = Convert.ToInt32(dr["idPagoParcial"]),
                                idCliente = Convert.ToInt32(dr["idCliente"]),
                                nombreCliente = dr["nombreCliente"] != DBNull.Value ? dr["nombreCliente"].ToString() : "",
                                monto = Convert.ToDecimal(dr["monto"]),
                                idVenta = dr["idVenta"] != DBNull.Value ? (int?)Convert.ToInt32(dr["idVenta"]) : null,
                                numeroVenta = dr["numeroVenta"].ToString(),
                                fechaRegistro = Convert.ToDateTime(dr["fechaRegistro"]),
                                estado = Convert.ToBoolean(dr["estado"]),
                                formaPago = dr["formaPago"].ToString(),
                                productoReservado = dr["productoReservado"].ToString(),
                                vendedor = dr["vendedor"].ToString(),
                                idNegocio = idNegocio,
                                nombreLocal = nombreLocal, // Asigna el nombre del local
                                moneda = dr["moneda"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<PagoParcial>();
                // Opcional: loggear el mensaje de error para revisión
            }

            return lista;
        }



        public bool DarDeBajaPagoParcial(int idPagoParcial)
        {
            bool exito = false;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    string query = @"UPDATE PAGOPARCIAL
                             SET estado = 0
                             WHERE idPagoParcial = @idPagoParcial";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@idPagoParcial", idPagoParcial);

                    oconexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    // Si al menos una fila fue afectada, consideramos que la operación fue exitosa
                    exito = filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                // Opcional: loggear el mensaje de error para revisión
                exito = false;
            }

            return exito;
        }




        public int RegistrarPagoParcial(PagoParcial objPagoParcial, out string mensaje)
        {
            int idPagoParcialGenerado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARPAGOPARCIAL", oconexion);
                    cmd.Parameters.AddWithValue("idCliente", objPagoParcial.idCliente);
                    cmd.Parameters.AddWithValue("monto", objPagoParcial.monto);
                    cmd.Parameters.AddWithValue("moneda", objPagoParcial.moneda);
                    cmd.Parameters.AddWithValue("estado", objPagoParcial.estado);
                    cmd.Parameters.AddWithValue("formaPago", objPagoParcial.formaPago);
                    cmd.Parameters.AddWithValue("fecha", objPagoParcial.fechaRegistro);
                    cmd.Parameters.AddWithValue("idNegocio", objPagoParcial.idNegocio);
                    cmd.Parameters.AddWithValue("productoReservado", objPagoParcial.productoReservado);
                    cmd.Parameters.AddWithValue("vendedor", objPagoParcial.vendedor);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    idPagoParcialGenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idPagoParcialGenerado = 0;
                mensaje = ex.Message;
            }

            return idPagoParcialGenerado;
        }


        public bool ModificarPagoParcial(PagoParcial objPagoParcial, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_MODIFICARPAGOPARCIAL", oconexion);
                    cmd.Parameters.AddWithValue("idPagoParcial", objPagoParcial.idPagoParcial);
                    cmd.Parameters.AddWithValue("monto", objPagoParcial.monto);
                    cmd.Parameters.AddWithValue("moneda", objPagoParcial.moneda);
                    cmd.Parameters.AddWithValue("estado", objPagoParcial.estado);
                    cmd.Parameters.AddWithValue("formaPago", objPagoParcial.formaPago);
                    cmd.Parameters.AddWithValue("formaPago", objPagoParcial.idNegocio);
                    cmd.Parameters.AddWithValue("fecha", objPagoParcial.fechaRegistro);
                    cmd.Parameters.AddWithValue("idCliente", objPagoParcial.idCliente);
                    cmd.Parameters.AddWithValue("productoReservado", objPagoParcial.productoReservado);
                    cmd.Parameters.AddWithValue("vendedor", objPagoParcial.vendedor);

                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return resultado;
        }


        public bool EliminarPagoParcial(int idPagoParcial, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINARPAGOPARCIAL", oconexion);
                    cmd.Parameters.AddWithValue("idPagoParcial", idPagoParcial);

                    cmd.Parameters.Add("respuesta", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["respuesta"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return resultado;
        }

        public List<PagoParcial> ConsultarPagosParcialesPorCliente(int idCliente)
        {
            List<PagoParcial> listaPagosParciales = new List<PagoParcial>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_CONSULTARPAGOPARCIAL", oconexion);
                    cmd.Parameters.AddWithValue("idCliente", idCliente);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            bool estado = Convert.ToBoolean(dr["estado"]);
                            if (estado) // Solo agregar si estado es true (pendiente)
                            {
                                listaPagosParciales.Add(new PagoParcial()
                                {
                                    idPagoParcial = Convert.ToInt32(dr["idPagoParcial"]),
                                    idCliente = Convert.ToInt32(dr["idCliente"]),
                                    monto = Convert.ToDecimal(dr["monto"]),
                                    fechaRegistro = Convert.ToDateTime(dr["fechaRegistro"]),
                                    estado = estado,
                                    nombreCliente = dr["NombreCliente"].ToString(),
                                    numeroVenta = dr["NumeroVenta"].ToString(),
                                    productoReservado = dr["productoReservado"].ToString(),
                                    vendedor = dr["vendedor"].ToString(),
                                    moneda = dr["moneda"].ToString(),
                                    formaPago = dr["formaPago"].ToString(),
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listaPagosParciales = new List<PagoParcial>(); // Si ocurre un error, devolvemos una lista vacía
            }

            return listaPagosParciales;
        }


    }
}
