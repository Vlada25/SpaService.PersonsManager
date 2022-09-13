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
    public class MasterDeletedSender : IPersonsSender
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MasterDeletedSender(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task SendMessage(Person person)
        {
            MasterDeleted master = new MasterDeleted
            {
                Id = person.Id
            };

            await _publishEndpoint.Publish(master);
        }
    }
}
