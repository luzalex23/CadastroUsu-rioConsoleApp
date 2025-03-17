using CadastroUsuarioConsoleApp.Core.Entities;
using CadastroUsuarioConsoleApp.Core.Interfaces;
using Microsoft.Data.Sqlite;

namespace CadastroUsuarioConsoleApp.Infrastructure.Repositories;

public class SQLiteUserRepository : IUserRepository
{
    private readonly string _connectionString;
    public SQLiteUserRepository(string connectionString)
    {
        _connectionString = connectionString;

        // Criar tabela, se não existir
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Email TEXT NOT NULL,
                Age INTEGER NOT NULL
            );
        ";
        command.ExecuteNonQuery();
    }
    public void AddUser(User user)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Users (Name, Email, Age) VALUES (@name, @Email, @Age);";
        command.Parameters.AddWithValue("@name", user.Name);
        command.Parameters.AddWithValue("@Email", user.Email);
        command.Parameters.AddWithValue("@Age", user.Age);

        command.ExecuteNonQuery();
    }

    public List<User> GetUsers()
    {
        var users = new List<User>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT Name, Email, Age FROM Users;";
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            users.Add(new User(reader.GetString(0), reader.GetString(1), reader.GetInt32(2)));
        }

        return users;
    }

    public User GetUserByName(string name)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT Name, Email, Age FROM Users WHERE Name LIKE @Name;";
        command.Parameters.AddWithValue("@Name", $"%{name}%");

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new User(reader.GetString(0), reader.GetString(1), reader.GetInt32(2));
        }
        else
        {
            throw new InvalidOperationException($"User with name '{name}' not found.");
        }
    }

}
