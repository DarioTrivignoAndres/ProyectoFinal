using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestion
{
    public ProductoVendido ObtenerProductoPorId(int IdUsuario)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            const string query = @"SELECT Stock, IdProducto,IdVenta FROM ProductoVendido INNER JOIN Producto WHERE Producto.IdUsuario = @IdUsuario";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                using (var reader = command.ExecuteReader())

                {
                    if (reader.Read())
                    {
                        return new ProductoVendido;
                        {
                            Stock = reader.GetInt64(0);
                            IdProducto = reader.GetInt64(1);
                            IdVenta = reader.GetInt64(2);  

                        };
                    }
                    else
                    {
                        return null;
                    }
             connection.Close();

                }

            }
