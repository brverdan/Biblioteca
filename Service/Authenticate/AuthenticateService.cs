using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Conta;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Authenticate
{
    public class AuthenticateService
    {
        private readonly IContaRepository _contaRepository;

        private readonly IConfiguration _configuration;

        public AuthenticateService(IContaRepository contaRepository, IConfiguration configurantion)
        {
            _contaRepository = contaRepository;
            _configuration = configurantion;
        }

        public string AuthenticateUser(string email, string password)
        {
            var account = _contaRepository.GetAccountByEmailPassword(email, password);
            
            if(account.Result == null)
            {
                return null;
            }

            return CreateToken(account);
        }

        private string CreateToken(Task<Domain.Conta> conta)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Token:Secret"]);

            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, conta.Result.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, conta.Result.Email));

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Audience = "BIBLIOTECA-API",
                Issuer = "BIBLIOTECA-API"
            };

            var securityToken = tokenHandler.CreateToken(tokenDescription);

            var token = tokenHandler.WriteToken(securityToken); 

            return token;
        }
    }
}
