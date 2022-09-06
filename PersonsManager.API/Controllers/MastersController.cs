using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonsManager.Domain.Models;
using PersonsManager.DTO.Master;
using PersonsManager.Interfaces;
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
        public IActionResult Update([FromBody] MasterForUpdateDto master)
        {
            if (master == null)
            {
                return BadRequest("Object sent from user is null");
            }

            var isEntityFound = _mastersService.Update(master);

            if (!isEntityFound)
            {
                return NotFound($"Entity with id: {master.Id} doesn't exist in datebase");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var isEntityFound = _mastersService.Delete(id);

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
        public IActionResult DeleteByUserId(Guid userId)
        {
            var isEntityFound = _mastersService.DeleteByUserId(userId);

            if (!isEntityFound)
            {
                return NotFound($"Entity with userId: {userId} doesn't exist in the database.");
            }

            return NoContent();
        }
    }
}
