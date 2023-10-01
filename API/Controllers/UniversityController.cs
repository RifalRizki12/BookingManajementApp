using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityRepository _universityRepository;

        public UniversityController(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _universityRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _universityRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(University university)
        {
            var result = _universityRepository.Create(university);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok(result);
        }

        [HttpPut("{guid}")]
        public IActionResult Update(Guid guid, [FromBody] University updatedUniversity)
        {
            if (updatedUniversity == null)
            {
                return BadRequest("Invalid data");
            }

            var existingUniversity = _universityRepository.GetByGuid(guid);

            if (existingUniversity == null)
            {
                return NotFound("University not found");
            }

            // Lakukan pembaruan data berdasarkan updatedUniversity
            existingUniversity.Code = updatedUniversity.Code;
            existingUniversity.Name = updatedUniversity.Name;

            var result = _universityRepository.Update(existingUniversity);

            if (result == null)
            {
                return BadRequest("Failed to update data");
            }

            return Ok(result);
        }


        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var existingUniversity = _universityRepository.GetByGuid(guid);

            if (existingUniversity is null)
            {
                return NotFound("University not found");
            }

            var deleted = _universityRepository.Delete(existingUniversity);

            if (!deleted)
            {
                return BadRequest("Failed to delete university");
            }

            return NoContent(); // Kode status 204 No Content untuk sukses penghapusan tanpa respons.
        }

    }
}
