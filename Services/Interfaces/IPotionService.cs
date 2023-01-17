using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Services.Interfaces
{
    public interface IPotionService
    {
        Task<List<Potion>> GetAllPotion();
        Task<Potion> AddPotion (Potion potion, Student brewerStudent);
        Task<Potion> BrewPotion(Student student, Ingredient ingredient);
        Task DeletePotion (long id);
        Task UpdatePotion(Potion potion);
        Task<Potion> GetPotion (long id);
        bool IsPotionReplica(Potion potion);
        Task AddNewRecipe (Student student, Ingredient ingredient);
        Task CreateNameRole (Student student, BrewingStatus brewingStatus, int count);
        Task<List<Potion>> GetPotionsByStudent(long studentId);
        Recipe GetRecipeByIngredients(HashSet<Ingredient> ingredients);
        Recipe NewRecipeByPotion(Potion potion);
        int OccurrencesCounter(Potion potion);
    }
}
