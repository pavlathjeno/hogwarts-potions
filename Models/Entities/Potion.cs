using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Models.Entities
{
    public class Potion
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Brewer student is required!")]
        public Student BrewStudent { get; set; }
        public string Name => $"{BrewStudent.Name}'s potion.";
        public HashSet<Ingredient> Ingredients { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BrewingStatus Status { get; set; } = BrewingStatus.Brew;
        public Recipe Recipe { get; set; }

        public Potion()
        {
            Ingredients = new HashSet<Ingredient>();
        }
    }
}
