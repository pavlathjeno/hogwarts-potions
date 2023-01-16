using System.Collections.Generic;
using System.Linq;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Data
{
    public static class DbInitializer
    {
        public static void Initializer(HogwartsContext context)
        {
            context.Database.EnsureCreated();
            if (context.Students.Any() || context.Rooms.Any())
            {
                return;
            }

            //Initialize students
            var harry = new Student
                { Name = "Harry Potter", HouseType = HouseType.Gryffindor, PetType = PetType.Owl };
            var hermione = new Student
                { Name = "Hermione Granger", HouseType = HouseType.Gryffindor, PetType = PetType.Cat };
            
            var students = new[] { harry, hermione };
            
            context.Students.AddRange(students);
            context.SaveChanges();

            //Initialize rooms
            var room1 = new Room { Capacity = 5, Residents = new HashSet<Student>()};
            var room2 = new Room { Capacity = 5, Residents = new HashSet<Student>()};
            var room3 = new Room { Capacity = 5, Residents = new HashSet<Student>() };
            var room4 = new Room { Capacity = 5, Residents = new HashSet<Student>() };
            room1.Residents.Add(harry);
            room2.Residents.Add(hermione);
            var rooms = new[] {room1, room2, room3, room4 };
            
            context.Rooms.AddRange(rooms);
            context.SaveChanges();

            //Initialize ingredients
            var paprika = new Ingredient { Name = "Paprika" };
            var paradicsom = new Ingredient { Name = "Paradicsom" };
            var hagyma = new Ingredient { Name = "Vöröshagyma" };
            var szalonna = new Ingredient { Name = "Szalonna" };
            var so = new Ingredient { Name = "Só" };
            var bors = new Ingredient { Name = "Bors" };
            var husi = new Ingredient { Name = "Sertéscomb" };
            var pirospaprika = new Ingredient { Name = "Pirospaprika" };

            var ingredients = new[] { paprika, paradicsom, szalonna, so, bors, husi, pirospaprika, hagyma};
            context.Ingredients.AddRange(ingredients);
            context.SaveChanges();

            //Initialize recipes
            var lecso = new Recipe { Name = "Lecsó", Student = hermione, Ingredients = new HashSet<Ingredient> { paprika, paradicsom, szalonna, so, bors } };
            var porkolt = new Recipe { Name = "Sertéspöri", Student = harry, Ingredients = new HashSet<Ingredient> { szalonna, hagyma, husi, paradicsom, so } };

            var recipes = new[] { lecso, porkolt };
            context.Recipes.AddRange(recipes);
            context.SaveChanges();

            var potions = new Potion[]
            {
                new()
                {
                    Status = BrewingStatus.Discovery,
                    BrewStudent = harry,
                    Recipe = porkolt,
                    Ingredients = new HashSet<Ingredient>
                        { szalonna, hagyma, husi, paradicsom, so },
                }
            };

            context.Potions.AddRange(potions);
            context.SaveChanges();
        }
    }
}