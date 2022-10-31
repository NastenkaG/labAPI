using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using labAPI.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Collections.Specialized.BitVector32;

namespace labAPI.Controllers
{
    [Route("api/gardens")]
    [ApiController]
    public class GardensController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public GardensController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet("{id}", Name = "GardenById")]
        public IActionResult GetGardens(Guid id)
        {
            var garden = _repository.Garden.GetGarden(id, trackChanges:false);
            if (garden == null)
            {
                _logger.LogInfo($"Garden with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var gardenDto = _mapper.Map<GardenDto>(garden);
                return Ok(gardenDto);
            }
        }
        [HttpPost("collection")]
        public IActionResult CreateGardenCollection([FromBody] IEnumerable<GardenForCreationDto> gardenCollection)
        {
            if (gardenCollection == null)
            {
                _logger.LogError("Garden collection sent from client is null.");
                return BadRequest("Garden collection is null");
            }
            var gardenPlant = _mapper.Map<IEnumerable<Garden>>(gardenCollection);
            foreach (var garden in gardenPlant)
            {
                _repository.Garden.CreateGarden(garden);
            }
            _repository.Save();
            var gardenCollectionToReturn = _mapper.Map<IEnumerable<GardenDto>>(gardenPlant);
            var ids = string.Join(",", gardenCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("GardenCollection", new { ids }, gardenCollectionToReturn);
        }

        [HttpGet("collection/({ids})", Name = "GardenCollection")]
        public IActionResult GetGardenCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var gardenPlant = _repository.Garden.GetByIds(ids, trackChanges: false);
            if (ids.Count() != gardenPlant.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var gardensToReturn = _mapper.Map<IEnumerable<GardenDto>>(gardenPlant);
            return Ok(gardensToReturn);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteGarden(Guid id)
        {
            var garden = _repository.Garden.GetGarden(id, trackChanges: false);
            if (garden == null)
            {
                _logger.LogInfo($"Garden with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Garden.DeleteGarden(garden);
            _repository.Save();
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateGarden(Guid id, [FromBody] GardenForUpdateDto garden)
        {
            if (garden == null)
            {
                _logger.LogError("GardenForUpdateDto object sent from client is null.");
                return BadRequest("GardenForUpdateDto object is null");
            }
            var gardenEntity = _repository.Garden.GetGarden(id, trackChanges: true);
            if (gardenEntity == null)
            {
                _logger.LogInfo($"Garden with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(garden, gardenEntity);
            _repository.Save();
            return NoContent();
        }
    }
}

