using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Repositories
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {

        private readonly IRoomRepository _roomRepository;

        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _roomRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _roomRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Room room)
        {
            var result = _roomRepository.Create(room);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok(result);
        }

        [HttpPut("{guid}")]
        public IActionResult Update(Guid guid, [FromBody] Room updatedRoom)
        {
            if (updatedRoom == null)
            {
                return BadRequest("Invalid data");
            }

            var existingRoom = _roomRepository.GetByGuid(guid);

            if (existingRoom == null)
            {
                return NotFound("Room not found");
            }

            // Lakukan pembaruan data berdasarkan updatedRoom
            existingRoom.Name = updatedRoom.Name;
            existingRoom.Floor = updatedRoom.Floor;
            existingRoom.Capacity = updatedRoom.Capacity;

            var result = _roomRepository.Update(existingRoom);

            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok(existingRoom); // Anda bisa mengembalikan existingRoom yang telah diperbarui jika diperlukan.
        }



        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var existingRoom = _roomRepository.GetByGuid(guid);

            if (existingRoom is null)
            {
                return NotFound("Room not found");
            }

            var deleted = _roomRepository.Delete(existingRoom);

            if (!deleted)
            {
                return BadRequest("Failed to delete room");
            }

            return NoContent(); // Kode status 204 No Content untuk sukses penghapusan tanpa respons.
        }

    }
}
