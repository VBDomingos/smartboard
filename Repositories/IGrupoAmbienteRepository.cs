using SmartBoard.Models;

namespace SmartBoard.Repositories
{
    public interface IGrupoAmbienteRepository
    {
        void Create(GrupoAmbienteModel grupoAmbiente);
        GrupoAmbienteModel Read(int id);
        IEnumerable<GrupoAmbienteModel> Read();
        void Update(GrupoAmbienteModel grupoAmbiente);
        void Delete(int id);
    }
}