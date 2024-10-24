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
                    string query = @"SELECT p.idPagoParcial, p.idCliente, c.nombreCompleto AS nombreCliente, p.monto, p.idVenta, 
                                    v.numeroVenta, p.fechaRegistro, p.estado, p.formaPago
                             FROM PAGOPARCIAL p
                             INNER JOIN CLIENTE c ON p.idCliente = c.idCliente
                             LEFT JOIN VENTA v ON p.idVenta = v.idVenta"; // LEFT JOIN porque puede no tener una venta asociada

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new PagoParcial()
                            {
                                idPagoParcial = Convert.ToInt32(dr["idPagoParcial"]),
                                idCliente = Convert.ToInt32(dr["idCliente"]),
                                nombreCliente = dr["nombreCliente"] != DBNull.Value ? dr["nombreCliente"].ToString() : "", // Asigna cadena vacía si es null
                                monto = Convert.ToDecimal(dr["monto"]),
                                idVenta = dr["idVenta"] != DBNull.Value ? (int?)Convert.ToInt32(dr["idVenta"]) : null,
                                numeroVenta = dr["numeroVenta"] != DBNull.Value ? dr["numeroVenta"].ToString() : "", // Asigna cadena vacía si es null
                                fechaRegistro = Convert.ToDateTime(dr["fechaRegistro"]),
                                estado = Convert.ToBoolean(dr["estado"]),
                                formaPago = dr["formaPago"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<PagoParcial>();
                // Manejo de la excepción si es necesario
            }

            return lista;
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
                    cmd.Parameters.AddWithValue("estado", objPagoParcial.estado);
                    cmd.Parameters.AddWithValue("formaPago", objPagoParcial.formaPago);
                    cmd.Parameters.AddWithValue("fecha", objPagoParcial.fechaRegistro);

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
                    cmd.Parameters.AddWithValue("estado", objPagoParcial.estado);
                    cmd.Parameters.AddWithValue("formaPago", objPagoParcial.formaPago);
                    cmd.Parameters.AddWithValue("fecha", objPagoParcial.fechaRegistro);
                    cmd.Parameters.AddWithValue("idCliente", objPagoParcial.idCliente);

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
                            listaPagosParciales.Add(new PagoParcial()
                            {
                                idPagoParcial = Convert.ToInt32(dr["idPagoParcial"]),
                                idCliente = Convert.ToInt32(dr["idCliente"]),
                                monto = Convert.ToDecimal(dr["monto"]),
                                fechaRegistro = Convert.ToDateTime(dr["fechaRegistro"]),
                                estado = Convert.ToBoolean(dr["estado"]),
                                nombreCliente = dr["NombreCliente"].ToString(),
                                numeroVenta = dr["NumeroVenta"].ToString(),
                            });
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
