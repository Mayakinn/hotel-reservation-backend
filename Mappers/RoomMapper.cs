using viesbuciu_rezervacija_backend.DTOs;
using viesbuciu_rezervacija_backend.Models;

namespace viesbuciu_rezervacija_backend.Mappers
{
    public static class RoomMapper
    {
        public static RoomDto ToRoomDto(this Room room)
        {
            return new RoomDto
            {
                Id = room.Id,
                NumberOfBeds = room.NumberOfBeds,
                SquareMeters = room.SquareMeters,
                Toileteries = room.Toileteries,
                HotelId = room.HotelId,
                CreatedAt = room.CreatedAt
            };
        }

        public static RoomDetailedDto ToRoomDetailedDto(this Room room, bool includeReviews, bool includePictures)
        {
            return new RoomDetailedDto
            {
                Id = room.Id,
                NumberOfBeds = room.NumberOfBeds,
                SquareMeters = room.SquareMeters,
                Toileteries = room.Toileteries,
                HotelId = room.HotelId,
                Reviews = includeReviews
                    ? room.Reviews.Select(r => r.ToReviewDto()).ToList()
                    : new List<ReviewDto>(),
                Pictures = includePictures
                    ? room.Pictures.Select(p => p.ToRoomPictureDto()).ToList()
                    : new List<RoomPictureDto>()
            };
        }


        public static Room ToRoomFromCreate(this CreateRoomRequestDto dto, int hotelId)
        {
            return new Room
            {
                NumberOfBeds = dto.NumberOfBeds,
                SquareMeters = dto.SquareMeters,
                Toileteries = dto.Toileteries,
                HotelId = hotelId
            };
        }
    }
}