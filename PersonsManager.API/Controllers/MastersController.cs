using Microsoft.AspNetCore.Mvc;
using PersonsManager.DTO.Master;
using PersonsManager.Interfaces.Services;

namespace PersonsManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MastersController : ControllerBase
    {
        private readonly IMastersService _mastersService;

        public MastersController(IMastersService mastersService)
        {
            _mastersService = mastersService;
        }

        #region CRUD

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var masters = await _mastersService.GetAll();

            return Ok(masters);
        }

        [HttpGet("{id}", Name = "MasterById")]
        public async Task<IActionResult> Get(Guid id)
        {
            var master = await _mastersService.GetById(id);

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
        public async Task<IActionResult> Create([FromBody] MasterForCreationDto master)
        {
            if (master == null)
            {
                return BadRequest("Object sent from user is null");
            }

            var masterEntity = await _mastersService.Create(master);

            return CreatedAtRoute("MasterById", new { id = masterEntity.Id }, masterEntity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MasterForUpdateDto master)
        {
            if (master == null)
            {
                return BadRequest("Object sent from user is null");
            }

            var isEntityFound = await _mastersService.Update(id, master);

            if (!isEntityFound)
            {
                return NotFound($"Entity with id: {id} doesn't exist in datebase");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isEntityFound = await _mastersService.Delete(id);

            if (!isEntityFound)
            {
                return NotFound($"Entity with id: {id} doesn't exist in the database.");
            }

            return NoContent();
        }

        #endregion
    }
}
