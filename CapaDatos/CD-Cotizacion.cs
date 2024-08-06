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
  public class CD_Cotizacion
    {

        public Cotizacion CotizacionActiva()
        {
            Cotizacion cotizacionDolar = new Cotizacion();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select idCotizacion, importe, fecha, estado from COTIZACION ");
                    query.AppendLine("where estado = 1 ");




                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    ;
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            cotizacionDolar.idCotizacion = Convert.ToInt32(dr["idCotizacion"]);
                            cotizacionDolar.importe = Convert.ToDecimal(dr["importe"]);
                            cotizacionDolar.fecha = Convert.ToDateTime(dr["fecha"]);
                            cotizacionDolar.estado = Convert.ToBoolean(dr["estado"]);
                            




                        }
                    }

                }
                catch (Exception ex)
                {
                    cotizacionDolar = new Cotizacion();

                }

            }
            return cotizacionDolar;


        }


        public List<Cotizacion> HistoricoCotizaciones()
        {
            List<Cotizacion> lista = new List<Cotizacion>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select idCotizacion, importe, fecha, estado from COTIZACION ");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Cotizacion()
                            {
                                idCotizacion = Convert.ToInt32(dr["idCotizacion"]),
                                fecha = Convert.ToDateTime(dr["fecha"]),
                                importe= Convert.ToDecimal(dr["importe"]),
                                estado = Convert.ToBoolean(dr["estado"])

                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<Cotizacion>();
                }

            }
            return lista;
        }


        public int Registrar(Cotizacion objCotizacion, out string mensaje)
        {
            int idCotizaciongenerado = 0;
            mensaje = string.Empty;

            try
            {



                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARCOTIZACION", oconexion);

                    cmd.Parameters.AddWithValue("fecha", objCotizacion.fecha);
                    cmd.Parameters.AddWithValue("importe", objCotizacion.importe);
                    

                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idCotizaciongenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();


                }

            }

            catch (Exception ex)
            {
                idCotizaciongenerado = 0;
                mensaje = ex.Message;

            }


            return idCotizaciongenerado;

        }


        public bool Editar(Cotizacion objCotizacion, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {



                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITARCOTIZACION", oconexion);
                    cmd.Parameters.AddWithValue("idCotizacion", objCotizacion.idCotizacion);
                    

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
