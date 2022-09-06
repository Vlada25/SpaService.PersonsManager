using PersonsManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsManager.Interfaces.Repositories
{
    public interface IMastersRepository
    {
        IEnumerable<Master> GetAll(bool trackChanges);
        Master GetById(Guid id, bool trackChanges);
        Master GetByUserId(Guid userId);
        void Create(Master entity);
        void Delete(Master entity);
    }
}
