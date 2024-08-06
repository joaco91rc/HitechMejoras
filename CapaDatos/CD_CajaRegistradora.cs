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
    public class CD_CajaRegistradora
    {


        public int AperturaCaja(CajaRegistradora objCajaRegistradora, out string mensaje, int idNegocio)
        {
            int idCajaGenerado = 0;
            mensaje = string.Empty;

            try
            {



                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_APERTURACAJA", oconexion);
                    cmd.Parameters.AddWithValue("usuarioAperturaCaja", objCajaRegistradora.usuarioAperturaCaja);
                    cmd.Parameters.AddWithValue("estado", 1);
                    cmd.Parameters.AddWithValue("saldo", objCajaRegistradora.saldo);
                    cmd.Parameters.AddWithValue("saldoMP", objCajaRegistradora.saldoMP);
                    cmd.Parameters.AddWithValue("saldoUSS", objCajaRegistradora.saldoUSS);
                    cmd.Parameters.AddWithValue("saldoGalicia", objCajaRegistradora.saldoGalicia);
                    cmd.Parameters.AddWithValue("idNegocio", idNegocio);

                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idCajaGenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();


                }

            }

            catch (Exception ex)
            {
                idCajaGenerado = 0;
                mensaje = ex.Message;

            }


            return idCajaGenerado;

        }


        public CajaRegistradora ObtenerCajaPorFecha(string fecha , int idNegocio)
        {
            CajaRegistradora caja = new CajaRegistradora();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select * FROM CAJA_REGISTRADORA");
                    query.AppendLine("where CONVERT(DATE, fechaApertura) = @fecha AND idNegocio = @idNegocio ");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@idNegocio", idNegocio);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            caja.idCajaRegistradora = Convert.ToInt32(dr["idCajaRegistradora"]);
                            caja.fechaApertura = dr["fechaApertura"].ToString();
                            caja.fechaCierre = dr["fechaCierre"].ToString();
                            caja.usuarioAperturaCaja = dr["usuarioAperturaCaja"].ToString();
                            caja.estado = Convert.ToBoolean(dr["estado"]);
                            

                            // Verificar y convertir los valores nulos
                            if (dr["saldo"] != DBNull.Value)
                            {
                                caja.saldo = Convert.ToDecimal(dr["saldo"]);
                            }
                            if (dr["saldoMP"] != DBNull.Value)
                            {
                                caja.saldoMP = Convert.ToDecimal(dr["saldoMP"]);
                            }
                            if (dr["saldoUSS"] != DBNull.Value)
                            {
                                caja.saldoUSS = Convert.ToDecimal(dr["saldoUSS"]);
                            }
                            if (dr["saldoGalicia"] != DBNull.Value)
                            {
                                caja.saldoGalicia = Convert.ToDecimal(dr["saldoGalicia"]);
                            }

                            
                        }
                    }

                }
                catch (Exception ex)
                {
                    caja = new CajaRegistradora();
                }

            }
            return caja;
        }


        public List<CajaRegistradora> Listar(int idNegocio)
        {
            List<CajaRegistradora> lista = new List<CajaRegistradora>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select * FROM CAJA_REGISTRADORA WHERE idNegocio = @idNegocio");
                   
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idNegocio", idNegocio);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            CajaRegistradora caja = new CajaRegistradora()
                            {
                                idCajaRegistradora = Convert.ToInt32(dr["idCajaRegistradora"]),
                                fechaApertura = dr["fechaApertura"].ToString(),
                                fechaCierre = dr["fechaCierre"].ToString(),
                                usuarioAperturaCaja = dr["usuarioAperturaCaja"].ToString(),
                                estado = Convert.ToBoolean(dr["estado"]),
                                idNegocio = idNegocio
                                
                            };

                            // Verificar y convertir los valores nulos
                            if (dr["saldo"] != DBNull.Value)
                            {
                                caja.saldo = Convert.ToDecimal(dr["saldo"]);
                            }
                            if (dr["saldoMP"] != DBNull.Value)
                            {
                                caja.saldoMP = Convert.ToDecimal(dr["saldoMP"]);
                            }
                            if (dr["saldoUSS"] != DBNull.Value)
                            {
                                caja.saldoUSS = Convert.ToDecimal(dr["saldoUSS"]);
                            }
                            if (dr["saldoGalicia"] != DBNull.Value)
                            {
                                caja.saldoGalicia = Convert.ToDecimal(dr["saldoGalicia"]);
                            }

                            lista.Add(caja);
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<CajaRegistradora>();
                }

            }
            return lista;
        }



        public bool CerrarCaja(CajaRegistradora objCajaRegistradora, out string mensaje, int idNegocio)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {



                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_CERRARCAJA", oconexion);
                    cmd.Parameters.AddWithValue("idCajaRegistradora", objCajaRegistradora.idCajaRegistradora);
                    cmd.Parameters.AddWithValue("fechaCierre", Convert.ToDateTime(objCajaRegistradora.fechaCierre));
                    cmd.Parameters.AddWithValue("saldo", objCajaRegistradora.saldo);
                    cmd.Parameters.AddWithValue("saldoMP", objCajaRegistradora.saldoMP);
                    cmd.Parameters.AddWithValue("saldoUSS", objCajaRegistradora.saldoUSS);
                    cmd.Parameters.AddWithValue("saldoGalicia", objCajaRegistradora.saldoGalicia);
                    cmd.Parameters.AddWithValue("idNegocio", idNegocio);

                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();


                }

            }

            catch (Exception ex)
            {
                respuesta = false;
                mensaje = ex.Message;

            }


            return respuesta;

        }



    }
}
