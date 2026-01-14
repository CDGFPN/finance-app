using FinanceApp.Models;
using FinanceApp.Repositories;

namespace FinanceApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        public void AddTransaction(Transaction transaction)
        {
            if (transaction.Value < 0)
            {
                throw new ArgumentException("O valor da transação não pode ser negativo.");
            }

            transactionRepository.Save(transaction);
        }

        public List<Transaction> GetAllByUser(Guid userId)
        {
            return transactionRepository.GetByUserId(userId);
        }

        public Transaction? GetById(Guid transactionId)
        {
            return transactionRepository.GetById(transactionId);
        }

        public void Update(Transaction transaction)
        {
            transactionRepository.Update(transaction);
        }

        public void Delete(Guid transactionId)
        {
            transactionRepository.Delete(transactionId);
        }

        public decimal GetBalance(Guid userId)
        {
            List<Transaction> userTransactions = transactionRepository.GetByUserId(userId);

            decimal totalIncome = userTransactions
                .Where(transaction => transaction.Type == TransactionType.Receita)
                .Sum(transaction => transaction.Value);

            decimal totalExpenses = userTransactions
                .Where(transaction => transaction.Type == TransactionType.Despesa)
                .Sum(transaction => transaction.Value);

            return totalIncome - totalExpenses;
        }
    }
}
