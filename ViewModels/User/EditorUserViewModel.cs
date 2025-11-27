using System.ComponentModel.DataAnnotations;

namespace FinaControl.ViewModels.User;

public record EditorUserViewModel(
    [Required(ErrorMessage = "O nome é obrigatório")] string name,
    [Required(ErrorMessage = "O Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Digite um email valido")]
    string email
    );