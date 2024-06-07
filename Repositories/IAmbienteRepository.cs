using SmartBoard.Models;

namespace SmartBoard.Repositories
{
    public interface IAmbienteRepository
    {
        void Create(Ambiente ambiente);
        Ambiente Read(int id);
        IEnumerable<Ambiente> Read();
        void Update(Ambiente ambiente);
        void Delete(int id);
    }
}
