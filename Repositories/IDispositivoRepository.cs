using Dashboard.Models;

namespace Dashboard.Repositories
{
    public interface IDispositivoRepository
    {
        void Create(Dispositivo dispositivo);
        Dispositivo Read(int id);
        IEnumerable<Dispositivo> Read();
        void Update(Dispositivo dispositivo);
        void Delete(int id);
    }
}
