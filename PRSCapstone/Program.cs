using Microsoft.EntityFrameworkCore;
using PRSCapstone.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(x => {
    x.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext"));
});

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
