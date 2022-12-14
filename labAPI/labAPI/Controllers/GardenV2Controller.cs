using Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace labAPI.Controllers
{
    [ApiVersion("2.0", Deprecated = true)]

    [Route("api/gardens")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    public class GardenV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        public GardenV2Controller(IRepositoryManager repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetGardens()
        {
            var garden = await _repository.Garden.GetAllGardensAsync(trackChanges:false);
            return Ok(garden);
        }
    }
}
