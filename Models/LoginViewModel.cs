using System.ComponentModel.DataAnnotations;

namespace PousadaIdentity.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "o email é obrigatorio")]
        [EmailAddress(ErrorMessage ="Email invalido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatoria")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Lembrar-me")]
        public bool RememberMe { get; set; }

        public int PessoaId { get; set; }

    }
}
