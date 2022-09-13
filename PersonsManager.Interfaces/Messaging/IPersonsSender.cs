using PersonsManager.Domain.Models;

namespace PersonsManager.Interfaces.Messaging
{
    public interface IPersonsSender
    {
        Task SendMessage(Person person);
    }
}
