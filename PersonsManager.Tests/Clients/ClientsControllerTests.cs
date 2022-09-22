using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PersonsManager.API.Controllers;
using PersonsManager.Domain.Models;
using PersonsManager.DTO.Client;
using PersonsManager.Interfaces.Services;
using Xunit;

namespace PersonsManager.Tests.Clients
{
    public class ClientsControllerTests
    {
        private readonly Mock<IClientsService> _clientServiceMock;

        public ClientsControllerTests()
        {
            _clientServiceMock = new Mock<IClientsService>();
        }

        private List<Client> _testClients = new List<Client>
        {
            new Client
            {
                Id = new Guid("3e77e8aa-b920-4bca-adb6-4a5e2de117f2"),
                Surname = "Leonenko",
                Name = "Vladislava",
                MiddleName = "Jurievna"
            },
            new Client
            {
                Id = new Guid("7e1ca877-fb7a-46d0-b3d6-e0e102d5b6ad"),
                Surname = "Holosova",
                Name = "Anna",
                MiddleName = "Denisovna"
            },
            new Client
            {
                Id = new Guid("e3d934ae-4002-41b7-8dcf-99709425f227"),
                Surname = "Ivanov",
                Name = "Ivan",
                MiddleName = "Ivanovich"
            },
        };


        [Fact]
        public async Task GetAll_ClientsExist_ReturnsListOfClientsAndStatusCode200()
        {
            // Arrange
            _clientServiceMock.Setup(s => s.GetAll()).ReturnsAsync(_testClients);
            var controller = new ClientsController(_clientServiceMock.Object);

            // Act
            var response = await controller.GetAll();

            // Assert
            response.Should().BeOfType<OkObjectResult>();

            var result = response as OkObjectResult;

            result.Value.Should().NotBeNull();

            var models = result.Value as IEnumerable<Client>;

            models.Should().HaveSameCount(_testClients);
            models.Should().BeEquivalentTo(_testClients);
        }

        [Fact]
        public async Task GetAll_ClientsDontExist_ReturnsEmptyAndStatusCode200()
        {
            // Arrange
            _clientServiceMock.Setup(s => s.GetAll()).ReturnsAsync(new List<Client>());
            var controller = new ClientsController(_clientServiceMock.Object);

            // Act
            var response = await controller.GetAll();

            // Assert
            response.Should().BeOfType<OkObjectResult>();

            var result = response as OkObjectResult;

            result.Value.Should().NotBeNull();

            var models = result.Value as IEnumerable<Client>;

            models.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetById_ClientExists_ReturnsClientAndStatusCode200()
        {
            // Arrange
            Client client = _testClients[0];

            _clientServiceMock.Setup(s => s.GetById(client.Id)).ReturnsAsync(client);
            var controller = new ClientsController(_clientServiceMock.Object);

            // Act
            var response = await controller.Get(client.Id);

            // Assert
            response.Should().BeOfType<OkObjectResult>();

            var result = response as OkObjectResult;

            result.Value.Should().NotBeNull();

            var model = result.Value as Client;

            model.Should().BeEquivalentTo(client);
        }

        [Fact]
        public async Task GetById_ClientDoesntExist_ReturnsStatusCode404()
        {
            // Arrange
            Guid clientId = Guid.NewGuid();
            var message = $"Entity with id: {clientId} doesn't exist in datebase";

            _clientServiceMock.Setup(s => s.GetById(clientId))
                .ReturnsAsync((Client)null);
            var controller = new ClientsController(_clientServiceMock.Object);

            // Act
            var response = await controller.Get(clientId);

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>();

            var result = response as NotFoundObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(message);
        }

        [Fact]
        public async Task Create_ClientDoesntExist_ReturnsClientAndStatusCode201()
        {
            // Arrange
            ClientForCreationDto clientForCreation = new ClientForCreationDto
            {
                Name = "Danila",
                Surname = "Kiselev"
            };

            _clientServiceMock.Setup(s => s.Create(clientForCreation))
                .ReturnsAsync(new Client { Name = "Danila", Surname = "Kiselev"});
            var controller = new ClientsController(_clientServiceMock.Object);

            // Act

            var response = await controller.Create(clientForCreation);


            // Assert

            response.Should().BeOfType<CreatedAtRouteResult>();

            var result = response as CreatedAtRouteResult;

            result.Value.Should().NotBeNull();

            var model = result.Value as Client;

            model.Should().BeEquivalentTo(clientForCreation);
        }

        [Fact]
        public async Task Update_ClientExists_ReturnsStatusCode204()
        {
            // Arrange
            Guid clientId = Guid.NewGuid();

            ClientForUpdateDto clientForUpdate = new ClientForUpdateDto
            {
                Name = "Danila",
                Surname = "Kiselev"
            };

            _clientServiceMock.Setup(s => s.Update(clientId, clientForUpdate))
                .ReturnsAsync(true);
            var controller = new ClientsController(_clientServiceMock.Object);

            // Act
            var response = await controller.Update(clientId, clientForUpdate);

            // Assert
            response.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Delete_ClientExists_ReturnsStatusCode204()
        {
            // Arrange
            Guid clientId = Guid.NewGuid();

            _clientServiceMock.Setup(s => s.Delete(clientId))
                .ReturnsAsync(true);
            var controller = new ClientsController(_clientServiceMock.Object);

            // Act
            var response = await controller.Delete(clientId);

            // Assert
            response.Should().BeOfType<NoContentResult>();
        }
    }
}
