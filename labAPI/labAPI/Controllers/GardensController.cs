using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
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
        [HttpGet]
        public IActionResult GetGardens()
        {
            var gardens = _repository.Garden.GetAllGardens(trackChanges:false);
            var gardensDto = _mapper.Map<IEnumerable<GardenDto>>(gardens);
            return Ok(gardensDto);
        }
    }
}
