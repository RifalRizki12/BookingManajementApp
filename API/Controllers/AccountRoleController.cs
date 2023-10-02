using API.Contracts;
using API.DTOs.AccountRoles;
using API.DTOs.Employees;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountRoleController : ControllerBase
    {

        private readonly IAccountRoleRepository _accountRepository;

        public AccountRoleController(IAccountRoleRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _accountRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            var data = result.Select(x => (AccountRoleDto)x);
            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _accountRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(CreateAccountRoleDto accountRoleDto)
        {
            var result = _accountRepository.Create(accountRoleDto);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok((AccountRoleDto)result);
        }

        [HttpPut("{guid}")]
        public IActionResult Update(AccountRoleDto accountRoleDto)
        {

            var existingEmployee = _accountRepository.GetByGuid(accountRoleDto.Guid);

            if (existingEmployee == null)
            {
                return NotFound("Employee not found");
            }

            AccountRole toUpdate = accountRoleDto;
            toUpdate.CreatedDate = existingEmployee.CreatedDate;

            var result = _accountRepository.Update(toUpdate);
            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok("Data Updated");
        }



        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var existingAccount = _accountRepository.GetByGuid(guid);

            if (existingAccount is null)
            {
                return NotFound("Account not found");
            }

            var deleted = _accountRepository.Delete(existingAccount);

            if (!deleted)
            {
                return BadRequest("Failed to delete account");
            }

            return NoContent(); // Kode status 204 No Content untuk sukses penghapusan tanpa respons.
        }
    }

}
