using CadastroUsuarioConsoleApp.Core.Usecases.Filter;
using CadastroUsuarioConsoleApp.Core.Usecases.Register;
using CadastroUsuarioConsoleApp.Core.Usecases.Update;
using CadastroUsuarioConsoleApp.Infrastructure.Repositories;
using CadastroUsuarioConsoleApp.Presentation;
/*Instanciando o repositórios*/
var connectionString = "Data Source=users.db;";
var userRepository = new SQLiteUserRepository(connectionString);
/*Instanciando os casos de uso*/
var userListUseCase = new UserListUseCase(userRepository);
var userAddUseCase = new UserAddUseCase(userRepository);
var userUpdateUseCase = new UserUpdateUseCase(userRepository);
var consoleUI = new ConsoleUI(userListUseCase, userAddUseCase,userUpdateUseCase);

consoleUI.Run();