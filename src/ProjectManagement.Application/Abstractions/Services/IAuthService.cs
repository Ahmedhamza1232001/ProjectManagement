using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Abstractions.Services;

public interface IAuthService
{
    string GenerateJwtToken(User user, CancellationToken cancellationToken = default);
    (string AccessToken, string RefreshToken) GenerateTokens(User user, CancellationToken cancellationToken = default);
    Task<bool> ValidateRefreshTokenAsync(User user, string refreshToken, CancellationToken cancellationToken = default);
    void InvalidateRefreshToken(User user, string refreshToken, CancellationToken cancellationToken = default);
    bool VerifyPassword(string password, string passwordHash);
}
