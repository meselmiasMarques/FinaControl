using System.ComponentModel.DataAnnotations;

namespace FinaControl.ViewModels.Role;

public record EditorRoleViewModel([Required(ErrorMessage = "O Nome é obrigatório")] string? Name);