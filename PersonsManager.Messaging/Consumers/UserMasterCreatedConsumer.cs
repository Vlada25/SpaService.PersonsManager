using MassTransit;
using PersonsManager.DTO.Master;
using PersonsManager.Interfaces.Services;
using SpaService.Domain.Messages.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsManager.Messaging.Consumers
{
    public class UserMasterCreatedConsumer : IConsumer<UserMasterCreated>
    {
        private readonly IMastersService _mastersService;

        public UserMasterCreatedConsumer(IMastersService mastersService)
        {
            _mastersService = mastersService;
        }

        public async Task Consume(ConsumeContext<UserMasterCreated> context)
        {
            var message = context.Message;

            await _mastersService.Create(new MasterForCreationDto
            {
                Surname = message.Surname,
                Name = message.Name,
                MiddleName = message.MiddleName,
                PhoneNumber = "",
                AddressId = message.AddressId,
                UserId = message.Id
            });
        }
    }
}
