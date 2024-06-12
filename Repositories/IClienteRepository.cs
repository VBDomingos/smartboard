using SmartBoard.Models;

namespace SmartBoard.Repositories
{
    public interface IClienteRepository
    {
        void Create(ClienteModel cliente);
        ClienteModel Read(int id);
        IEnumerable<ClienteModel> Read();
        void Update(ClienteModel cliente);
        void Delete(int id);
    }
}
