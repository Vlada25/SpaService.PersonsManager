using Microsoft.AspNetCore.Mvc;
using Moq;
using PersonsManager.API.Controllers;
using PersonsManager.Domain.Models;
using PersonsManager.DTO.Master;
using PersonsManager.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PersonsManager.Tests.Masters
{
    public class MastersControllerTests
    {
        private readonly Mock<IMastersService> _masterServiceMock;

        public MastersControllerTests()
        {
            _masterServiceMock = new Mock<IMastersService>();
        }

        private List<Master> _testMasters = new List<Master>
        {
            new Master
            {
                Id = new Guid("3e77e8aa-b920-4bca-adb6-4a5e2de117f2"),
                Surname = "Leonenko",
                Name = "Vladislava",
                MiddleName = "Jurievna"
            },
            new Master
            {
                Id = new Guid("7e1ca877-fb7a-46d0-b3d6-e0e102d5b6ad"),
                Surname = "Holosova",
                Name = "Anna",
                MiddleName = "Denisovna"
            },
            new Master
            {
                Id = new Guid("e3d934ae-4002-41b7-8dcf-99709425f227"),
                Surname = "Ivanov",
                Name = "Ivan",
                MiddleName = "Ivanovich"
            },
        };


        [Fact]
        public async Task GetAll_MastersExist_ReturnsListOfMastersAndStatusCode200()
        {
            // Arrange
            _masterServiceMock.Setup(s => s.GetAll()).ReturnsAsync(_testMasters);
            var controller = new MastersController(_masterServiceMock.Object);

            // Act
            var response = await controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(response);

            var result = response as OkObjectResult;

            Assert.NotNull(result.Value);

            var models = result.Value as IEnumerable<Master>;

            Assert.Equal(_testMasters, models);
        }

        [Fact]
        public async Task GetAll_MastersDontExist_ReturnsEmptyAndStatusCode200()
        {
            // Arrange
            _masterServiceMock.Setup(s => s.GetAll()).ReturnsAsync(new List<Master>());
            var controller = new MastersController(_masterServiceMock.Object);

            // Act
            var response = await controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(response);

            var result = response as OkObjectResult;

            Assert.NotNull(result.Value);

            var models = result.Value as IEnumerable<Master>;

            Assert.Empty(models);
        }

        [Fact]
        public async Task GetById_MasterExists_ReturnsMasterAndStatusCode200()
        {
            // Arrange
            Master master = _testMasters[0];

            _masterServiceMock.Setup(s => s.GetById(master.Id)).ReturnsAsync(master);
            var controller = new MastersController(_masterServiceMock.Object);

            // Act
            var response = await controller.Get(master.Id);

            // Assert
            Assert.IsType<OkObjectResult>(response);

            var result = response as OkObjectResult;

            Assert.NotNull(result.Value);

            var model = result.Value as Master;

            Assert.Equal(master, model);
        }

        [Fact]
        public async Task GetById_MasterDoesntExist_CatchsException()
        {
            // Arrange
            Guid masterId = Guid.NewGuid();
            var message = $"Entity with id: {masterId} doesn't exist in database";

            _masterServiceMock.Setup(s => s.GetById(masterId))
                .ReturnsAsync((Master)null);
            var controller = new MastersController(_masterServiceMock.Object);

            // Act
            Func<Task> response = () => controller.Get(masterId);

            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(response);

            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public async Task Create_MasterDoesntExist_ReturnsMasterAndStatusCode201()
        {
            // Arrange
            MasterForCreationDto masterForCreation = new MasterForCreationDto
            {
                Name = "Danila",
                Surname = "Kiselev"
            };

            _masterServiceMock.Setup(s => s.Create(masterForCreation))
                .ReturnsAsync(new Master { Name = "Danila", Surname = "Kiselev" });
            var controller = new MastersController(_masterServiceMock.Object);

            // Act

            var response = await controller.Create(masterForCreation);


            // Assert

            Assert.IsType<CreatedAtRouteResult>(response);

            var result = response as CreatedAtRouteResult;

            Assert.NotNull(result.Value);

            var model = result.Value as Master;

            Assert.Equal(model.Name, masterForCreation.Name);
            Assert.Equal(model.Surname, masterForCreation.Surname);
        }

        [Fact]
        public async Task Update_MasterExists_ReturnsStatusCode204()
        {
            // Arrange
            Guid masterId = Guid.NewGuid();

            MasterForUpdateDto masterForUpdate = new MasterForUpdateDto
            {
                Name = "Danila",
                Surname = "Kiselev"
            };

            _masterServiceMock.Setup(s => s.Update(masterId, masterForUpdate))
                .ReturnsAsync(true);
            var controller = new MastersController(_masterServiceMock.Object);

            // Act
            var response = await controller.Update(masterId, masterForUpdate);

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async Task Update_MasterExists_CatchsException()
        {
            // Arrange
            Guid masterId = Guid.NewGuid();
            var message = $"Entity with id: {masterId} doesn't exist in database";

            MasterForUpdateDto masterForUpdate = new MasterForUpdateDto
            {
                Name = "Danila",
                Surname = "Kiselev"
            };

            _masterServiceMock.Setup(s => s.Update(masterId, null))
                .ReturnsAsync(false);
            var controller = new MastersController(_masterServiceMock.Object);

            // Act
            Func<Task> response = () => controller.Update(masterId, masterForUpdate);

            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(response);

            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public async Task Delete_MasterExists_ReturnsStatusCode204()
        {
            // Arrange
            Guid masterId = Guid.NewGuid();

            _masterServiceMock.Setup(s => s.Delete(masterId))
                .ReturnsAsync(true);
            var controller = new MastersController(_masterServiceMock.Object);

            // Act
            var response = await controller.Delete(masterId);

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async Task Delete_MasterExists_CatchsException()
        {
            // Arrange
            Guid masterId = Guid.NewGuid();
            var message = $"Entity with id: {masterId} doesn't exist in database";

            _masterServiceMock.Setup(s => s.Delete(masterId))
                .ReturnsAsync(false);
            var controller = new MastersController(_masterServiceMock.Object);

            // Act
            Func<Task> response = () => controller.Delete(masterId);

            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(response);

            Assert.Equal(message, exception.Message);
        }
    }
}
