using MassTransit;
using PersonsManager.Domain.Models;
using PersonsManager.Interfaces.Messaging;
using SpaService.Domain.Messages.Person;

namespace PersonsManager.Messaging.Senders
{
    public class ClientUpdatedSender : IPersonsSender
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ClientUpdatedSender(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task SendMessage(Person person)
        {
            ClientUpdated client = new ClientUpdated
            {
                Id = person.Id,
                Name = person.Name,
                Surname = person.Surname
            };

            await _publishEndpoint.Publish(client);
        }
    }
}
