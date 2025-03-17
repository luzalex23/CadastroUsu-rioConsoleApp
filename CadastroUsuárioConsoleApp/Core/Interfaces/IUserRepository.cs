using CadastroUsuarioConsoleApp.Core.Entities;

namespace CadastroUsuarioConsoleApp.Core.Interfaces;

public interface IUserRepository
{
    void AddUser(User user);
    void UpdateUser(string currentName, string newName, string newEmail, int newAge);
    List<User> GetUsers();
    List<User> GetUserByName(string name);
}
