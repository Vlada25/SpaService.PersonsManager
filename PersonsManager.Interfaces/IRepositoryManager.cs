using PersonsManager.Interfaces.Repositories;

namespace PersonsManager.Interfaces
{
    public interface IRepositoryManager
    {
        IClientsRepository ClientsRepository { get; }
        IMastersRepository MastersRepository { get; }

        void Save();
    }
}
