using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using System.Reflection;
using System.Text.Json.Serialization;
using users_service.Database.Context;
using users_service.Database.Entities;
using users_service.Database.Repositories;
using users_service.Database.Repositories.Interfaces;
using users_service.Resources.Users;
using users_service.Resources.Users.Interfaces;


Logger logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(@"Host=postgresDb-users;Username=maykl;Password=sandman;Database=users_db");
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddSwaggerGen(options =>
{
    string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<UsersPerformers>, UsersPerformersRepository>();
builder.Services.AddScoped<IRepository<UsersAlbums>, UsersAlbumsRepository>();
builder.Services.AddScoped<IRepository<UsersSongs>, UsersSongsRepository>();
builder.Services.AddScoped<IRepository<Photo>, PhotoRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUsersPerformersService, UsersPerformersService>();
builder.Services.AddScoped<IUsersSongsService, UsersSongsService>();
builder.Services.AddScoped<IUsersAlbumsService, UsersAlbumsService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();

WebApplication app = builder.Build();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");

app.Run();
