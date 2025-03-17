using CadastroUsuárioConsoleApp.Core.Usecases.Filter;
using CadastroUsuárioConsoleApp.Core.Usecases.Register;
using CadastroUsuárioConsoleApp.Infrastructure.Repositories;
using CadastroUsuárioConsoleApp.Presentation;

var userListUseCase = new UserListUseCase(new InMemoryUserRepository());
var userAddUseCase = new UserAddUseCase(new InMemoryUserRepository());
var consoleUI = new ConsoleUI(userListUseCase, userAddUseCase);
consoleUI.Run();