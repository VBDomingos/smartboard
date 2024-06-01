using Dashboard.Models;

namespace Dashboard.Repositories
{
    public interface ITecnicoRepository
    {
        void Create(Tecnico tecnico);
        Tecnico Read(int id);
        IEnumerable<Tecnico> Read();
        void Update(Tecnico tecnico);
        void Delete(int id);
    }
}
