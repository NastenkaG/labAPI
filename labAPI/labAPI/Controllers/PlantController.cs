using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace labAPI.Controllers
{
    [Route("api/garden/{gardenId}/plant")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public PlantController(IRepositoryManager repository, ILoggerManager logger,
        IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlants()
        {
            var plant = await _repository.Plant.GetAllPlantsAsync(trackChanges:false);
            var plantDto = _mapper.Map<IEnumerable<PlantDto>>(plant);
            return Ok(plantDto);
        }

        [HttpGet("{id}", Name = "GetPlantForCompany")]
        public async Task<IActionResult> GetPlantForGarden(Guid gardenId, Guid id)
        {
            var garden = await _repository.Garden.GetGardenAsync(gardenId, trackChanges: false);
            if (garden == null)
            {
                _logger.LogInfo($"Garden with id: {gardenId} doesn't exist in the database.");
            return NotFound();
            }
            var plantDb = await _repository.Plant.GetPlantAsync(gardenId, id, trackChanges:false);
            if (plantDb == null)
            {
                _logger.LogInfo($"Plant with id: {id} doesn't exist in the database.");
            return NotFound();
            }
            var plant = _mapper.Map<PlantDto>(plantDb);
            return Ok(plant);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlantForGarden(Guid gardenId, [FromBody] PlantForCreationDto plant)
        {
            if (plant == null)
            {
                _logger.LogError("PlantForCreationDto object sent from client isnull.");
                return BadRequest("PlantForCreationDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the PlantForCreationDto object");
                return UnprocessableEntity(ModelState);
            }
            var garden = await _repository.Garden.GetGardenAsync(gardenId, trackChanges: false);
            if (garden == null)
            {
                _logger.LogInfo($"Garden with id: {gardenId} doesn't exist in the database.");
                return NotFound();
            }
            var plantEntity = _mapper.Map<Plant>(plant);
            _repository.Plant.CreatePlantForCompany(gardenId, plantEntity);
            await _repository.SaveAsync();
            var plantToReturn = _mapper.Map<PlantDto>(plantEntity);
            return CreatedAtRoute("GetPlantForCompany", new
            {
                gardenId,
                id = plantToReturn.Id
            }, plantToReturn);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlantForGarden(Guid gardenId, Guid id)
        {
            var garden = await _repository.Garden.GetGardenAsync(gardenId, trackChanges: false);
            if (garden == null)
            {
                _logger.LogInfo($"Garden with id: {gardenId} doesn't exist in the database.");
                return NotFound();
            }
            var plantForCompany = await _repository.Plant.GetPlantAsync(gardenId, id, trackChanges: false);
            if (plantForCompany == null)
            {
                _logger.LogInfo($"Plant with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Plant.DeletePlant(plantForCompany);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlantForCompany(Guid gardenId, Guid id, [FromBody] PlantForUpdateDto plant)
        {
            if (plant == null)
            {
                _logger.LogError("PlantForUpdateDto object sent from client is null.");
            return BadRequest("PlantForUpdateDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the PlantForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }
            var garden = await _repository.Garden.GetGardenAsync(gardenId, trackChanges: false);
            if (garden == null)
            {
                _logger.LogInfo($"Garden with id: {gardenId} doesn't exist in the database.");
            return NotFound();
            }
            var plantEntity = await _repository.Plant.GetPlantAsync(gardenId, id, trackChanges: true);
            if (plantEntity == null)
            {
                _logger.LogInfo($"Plant with id: {id} doesn't exist in the database.");
            return NotFound();
            }
            _mapper.Map(plant, plantEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdatePlantForGarden(Guid gardenId, Guid id, [FromBody] JsonPatchDocument<PlantForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var garden = await _repository.Garden.GetGardenAsync(gardenId, trackChanges: false);
            if (garden == null)
            {
                _logger.LogInfo($"Garden with id: {gardenId} doesn't exist in the database.");
                return NotFound();
            }
            var plantEntity = await _repository.Plant.GetPlantAsync(gardenId, id, trackChanges: true);
            if (plantEntity == null)
            {
                _logger.LogInfo($"Plant with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var plantToPatch = _mapper.Map<PlantForUpdateDto>(plantEntity);
            patchDoc.ApplyTo(plantToPatch, ModelState);
            TryValidateModel(plantToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }
            _mapper.Map(plantToPatch, plantEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
