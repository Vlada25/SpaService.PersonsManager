using PersonsManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsManager.Interfaces.Repositories
{
    public interface IClientsRepository
    {
        IEnumerable<Client> GetAll(bool trackChanges);
        Client GetById(Guid id, bool trackChanges);
        Client GetByUserId(Guid userId);
        void Create(Client entity);
        void Delete(Client entity);
    }
}
