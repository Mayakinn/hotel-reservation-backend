using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using viesbuciu_rezervacija_backend.Data;
using viesbuciu_rezervacija_backend.Interfaces;
using viesbuciu_rezervacija_backend.Mappers;
using viesbuciu_rezervacija_backend.Models;

namespace viesbuciu_rezervacija_backend.Controllers
{
    [Route("api/hotel/{hotelId}/room")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepo;
        private readonly IHotelRepository _hotelRepo;

        private readonly HotelDbContext _context;
        private readonly BlobService _blobService;

        public RoomController(
            IRoomRepository roomRepo,
            IHotelRepository hotelRepo,
            HotelDbContext context,
            BlobService blobService)
        {
            _roomRepo = roomRepo;
            _hotelRepo = hotelRepo;
            _context = context;
            _blobService = blobService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllForHotel([FromRoute] int hotelId)
        {
            if (!await _hotelRepo.HotelExists(hotelId))
                return NotFound("Hotel not found.");

            var rooms = await _roomRepo.GetAllByHotelAsync(hotelId);
            return Ok(rooms.Select(r => r.ToRoomDto()));
        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetRoomById(
            int hotelId,
            int roomId,
            [FromQuery] bool includeReviews = false,
            [FromQuery] bool includePictures = false)
        {
            var room = await _roomRepo.GetByIdAsync(roomId, includeReviews, includePictures);

            if (room == null || room.HotelId != hotelId)
                return NotFound();

            var dto = room.ToRoomDetailedDto(includeReviews, includePictures);

            return Ok(dto);
        }
        [Authorize(Roles = "HotelOwner")]
        [HttpDelete("{roomId}/pictures/{pictureId}")]
        public async Task<IActionResult> DeleteRoomImage(int roomId, int pictureId)
        {
            var room = await _roomRepo.GetByIdAsync(roomId, includePictures: true);
            if (room == null)
                return NotFound("Room not found");

            var picture = room.Pictures.FirstOrDefault(p => p.Id == pictureId);
            if (picture == null)
                return NotFound("Picture not found");

            _context.RoomPictures.Remove(picture);
            await _context.SaveChangesAsync();

            await _blobService.DeleteAsync(picture.Url);

            return NoContent();
        }



        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromRoute] int hotelId, [FromBody] CreateRoomRequestDto dto)
        {
            if (!await _hotelRepo.HotelExists(hotelId))
                return BadRequest("Hotel doesn't exist!");

            var roomModel = dto.ToRoomFromCreate(hotelId);
            await _roomRepo.CreateAsync(roomModel);

            return CreatedAtAction(nameof(GetRoomById),
                new { hotelId = hotelId, roomId = roomModel.Id },
                roomModel.ToRoomDto());
        }

        [HttpPut("{roomId}")]
        public async Task<IActionResult> UpdateRoom(
    int hotelId,
    int roomId,
    [FromBody] UpdateRoomRequestDto roomDto)
        {
            if (!await _hotelRepo.HotelExists(hotelId))
                return NotFound("Hotel not found");

            var existingRoom = await _roomRepo.GetByIdAsync(roomId);
            if (existingRoom == null || existingRoom.HotelId != hotelId)
                return NotFound("Room not found in this hotel");

            existingRoom.NumberOfBeds = roomDto.NumberOfBeds;
            existingRoom.SquareMeters = roomDto.SquareMeters;
            existingRoom.Toileteries = roomDto.Toileteries;

            var updatedRoom = await _roomRepo.UpdateAsync(existingRoom);

            return Ok(updatedRoom.ToRoomDto());
        }

        [HttpDelete("{roomId}")]
        public async Task<IActionResult> DeleteRoom(int hotelId, int roomId)
        {
            if (!await _hotelRepo.HotelExists(hotelId))
                return NotFound("Hotel not found");

            var room = await _roomRepo.GetByIdAsync(roomId);
            if (room == null || room.HotelId != hotelId)
                return NotFound("Room not found in this hotel");

            await _roomRepo.DeleteAsync(roomId);
            return NoContent();
        }

        [Authorize(Roles = "HotelOwner")]
        [HttpPost("{roomId}/upload-image")]
        public async Task<IActionResult> UploadRoomImage(
            int roomId,
            IFormFile imageFile,
            [FromServices] BlobService blobService)
        {
            if (imageFile == null || imageFile.Length == 0)
                return BadRequest("No file uploaded");

            var room = await _roomRepo.GetByIdAsync(roomId, includePictures: true);
            if (room == null)
                return NotFound("Room not found");

            var imageUrl = await blobService.UploadAsync(imageFile, $"rooms/{roomId}");
            room.Pictures.Add(new RoomPicture
            {
                Url = imageUrl,
                RoomId = roomId,
                Room = room
            });


            await _roomRepo.SaveAsync();

            return Ok(new { url = imageUrl });
        }



    }

}