using mf_apis_web_services_fuel_manager.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(j => j.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Servi�o de autentica��o com JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    // Add Jwt Beares
   .AddJwtBearer(options =>
   {
       options.SaveToken = true;
       options.RequireHttpsMetadata = false;       
       options.TokenValidationParameters = new TokenValidationParameters
       {                    
           ValidateIssuer = false,
           ValidateAudience = false,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("F9KZHQ#Yav3DN430vA8m6^7G1Jn*f*M^"))
       };
   });


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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


//---------------ANOTA��ES
/* Configura��o necess�ria para quando retornar o JSON - quando define o include - n�o fique em ciclos, ou seja, o veic aponta para o consumo e vice e versa.

Libs:
using System.Text;
using System.Text.Json.Serialization;

builder.Services.AddControllers()
    .AddJsonOptions(j => j.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

 */

/* -------------------- AUTENTICA��O - JWT
 Adicionando o servi�o de autentica��o com a lib JwtBearer
O c�digo todo fiz que para utilizar uma rota, temos que estar autenticado. Essa autentica��o vem pelo cabe�alho da request, passando o JWT.

1 - Add servi�o d auth
2 - Definindo os esquemas de auth do jwt
3 - AddBearer configuramos para salvar o token
4 - Em Token Validation definimos a chave do token, que precisamos para validar o token. Nesse caso utilizamos 32 caracteres aleat�rio.

A cada request ele valida a auth. O pr�prio framework faz essa verifica��o.

Para finalizar as config, temos que dizer: appUseAuthentication();

Para os microservices, tamb�m precisamos definir essa chave para validar uma auth externa, por exemplo.

 */