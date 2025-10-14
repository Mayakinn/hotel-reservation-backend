namespace viesbuciu_rezervacija_backend.Models
{
    public class Review : BaseEntity
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public required string Comment { get; set; } = String.Empty;
        public required int Rating { get; set; }
    }

}