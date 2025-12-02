using System.ComponentModel.DataAnnotations;

namespace FinaControl.ViewModels.Category;

public record EditorCategoryViewModel([Required(ErrorMessage = "O Nome é obrigatório")] string? Name);