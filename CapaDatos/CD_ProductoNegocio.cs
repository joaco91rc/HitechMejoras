﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_ProductoNegocio
    {

        public int ObtenerStockProductoEnSucursal(int idProducto, int idNegocio)
        {
            int stock = 0;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand(@"
            SELECT stock 
            FROM PRODUCTONEGOCIO 
            WHERE idProducto = @idProducto AND idNegocio = @idNegocio", oconexion);
                cmd.Parameters.AddWithValue("@idProducto", idProducto);
                cmd.Parameters.AddWithValue("@idNegocio", idNegocio);

                oconexion.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (reader["stock"] != DBNull.Value)
                        {
                            stock = Convert.ToInt32(reader["stock"]);
                        }
                    }
                }
            }
            return stock;
        }

        //este metodo ses como deberia de funcionar la realidad pero el cliente m,epide otro emtodo que pise stock y que no actualice el ya existente
        public string CargarOActualizarStockProducto(int idProducto, int idNegocio, int stock)
        {
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                SqlCommand checkCmd = new SqlCommand(@"
            SELECT stock 
            FROM PRODUCTONEGOCIO 
            WHERE idProducto = @idProducto AND idNegocio = @idNegocio", oconexion);
                checkCmd.Parameters.AddWithValue("@idProducto", idProducto);
                checkCmd.Parameters.AddWithValue("@idNegocio", idNegocio);

                oconexion.Open();
                object result = checkCmd.ExecuteScalar();

                if (result == null)
                {
                    // Insertar nuevo registro
                    SqlCommand insertCmd = new SqlCommand(@"
                INSERT INTO PRODUCTONEGOCIO (idProducto, idNegocio, stock)
                VALUES (@idProducto, @idNegocio, @stock)", oconexion);
                    insertCmd.Parameters.AddWithValue("@idProducto", idProducto);
                    insertCmd.Parameters.AddWithValue("@idNegocio", idNegocio);
                    insertCmd.Parameters.AddWithValue("@stock", stock);

                    int rowsAffected = insertCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return "Se ha insertado un nuevo registro de stock.";
                    }
                }
                else
                {
                    // Actualizar el stock existente
                    int currentStock = Convert.ToInt32(result);
                    int newStock = currentStock + stock;

                    SqlCommand updateCmd = new SqlCommand(@"
                UPDATE PRODUCTONEGOCIO
                SET stock = @newStock
                WHERE idProducto = @idProducto AND idNegocio = @idNegocio", oconexion);
                    updateCmd.Parameters.AddWithValue("@idProducto", idProducto);
                    updateCmd.Parameters.AddWithValue("@idNegocio", idNegocio);
                    updateCmd.Parameters.AddWithValue("@newStock", newStock);

                    int rowsAffected = updateCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return "El stock se ha actualizado correctamente.";
                    }
                }
            }

            return "No se realizó ninguna operación.";
        }

        //este es el metodo q sobrescribe stock
        public void SobrescribirStock(int idProducto, int idNegocio, int stock)
        {
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                SqlCommand checkCmd = new SqlCommand(@"
        SELECT stock 
        FROM PRODUCTONEGOCIO 
        WHERE idProducto = @idProducto AND idNegocio = @idNegocio", oconexion);
                checkCmd.Parameters.AddWithValue("@idProducto", idProducto);
                checkCmd.Parameters.AddWithValue("@idNegocio", idNegocio);

                oconexion.Open();
                object result = checkCmd.ExecuteScalar();

                if (result == null)
                {
                    // Insertar nuevo registro
                    SqlCommand insertCmd = new SqlCommand(@"
            INSERT INTO PRODUCTONEGOCIO (idProducto, idNegocio, stock)
            VALUES (@idProducto, @idNegocio, @stock)", oconexion);
                    insertCmd.Parameters.AddWithValue("@idProducto", idProducto);
                    insertCmd.Parameters.AddWithValue("@idNegocio", idNegocio);
                    insertCmd.Parameters.AddWithValue("@stock", stock);

                    insertCmd.ExecuteNonQuery();
                }
                else
                {
                    // Actualizar el stock existente
                    SqlCommand updateCmd = new SqlCommand(@"
            UPDATE PRODUCTONEGOCIO
            SET stock = @stock
            WHERE idProducto = @idProducto AND idNegocio = @idNegocio", oconexion);
                    updateCmd.Parameters.AddWithValue("@idProducto", idProducto);
                    updateCmd.Parameters.AddWithValue("@idNegocio", idNegocio);
                    updateCmd.Parameters.AddWithValue("@stock", stock);

                    updateCmd.ExecuteNonQuery();
                }
            }
        }


        public void EliminarProductoNegocio(int idProducto, int idNegocio)
        {
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                // Comando SQL para eliminar el producto
                SqlCommand deleteCmd = new SqlCommand(@"
        DELETE FROM PRODUCTONEGOCIO 
        WHERE idProducto = @idProducto AND idNegocio = @idNegocio", oconexion);

                deleteCmd.Parameters.AddWithValue("@idProducto", idProducto);
                deleteCmd.Parameters.AddWithValue("@idNegocio", idNegocio);

                oconexion.Open();
                int rowsAffected = deleteCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Producto eliminado exitosamente.");
                }
                else
                {
                    Console.WriteLine("No se encontró el producto con el ID especificado.");
                }
            }
        }
    }
}
