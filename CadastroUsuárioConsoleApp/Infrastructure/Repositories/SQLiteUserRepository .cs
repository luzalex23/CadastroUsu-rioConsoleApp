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

    public List<User> GetUserByName(string name)
    {
        var users = new List<User>();
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT Name, Email, Age FROM Users WHERE Name LIKE @Name";
        command.Parameters.AddWithValue("@Name", $"{name}%"); // Busca por nomes que começam com o termo

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            users.Add(new User(reader.GetString(0), reader.GetString(1), reader.GetInt32(2)));
        }

        return users;
    }
    public void UpdateUser(string currentName, string newName, string newEmail, int newAge)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
        UPDATE Users
        SET Name = @NewName, Email = @NewEmail, Age = @NewAge
        WHERE Name = @CurrentName;
    ";
        command.Parameters.AddWithValue("@NewName", newName);
        command.Parameters.AddWithValue("@NewEmail", newEmail);
        command.Parameters.AddWithValue("@NewAge", newAge);
        command.Parameters.AddWithValue("@CurrentName", currentName);

        int rowsAffected = command.ExecuteNonQuery();
        Console.WriteLine(rowsAffected > 0
            ? "Usuário atualizado com sucesso!"
            : "Usuário não encontrado.");
    }

}
