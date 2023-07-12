using Api_Enpoint.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Api_Enpoint.Repository
{
    public class ADO_ProductoVendido
    {
        static string connectionString = "Server=localhost\\MSSQLSERVER01;Database=master;Trusted_Connection=True";
        public static List<ProductoVendido> ObtenerProductosVendidos()
        {
            List<ProductoVendido> productosVendidos = new List<ProductoVendido>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, IdProducto, Stock, IdVenta FROM Producto";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ProductoVendido productoVendido = new ProductoVendido();
                    productoVendido.Id = Convert.ToInt32(reader["Id"]);
                    productoVendido.IdProducto = Convert.ToInt32(reader["IdProducto"]);
                    productoVendido.Stock = Convert.ToInt32(reader["Stock"]);
                    productoVendido.IdVenta = Convert.ToInt32(reader["IdVenta"]);

                    productosVendidos.Add(productoVendido);

                }

                connection.Close();

            }

            return productosVendidos;

        }

        public static void EliminarProductosVendidosId(int id)
        {
            var query = "DELETE FROM ProductoVendido where Id = @Id ";

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
                            Console.WriteLine("ProductoVendido Eliminado correctamente");
                        else
                            Console.WriteLine("No se encontro ningun productovendido con este id {0}", id);
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error al eliminar el productovendido: " + ex.Message);
                    }

                }
                connection.Close();
            }

        }

    }
}