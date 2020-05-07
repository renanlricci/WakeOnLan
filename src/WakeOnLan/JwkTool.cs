using Microsoft.IdentityModel.Tokens;
using Raspberry.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Raspberry
{
    public class JwtTool
    {
        private readonly JwtConfig _config;

        public JwtTool(JwtConfig config)
        {
            _config = config;
        }

        public string GenerateToken(string username, params string[] userClaims)
        {
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Secret));
            var now = DateTime.Now;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
            };

            claims.AddRange(userClaims.Select(claim => new Claim(ClaimTypes.Role, claim)));

            var specifications = new JwtSecurityToken(
                _config.Issuer,
                _config.Audience,
                claims,
                now,
                now.AddMinutes(_config.Expiration),
                new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(specifications);
        }
    }
}
