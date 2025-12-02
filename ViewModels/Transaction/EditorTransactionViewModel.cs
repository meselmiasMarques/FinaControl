using System.ComponentModel.DataAnnotations;
using FinaControl.Models;
using FinaControl.Models.Enums;

namespace FinaControl.ViewModels.Transaction;

public class EditorTransactionViewModel
{
    [Required(ErrorMessage = "O Campo é obrigatorio")]
    public decimal Amount { get; set; }
    
    [Required(ErrorMessage = "O Campo é obrigatorio")]
    public ETransactionType Type { get; set; } = ETransactionType.Widthdrawal;

    [Required(ErrorMessage = "O Campo é obrigatorio")]
    public long CategoryId { get; set; }
    
}