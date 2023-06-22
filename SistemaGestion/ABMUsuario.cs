using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestion
{
using System.Data;
using System.Data.SqlClient;

public class UsuarioDataService
{
    private readonly string connectionString;

    // Constructor que recibe el string de conexión a la Base de Datos
    public UsuarioDataService(string connectionString)
    {
        this.connectionString = connectionString;
    }

    // Método para insertar un nuevo usuario en la Base de Datos
    // Recibe un objeto Usuario con la información del usuario a crear
    // Devuelve el Id asignado al nuevo registro
    public int CrearUsuario(Usuario usuario)
    {
        // Creamos una nueva conexión a la base de datos utilizando el string de conexión que se recibió en el constructor
        using (var connection = new SqlConnection(connectionString))
        {
            // Abrimos la conexión
            connection.Open();

            // Definimos la consulta SQL que vamos a ejecutar
            const string query = @"INSERT INTO Usuario (nombre, apellido, nombreUsuario, contrasenia, mail) 
                                   VALUES (@Nombre, @Apellido, @NombreUsuario, @Contrasenia, @Mail);
                                   SELECT SCOPE_IDENTITY()";
            // Creamos una nueva instancia de SqlCommand con la consulta SQL y la conexión asociada
            using (var command = new SqlCommand(query, connection))
            {
                // Agregamos los parámetros correspondientes a la consulta SQL utilizando el objeto Usuario recibido como parámetro
                command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                command.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                command.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                command.Parameters.AddWithValue("@Contraseña", usuario.Contrasenia);
                command.Parameters.AddWithValue("@Mail", usuario.Mail);

                // Ejecutamos la consulta SQL utilizando ExecuteScalar() que retorna el id generado para nuevo registro insertado
                return (int)(decimal)command.ExecuteScalar();
            }
        }
    }

    // Método para eliminar un usuario de la Base de Datos según su Id
    // Recibe el Id del usuario que se desea eliminar
    // Devuelve true si la eliminación fue exitosa, false si no
    public bool EliminarUsuario(int id)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Definimos la consulta SQL que vamos a ejecutar
            const string query = @"DELETE FROM Usuario WHERE Id = @Id";
            // Creamos una nueva instancia de SqlCommand con la consulta SQL y la conexión asociada
            using (var command = new SqlCommand(query, connection))
            {
                // Agregamos el parámetro correspondiente a la consulta SQL utilizando el Id recibido como parámetro
                command.Parameters.AddWithValue("@Id", id);

                // Ejecutamos la consulta SQL utilizando ExecuteNonQuery() que retorna la cantidad de filas afectadas por la consulta SQL
                // En este caso, debería ser 1 si se eliminó el usuario correctamente, o 0 si no se encontró el usuario con el Id correspondiente
                return command.ExecuteNonQuery() > 0;
            }
        }
    }

    // Método para modificar un usuario existente en la Base de Datos
    // Recibe un objeto Usuario con la información actualizada del usuario a modificar
    // Devuelve true si la modificación fue exitosa, false si no
    public bool ModificarUsuario(Usuario usuario)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Definimos la consulta SQL que vamos a ejecutar
            const string query = @"UPDATE Usuario SET Nombre = @Nombre, Apellido = @Apellido, 
                                   NombreUsuario = @NombreUsuario, Contrasenia = @Contrasenia, Mail = @Mail 
                                   WHERE Id = @Id";
            // Creamos una nueva instancia de SqlCommand con la consulta SQL y la conexión asociada
            using (var command = new SqlCommand(query, connection))
            {
                // Agregamos los parámetros correspondientes a la consulta SQL utilizando el objeto Usuario recibido como parámetro
                command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                command.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                command.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                command.Parameters.AddWithValue("@Contraseña", usuario.Contrasenia);
                command.Parameters.AddWithValue("@Mail", usuario.Mail);
                command.Parameters.AddWithValue("@Id", usuario.Id);

                // Ejecutamos la consulta SQL utilizando ExecuteNonQuery() que retorna la cantidad de filas afectadas por la consulta SQL
                // En este caso, debería ser 1 si se modificó el usuario correctamente, o 0 si no se encontró el usuario con el Id correspondiente
                return command.ExecuteNonQuery() > 0;
            }
        }
    }

    // Método para obtener la información de un usuario según su Id
    // Recibe el Id del usuario que se desea obtener
    // Devuelve un objeto Usuario con la información correspondiente, o null si no se encontró
    public Usuario ObtenerUsuarioPorId(int id)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Definimos la consulta SQL que vamos a ejecutar
            const string query = @"SELECT Id, Nombre, Apellido, NombreUsuario, Contrasenia, Mail 
                                   FROM Usuario WHERE Id = @Id";
            // Creamos una nueva instancia de SqlCommand con la consulta SQL y la conexión asociada
            using (var command = new SqlCommand(query, connection))
            {
                // Agregamos el parámetro correspondiente a la consulta SQL utilizando el Id recibido como parámetro
                command.Parameters.AddWithValue("@Id", id);

                // Ejecutamos la consulta SQL utilizando ExecuteReader() que retorna un objeto SqlDataReader que podemos utilizar para leer los datos devueltos por la consulta SQL
                using (var reader = command.ExecuteReader())
                                    
                    {
                    if (reader.Read())
                    {
                        // Creamos un nuevo objeto Usuario con la información obtenida del objeto SqlDataReader
                        return new Usuario
                        {
                            Id = reader.GetInt64(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            NombreUsuario = reader.GetString(3),
                            Contrasenia = reader.GetString(4),
                            Mail = reader.GetString(5)
                        };
                    }
                    else
                    {
                        // Si no se encontró ningún registro, devolvemos null
                        return null;
                    }
                }
                connection.Close();


                }
            }
    }
}
}