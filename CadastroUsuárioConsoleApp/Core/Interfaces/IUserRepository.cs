using CadastroUsuárioConsoleApp.Core.Entities;

namespace CadastroUsuárioConsoleApp.Core.Interfaces;

public interface IUserRepository
{
    void AddUser(User user);
    List<User> GetUsers();
    User GetUserByName(string name);
}
