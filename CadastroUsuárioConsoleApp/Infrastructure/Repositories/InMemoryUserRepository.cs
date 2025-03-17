using CadastroUsuárioConsoleApp.Core.Entities;
using CadastroUsuárioConsoleApp.Core.Interfaces;

namespace CadastroUsuárioConsoleApp.Infrastructure.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public void AddUser(User user) => _users.Add(user);

    public List<User> GetUsers() => _users;

    public User GetUserByName(string name) => _users.FirstOrDefault(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) ?? throw new InvalidOperationException("User not found");
}
