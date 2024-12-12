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
    public class CD_PrecioProducto
    {
        public List<PrecioProducto> ListarPreciosProducto()
        {
            List<PrecioProducto> lista = new List<PrecioProducto>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_LISTARPRECIOPRODUCTOS", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new PrecioProducto()
                            {
                                IdPrecioProducto = Convert.ToInt32(dr["idPrecioProducto"]),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                NombreMoneda = dr["NombreMoneda"].ToString(),
                                SimboloMoneda = dr["simbolo"].ToString(),
                                PrecioCompra = Convert.ToDecimal(dr["precioCompra"]),
                                PrecioVenta = Convert.ToDecimal(dr["precioVenta"]),
                                PrecioLista = Convert.ToDecimal(dr["precioLista"]),
                                PrecioEfectivo = Convert.ToDecimal(dr["precioEfectivo"]),
                                FechaRegistro = Convert.ToDateTime(dr["fechaRegistro"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<PrecioProducto>();
            }

            return lista;
        }

        public PrecioProducto ObtenerPreciosPorProductoYMoneda(int idProducto, int idMoneda)
        {
            PrecioProducto precioProducto = null;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_OBTENERPRECIOPORPRODUCTOYMONEDA", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                    cmd.Parameters.AddWithValue("@IdMoneda", idMoneda);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            precioProducto = new PrecioProducto()
                            {
                                IdPrecioProducto = Convert.ToInt32(dr["idPrecioProducto"]),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                NombreMoneda = dr["NombreMoneda"].ToString(),
                                SimboloMoneda = dr["simbolo"].ToString(),
                                PrecioCompra = Convert.ToDecimal(dr["precioCompra"]),
                                PrecioVenta = Convert.ToDecimal(dr["precioVenta"]),
                                PrecioLista = Convert.ToDecimal(dr["precioLista"]),
                                PrecioEfectivo = Convert.ToDecimal(dr["precioEfectivo"]),
                                FechaRegistro = Convert.ToDateTime(dr["fechaRegistro"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción si es necesario
                precioProducto = null;
            }

            return precioProducto;
        }
        public int RegistrarPrecioProducto(PrecioProducto objPrecioProducto, out string mensaje)
        {
            int idPrecioProductoGenerado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARPRECIOPRODUCTO", oconexion);
                    cmd.Parameters.AddWithValue("idProducto", objPrecioProducto.IdProducto);
                    cmd.Parameters.AddWithValue("idMoneda", objPrecioProducto.IdMoneda);
                    cmd.Parameters.AddWithValue("precioCompra", objPrecioProducto.PrecioCompra);
                    cmd.Parameters.AddWithValue("precioVenta", objPrecioProducto.PrecioVenta);
                    cmd.Parameters.AddWithValue("precioLista", objPrecioProducto.PrecioLista);
                    cmd.Parameters.AddWithValue("precioEfectivo", objPrecioProducto.PrecioEfectivo);
                    cmd.Parameters.AddWithValue("fecha", objPrecioProducto.FechaRegistro);

                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    idPrecioProductoGenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idPrecioProductoGenerado = 0;
                mensaje = ex.Message;
            }

            return idPrecioProductoGenerado;
        }


        public bool EditarPrecioProducto(PrecioProducto objPrecioProducto, out string mensaje)
        
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITARPRECIOPRODUCTO", oconexion);
                    cmd.Parameters.AddWithValue("idPrecioProducto", objPrecioProducto.IdPrecioProducto);
                    cmd.Parameters.AddWithValue("idProducto", objPrecioProducto.IdProducto);
                    cmd.Parameters.AddWithValue("idMoneda", objPrecioProducto.IdMoneda);
                    cmd.Parameters.AddWithValue("precioCompra", objPrecioProducto.PrecioCompra);
                    cmd.Parameters.AddWithValue("precioVenta", objPrecioProducto.PrecioVenta);
                    cmd.Parameters.AddWithValue("precioLista", objPrecioProducto.PrecioLista);
                    cmd.Parameters.AddWithValue("precioEfectivo", objPrecioProducto.PrecioEfectivo);

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

        public bool EliminarPrecioProducto(int idPrecioProducto, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINARPRECIOPRODUCTO", oconexion);
                    cmd.Parameters.AddWithValue("idPrecioProducto", idPrecioProducto);

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
