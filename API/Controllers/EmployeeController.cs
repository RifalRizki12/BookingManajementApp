using API.Contracts;
using API.Models;
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
        public IActionResult Create([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _employeeRepository.Create(employee);

            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok(result);
        }

        [HttpPut("{guid}")]
        public IActionResult Update(Guid guid, [FromBody] Employee updatedEmployee)
        {
            if (updatedEmployee == null)
            {
                return BadRequest("Invalid data");
            }

            var existingEmployee = _employeeRepository.GetByGuid(guid);

            if (existingEmployee == null)
            {
                return NotFound("Employee not found");
            }

            // Lakukan pembaruan data berdasarkan updatedEmployee
            existingEmployee.Nik = updatedEmployee.Nik;
            existingEmployee.FirstName = updatedEmployee.FirstName;
            existingEmployee.LastName = updatedEmployee.LastName;
            existingEmployee.BirthDate = updatedEmployee.BirthDate;
            existingEmployee.Gender = updatedEmployee.Gender;
            existingEmployee.HiringDate = updatedEmployee.HiringDate;
            existingEmployee.Email = updatedEmployee.Email;
            existingEmployee.PhoneNumber = updatedEmployee.PhoneNumber;

            var result = _employeeRepository.Update(existingEmployee);

            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok(existingEmployee); // Anda bisa mengembalikan existingEmployee yang telah diperbarui jika diperlukan.
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
