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
    public class CD_EquipoST
    {
        public int Registrar(EquipoST objEquipo, out string mensaje)
        {
            int idEquipoGenerado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRAREQUIPOST", oconexion);
                    cmd.Parameters.AddWithValue("nombre", objEquipo.nombre);
                    cmd.Parameters.AddWithValue("idNegocio", objEquipo.idNegocio);
                    cmd.Parameters.AddWithValue("tipoEquipo", objEquipo.tipoEquipo);
                    cmd.Parameters.AddWithValue("marca", objEquipo.marca);
                    cmd.Parameters.AddWithValue("modelo", objEquipo.modelo);
                    cmd.Parameters.AddWithValue("serialNumber", objEquipo.serialNumber);
                    cmd.Parameters.AddWithValue("idCliente", objEquipo.idCliente);

                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idEquipoGenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idEquipoGenerado = 0;
                mensaje = ex.Message;
            }

            return idEquipoGenerado;
        }

        public bool Editar(EquipoST objEquipo, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITAREQUIPOST", oconexion);
                    cmd.Parameters.AddWithValue("idEquipoST", objEquipo.idEquipoST);
                    cmd.Parameters.AddWithValue("nombre", objEquipo.nombre);
                    cmd.Parameters.AddWithValue("idNegocio", objEquipo.idNegocio);
                    cmd.Parameters.AddWithValue("tipoEquipo", objEquipo.tipoEquipo);
                    cmd.Parameters.AddWithValue("marca", objEquipo.marca);
                    cmd.Parameters.AddWithValue("modelo", objEquipo.modelo);
                    cmd.Parameters.AddWithValue("serialNumber", objEquipo.serialNumber);
                    cmd.Parameters.AddWithValue("idCliente", objEquipo.idCliente);

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

        public bool Eliminar(EquipoST objEquipo, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINAREQUIPOST", oconexion);
                    cmd.Parameters.AddWithValue("idEquipoST", objEquipo.idEquipoST);
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

        public List<EquipoST> Listar()
        {
            List<EquipoST> lista = new List<EquipoST>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_LISTAREQUIPOST", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new EquipoST()
                            {
                                idEquipoST = Convert.ToInt32(dr["idEquipoST"]),
                                nombre = dr["nombre"].ToString(),
                                tipoEquipo = dr["descripcion"].ToString(),
                                marca = dr["marca"].ToString(),
                                modelo = dr["modelo"].ToString(),
                                serialNumber = dr["serialNumber"].ToString(),
                                idCliente = Convert.ToInt32(dr["idCliente"]),
                                idNegocio = Convert.ToInt32(dr["idNegocio"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<EquipoST>();
            }

            return lista;
        }
    }

}

