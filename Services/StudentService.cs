using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Services
{
    public class StudentService : IStudentService
    {
        private readonly HogwartsContext _context;

        public StudentService(HogwartsContext context)
        {
            _context = context;
        }
        public async Task<List<Student>> GetAllStudents()
        {
            return await _context.Students
                .Include(s => s.Room)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await _context.Students
                .Include(s => s.Room)
                .FirstAsync(s => s.Id == id);
        }
    }
}
