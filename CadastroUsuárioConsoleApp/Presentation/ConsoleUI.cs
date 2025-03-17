using CadastroUsuarioConsoleApp.Core.Usecases.Filter;
using CadastroUsuarioConsoleApp.Core.Usecases.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroUsuarioConsoleApp.Presentation;

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
        while (true) // Loop infinito para manter o menu ativo
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
                    ListUsers();
                    break;
                case "3":
                    SearchUser();
                    break;
                case "4":
                    Console.WriteLine("Saindo do programa. Até mais!");
                    return; // Encerra o programa
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }




    private void RegisterUser()
    {
        while (true)
        {
            Console.Write("Nome: ");
            string? name = Console.ReadLine();

            Console.Write("E-mail: ");
            string? email = Console.ReadLine();

            Console.Write("Idade: ");
            if (int.TryParse(Console.ReadLine(), out int age))
            {
                try
                {
                    _userAddUseCase.RegisterUser(name, email, age);
                    Console.WriteLine("Usuário cadastrado com sucesso!");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Idade inválida. O cadastro foi cancelado.");
            }
            ListUsers();
            Console.Write("Deseja cadastrar outro usuário? (S/N): ");
            if (Console.ReadLine()?.Trim().ToUpper() != "S") break;
        }
    }

    private void ListUsers()
    {
        Console.WriteLine("\nLista de Usuários:");
        _userListUseCase.ListUsers();
    }

    private void SearchUser()
    {
        Console.Write("Digite o nome do usuário: ");
        string name = Console.ReadLine();
        _userListUseCase.SearchUser(name);
    }
}

