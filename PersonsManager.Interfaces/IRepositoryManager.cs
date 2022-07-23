using PersonsManager.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsManager.Interfaces
{
    public interface IRepositoryManager
    {
        IClientsRepository ClientsRepository { get; }
        IMastersRepository MastersRepository { get; }

        void Save();
    }
}
