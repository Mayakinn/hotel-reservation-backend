namespace viesbuciu_rezervacija_backend.Models
{
    public class RoomPicture : BaseEntity
    {
        public int Id { get; set; }
        public string Url { get; set; } = String.Empty;

        public int RoomId { get; set; }
        public required Room Room { get; set; }
    }

}