using API.Contracts;
using API.Models;
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
        public IActionResult Create(AccountRole account)
        {
            var result = _accountRepository.Create(account);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok(result);
        }

        [HttpPut("{guid}")]
        public IActionResult Update(Guid guid, [FromBody] AccountRole updatedAccount)
        {
            if (updatedAccount == null)
            {
                return BadRequest("Invalid data");
            }

            var existingAccount = _accountRepository.GetByGuid(guid);

            if (existingAccount == null)
            {
                return NotFound("Account not found");
            }

            // Lakukan pembaruan data berdasarkan updatedAccount
            existingAccount.RoleGuid = updatedAccount.RoleGuid;

            var result = _accountRepository.Update(existingAccount);

            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok(existingAccount); // Anda bisa mengembalikan existingAccount yang telah diperbarui jika diperlukan.
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
