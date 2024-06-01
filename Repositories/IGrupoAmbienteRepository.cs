using Dashboard.Models;

namespace Dashboard.Repositories
{
    public interface IGrupoAmbienteRepository
    {
        void Create(GrupoAmbiente grupoAmbiente);
        GrupoAmbiente Read(int id);
        IEnumerable<GrupoAmbiente> Read();
        void Update(GrupoAmbiente grupoAmbiente);
        void Delete(int id);
    }
}