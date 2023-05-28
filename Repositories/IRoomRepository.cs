using pracapiapp.Models;

namespace pracapiapp.Repositories
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllRooms();
        Task<Room> GetRoomById(int id);
        Task CreateRoom(Room room);
        Task UpdateRoom(Room room);
        Task DeleteRoom(int id);
        bool RoomExists(int id);
        Task<int> GetAvailableRoomCountByHotel(int hotelId);
        Task<IEnumerable<Room>> GetRoomsByHotelAndAvailability(int hotelId, string availability);
    }


}
