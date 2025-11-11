using System.Text.Json.Serialization;

namespace viesbuciu_rezervacija_backend.Models
{
    public class Hotel : BaseEntity
    {
        public int Id { get; set; }

        public required string Name { get; set; } = String.Empty;
        public required string Description { get; set; } = String.Empty;
        public required string PhoneNumber { get; set; } = String.Empty;
        public required string City { get; set; } = String.Empty;
        public required string Street { get; set; } = String.Empty;
        public required string StreetNumber { get; set; } = String.Empty;
        public bool Parking { get; set; }
        public bool Breakfast { get; set; }

        public HotelType Type { get; set; }
        [JsonIgnore]
        public ApplicationUser? Owner { get; set; }
        public string? OwnerId { get; set; }
        public List<Room> Rooms { get; set; } = new List<Room>();
    }
    public class CreateHotelRequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;
        public string Street { get; set; } = String.Empty;
        public string StreetNumber { get; set; } = String.Empty;
        public bool Parking { get; set; }
        public bool Breakfast { get; set; }
        public string? OwnerId { get; set; }
        public HotelType Type { get; set; }
    }

    public class UpdateHotelRequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;
        public string Street { get; set; } = String.Empty;
        public string StreetNumber { get; set; } = String.Empty;
        public bool Parking { get; set; }
        public bool Breakfast { get; set; }
        public string? OwnerId { get; set; }
        public HotelType Type { get; set; }
    }





}