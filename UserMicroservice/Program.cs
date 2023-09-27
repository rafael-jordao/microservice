using Data;
using Microsoft.EntityFrameworkCore;
using UserMicroservice.Repositories; // Importe os namespaces necessários
using UserMicroservice.Services; // Importe os namespaces necessários

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Use the connection string directly
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure Entity Framework Core
builder.Services.AddDbContext<UserContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
