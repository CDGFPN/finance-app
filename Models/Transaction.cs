namespace FinanceApp.Models
{
    public class Transaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Value { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; } = string.Empty;
        public TransactionType Type { get; set; }
        public Category Category { get; set; }

        // Foreign Key - referência ao User dono desta transação
        public Guid UserId { get; set; }

        // Navigation property - acesso direto ao objeto User
        public User User { get; set; } = null!;
    }
}
