using System.ComponentModel.DataAnnotations;

namespace PousadaIdentity.Models
{
    public class LoginViewModel
    {
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Lembrar-me")]
        public bool RememberMe { get; set; }

        public int PessoaId { get; set; }

    }
}
