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
    public class CD_Vendedor
    {

        public List<Vendedor> ListarVendedores()
        {
            List<Vendedor> lista = new List<Vendedor>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT idVendedor, dni, nombre, apellido, sueldoBase, sueldoComision, estado FROM VENDEDOR");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Vendedor()
                            {
                                idVendedor = Convert.ToInt32(dr["idVendedor"]),
                                DNI = dr["dni"].ToString(),
                                nombre = dr["nombre"].ToString(),
                                apellido = dr["apellido"].ToString(),
                                sueldoBase = Convert.ToDecimal(dr["sueldoBase"]),
                                sueldoComision = Convert.ToDecimal(dr["sueldoComision"]),
                                estado = Convert.ToBoolean(dr["estado"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Vendedor>();
                }
            }
            return lista;
        }

        public int RegistrarVendedor(Vendedor objVendedor, out string mensaje)
        {
            int idVendedorGenerado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARVENDEDOR", oconexion);
                    cmd.Parameters.AddWithValue("dni", objVendedor.DNI);
                    cmd.Parameters.AddWithValue("nombre", objVendedor.nombre);
                    cmd.Parameters.AddWithValue("apellido", objVendedor.apellido);
                    cmd.Parameters.AddWithValue("sueldoBase", objVendedor.sueldoBase);
                    cmd.Parameters.AddWithValue("sueldoComision", objVendedor.sueldoComision);
                    cmd.Parameters.AddWithValue("estado", objVendedor.estado);

                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idVendedorGenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idVendedorGenerado = 0;
                mensaje = ex.Message;
            }

            return idVendedorGenerado;
        }

        public bool EditarVendedor(Vendedor objVendedor, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_MODIFICARVENDEDOR", oconexion);
                    cmd.Parameters.AddWithValue("idVendedor", objVendedor.idVendedor);
                    cmd.Parameters.AddWithValue("dni", objVendedor.DNI);
                    cmd.Parameters.AddWithValue("nombre", objVendedor.nombre);
                    cmd.Parameters.AddWithValue("apellido", objVendedor.apellido);
                    cmd.Parameters.AddWithValue("sueldoBase", objVendedor.sueldoBase);
                    cmd.Parameters.AddWithValue("sueldoComision", objVendedor.sueldoComision);
                    cmd.Parameters.AddWithValue("estado", objVendedor.estado);

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

        public bool EliminarVendedor(Vendedor objVendedor, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINARVENDEDOR", oconexion);
                    cmd.Parameters.AddWithValue("idVendedor", objVendedor.idVendedor);
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
