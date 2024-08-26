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
    public class CD_ServicioTecnico
    {

        public List<ServicioTecnico> Listar(int idNegocio)
        {
            List<ServicioTecnico> lista = new List<ServicioTecnico>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT st.*, c.nombreCompleto AS NombreCliente");
                    query.AppendLine("FROM SERVICIOTECNICO st");
                    query.AppendLine("INNER JOIN CLIENTE c ON st.idCliente = c.idCliente");
                    query.AppendLine("WHERE st.estadoServicio <> @estadoCompletado");
                    query.AppendLine("AND st.idNegocio = @idNegocio");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@estadoCompletado", "Completado");
                    cmd.Parameters.AddWithValue("@idNegocio", idNegocio);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ServicioTecnico()
                            {
                                IdServicio = Convert.ToInt32(dr["idServicio"]),
                                IdCliente = Convert.ToInt32(dr["idCliente"]),
                                NombreCliente = dr["NombreCliente"].ToString(),
                                Producto = dr["producto"].ToString(),
                                FechaRecepcion = Convert.ToDateTime(dr["fechaRecepcion"]),
                                FechaEntregaEstimada = dr["fechaEntregaEstimada"] != DBNull.Value ? Convert.ToDateTime(dr["fechaEntregaEstimada"]) : (DateTime?)null,
                                FechaEntregaReal = dr["fechaEntregaReal"] != DBNull.Value ? Convert.ToDateTime(dr["fechaEntregaReal"]) : (DateTime?)null,
                                DescripcionProblema = dr["descripcionProblema"].ToString(),
                                DescripcionReparacion = dr["descripcionReparacion"].ToString(),
                                EstadoServicio = dr["estadoServicio"].ToString(),
                                CostoEstimado = dr["costoEstimado"] != DBNull.Value ? Convert.ToDecimal(dr["costoEstimado"]) : (decimal?)null,
                                CostoReal = dr["costoReal"] != DBNull.Value ? Convert.ToDecimal(dr["costoReal"]) : (decimal?)null,
                                Observaciones = dr["observaciones"].ToString(),
                                FechaRegistro = Convert.ToDateTime(dr["fechaRegistro"]),
                                idNegocio = Convert.ToInt32(dr["idNegocio"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<ServicioTecnico>();
                    // Opcional: manejar el error, por ejemplo, registrarlo en un log.
                }
            }
            return lista;
        }



        public bool InsertarServicioTecnico(ServicioTecnico servicioTecnico, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("INSERT INTO SERVICIOTECNICO (idCliente, producto, fechaRecepcion, fechaEntregaEstimada, fechaEntregaReal, descripcionProblema, descripcionReparacion, estadoServicio, costoEstimado, costoReal, observaciones, fechaRegistro, idNegocio)");
                    query.AppendLine("VALUES (@idCliente, @producto, @fechaRecepcion, @fechaEntregaEstimada, @fechaEntregaReal, @descripcionProblema, @descripcionReparacion, @estadoServicio, @costoEstimado, @costoReal, @observaciones, @fechaRegistro, @idNegocio)");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@idNegocio", servicioTecnico.idNegocio);
                    cmd.Parameters.AddWithValue("@idCliente", servicioTecnico.IdCliente);
                    cmd.Parameters.AddWithValue("@producto", servicioTecnico.Producto);
                    cmd.Parameters.AddWithValue("@fechaRecepcion", servicioTecnico.FechaRecepcion);
                    cmd.Parameters.AddWithValue("@fechaEntregaEstimada", servicioTecnico.FechaEntregaEstimada.HasValue ? (object)servicioTecnico.FechaEntregaEstimada.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@fechaEntregaReal", servicioTecnico.FechaEntregaReal.HasValue ? (object)servicioTecnico.FechaEntregaReal.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@descripcionProblema", servicioTecnico.DescripcionProblema);
                    cmd.Parameters.AddWithValue("@descripcionReparacion", string.IsNullOrEmpty(servicioTecnico.DescripcionReparacion) ? DBNull.Value : (object)servicioTecnico.DescripcionReparacion);
                    cmd.Parameters.AddWithValue("@estadoServicio", servicioTecnico.EstadoServicio);
                    cmd.Parameters.AddWithValue("@costoEstimado", servicioTecnico.CostoEstimado.HasValue ? (object)servicioTecnico.CostoEstimado.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@costoReal", servicioTecnico.CostoReal.HasValue ? (object)servicioTecnico.CostoReal.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@observaciones", string.IsNullOrEmpty(servicioTecnico.Observaciones) ? DBNull.Value : (object)servicioTecnico.Observaciones);
                    cmd.Parameters.AddWithValue("@fechaRegistro", servicioTecnico.FechaRegistro);
                    
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

        public List<ServicioTecnico> ListarServiciosCompletados()
        {
            List<ServicioTecnico> lista = new List<ServicioTecnico>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT * FROM SERVICIOTECNICO");
                    query.AppendLine("WHERE estadoServicio = @estadoCompletado");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@estadoCompletado", "Completado");

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ServicioTecnico()
                            {
                                IdServicio = Convert.ToInt32(dr["idServicio"]),
                                IdCliente = Convert.ToInt32(dr["idCliente"]),
                                Producto = dr["producto"].ToString(),
                                FechaRecepcion = Convert.ToDateTime(dr["fechaRecepcion"]),
                                FechaEntregaEstimada = dr["fechaEntregaEstimada"] != DBNull.Value ? Convert.ToDateTime(dr["fechaEntregaEstimada"]) : (DateTime?)null,
                                FechaEntregaReal = dr["fechaEntregaReal"] != DBNull.Value ? Convert.ToDateTime(dr["fechaEntregaReal"]) : (DateTime?)null,
                                DescripcionProblema = dr["descripcionProblema"].ToString(),
                                DescripcionReparacion = dr["descripcionReparacion"].ToString(),
                                EstadoServicio = dr["estadoServicio"].ToString(),
                                CostoEstimado = dr["costoEstimado"] != DBNull.Value ? Convert.ToDecimal(dr["costoEstimado"]) : (decimal?)null,
                                CostoReal = dr["costoReal"] != DBNull.Value ? Convert.ToDecimal(dr["costoReal"]) : (decimal?)null,
                                Observaciones = dr["observaciones"].ToString(),
                                FechaRegistro = Convert.ToDateTime(dr["fechaRegistro"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<ServicioTecnico>();
                    // Opcional: manejar el error, por ejemplo, registrarlo en un log.
                }
            }
            return lista;
        }

        public bool CambiarEstadoIngresadoAPendiente(int idServicio, out string mensaje)
        {
            return CambiarEstado(idServicio, "PENDIENTE", "","", out mensaje);
        }

        public bool CambiarEstadoPendienteACompletado(int idServicio, string descripcionReparacion, string observaciones, out string mensaje)
        {
            return CambiarEstado(idServicio, "COMPLETADO", descripcionReparacion,observaciones, out mensaje);
        }

        private bool CambiarEstado(int idServicio, string nuevoEstado, string descripcionReparacion, string observaciones, out string mensaje)
        {
            bool resultado = false;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("UPDATE SERVICIOTECNICO SET estadoServicio = @nuevoEstado");

                    // Si el estado es "Pendiente", no se actualiza ni la fecha ni el costo
                    if (nuevoEstado == "COMPLETADO")
                    {
                        query.AppendLine(", descripcionReparacion = @descripcionReparacion, observaciones = @observaciones");
                    }

                    query.AppendLine("WHERE idServicio = @idServicio");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@nuevoEstado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@idServicio", idServicio);

                    // Si el estado es "Completado", se actualizan la fecha y el costo real
                    if (nuevoEstado == "COMPLETADO")
                    {
                        cmd.Parameters.AddWithValue("@descripcionreparacion", descripcionReparacion);
                        cmd.Parameters.AddWithValue("@observaciones", observaciones);
                    }

                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    resultado = false;
                    mensaje = ex.ToString();
                    // Opcional: registrar el error para depuración
                }
            }
            return resultado;
        }


    }
}
