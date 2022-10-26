﻿using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
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
        [HttpGet("{id}")]
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
    }
}
