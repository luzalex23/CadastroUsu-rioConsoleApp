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

        if (users.Count == 0)
        {
            Console.WriteLine($"❌ Nenhum usuário encontrado com o nome '{currentName}'.");
            return;
        }

        if (users.Count == 1)
        {
            var user = users[0];
            _userRepository.UpdateUser(user.Name, newName, newEmail, newAge);
            Console.WriteLine($"Usuário atualizado com sucesso: {newName}, {newEmail}, {newAge}!");
            return;
        }

        // Se houver mais de um usuário, pedir para selecionar
        Console.WriteLine("\nVários usuários encontrados:");
        for (int i = 0; i < users.Count; i++)
        {
            Console.WriteLine($"{i + 1}. Nome: {users[i].Name}, E-mail: {users[i].Email}, Idade: {users[i].Age}");
        }

        Console.Write("\nDigite o número do usuário que deseja atualizar: ");
        if (int.TryParse(Console.ReadLine(), out int selection) && selection > 0 && selection <= users.Count)
        {
            var selectedUser = users[selection - 1];
            _userRepository.UpdateUser(selectedUser.Name, newName, newEmail, newAge);
            Console.WriteLine($"Usuário '{selectedUser.Name}' atualizado para: {newName}, {newEmail}, {newAge}!");
        }
        else
        {
            Console.WriteLine("Seleção inválida. A atualização foi cancelada.");
        }
    }
}
