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
    public class CD_Concepto
    {

        public List<Concepto> Listar()
        {
            List<Concepto> lista = new List<Concepto>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT idConcepto, descripcion, tipo, estado, fechaRegistro FROM CONCEPTO");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Concepto()
                            {
                                idConcepto = Convert.ToInt32(dr["idConcepto"]),
                                descripcion = dr["descripcion"].ToString(),
                                estado = Convert.ToBoolean(dr["estado"]),
                                fechaRegistro = Convert.ToDateTime(dr["fechaRegistro"]),
                                tipo = dr["tipo"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Concepto>();
                }
            }
            return lista;
        }


        public int Registrar(Concepto objConcepto, out string mensaje)
        {
            int idConcpetoGenerado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARCONCEPTO", oconexion);
                    cmd.Parameters.AddWithValue("descripcion", objConcepto.descripcion);
                    cmd.Parameters.AddWithValue("estado", objConcepto.estado);
                    cmd.Parameters.AddWithValue("fechaRegistro", objConcepto.fechaRegistro);
                    cmd.Parameters.AddWithValue("tipo", objConcepto.tipo);

                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idConcpetoGenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idConcpetoGenerado = 0;
                mensaje = ex.Message;
            }

            return idConcpetoGenerado;
        }

        public bool Editar(Concepto objConcepto, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITARCONCEPTO", oconexion);
                    cmd.Parameters.AddWithValue("idConcepto", objConcepto.idConcepto);
                    cmd.Parameters.AddWithValue("descripcion", objConcepto.descripcion);
                    cmd.Parameters.AddWithValue("estado", objConcepto.estado);
                    cmd.Parameters.AddWithValue("fechaRegistro", objConcepto.fechaRegistro);
                    cmd.Parameters.AddWithValue("tipo", objConcepto.tipo);

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

        public bool Eliminar(Concepto objConcepto, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINARCONCEPTO", oconexion);
                    cmd.Parameters.AddWithValue("idConcepto", objConcepto.idConcepto);
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
