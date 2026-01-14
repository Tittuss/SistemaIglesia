using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(AppDbContext context) : base(context)
        {
        }

        public Task<IEnumerable<Course>> GetCoursesByTeacherIdAsync(Guid teacherId)
        {
            return _context.Courses
                .Where(c => c.TeacherId == teacherId)
                .ToListAsync()
                .ContinueWith(t => (IEnumerable<Course>)t.Result);
        }

        public Task<Course?> GetCourseWithDetailsAsync(Guid courseId)
        {
            return _context.Courses
                .Include(c => c.Teacher)
                .Include(c => c.Enrollments)
                .ThenInclude(e => e.Student)
                .FirstOrDefaultAsync(c => c.Id == courseId);
        }

        public async Task<IEnumerable<Course>> GetAllWithDetailsAsync()
        {
            return await _context.Courses
                .Include(c => c.Teacher)
                .Include(c => c.AcademicPeriod)
                .ToListAsync();
        }
    }
}
