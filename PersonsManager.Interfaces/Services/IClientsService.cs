using PersonsManager.Domain.Models;
using PersonsManager.DTO.Client;

namespace PersonsManager.Interfaces.Services
{
    public interface IClientsService
    {
        Task<IEnumerable<Client>> GetAll();
        Task<Client> GetById(Guid id);
        Task<Client> Create(ClientForCreationDto entityForCreation);
        Task<bool> Delete(Guid id);
        Task<bool> DeleteByUserId(Guid userId);
        Task<Client> GetByUserId(Guid userId);
        Task<bool> Update(Guid id, ClientForUpdateDto entityForUpdate);
    }
}
