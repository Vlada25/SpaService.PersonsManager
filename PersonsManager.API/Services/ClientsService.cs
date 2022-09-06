using AutoMapper;
using PersonsManager.Domain.Models;
using PersonsManager.DTO.Client;
using PersonsManager.Interfaces;
using PersonsManager.Interfaces.Services;

namespace PersonsManager.API.Services
{
    public class ClientsService : IClientsService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ClientsService(IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public Client Create(ClientForCreationDto entityForCreation)
        {
            var entity = _mapper.Map<Client>(entityForCreation);

            _repositoryManager.ClientsRepository.Create(entity);
            _repositoryManager.Save();

            return entity;
        }

        public bool Delete(Guid id)
        {
            var entity = _repositoryManager.ClientsRepository.GetById(id, trackChanges: false);

            if (entity == null)
            {
                return false;
            }

            _repositoryManager.ClientsRepository.Delete(entity);
            _repositoryManager.Save();

            return true;
        }

        public bool DeleteByUserId(Guid userId)
        {
            var entity = _repositoryManager.ClientsRepository.GetByUserId(userId);

            if (entity == null)
            {
                return false;
            }

            _repositoryManager.ClientsRepository.Delete(entity);
            _repositoryManager.Save();

            return true;
        }

        public IEnumerable<Client> GetAll() =>
            _repositoryManager.ClientsRepository.GetAll(trackChanges: false);

        public Client GetById(Guid id) =>
            _repositoryManager.ClientsRepository.GetById(id, trackChanges: false);

        public Client GetByUserId(Guid userId) =>
            _repositoryManager.ClientsRepository.GetByUserId(userId);

        public bool Update(ClientForUpdateDto entityForUpdate)
        {
            var entity = _repositoryManager.ClientsRepository.GetById(entityForUpdate.Id, trackChanges: true);

            if (entity == null)
            {
                return false;
            }

            _mapper.Map(entityForUpdate, entity);
            _repositoryManager.Save();

            return true;
        }
    }
}
