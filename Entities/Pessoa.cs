using System.ComponentModel.DataAnnotations;

namespace PousadaIdentity.Entities
{
    public class Pessoa
    {

        [Key]
        public int PessoaId { get; set; }

        public string? Nome { get; set; }

        public string? CPF { get; set; }

        public string? Senha { get; set; }

        public string? Email { get; set; }

        public string? Usuario { get; set; }

        public string? Token { get; set;}

    }
}
