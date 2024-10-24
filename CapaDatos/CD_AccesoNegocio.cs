using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
   public  class CD_AccesoNegocio
    {
        public bool TienePermiso(int idUsuario, int idNegocio)
        {
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT COUNT(*) FROM ACCESONEGOCIO");
                    query.AppendLine("WHERE idUsuario = @idUsuario AND idNegocio = @idNegocio");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@idNegocio", idNegocio);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    int count = (int)cmd.ExecuteScalar(); // Obtener el número de coincidencias
                    return count > 0; // Retorna true si hay coincidencias, false en caso contrario
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones (opcional)
                    // Puedes registrar el error aquí si lo deseas.
                    return false; // Retorna false en caso de error
                }
            }
        }

        public List<int> ListarNegociosPermitidos(int idUsuario)
        {
            List<int> negociosPermitidos = new List<int>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT idNegocio FROM ACCESONEGOCIO");
                    query.AppendLine("WHERE idUsuario = @idUsuario");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            negociosPermitidos.Add(Convert.ToInt32(dr["idNegocio"])); // Agregar cada idNegocio a la lista
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones (opcional)
                    // Puedes registrar el error aquí si lo deseas.
                    negociosPermitidos = new List<int>(); // Retornar lista vacía en caso de error
                }
            }

            return negociosPermitidos;
        }


        public bool Asignarpermiso(int idUsuario, int idNegocio)
        {
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("INSERT INTO ACCESONEGOCIO (idUsuario, idNegocio, fechaAsignacion)");
                    query.AppendLine("VALUES (@idUsuario, @idNegocio, DEFAULT);"); // Usa DEFAULT para la fecha

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@idNegocio", idNegocio);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery(); // Ejecuta la inserción y obtiene el número de filas afectadas
                    return filasAfectadas > 0; // Retorna true si la inserción fue exitosa
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones (opcional)
                    // Puedes registrar el error aquí si lo deseas.
                    return false; // Retorna false en caso de error
                }
            }
        }

        public bool ModificarPermisos(int idUsuario, List<int> listaNegocios)
        {
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                SqlTransaction transaccion = null;
                try
                {
                    oconexion.Open();
                    transaccion = oconexion.BeginTransaction();

                    // 1. Eliminar todos los permisos del usuario
                    StringBuilder queryEliminar = new StringBuilder();
                    queryEliminar.AppendLine("DELETE FROM ACCESONEGOCIO WHERE idUsuario = @idUsuario");

                    SqlCommand cmdEliminar = new SqlCommand(queryEliminar.ToString(), oconexion, transaccion);
                    cmdEliminar.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmdEliminar.CommandType = CommandType.Text;
                    cmdEliminar.ExecuteNonQuery();

                    // 2. Insertar los nuevos permisos
                    StringBuilder queryInsertar = new StringBuilder();
                    queryInsertar.AppendLine("INSERT INTO ACCESONEGOCIO (idUsuario, idNegocio, fechaAsignacion)");
                    queryInsertar.AppendLine("VALUES (@idUsuario, @idNegocio, DEFAULT)");

                    foreach (int idNegocio in listaNegocios)
                    {
                        SqlCommand cmdInsertar = new SqlCommand(queryInsertar.ToString(), oconexion, transaccion);
                        cmdInsertar.Parameters.AddWithValue("@idUsuario", idUsuario);
                        cmdInsertar.Parameters.AddWithValue("@idNegocio", idNegocio);
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

        public bool EliminarPermisos(int idUsuario)
        {
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("DELETE FROM ACCESONEGOCIO WHERE idUsuario = @idUsuario");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
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
