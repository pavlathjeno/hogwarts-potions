using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HogwartsPotions.Controllers
{
    [ApiController]
    public class PotionController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IStudentService _studentService;
        private readonly HogwartsContext _context;
        private readonly IPotionService _potionService;

        public PotionController(IRoomService roomService, IStudentService studentService, HogwartsContext context, IPotionService potionService)
        {
            _roomService = roomService;
            _studentService = studentService;
            _context = context;
            _potionService = potionService;
        }
        [HttpGet("potions")]
        public async Task<List<Potion>> GetAllPotion()
        {
            return await _potionService.GetAllPotion();
        }
        [HttpPost("potions")]
        public async Task AddPotion([FromBody] Potion potion)
        {
            potion.BrewStudent = await _studentService.GetStudentById(potion.BrewStudent.Id);
            await _potionService.AddPotion(potion);
        }
        [HttpGet("potions/{studentId:long}")]
        public async Task<List<Potion>> GetPotionByStudent(long studentId)
        {
            return await _potionService.GetPotionByStudent(studentId);
        }
    }
}
