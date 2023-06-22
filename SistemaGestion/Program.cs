// See https://aka.ms/new-console-template for more information


using SistemaGestion;
using System.Data.SqlClient;

static void Main(string[] args)

{
    private static Usuario IniciarSesion(string nombreUsuario, string contrasenia)

    {
        
        using var connection = new SqlConnection(connectionString);

        {

            connection.Open();

            const string query = @"SELECT id, nombre, apellido, nombreUsuario, contrasenia, mail
        
                                    FROM Usuario WHERE nombreUsuario = @nombreUsuario AND contrasenia = @contrasenia";

            using var command = new SqlCommand(query,connection);

            {

                command.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);

                command.Parameters.AddWithValue("@contrasenia", contrasenia);

                using (var reader = command.ExecuteReader())

                {

                    if (reader.Read())

                    {
                        return new Usuario;

                        {
                            id = reader.GetInt64(0);

                            nombre = reader.GetString(1);

                            apellido = reader.GetString(2);

                            nombreUsuario = reader.GetString(3);

                            contrasenia = reader.GetString(4);

                            mail = reader.GetString(5);

                        }

                    }

                    else
                    {
                        return null;

                    }

                }

            }

        }

    }
    
}