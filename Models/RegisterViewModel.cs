using System.ComponentModel.DataAnnotations;

namespace PousadaIdentity.Models
{
    public class RegisterViewModel
    {


        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [EmailAddress]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Password", ErrorMessage = "As senhas divergem!")]
        [Required(ErrorMessage = "O campo Confirmar Senha é obrigatório.")]
        public string? ConfirmSenha { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        public string? CPF { get; set; }

        [Required(ErrorMessage = "O campo Usuario é obrigatório.")]
        public string? Usuario { get; set; }



    }
}
