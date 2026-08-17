using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.API.DTOs.User;
using StudentManagement.API.Entities;
using StudentManagement.API.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public AccountController(IUserRepository userRepository, IPasswordService passwordService, IJwtService jwtService, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto request)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return BadRequest("User already exists");
            }

            //DTO to Entity mapping hai  (Yhn automapper islye ni lagya kynky User entity class m PasswordHash hai aur DTO m Passwprd hai dono ky nam alg tou automapper automatically map ni krpayega)

            //var user = new User
            //{
            //    Email = request.Email,
            //    PasswordHash = _passwordService.HashPassword(request.Password)
            //};

            var user = _mapper.Map<User>(request);
            user.PasswordHash = _passwordService.HashPassword(request.Password);

            await _userRepository.RegisterAsync(user);
            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            var isPasswordValid= _passwordService.VerifyPassword(
                request.Password,
                user.PasswordHash
                );

            if (!isPasswordValid)
            {
                return Unauthorized("Invalid username or password");
            }

            var token = _jwtService.GenerateToken(user);
            return Ok(new
            {
                token = token
            });

        }

    }
}
