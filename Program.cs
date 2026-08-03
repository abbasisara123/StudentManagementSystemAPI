using Microsoft.EntityFrameworkCore;
using StudentManagement.API.Data;
using StudentManagement.API.Interfaces;
using StudentManagement.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllers();
services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
services.AddScoped<IDepartmentRepository,DepartmentRepository>();
services.AddAutoMapper(typeof(Program));








services.AddSwaggerGen();





var app = builder.Build();


// Configure the HTTP request pipeline. 
if (app.Environment.IsDevelopment())
{
    //errors ko detail m dikhao
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
