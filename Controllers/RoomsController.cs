using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pracapiapp.DB;
using pracapiapp.Models;
using pracapiapp.Repositories;

namespace pracapiapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;

        public RoomsController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            var rooms = await _roomRepository.GetAllRooms();
            return Ok(rooms);
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var room = await _roomRepository.GetRoomById(id);
            if (room == null)
                return NotFound();

            return Ok(room);
        }

        // PUT: api/Rooms/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            if (id != room.RoomId)
                return BadRequest();

            if (!_roomRepository.RoomExists(id))
                return NotFound();

            await _roomRepository.UpdateRoom(room);
            return NoContent();
        }

        // POST: api/Rooms
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            await _roomRepository.CreateRoom(room);
            return CreatedAtAction("GetRoom", new { id = room.RoomId }, room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            if (!_roomRepository.RoomExists(id))
                return NotFound();

            await _roomRepository.DeleteRoom(id);
            return NoContent();
        }

        // GET: api/Rooms/Hotel/5/Availability/Available
        [HttpGet("Hotel/{hotelId}/Availability/{availability}")]
        public async Task<ActionResult<IEnumerable<Room>>> GetRoomsByHotelAndAvailability(int hotelId, string availability)
        {
            var rooms = await _roomRepository.GetRoomsByHotelAndAvailability(hotelId, availability);
            return Ok(rooms);
        }

        // GET: api/Rooms/Hotel/5/AvailableCount
        [HttpGet("Hotel/{hotelId}/AvailableCount")]
        public async Task<ActionResult<int>> GetAvailableRoomCountByHotel(int hotelId)
        {
            var count = await _roomRepository.GetAvailableRoomCountByHotel(hotelId);
            return Ok(count);
        }
    }


}
