using PersonsManager.Domain.Models;
using PersonsManager.DTO.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsManager.Interfaces.Services
{
    public interface IMastersService
    {
        IEnumerable<Master> GetAll();
        Master GetById(Guid id);
        Master Create(MasterForCreationDto entityForCreation);
        bool Delete(Guid id);
        bool Update(MasterForUpdateDto entityForUpdate);
    }
}
