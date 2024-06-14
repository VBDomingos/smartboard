using SmartBoard.Models;

namespace SmartBoard.Repositories
{
    public interface ITipoDispositivoRepository
    {
        void Create(TelefoneModel telefone);

        IEnumerable<TipoDispositivoModel> GetAllTipoDispositivo();
    }
}
