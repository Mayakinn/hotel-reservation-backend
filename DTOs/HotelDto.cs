using viesbuciu_rezervacija_backend.Models;

namespace viesbuciu_rezervacija_backend.DTOs
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string StreetNumber { get; set; } = string.Empty;
        public bool Parking { get; set; }
        public bool Breakfast { get; set; }
        public HotelType Type { get; set; }
        public List<RoomDto> Rooms { get; set; } = new();
        public DateTime CreatedAt { get; set; }

    }


}
