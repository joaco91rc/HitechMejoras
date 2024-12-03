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
    public class CD_OrdenTraspaso
    {

        public List<OrdenTraspaso> ListarOrdenesTraspaso()
        {
            List<OrdenTraspaso> lista = new List<OrdenTraspaso>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT Id, IdSucursalOrigen, IdSucursalDestino, IdProducto, Cantidad, Confirmada, FechaCreacion, FechaCompletado, CostoProducto, SerialNumber");
                    query.AppendLine("FROM ORDENTRASPASO");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new OrdenTraspaso()
                            {
                                IdOrdenTraspaso = Convert.ToInt32(dr["Id"]),
                                IdSucursalOrigen = Convert.ToInt32(dr["IdSucursalOrigen"]),
                                IdSucursalDestino = Convert.ToInt32(dr["IdSucursalDestino"]),
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                Cantidad = Convert.ToInt32(dr["Cantidad"]),
                                Confirmada = dr["Confirmada"].ToString(),
                                FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]),
                                FechaConfirmacion = dr["FechaCompletado"] != DBNull.Value ? Convert.ToDateTime(dr["FechaCompletado"]) : (DateTime?)null,
                                CostoProducto = Convert.ToDecimal(dr["CostoProducto"]),
                                SerialNumber = dr["SerialNumber"].ToString(),
                                // Agregar las columnas con los nombres de las sucursales
                                LocalOrigen = ObtenerNombreSucursal(Convert.ToInt32(dr["IdSucursalOrigen"])),
                                LocalDestino = ObtenerNombreSucursal(Convert.ToInt32(dr["IdSucursalDestino"]))
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<OrdenTraspaso>();
                    // Opcional: registrar el error para depuración
                }
            }
            return lista;
        }

        // Función para obtener el nombre de la sucursal en función del ID
        private string ObtenerNombreSucursal(int idSucursal)
        {
            switch (idSucursal)
            {
                case 1:
                    return "HITECH 1";
                case 2:
                    return "HITECH 2";
                case 3:
                    return "APPLE 49";
                case 4:
                    return "APPLE CAFE";
                default:
                    return "Sucursal Desconocida"; // En caso de un ID no esperado
            }
        }


        public OrdenTraspaso ObtenerOrdenTraspasoPorId(int id)
        {
            OrdenTraspaso ordenTraspaso = null;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT Id, IdSucursalOrigen, IdSucursalDestino, IdProducto, Cantidad, Confirmada, FechaCreacion, FechaConfirmacion, CostoProducto, SerialNumber");
                    query.AppendLine("FROM OrdenTraspaso");
                    query.AppendLine("WHERE Id = @Id");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Id", id);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            ordenTraspaso = new OrdenTraspaso()
                            {
                                IdOrdenTraspaso= Convert.ToInt32(dr["Id"]),
                                IdSucursalOrigen = Convert.ToInt32(dr["IdSucursalOrigen"]),
                                IdSucursalDestino = Convert.ToInt32(dr["IdSucursalDestino"]),
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                Cantidad = Convert.ToInt32(dr["Cantidad"]),
                                Confirmada = dr["Confirmada"].ToString(),
                                FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]),
                                FechaConfirmacion = dr["FechaConfirmacion"] != DBNull.Value ? Convert.ToDateTime(dr["FechaConfirmacion"]) : (DateTime?)null,
                                CostoProducto = Convert.ToDecimal(dr["CostoProducto"]),
                                SerialNumber = dr["CostoProducto"].ToString()
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    ordenTraspaso = null;
                    // Opcional: registrar el error para depuración
                }
            }
            return ordenTraspaso;
        }

        public bool InsertarOrdenTraspaso(OrdenTraspaso ordenTraspaso)
        {
            bool resultado = false;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("INSERT INTO ORDENTRASPASO (IdSucursalOrigen, IdSucursalDestino, IdProducto, Cantidad, Confirmada, FechaCreacion, FechaCompletado,CostoProducto,SerialNumber)");
                    query.AppendLine("VALUES (@IdSucursalOrigen, @IdSucursalDestino, @IdProducto, @Cantidad, @Confirmada, @FechaCreacion, @FechaConfirmacion, @CostoProducto,@SerialNumber)");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@IdSucursalOrigen", ordenTraspaso.IdSucursalOrigen);
                    cmd.Parameters.AddWithValue("@IdSucursalDestino", ordenTraspaso.IdSucursalDestino);
                    cmd.Parameters.AddWithValue("@IdProducto", ordenTraspaso.IdProducto);
                    cmd.Parameters.AddWithValue("@Cantidad", ordenTraspaso.Cantidad);
                    cmd.Parameters.AddWithValue("@Confirmada", ordenTraspaso.Confirmada);
                    cmd.Parameters.AddWithValue("@FechaCreacion", ordenTraspaso.FechaCreacion);
                    cmd.Parameters.AddWithValue("@CostoProducto", ordenTraspaso.CostoProducto);
                    cmd.Parameters.AddWithValue("@SerialNumber", ordenTraspaso.SerialNumber);
                    if (ordenTraspaso.FechaConfirmacion == null)
                    {
                        cmd.Parameters.AddWithValue("@FechaConfirmacion", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@FechaConfirmacion", ordenTraspaso.FechaConfirmacion);
                    }

                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    resultado = false;
                    // Opcional: registrar el error para depuración
                }
            }
            return resultado;
        }

        public bool ConfirmarOrdenTraspaso(int id)
        {
            bool resultado = false;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("UPDATE OrdenTraspaso");
                    query.AppendLine("SET Confirmada = @Confirmada, FechaCompletado = @FechaCompletado");
                    query.AppendLine("WHERE Id = @Id");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Confirmada", "1");
                    cmd.Parameters.AddWithValue("@FechaCompletado", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@Id", id);

                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    resultado = false;
                    // Opcional: registrar el error para depuración
                }
            }
            return resultado;
        }

        public bool RechazarOrdenTraspaso(int id)
        {
            bool resultado = false;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("DELETE FROM OrdenTraspaso");
                    query.AppendLine("WHERE Id = @Id");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Id", id);

                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    resultado = false;
                    // Opcional: registrar el error para depuración
                }
            }
            return resultado;
        }

    }
}
