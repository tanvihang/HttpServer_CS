using System.Runtime.CompilerServices;
using HttpServerBasic.Model;
using Microsoft.Data.SqlClient;

namespace HttpServerBasic.Sys.Repository.Impl;

public class UserRepository:IUserRepository
{
    private static UserRepository instance;
    private string connectionString;
    private SqlConnection connection;
    
    private UserRepository()
    {
        
    }

    public static UserRepository GetInstance()
    {
        if (instance == null)
        {
            instance = new UserRepository();
            //TODO connection string注入
        }

        return instance;
    }

    public void SetConnectionString(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public User GetUser(string userName)
    {
        //TODO implementation of getting data from user table
        using (connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM [User] WHERE userName  = @userName";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@userName", userName);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User(
                            (string)reader["userName"],
                            (string)reader["password"]
                            );
                    }
                }
            }
            
            return null;
        }
    }
}