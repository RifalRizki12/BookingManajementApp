using API.Contracts;
using API.DTOs.Rooms;
using API.DTOs.Rooms;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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

            var data = result.Select(x => (RoomDto)x);

            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _roomRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok((RoomDto)result);
        }

        [HttpPost]
        public IActionResult Create(CreateRoomDto roomDto)
        {
            var result = _roomRepository.Create(roomDto);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok((RoomDto)result);
        }

        [HttpPut]
        public IActionResult Update(RoomDto roomDto)
        {
            var entity = _roomRepository.GetByGuid(roomDto.Guid);
            if (entity is null)
            {
                return NotFound("Id Not Found");
            }

            Room toUpdate = roomDto;
            toUpdate.CreatedDate = entity.CreatedDate;

            var result = _roomRepository.Update(toUpdate);
            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok("Data Updated");

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
