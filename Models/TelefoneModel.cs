using System.ComponentModel.DataAnnotations;

namespace SmartBoard.Models
{
    public class TelefoneModel
    {
        public int IdTelefone { get; set; }
        public int IdPessoa { get; set; }
        [Required(ErrorMessage = "Digite o Celular")]
        public Int64 Numero { get; set; }
    }
}
