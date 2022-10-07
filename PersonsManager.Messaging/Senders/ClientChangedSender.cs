using MassTransit;
using PersonsManager.Domain.Models;
using SpaService.Domain.Messages.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsManager.Messaging.Senders
{
    public class ClientChangedSender
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ClientChangedSender(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task SendDeletedMessage(Person person)
        {
            ClientDeleted client = new ClientDeleted
            {
                Id = person.Id
            };

            await _publishEndpoint.Publish(client);
        }

        public async Task SendUpdatedMessage(Person person)
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
