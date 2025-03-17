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
    /*Método para imprimir o título centralizado*/
    void PrintTitle(string title)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;

        PrintCentered("============================");
        PrintCentered($"      {title}      ");
        PrintCentered("============================");

        Console.ResetColor();
    }
    /*Método para imprimir texto centralizado*/
    void PrintCentered(string text)
    {
        int windowWidth = Console.WindowWidth;
        int textLength = text.Length;
        int leftPadding = (windowWidth - textLength) / 2;

        Console.WriteLine($"{new string(' ', leftPadding)}{text}");
    }
    public void Run()
    {
        while (true) // Loop infinito para manter o menu ativo
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            PrintCentered("============================");
            PrintCentered("      MENU PRINCIPAL      ");
            PrintCentered("============================");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintCentered("1 - Cadastrar usuário");
            PrintCentered("2 - Listar usuários");
            PrintCentered("3 - Buscar usuário por nome");
            PrintCentered("4 - Atualizar usuário");
            PrintCentered("5 - Sair");
            Console.ResetColor();
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
            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey(); // Aguarda o usuário pressionar uma tecla antes de limpar a tela
        }
    }
    private void RegisterUser()
    {
        Console.Clear();
        PrintTitle("CADASTRO DE USUÁRIO");

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
                    Console.WriteLine("\nUsuário cadastrado com sucesso!");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Idade inválida. O cadastro foi cancelado.");
            }

            Console.Write("\nDeseja cadastrar outro usuário? (S/N): ");
            if (Console.ReadLine()?.Trim().ToUpper() != "S") break;
        }
    }

    private void ListUsers()
    {
        Console.Clear();
        PrintTitle("LISTAR USUÁRIOS");

        Console.ForegroundColor = ConsoleColor.Blue;
        _userListUseCase.ListUsers();
        Console.ResetColor();

    }

    private void SearchUser()
    {
        Console.Clear();
        PrintTitle("BUSCAR USUÁRIO POR NOME");

        Console.WriteLine("Digite o nome do usuário: ");
        string name = Console.ReadLine();
        var users = _userListUseCase.SearchUser(name);

        if (users.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintCentered($"Nenhum usuário encontrado com o nome: '{name}'.");
            Console.ResetColor();
            return;
        }

        foreach (var user in users)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            PrintCentered($"Nome: {user.Name}, E-mail: {user.Email},Idade: {user.Age}");
            Console.ResetColor();
        }
    }
    private void UpdateUser()
    {
        Console.Clear();
        PrintTitle("ATUALIZAR USUÁRIO");

        Console.Write("\nDigite o nome do usuário atual: ");
        string? currentName = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(currentName))
        {
            PrintCentered("Nome inválido.");
            return;
        }

        var users = _userListUseCase.SearchUser(currentName);
        if (users.Count == 0)
        {
            PrintCentered($"Nenhum usuário encontrado com o nome '{currentName}'.");
            return;
        }

        if (users.Count > 1)
        {
            PrintCentered("\nVários usuários encontrados:");
            for (int i = 0; i < users.Count; i++)
            {
                PrintCentered($"{i + 1}. {users[i]}");
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
