using CadastroUsuárioConsoleApp.Core.Entities;
using CadastroUsuarioConsoleApp.Core.Interfaces;

namespace CadastroUsuarioConsoleApp.Core.Usecases.Register;
/*Use case para registrar umnovo usuário, aqui validamos alguns dados como idade por exemplo*/
public class UserAddUseCase
{
    private readonly IUserRepository _userRepository;
    public UserAddUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public void RegisterUser(string name, string email, int age)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || age <= 0)
        {
            Console.WriteLine("Dados inválidos. O usuário não foi cadastrado.");
            return;
        }

        var user = new User(name, email, age);
        _userRepository.AddUser(user);
        Console.WriteLine("Usuário cadastrado com sucesso!");
    }

}
