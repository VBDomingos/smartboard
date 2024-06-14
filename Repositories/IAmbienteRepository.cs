using SmartBoard.Models;

namespace SmartBoard.Repositories
{
    public interface IAmbienteRepository
    {
        void Create(AmbienteModel ambiente);
        AmbienteModel Read(int id);

        IEnumerable<AmbienteModel> GetAllAmbientes();
    }
}
