using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestion
{
    public Venta ObtenerVentaPorId(int Id)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            const string query = @"SELECT Id, Comentarios, FROM Venta WHERE Id = @Id";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", Id);

                using (var reader = command.ExecuteReader())

                {
                    if (reader.Read())
                    {
                        return new Venta;
                        {
                            Id = reader.GetInt64(0);
                            Comentarios = reader.GetString(1);
                        };
                    }
                    else
                    {
                        return null;
                    }
                    connection.Close();

                }
            }
        }
      }
    }