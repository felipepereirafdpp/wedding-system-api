using Microsoft.EntityFrameworkCore;
using wedding_system_api.Data;
using wedding_system_api.InterfaceServices;
using wedding_system_api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<wedding_system_apiContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("wedding_system_apiContext")
        ?? throw new InvalidOperationException(
            "Connection string 'wedding_system_apiContext' not found.")));

builder.Services.AddScoped<IPresentInterface, PresentService>();

builder.Services.AddControllers();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Teste simples para confirmar que esta aplicação está sendo executada
app.MapGet("/teste", () => "API funcionando!");

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();