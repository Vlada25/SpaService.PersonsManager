using AutoMapper;
using PersonsManager.Domain.Models;
using PersonsManager.DTO.Client;
using PersonsManager.Interfaces;
using PersonsManager.Interfaces.Services;
using PersonsManager.Messaging.Senders;

namespace PersonsManager.API.Services
{
    public class ClientsService : IClientsService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ClientDeletedSender _clientDeletedSender;
        private readonly ClientUpdatedSender _clientUpdatedSender;

        public ClientsService(IRepositoryManager repositoryManager,
            IMapper mapper,
            ClientDeletedSender clientDeletedSender,
            ClientUpdatedSender clientUpdatedSender)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _clientDeletedSender = clientDeletedSender;
            _clientUpdatedSender = clientUpdatedSender;
        }

        public Client Create(ClientForCreationDto entityForCreation)
        {
            var entity = _mapper.Map<Client>(entityForCreation);

            _repositoryManager.ClientsRepository.Create(entity);
            _repositoryManager.Save();

            return entity;
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = _repositoryManager.ClientsRepository.GetById(id, trackChanges: false);

            if (entity == null)
            {
                return false;
            }

            _repositoryManager.ClientsRepository.Delete(entity);
            _repositoryManager.Save();

            await _clientDeletedSender.SendMessage(entity);

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

        public async Task<bool> Update(ClientForUpdateDto entityForUpdate)
        {
            var entity = _repositoryManager.ClientsRepository.GetById(entityForUpdate.Id, trackChanges: true);

            if (entity == null)
            {
                return false;
            }

            _mapper.Map(entityForUpdate, entity);
            _repositoryManager.Save();

            await _clientUpdatedSender.SendMessage(entity);

            return true;
        }
    }
}
