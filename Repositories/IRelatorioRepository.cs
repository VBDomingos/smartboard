using Dashboard.Models;

namespace Dashboard.Repositories
{
    public interface IRelatorioRepository
    {
        void Create(Relatorio relatorio);
        Relatorio Read(int id);
        IEnumerable<Relatorio> Read();
        void Update(Relatorio relatorio);
        void Delete(int id);
    }
}
