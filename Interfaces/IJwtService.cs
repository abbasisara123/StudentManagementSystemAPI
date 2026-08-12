using StudentManagement.API.Entities;

namespace StudentManagement.API.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
