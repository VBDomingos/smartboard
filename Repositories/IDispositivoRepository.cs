using SmartBoard.Models;

namespace SmartBoard.Repositories
{
    public interface IDispositivoRepository
    {
        void Create(DispositivoModel dispositivo);
        DispositivoModel Read(int id);
        IEnumerable<DispositivoModel> Read();
        void Update(DispositivoModel dispositivo);
        void Delete(int id);
    }
}
