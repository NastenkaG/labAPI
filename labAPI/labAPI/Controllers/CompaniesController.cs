﻿using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Collections.Specialized.BitVector32;

namespace labAPI.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public CompaniesController(IRepositoryManager repository, ILoggerManager
       logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetCompanies()
        {
            var companies = _repository.Company.GetAllCompanies(trackChanges:false);
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return Ok(companiesDto);
        }
    }
}