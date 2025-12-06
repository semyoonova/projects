using AspLessons.Abstractions;
using AspLessons.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AspLessons.Helpers
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private JwtConfig _jwtConfig;
        public JwtTokenGenerator(IOptionsMonitor<JwtConfig> configOptions)
        {
            _jwtConfig = configOptions.CurrentValue;
        }
        public string? CreateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new (ClaimTypes.Role, user.Role),
                new (ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var jwt = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtConfig.ExpiredAtMinutes),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.IssuerSignKey)),
                    SecurityAlgorithms.HmacSha256));

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwt);

            return jwtToken;
        }

        
    }
}
