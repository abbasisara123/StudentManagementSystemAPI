using StudentManagement.API.Entities;

namespace StudentManagement.API.Interfaces
{
    public interface IUserRepository
    {
        Task<User> RegisterAsync(User user);
        Task<User?> GetByEmailAsync(string email);
    }
}
