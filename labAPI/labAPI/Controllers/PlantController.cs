using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using labAPI.ActionFilters;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        /*[HttpGet]
        public async Task<IActionResult> GetPlants()
        {
            var plant = await _repository.Plant.GetPlantsAsync(trackChanges:false);
            var plantDto = _mapper.Map<IEnumerable<PlantDto>>(plant);
            return Ok(plantDto);
        }*/

        [HttpGet]
        public async Task<IActionResult> GetPlantForGarden(Guid gardenId,
            [FromQuery] PlantParameters plantParameters)
        {
            var garden = await _repository.Garden.GetGardenAsync(gardenId, trackChanges: false);
            if (garden == null)
            {
                _logger.LogInfo($"Garden with id: {gardenId} doesn't exist in the database.");
                return NotFound();
            }
            var plantFromDb = await _repository.Plant.GetPlantsAsync(gardenId, plantParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(plantFromDb.MetaData));
            var plantDto = _mapper.Map<IEnumerable<PlantDto>>(plantFromDb);
            return Ok(plantDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreatePlantForGarden(Guid gardenId, [FromBody] PlantForCreationDto plant)
        {
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
        [ServiceFilter(typeof(ValidatePlantForGardenExistsAttribute))]
        public async Task<IActionResult> DeletePlantForGarden(Guid gardenId, Guid id)
        {
            var plantForCompany = HttpContext.Items["plant"] as Plant;
            _repository.Plant.DeletePlant(plantForCompany);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidatePlantForGardenExistsAttribute))]
        public async Task<IActionResult> UpdatePlantForCompany(Guid gardenId, Guid id, [FromBody] PlantForUpdateDto plant)
        {
            var plantEntity = HttpContext.Items["plant"] as Plant;
            _mapper.Map(plant, plantEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPatch("{id}")]
        [ServiceFilter(typeof(ValidatePlantForGardenExistsAttribute))]
        public async Task<IActionResult> PartiallyUpdatePlantForGarden(Guid gardenId, Guid id, [FromBody] JsonPatchDocument<PlantForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var plantEntity = HttpContext.Items["plant"] as Plant;
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
