using CadastroUsuarioConsoleApp.Core.Interfaces;

namespace CadastroUsuarioConsoleApp.Core.Usecases.Update;

public class UserUpdateUseCase
{
    private readonly IUserRepository _userRepository;

    public UserUpdateUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void UpdateUser(string currentName, string newName, string newEmail, int newAge)
    {
        var users = _userRepository.GetUserByName(currentName);

          var selectedUser = users[0];
        _userRepository.UpdateUser(selectedUser.Name, newName, newEmail, newAge);
        Console.WriteLine($"Usuário atualizado com sucesso: {newName}, {newEmail}, {newAge}!");
    }
}
