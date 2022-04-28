using GameListProject.Models;
using GameStoreApi.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Net;
using UserStoreApi.Services;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<UserDatabaseSettings>(
    builder.Configuration.GetSection("Users"));
builder.Services.Configure<GameDatabaseSettings>(
    builder.Configuration.GetSection("Games"));

builder.Services.AddSingleton<UsersService>();
builder.Services.AddSingleton<GamesService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000").AllowAnyHeader();
                          policy.WithOrigins("http://localhost:3000").AllowAnyMethod();
                          policy.WithOrigins("http://localhost:3000").AllowAnyOrigin();
                      });
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();


