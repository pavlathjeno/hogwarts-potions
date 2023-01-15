using System.Collections.Generic;

namespace HogwartsPotions.Models.Entities
{
    public class Recipe
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Student Student { get; set; }
        public HashSet<Ingredient> Ingredients { get; set; }
    }
}
