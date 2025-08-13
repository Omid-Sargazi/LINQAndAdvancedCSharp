using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAuthLab.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthLab.Services
{
    public sealed class TokenService
    {
        private readonly JwtOptions _opt;
        private readonly SymmetricSecurityKey _signingKey;
        public TokenService(IOptions<JwtOptions> options)
        {
            _opt = options.Value;
            _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.Secret));
        }


        public string IssueAccessToken(string userId, string userName, IEnumerable<string> roles)
        {
            var creds = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub,userId),
                new(ClaimTypes.Name,userName),
                new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat,DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())
            };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));


            var token = new JwtSecurityToken
            (
                issuer: _opt.Issuer,
                audience: _opt.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_opt.AccessTokenMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public TokenValidationParameters GetValidationParameters() => new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _opt.Issuer,
            ValidateAudience = true,
            ValidAudience = _opt.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _signingKey,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(2)
        };


    }
}