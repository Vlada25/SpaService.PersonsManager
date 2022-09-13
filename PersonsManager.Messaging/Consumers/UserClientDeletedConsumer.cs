using MassTransit;
using PersonsManager.Interfaces.Services;
using SpaService.Domain.Messages.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsManager.Messaging.Consumers
{
    public class UserClientDeletedConsumer : IConsumer<UserClientDeleted>
    {
        private readonly IClientsService _clientsService;

        public UserClientDeletedConsumer(IClientsService clientsService)
        {
            _clientsService = clientsService;
        }

        public async Task Consume(ConsumeContext<UserClientDeleted> context)
        {
            var message = context.Message;

            _clientsService.DeleteByUserId(message.Id);
        }
    }
}
