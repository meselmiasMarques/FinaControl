using FinaControl.Models.Enums;

namespace FinaControl.Models
{
    public class Transaction
    {
        public long Id { get; set; }

        public string Description { get; set; } = string.Empty;
        
        public decimal Amount { get; set; }
        public ETransactionType Type { get; set; } = ETransactionType.Widthdrawal;

        public long CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public long UserId { get; set; }
        public virtual User? User { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}