using System.ComponentModel.DataAnnotations;
using FinaControl.Models;
using FinaControl.Models.Enums;

namespace FinaControl.ViewModels.Transaction;

public class EditorTransactionViewModel
{
    [Required(ErrorMessage = "O Campo é obrigatorio")]
    public decimal Amount { get; set; }
    
    [Required(ErrorMessage = "O Campo é obrigatorio")]
    [MaxLength(ErrorMessage = "Digite uma descrição de no máximo 100 caracteres")]
    public string Description { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "O Campo é obrigatorio")]
    public long CategoryId { get; set; }
    
}