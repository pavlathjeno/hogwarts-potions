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

            var hermione = new Student
                { Name = "Harry Potter", HouseType = HouseType.Gryffindor, PetType = PetType.Owl };
            var harry = new Student
                { Name = "Hermione Granger", HouseType = HouseType.Gryffindor, PetType = PetType.Cat };
            
            var students = new[] { harry, hermione };
            
            context.Students.AddRange(students);
            context.SaveChanges();

            var room1 = new Room { Capacity = 5, Residents = new HashSet<Student>()};
            var room2 = new Room { Capacity = 5, Residents = new HashSet<Student>()};
            var room3 = new Room { Capacity = 5, Residents = new HashSet<Student>() };
            var room4 = new Room { Capacity = 5, Residents = new HashSet<Student>() };
            room1.Residents.Add(harry);
            room2.Residents.Add(hermione);
            var rooms = new[] {room1, room2, room3, room4 };
            
            context.Rooms.AddRange(rooms);
            context.SaveChanges();
        }
    }
}
