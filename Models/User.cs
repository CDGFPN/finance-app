using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [StringLength(150, ErrorMessage = "Email muito longo")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$",
            ErrorMessage = "Senha deve ter no mínimo 8 caracteres, incluindo: maiúscula, número e símbolo")]
        public string Password { get; set; } = string.Empty;

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
