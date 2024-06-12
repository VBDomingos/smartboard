using SmartBoard.Models;

namespace SmartBoard.Repositories
{
    public interface IAdministradorRepository
    {
        void Create(AdministradorModel administrador);
        AdministradorModel Read(int id);
        IEnumerable<AdministradorModel> Read();
        void Update(AdministradorModel administrador);
        void Delete(int id);
    }
}
