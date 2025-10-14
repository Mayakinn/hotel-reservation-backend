using viesbuciu_rezervacija_backend.DTOs;
using viesbuciu_rezervacija_backend.Models;

namespace viesbuciu_rezervacija_backend.Mappers
{
    public static class RoomPictureMapper
    {
        public static RoomPictureDto ToRoomPictureDto(this RoomPicture roomPicture)
        {
            return new RoomPictureDto
            {
                Id = roomPicture.Id,
                Url = roomPicture.Url,
                RoomId = roomPicture.RoomId
            };
        }
    }
}
