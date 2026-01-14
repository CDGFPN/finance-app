using FinanceApp.Models;

namespace FinanceApp.Services
{
    public interface ITransactionService
    {
        void AddTransaction(Transaction transaction);
        List<Transaction> GetAllByUser(Guid userId);
        Transaction? GetById(Guid transactionId);
        void Update(Transaction transaction);
        void Delete(Guid transactionId);
        decimal GetBalance(Guid userId);
    }
}
