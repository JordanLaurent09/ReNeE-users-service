using Microsoft.EntityFrameworkCore;
using Npgsql;
using users_service.Database.Context;
using users_service.Database.Entities;
using users_service.Database.Repositories;
using users_service.Database.Repositories.Interfaces;
using users_service.Resources.Users;
using users_service.Resources.Users.Interfaces;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(@"Host=localhost:6433;Port=5432;Username=maykl;Password=sandman;Database=users_db");
});


builder.Services.AddControllers();

builder.Services.AddScoped<IRepository<User, string>, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

WebApplication app = builder.Build();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
