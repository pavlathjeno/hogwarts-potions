using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Services.Interfaces
{
    public interface IPotionService
    {
        Task<List<Potion>> GetAllPotion();
        Task<Potion> AddPotion (Potion potion);
        Task<Potion> BrewPotion(Student student);
        Task DeletePotion (long id);
        Task UpdatePotion(Potion potion);
        Task<Potion> GetPotion (long id);
        bool IsPotionReplica(Potion potion);
        Task<List<Potion>> GetPotionsByStudent(long studentId);
        Recipe GetRecipeByIngredients(HashSet<Ingredient> ingredients);
        Recipe NewRecipeByPotion(Potion potion);
        int OccurrencesCounter(Potion potion);
        Task<List<Potion>> GetPotionByStudent(long studentId);
        Task<Potion> AddNewIngredientToPotion(Ingredient newIngredient, Potion potion);
        Task<List<Recipe>> GetAllRecipeByIngredients(long potionId);
        bool IsIngredientsForPotionsNotFull(Potion potion);
    }
}
