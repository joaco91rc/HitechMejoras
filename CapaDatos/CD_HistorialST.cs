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
    public class CD_HistorialST
    {

        public int Registrar(HistorialST objHistorial, out string mensaje)
        {
            int idHistorialGenerado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARHISTORIALST", oconexion);
                    cmd.Parameters.AddWithValue("idEquipoST", objHistorial.idEquipoST);
                    cmd.Parameters.AddWithValue("descripcion", objHistorial.descripcion);
                    cmd.Parameters.AddWithValue("fecha", objHistorial.fecha);
                    cmd.Parameters.AddWithValue("estado", objHistorial.estado);
                    cmd.Parameters.AddWithValue("tecnico", objHistorial.tecnico);

                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idHistorialGenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idHistorialGenerado = 0;
                mensaje = ex.Message;
            }

            return idHistorialGenerado;
        }

        public bool Editar(HistorialST objHistorial, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITARHISTORIALST", oconexion);
                    cmd.Parameters.AddWithValue("idHistorialST", objHistorial.idHistorialST);
                    cmd.Parameters.AddWithValue("idEquipoST", objHistorial.idEquipoST);
                    cmd.Parameters.AddWithValue("descripcion", objHistorial.descripcion);
                    cmd.Parameters.AddWithValue("fecha", objHistorial.fecha);
                    cmd.Parameters.AddWithValue("estado", objHistorial.estado);
                    cmd.Parameters.AddWithValue("tecnico", objHistorial.tecnico);

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

        public bool Eliminar(HistorialST objHistorial, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINARHISTORIALST", oconexion);
                    cmd.Parameters.AddWithValue("idHistorialST", objHistorial.idHistorialST);
                    cmd.Parameters.Add("respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["respuesta"].Value);
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

        public List<HistorialST> Listar()
        {
            List<HistorialST> lista = new List<HistorialST>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_LISTARHISTORIALST", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new HistorialST()
                            {
                                idHistorialST = Convert.ToInt32(dr["idHistorialST"]),
                                idEquipoST = Convert.ToInt32(dr["idEquipoST"]),
                                descripcion = dr["descripcion"].ToString(),
                                fecha = Convert.ToDateTime(dr["fecha"]),
                                estado= dr["estado"].ToString(),
                                tecnico = dr["tecnico"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<HistorialST>();
            }

            return lista;
        }

    }
}
