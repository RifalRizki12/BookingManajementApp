using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducataionController : ControllerBase
    {

        private readonly IEducationRepository _educationRepository;

        public EducataionController(IEducationRepository educationRepository)
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
        public IActionResult Create(Education education)
        {
            var result = _educationRepository.Create(education);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok(result);
        }

        [HttpPut("{guid}")]
        public IActionResult Update(Guid guid, [FromBody] Education updatedEducation)
        {
            if (updatedEducation == null)
            {
                return BadRequest("Invalid data");
            }

            var existingEducation = _educationRepository.GetByGuid(guid);

            if (existingEducation == null)
            {
                return NotFound("Education not found");
            }

            // Lakukan pembaruan data berdasarkan updatedEducation
            existingEducation.Major = updatedEducation.Major;
            existingEducation.Degree = updatedEducation.Degree;
            existingEducation.Gpa = updatedEducation.Gpa;
            existingEducation.UniversityGuid = updatedEducation.UniversityGuid;

            var result = _educationRepository.Update(existingEducation);

            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok(existingEducation); // Anda bisa mengembalikan existingEducation yang telah diperbarui jika diperlukan.
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
