using FinanceApp.Models;

namespace FinanceApp.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class TransactionTypeAttribute : Attribute
    {
        public TransactionType Type { get; }

        public TransactionTypeAttribute(TransactionType type)
        {
            Type = type;
        }
    }
}
