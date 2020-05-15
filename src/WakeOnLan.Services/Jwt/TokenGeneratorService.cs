using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WakeOnLan.CrossCutting.Configuration;
using WakeOnLan.Domain.Interfaces.Services;

namespace WakeOnLan.Services.Jwt
{
    public sealed class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly AppSettings _appSettings;

        public TokenGeneratorService(IOptionsMonitor<AppSettings> appSettings)
        {
            _appSettings = appSettings.CurrentValue;
        }
        public async Task<string> GenerateToken(string username, params string[] userClaims)
        {
            return await Task.Run(() =>
            {
                var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtSettings.Secret));
                var now = DateTime.Now;
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };

                if (userClaims != null)
                    claims.AddRange(userClaims.Select(claim => new Claim(ClaimTypes.Role, claim)));

                var specifications = new JwtSecurityToken(
                    _appSettings.JwtSettings.Issuer,
                    _appSettings.JwtSettings.Audience,
                    claims,
                    now,
                    now.AddMinutes(_appSettings.JwtSettings.Expiration),
                    new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256));

                return new JwtSecurityTokenHandler().WriteToken(specifications);
            });
        }
    }
}
