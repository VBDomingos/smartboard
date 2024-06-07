using SmartBoard.Models;

namespace SmartBoard.Repositories
{
    public interface IAmbienteGrupoRepository
    {
        void Create(AmbienteGrupo ambienteGrupo);
        IEnumerable<AmbienteGrupo> Read();
        void Delete(int idGrupo, int idAmbiente);
    }
}
