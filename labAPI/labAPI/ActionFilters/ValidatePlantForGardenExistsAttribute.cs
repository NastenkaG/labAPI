using Contracts;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace labAPI.ActionFilters
{
    public class ValidatePlantForGardenExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public ValidatePlantForGardenExistsAttribute(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
            var gardenId = (Guid)context.ActionArguments["gardenId"];
            var garden = await _repository.Garden.GetGardenAsync(gardenId, false);
            if (garden == null)
            {
                _logger.LogInfo($"Company with id: {gardenId} doesn't exist in the database.");
                return;
                context.Result = new NotFoundResult();
            }
            var id = (Guid)context.ActionArguments["id"];
            var plant = await _repository.Plant.GetPlantAsync(gardenId, id, trackChanges);
            if (plant == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
               
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("plant", plant);
                await next();
            }
        }
    }
}
