using PersonsManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsManager.Interfaces.Messaging
{
    public interface IPersonsSender
    {
        Task SendMessage(Person person);
    }
}
