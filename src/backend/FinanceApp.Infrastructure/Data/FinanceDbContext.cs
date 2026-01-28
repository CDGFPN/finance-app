using Microsoft.EntityFrameworkCore;
using FinanceApp.Domain.Entities;

namespace FinanceApp.Infrastructure.Data
{
    public class FinanceDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public FinanceDbContext(DbContextOptions<FinanceDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacionamento: User (1) -> (*) Transactions
            modelBuilder.Entity<User>()
                .HasMany(user => user.Transactions)
                .WithOne(transaction => transaction.User)
                .HasForeignKey(transaction => transaction.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Email único
            modelBuilder.Entity<User>()
                .HasIndex(user => user.Email)
                .IsUnique();

            // Precisão do decimal (18 dígitos, 2 casas decimais)
            modelBuilder.Entity<Transaction>()
                .Property(transaction => transaction.Value)
                .HasPrecision(18, 2);

            // Enums salvos como string no banco
            modelBuilder.Entity<Transaction>()
                .Property(transaction => transaction.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Transaction>()
                .Property(transaction => transaction.Category)
                .HasConversion<string>();
        }
    }
}
