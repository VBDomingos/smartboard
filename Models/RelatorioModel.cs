namespace SmartBoard.Models
{
    public class RelatorioModel
    {
        public int IdRelatorio { get; set; }
        public int IdCliente { get; set; }
        public int IdTecnico { get; set; }
        public int IdDispositivo { get; set; }
        public DateTime DataInstalacao { get; set; }
        public string Descricao { get; set; }
    }
}
