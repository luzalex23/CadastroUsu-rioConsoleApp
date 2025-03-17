using CadastroUsuárioConsoleApp.Core.Usecases.Filter;
using CadastroUsuárioConsoleApp.Core.Usecases.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroUsuárioConsoleApp.Presentation;

public class ConsoleUI
{
    private readonly UserListUseCase _userListUseCase;
    private readonly UserAddUseCase _userAddUseCase;

    public ConsoleUI(UserListUseCase userListUseCase, UserAddUseCase userAddUseCase)
    {
        _userListUseCase = userListUseCase;
        _userAddUseCase = userAddUseCase;
    }

    public void Run()
    {
        Console.WriteLine("\nSelecione uma opção:");
        Console.WriteLine("1 - Cadastrar usuário");
        Console.WriteLine("2 - Listar usuários");
        Console.WriteLine("3 - Buscar usuário por nome");
        Console.WriteLine("4 - Sair");
        Console.Write("Opção: ");
        string option = Console.ReadLine();

        switch (option)
        {
            case "1":
                RegisterUser();
                break;
            case "2":
                _userListUseCase.ListUsers();
                break;
            case "3":
                SearchUser();
                break;
            case "4":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Opção inválida.");
                break;
        }


    }

    private void RegisterUser()
    {
        Console.Write("Nome: ");
        string? name = Console.ReadLine();

        Console.Write("E-mail: ");
        string? email = Console.ReadLine();

        Console.Write("Idade: ");
        if (int.TryParse(Console.ReadLine(), out int age))
        {
            if (name != null && email != null)
            {
                _userAddUseCase.RegisterUser(name, email, age);
            }
            else
            {
                Console.WriteLine("Nome ou e-mail inválido. O cadastro foi cancelado.");
            }
        }
        else
        {
            Console.WriteLine("Idade inválida. O cadastro foi cancelado.");
        }
    }

    private void SearchUser()
    {
        Console.Write("Digite o nome do usuário: ");
        string name = Console.ReadLine();
        _userListUseCase.SearchUser(name);
    }
}

