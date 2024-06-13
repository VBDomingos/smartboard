using System.ComponentModel.DataAnnotations;

namespace SmartBoard.Models
{
    public class TelefoneModel
    {
        public int IdTelefone { get; set; }
        public int IdPessoa { get; set; }
        [Required(ErrorMessage = "Digite o Celular")]
        public int Celular { get; set; }
        public Nullable<int> Comercial { get; set; }
    }
}
