using SmartBoard.Models;

namespace SmartBoard.Repositories
{
    public interface IPessoaRepository
    {
        void CreateClient(PessoaClienteViewModel pessoaClienteModel);
        PessoaModel Read(int id);
        IEnumerable<PessoaModel> Read();
        void Update(PessoaModel pessoa);
        void Delete(int id);

        PessoaModel Login(string email, string senha);
    }
}
