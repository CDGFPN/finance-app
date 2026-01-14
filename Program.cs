using FinanceApp.Repositories;
using FinanceApp.Services;
using FinanceApp.UI;

var userRepository = new UserRepository();
var userService = new UserService(userRepository);

var transactionRepository = new TransactionRepository();
var transactionService = new TransactionService(transactionRepository);

var menu = new Menu(userService, transactionService);

bool applicationIsRunning = true;

while (applicationIsRunning)
{
    Console.Clear();
    menu.ShowMenu();

    string? userInput = Console.ReadLine();

    if (int.TryParse(userInput, out int userChoice))
    {
        applicationIsRunning = menu.HandleUserInput(userChoice);
    }
    else
    {
        Console.WriteLine("Entrada inválida.");
    }

    if (applicationIsRunning)
    {
        Console.WriteLine("\nPressione Enter para continuar...");
        Console.ReadLine();
    }
}

Console.WriteLine("Aplicação encerrada.");
