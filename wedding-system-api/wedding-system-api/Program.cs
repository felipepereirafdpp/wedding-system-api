using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using wedding_system_api.Data;
using wedding_system_api.Services;
using wedding_system_api.Services.InterfaceServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<wedding_system_apiContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("wedding_system_apiContext") ?? throw new InvalidOperationException("Connection string 'wedding_system_apiContext' not found.")));

builder.Services.AddScoped<IPresentInterface, PresentService>();

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

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();