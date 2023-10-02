using API.Contracts;
using API.DTOs.Educations;
using API.DTOs.Employees;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {

        private readonly IEducationRepository _educationRepository;

        public EducationController(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _educationRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            var data = result.Select(x => (EducationDto)x);
            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _educationRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(CreateEducationDto educationDto)
        {
            var result = _educationRepository.Create(educationDto);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok((EducationDto)result);
        }

        [HttpPut]
        public IActionResult Update(EducationDto educationDto)
        {

            var existingEmployee = _educationRepository.GetByGuid(educationDto.Guid);

            if (existingEmployee == null)
            {
                return NotFound("Employee not found");
            }

            Education toUpdate = educationDto;
            toUpdate.CreatedDate = existingEmployee.CreatedDate;

            var result = _educationRepository.Update(toUpdate);
            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok("Data Updated");
        }


        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var existingEducation = _educationRepository.GetByGuid(guid);

            if (existingEducation is null)
            {
                return NotFound("Education not found");
            }

            var deleted = _educationRepository.Delete(existingEducation);

            if (!deleted)
            {
                return BadRequest("Failed to delete education");
            }

            return NoContent(); // Kode status 204 No Content untuk sukses penghapusan tanpa respons.
        }

    }
}
