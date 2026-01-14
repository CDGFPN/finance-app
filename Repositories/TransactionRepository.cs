using System.Text.Json;
using FinanceApp.Models;

namespace FinanceApp.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private const string FilePath = "transactions.json";
        private List<Transaction> transactions = new();

        public TransactionRepository()
        {
            LoadFromFile();
        }

        public void Save(Transaction transaction)
        {
            transactions.Add(transaction);
            SaveToFile();
        }

        public List<Transaction> GetAll()
        {
            return new List<Transaction>(transactions);
        }

        public Transaction? GetById(Guid id)
        {
            return transactions.FirstOrDefault(transaction => transaction.Id == id);
        }

        public List<Transaction> GetByUserId(Guid userId)
        {
            return transactions
                .Where(transaction => transaction.UserId == userId)
                .ToList();
        }

        public void Update(Transaction transaction)
        {
            int transactionIndex = transactions.FindIndex(t => t.Id == transaction.Id);

            if (transactionIndex != -1)
            {
                transactions[transactionIndex] = transaction;
            }

            SaveToFile();
        }

        public void Delete(Guid id)
        {
            int transactionIndex = transactions.FindIndex(transaction => transaction.Id == id);

            if (transactionIndex != -1)
            {
                transactions.RemoveAt(transactionIndex);
            }

            SaveToFile();
        }

        private void LoadFromFile()
        {
            if (!File.Exists(FilePath))
            {
                return;
            }

            string jsonContent = File.ReadAllText(FilePath);
            transactions = JsonSerializer.Deserialize<List<Transaction>>(jsonContent) ?? new();
        }

        private void SaveToFile()
        {
            var serializerOptions = new JsonSerializerOptions { WriteIndented = true };
            string jsonContent = JsonSerializer.Serialize(transactions, serializerOptions);
            File.WriteAllText(FilePath, jsonContent);
        }
    }
}
