using SmartBoard.Models;

namespace SmartBoard.Repositories
{
    public interface ITecnicoRepository
    {
        void Create(TecnicoModel tecnico);
        TecnicoModel Read(int id);
        IEnumerable<TecnicoModel> Read();
        void Update(TecnicoModel tecnico);
        void Delete(int id);
    }
}
