using HotelBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            try
            {
                var rooms = await _roomRepository.GetAllRooms();
                return Ok(rooms);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving rooms.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            try
            {
                var room = await _roomRepository.GetRoomById(id);
                if (room == null)
                    return NotFound();

                return Ok(room);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the room.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            try
            {
                if (id != room.RoomId)
                    return BadRequest();

                if (!_roomRepository.RoomExists(id))
                    return NotFound();

                await _roomRepository.UpdateRoom(room);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the room.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            try
            {
                await _roomRepository.CreateRoom(room);
                return CreatedAtAction("GetRoom", new { id = room.RoomId }, room);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the room.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            try
            {
                if (!_roomRepository.RoomExists(id))
                    return NotFound();

                await _roomRepository.DeleteRoom(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the room.");
            }
        }

        [HttpGet("Hotel/{hotelId}/Availability/{availability}")]
        public async Task<ActionResult<IEnumerable<Room>>> GetRoomsByHotelAndAvailability(int hotelId, string availability)
        {
            try
            {
                var rooms = await _roomRepository.GetRoomsByHotelAndAvailability(hotelId, availability);
                return Ok(rooms);
            }
            catch (Exception )
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving rooms by hotel and availability.");
            }
        }

        [HttpGet("Hotel/{hotelId}/AvailableCount")]
        public async Task<ActionResult<int>> GetAvailableRoomCountByHotel(int hotelId)
        {
            try
            {
                var count = await _roomRepository.GetAvailableRoomCountByHotel(hotelId);
                return Ok(count);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the available room count by hotel.");
            }
        }
    }
}
