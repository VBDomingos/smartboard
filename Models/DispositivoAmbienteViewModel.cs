namespace SmartBoard.Models
{
    public class DispositivoAmbienteViewModel
    {
        public DispositivoModel Dispositivo { get; set; }
        public List<AmbienteModel> Ambiente { get; set; }
        public List<TipoDispositivoModel> TipoDispositivo { get; set; }

        public DispositivoAmbienteViewModel()
        {
            Dispositivo = new DispositivoModel();
            Ambiente = new List<AmbienteModel>();
            TipoDispositivo = new List<TipoDispositivoModel>();
        }
    }
}
