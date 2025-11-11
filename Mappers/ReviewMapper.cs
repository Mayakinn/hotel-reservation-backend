using viesbuciu_rezervacija_backend.DTOs;
using viesbuciu_rezervacija_backend.Models;

namespace viesbuciu_rezervacija_backend.Mappers
{
    public static class ReviewMapper
    {
        public static ReviewDto ToReviewDto(this Review review)
        {
            return new ReviewDto
            {
                Id = review.Id,
                RoomId = review.RoomId,
                Comment = review.Comment,
                Rating = review.Rating,
                CreatedAt = review.CreatedAt,
                ReviewerName = review.ReviewerName,
                UserId = review.UserId
            };
        }

        public static Review ToReviewFromCreate(this CreateReviewRequestDto dto, int roomId, string userId, string reviewerName)
        {
            return new Review
            {
                RoomId = roomId,
                Comment = dto.Comment,
                Rating = dto.Rating,
                CreatedAt = DateTime.UtcNow,
                UserId = userId,
                ReviewerName = reviewerName
            };
        }
    }
}
