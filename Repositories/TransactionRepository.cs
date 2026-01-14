using FinanceApp.Data;
using FinanceApp.Models;

namespace FinanceApp.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly FinanceDbContext context;

        public TransactionRepository(FinanceDbContext context)
        {
            this.context = context;
        }

        public void Save(Transaction transaction)
        {
            context.Transactions.Add(transaction);
            context.SaveChanges();
        }

        public List<Transaction> GetAll()
        {
            return context.Transactions.ToList();
        }

        public Transaction? GetById(Guid id)
        {
            return context.Transactions.FirstOrDefault(transaction => transaction.Id == id);
        }

        public List<Transaction> GetByUserId(Guid userId)
        {
            return context.Transactions
                .Where(transaction => transaction.UserId == userId)
                .ToList();
        }

        public void Update(Transaction transaction)
        {
            context.Transactions.Update(transaction);
            context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            Transaction? transaction = GetById(id);

            if (transaction != null)
            {
                context.Transactions.Remove(transaction);
                context.SaveChanges();
            }
        }
    }
}
