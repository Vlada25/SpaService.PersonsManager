using Microsoft.AspNetCore.Mvc;
using PersonsManager.DTO.Master;
using PersonsManager.Interfaces.Services;

namespace PersonsManager.API.Controllers
{
    [Route("api/[controller]/[action]")]
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
        public IActionResult GetAll()
        {
            var masters = _mastersService.GetAll();

            return Ok(masters);
        }

        [HttpGet("{id}", Name = "MasterById")]
        public IActionResult Get(Guid id)
        {
            var master = _mastersService.GetById(id);

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
                return BadRequest("Object sent from user is null");
            }

            var masterEntity = _mastersService.Create(master);

            return CreatedAtRoute("MasterById", new { id = masterEntity.Id }, masterEntity);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] MasterForUpdateDto master)
        {
            if (master == null)
            {
                return BadRequest("Object sent from user is null");
            }

            var isEntityFound = await _mastersService.Update(master);

            if (!isEntityFound)
            {
                return NotFound($"Entity with id: {master.Id} doesn't exist in datebase");
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


        [HttpGet("{userId}")]
        public IActionResult GetByUserId(Guid userId)
        {
            var master = _mastersService.GetByUserId(userId);

            if (master == null)
            {
                return NotFound($"Entity with userId: {userId} doesn't exist in datebase");
            }
            else
            {
                return Ok(master);
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteByUserId(Guid userId)
        {
            var isEntityFound = await _mastersService.DeleteByUserId(userId);

            if (!isEntityFound)
            {
                return NotFound($"Entity with userId: {userId} doesn't exist in the database.");
            }

            return NoContent();
        }
    }
}
