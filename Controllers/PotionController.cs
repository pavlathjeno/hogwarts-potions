using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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

        [HttpPost("potions/brew/{studentId:long}")]
        public async Task BrewPotion(long studentId)
        {
            Student brewer = await _studentService.GetStudentById(studentId);
            await _potionService.BrewPotion(brewer);
        }
        [HttpPut("potions/{potionId:long}/add")]
        public async Task AddIngredientToPotion(long potionId, [FromBody] Ingredient ingredient)
        {
            Potion potion = await _potionService.GetPotion(potionId);
            await _potionService.AddNewIngredientToPotion(ingredient, potion);
        }
        [HttpGet("potions/{potionId:long}/help")]
        public async Task<ActionResult<List<Recipe>>> GetAllRecipesByPotionIngredients(long potionId)
        {
            List<Recipe> recipes = await _potionService.GetAllRecipeByIngredients(potionId);
            Potion potion = await _potionService.GetPotion(potionId);
            if (_potionService.IsIngredientsForPotionsNotFull(potion) && _potionService.IsPotionReplica(potion))
            {
                return StatusCode(StatusCodes.Status200OK, recipes);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, "No match found!");
            }
        }
    }
}
