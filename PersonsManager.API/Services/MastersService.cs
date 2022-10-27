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
        private readonly MasterChangedSender _masterChangedSender;

        public MastersService(IRepositoryManager repositoryManager,
            IMapper mapper,
            MasterChangedSender masterDeletedSender)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _masterChangedSender = masterDeletedSender;
        }

        public async Task<Master> Create(MasterForCreationDto entityForCreation)
        {
            var entity = _mapper.Map<Master>(entityForCreation);

            await _repositoryManager.MastersRepository.Create(entity);
            await _repositoryManager.Save();

            return entity;
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await _repositoryManager.MastersRepository.GetById(id, trackChanges: false);

            if (entity == null)
            {
                return false;
            }

            _repositoryManager.MastersRepository.Delete(entity);
            await _repositoryManager.Save();

            //await _masterChangedSender.SendDeletedMessage(entity);

            return true;
        }

        public async Task<bool> DeleteByUserId(Guid userId)
        {
            var entity = await _repositoryManager.MastersRepository.GetByUserId(userId);

            if (entity == null)
            {
                return false;
            }

            _repositoryManager.MastersRepository.Delete(entity);
            await _repositoryManager.Save();

            await _masterChangedSender.SendDeletedMessage(entity);

            return true;
        }

        public async Task<IEnumerable<Master>> GetAll() =>
            await _repositoryManager.MastersRepository.GetAll(trackChanges: false);

        public async Task<IEnumerable<Master>> GetByAddressId(Guid addressId) =>
            await _repositoryManager.MastersRepository.GetByAddressId(addressId, trackChanges: false);

        public async Task<Master> GetById(Guid id) =>
            await _repositoryManager.MastersRepository.GetById(id, trackChanges: false);

        public async Task<Master> GetByUserId(Guid userId) =>
            await _repositoryManager.MastersRepository.GetByUserId(userId);

        public async Task<bool> Update(Guid id, MasterForUpdateDto entityForUpdate)
        {
            var entity = await _repositoryManager.MastersRepository.GetById(id, trackChanges: true);

            if (entity == null)
            {
                return false;
            }

            _mapper.Map(entityForUpdate, entity);

            _repositoryManager.MastersRepository.Update(entity);
            await _repositoryManager.Save();

            await _masterChangedSender.SendUpdatedMessage(entity);

            return true;
        }
    }
}
