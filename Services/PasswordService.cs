using Microsoft.AspNetCore.Identity;
using StudentManagement.API.Entities;
using StudentManagement.API.Interfaces;

namespace StudentManagement.API.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        public PasswordService(IPasswordHasher<User> passwordHasher) 
        {
            _passwordHasher =passwordHasher;
        }

        //run when registration
        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null,password);  //user object use ni kr ry islye null derhy
        }

        //run when Login
        public bool VerifyPassword(string password, string passwordHash)
        {
            var result = _passwordHasher.VerifyHashedPassword(
                null,  //user object use ni kr ry islye null derhy
                passwordHash,
                password);

            return result== PasswordVerificationResult.Success;
        }
    }
}
