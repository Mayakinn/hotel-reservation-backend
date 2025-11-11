using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "HotelOwner")]
        public async Task<IActionResult> PostHotel([FromBody] CreateHotelRequestDto hotelDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var hotelModel = hotelDto.ToHotelFromCreateDto();
            hotelModel.OwnerId = userId;
            await _hotelRepo.CreateAsync(hotelModel);
            return CreatedAtAction(nameof(GetById), new { id = hotelModel.Id }, hotelModel);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "HotelOwner")]
        public async Task<IActionResult> UpdateHotel([FromRoute] int id, [FromBody] UpdateHotelRequestDto hotelDto)
        {
            var hotel = await _hotelRepo.GetByIdAsync(id);
            if (hotel == null)
                return NotFound("Hotel not found");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (hotel.OwnerId != userId)
                return StatusCode(StatusCodes.Status403Forbidden, "You can only update your own Hotel.");
            var updatedHotel = await _hotelRepo.UpdateHotel(id, hotelDto);
            return Ok(updatedHotel.ToHotelDto());
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "HotelOwner")]
        public async Task<IActionResult> DeleteHotel([FromRoute] int id)
        {
            var hotel = await _hotelRepo.GetByIdAsync(id);
            if (hotel == null)
                return NotFound("Hotel not found");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (hotel.OwnerId != userId)
                return StatusCode(StatusCodes.Status403Forbidden, "You can only delete your own Hotel.");
            await _hotelRepo.DeleteHotelAsync(id);
            return NoContent();
        }
    }
}
