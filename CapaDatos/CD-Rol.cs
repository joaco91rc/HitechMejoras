﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Rol
    {

        public List<Rol> Listar()
        {
            List<Rol> lista = new List<Rol>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select idRol, descripcion from ROL ");





                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    ;
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Rol()
                            {
                                idRol = Convert.ToInt32(dr["idRol"]),
                                descripcion = dr["Descripcion"].ToString()


                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<Rol>();
                    
                }

            }
            return lista;


        }
    }
}
    
