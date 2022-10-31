using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
        [HttpGet("{id}", Name = "GetPlantForCompany")]
        public IActionResult GetPlantForGarden(Guid gardenId, Guid id)
        {
            var garden = _repository.Garden.GetGarden(gardenId, trackChanges: false);
            if (garden == null)
            {
                _logger.LogInfo($"Garden with id: {gardenId} doesn't exist in the database.");
            return NotFound();
            }
            var plantDb = _repository.Plant.GetPlant(gardenId, id, trackChanges:false);
            if (plantDb == null)
            {
                _logger.LogInfo($"Plant with id: {id} doesn't exist in the database.");
            return NotFound();
            }
            var plant = _mapper.Map<PlantDto>(plantDb);
            return Ok(plant);
        }

        [HttpPost]
        public IActionResult CreatePlantForCompany(Guid gardenId, [FromBody] PlantForCreationDto plant)
        {
            if (plant == null)
            {
                _logger.LogError("PlantForCreationDto object sent from client isnull.");
                return BadRequest("PlantForCreationDto object is null");
            }
            var garden = _repository.Garden.GetGarden(gardenId, trackChanges: false);
            if (garden == null)
            {
                _logger.LogInfo($"Garden with id: {gardenId} doesn't exist in the database.");
                return NotFound();
            }
            var plantEntity = _mapper.Map<Plant>(plant);
            _repository.Plant.CreatePlantForCompany(gardenId, plantEntity);
            _repository.Save();
            var plantToReturn = _mapper.Map<PlantDto>(plantEntity);
            return CreatedAtRoute("GetPlantForCompany", new
            {
                gardenId,
                id = plantToReturn.Id
            }, plantToReturn);
        }
        [HttpDelete("{id}")]
        public IActionResult DeletePlantForCompany(Guid gardenId, Guid id)
        {
            var garden = _repository.Garden.GetGarden(gardenId, trackChanges: false);
            if (garden == null)
            {
                _logger.LogInfo($"Garden with id: {gardenId} doesn't exist in the database.");
                return NotFound();
            }
            var plantForCompany = _repository.Plant.GetPlant(gardenId, id, trackChanges: false);
            if (plantForCompany == null)
            {
                _logger.LogInfo($"Plant with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Plant.DeletePlant(plantForCompany);
            _repository.Save();
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult UpdatePlantForCompany(Guid gardenId, Guid id, [FromBody] PlantForUpdateDto plant)
        {
            if (plant == null)
            {
                _logger.LogError("PlantForUpdateDto object sent from client is null.");
            return BadRequest("PlantForUpdateDto object is null");
            }
            var garden = _repository.Garden.GetGarden(gardenId, trackChanges: false);
            if (garden == null)
            {
                _logger.LogInfo($"Garden with id: {gardenId} doesn't exist in the database.");
            return NotFound();
            }
            var plantEntity = _repository.Plant.GetPlant(gardenId, id, trackChanges: true);
            if (plantEntity == null)
            {
                _logger.LogInfo($"Plant with id: {id} doesn't exist in the database.");
            return NotFound();
            }
            _mapper.Map(plant, plantEntity);
            _repository.Save();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdatePlantForGarden(Guid gardenId, Guid id,
            [FromBody] JsonPatchDocument<PlantForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var garden = _repository.Garden.GetGarden(gardenId, trackChanges: false);
            if (garden == null)
            {
                _logger.LogInfo($"Garden with id: {gardenId} doesn't exist in the database.");
                return NotFound();
            }
            var plantEntity = _repository.Plant.GetPlant(gardenId, id, trackChanges: true);
            if (plantEntity == null)
            {
                _logger.LogInfo($"Plant with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var plantToPatch = _mapper.Map<PlantForUpdateDto>(plantEntity);
            patchDoc.ApplyTo(plantToPatch);
            _mapper.Map(plantToPatch, plantEntity);
            _repository.Save();
            return NoContent();
        }
    }
}
