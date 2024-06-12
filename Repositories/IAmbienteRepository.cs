using SmartBoard.Models;

namespace SmartBoard.Repositories
{
    public interface IAmbienteRepository
    {
        void Create(AmbienteModel ambiente);
        AmbienteModel Read(int id);
        IEnumerable<AmbienteModel> Read();
        void Update(AmbienteModel ambiente);
        void Delete(int id);
    }
}
