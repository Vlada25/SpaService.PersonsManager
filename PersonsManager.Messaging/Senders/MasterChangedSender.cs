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
    public class MasterChangedSender
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MasterChangedSender(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task SendDeletedMessage(Person person)
        {
            MasterDeleted master = new MasterDeleted
            {
                Id = person.Id
            };

            await _publishEndpoint.Publish(master);
        }

        public async Task SendUpdatedMessage(Person person)
        {
            MasterUpdated master = new MasterUpdated
            {
                Id = person.Id,
                Name = person.Name,
                Surname = person.Surname
            };

            await _publishEndpoint.Publish(master);
        }
    }
}
