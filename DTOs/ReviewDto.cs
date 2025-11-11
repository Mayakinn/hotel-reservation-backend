namespace viesbuciu_rezervacija_backend.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }

        public string ReviewerName { get; set; } = string.Empty;
        public string? UserId { get; set; }
    }

    public class CreateReviewRequestDto
    {
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; }
    }

    public class UpdateReviewRequestDto
    {
        public int Id { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; }
    }
}
