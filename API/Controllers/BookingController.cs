using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {

        private readonly IBookingRepository _bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _bookingRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _bookingRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Booking booking)
        {
            var result = _bookingRepository.Create(booking);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok(result);
        }

        [HttpPut("{guid}")]
        public IActionResult Update(Guid guid, [FromBody] Booking updatedBooking)
        {
            if (updatedBooking == null)
            {
                return BadRequest("Invalid data");
            }

            var existingBooking = _bookingRepository.GetByGuid(guid);

            if (existingBooking == null)
            {
                return NotFound("Booking not found");
            }

            // Lakukan pembaruan data berdasarkan updatedBooking
            existingBooking.Status = updatedBooking.Status;
            existingBooking.Remarks = updatedBooking.Remarks;
            existingBooking.RoomGuid = updatedBooking.RoomGuid;
            existingBooking.EmployeeGuid = updatedBooking.EmployeeGuid;

            var result = _bookingRepository.Update(existingBooking);

            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok(existingBooking); // Anda bisa mengembalikan existingBooking yang telah diperbarui jika diperlukan.
        }



        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var existingBooking = _bookingRepository.GetByGuid(guid);

            if (existingBooking is null)
            {
                return NotFound("Booking not found");
            }

            var deleted = _bookingRepository.Delete(existingBooking);

            if (!deleted)
            {
                return BadRequest("Failed to delete booking");
            }

            return NoContent(); // Kode status 204 No Content untuk sukses penghapusan tanpa respons.
        }

    }
}
