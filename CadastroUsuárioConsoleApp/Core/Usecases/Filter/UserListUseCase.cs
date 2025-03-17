using CadastroUsuarioConsoleApp.Core.Entities;
using CadastroUsuarioConsoleApp.Core.Interfaces;

namespace CadastroUsuarioConsoleApp.Core.Usecases.Filter;
/*Use case para caso de uso referente a busca de usuários*/
public class UserListUseCase
{
    private readonly IUserRepository _userRepository;
    public UserListUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    /*Método para imprimir texto centralizado*/
    void PrintCentered(string text)
    {
        int windowWidth = Console.WindowWidth;
        int textLength = text.Length;
        int leftPadding = (windowWidth - textLength) / 2;

        Console.WriteLine($"{new string(' ', leftPadding)}{text}");
    }
    /*Aqui Filtra todos usuáris*/
    public void ListUsers()
    {
        var users = _userRepository.GetUsers();
        if (users.Count == 0)
        {
            PrintCentered("Nenhum usuário cadastrado.");
            return;
        }

        PrintCentered("\nLista de Usuários:");
        foreach (var user in users)
        {
            PrintCentered($"Nome: {user.Name}, E-mail: {user.Email},Idade: {user.Age}");
        }
    }


    /*Aqui Filtra um usuário pelo nome*/
    public List<User> SearchUser(string name)
    {
        return _userRepository.GetUserByName(name);

    }

}
