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
    public class CD_Deuda
    {
        public List<Deuda> ListarDeudas()
        {
            List<Deuda> lista = new List<Deuda>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT d.idDeuda, d.fecha, d.costo, d.estado,d.simboloMoneda, d.idTraspasoMercaderia, d.idSucursalOrigen, d.idSucursalDestino, ");
                    query.AppendLine("nOrigen.nombre AS nombreSucursalOrigen, nDestino.nombre AS nombreSucursalDestino, ");
                    query.AppendLine("ot.idProducto, p.nombre AS nombreProducto "); // Añadir idProducto y nombre del producto
                    query.AppendLine("FROM Deuda d ");
                    query.AppendLine("INNER JOIN Negocio nOrigen ON d.idSucursalOrigen = nOrigen.idNegocio ");
                    query.AppendLine("INNER JOIN Negocio nDestino ON d.idSucursalDestino = nDestino.idNegocio ");
                    query.AppendLine("INNER JOIN OrdenTraspaso ot ON d.idTraspasoMercaderia = ot.Id "); // JOIN para obtener OrdenTraspaso
                    query.AppendLine("INNER JOIN Producto p ON ot.idProducto = p.idProducto "); // JOIN para obtener el nombre del producto
                    query.AppendLine("WHERE d.estado = 'NO PAGO'");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Deuda()
                            {
                                idDeuda = Convert.ToInt32(dr["idDeuda"]),
                                fecha = Convert.ToDateTime(dr["fecha"]),
                                costo = Convert.ToDecimal(dr["costo"]),
                                nombreSucursalOrigen = dr["nombreSucursalOrigen"].ToString(),
                                nombreSucursalDestino = dr["nombreSucursalDestino"].ToString(),
                                estado = dr["estado"].ToString(),
                                idTraspasoMercaderia = dr["idTraspasoMercaderia"] != DBNull.Value ? (int?)Convert.ToInt32(dr["idTraspasoMercaderia"]) : null,
                                idSucursalOrigen = Convert.ToInt32(dr["idSucursalOrigen"]),
                                idSucursalDestino = Convert.ToInt32(dr["idSucursalDestino"]),
                                nombreProducto = dr["nombreProducto"].ToString(), // Asumiendo que añadiste esta propiedad a Deuda
                                simboloMoneda = dr["simboloMoneda"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Deuda>();
                    // Opcional: registrar el error para depuración
                }
            }
            return lista;
        }



        public bool InsertarDeuda(Deuda deuda)
        {
            bool resultado = false;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("INSERT INTO Deuda (fecha, costo, idSucursalOrigen, idSucursalDestino, estado, idTraspasoMercaderia, simboloMoneda)");
                    query.AppendLine("VALUES (@fecha, @costo, @idSucursalOrigen, @idSucursalDestino, @estado, @idTraspasoMercaderia,@simboloMoneda)");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@fecha", deuda.fecha);
                    cmd.Parameters.AddWithValue("@costo", deuda.costo);
                    cmd.Parameters.AddWithValue("@idSucursalOrigen", deuda.idSucursalOrigen);
                    cmd.Parameters.AddWithValue("@idSucursalDestino", deuda.idSucursalDestino);
                    cmd.Parameters.AddWithValue("@estado", deuda.estado);
                    cmd.Parameters.AddWithValue("@simboloMoneda", deuda.simboloMoneda);
                    cmd.Parameters.AddWithValue("@idTraspasoMercaderia", deuda.idTraspasoMercaderia.HasValue ? (object)deuda.idTraspasoMercaderia.Value : DBNull.Value);

                    

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

        public bool ActualizarEstadoDeuda(int idDeuda)
        {
            bool resultado = false;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("UPDATE Deuda SET estado = @estado WHERE idDeuda = @idDeuda");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@estado", "PAGO"); // Establecer el estado a "PAGO"
                    cmd.Parameters.AddWithValue("@idDeuda", idDeuda); // Identificar la deuda a actualizar

                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0; // Retorna true si se actualizó al menos una fila
                }
                catch (Exception ex)
                {
                    resultado = false;
                    // Opcional: registrar el error para depuración
                }
            }
            return resultado;
        }


        public Dictionary<int, Dictionary<string, decimal>> CalcularDeudaTotalPorSucursalYMoneda(int idSucursalDeudora)
        {
            var deudasPorSucursal = new Dictionary<int, Dictionary<string, decimal>>(); // Diccionario para almacenar deudas por sucursal y moneda
            var sucursales = new[] { 1, 2, 3, 4 }; // IDs de las sucursales específicas
            var monedas = new[] { "ARS", "USD" }; // Tipos de monedas

            // Inicializar el diccionario con cero para todas las sucursales y monedas
            foreach (var sucursal in sucursales)
            {
                deudasPorSucursal[sucursal] = new Dictionary<string, decimal>();
                foreach (var moneda in monedas)
                {
                    deudasPorSucursal[sucursal][moneda] = 0;
                }
            }

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT idSucursalOrigen, simboloMoneda, ISNULL(SUM(costo), 0) AS TotalDeuda ");
                    query.AppendLine("FROM Deuda ");
                    query.AppendLine("WHERE estado = 'NO PAGO' ");
                    query.AppendLine("AND idSucursalDestino = @idSucursalDeudora "); // Filtrar por la sucursal deudora
                    query.AppendLine("GROUP BY idSucursalOrigen, simboloMoneda"); // Agrupar por sucursal y moneda

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idSucursalDeudora", idSucursalDeudora); // Pasar el parámetro de la sucursal deudora
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int idSucursal = Convert.ToInt32(dr["idSucursalOrigen"]);
                            string moneda = dr["simboloMoneda"].ToString();
                            decimal totalDeuda = Convert.ToDecimal(dr["TotalDeuda"]);

                            // Almacenar en el diccionario por sucursal y moneda
                            if (deudasPorSucursal.ContainsKey(idSucursal) && deudasPorSucursal[idSucursal].ContainsKey(moneda))
                            {
                                deudasPorSucursal[idSucursal][moneda] = totalDeuda;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejar la excepción, opcionalmente registrar el error
                    // Console.WriteLine(ex.Message); // Para depuración
                }
            }
            return deudasPorSucursal; // Devolver el diccionario de deudas por sucursal y moneda
        }



        public Dictionary<int, Dictionary<string, decimal>> CalcularDeudaPorSucursalRestante(int idSucursalDeudora)
        {
            var deudasRestantesPorSucursal = new Dictionary<int, Dictionary<string, decimal>>(); // Diccionario para almacenar deudas por sucursal y moneda
            var sucursales = new[] { 1, 2, 3, 4 }; // IDs de las sucursales específicas
            var monedas = new[] { "ARS", "USD" }; // Tipos de monedas

            // Inicializar el diccionario con cero para todas las sucursales y monedas
            foreach (var sucursal in sucursales)
            {
                if (sucursal != idSucursalDeudora) // Solo inicializamos para las sucursales que no son la deudora
                {
                    deudasRestantesPorSucursal[sucursal] = new Dictionary<string, decimal>();
                    foreach (var moneda in monedas)
                    {
                        deudasRestantesPorSucursal[sucursal][moneda] = 0;
                    }
                }
            }

            // Obtener las deudas totales por sucursal y moneda (ya hecho previamente en otro método)
            var deudasTotales = CalcularDeudaTotalPorSucursalYMoneda(idSucursalDeudora);

           

            return deudasTotales; // Devolver el diccionario con las deudas restantes por sucursal
        }




    }
}
