using mf_apis_web_services_fuel_manager.Models;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace mf_apis_web_services_fuel_manager.Service
{
    [NotMapped]
    public class TokenService
    {
        public string GenerateJwtToken(Usuario model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("F9KZHQ#Yav3DN430vA8m6^7G1Jn*f*M^");

            var claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
                new Claim(ClaimTypes.Role, model.Perfil.ToString()),
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}

/*---------------------- JWT GenerateJwtToken(Usuario model)
1 - Cria um token
2 - Associa a key 
3 - Inclui os claims (declarações/atributos do usuário), ou seja, se adm, se é user...
4 - Cria o token com os claims, data de expiração, critografa (signingcredentials)
5 - Salva na variável token
6 - Retorna o token

 */