using Api_Enpoint.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Api_Enpoint.Repository
{
    public class ADO_Usuario
    {

        static string connectionString = " Server=localhost\\MSSQLSERVER01;Database=master;Trusted_Connection=True";

        public static List<Usuario> GetUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Nombre, Apellido, NombreUsuario, Contrasena, Mail FROM Usuario";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = Convert.ToInt32(reader["Id"]);
                    usuario.Nombre = (string)reader["Nombre"];
                    usuario.Apellido = (string)reader["Apellido"];
                    usuario.NombreUsuario = (string)reader["NombreUsuario"];
                    usuario.Contraseña = (string)reader["Contrasena"];
                    usuario.Mail = (string)reader["Mail"];
                    usuarios.Add(usuario);

                }

                connection.Close();

            }

            return usuarios;
        }

        public void ModificarUsuario(int idUsuario, string nombre, string apellido)
        {
            string query = "UPDATE Usuario SET Nombre = @nombreAct, Apellido = @apellidoAct WHERE idUsuario = @IdUsuario";

            using (SqlConnection connection = new SqlConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    command.Parameters.AddWithValue("@nombreAct", nombre);
                    command.Parameters.AddWithValue("@apellidoAct", apellido);

                    connection.Open();

                    try
                    {

                        int filasAfectadas = command.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                            Console.WriteLine("Usuario actualizado correctamente");
                        else
                            Console.WriteLine("No se encontro ningun usuario con ese ID");

                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error al actualizar el usuario " + ex.Message);
                    }
                    catch (Exception ex) 
                    {
                        Console.WriteLine("Error inesperado al actualizar el usuario " + ex.Message);
                    
                    }

                    connection.Close();

                }


            }

        }
    }
}