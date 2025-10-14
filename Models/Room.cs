namespace viesbuciu_rezervacija_backend.Models
{
    public class Room : BaseEntity
    {
        public int Id { get; set; }
        public required int NumberOfBeds { get; set; }
        public required decimal SquareMeters { get; set; }
        public required bool Toileteries { get; set; }

        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        public List<Review> Reviews { get; set; } = new();
        public List<RoomPicture> Pictures { get; set; } = new();
    }


    public class CreateRoomRequestDto : BaseEntity
    {
        public int Id { get; set; }
        public int NumberOfBeds { get; set; }
        public decimal SquareMeters { get; set; }
        public bool Toileteries { get; set; }

        public int HotelId { get; set; }


    }

    public class UpdateRoomRequestDto
    {
        public int Id { get; set; }
        public int NumberOfBeds { get; set; }
        public decimal SquareMeters { get; set; }
        public bool Toileteries { get; set; }
    }
}