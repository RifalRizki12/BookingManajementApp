using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/generic/[controller]")]
    public class GenericController<T> : ControllerBase where T : class
    {
        private readonly IRepository<T> _repository;

        public GenericController(IRepository<T> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _repository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _repository.GetById(guid);
            if (result == null)
            {
                return NotFound("Id Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(T entity)
        {
            var result = _repository.Create(entity);
            if (result == null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok(result);
        }

        [HttpPut("{guid}")]
        public IActionResult Update(Guid guid, [FromBody] T updatedEntity)
        {
            if (updatedEntity == null)
            {
                return BadRequest("Invalid data");
            }

            var existingEntity = _repository.GetById(guid);

            if (existingEntity == null)
            {
                return NotFound("Data not found");
            }

            // Lakukan pembaruan data berdasarkan updatedEntity
            // ...

            var result = _repository.Update(existingEntity);

            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return NoContent(); // Kode status 204 No Content untuk sukses pembaruan tanpa respons.
        }


        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var deleted = _repository.Delete(guid);

            if (!deleted)
            {
                return BadRequest("Failed to delete data");
            }

            return NoContent(); // Kode status 204 No Content untuk sukses penghapusan tanpa respons.
        }

    }

}
