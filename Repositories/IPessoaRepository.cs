using SmartBoard.Models;

namespace SmartBoard.Repositories
{
    public interface IPessoaRepository
    {
        void Create(PessoaModel pessoa);
        PessoaModel Read(int id);
        IEnumerable<PessoaModel> Read();
        void Update(PessoaModel pessoa);
        void Delete(int id);

        PessoaModel Login(string email, string senha);
    }
}
