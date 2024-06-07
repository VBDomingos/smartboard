using SmartBoard.Models;

namespace SmartBoard.Repositories
{
    public interface IClienteRepository
    {
        void Create(Cliente cliente);
        Cliente Read(int id);
        IEnumerable<Cliente> Read();
        void Update(Cliente cliente);
        void Delete(int id);
    }
}
