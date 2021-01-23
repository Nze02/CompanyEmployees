using AutoMapper;
using CompanyEmployees.Contracts;
using CompanyEmployees.DataTransferObjects;
using CompanyEmployees.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEmployees.Controllers
{

    [ApiController]
    [Route("api/companies/{companyId}/employees")]
    public class EmployeesController : ControllerBase
    {
        private IRepositoryManager _repository;
        private ILoggerManager _logger;
        private IMapper _mapper;

        public EmployeesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetEmployeesForCompany(Guid companyId) 
        {
            var company = _repository.Company.GetCompany(companyId, false);
            if(company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }

            var employees = _repository.Employee.GetEmployees(companyId, trackChanges: false);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return Ok(employeesDto);
        }


        [HttpGet("{employeeId}")]
        public IActionResult GetEmployee(Guid companyId, Guid employeeId) 
        {
            var company = _repository.Company.GetCompany(companyId, false);
            if (company == null) 
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }

            var employee = _repository.Employee.GetEmployee(companyId, employeeId, false);
            if (employee == null)
            {
                _logger.LogInfo($"Employee with id: {employeeId} doesn't exist in the database."); return NotFound(); 
            }
            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return Ok(employeeDto);
        }
    }
}
