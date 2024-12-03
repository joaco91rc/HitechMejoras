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
    public class CD_Moneda
    {
        public List<Moneda> ListarMonedas()
        {
            List<Moneda> lista = new List<Moneda>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_LISTARMONEDAS", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Moneda()
                            {
                                IdMoneda = Convert.ToInt32(dr["idMoneda"]),
                                Nombre = dr["nombre"].ToString(),
                                Simbolo = dr["simbolo"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Moneda>();
            }

            return lista;
        }

        public Moneda ObtenerMonedaPorId(int idMoneda)
        {
            Moneda moneda = null;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_OBTENEMONEDA", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idMoneda", idMoneda);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            moneda = new Moneda()
                            {
                                IdMoneda = Convert.ToInt32(dr["idMoneda"]),
                                Nombre = dr["nombre"].ToString(),
                                Simbolo = dr["simbolo"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                moneda = null;
            }

            return moneda;
        }
        public int RegistrarMoneda(Moneda objMoneda, out string mensaje)
        {
            int idMonedaGenerada = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARMONEDA", oconexion);
                    cmd.Parameters.AddWithValue("nombre", objMoneda.Nombre);
                    cmd.Parameters.AddWithValue("simbolo", objMoneda.Simbolo);

                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    idMonedaGenerada = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idMonedaGenerada = 0;
                mensaje = ex.Message;
            }

            return idMonedaGenerada;
        }

        public bool EditarMoneda(Moneda objMoneda, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITARMONEDA", oconexion);
                    cmd.Parameters.AddWithValue("idMoneda", objMoneda.IdMoneda);
                    cmd.Parameters.AddWithValue("nombre", objMoneda.Nombre);
                    cmd.Parameters.AddWithValue("simbolo", objMoneda.Simbolo);

                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
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

        public bool EliminarMoneda(int idMoneda, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINARMONEDA", oconexion);
                    cmd.Parameters.AddWithValue("idMoneda", idMoneda);

                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
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


    }
}
