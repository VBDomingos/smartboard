using SmartBoard.Models;

namespace SmartBoard.Repositories
{
    public interface IPessoaRepository
    {
        void Create(Pessoa pessoa);
        Pessoa Read(int id);
        IEnumerable<Pessoa> Read();
        void Update(Pessoa pessoa);
        void Delete(int id);
    }
}
