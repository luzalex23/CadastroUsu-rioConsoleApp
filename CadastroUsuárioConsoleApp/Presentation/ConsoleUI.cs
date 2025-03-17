using CadastroUsuarioConsoleApp.Core.Usecases.Filter;
using CadastroUsuarioConsoleApp.Core.Usecases.Register;
using CadastroUsuarioConsoleApp.Core.Usecases.Update;
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
    private readonly UserUpdateUseCase _userUpdateUseCase;

    public ConsoleUI(UserListUseCase userListUseCase, UserAddUseCase userAddUseCase, UserUpdateUseCase userUpdateUseCase)
    {
        _userListUseCase = userListUseCase;
        _userAddUseCase = userAddUseCase;
        _userUpdateUseCase = userUpdateUseCase;
    }

    public void Run()
    {
        while (true) // Loop infinito para manter o menu ativo
        {
            Console.WriteLine("\nSelecione uma opção:");
            Console.WriteLine("1 - Cadastrar usuário");
            Console.WriteLine("2 - Listar usuários");
            Console.WriteLine("3 - Buscar usuário por nome");
            Console.WriteLine("4 - Atualizar usuário ");
            Console.WriteLine("5 - Sair");
            Console.Write("Opção: ");
            string ?option = Console.ReadLine();

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
                    UpdateUser();
                    break;
                case "5":
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
        var users = _userListUseCase.SearchUser(name);

        if (users.Count == 0)
        {
            Console.WriteLine($"Nenhum usuário encontrado com o nome: '{name}'.");
            return;
        }

        foreach (var user in users)
        {
            Console.WriteLine(user);
        }
    }
    private void UpdateUser()
    {
        Console.Write("\n✏ Digite o nome do usuário atual: ");
        string? currentName = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(currentName))
        {
            Console.WriteLine("⚠ Nome inválido.");
            return;
        }

        var users = _userListUseCase.SearchUser(currentName);
        if (users.Count == 0)
        {
            Console.WriteLine($"❌ Nenhum usuário encontrado com o nome '{currentName}'.");
            return;
        }

        if (users.Count > 1)
        {
            Console.WriteLine("\nVários usuários encontrados:");
            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {users[i]}");
            }

            Console.Write("\nDigite o número do usuário que deseja atualizar: ");
            if (int.TryParse(Console.ReadLine(), out int selection) && selection > 0 && selection <= users.Count)
            {
                currentName = users[selection - 1].Name;
            }
            else
            {
                Console.WriteLine("Seleção inválida. A atualização foi cancelada.");
                return;
            }
        }

        Console.Write("Novo nome: ");
        string? newName = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(newName))
        {
            Console.WriteLine("Nome inválido.");
            return;
        }

        Console.Write("Novo e-mail: ");
        string? newEmail = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(newEmail))
        {
            Console.WriteLine("E-mail inválido.");
            return;
        }

        Console.Write("Nova idade: ");
        if (int.TryParse(Console.ReadLine(), out int newAge) && newAge > 0)
        {
            try
            {
                _userUpdateUseCase.UpdateUser(currentName, newName, newEmail, newAge);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar usuário: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Idade inválida.");
        }
    }
}
