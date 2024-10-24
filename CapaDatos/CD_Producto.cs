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
                    query.AppendLine("select p.idProducto, p.codigo, p.nombre, p.descripcion, c.idCategoria, c.descripcion[DescripcionCategoria],p.ventaPesos,");
                    query.AppendLine("ISNULL(pn.stock, 0) as stock, p.precioCompra, p.precioVenta, p.estado, p.costoPesos, p.prodSerializable");
                    query.AppendLine("from Producto p");
                    query.AppendLine("inner join CATEGORIA c on c.idCategoria = p.idCategoria");
                    query.AppendLine("left join PRODUCTONEGOCIO pn on pn.idProducto = p.idProducto and pn.idNegocio = @idNegocio");
                    query.AppendLine("where p.estado = 1");
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
                                stock = Convert.ToInt32(dr["stock"]),
                                
                                prodSerializable = Convert.ToBoolean(dr["prodSerializable"]),
                                ventaPesos = Convert.ToDecimal(dr["ventaPesos"])
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

        public List<ProductoDetalle> ListarProductosConSerialNumberByIDNegocio(int idProducto, int idNegocio)
        {
            List<ProductoDetalle> lista = new List<ProductoDetalle>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT PD.*, P.codigo, P.nombre FROM PRODUCTO_DETALLE PD");
                    query.AppendLine("INNER JOIN PRODUCTO P ON PD.idProducto = P.idProducto");
                    query.AppendLine("WHERE PD.idProducto = @idProducto AND PD.estado = 1 AND PD.idNegocio = @idNegocio");
                    query.AppendLine("AND P.prodSerializable = 1");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);
                    cmd.Parameters.AddWithValue("@idNegocio", idNegocio); // Filtro por idNegocio

                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ProductoDetalle detalle = new ProductoDetalle()
                            {
                                idProductoDetalle = Convert.ToInt32(dr["idProductoDetalle"]),
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                numeroSerie = dr["numeroSerie"].ToString(),
                                color = dr["color"].ToString(),
                                modelo = dr["modelo"].ToString(),
                                marca = dr["marca"].ToString(),
                                idNegocio = Convert.ToInt32(dr["idNegocio"]),
                                codigo = dr["codigo"].ToString(),
                                nombre = dr["nombre"].ToString(),
                                estado = Convert.ToBoolean(dr["estado"]),
                            };

                            lista.Add(detalle);
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<ProductoDetalle>();
                }
            }
            return lista;
        }

        public List<ProductoDetalle> ListarProductosConSerialNumberByID(int idProducto)
        {
            List<ProductoDetalle> lista = new List<ProductoDetalle>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT PD.*, P.codigo, P.nombre FROM PRODUCTO_DETALLE PD");
                    query.AppendLine("INNER JOIN PRODUCTO P ON PD.idProducto = P.idProducto");
                    query.AppendLine("WHERE PD.idProducto = @idProducto AND PD.estado = 1"); // Filtro por idProducto y estado activo
                    query.AppendLine("AND P.prodSerializable = 1");


                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idProducto", idProducto); // Agregar parámetro

                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ProductoDetalle detalle = new ProductoDetalle()
                            {
                                idProductoDetalle = Convert.ToInt32(dr["idProductoDetalle"]),
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                numeroSerie = dr["numeroSerie"].ToString(),
                                color = dr["color"].ToString(),
                                modelo = dr["modelo"].ToString(),
                                marca = dr["marca"].ToString(),
                                idNegocio = Convert.ToInt32(dr["idNegocio"]),
                                codigo = dr["codigo"].ToString(), // Asegúrate de que exista esta propiedad en ProductoDetalle
                                nombre = dr["nombre"].ToString(),
                                estado = Convert.ToBoolean(dr["estado"]),

                            };

                            lista.Add(detalle);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejar excepciones si es necesario
                    lista = new List<ProductoDetalle>();
                }
            }
            return lista;
        }

        public List<ProductoDetalle> ListarProductosConSerialNumberEnStockTodosLocales()
        {
            List<ProductoDetalle> lista = new List<ProductoDetalle>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT PD.*, P.codigo, P.nombre FROM PRODUCTO_DETALLE PD");
                    query.AppendLine("INNER JOIN PRODUCTO P ON PD.idProducto = P.idProducto");
                    query.AppendLine("WHERE PD.estado = 1"); // Filtrar solo productos con estado = 1

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);

                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ProductoDetalle detalle = new ProductoDetalle()
                            {
                                idProductoDetalle = Convert.ToInt32(dr["idProductoDetalle"]),
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                numeroSerie = dr["numeroSerie"].ToString(),
                                color = dr["color"].ToString(),
                                modelo = dr["modelo"].ToString(),
                                marca = dr["marca"].ToString(),
                                idNegocio = Convert.ToInt32(dr["idNegocio"]),
                                codigo = dr["codigo"].ToString(),
                                nombre = dr["nombre"].ToString(),
                                estado = Convert.ToBoolean(dr["estado"]),
                                estadoVendido = Convert.ToBoolean(dr["estado"]) == true ? "EN STOCK" : "VENDIDO"
                            };

                            // Asignar nombreLocal basado en el idNegocio
                            switch (detalle.idNegocio)
                            {
                                case 1:
                                    detalle.nombreLocal = "HITECH 1";
                                    break;
                                case 2:
                                    detalle.nombreLocal = "HITECH 2";
                                    break;
                                case 3:
                                    detalle.nombreLocal = "APPLE 49";
                                    break;
                                case 4:
                                    detalle.nombreLocal = "APPLE CAFÉ";
                                    break;
                                default:
                                    detalle.nombreLocal = ""; // Si no coincide con los casos
                                    break;
                            }

                            lista.Add(detalle);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejar excepciones si es necesario
                    lista = new List<ProductoDetalle>();
                }
            }
            return lista;
        }


        public List<ProductoDetalle> ListarProductosConSerialNumber()
        {
            List<ProductoDetalle> lista = new List<ProductoDetalle>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT PD.*, P.codigo, P.nombre FROM PRODUCTO_DETALLE PD");
                    query.AppendLine("INNER JOIN PRODUCTO P ON PD.idProducto = P.idProducto");
                    

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ProductoDetalle detalle = new ProductoDetalle()
                            {
                                idProductoDetalle = Convert.ToInt32(dr["idProductoDetalle"]),
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                numeroSerie = dr["numeroSerie"].ToString(),
                                color = dr["color"].ToString(),
                                modelo = dr["modelo"].ToString(),
                                marca = dr["marca"].ToString(),
                                idNegocio = Convert.ToInt32(dr["idNegocio"]),
                                codigo = dr["codigo"].ToString(), // Asegúrate de que exista esta propiedad en ProductoDetalle
                                nombre = dr["nombre"].ToString(),
                                estado =Convert.ToBoolean(dr["estado"]),
                                estadoVendido = Convert.ToBoolean(dr["estado"]) == true?"EN STOCK":"VENDIDO"
                            };

                            lista.Add(detalle);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejar excepciones si es necesario
                    lista = new List<ProductoDetalle>();
                }
            }
            return lista;
        }

        public List<ProductoDetalle> ListarProductosConSerialNumberPorLocalDisponibles(int idNegocio)
        {
            List<ProductoDetalle> lista = new List<ProductoDetalle>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT PD.*, P.codigo, P.nombre FROM PRODUCTO_DETALLE PD");
                    query.AppendLine("INNER JOIN PRODUCTO P ON PD.idProducto = P.idProducto");
                    query.AppendLine("WHERE PD.idNegocio = @idNegocio AND PD.estado = 1"); // Filtrar por estado = 1 (disponible)

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idNegocio", idNegocio); // Añadir el parámetro

                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ProductoDetalle detalle = new ProductoDetalle()
                            {
                                idProductoDetalle = Convert.ToInt32(dr["idProductoDetalle"]),
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                numeroSerie = dr["numeroSerie"].ToString(),
                                color = dr["color"].ToString(),
                                modelo = dr["modelo"].ToString(),
                                marca = dr["marca"].ToString(),
                                idNegocio = Convert.ToInt32(dr["idNegocio"]),
                                codigo = dr["codigo"].ToString(), // Asegúrate de que exista esta propiedad en ProductoDetalle
                                nombre = dr["nombre"].ToString(),
                                estado = Convert.ToBoolean(dr["estado"]),
                                estadoVendido = Convert.ToBoolean(dr["estado"]) == true ? "EN STOCK" : "VENDIDO"
                            };

                            // Asignar nombreLocal basado en el idNegocio
                            switch (detalle.idNegocio)
                            {
                                case 1:
                                    detalle.nombreLocal = "HITECH 1";
                                    break;
                                case 2:
                                    detalle.nombreLocal = "HITECH 2";
                                    break;
                                case 3:
                                    detalle.nombreLocal = "APPLE 49";
                                    break;
                                case 4:
                                    detalle.nombreLocal = "APPLE CAFÉ";
                                    break;
                                default:
                                    detalle.nombreLocal = ""; // Si no coincide con los casos
                                    break;
                            }

                            lista.Add(detalle);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejar excepciones si es necesario
                    lista = new List<ProductoDetalle>();
                }
            }
            return lista;
        }



        public List<ProductoDetalle> ListarProductosVendidos(int idNegocio)
        {
            List<ProductoDetalle> lista = new List<ProductoDetalle>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT PD.*, P.codigo, P.nombre, V.nroDocumento FROM PRODUCTO_DETALLE PD");
                    query.AppendLine("INNER JOIN PRODUCTO P ON PD.idProducto = P.idProducto");
                    query.AppendLine("INNER JOIN VENTA V ON PD.idVenta = V.idVenta"); // Asegúrate de que esta relación sea correcta
                    query.AppendLine("WHERE PD.estado = 0 AND PD.idVenta <> 0 AND PD.idNegocio = @idNegocio"); // Filtrar por estado = 0 (vendido) y idNegocio

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idNegocio", idNegocio); // Añadir parámetro
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ProductoDetalle detalle = new ProductoDetalle()
                            {
                                idProductoDetalle = Convert.ToInt32(dr["idProductoDetalle"]),
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                numeroSerie = dr["numeroSerie"].ToString(),
                                color = dr["color"].ToString(),
                                modelo = dr["modelo"].ToString(),
                                marca = dr["marca"].ToString(),
                                idNegocio = Convert.ToInt32(dr["idNegocio"]),
                                codigo = dr["codigo"].ToString(), // Asegúrate de que exista esta propiedad en ProductoDetalle
                                nombre = dr["nombre"].ToString(),
                                numeroVenta = dr["nroDocumento"].ToString() // Asegúrate de tener esta propiedad en ProductoDetalle
                            };

                            // Asignar nombreLocal basado en el idNegocio
                            switch (detalle.idNegocio)
                            {
                                case 1:
                                    detalle.nombreLocal = "HITECH 1";
                                    break;
                                case 2:
                                    detalle.nombreLocal = "HITECH 2";
                                    break;
                                case 3:
                                    detalle.nombreLocal = "APPLE 49";
                                    break;
                                case 4:
                                    detalle.nombreLocal = "APPLE CAFÉ";
                                    break;
                                default:
                                    detalle.nombreLocal = ""; // Valor por defecto si no coincide con los casos
                                    break;
                            }

                            lista.Add(detalle);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejar excepciones si es necesario
                    lista = new List<ProductoDetalle>();
                    // Puedes registrar el error o lanzar una excepción
                }
            }
            return lista;
        }

        public List<ProductoDetalle> ListarProductosVendidosTodosLocales()
        {
            List<ProductoDetalle> lista = new List<ProductoDetalle>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT PD.*, P.codigo, P.nombre, V.nroDocumento FROM PRODUCTO_DETALLE PD");
                    query.AppendLine("INNER JOIN PRODUCTO P ON PD.idProducto = P.idProducto");
                    query.AppendLine("INNER JOIN VENTA V ON PD.idVenta = V.idVenta"); // Asegúrate de que esta relación sea correcta
                    query.AppendLine("WHERE PD.estado = 0 AND PD.idVenta <> 0"); // Filtrar por estado = 0 (vendido)

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ProductoDetalle detalle = new ProductoDetalle()
                            {
                                idProductoDetalle = Convert.ToInt32(dr["idProductoDetalle"]),
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                numeroSerie = dr["numeroSerie"].ToString(),
                                color = dr["color"].ToString(),
                                modelo = dr["modelo"].ToString(),
                                marca = dr["marca"].ToString(),
                                idNegocio = Convert.ToInt32(dr["idNegocio"]),
                                codigo = dr["codigo"].ToString(), // Asegúrate de que exista esta propiedad en ProductoDetalle
                                nombre = dr["nombre"].ToString(),
                                numeroVenta = dr["nroDocumento"].ToString() // Asegúrate de tener esta propiedad en ProductoDetalle
                            };

                            // Asignar nombreLocal basado en el idNegocio
                            switch (detalle.idNegocio)
                            {
                                case 1:
                                    detalle.nombreLocal = "HITECH 1";
                                    break;
                                case 2:
                                    detalle.nombreLocal = "HITECH 2";
                                    break;
                                case 3:
                                    detalle.nombreLocal = "APPLE 49";
                                    break;
                                case 4:
                                    detalle.nombreLocal = "APPLE CAFÉ";
                                    break;
                                default:
                                    detalle.nombreLocal = ""; // Valor por defecto si no coincide con los casos
                                    break;
                            }

                            lista.Add(detalle);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejar excepciones si es necesario
                    lista = new List<ProductoDetalle>();
                    // Puedes registrar el error o lanzar una excepción
                }
            }
            return lista;
        }




        public List<ProductoDetalle> ListarProductosSerialesPorVenta(int idVenta)
        {
            List<ProductoDetalle> lista = new List<ProductoDetalle>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT PD.*, P.codigo, P.nombre FROM PRODUCTO_DETALLE PD");
                    query.AppendLine("INNER JOIN PRODUCTO P ON PD.idProducto = P.idProducto");
                    query.AppendLine("WHERE PD.idVenta = @idVenta"); // Filtrar por idVenta

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idVenta", idVenta); // Agregar parámetro idVenta

                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ProductoDetalle detalle = new ProductoDetalle()
                            {
                                idProductoDetalle = Convert.ToInt32(dr["idProductoDetalle"]),
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                numeroSerie = dr["numeroSerie"].ToString(),
                                color = dr["color"].ToString(),
                                modelo = dr["modelo"].ToString(),
                                marca = dr["marca"].ToString(),
                                idNegocio = Convert.ToInt32(dr["idNegocio"]),
                                codigo = dr["codigo"].ToString(),
                                nombre = dr["nombre"].ToString()
                            };

                            lista.Add(detalle);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejar excepciones si es necesario
                    lista = new List<ProductoDetalle>();
                }
            }
            return lista;
        }


        public int ContarProductosSerializados(int idProducto, int idNegocio)
        {
            int cantidadSerializados = 0;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT COUNT(*) FROM PRODUCTO_DETALLE");
                query.AppendLine("WHERE idProducto = @idProducto AND estado = 1 AND idNegocio = @idNegocio");

                SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                cmd.Parameters.AddWithValue("@idProducto", idProducto);
                cmd.Parameters.AddWithValue("@idNegocio", idNegocio);

                oconexion.Open();
                cantidadSerializados = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return cantidadSerializados;
        }



        public List<Producto> ListarSerializablesPorNegocio(int idNegocio)
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT p.idProducto, p.codigo, p.nombre, p.descripcion, c.idCategoria, c.descripcion[DescripcionCategoria], p.ventaPesos,");
                    query.AppendLine("ISNULL(pn.stock, 0) AS stock, p.precioCompra, p.precioVenta, p.estado, p.costoPesos, p.prodSerializable");
                    query.AppendLine("FROM Producto p");
                    query.AppendLine("INNER JOIN CATEGORIA c ON c.idCategoria = p.idCategoria");
                    query.AppendLine("LEFT JOIN PRODUCTONEGOCIO pn ON pn.idProducto = p.idProducto");
                    query.AppendLine("WHERE p.estado = 1 AND pn.idNegocio = @idNegocio AND p.prodSerializable = 1"); // Agregado filtro por prodSerializable = 1

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idNegocio", idNegocio); // Parametro de negocio
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
                                oCategoria = new Categoria()
                                {
                                    idCategoria = Convert.ToInt32(dr["idCategoria"]),
                                    descripcion = dr["DescripcionCategoria"].ToString()
                                },
                                costoPesos = Convert.ToDecimal(dr["costoPesos"]),
                                precioCompra = Convert.ToDecimal(dr["precioCompra"]),
                                precioVenta = Convert.ToDecimal(dr["precioVenta"]),
                                estado = Convert.ToBoolean(dr["estado"]),
                                stock = Convert.ToInt32(dr["stock"]),
                                prodSerializable = Convert.ToBoolean(dr["prodSerializable"]),
                                ventaPesos = Convert.ToDecimal(dr["ventaPesos"])
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

        public List<Producto> ListarSerializables()
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT p.idProducto, p.codigo, p.nombre, p.descripcion, c.idCategoria, c.descripcion AS DescripcionCategoria,");
                    query.AppendLine("p.ventaPesos, ISNULL(pn.stock, 0) AS stock, p.precioCompra, p.precioVenta, p.estado,");
                    query.AppendLine("p.costoPesos, p.prodSerializable, pn.idNegocio");
                    query.AppendLine("FROM Producto p");
                    query.AppendLine("INNER JOIN CATEGORIA c ON c.idCategoria = p.idCategoria");
                    query.AppendLine("LEFT JOIN PRODUCTONEGOCIO pn ON pn.idProducto = p.idProducto");
                    query.AppendLine("WHERE p.estado = 1 AND p.prodSerializable = 1");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var producto = new Producto()
                            {
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                codigo = dr["codigo"].ToString(),
                                nombre = dr["nombre"].ToString(),
                                descripcion = dr["descripcion"].ToString(),
                                oCategoria = new Categoria()
                                {
                                    idCategoria = Convert.ToInt32(dr["idCategoria"]),
                                    descripcion = dr["DescripcionCategoria"].ToString()
                                },
                                costoPesos = Convert.ToDecimal(dr["costoPesos"]),
                                precioCompra = Convert.ToDecimal(dr["precioCompra"]),
                                precioVenta = Convert.ToDecimal(dr["precioVenta"]),
                                estado = Convert.ToBoolean(dr["estado"]),
                                stock = Convert.ToInt32(dr["stock"]),
                                prodSerializable = Convert.ToBoolean(dr["prodSerializable"]),
                                ventaPesos = Convert.ToDecimal(dr["ventaPesos"])
                            };

                            // Asignación de nombreLocal basado en idNegocio usando if-else
                            int idNegocio = dr["idNegocio"] != DBNull.Value ? Convert.ToInt32(dr["idNegocio"]) : 0;
                            if (idNegocio == 1)
                                producto.nombreLocal = "HITECH 1";
                            else if (idNegocio == 2)
                                producto.nombreLocal = "HITECH 2";
                            else if (idNegocio == 3)
                                producto.nombreLocal = "APPLE 49";
                            else if (idNegocio == 4)
                                producto.nombreLocal = "APPLE CAFÉ";
                            else
                                producto.nombreLocal = "";

                            lista.Add(producto);
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






        public List<Producto> ListarPorNegocio(int idNegocio)
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.idProducto, p.codigo, p.nombre, p.descripcion, c.idCategoria, c.descripcion[DescripcionCategoria],p.ventaPesos,");
                    query.AppendLine("ISNULL(pn.stock, 0) as stock, p.precioCompra, p.precioVenta, p.estado, p.costoPesos, p.prodSerializable");
                    query.AppendLine("from Producto p");
                    query.AppendLine("inner join CATEGORIA c on c.idCategoria = p.idCategoria");
                    query.AppendLine("left join PRODUCTONEGOCIO pn on pn.idProducto = p.idProducto");
                    query.AppendLine("where p.estado = 1 and pn.idNegocio = @idNegocio");
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
                                stock = Convert.ToInt32(dr["stock"]),
                                
                                prodSerializable = Convert.ToBoolean(dr["prodSerializable"]),
                                ventaPesos = Convert.ToDecimal(dr["ventaPesos"])

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
                    query.AppendLine("SELECT p.idProducto, p.codigo, p.nombre, p.descripcion, c.idCategoria, c.descripcion[DescripcionCategoria], p.prodSerializable, p.ventaPesos,");
                    query.AppendLine("ISNULL(SUM(CASE WHEN pn.idNegocio = 1 THEN pn.stock ELSE 0 END), 0) as stockH1,");
                    query.AppendLine("ISNULL(SUM(CASE WHEN pn.idNegocio = 2 THEN pn.stock ELSE 0 END), 0) as stockH2,");
                    query.AppendLine("ISNULL(SUM(CASE WHEN pn.idNegocio = 3 THEN pn.stock ELSE 0 END), 0) as stockAS,");
                    query.AppendLine("ISNULL(SUM(CASE WHEN pn.idNegocio = 4 THEN pn.stock ELSE 0 END), 0) as stockAC,");
                    query.AppendLine("p.precioCompra, p.precioVenta, p.estado, p.costoPesos");
                    query.AppendLine("FROM Producto p");
                    query.AppendLine("INNER JOIN CATEGORIA c ON c.idCategoria = p.idCategoria");
                    query.AppendLine("LEFT JOIN PRODUCTONEGOCIO pn ON pn.idProducto = p.idProducto");
                    query.AppendLine("WHERE p.estado = 1");
                    query.AppendLine("GROUP BY p.idProducto, p.codigo, p.nombre, p.descripcion, c.idCategoria, c.descripcion, p.precioCompra, p.precioVenta, p.estado, p.costoPesos, p.prodSerializable, p.ventaPesos");

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
                                stockAC = Convert.ToInt32(dr["stockAC"]),
                                
                                prodSerializable = Convert.ToBoolean(dr["prodSerializable"]),
                                ventaPesos = Convert.ToDecimal(dr["ventaPesos"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Producto>(); // Si hay un error, la lista se inicializa vacía.
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
                    query.AppendLine("select p.idProducto, p.costoPesos, p.codigo, p.nombre, p.descripcion, c.idCategoria, c.descripcion[DescripcionCategoria], p.stock, p.precioCompra, p.precioVenta, p.estado,p.prodSerializable,p.ventaPesos");
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
                                costoPesos = Convert.ToDecimal(dr["costoPesos"]),
                                precioCompra = Convert.ToDecimal(dr["precioCompra"]),
                                precioVenta = Convert.ToDecimal(dr["precioVenta"]),
                                estado = Convert.ToBoolean(dr["estado"]),
                                
                                prodSerializable = Convert.ToBoolean(dr["prodSerializable"]),
                                ventaPesos = Convert.ToDecimal(dr["ventaPesos"])
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
                    cmd.Parameters.AddWithValue("prodSerializable", objProducto.prodSerializable);
                    
                    cmd.Parameters.AddWithValue("estado", objProducto.estado);
                    cmd.Parameters.AddWithValue("costoPesos", objProducto.costoPesos);
                    cmd.Parameters.AddWithValue("precioCompra", objProducto.precioCompra);
                    cmd.Parameters.AddWithValue("precioVenta", objProducto.precioVenta);
                    cmd.Parameters.AddWithValue("ventaPesos", objProducto.ventaPesos);
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


        public bool SumarStockPorRMA(int idProducto, int cantidad, int idNegocio, out string mensaje)
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
                    cmd.Parameters.AddWithValue("idNegocio", idNegocio); // Nuevo parámetro

                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
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


        public bool RestarStockPorRMA(int idProducto, int cantidad, int idNegocio, out string mensaje)
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
                    cmd.Parameters.AddWithValue("idNegocio", idNegocio); // Nuevo parámetro

                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
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
                    cmd.Parameters.AddWithValue("ventaPesos", objProducto.ventaPesos);
                    cmd.Parameters.AddWithValue("prodSerializable", objProducto.prodSerializable);
                    
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

        public bool EditarPrecios(Producto objProducto, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    // Consulta SQL para actualizar precios y costos
                    string query = "UPDATE PRODUCTO SET precioCompra = @precioCompra, precioVenta = @precioVenta, costoPesos = @costoPesos, ventaPesos = @ventaPesos WHERE idProducto = @idProducto";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@precioCompra", objProducto.precioCompra);
                    cmd.Parameters.AddWithValue("@precioVenta", objProducto.precioVenta);
                    cmd.Parameters.AddWithValue("@costoPesos", objProducto.costoPesos);
                    cmd.Parameters.AddWithValue("@ventaPesos", objProducto.ventaPesos);
                    cmd.Parameters.AddWithValue("@idProducto", objProducto.idProducto);

                    oconexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery(); // Ejecutar la consulta

                    if (filasAfectadas > 0)
                    {
                        respuesta = true;
                        mensaje = "Los precios y costos se han actualizado correctamente.";
                    }
                    else
                    {
                        mensaje = "No se encontró el producto o no se realizaron cambios.";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = ex.Message;
            }

            return respuesta;
        }

        public decimal ObtenerCostoProducto(int idProducto)
        {
            decimal costo = 0;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT precioCompra FROM Producto WHERE idProducto = @IdProducto");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                    oconexion.Open();
                    costo = (decimal)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    // Opcional: registrar el error para depuración
                }
            }
            return costo;
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



        public bool DarBajaLogica(int idProducto, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("UPDATE Producto SET estado = 0 WHERE idProducto = @idProducto");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        return true;
                    }
                    else
                    {
                        mensaje = "No se pudo Eliminar  producto.";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error: " + ex.Message;
                return false;
            }
        }

        public int RegistrarSerialNumber(ProductoDetalle productoDetalle, out string mensaje)
        {
            int resultado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARSERIALNUMBER", oconexion);
                    cmd.Parameters.AddWithValue("idProducto", productoDetalle.idProducto);
                    cmd.Parameters.AddWithValue("numeroSerie", productoDetalle.numeroSerie);
                    cmd.Parameters.AddWithValue("color", productoDetalle.color);
                    cmd.Parameters.AddWithValue("modelo", productoDetalle.modelo);
                    cmd.Parameters.AddWithValue("marca", productoDetalle.marca);
                    cmd.Parameters.AddWithValue("idNegocio", productoDetalle.idNegocio);
                    cmd.Parameters.AddWithValue("idVenta", productoDetalle.idVenta);

                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
                mensaje = ex.Message;
            }

            return resultado; // Devuelve el resultado de la inserción
        }

        public int DesactivarProductoDetalle(int idProductoDetalle, int idVenta, out string mensaje)
        {
            int resultado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    // Definir la consulta SQL para actualizar el estado y el idVenta
                    string query = "UPDATE PRODUCTO_DETALLE SET estado = 0, idVenta = @idVenta WHERE idProductoDetalle = @idProductoDetalle";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@idProductoDetalle", idProductoDetalle);
                    cmd.Parameters.AddWithValue("@idVenta", idVenta);

                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery(); // Ejecutar la consulta

                    if (resultado > 0)
                    {
                        mensaje = "Producto desactivado correctamente.";
                    }
                    else
                    {
                        mensaje = "No se pudo desactivar el producto. Verifique si el ID es correcto.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
                mensaje = "Error: " + ex.Message;
            }

            return resultado;
        }

        public int ActivarProductoDetalle(int idProductoDetalle, out string mensaje)
        {
            int resultado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    // Consulta SQL para activar el producto y poner idVenta en 0
                    string query = "UPDATE PRODUCTO_DETALLE SET estado = 1, idVenta = 0 WHERE idProductoDetalle = @idProductoDetalle";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@idProductoDetalle", idProductoDetalle); // Parámetro para el ID del producto

                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery(); // Ejecutar la consulta

                    if (resultado > 0)
                    {
                        mensaje = "Producto activado correctamente.";
                    }
                    else
                    {
                        mensaje = "No se pudo activar el producto. Verifique si el ID es correcto.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
                mensaje = "Error: " + ex.Message;
            }

            return resultado;
        }


        // Similarmente para los otros métodos
        public bool EditarSerialNumber(ProductoDetalle productoDetalle, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITARSERIALNUMBER", oconexion);
                    cmd.Parameters.AddWithValue("idProductoDetalle", productoDetalle.idProductoDetalle);
                    cmd.Parameters.AddWithValue("idProducto", productoDetalle.idProducto);
                    cmd.Parameters.AddWithValue("numeroSerie", productoDetalle.numeroSerie);
                    cmd.Parameters.AddWithValue("color", productoDetalle.color);
                    cmd.Parameters.AddWithValue("modelo", productoDetalle.modelo);
                    cmd.Parameters.AddWithValue("marca", productoDetalle.marca);
                    cmd.Parameters.AddWithValue("idNegocio", productoDetalle.idNegocio);

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

        public bool TraspasarSerialNumber(ProductoDetalle productoDetalle, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    // Definir la consulta de actualización
                    string query = "UPDATE PRODUCTO_DETALLE SET idNegocio = @idNegocio WHERE numeroSerie = @numeroSerie";

                    SqlCommand cmd = new SqlCommand(query, oconexion);

                    // Agregar los parámetros necesarios
                    cmd.Parameters.AddWithValue("@idNegocio", productoDetalle.idNegocio);
                    cmd.Parameters.AddWithValue("@numeroSerie", productoDetalle.numeroSerie);

                    oconexion.Open();

                    // Ejecutar la consulta y verificar si alguna fila fue afectada
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        respuesta = true;
                        mensaje = "El idNegocio se actualizó correctamente.";
                    }
                    else
                    {
                        respuesta = false;
                        mensaje = "No se encontró un registro con el número de serie especificado.";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = ex.Message;
            }

            return respuesta;
        }



        public bool EliminarSerialNumber(ProductoDetalle productoDetalle, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINARSERIALNUMBER", oconexion);
                    cmd.Parameters.AddWithValue("idProducto", productoDetalle.idProducto);
                    cmd.Parameters.AddWithValue("numeroSerie", productoDetalle.numeroSerie);
                    cmd.Parameters.AddWithValue("idNegocio", productoDetalle.idNegocio);

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



