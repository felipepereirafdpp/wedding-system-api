using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using wedding_system_api.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<wedding_system_apiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("wedding_system_apiContext") ?? throw new InvalidOperationException("Connection string 'wedding_system_apiContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
