using Microsoft.EntityFrameworkCore;
using StudentManagement.API.Data;
using StudentManagement.API.Entities;
using StudentManagement.API.Interfaces;
using System.Linq;

namespace StudentManagement.API.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task <(List<Student> Students, int TotalCount)> GetAllAsync(int pageNumber, 
            int pageSize, 
            string? search,
            string? sortBy,
            string? sortOrder,
            int? departmentId)
        {
            //return await _context.Students
            //    .Include(s => s.Department)
            //    .Skip((pageNumber-1)*pageSize)
            //    .Take(pageSize)
            //    .ToListAsync();

            // SEARCHING
            IQueryable<Student> query = _context.Students
                .Include(s => s.Department);     ///eager loading
            if (!string.IsNullOrWhiteSpace(search))
            {
                 query = query.Where(s =>
                    s.studentName.Contains(search) ||
                    s.Email.Contains(search));
            }


            // FILTERING
            if (departmentId.HasValue)
            {
                query = query.Where(s => s.DepartmentId == departmentId.Value);
            }



            // SORTING
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.ToLower()=="studentname")
                {

                    // turnary operators
                    query = sortOrder?.ToLower() == "desc"
                        ? query.OrderByDescending(s => s.studentName)
                        : query.OrderBy(s => s.studentName);    // ascending order
                }

                else if (sortBy.ToLower() == "age")
                {
                    query = sortOrder?.ToLower() == "desc"
                        ? query.OrderByDescending(s => s.Age)
                        : query.OrderBy(s => s.Age);
                }

                else if (sortBy.ToLower() == "email")
                {
                    query = sortOrder?.ToLower() == "des"
                        ? query.OrderByDescending(s => s.Email)
                        : query.OrderBy(s => s.Email);
                }
            }

            // TOTAL COUNT 
            var totalCount = await query.CountAsync();


            // PAGINATION
            var students = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (students, totalCount);
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students.Include(s=>s.Department).FirstOrDefaultAsync(s=>s.Id==id);
        }

        public async Task<Student> AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return false;
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
