using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
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
        [HttpGet("{id}")]
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
    }
}
