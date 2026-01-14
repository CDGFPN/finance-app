using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Models
{
    public class Transaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Valor é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Valor deve ser maior que zero")]
        public decimal Value { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Descrição é obrigatória")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Descrição deve ter entre 3 e 200 caracteres")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tipo é obrigatório")]
        public TransactionType Type { get; set; }

        [Required(ErrorMessage = "Categoria é obrigatória")]
        public Category Category { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
