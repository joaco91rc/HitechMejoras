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
    public class CD_Cliente
    {

        public List<Cliente> ListarClientes()
        {
            List<Cliente> lista = new List<Cliente>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT c.idCliente, c.documento, c.nombreCompleto, c.correo, c.telefono, c.estado, c.ciudad, c.direccion, ");
                    query.AppendLine("(CASE WHEN cn1.idCliente IS NOT NULL THEN 'Si' ELSE 'No' END) AS hitech1, ");
                    query.AppendLine("(CASE WHEN cn2.idCliente IS NOT NULL THEN 'Si' ELSE 'No' END) AS hitech2, ");
                    query.AppendLine("(CASE WHEN cn3.idCliente IS NOT NULL THEN 'Si' ELSE 'No' END) AS appleStore, ");
                    query.AppendLine("(CASE WHEN cn4.idCliente IS NOT NULL THEN 'Si' ELSE 'No' END) AS appleCafe ");
                    query.AppendLine("FROM Cliente c ");
                    query.AppendLine("LEFT JOIN CLIENTESNEGOCIO cn1 ON c.idCliente = cn1.idCliente AND cn1.idNegocio = 1 "); // ID de hitech1
                    query.AppendLine("LEFT JOIN CLIENTESNEGOCIO cn2 ON c.idCliente = cn2.idCliente AND cn2.idNegocio = 2 "); // ID de hitech2
                    query.AppendLine("LEFT JOIN CLIENTESNEGOCIO cn3 ON c.idCliente = cn3.idCliente AND cn3.idNegocio = 3 "); // ID de appleStore
                    query.AppendLine("LEFT JOIN CLIENTESNEGOCIO cn4 ON c.idCliente = cn4.idCliente AND cn4.idNegocio = 4 "); // ID de appleCafe

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Cliente()
                            {
                                idCliente = Convert.ToInt32(dr["idCliente"]),
                                documento = dr["documento"].ToString(),
                                nombreCompleto = dr["nombreCompleto"].ToString(),
                                correo = dr["correo"].ToString(),
                                ciudad = dr["ciudad"].ToString(),
                                direccion = dr["direccion"].ToString(),
                                telefono = dr["telefono"].ToString(),
                                estado = Convert.ToBoolean(dr["estado"]),
                                hitech1 = dr["hitech1"].ToString(),
                                hitech2 = dr["hitech2"].ToString(),
                                appleStore = dr["appleStore"].ToString(),
                                appleCafe = dr["appleCafe"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    lista = new List<Cliente>();
                }
            }
            return lista;
        }


        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select * from Cliente ");
                    
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Cliente()
                            {
                                idCliente = Convert.ToInt32(dr["idCliente"]),
                                documento = dr["documento"].ToString(),
                                nombreCompleto = dr["nombreCompleto"].ToString(),
                                correo = dr["correo"].ToString(),
                                ciudad = dr["ciudad"].ToString(),
                                direccion = dr["direccion"].ToString(),
                                telefono = dr["telefono"].ToString(),
                                estado = Convert.ToBoolean(dr["estado"])
                                
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<Cliente>();
                }

            }
            return lista;
        }

        public Cliente ObtenerClientePorDocumentoYNombre(string documentoCliente, string nombreCompleto)
        {
            Cliente cliente = null; // Valor por defecto si no se encuentra el cliente

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Crear la consulta SQL
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT *");
                    query.AppendLine("FROM Cliente WHERE documento = @documento AND nombreCompleto = @nombreCompleto");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Agregar parámetros para evitar inyecciones SQL
                    cmd.Parameters.AddWithValue("@documento", documentoCliente);
                    cmd.Parameters.AddWithValue("@nombreCompleto", nombreCompleto);

                    oconexion.Open();

                    // Ejecutar el comando y leer los datos
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            cliente = new Cliente
                            {
                                idCliente = Convert.ToInt32(dr["idCliente"]),
                                documento = dr["documento"].ToString(),
                                nombreCompleto = dr["nombreCompleto"].ToString(),
                                correo = dr["correo"].ToString(),
                                ciudad = dr["ciudad"].ToString(),
                                direccion = dr["direccion"].ToString(),
                                telefono = dr["telefono"].ToString(),
                                estado = Convert.ToBoolean(dr["estado"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores (opcional: podrías registrar el error)
                    cliente = null; // Asegurarse de que se devuelve null en caso de error
                }
            }

            return cliente;
        }

        public int ObtenerIdClientePorDocumentoYNombre(string documentoCliente, string nombreCompleto)
        {
            int idCliente = -1; // Valor por defecto si no se encuentra el cliente
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Crear la consulta SQL
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT idCliente FROM Cliente WHERE documento = @documento AND nombreCompleto = @nombreCompleto");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Agregar parámetros para evitar inyecciones SQL
                    cmd.Parameters.AddWithValue("@documento", documentoCliente);
                    cmd.Parameters.AddWithValue("@nombreCompleto", nombreCompleto);

                    oconexion.Open();

                    // Ejecutar el comando y obtener el resultado
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        idCliente = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores (opcional: podrías registrar el error)
                    idCliente = -1; // Asegurarte de que se devuelve un valor por defecto
                }
            }
            return idCliente;
        }

        public List<Cliente> ListarClientesPorNegocio(int idNegocio)
        {
            List<Cliente> lista = new List<Cliente>();
            CD_ClienteNegocio cdClientesNegocio = new CD_ClienteNegocio();

            // Obtener la lista de clientes asignados al negocio especificado
            List<int> clienteIds = cdClientesNegocio.ListarClientesPorNegocio(idNegocio);

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Si hay clientes asignados al negocio, continuamos
                    if (clienteIds.Count > 0)
                    {
                        StringBuilder query = new StringBuilder();
                        query.AppendLine("SELECT idCliente, documento, nombreCompleto, correo, telefono, estado, ciudad, direccion");
                        query.AppendLine("FROM Cliente");
                        query.AppendLine("WHERE idCliente IN (" + string.Join(",", clienteIds) + ")"); // Usar directamente los IDs en la consulta

                        SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                        cmd.CommandType = CommandType.Text;

                        oconexion.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new Cliente()
                                {
                                    idCliente = Convert.ToInt32(dr["idCliente"]),
                                    documento = dr["documento"].ToString(),
                                    nombreCompleto = dr["nombreCompleto"].ToString(),
                                    correo = dr["correo"].ToString(),
                                    ciudad = dr["ciudad"].ToString(),
                                    direccion = dr["direccion"].ToString(),
                                    telefono = dr["telefono"].ToString(),
                                    estado = Convert.ToBoolean(dr["estado"])
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    lista = new List<Cliente>();
                }
            }
            return lista;
        }



        public int Registrar(Cliente objCliente, out string mensaje)
        {
            int idClienteGenerado = 0;
            mensaje = string.Empty;

            try
            {



                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARCLIENTE", oconexion);
                    cmd.Parameters.AddWithValue("documento", objCliente.documento);
                    cmd.Parameters.AddWithValue("nombreCompleto", objCliente.nombreCompleto);
                    cmd.Parameters.AddWithValue("correo", objCliente.correo);
                    cmd.Parameters.AddWithValue("telefono", objCliente.telefono);
                     cmd.Parameters.AddWithValue("estado", objCliente.estado);
                    cmd.Parameters.AddWithValue("ciudad", objCliente.ciudad);
                    cmd.Parameters.AddWithValue("direccion", objCliente.direccion);

                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idClienteGenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();


                }

            }

            catch (Exception ex)
            {
                idClienteGenerado = 0;
                mensaje = ex.Message;

            }


            return idClienteGenerado;

        }

        public bool Editar(Cliente objCliente, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {



                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_MODIFICARCLIENTE", oconexion);
                    cmd.Parameters.AddWithValue("idCliente", objCliente.idCliente);
                    cmd.Parameters.AddWithValue("documento", objCliente.documento);
                    cmd.Parameters.AddWithValue("nombreCompleto", objCliente.nombreCompleto);
                    cmd.Parameters.AddWithValue("correo", objCliente.correo);
                    cmd.Parameters.AddWithValue("telefono", objCliente.telefono);
                    cmd.Parameters.AddWithValue("ciudad", objCliente.ciudad);
                    cmd.Parameters.AddWithValue("direccion", objCliente.direccion);

                    cmd.Parameters.AddWithValue("estado", objCliente.estado);

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


        public bool Eliminar(Cliente objCliente, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {



                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("delete from cliente where idCliente=@idCliente", oconexion);
                    cmd.Parameters.AddWithValue("idCliente", objCliente.idCliente);
                    cmd.Parameters.Add("respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;



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
