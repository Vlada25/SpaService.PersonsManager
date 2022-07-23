using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonsManager.Domain.Models;
using PersonsManager.DTO.Master;
using PersonsManager.Interfaces;

namespace PersonsManager.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MastersController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public MastersController(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repository = repositoryManager;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var masters = _repository.MastersRepository.GetAll(trackChanges: false);

            return Ok(masters);
        }

        [HttpGet("{id}", Name = "MasterById")]
        public IActionResult Get(Guid id)
        {
            var master = _repository.MastersRepository.GetById(id, trackChanges: false);

            if (master == null)
            {
                return NotFound($"Entity with id: {id} doesn't exist in datebase");
            }
            else
            {
                return Ok(master);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] MasterForCreationDto master)
        {
            if (master == null)
            {
                return BadRequest("Object sent from client is null");
            }

            var masterEntity = _mapper.Map<Master>(master);

            _repository.MastersRepository.Create(masterEntity);
            _repository.Save();

            return CreatedAtRoute("MasterById", new { id = masterEntity.Id }, masterEntity);
        }

        [HttpPut]
        public IActionResult Update([FromBody] MasterForUpdateDto master)
        {
            if (master == null)
            {
                return BadRequest("Object sent from client is null");
            }

            var masterEntity = _repository.MastersRepository.GetById(master.Id, trackChanges: true);

            if (masterEntity == null)
            {
                return NotFound($"Entity with id: {master.Id} doesn't exist in datebase");
            }

            _mapper.Map(master, masterEntity);
            _repository.Save();

            return Ok("Entity was updated");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var master = _repository.MastersRepository.GetById(id, trackChanges: false);

            if (master == null)
            {
                return NotFound($"Entity with id: {id} doesn't exist in the database.");
            }

            _repository.MastersRepository.Delete(master);
            _repository.Save();

            return Ok("Entity was deleted");
        }
    }
}
