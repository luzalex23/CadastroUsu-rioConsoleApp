using CadastroUsuárioConsoleApp.Core.Usecases.Filter;
using CadastroUsuárioConsoleApp.Core.Usecases.Register;
using CadastroUsuárioConsoleApp.Infrastructure.Repositories;
using CadastroUsuárioConsoleApp.Presentation;

var connectionString = "Data Source=users.db;";
var userRepository = new SQLiteUserRepository(connectionString);

var userListUseCase = new UserListUseCase(userRepository);
var userAddUseCase = new UserAddUseCase(userRepository);
var consoleUI = new ConsoleUI(userListUseCase, userAddUseCase);
consoleUI.Run();