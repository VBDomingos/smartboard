using SmartBoard.Models;

namespace SmartBoard.Repositories
{
    public interface IClienteRepository
    {
        void Create(ClienteModel cliente);
        ClienteModel Read(int id);
        IEnumerable<ClienteModel> Read();
        void UpdatePessoaByCliente(PessoaClienteUpdateViewModel cliente);
        void Delete(int id);

        Tuple<List<ClienteModel>, List<PessoaModel>, List<TelefoneModel>> GetAllClienteInfo();

        Tuple<ClienteModel, PessoaUpdateModel, TelefoneModel> GetClientById(int idCliente);
    }
}
