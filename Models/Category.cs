using FinanceApp.Attributes;

namespace FinanceApp.Models
{
    public enum Category
    {
        [TransactionType(TransactionType.Despesa)]
        Alimentacao,

        [TransactionType(TransactionType.Despesa)]
        Transporte,

        [TransactionType(TransactionType.Despesa)]
        Moradia,

        [TransactionType(TransactionType.Despesa)]
        Contas,

        [TransactionType(TransactionType.Despesa)]
        Lazer,

        [TransactionType(TransactionType.Despesa)]
        Saude,

        [TransactionType(TransactionType.Despesa)]
        Educacao,

        [TransactionType(TransactionType.Receita)]
        Salario,

        [TransactionType(TransactionType.Receita)]
        Investimentos,

        [TransactionType(TransactionType.Receita)]
        Freelance,

        Outros
    }
}
