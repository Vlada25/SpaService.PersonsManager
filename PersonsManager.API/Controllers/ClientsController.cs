using Microsoft.AspNetCore.Mvc;
using PersonsManager.DTO.Client;
using PersonsManager.Interfaces.Services;

namespace PersonsManager.API.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> GetAll()
        {
            var clients = await _clientsService.GetAll();

            return Ok(clients);
        }

        [HttpGet("{id}", Name = "ClientById")]
        public async Task<IActionResult> Get(Guid id)
        {
            var client = await _clientsService.GetById(id);

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
        public async Task<IActionResult> Create([FromBody] ClientForCreationDto client)
        {
            if (client == null)
            {
                return BadRequest("Object sent from client is null");
            }

            var clientEntity = await _clientsService.Create(client);

            return CreatedAtRoute("ClientById", new { id = clientEntity.Id }, clientEntity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ClientForUpdateDto client)
        {
            if (client == null)
            {
                return BadRequest("Object sent from client is null");
            }

            var isEntityFound = await _clientsService.Update(id, client);

            if (!isEntityFound)
            {
                return NotFound($"Entity with id: {id} doesn't exist in datebase");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isEntityFound = await _clientsService.Delete(id);

            if (!isEntityFound)
            {
                return NotFound($"Entity with id: {id} doesn't exist in the database.");
            }

            return NoContent();
        }

        #endregion

        [HttpGet("Users/{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var client = await _clientsService.GetByUserId(userId);

            if (client == null)
            {
                return NotFound($"Entity with userId: {userId} doesn't exist in datebase");
            }

            return Ok(client);
        }
    }
}
