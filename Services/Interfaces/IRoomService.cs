using HogwartsPotions.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HogwartsPotions.Services.Interfaces
{
    public interface IRoomService
    {
        Task<List<Room>> GetAllRooms();
        Task AddRoom(Room room);
        Task<Room> GetRoom(long id);
        Task Update(Room room);
        Task DeleteRoom(long id);
        Task<List<Room>> GetAvailableRooms();
        Task<List<Room>> GetRoomsForRatOwners();
    }
}
