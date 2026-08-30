using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StudentManagement.API.Data;
using StudentManagement.API.Entities;
using StudentManagement.API.Interfaces;
using StudentManagement.API.Mappings;
using StudentManagement.API.Middleware;
using StudentManagement.API.Repositories;
using StudentManagement.API.Services;
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
services.AddExceptionHandler<GlobalExceptionHandler>();   // ASP.NET Core ke DI container ko bata rahe hain ke jab exception handle karni ho, GlobalExceptionHandler use karna.

services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<UserProfile>();
    cfg.AddProfile<StudentProfile>();
    cfg.AddProfile<DepartmentProfile>();
});

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

//services.AddEndpointsApiExplorer();
//services.AddSwaggerGen();

var app = builder.Build();


//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

// Configure the HTTP request pipeline. 
if (app.Environment.IsDevelopment())
{
    //errors ko detail m dikhao
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

//app.UseMiddleware<ExceptionHandlingMiddleware>();   // custom middleware

app.UseExceptionHandler(_ => { });                           // built-in middleware

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
