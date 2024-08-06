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
    public class CD_ProductoRMA
    {
        public List<ProductoRMA> ListarProductosRMA()
        {
            List<ProductoRMA> lista = new List<ProductoRMA>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select idProductoRMA,fechaIngreso,descripcionProductoRMA, cantidad, estado, fechaEgreso, idProducto from productorma");
                    
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            DateTime fechaEgreso = DateTime.MinValue; // Valor por defecto para el campo fechaEgreso si es NULL

                            if (!Convert.IsDBNull(dr["fechaEgreso"]))
                            {
                                fechaEgreso = Convert.ToDateTime(dr["fechaEgreso"]);
                            }

                            lista.Add(new ProductoRMA()
                            {
                                idProductoRMA = Convert.ToInt32(dr["idProductoRMA"]),
                                fechaIngreso = Convert.ToDateTime(dr["fechaIngreso"]),
                                descripcionProductoRMA = dr["descripcionProductoRMA"].ToString(),
                                cantidad = Convert.ToInt32(dr["cantidad"]),
                                estado = dr["estado"].ToString(),
                                fechaEgreso = fechaEgreso, // Asignamos la fechaEgreso o DateTime.MinValue si es NULL
                                idProducto = Convert.ToInt32(dr["idProducto"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<ProductoRMA>();
                }

            }
            return lista;
        }


        public int RegistrarProductoXRMA(ProductoRMA objProductoRMA, out string mensaje)
        {
            int idProductoXRMAgenerado = 0;
            mensaje = string.Empty;

            try
            {



                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARPRODUCTORMA", oconexion);

                    cmd.Parameters.AddWithValue("fechaIngreso", objProductoRMA.fechaIngreso);
                    cmd.Parameters.AddWithValue("cantidad", objProductoRMA.cantidad);
                    cmd.Parameters.AddWithValue("estado", objProductoRMA.estado);
                    cmd.Parameters.AddWithValue("descripcionProductoRMA", objProductoRMA.descripcionProductoRMA);
                    cmd.Parameters.AddWithValue("idProducto", objProductoRMA.idProducto);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idProductoXRMAgenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();


                }

            }

            catch (Exception ex)
            {
                idProductoXRMAgenerado = 0;
                mensaje = ex.Message;

            }


            return idProductoXRMAgenerado;

        }

        public bool EditarProductoRMA(int idProductoRMA,string estado,DateTime fechaEgreso, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {



                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITARPRODUCTORMA", oconexion);
                    cmd.Parameters.AddWithValue("idProductoRMA", idProductoRMA);
                    cmd.Parameters.AddWithValue("estado", estado);
                    cmd.Parameters.AddWithValue("fechaEgreso", fechaEgreso);
                    

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


        public bool EliminarProductoXRMA(int idProductoRMA, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {



                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINARPRODUCTORMA", oconexion);
                    cmd.Parameters.AddWithValue("idProductoRMA", idProductoRMA);
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
