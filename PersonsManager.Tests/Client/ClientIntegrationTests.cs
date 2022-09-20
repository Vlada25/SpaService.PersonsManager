using AutoMapper;
using Moq;
using PersonsManager.Domain;
using PersonsManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PersonsManager.Tests.Client
{
    public class ClientIntegrationTests
    {
        private static IMapper _mapper;

        public ClientIntegrationTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public void GetAll_SendRequest_ReturnEmptyList()
        {
            // Arrange
            var repositoryManager = new Mock<IRepositoryManager>();

            repositoryManager.Setup(r => r.ClientsRepository.GetAll(false))
                .Returns(new IEnumerable<Client>());

            var controller = new FridgeModelsController(repositoryManager.Object, null, null);

            // Act
            var response = controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(response);

            var result = response as OkObjectResult;

            Assert.NotNull(result.Value);

            var models = result.Value as IEnumerable<FridgeModel>;
            models.Should().HaveCount(0);
        }
    }
}
