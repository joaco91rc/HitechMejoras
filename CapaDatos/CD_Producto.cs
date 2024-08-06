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
    public class CD_Producto
    {

        public List<Producto> Listar(int idNegocio)
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.idProducto, p.codigo, p.nombre, p.descripcion, c.idCategoria, c.descripcion[DescripcionCategoria],");
                    query.AppendLine("ISNULL(pn.stock, 0) as stock, p.precioCompra, p.precioVenta, p.estado, p.costoPesos");
                    query.AppendLine("from Producto p");
                    query.AppendLine("inner join CATEGORIA c on c.idCategoria = p.idCategoria");
                    query.AppendLine("left join PRODUCTONEGOCIO pn on pn.idProducto = p.idProducto and pn.idNegocio = @idNegocio");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idNegocio", idNegocio);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                codigo = dr["codigo"].ToString(),
                                nombre = dr["nombre"].ToString(),
                                descripcion = dr["descripcion"].ToString(),
                                oCategoria = new Categoria() { idCategoria = Convert.ToInt32(dr["idCategoria"]), descripcion = dr["DescripcionCategoria"].ToString() },
                                costoPesos = Convert.ToDecimal(dr["costoPesos"]),
                                precioCompra = Convert.ToDecimal(dr["precioCompra"]),
                                precioVenta = Convert.ToDecimal(dr["precioVenta"]),
                                estado = Convert.ToBoolean(dr["estado"]),
                                stock = Convert.ToInt32(dr["stock"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Producto>();
                }
            }
            return lista;
        }

        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT p.idProducto, p.codigo, p.nombre, p.descripcion, c.idCategoria, c.descripcion[DescripcionCategoria],");
                    query.AppendLine("ISNULL(SUM(CASE WHEN pn.idNegocio = 1 THEN pn.stock ELSE 0 END), 0) as stockH1,");
                    query.AppendLine("ISNULL(SUM(CASE WHEN pn.idNegocio = 2 THEN pn.stock ELSE 0 END), 0) as stockH2,");
                    query.AppendLine("ISNULL(SUM(CASE WHEN pn.idNegocio = 3 THEN pn.stock ELSE 0 END), 0) as stockAS,");
                    query.AppendLine("ISNULL(SUM(CASE WHEN pn.idNegocio = 4 THEN pn.stock ELSE 0 END), 0) as stockAC,");
                    query.AppendLine("p.precioCompra, p.precioVenta, p.estado, p.costoPesos");
                    query.AppendLine("FROM Producto p");
                    query.AppendLine("INNER JOIN CATEGORIA c ON c.idCategoria = p.idCategoria");
                    query.AppendLine("LEFT JOIN PRODUCTONEGOCIO pn ON pn.idProducto = p.idProducto");
                    query.AppendLine("GROUP BY p.idProducto, p.codigo, p.nombre, p.descripcion, c.idCategoria, c.descripcion, p.precioCompra, p.precioVenta, p.estado, p.costoPesos");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                codigo = dr["codigo"].ToString(),
                                nombre = dr["nombre"].ToString(),
                                descripcion = dr["descripcion"].ToString(),
                                oCategoria = new Categoria() { idCategoria = Convert.ToInt32(dr["idCategoria"]), descripcion = dr["DescripcionCategoria"].ToString() },
                                costoPesos = Convert.ToDecimal(dr["costoPesos"]),
                                precioCompra = Convert.ToDecimal(dr["precioCompra"]),
                                precioVenta = Convert.ToDecimal(dr["precioVenta"]),
                                estado = Convert.ToBoolean(dr["estado"]),
                                stockH1 = Convert.ToInt32(dr["stockH1"]),
                                stockH2 = Convert.ToInt32(dr["stockH2"]),
                                stockAS = Convert.ToInt32(dr["stockAS"]),
                                stockAC = Convert.ToInt32(dr["stockAC"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Producto>();
                }
            }
            return lista;
        }

        public Producto ObtenerProductoPorId(int idProducto)
        {
            Producto producto = null;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.idProducto, p.costoPesos, p.codigo, p.nombre, p.descripcion, c.idCategoria, c.descripcion[DescripcionCategoria], p.stock, p.precioCompra, p.precioVenta, p.estado");
                    query.AppendLine("from Producto p");
                    query.AppendLine("inner join CATEGORIA c on c.idCategoria = p.idCategoria");
                    query.AppendLine("where p.idProducto = @idProducto");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            producto = new Producto()
                            {
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                codigo = dr["codigo"].ToString(),
                                nombre = dr["nombre"].ToString(),
                                descripcion = dr["descripcion"].ToString(),
                                oCategoria = new Categoria() { idCategoria = Convert.ToInt32(dr["idCategoria"]), descripcion = dr["DescripcionCategoria"].ToString() },
                                costoPesos= Convert.ToDecimal(dr["costoPesos"]),
                                precioCompra = Convert.ToDecimal(dr["precioCompra"]),
                                precioVenta = Convert.ToDecimal(dr["precioVenta"]),
                                estado = Convert.ToBoolean(dr["estado"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    producto = null;
                }
            }
            return producto;
        }


        public int Registrar(Producto objProducto, out string mensaje)
        {
            int idProductoGenerado = 0;
            mensaje = string.Empty;

            try
            {



                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARPRODUCTO", oconexion);
                    cmd.Parameters.AddWithValue("codigo", objProducto.codigo);
                    cmd.Parameters.AddWithValue("nombre", objProducto.nombre);
                    cmd.Parameters.AddWithValue("descripcion", objProducto.descripcion);
                    cmd.Parameters.AddWithValue("idCategoria", objProducto.oCategoria.idCategoria);
                    
                    cmd.Parameters.AddWithValue("estado", objProducto.estado);
                    cmd.Parameters.AddWithValue("costoPesos", objProducto.costoPesos);
                    cmd.Parameters.AddWithValue("precioCompra", objProducto.precioCompra);
                    cmd.Parameters.AddWithValue("precioVenta", objProducto.precioVenta);

                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idProductoGenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();


                }

            }

            catch (Exception ex)
            {
                idProductoGenerado = 0;
                mensaje = ex.Message;

            }


            return idProductoGenerado;

        }


        public bool SumarStockPorRMA(int idProducto, int cantidad, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {



                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_SUMARPRODUCTOXRMA", oconexion);
                    cmd.Parameters.AddWithValue("idProducto", idProducto);
                    cmd.Parameters.AddWithValue("cantidad", cantidad);


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

        public bool RestarStockPorRMA(int idProducto, int cantidad, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {



                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITARPRODUCTOXRMA", oconexion);
                    cmd.Parameters.AddWithValue("idProducto", idProducto);
                    cmd.Parameters.AddWithValue("cantidad", cantidad);
                    

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



        public bool Editar(Producto objProducto, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {



                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITARPRODUCTO", oconexion);
                    cmd.Parameters.AddWithValue("idProducto", objProducto.idProducto);
                    cmd.Parameters.AddWithValue("codigo", objProducto.codigo);
                    cmd.Parameters.AddWithValue("nombre", objProducto.nombre);
                    cmd.Parameters.AddWithValue("descripcion", objProducto.descripcion);
                    cmd.Parameters.AddWithValue("idCategoria", objProducto.oCategoria.idCategoria);
                    cmd.Parameters.AddWithValue("estado", objProducto.estado);
                    cmd.Parameters.AddWithValue("costoPesos", objProducto.costoPesos);
                    cmd.Parameters.AddWithValue("precioCompra", objProducto.precioCompra);
                    cmd.Parameters.AddWithValue("precioVenta", objProducto.precioVenta);

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


        public bool Eliminar(Producto objProducto, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {



                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINARPRODUCTO", oconexion);
                    cmd.Parameters.AddWithValue("idProducto", objProducto.idProducto);
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


    }


}

