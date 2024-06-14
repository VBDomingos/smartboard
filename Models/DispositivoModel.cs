using System.ComponentModel.DataAnnotations;

namespace SmartBoard.Models
{
    public class DispositivoModel
    {
        public int IdDispositivo { get; set; }
        [Required(ErrorMessage = "Digite o Nome do Dispositivo")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Selecione o Ambiente")]
        public int IdAmbiente { get; set; }
        [Required(ErrorMessage = "Digite a porta")]
        public int Porta { get; set; }
        public int IdCliente { get; set; }
        public int IdTecnico { get; set; }
        public int IdTipoDispositivo { get; set; }
    }
}