using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SmartBoard.Models
{
    public class PessoaModel
    {
        public int IdPessoa { get; set; }
        [Required(ErrorMessage = "Digite o Nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite a senha")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Digite o cpf")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Digite o cep")]
        public string Cep { get; set; }
        [Required(ErrorMessage = "Digite o número da casa")]
        public Nullable<int> Numero { get; set; }
        public bool Ativo { get; set; }
        public Char TipoPessoa { get; set; }
    }

    public class PessoaUpdateModel
    {
        public int IdPessoa { get; set; }
        [Required(ErrorMessage = "Digite o Nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite o cpf")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Digite o cep")]
        public string Cep { get; set; }
        [Required(ErrorMessage = "Digite o número da casa")]
        public Nullable<int> Numero { get; set; }
        public bool Ativo { get; set; }
        public Char TipoPessoa { get; set; }
    }
}
