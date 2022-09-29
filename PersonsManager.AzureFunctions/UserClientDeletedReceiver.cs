using System;
using System.Text.Json;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using PersonsManager.Interfaces.Services;
using SpaService.Domain.Messages.User;

namespace PersonsManager.AzureFunctions
{
    public class UserClientDeletedReceiver
    {
        private readonly IClientsService _clientsService;

        public UserClientDeletedReceiver(IClientsService clientsService)
        {
            _clientsService = clientsService;
        }

        [FunctionName("UserClientDeletedReceiver")]
        public void Run([ServiceBusTrigger("user-deleted", "client", Connection = "ConnectionString")]string mySbMsg)
        {
            UserClientDeleted user = JsonSerializer.Deserialize<UserClientDeleted>(mySbMsg);

            _clientsService.DeleteByUserId(user.Id);
        }
    }
}
