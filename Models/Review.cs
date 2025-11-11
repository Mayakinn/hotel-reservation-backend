namespace viesbuciu_rezervacija_backend.Models
{
    public class Review : BaseEntity
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public required string Comment { get; set; } = string.Empty;
        public required int Rating { get; set; }

        public required string UserId { get; set; }
        public required string ReviewerName { get; set; } = string.Empty;

        public Room Room { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}
