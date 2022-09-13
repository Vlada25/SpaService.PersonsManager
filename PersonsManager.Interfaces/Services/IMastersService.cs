using PersonsManager.Domain.Models;
using PersonsManager.DTO.Master;

namespace PersonsManager.Interfaces.Services
{
    public interface IMastersService
    {
        IEnumerable<Master> GetAll();
        Master GetById(Guid id);
        Master GetByUserId(Guid userId);
        Master Create(MasterForCreationDto entityForCreation);
        Task<bool> Delete(Guid id);
        Task<bool> DeleteByUserId(Guid userId);
        Task<bool> Update(MasterForUpdateDto entityForUpdate);
    }
}
