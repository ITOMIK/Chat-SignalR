using Chat_SignalR.Data;
using Chat_SignalR.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Chat_SignalR.Services
{
    public class JwtService
    {
        private readonly AuthOptions _jwtOptions;
        private readonly ILogger<JwtService> _logger;
        public JwtService(IOptions<AuthOptions> jwtopt, ILogger<JwtService> logger)
        {
            _jwtOptions = jwtopt.Value;
            _logger = logger;
        }
        public string GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>{
                new("userId", user.publicId.ToString())
            };
            foreach (var role in user.roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }
            var signingCredentital = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key)), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(signingCredentials: signingCredentital,
                expires: DateTime.UtcNow.AddHours(_jwtOptions.TokenLifetimeHours),
                claims: claims.ToArray(),
                issuer: _jwtOptions.Issuer,     
                audience: _jwtOptions.Audience
                );
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }
    }
}
