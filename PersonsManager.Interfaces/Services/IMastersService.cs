using PersonsManager.Domain.Models;
using PersonsManager.DTO.Master;

namespace PersonsManager.Interfaces.Services
{
    public interface IMastersService
    {
        Task<IEnumerable<Master>> GetAll();
        Task<IEnumerable<Master>> GetByAddressId(Guid addressId);
        Task<Master> GetById(Guid id);
        Task<Master> GetByUserId(Guid userId);
        Task<Master> Create(MasterForCreationDto entityForCreation);
        Task<bool> Delete(Guid id);
        Task<bool> DeleteByUserId(Guid userId);
        Task<bool> Update(Guid id, MasterForUpdateDto entityForUpdate);
    }
}
