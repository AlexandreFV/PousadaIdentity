using System.ComponentModel.DataAnnotations;

namespace PousadaIdentity.Models
{
    public class RegisterViewModel
    {


        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Password", ErrorMessage = "As senhas divergem")]
        public string? ConfirmSenha { get; set; }

        [Required]
        public string? Nome { get; set; }

        [Required]
        public string? CPF { get; set; }

        [Required]
        public string? Usuario { get; set; }



    }
}
