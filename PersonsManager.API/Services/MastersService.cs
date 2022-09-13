using AutoMapper;
using PersonsManager.Domain.Models;
using PersonsManager.DTO.Master;
using PersonsManager.Interfaces;
using PersonsManager.Interfaces.Services;
using PersonsManager.Messaging.Senders;

namespace PersonsManager.API.Services
{
    public class MastersService : IMastersService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly MasterDeletedSender _masterDeletedSender;
        private readonly MasterUpdatedSender _masterUpdatedSender;

        public MastersService(IRepositoryManager repositoryManager,
            IMapper mapper,
            MasterDeletedSender masterDeletedSender,
            MasterUpdatedSender masterUpdatedSender)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _masterDeletedSender = masterDeletedSender;
            _masterUpdatedSender = masterUpdatedSender;
        }

        public Master Create(MasterForCreationDto entityForCreation)
        {
            var entity = _mapper.Map<Master>(entityForCreation);

            _repositoryManager.MastersRepository.Create(entity);
            _repositoryManager.Save();

            return entity;
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = _repositoryManager.MastersRepository.GetById(id, trackChanges: false);

            if (entity == null)
            {
                return false;
            }

            _repositoryManager.MastersRepository.Delete(entity);
            _repositoryManager.Save();

            await _masterDeletedSender.SendMessage(entity);

            return true;
        }

        public bool DeleteByUserId(Guid userId)
        {
            var entity = _repositoryManager.MastersRepository.GetByUserId(userId);

            if (entity == null)
            {
                return false;
            }

            _repositoryManager.MastersRepository.Delete(entity);
            _repositoryManager.Save();

            return true;
        }

        public IEnumerable<Master> GetAll() =>
            _repositoryManager.MastersRepository.GetAll(trackChanges: false);

        public Master GetById(Guid id) =>
            _repositoryManager.MastersRepository.GetById(id, trackChanges: false);

        public Master GetByUserId(Guid userId) =>
            _repositoryManager.MastersRepository.GetByUserId(userId);

        public async Task<bool> Update(MasterForUpdateDto entityForUpdate)
        {
            var entity = _repositoryManager.MastersRepository.GetById(entityForUpdate.Id, trackChanges: true);

            if (entity == null)
            {
                return false;
            }

            _mapper.Map(entityForUpdate, entity);
            _repositoryManager.Save();

            await _masterUpdatedSender.SendMessage(entity);

            return true;
        }
    }
}
