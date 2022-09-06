using PersonsManager.Domain.Models;
using PersonsManager.DTO.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsManager.Interfaces.Services
{
    public interface IClientsService
    {
        IEnumerable<Client> GetAll();
        Client GetById(Guid id);
        Client Create(ClientForCreationDto entityForCreation);
        bool Delete(Guid id);
        bool DeleteByUserId(Guid userId);
        bool Update(ClientForUpdateDto entityForUpdate);
        Client GetByUserId(Guid userId);
    }
}
