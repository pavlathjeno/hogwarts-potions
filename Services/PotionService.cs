using System.Collections.Generic;
using System.Linq;
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
                .ToListAsync();
        }

        public async Task<Potion> AddPotion(Potion potion)
        {
            Potion newPotion = new Potion();
            newPotion.BrewStudent = potion.BrewStudent;
            if (IsPotionReplica(potion))
            {
                newPotion.Status = BrewingStatus.Replica;
                newPotion.Recipe = GetRecipeByIngredients(potion.Ingredients);
                newPotion.Ingredients = newPotion.Recipe.Ingredients;
            }
            else
            {
                newPotion.Status = BrewingStatus.Discovery;
                newPotion.Recipe = NewRecipeByPotion(potion);
                var correctIngredients = await GetCorrectIngredients(potion);
                newPotion.Ingredients = correctIngredients.Ingredients;
            }
            newPotion.Name = $"{newPotion.BrewStudent.Name}'s {newPotion.Status} potion #{OccurrencesCounter(newPotion)}";
            await _context.AddAsync(newPotion);
            await _context.SaveChangesAsync();
            return newPotion;
        }
        private async Task<Potion> GetCorrectIngredients(Potion potion)
        {
            Potion newPotion = new Potion();
            foreach (var ingredient in potion.Ingredients)
            {
                if (!IsIngredientPersisted(ingredient))
                {
                    await AddNewIngredient(ingredient);
                    newPotion.Ingredients.Add(ingredient);
                }
                else
                {
                    var existingIngredient = await GetIngredientByName(ingredient);
                    newPotion.Ingredients.Add(existingIngredient);
                }
            }
            return newPotion;
        }
        private async Task<Ingredient> GetIngredientByName(Ingredient ingredient)
        {
            return await _context.Ingredients.FirstOrDefaultAsync(i => i.Name == ingredient.Name);
        }
        private bool IsIngredientPersisted(Ingredient ingredient)
        {
            return _context.Ingredients.Any(i => i.Name == ingredient.Name);
        }


        public async Task AddNewIngredient(Ingredient ingredient)
        {
            await _context.Ingredients.AddAsync(ingredient);
            await _context.SaveChangesAsync();
        }
        public Recipe NewRecipeByPotion(Potion potion)
        {
            Recipe newRecipe = new Recipe();
            newRecipe.Ingredients = potion.Ingredients;
            newRecipe.Student = potion.BrewStudent;
            newRecipe.Name = $"{potion.BrewStudent.Name}'s recipe #{OccurrencesCounter(potion)}";
            return newRecipe;
        }

        public int OccurrencesCounter(Potion potion)
        {
            {
                int baseIndex = 1;
                int count = _context.Potions.Count(p => 
                    p.BrewStudent == potion.BrewStudent && 
                    p.Status == potion.Status) + baseIndex;
                return count;
            }
        }

        public async Task<List<Potion>> GetPotionByStudent(long studentId)
        {
            return await _context.Potions.Where(p => p.BrewStudent.Id == studentId).ToListAsync();
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

        public bool IsPotionReplica(Potion potion)
        {
            return _context.Recipes.Include(r=>r.Ingredients).AsEnumerable()
                .Any(r=>r.Ingredients
                    .Select(i=>i.Name).OrderBy(x=>x)
                    .SequenceEqual(potion.Ingredients
                        .Select(i=>i.Name).OrderBy(y=>y)));
        }

        public Task AddNewRecipe(Student student, Ingredient ingredient)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateNameRole(Student student, BrewingStatus brewingStatus, int count)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Potion>> GetPotionsByStudent(long studentId)
        {
            return await _context.Potions
                .Include(p => p.Ingredients)
                .Include(p => p.Recipe)
                .Where(p=>p.BrewStudent.Id == studentId)
                .ToListAsync();
        }

        public Recipe GetRecipeByIngredients(HashSet<Ingredient> ingredients)
        {
            return _context.Recipes
                .Include(recipe => recipe.Ingredients)
                .AsEnumerable()
                .FirstOrDefault(recipe => recipe.Ingredients
                    .Select(ingredient => ingredient.Name)
                    .OrderBy(x => x)
                    .SequenceEqual(ingredients
                        .Select(ingredient => ingredient.Name)
                        .OrderBy(y => y)));
        }
    }
}
