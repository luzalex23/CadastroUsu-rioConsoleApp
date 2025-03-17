using CadastroUsuarioConsoleApp.Core.Entities;

namespace CadastroUsuarioConsoleApp.Core.Interfaces;

public interface IUserRepository
{
    void AddUser(User user);
    List<User> GetUsers();
    User GetUserByName(string name);
}
