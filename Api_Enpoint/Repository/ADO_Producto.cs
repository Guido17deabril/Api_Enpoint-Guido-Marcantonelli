using Api_Enpoint.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Api_Enpoint.Repository
{
    public class ADO_Producto
    {
        static string connectionString = "Server=localhost\\MSSQLSERVER01;Database=master;Trusted_Connection=True";

        public static List<Producto> ObtenerProductos()
        {
            List<Producto> productos = new List<Producto>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Descripciones, Costo, PrecioVenta, Stock, IdUsuario FROM Producto";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Producto producto = new Producto();
                    producto.Id = Convert.ToInt32(reader["Id"]);
                    producto.Descripcion = (string)reader["Description"];
                    producto.Costo = Convert.ToInt32(reader["Costo"]);
                    producto.PrecioVenta = Convert.ToInt32(reader["PrecioVenta"]);
                    producto.Stock = Convert.ToInt32(reader["Stock"]);
                    producto.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);

                    productos.Add(producto);

                }

                connection.Close();

            }

            return productos;

        }

        public static void EliminarProductoId(int id)
        {
            var query = "DELETE FROM Productos where Id = @Id ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("Id", id);

                    try
                    {
                        connection.Open();

                        SqlParameter rowsAffectedParameter = new SqlParameter("@RowsAffected", SqlDbType.Int);
                        rowsAffectedParameter.Direction = ParameterDirection.Output;
                        command.Parameters.Add(rowsAffectedParameter);
                        command.ExecuteNonQuery();

                        int filasAfectadas = (int)rowsAffectedParameter.Value;

                        if (filasAfectadas > 0)
                            Console.WriteLine("Producto Eliminado correctamente");
                        else
                            Console.WriteLine("No se encontro ningun producto con este id {0}", id);
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error al eliminar el producto: " + ex.Message);
                    }

                }
                connection.Close();
            }

        }
    }
}