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
    public class UserMasterDeletedConsumer : IConsumer<UserMasterDeleted>
    {
        private readonly IMastersService _mastersService;

        public UserMasterDeletedConsumer(IMastersService mastersService)
        {
            _mastersService = mastersService;
        }

        public async Task Consume(ConsumeContext<UserMasterDeleted> context)
        {
            var message = context.Message;

            _mastersService.DeleteByUserId(message.Id);
        }
    }
}
