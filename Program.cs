using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; 
using StudentManagement.API.Data;
using StudentManagement.API.Entities;
using StudentManagement.API.Interfaces;
using StudentManagement.API.Repositories;
using StudentManagement.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllers();
services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
services.AddScoped<IDepartmentRepository,DepartmentRepository>();
services.AddScoped<IStudentRepository, StudentRepository>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
services.AddScoped<IPasswordService, PasswordService>();
services.AddScoped<IJwtService, JwtService>();
services.AddAutoMapper(typeof(Program));

//jb bhi koi client token lekr aye usko validate kesy krna hai uski settings program.cs m configure kri hai
services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey=true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:Key"]!
                )
                )
        };
    });

var app = builder.Build();


// Configure the HTTP request pipeline. 
if (app.Environment.IsDevelopment())
{
    //errors ko detail m dikhao
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
