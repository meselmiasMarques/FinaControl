using System.ComponentModel.DataAnnotations;

namespace FinaControl.ViewModels.Role;

public class AssociateRoleUserViewModel
{
    [Required(ErrorMessage = "O Usuário é obrigatorio")]
    public long UserId { get; set; }
    
    [Required(ErrorMessage = "O Perfil é obrigatorio")]
    public long RoleId { get; set; }
}