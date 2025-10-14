namespace viesbuciu_rezervacija_backend.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }
        public int NumberOfBeds { get; set; }
        public decimal SquareMeters { get; set; }
        public bool Toileteries { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedAt { get; set; }
    }


    public class RoomDetailedDto
    {
        public int Id { get; set; }
        public int NumberOfBeds { get; set; }
        public decimal SquareMeters { get; set; }
        public bool Toileteries { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<ReviewDto> Reviews { get; set; } = new();
        public List<RoomPictureDto> Pictures { get; set; } = new();
    }

}