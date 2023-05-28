using Microsoft.EntityFrameworkCore;
using pracapiapp.DB;
using pracapiapp.Models;

namespace pracapiapp.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelResDbContext _context;

        public RoomRepository(HotelResDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> GetRoomById(int id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        public async Task CreateRoom(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoom(Room room)
        {
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return;

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }

        public bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.RoomId == id);
        }

        public async Task<int> GetAvailableRoomCountByHotel(int hotelId)
        {
            return await _context.Rooms.CountAsync(r => r.HotelId == hotelId && r.RoomAvailability == "yes");
        }

        public async Task<IEnumerable<Room>> GetRoomsByHotelAndAvailability(int hotelId, string availability)
        {
            return await _context.Rooms.Where(r => r.HotelId == hotelId && r.RoomAvailability == availability).ToListAsync();
        }
    }


}
