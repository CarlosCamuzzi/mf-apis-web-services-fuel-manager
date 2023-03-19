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

// Serviço de autenticação com JWT
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


//---------------ANOTAÇÕES
/* Configuração necessária para quando retornar o JSON - quando define o include - não fique em ciclos, ou seja, o veic aponta para o consumo e vice e versa.

Libs:
using System.Text;
using System.Text.Json.Serialization;

builder.Services.AddControllers()
    .AddJsonOptions(j => j.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

 */

/* -------------------- AUTENTICAÇÃO - JWT
 Adicionando o serviço de autenticação com a lib JwtBearer
O código todo fiz que para utilizar uma rota, temos que estar autenticado. Essa autenticação vem pelo cabeçalho da request, passando o JWT.

1 - Add serviço d auth
2 - Definindo os esquemas de auth do jwt
3 - AddBearer configuramos para salvar o token
4 - Em Token Validation definimos a chave do token, que precisamos para validar o token. Nesse caso utilizamos 32 caracteres aleatório.

A cada request ele valida a auth. O próprio framework faz essa verificação.

Para finalizar as config, temos que dizer: appUseAuthentication();

Para os microservices, também precisamos definir essa chave para validar uma auth externa, por exemplo.

 */