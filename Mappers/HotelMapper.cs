using viesbuciu_rezervacija_backend.DTOs;
using viesbuciu_rezervacija_backend.Models;

namespace viesbuciu_rezervacija_backend.Mappers
{
    public static class HotelMapper
    {
        public static HotelDto ToHotelDto(this Hotel hotel)
        {
            return new HotelDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Description = hotel.Description,
                PhoneNumber = hotel.PhoneNumber,
                City = hotel.City,
                Street = hotel.Street,
                StreetNumber = hotel.StreetNumber,
                Parking = hotel.Parking,
                Breakfast = hotel.Breakfast,
                Type = hotel.Type,
                Rooms = hotel.Rooms.Select(r => r.ToRoomDto()).ToList(),
                CreatedAt = hotel.CreatedAt,
            };
        }

        public static Hotel ToHotelFromCreateDto(this CreateHotelRequestDto hotelDto)
        {
            return new Hotel
            {
                Name = hotelDto.Name,
                Description = hotelDto.Description,
                PhoneNumber = hotelDto.PhoneNumber,
                City = hotelDto.City,
                Street = hotelDto.Street,
                StreetNumber = hotelDto.StreetNumber,
                Parking = hotelDto.Parking,
                Breakfast = hotelDto.Breakfast,
                Type = hotelDto.Type,

            };
        }
    }
}