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
        private readonly ClientChangedSender _clientChangedSender;

        public ClientsService(IRepositoryManager repositoryManager,
            IMapper mapper,
            ClientChangedSender clientDeletedSender)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _clientChangedSender = clientDeletedSender;
        }

        public async Task<Client> Create(ClientForCreationDto entityForCreation)
        {
            var entity = _mapper.Map<Client>(entityForCreation);

            await _repositoryManager.ClientsRepository.Create(entity);
            await _repositoryManager.Save();

            return entity;
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await _repositoryManager.ClientsRepository.GetById(id, trackChanges: false);

            if (entity == null)
            {
                return false;
            }

            _repositoryManager.ClientsRepository.Delete(entity);
            await _repositoryManager.Save();

            await _clientChangedSender.SendDeletedMessage(entity);

            return true;
        }

        public async Task<bool> DeleteByUserId(Guid userId)
        {
            var entity = await _repositoryManager.ClientsRepository.GetByUserId(userId);

            if (entity == null)
            {
                return false;
            }

            _repositoryManager.ClientsRepository.Delete(entity);
            await _repositoryManager.Save();

            await _clientChangedSender.SendDeletedMessage(entity);

            return true;
        }

        public async Task<IEnumerable<Client>> GetAll() =>
            await _repositoryManager.ClientsRepository.GetAll(trackChanges: false);

        public async Task<Client> GetById(Guid id) =>
            await _repositoryManager.ClientsRepository.GetById(id, trackChanges: false);

        public async Task<Client> GetByUserId(Guid userId) =>
            await _repositoryManager.ClientsRepository.GetByUserId(userId);

        public async Task<bool> Update(Guid id, ClientForUpdateDto entityForUpdate)
        {
            var entity = await _repositoryManager.ClientsRepository.GetById(id, trackChanges: true);

            if (entity == null)
            {
                return false;
            }

            _mapper.Map(entityForUpdate, entity);

            _repositoryManager.ClientsRepository.Update(entity);
            await _repositoryManager.Save();

            await _clientChangedSender.SendUpdatedMessage(entity);

            return true;
        }
    }
}
