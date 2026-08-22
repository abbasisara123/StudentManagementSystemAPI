using StudentManagement.API.DTOs.Student;
using StudentManagement.API.Entities;

namespace StudentManagement.API.Interfaces
{
    public interface IStudentRepository
    {
        Task <(List<Student>Students, int TotalCount)> GetAllAsync(int pageNumber, 
            int pageSize, 
            string? search,
            string? sortBy,       //  kis feild ky acc sort krna hai
            string? sortOrder,
            int? departmentId);   //  kis direction m sort krna hai?  (asc or dsc)

        Task<Student?> GetByIdAsync(int id);
        Task<Student> AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task<bool> DeleteAsync(int id);
    }
}
