﻿using API.Contracts;
using API.DTOs.Employees;
using API.DTOs.Univers;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _employeeRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            var data = result.Select(x => (EmployeeDto)x);
            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _employeeRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(CreatedEmployeeDto employeeDto)
        {
            var result = _employeeRepository.Create(employeeDto);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok((EmployeeDto)result);
        }

        [HttpPut("{guid}")]
        public IActionResult Update(EmployeeDto employeeDto)
        {

            var existingEmployee = _employeeRepository.GetByGuid(employeeDto.Guid);

            if (existingEmployee == null)
            {
                return NotFound("Employee not found");
            }

            Employee toUpdate = employeeDto;
            toUpdate.CreatedDate = existingEmployee.CreatedDate;

            var result = _employeeRepository.Update(toUpdate);
            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok("Data Updated");
        }



        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var existingEmployee = _employeeRepository.GetByGuid(guid);

            if (existingEmployee is null)
            {
                return NotFound("Employee not found");
            }

            var deleted = _employeeRepository.Delete(existingEmployee);

            if (!deleted)
            {
                return BadRequest("Failed to delete employee");
            }

            return NoContent(); // Kode status 204 No Content untuk sukses penghapusan tanpa respons.
        }

    }
}
