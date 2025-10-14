using Microsoft.AspNetCore.Mvc;
using viesbuciu_rezervacija_backend.Interfaces;
using viesbuciu_rezervacija_backend.Mappers;
using viesbuciu_rezervacija_backend.Models;
using viesbuciu_rezervacija_backend.DTOs;

namespace viesbuciu_rezervacija_backend.Controllers
{
    [ApiController]
    [Route("api/hotel/{hotelId}/room/{roomId}/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepo;
        private readonly IRoomRepository _roomRepo;
        private readonly IReviewRepository _reviewRepo;

        public ReviewController(
            IHotelRepository hotelRepo,
            IRoomRepository roomRepo,
            IReviewRepository reviewRepo)
        {
            _hotelRepo = hotelRepo;
            _roomRepo = roomRepo;
            _reviewRepo = reviewRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews(int hotelId, int roomId)
        {
            if (!await _hotelRepo.HotelExists(hotelId))
                return NotFound("Hotel not found");

            var room = await _roomRepo.GetByIdAsync(roomId);
            if (room == null || room.HotelId != hotelId)
                return NotFound("Room not found in this hotel");

            var reviews = await _reviewRepo.GetByRoomIdAsync(roomId);
            return Ok(reviews.Select(r => r.ToReviewDto()));
        }

        [HttpGet("{reviewId}")]
        public async Task<IActionResult> GetReviewById(int hotelId, int roomId, int reviewId)
        {
            var review = await _reviewRepo.GetByIdAsync(reviewId);

            if (review == null || review.RoomId != roomId)
                return NotFound();

            return Ok(review.ToReviewDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(
            int hotelId,
            int roomId,
            [FromBody] CreateReviewRequestDto reviewDto)
        {
            if (!await _hotelRepo.HotelExists(hotelId))
                return NotFound("Hotel not found");

            var room = await _roomRepo.GetByIdAsync(roomId);
            if (room == null || room.HotelId != hotelId)
                return NotFound("Room not found in this hotel");

            var reviewModel = reviewDto.ToReviewFromCreate(roomId);
            await _reviewRepo.CreateAsync(reviewModel);

            return CreatedAtAction(nameof(GetReviewById),
                new { hotelId, roomId, reviewId = reviewModel.Id },
                reviewModel.ToReviewDto());
        }

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReview(int hotelId, int roomId, int reviewId)
        {
            var review = await _reviewRepo.GetByIdAsync(reviewId);
            if (review == null || review.RoomId != roomId)
                return NotFound();

            await _reviewRepo.DeleteAsync(review);
            return NoContent();
        }

        [HttpPut("{reviewId}")]
        public async Task<IActionResult> UpdateReview(
    int hotelId,
    int roomId,
    int reviewId,
    [FromBody] UpdateReviewRequestDto dto)
        {
            if (!await _hotelRepo.HotelExists(hotelId))
                return NotFound("Hotel not found");

            var room = await _roomRepo.GetByIdAsync(roomId, true);
            if (room == null || room.HotelId != hotelId)
                return NotFound("Room not found in this hotel");

            var review = room.Reviews.FirstOrDefault(r => r.Id == reviewId);
            if (review == null)
                return NotFound("Review not found in this room");

            review.Comment = dto.Comment;
            review.Rating = dto.Rating;

            var updatedReview = await _reviewRepo.UpdateAsync(review);

            return Ok(updatedReview.ToReviewDto());
        }
    }
}
