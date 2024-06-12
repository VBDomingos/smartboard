using SmartBoard.Models;

namespace SmartBoard.Repositories
{
    public interface IAmbienteGrupoRepository
    {
        void Create(AmbienteGrupoModel ambienteGrupo);
        IEnumerable<AmbienteGrupoModel> Read();
        void Delete(int idGrupo, int idAmbiente);
    }
}
