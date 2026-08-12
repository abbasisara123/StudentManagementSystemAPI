using Microsoft.IdentityModel.Tokens;
using StudentManagement.API.Entities;
using StudentManagement.API.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentManagement.API.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!
                    )
                );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
                );

            // JWT Token ka object
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:issuer"],        //token kis API ne banaya
                audience: _configuration["Jwt:audience"],    //token kis client ke liye hai
                claims: claims,                              //kis user ki information token mein hai
                expires: DateTime.UtcNow.AddHours(1),        //token kab expire hoga
                signingCredentials: credentials              //token ko kis key + algorithm se sign karna hai
                );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
