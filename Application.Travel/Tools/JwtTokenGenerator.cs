using Application.Travel.Dtos;
using Application.Travel.Features.CQRS.Results.UserResults;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Tools 
{ 

    public class JwtTokenGenerator
    {

        private readonly IConfiguration _config;
        public JwtTokenGenerator(IConfiguration config)
        {
            _config = config;
        }
        public JwtTokenGenerator()
        {
            // Parametresiz kurucu gövdesi
        }

        public TokenResponseDto GenerateToken(GetCheckUserResult result)
        {
            var claims=new List<Claim>();
            if (result.RoleId != null)
                claims.Add(new Claim(ClaimTypes.Role, result.RoleId.ToString()));

            claims.Add(new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()));

            if (!string.IsNullOrWhiteSpace(result.Username))
                claims.Add(new Claim("Username",result.Username));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTKey:Secret"]));
            var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var _TokenExpiryTimeInHour = DateTime.UtcNow.AddHours(double.Parse(_config["JWTKey:TokenExpiryTimeInHour"]));

            JwtSecurityToken token = new JwtSecurityToken
                (issuer: _config["JWTKey:ValidIssuer"], audience: _config["JWTKey:ValidAudience"], claims: claims, notBefore: DateTime.UtcNow,
                expires: _TokenExpiryTimeInHour, signingCredentials: signinCredentials);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return new TokenResponseDto(tokenHandler.WriteToken(token), _TokenExpiryTimeInHour);
        }
    }
}
