using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using HogwartsPotions.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Services
{
    public class PotionService : IPotionService
    {
        private readonly HogwartsContext _context;
        public PotionService(HogwartsContext context)
        {
            _context = context;
        }
        public async Task<List<Potion>> GetAllPotion()
        {
            return await _context.Potions
                .Include(p => p.Ingredients)
                .Include(p => p.BrewStudent)
                .Include(p => p.Recipe)
                .ThenInclude(r => r.Ingredients)
                .Include(potion => potion.Recipe).ThenInclude(r => r.Student).ThenInclude(s => s.Room)
                .ToListAsync();
        }

        public Task<Potion> AddPotion(Potion potion)
        {
            throw new System.NotImplementedException();
        }

        public Task<Potion> BrewPotion(Student student, Ingredient ingredient)
        {
            throw new System.NotImplementedException();
        }

        public Task DeletePotion(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdatePotion(Potion potion)
        {
            throw new System.NotImplementedException();
        }

        public Task<Potion> GetPotion(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task IsPotionReplica(Potion potion)
        {
            throw new System.NotImplementedException();
        }

        public Task AddNewRecipe(Student student, Ingredient ingredient)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateNameRole(Student student, BrewingStatus brewingStatus, int count)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Potion>> GetPotionsByStudent(long studentId)
        {
            throw new System.NotImplementedException();
        }
    }
}
