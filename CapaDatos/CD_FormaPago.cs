using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CapaDatos
{
    public class CD_FormaPago
    {
        public List<FormaPago> ListarFormasDePago()
        {
            List<FormaPago> lista = new List<FormaPago>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT idFormaPago, descripcion, porcentajeRetencion , cajaAsociada FROM FORMAPAGO");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new FormaPago()
                            {
                                idFormaPago = Convert.ToInt32(dr["idFormaPago"]),
                                descripcion = dr["descripcion"].ToString(),
                                porcentajeRetencion = Convert.ToDecimal(dr["porcentajeRetencion"]),
                                cajaAsociada = dr["cajaAsociada"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<FormaPago>();
                    // Log the exception if needed
                    // Console.WriteLine(ex.Message);
                }
            }
            return lista;
        }


        public int RegistrarFormaPago(FormaPago objFormaPago, out string mensaje)
        {
            int idFormaPagoGenerado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARFORMAPAGO", oconexion);
                    cmd.Parameters.AddWithValue("descripcion", objFormaPago.descripcion);
                    cmd.Parameters.AddWithValue("porcentajeRetencion", objFormaPago.porcentajeRetencion);
                    cmd.Parameters.AddWithValue("cajaAsociada", objFormaPago.cajaAsociada);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idFormaPagoGenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idFormaPagoGenerado = 0;
                mensaje = ex.Message;
            }

            return idFormaPagoGenerado;
        }


        public bool EditarFormaPago(FormaPago objFormaPago, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITARFORMAPAGO", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idFormaPago", objFormaPago.idFormaPago);
                    cmd.Parameters.AddWithValue("descripcion", objFormaPago.descripcion);
                    cmd.Parameters.AddWithValue("cajaAsociada", objFormaPago.cajaAsociada);
                    cmd.Parameters.AddWithValue("porcentajeRetencion", objFormaPago.porcentajeRetencion);

                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = "Error al editar la forma de pago: " + ex.Message;
            }

            return resultado;
        }


        public FormaPago ObtenerFPPorDescripcion(string descripcion)
        {
            FormaPago formaPago = null;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = "SELECT idFormaPago, descripcion, porcentajeRetencion, cajaAsociada FROM FORMAPAGO WHERE descripcion = @descripcion";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            formaPago = new FormaPago()
                            {
                                idFormaPago = Convert.ToInt32(dr["idformapago"]),
                                descripcion = dr["descripcion"].ToString(),
                                porcentajeRetencion = Convert.ToDecimal(dr["porcentajeRetencion"]),
                                cajaAsociada = dr["cajaAsociada"].ToString(),
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    throw new Exception("Error al obtener la forma de pago por descripción", ex);
                }
            }

            return formaPago;
        }


    }
}
