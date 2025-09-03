using ProjectManagement.Application.Abstractions.Services;
using ProjectManagement.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProjectManagement.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _jwtSecret = "YOUR_SUPER_SECRET_KEY_12345"; // Move to configuration
        private readonly int _jwtExpiryMinutes = 60;
        private readonly int _refreshTokenExpiryDays = 7;

        public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        public bool VerifyPassword(string password, string hashed) => BCrypt.Net.BCrypt.Verify(password, hashed);

        public string GenerateJwtToken(User user, CancellationToken cancellationToken = default)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSecret);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username)
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtExpiryMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public (string AccessToken, string RefreshToken) GenerateTokens(User user, CancellationToken cancellationToken = default)
        {
            var accessToken = GenerateJwtToken(user, cancellationToken);
            var refreshToken = Guid.NewGuid().ToString(); // Simplified, can store in DB
            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays)
            });

            return (accessToken, refreshToken);
        }

        public async Task<bool> ValidateRefreshTokenAsync(User user, string refreshToken, CancellationToken cancellationToken = default)
        {
            var token = user.RefreshTokens.Find(t => t.Token == refreshToken);
            return token != null && token.ExpiresAt > DateTime.UtcNow;
        }

        public  void InvalidateRefreshToken(User user, string refreshToken, CancellationToken cancellationToken = default)
        {
            var token = user.RefreshTokens.Find(t => t.Token == refreshToken);
            if (token != null)
            {
                user.RefreshTokens.Remove(token);
            }
        }
    }
}
