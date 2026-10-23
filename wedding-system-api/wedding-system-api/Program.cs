using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using wedding_system_api.Data;
using wedding_system_api.InterfaceServices;
using wedding_system_api.Services;
using wedding_system_api.Settings;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<wedding_system_apiContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("wedding_system_apiContext") ?? throw new InvalidOperationException("Connection string 'wedding_system_apiContext' not found.")));

builder.Services.AddScoped<IPresentInterface, PresentService>();

// NOVO: lê a seção "MercadoPago" da configuração (User Secrets em desenvolvimento)
builder.Services.Configure<MercadoPagoSettings>(
    builder.Configuration.GetSection(MercadoPagoSettings.SectionName));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// NOVO: a API não sobe se o token do Mercado Pago não estiver configurado
var mercadoPagoSettings = app.Services.GetRequiredService<IOptions<MercadoPagoSettings>>().Value;
if (string.IsNullOrWhiteSpace(mercadoPagoSettings.AccessToken))
{
    throw new InvalidOperationException(
        "MercadoPago:AccessToken não está configurado. " +
        "Em desenvolvimento use: dotnet user-secrets set \"MercadoPago:AccessToken\" \"SEU_TOKEN\".");
}

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