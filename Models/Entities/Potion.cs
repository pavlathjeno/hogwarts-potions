using System.Collections.Generic;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Models.Entities
{
    public class Potion
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Student BrewStudent { get; set; }
        public HashSet<Ingredient> Ingredients { get; set; }
        public BrewingStatus Status { get; set; } = BrewingStatus.Brew;
        public Recipe Recipe { get; set; }

        public Potion()
        {
            Ingredients = new HashSet<Ingredient>();
        }
    }
}
