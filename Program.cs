using Microsoft.EntityFrameworkCore;
// using Trim.Interfaces;
// using Trim.Services;
using CrispCut.Data;
using Swashbuckle.AspNetCore.SwaggerGen;
using CrispCut.Interfaces;
using CrispCut.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

   builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(connectionString));

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

