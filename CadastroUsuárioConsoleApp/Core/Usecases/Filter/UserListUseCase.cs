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
    /*Aqui Filtra todos usuáris*/
    public void ListUsers()
    {
        var users = _userRepository.GetUsers();
        if (users.Count == 0)
        {
            Console.WriteLine("Nenhum usuário cadastrado.");
            return;
        }

        Console.WriteLine("\nLista de Usuários:");
        foreach (var user in users)
        {
            Console.WriteLine(user);
        }
    }


    /*Aqui Filtra um usuário pelo nome*/
    public void SearchUser(string name)
    {
        var user = _userRepository.GetUserByName(name);
        Console.WriteLine($"Nenhum usuário encontrado com o nome: '{name}'.");
    }

}
