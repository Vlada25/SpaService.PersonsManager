using MassTransit;
using PersonsManager.Domain.Models;
using PersonsManager.Interfaces.Messaging;
using SpaService.Domain.Messages.Person;

namespace PersonsManager.Messaging.Senders
{
    public class ClientDeletedSender : IPersonsSender
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ClientDeletedSender(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task SendMessage(Person person)
        {
            ClientDeleted client = new ClientDeleted
            {
                Id = person.Id
            };

            await _publishEndpoint.Publish(client);
        }
    }
}
