using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using viesbuciu_rezervacija_backend.Data;
using viesbuciu_rezervacija_backend.DTOs;
using viesbuciu_rezervacija_backend.Interfaces;
using viesbuciu_rezervacija_backend.Mappers;
using viesbuciu_rezervacija_backend.Models;

namespace viesbuciu_rezervacija_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly HotelDbContext _context;
        private readonly IHotelRepository _hotelRepo;
        public HotelController(HotelDbContext dbContext, IHotelRepository hotelRepository)
        {
            _hotelRepo = hotelRepository;
            _context = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hotels = await _hotelRepo.GetHotelsAsync();
            var hotelDtos = hotels.Select(h => h.ToHotelDto());
            return Ok(hotelDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var hotel = await _hotelRepo.GetByIdAsync(id);
            if (hotel == null)
                return NotFound();

            return Ok(hotel.ToHotelDto());
        }
        [HttpPost]
        public async Task<IActionResult> PostHotel([FromBody] CreateHotelRequestDto hotelDto)
        {
            var hotelModel = hotelDto.ToHotelFromCreateDto();
            await _hotelRepo.CreateAsync(hotelModel);
            return CreatedAtAction(nameof(GetById), new { id = hotelModel.Id }, hotelModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel([FromRoute] int id, [FromBody] UpdateHotelRequestDto hotelDto)
        {
            var hotelModel = await _hotelRepo.UpdateHotel(id, hotelDto);
            if (hotelModel == null)
            {
                return NotFound();
            }
            return Ok(hotelModel.ToHotelDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel([FromRoute] int id)
        {
            var hotelModel = await _hotelRepo.DeleteHotelAsync(id);
            if (hotelModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
