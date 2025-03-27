using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JWTAuth.Models;
using JWTAuth.Business;
using JWTAuth.Business.HotelService.Interface;

namespace JWTAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelRepository;

        public HotelController(IHotelService hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        // GET: api/hotel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
            return Ok(await _hotelRepository.GetHotelsAsync());
        }

        // GET: api/hotel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(id);
            if (hotel == null)
                return NotFound();
            return Ok(hotel);
        }

        // POST: api/hotel
        [HttpPost]
        public async Task<ActionResult<Hotel>> CreateHotel(Hotel hotel)
        {
            var newHotel = await _hotelRepository.CreateHotelAsync(hotel);
            return CreatedAtAction(nameof(GetHotel), new { id = newHotel.Id }, newHotel);
        }

        // PUT: api/hotel/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, Hotel hotel)
        {
            if (id != hotel.Id)
                return BadRequest("Hotel ID mismatch");

            await _hotelRepository.UpdateHotelAsync(hotel);
            return NoContent();
        }

        // DELETE: api/hotel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var deleted = await _hotelRepository.DeleteHotelAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
