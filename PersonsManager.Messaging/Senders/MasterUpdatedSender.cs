using MassTransit;
using PersonsManager.Domain.Models;
using PersonsManager.Interfaces.Messaging;
using SpaService.Domain.Messages.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsManager.Messaging.Senders
{
    public class MasterUpdatedSender : IPersonsSender
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MasterUpdatedSender(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task SendMessage(Person person)
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
