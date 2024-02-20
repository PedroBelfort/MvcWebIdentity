using System.ComponentModel.DataAnnotations;

namespace MvcWebIdentity.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="O Email é obrigatório")]
        [EmailAddress(ErrorMessage ="Email inválido")]
        public string? Email { get; set; }
        [Required(ErrorMessage ="A senha é obrigatória")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name ="Lembrar-me")]
        public bool RememberMe { get; set; }
    }
}
