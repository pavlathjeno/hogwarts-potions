﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Models.Entities
{
    public class Ingredient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public HashSet<Recipe> Recipes { get; set; }
        public HashSet<Potion> Potions { get; set; }

        public Ingredient()
        {
            Recipes = new HashSet<Recipe>();
        }
    }
}
