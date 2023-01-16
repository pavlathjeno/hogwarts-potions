using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.Data;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using HogwartsPotions.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Services
{
    public class RoomService : IRoomService
    {
        private readonly HogwartsContext _context;

        public RoomService(HogwartsContext context)
        {
            _context = context;
        }
        public async Task<List<Room>> GetAllRooms()
        {
            return await _context.Rooms.Include(r=>r.Residents).ToListAsync();
        }

        public async Task AddRoom(Room room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
        }

        public async Task<Room> GetRoom(long id)
        {
            return await _context.Rooms.Include(r => r.Residents).FirstOrDefaultAsync(room => room.Id == id);
        }

        public async Task Update(Room room)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoom(long id)
        {
            var room = await GetRoom(id);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Room>> GetAvailableRooms()
        {
            return await _context.Rooms.Include(r => r.Residents).Where(r => r.Capacity > r.Residents.Count).ToListAsync();
        }

        public async Task<List<Room>> GetRoomsForRatOwners()
        {
            return await _context.Rooms
                .Include(r => r.Residents)
                .Where(r => !r.Residents
                .Any(re => re.PetType == PetType.Cat || re.PetType == PetType.Owl)).ToListAsync();
        }
    }
}
