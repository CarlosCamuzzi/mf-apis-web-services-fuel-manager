using mf_apis_web_services_fuel_manager.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(j => j.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// Database Configuration
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.UseAuthorization();

app.MapControllers();

app.Run();


//---------------ANOTAÇÕES
/* Configuração necessária para quando retornar o JSON - quando define o include - não fique em ciclos, ou seja, o veic aponta para o consumo e vice e versa.

Libs:
using System.Text;
using System.Text.Json.Serialization;

builder.Services.AddControllers()
    .AddJsonOptions(j => j.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

 */
