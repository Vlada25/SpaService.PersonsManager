using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonsManager.Domain.Models;
using PersonsManager.DTO.Client;
using PersonsManager.Interfaces;
using PersonsManager.Interfaces.Services;

namespace PersonsManager.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService _clientsService;

        public ClientsController(IClientsService clientsService)
        {
            _clientsService = clientsService;
        }

        #region CRUD

        [HttpGet]
        public IActionResult GetAll()
        {
            var clients = _clientsService.GetAll();

            return Ok(clients);
        }

        [HttpGet("{id}", Name = "ClientById")]
        public IActionResult Get(Guid id)
        {
            var client = _clientsService.GetById(id);

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

            var clientEntity = _clientsService.Create(client);

            return CreatedAtRoute("ClientById", new { id = clientEntity.Id }, clientEntity);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ClientForUpdateDto client)
        {
            if (client == null)
            {
                return BadRequest("Object sent from client is null");
            }

            var isEntityFound = _clientsService.Update(client);

            if (!isEntityFound)
            {
                return NotFound($"Entity with id: {client.Id} doesn't exist in datebase");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var isEntityFound = _clientsService.Delete(id);

            if (!isEntityFound)
            {
                return NotFound($"Entity with id: {id} doesn't exist in the database.");
            }

            return NoContent();
        }

        #endregion

        [HttpGet("{userId}")]
        public IActionResult GetByUserId(Guid userId)
        {
            var client = _clientsService.GetByUserId(userId);

            if (client == null)
            {
                return NotFound($"Entity with userId: {userId} doesn't exist in datebase");
            }
            else
            {
                return Ok(client);
            }
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteByUserId(Guid userId)
        {
            var isEntityFound = _clientsService.DeleteByUserId(userId);

            if (!isEntityFound)
            {
                return NotFound($"Entity with userId: {userId} doesn't exist in the database.");
            }

            return NoContent();
        }
    }
}
