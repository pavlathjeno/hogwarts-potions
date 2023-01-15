using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IStudentService _studentService;
        private readonly HogwartsContext _context;

        public RoomController(IRoomService roomService, IStudentService studentService, HogwartsContext context)
        {
            _roomService = roomService;
            _studentService = studentService;
            _context = context;
        }

        [HttpGet("rooms")]
        public async Task<List<Room>> GetAllRooms()
        {
            return await _roomService.GetAllRooms();
        }

        [HttpPost("rooms")]
        public async  Task AddRoom([FromBody] Room room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
            //await _roomService.AddRoom(room);
            //return CreatedAtAction("AddRoom", room);
        }

        [HttpGet("rooms/{id:long}")]
        public async Task<Room> GetRoomById(long id)
        {
            return await _roomService.GetRoom(id);
        }

        [HttpPut("rooms/{id}")]
        public void UpdateRoomById(long id, [FromBody] Room updatedRoom)
        {
            _roomService.Update(updatedRoom);
        }

        [HttpDelete("rooms/{id}")]
        public async Task DeleteRoomById(long id)
        {
            await _roomService.DeleteRoom(id);
        }

        [HttpGet("rooms/available")]
        public async Task<List<Room>> GetAvailableRooms()
        {
            return await _roomService.GetAvailableRooms();
        }

        [HttpGet("rooms/rat-owners")]
        public async Task<List<Room>> GetRoomsForRatOwners()
        {
            return await _roomService.GetRoomsForRatOwners();
        }
    }
}
