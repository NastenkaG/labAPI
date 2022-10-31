using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
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
    }
}
