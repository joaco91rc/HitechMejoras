using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_ClienteNegocio
    {

        public bool ClienteAsignadoANegocio(int idCliente, int idNegocio)
        {
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT COUNT(*) FROM CLIENTESNEGOCIO");
                    query.AppendLine("WHERE idCliente = @idCliente AND idNegocio = @idNegocio");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    cmd.Parameters.AddWithValue("@idNegocio", idNegocio);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    int count = (int)cmd.ExecuteScalar(); // Obtener el número de coincidencias
                    return count > 0; // Retorna true si hay coincidencias, false en caso contrario
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones (opcional)
                    return false; // Retorna false en caso de error
                }
            }
        }

        public bool AsignarClienteANegocio(int idCliente, int idNegocio)
        {
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    DateTime fechaAsignacion = DateTime.Now;
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("INSERT INTO CLIENTESNEGOCIO (idCliente, idNegocio, fechaAsignacion)");
                    query.AppendLine("VALUES (@idCliente, @idNegocio, @fechaAsignacion);"); // Usa DEFAULT para la fecha

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    cmd.Parameters.AddWithValue("@idNegocio", idNegocio);
                    cmd.Parameters.AddWithValue("@fechaAsignacion", fechaAsignacion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery(); // Ejecuta la inserción y obtiene el número de filas afectadas
                    return filasAfectadas > 0; // Retorna true si la inserción fue exitosa
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones (opcional)
                    return false; // Retorna false en caso de error
                }
            }
        }

        public List<int> ListarClientesPorNegocio(int idNegocio)
        {
            List<int> clientes = new List<int>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT idCliente FROM CLIENTESNEGOCIO");
                    query.AppendLine("WHERE idNegocio = @idNegocio");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idNegocio", idNegocio);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clientes.Add(Convert.ToInt32(dr["idCliente"])); // Agregar cada idCliente a la lista
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones (opcional)
                    clientes = new List<int>(); // Retornar lista vacía en caso de error
                }
            }

            return clientes;
        }




        public List<int> ListarNegociosDeCliente(int idCliente)
        {
            List<int> negocios = new List<int>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT idNegocio FROM CLIENTESNEGOCIO");
                    query.AppendLine("WHERE idCliente = @idCliente");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            negocios.Add(Convert.ToInt32(dr["idNegocio"])); // Agregar cada idNegocio a la lista
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones (opcional)
                    negocios = new List<int>(); // Retornar lista vacía en caso de error
                }
            }

            return negocios;
        }

        public bool ModificarClientesEnNegocios(int idCliente, List<int> listaNegocios)
        {
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                SqlTransaction transaccion = null;
                try
                {
                    oconexion.Open();
                    transaccion = oconexion.BeginTransaction();

                    // 1. Eliminar todos los negocios del cliente
                    StringBuilder queryEliminar = new StringBuilder();
                    queryEliminar.AppendLine("DELETE FROM CLIENTESNEGOCIO WHERE idCliente = @idCliente");

                    SqlCommand cmdEliminar = new SqlCommand(queryEliminar.ToString(), oconexion, transaccion);
                    cmdEliminar.Parameters.AddWithValue("@idCliente", idCliente);
                    cmdEliminar.CommandType = CommandType.Text;
                    cmdEliminar.ExecuteNonQuery();
                    DateTime fechaAsignacion = DateTime.Now;
                    // 2. Insertar los nuevos negocios
                    StringBuilder queryInsertar = new StringBuilder();
                    queryInsertar.AppendLine("INSERT INTO CLIENTESNEGOCIO (idCliente, idNegocio, fechaAsignacion)");
                    queryInsertar.AppendLine("VALUES (@idCliente, @idNegocio, @fechaAsignacion)");

                    foreach (int idNegocio in listaNegocios)
                    {
                        SqlCommand cmdInsertar = new SqlCommand(queryInsertar.ToString(), oconexion, transaccion);
                        cmdInsertar.Parameters.AddWithValue("@idCliente", idCliente);
                        cmdInsertar.Parameters.AddWithValue("@idNegocio", idNegocio);
                        cmdInsertar.Parameters.AddWithValue("@fechaAsignacion", fechaAsignacion);
                        cmdInsertar.CommandType = CommandType.Text;
                        cmdInsertar.ExecuteNonQuery(); // Ejecuta la inserción
                    }

                    // 3. Confirmar la transacción
                    transaccion.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    if (transaccion != null)
                    {
                        transaccion.Rollback(); // En caso de error, deshacer la transacción
                    }
                    return false;
                }
            }
        }

        public bool EliminarClientesDeNegocio(int idCliente)
        {
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("DELETE FROM CLIENTESNEGOCIO WHERE idCliente = @idCliente");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery(); // Ejecuta la eliminación y obtiene el número de filas afectadas
                    return filasAfectadas > 0; // Retorna true si se eliminaron filas
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones (opcional)
                    return false; // Retorna false en caso de error
                }
            }
        }
    }
}
