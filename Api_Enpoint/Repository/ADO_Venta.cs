using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Api_Enpoint.Models
{
    public class ADO_Venta
    {        
        static string connectionString = "Server=localhost\\MSSQLSERVER01;Database=master;Trusted_Connection=True";

        public static List<Venta> ObtenerVentas()
        {
            List<Venta> ventas = new List<Venta>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Comentarios, CantidadVentas FROM Venta";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Venta venta = new Venta();
                    venta.Id = Convert.ToInt32(reader["Id"]);
                    venta.Comentarios = (string)reader["Nombre"];
                    venta.CantidadVentas = Convert.ToInt32(reader["CantidadVentas"]);
                    ventas.Add(venta);

                }

                connection.Close();

            }

            return ventas;

        }

    }
}