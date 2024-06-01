using Dashboard.Models;

namespace Dashboard.Repositories
{
    public interface IAdministradorRepository
    {
        void Create(Administrador administrador);
        Administrador Read(int id);
        IEnumerable<Administrador> Read();
        void Update(Administrador administrador);
        void Delete(int id);
    }
}
