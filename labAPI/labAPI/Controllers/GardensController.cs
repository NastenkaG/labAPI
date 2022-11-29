using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using labAPI.ActionFilters;
using labAPI.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [HttpGet]
        public async Task<IActionResult> GetGardens()
        {
            var garden = await _repository.Garden.GetAllGardensAsync(trackChanges: false);
            var gardenDto = _mapper.Map<IEnumerable<GardenDto>>(garden);
            return Ok(gardenDto);
        }

        [HttpGet("{id}", Name = "GardenById")]
        public async Task<IActionResult> GetGarden(Guid id)
        {
            var garden = await _repository.Garden.GetGardenAsync(id, trackChanges: false);
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

        [HttpGet("collection/({ids})", Name = "GardenCollection")]
        public async Task<IActionResult> GetGardenCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var gardenEntities = await _repository.Garden.GetByIdsAsync(ids,trackChanges: false);
            if (ids.Count() != gardenEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var gardenToReturn = _mapper.Map<IEnumerable<GardenDto>>(gardenEntities);
            return Ok(gardenToReturn);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateGarden([FromBody] GardenForCreationDto garden)
        {
            var gardenEntity = _mapper.Map<Garden>(garden);
            _repository.Garden.CreateGarden(gardenEntity);
            await _repository.SaveAsync();
            var gardenToReturn = _mapper.Map<GardenDto>(gardenEntity);
            return CreatedAtRoute("GardenById", new { id = gardenToReturn.Id }, gardenToReturn);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateCompanyExistsAttribute))]
        public async Task<IActionResult> UpdateGarden(Guid id, [FromBody] GardenForUpdateDto garden)
        {
            var gardenEntity = HttpContext.Items["garden"] as Garden;
            _mapper.Map(garden, gardenEntity);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateGardenCollection([FromBody] IEnumerable<GardenForCreationDto> gardenCollection)
        {
            if (gardenCollection == null)
            {
                _logger.LogError("Garden collection sent from client is null.");
                return BadRequest("Garden collection is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the GardenForCreationDto object");
                return UnprocessableEntity(ModelState);
            }
            var gardenPlant = _mapper.Map<IEnumerable<Garden>>(gardenCollection);
            foreach (var garden in gardenPlant)
            {
                _repository.Garden.CreateGarden(garden);
            }
            await _repository.SaveAsync();
            var gardenCollectionToReturn = _mapper.Map<IEnumerable<GardenDto>>(gardenPlant);
            var ids = string.Join(",", gardenCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("GardenCollection", new { ids }, gardenCollectionToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateCompanyExistsAttribute))]
        public async Task<IActionResult> DeleteGarden(Guid id)
        {
            var garden = HttpContext.Items["garden"] as Garden;
            _repository.Garden.DeleteGarden(garden);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}

