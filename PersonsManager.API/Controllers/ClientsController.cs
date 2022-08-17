using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonsManager.Domain.Models;
using PersonsManager.DTO.Client;
using PersonsManager.Interfaces;

namespace PersonsManager.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public ClientsController(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repository = repositoryManager;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var clients = _repository.ClientsRepository.GetAll(trackChanges: false);

            return Ok(clients);
        }

        [HttpGet("{id}", Name = "ClientById")]
        public IActionResult Get(Guid id)
        {
            var client = _repository.ClientsRepository.GetById(id, trackChanges: false);

            if (client == null)
            {
                return NotFound($"Entity with id: {id} doesn't exist in datebase");
            }
            else
            {
                return Ok(client);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] ClientForCreationDto client)
        {
            if (client == null)
            {
                return BadRequest("Object sent from client is null");
            }

            var clientEntity = _mapper.Map<Client>(client);

            _repository.ClientsRepository.Create(clientEntity);
            _repository.Save();

            return CreatedAtRoute("ClientById", new { id = clientEntity.Id }, clientEntity);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ClientForUpdateDto client)
        {
            if (client == null)
            {
                return BadRequest("Object sent from client is null");
            }

            var clientEntity = _repository.ClientsRepository.GetById(client.Id, trackChanges: true);

            if (clientEntity == null)
            {
                return NotFound($"Entity with id: {client.Id} doesn't exist in datebase");
            }

            _mapper.Map(client, clientEntity);
            _repository.Save();

            return Ok("Entity was updated");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var client = _repository.ClientsRepository.GetById(id, trackChanges: false);

            if (client == null)
            {
                return NotFound($"Entity with id: {id} doesn't exist in the database.");
            }

            _repository.ClientsRepository.Delete(client);
            _repository.Save();

            return Ok("Entity was deleted");
        }
    }
}
