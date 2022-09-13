using MassTransit;
using PersonsManager.DTO.Client;
using PersonsManager.Interfaces.Services;
using SpaService.Domain.Messages.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsManager.Messaging.Consumers
{
    public class UserClientCreatedConsumer : IConsumer<UserClientCreated>
    {
        private readonly IClientsService _clientsService;

        public UserClientCreatedConsumer(IClientsService clientsService)
        {
            _clientsService = clientsService;
        }

        public async Task Consume(ConsumeContext<UserClientCreated> context)
        {
            var message = context.Message;

            _clientsService.Create(new ClientForCreationDto
            {
                Surname = message.Surname,
                Name = message.Name,
                MiddleName = message.MiddleName,
                PhoneNumber = "",
                UserId = message.Id
            });
        }
    }
}
