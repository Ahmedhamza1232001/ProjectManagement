using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Abstractions.Services;

public interface IAuthService
{
    Task<string> GenerateJwtTokenAsync(User user, CancellationToken cancellationToken = default);
    Task<(string AccessToken, string RefreshToken)> GenerateTokensAsync(User user, CancellationToken cancellationToken = default);
    Task<bool> ValidateRefreshTokenAsync(User user, string refreshToken, CancellationToken cancellationToken = default);
    Task InvalidateRefreshTokenAsync(User user, string refreshToken, CancellationToken cancellationToken = default);
}
