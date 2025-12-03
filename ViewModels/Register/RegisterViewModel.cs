using System.ComponentModel.DataAnnotations;

namespace FinaControl.ViewModels.Register;

public record RegisterViewModel(
    [Required(ErrorMessage = "{0} é obrigatório")]
    string Name,
    [Required(ErrorMessage = "{0} é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail Invalido")]
    string Email 
    );