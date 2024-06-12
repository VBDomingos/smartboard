using SmartBoard.Models;

namespace SmartBoard.Repositories
{
    public interface IRelatorioRepository
    {
        void Create(RelatorioModel relatorio);
        RelatorioModel Read(int id);
        IEnumerable<RelatorioModel> Read();
        void Update(RelatorioModel relatorio);
        void Delete(int id);
    }
}
