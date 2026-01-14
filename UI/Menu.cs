using System.Reflection;
using FinanceApp.Attributes;
using FinanceApp.Models;
using FinanceApp.Services;

namespace FinanceApp.UI
{
    public class Menu
    {
        private readonly IUserService userService;
        private readonly ITransactionService transactionService;

        public Menu(IUserService userService, ITransactionService transactionService)
        {
            this.userService = userService;
            this.transactionService = transactionService;
        }

        public void ShowMenu()
        {
            Console.WriteLine("=== Aplicativo de Finanças ===");

            if (userService.CurrentUser == null)
            {
                ShowLoggedOutMenu();
            }
            else
            {
                ShowLoggedInMenu();
            }

            Console.Write("Escolha uma opção: ");
        }

        private void ShowLoggedOutMenu()
        {
            Console.WriteLine("1. Cadastrar");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Sair");
        }

        private void ShowLoggedInMenu()
        {
            Console.WriteLine($"Bem-vindo, {userService.CurrentUser!.Name}!");
            Console.WriteLine("1. Adicionar Receita");
            Console.WriteLine("2. Adicionar Despesa");
            Console.WriteLine("3. Ver Saldo");
            Console.WriteLine("4. Ver Transações");
            Console.WriteLine("5. Editar Transação");
            Console.WriteLine("6. Deletar Transação");
            Console.WriteLine("7. Editar Perfil");
            Console.WriteLine("8. Logout");
            Console.WriteLine("9. Sair");
        }

        public bool HandleUserInput(int userChoice)
        {
            if (userService.CurrentUser == null)
            {
                return HandleLoggedOutInput(userChoice);
            }

            return HandleLoggedInInput(userChoice);
        }

        private bool HandleLoggedOutInput(int userChoice)
        {
            switch (userChoice)
            {
                case 1:
                    Register();
                    break;
                case 2:
                    Login();
                    break;
                case 3:
                    return false;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            return true;
        }

        private bool HandleLoggedInInput(int userChoice)
        {
            switch (userChoice)
            {
                case 1:
                    AddTransaction(TransactionType.Receita);
                    break;
                case 2:
                    AddTransaction(TransactionType.Despesa);
                    break;
                case 3:
                    ShowBalance();
                    break;
                case 4:
                    ShowAllTransactions();
                    break;
                case 5:
                    UpdateTransaction();
                    break;
                case 6:
                    DeleteTransaction();
                    break;
                case 7:
                    EditProfile();
                    break;
                case 8:
                    userService.Logout();
                    Console.WriteLine("Logout realizado com sucesso!");
                    break;
                case 9:
                    return false;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            return true;
        }

        private void Register()
        {
            Console.Write("Digite um nome: ");
            string name = Console.ReadLine()!;

            Console.Write("Digite um email: ");
            string email = Console.ReadLine()!;

            Console.Write("Digite sua senha: ");
            string password = Console.ReadLine()!;

            try
            {
                userService.CreateUser(name, email, password);
                Console.WriteLine("Usuário cadastrado com sucesso!");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Erro: {exception.Message}");
            }
        }

        private void Login()
        {
            Console.Write("Digite seu email: ");
            string email = Console.ReadLine()!;

            Console.Write("Digite sua senha: ");
            string password = Console.ReadLine()!;

            if (userService.GetUserByEmail(email) == null)
            {
                Console.WriteLine("Email não encontrado.");
                return;
            }

            if (userService.Login(email, password))
            {
                Console.WriteLine($"Bem-vindo de volta, {userService.CurrentUser!.Name}!");
            }
            else
            {
                Console.WriteLine("Senha e/ou email inválido(s).");
            }
        }

        private void ShowBalance()
        {
            decimal balance = transactionService.GetBalance(userService.CurrentUser!.Id);
            Console.WriteLine($"Saldo atual: R$ {balance:N2}");
        }

        private void ShowAllTransactions()
        {
            List<Transaction> userTransactions = transactionService.GetAllByUser(userService.CurrentUser!.Id);

            if (userTransactions.Count == 0)
            {
                Console.WriteLine("Nenhuma transação encontrada.");
                return;
            }

            foreach (Transaction transaction in userTransactions)
            {
                string transactionType = transaction.Type == TransactionType.Receita ? "Receita" : "Despesa";
                Console.WriteLine($"ID: {transaction.Id}");
                Console.WriteLine($"  Tipo: {transactionType}");
                Console.WriteLine($"  Valor: R$ {transaction.Value:N2}");
                Console.WriteLine($"  Data: {transaction.Date:dd/MM/yyyy}");
                Console.WriteLine($"  Descrição: {transaction.Description}");
                Console.WriteLine($"  Categoria: {transaction.Category}");
                Console.WriteLine();
            }
        }

        private void EditProfile()
        {
            User currentUser = userService.CurrentUser!;

            Console.WriteLine($"Nome atual: {currentUser.Name}");
            Console.Write("Novo nome (ou pressione Enter para manter): ");
            string newName = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(newName))
            {
                newName = currentUser.Name;
            }

            Console.WriteLine($"Email atual: {currentUser.Email}");
            Console.Write("Novo email (ou pressione Enter para manter): ");
            string newEmail = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(newEmail))
            {
                newEmail = currentUser.Email;
            }

            Console.Write("Nova senha (ou pressione Enter para manter): ");
            string newPassword = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                newPassword = currentUser.Password;
            }

            try
            {
                userService.EditProfile(newName, newEmail, newPassword);
                Console.WriteLine("Perfil atualizado com sucesso!");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Erro: {exception.Message}");
            }
        }

        private void AddTransaction(TransactionType transactionType)
        {
            Console.Write("Digite a descrição: ");
            string description = Console.ReadLine()!;

            Console.Write("Digite o valor: ");
            string valueInput = Console.ReadLine()!;

            if (!decimal.TryParse(valueInput, out decimal value))
            {
                Console.WriteLine("Valor inválido.");
                return;
            }

            List<Category> availableCategories = GetCategoriesByType(transactionType).ToList();

            Console.WriteLine("Escolha uma categoria:");
            for (int index = 0; index < availableCategories.Count; index++)
            {
                Console.WriteLine($"{index + 1}. {availableCategories[index]}");
            }

            Console.Write("Digite o número da categoria: ");
            string categoryInput = Console.ReadLine()!;

            if (!int.TryParse(categoryInput, out int categoryChoice) ||
                categoryChoice < 1 ||
                categoryChoice > availableCategories.Count)
            {
                Console.WriteLine("Opção inválida.");
                return;
            }

            Category selectedCategory = availableCategories[categoryChoice - 1];

            var newTransaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Value = value,
                Date = DateTime.Now,
                Description = description,
                Type = transactionType,
                UserId = userService.CurrentUser!.Id,
                Category = selectedCategory
            };

            transactionService.AddTransaction(newTransaction);
            Console.WriteLine("Transação adicionada com sucesso!");
        }

        private void UpdateTransaction()
        {
            List<Transaction> userTransactions = transactionService
                .GetAllByUser(userService.CurrentUser!.Id)
                .ToList();

            if (userTransactions.Count == 0)
            {
                Console.WriteLine("Nenhuma transação encontrada.");
                return;
            }

            Console.WriteLine("Transações disponíveis:");
            for (int index = 0; index < userTransactions.Count; index++)
            {
                Transaction transaction = userTransactions[index];
                Console.WriteLine($"{index + 1}. {transaction.Description} - R$ {transaction.Value:N2} ({transaction.Type})");
            }

            Console.Write("Escolha uma transação: ");
            string transactionInput = Console.ReadLine()!;

            if (!int.TryParse(transactionInput, out int transactionChoice) ||
                transactionChoice < 1 ||
                transactionChoice > userTransactions.Count)
            {
                Console.WriteLine("Opção inválida.");
                return;
            }

            Transaction selectedTransaction = userTransactions[transactionChoice - 1];

            Console.WriteLine($"Valor atual: R$ {selectedTransaction.Value:N2}");
            Console.Write("Novo valor (pressione Enter para manter): ");
            string newValueInput = Console.ReadLine()!;

            decimal newValue = string.IsNullOrWhiteSpace(newValueInput)
                ? selectedTransaction.Value
                : decimal.Parse(newValueInput);

            Console.WriteLine($"Descrição atual: {selectedTransaction.Description}");
            Console.Write("Nova descrição (pressione Enter para manter): ");
            string newDescriptionInput = Console.ReadLine()!;

            string newDescription = string.IsNullOrWhiteSpace(newDescriptionInput)
                ? selectedTransaction.Description
                : newDescriptionInput;

            Console.WriteLine($"Categoria atual: {selectedTransaction.Category}");
            List<Category> allCategories = Enum.GetValues<Category>().ToList();

            Console.WriteLine("Categorias disponíveis:");
            for (int index = 0; index < allCategories.Count; index++)
            {
                Console.WriteLine($"{index + 1}. {allCategories[index]}");
            }

            Console.Write("Nova categoria (pressione Enter para manter): ");
            string newCategoryInput = Console.ReadLine()!;

            Category newCategory = selectedTransaction.Category;
            TransactionType newType = selectedTransaction.Type;

            if (!string.IsNullOrWhiteSpace(newCategoryInput) &&
                int.TryParse(newCategoryInput, out int categoryChoice) &&
                categoryChoice >= 1 &&
                categoryChoice <= allCategories.Count)
            {
                newCategory = allCategories[categoryChoice - 1];
                TransactionType? detectedType = GetTypeFromCategory(newCategory);

                if (detectedType != null)
                {
                    newType = detectedType.Value;
                }
            }

            selectedTransaction.Value = newValue;
            selectedTransaction.Description = newDescription;
            selectedTransaction.Category = newCategory;
            selectedTransaction.Type = newType;

            transactionService.Update(selectedTransaction);
            Console.WriteLine("Transação atualizada com sucesso!");
        }

        private void DeleteTransaction()
        {
            List<Transaction> userTransactions = transactionService
                .GetAllByUser(userService.CurrentUser!.Id)
                .ToList();

            if (userTransactions.Count == 0)
            {
                Console.WriteLine("Nenhuma transação encontrada.");
                return;
            }

            Console.WriteLine("Transações disponíveis:");
            for (int index = 0; index < userTransactions.Count; index++)
            {
                Transaction transaction = userTransactions[index];
                Console.WriteLine($"{index + 1}. {transaction.Description} - R$ {transaction.Value:N2} ({transaction.Type})");
            }

            Console.Write("Escolha uma transação para deletar: ");
            string transactionInput = Console.ReadLine()!;

            if (!int.TryParse(transactionInput, out int transactionChoice) ||
                transactionChoice < 1 ||
                transactionChoice > userTransactions.Count)
            {
                Console.WriteLine("Opção inválida.");
                return;
            }

            Transaction selectedTransaction = userTransactions[transactionChoice - 1];

            Console.Write($"Tem certeza que deseja deletar '{selectedTransaction.Description}'? (s/n): ");
            string confirmation = Console.ReadLine()!.ToLower();

            if (confirmation != "s")
            {
                Console.WriteLine("Operação cancelada.");
                return;
            }

            transactionService.Delete(selectedTransaction.Id);
            Console.WriteLine("Transação deletada com sucesso!");
        }

        private static TransactionType? GetTypeFromCategory(Category category)
        {
            FieldInfo? categoryField = typeof(Category).GetField(category.ToString());
            TransactionTypeAttribute? attribute = categoryField?.GetCustomAttribute<TransactionTypeAttribute>();
            return attribute?.Type;
        }

        private static IEnumerable<Category> GetCategoriesByType(TransactionType targetType)
        {
            return Enum.GetValues<Category>()
                .Where(category =>
                {
                    FieldInfo? categoryField = typeof(Category).GetField(category.ToString());
                    TransactionTypeAttribute? attribute = categoryField?.GetCustomAttribute<TransactionTypeAttribute>();
                    return attribute?.Type == targetType;
                });
        }
    }
}
