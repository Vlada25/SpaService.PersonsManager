using PersonsManager.Domain.Models;
using PersonsManager.DTO.Client;

namespace PersonsManager.Interfaces.Services
{
    public interface IClientsService
    {
        IEnumerable<Client> GetAll();
        Client GetById(Guid id);
        Client Create(ClientForCreationDto entityForCreation);
        Task<bool> Delete(Guid id);
        bool DeleteByUserId(Guid userId);
        Task<bool> Update(ClientForUpdateDto entityForUpdate);
        Client GetByUserId(Guid userId);
    }
}
