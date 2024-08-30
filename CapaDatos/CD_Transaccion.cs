﻿using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Transaccion
    {

        public List<TransaccionCaja> Listar(int idCajaRegistradora, int idNegocio)
        {
            List<TransaccionCaja> lista = new List<TransaccionCaja>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT * FROM TRANSACCION_CAJA WHERE idCajaRegistradora = @idCajaRegistradora AND idNegocio = @idNegocio");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("idCajaRegistradora", idCajaRegistradora);
                    cmd.Parameters.AddWithValue("idNegocio", idNegocio);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new TransaccionCaja()
                            {
                                idTransaccion = Convert.ToInt32(dr["idTransaccion"]),
                                idCajaRegistradora = Convert.ToInt32(dr["idCajaRegistradora"]),
                                hora = dr["hora"].ToString(),
                                tipoTransaccion = dr["tipoTransaccion"].ToString(),
                                monto = Convert.ToDecimal(dr["monto"]),
                                formaPago = dr["formaPago"].ToString(),
                                cajaAsociada = dr["cajaAsociada"].ToString(),
                                docAsociado = dr["docAsociado"].ToString(),
                                usuarioTransaccion = dr["usuarioTransaccion"].ToString(),
                                idCompra = dr["idCompra"] != DBNull.Value ? Convert.ToInt32(dr["idCompra"]) : 0,
                                idVenta = dr["idVenta"] != DBNull.Value ? Convert.ToInt32(dr["idVenta"]) : 0,
                                idNegocio = dr["idNegocio"] != DBNull.Value ? Convert.ToInt32(dr["idNegocio"]) :0,
                                concepto = dr["concepto"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<TransaccionCaja>();
                }
            }
            return lista;
        }

        public int RegistrarMovimiento(TransaccionCaja objTransaccion, out string mensaje)
        {
            int idTransaccionGenerado = 0;
            mensaje = string.Empty;

            try
            {



                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARMOVIMIENTO", oconexion);
                    cmd.Parameters.AddWithValue("idCajaRegistradora", objTransaccion.idCajaRegistradora);
                    cmd.Parameters.AddWithValue("tipoTransaccion", objTransaccion.tipoTransaccion);
                    cmd.Parameters.AddWithValue("monto", objTransaccion.monto);
                    cmd.Parameters.AddWithValue("docAsociado", objTransaccion.docAsociado);
                    cmd.Parameters.AddWithValue("fecha", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("usuarioTransaccion", objTransaccion.usuarioTransaccion);
                    cmd.Parameters.AddWithValue("formaPago", objTransaccion.formaPago);
                    cmd.Parameters.AddWithValue("cajaAsociada", objTransaccion.cajaAsociada);
                    cmd.Parameters.AddWithValue("concepto", objTransaccion.concepto);
                    // Verificar si idVenta es NULL, si lo es, enviar 0
                    cmd.Parameters.AddWithValue("idVenta", objTransaccion.idVenta.HasValue ? objTransaccion.idVenta.Value : 0);

                    // Verificar si idCompra es NULL, si lo es, enviar 0
                    cmd.Parameters.AddWithValue("idCompra", objTransaccion.idCompra.HasValue ? objTransaccion.idCompra.Value : 0);
                    cmd.Parameters.AddWithValue("idNegocio", objTransaccion.idNegocio);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idTransaccionGenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();


                }

            }

            catch (Exception ex)
            {
                idTransaccionGenerado = 0;
                mensaje = ex.Message;

            }


            return idTransaccionGenerado;

        }

        public bool EditarMovimiento(TransaccionCaja objTransaccion, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITARMOVIMIENTO", oconexion);
                    cmd.Parameters.AddWithValue("idTransaccion", objTransaccion.idTransaccion);
                    cmd.Parameters.AddWithValue("idCajaRegistradora", objTransaccion.idCajaRegistradora);
                    cmd.Parameters.AddWithValue("tipoTransaccion", objTransaccion.tipoTransaccion);
                    cmd.Parameters.AddWithValue("monto", objTransaccion.monto);
                    cmd.Parameters.AddWithValue("docAsociado", objTransaccion.docAsociado);
                    cmd.Parameters.AddWithValue("fecha", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("usuarioTransaccion", objTransaccion.usuarioTransaccion);
                    cmd.Parameters.AddWithValue("formaPago", objTransaccion.formaPago);
                    cmd.Parameters.AddWithValue("cajaAsociada", objTransaccion.cajaAsociada);
                    cmd.Parameters.AddWithValue("concepto", objTransaccion.concepto);
                    cmd.Parameters.AddWithValue("idVenta", objTransaccion.idVenta.HasValue ? objTransaccion.idVenta.Value : 0);
                    cmd.Parameters.AddWithValue("idCompra", objTransaccion.idCompra.HasValue ? objTransaccion.idCompra.Value : 0);
                    cmd.Parameters.AddWithValue("idNegocio", objTransaccion.idNegocio);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    int rowsAffected = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();

                    if (rowsAffected > 0)
                    {
                        resultado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return resultado;
        }



        public bool EliminarMovimiento(int idTransaccion, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();
                    SqlTransaction transaction = oconexion.BeginTransaction();

                    try
                    {
                        // Eliminar el movimiento de la caja registradora
                        SqlCommand cmd = new SqlCommand(@"
                    DELETE FROM TRANSACCION_CAJA 
                    WHERE idTransaccion = @idTransaccion", oconexion, transaction);
                        cmd.Parameters.AddWithValue("@idTransaccion", idTransaccion);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            transaction.Commit();
                            resultado = true;
                            mensaje = "Movimiento eliminado correctamente.";
                        }
                        else
                        {
                            transaction.Rollback();
                            mensaje = "No se encontró el movimiento con el ID especificado.";
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        mensaje = "Ocurrió un error al eliminar el movimiento: " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Ocurrió un error al conectar con la base de datos: " + ex.Message;
            }

            return resultado;
        }

        public bool  EliminarMovimientoCajaYVenta(int idVenta, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();
                    SqlTransaction transaction = oconexion.BeginTransaction();

                    try
                    {
                        // Eliminar el movimiento de la caja registradora
                        SqlCommand cmd = new SqlCommand(@"
                    DELETE FROM TRANSACCION_CAJA 
                    WHERE idVenta = @idVenta", oconexion, transaction);
                        cmd.Parameters.AddWithValue("@idVenta", idVenta);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            transaction.Commit();
                            resultado = true;
                            mensaje = "Movimiento eliminado correctamente.";
                        }
                        else
                        {
                            transaction.Rollback();
                            mensaje = "No se encontró el movimiento con el ID especificado.";
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        mensaje = "Ocurrió un error al eliminar el movimiento: " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Ocurrió un error al conectar con la base de datos: " + ex.Message;
            }

            return resultado;
        }

        public  bool EliminarMovimientoCajaYCompra(int idCompra, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();
                    SqlTransaction transaction = oconexion.BeginTransaction();

                    try
                    {
                        // Eliminar el movimiento de la caja registradora
                        SqlCommand cmd = new SqlCommand(@"
                    DELETE FROM TRANSACCION_CAJA 
                    WHERE idCompra = @idCompra", oconexion, transaction);
                        cmd.Parameters.AddWithValue("@idCompra", idCompra);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            transaction.Commit();
                            resultado = true;
                            mensaje = "Movimiento eliminado correctamente.";
                        }
                        else
                        {
                            transaction.Rollback();
                            mensaje = "No se encontró el movimiento con el ID especificado.";
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        mensaje = "Ocurrió un error al eliminar el movimiento: " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Ocurrió un error al conectar con la base de datos: " + ex.Message;
            }

            return resultado;
        }
    }
}
