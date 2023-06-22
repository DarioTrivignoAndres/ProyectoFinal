using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestion
{
using System.Collections;
using System.Data;
using System.Data.SqlClient;

public class ProductoDataService
{
    private readonly string connectionString;

    public ProductoDataService(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public int CrearProducto(Producto Producto)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            const string query = @"INSERT INTO Producto (Descripciones, Costo, PrecioVenta, Stock) 
                                   VALUES (@Descripciones, @Costo, @PrecioVenta, @Stock);
                                   SELECT SCOPE_IDENTITY()";

           
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Descripciones", Producto.Descripciones);
                command.Parameters.AddWithValue("@Descripciones", Producto.Descripciones);
                command.Parameters.AddWithValue("@PrecioVenta", Producto.PrecioVenta);
                command.Parameters.AddWithValue("@Stock", Producto.Stock);

                // Ejecutamos la consulta SQL utilizando ExecuteScalar() que retorna el id generado para nuevo registro insertado
                return (int)(decimal)command.ExecuteScalar();
            }
        }
    }

    public bool EliminarProducto(int id)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            const string query = @"DELETE FROM Producto WHERE Id = @Id";
            
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                return command.ExecuteNonQuery() > 0;
            }
        }
    }

      public bool ModificarProducto(Producto Producto)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            const string query = @"UPDATE Producto SET (Descripciones = @Descripciones, Costo = @Costo, PrecioVenta = @PrecioVenta, 
                                                         Stock = @Stock) WHERE Id = @Id";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Descripciones", Producto.Descripciones);
                command.Parameters.AddWithValue("@Costo", Producto.Costo);
                command.Parameters.AddWithValue("@PrecioVenta", Producto.PrecioVenta);
                command.Parameters.AddWithValue("@Stock", Producto.Stock);
                
                return command.ExecuteNonQuery() > 0;
            }
        }
    }

    public Producto ObtenerProductoPorId(int IdUsuario)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            const string query = @"SELECT Id, Descripciones, Costo, PrecioVenta, Stock FROM Producto WHERE IdUsuario = @IdUsuario";
            
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Producto;
                        {
                            id = reader.GetInt64(0); 
                            Descripciones = reader.GetString(1);
                            Costo = reader.GetInt64(2);
                            PrecioVenta = reader.GetInt64(3);
                            Stock = reader.GetInt64(4);
                            
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
                connection.Close();
                
                }
        }
    }
}
}