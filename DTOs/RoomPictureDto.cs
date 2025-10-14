namespace viesbuciu_rezervacija_backend.DTOs
{
    public class RoomPictureDto
    {
        public int Id { get; set; }
        public string Url { get; set; } = String.Empty;

        public int RoomId { get; set; }
    }

}