using SmartBoard.Models;

namespace SmartBoard.Repositories
{
    public interface IDispositivoRepository
    {
        void Create(DispositivoModel dispositivo);
        Tuple<ClienteModel, List<DispositivoModel>, List<AmbienteModel>, List<TipoDispositivoModel>> GetAllDispositivosByCliente(int id);
        IEnumerable<DispositivoModel> Read();
        void Update(DispositivoModel dispositivo);
        void Delete(int id);
    }
}
